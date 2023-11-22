using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for DeptIntegrationChallans
/// </summary>
public abstract class DeptIntegrationChallans
{
    #region Properties
    public Dictionary<string, IntegrationProp> Data { get; set; }
    public string AUIN { get; set; }
    public string ChallanType { get; set; }
    public int DeptCode { get; set; }
    public int ManualBanksType { get; set; }
    public double HeadTotalAmount { get; set; }
    public DataTable dtCheckHeads { get; set; }
    public int MerchantCode { get; set; }

    #endregion
    public static DeptIntegrationChallans SelectChallanType(string ChallanType)
    {
        Dictionary<string, DeptIntegrationChallans> Integration = new Dictionary<string, DeptIntegrationChallans>()
        {
            { "0", new GeneralIntegration() },
            { "OFFICEXML", new MultipleOfficeIntegration() },
            { "PD", new PDIntegration() },
            { "ME", new MEIntegration() }
           // { "MH", new MHIntegration() }  // Multiple Head Condition 21 April 2020
        };
        return Integration[ChallanType.ToUpper()];
    }
    public DeptIntegrationChallans()
    {

    }
    public virtual bool CheckIntegrationData(string PlainText)
    {
        SplitToProperties(PlainText);
        CheckMerchantCode();
        checkAUIN();
        if ( Data["Filler"].Value.ToString() != "A" && !Data["Filler"].Value.ToString().Contains("PD"))
            CheckDivisionCode();
        CheckOfcTreasDeptMapping();
        CheckDiscountAmt();
        CheckBudgetHeadConditions(ChallanType);
        CheckAmount();
        getSchemadt();
        return true;
    }
    public Int64 InsertChallan()
    {
        SqlParameter[] PM = new SqlParameter[Data.Count + 1];
        string[] keynames = Data.Keys.ToArray();
        if (Data["Filler"].Value.ToString() == "A")
            Data["Filler"].Value = "0";
        for (int i = 1; i <= Data.Count; i++)
        {
            PM[i] = new SqlParameter();
            PM[i].ParameterName = Data[keynames[i - 1]].ParameterName;
            PM[i].SqlDbType = Data[keynames[i - 1]].DbType;
            PM[i].Size = Data[keynames[i - 1]].Size;
            PM[i].Value = Data[keynames[i - 1]].Value;
        }
        EgIntegrationChallanBL objEgIntegrationChallanBL = new EgBL.EgIntegrationChallanBL();
        objEgIntegrationChallanBL.PM = PM;
        return objEgIntegrationChallanBL.InsertDynamicChallan();
    }
    #region Conditions
    public bool IsAlphaNumeric(String strToCheck)
    {
        Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
        return !objAlphaNumericPattern.IsMatch(strToCheck);
    }
    public void checkAUIN()
    {
        string msg = string.Empty;
        if (AUIN != Data["RefNumber"].Value.ToString())
        {
            throw new Exception("AUIN Number in plaintext is not matching with post AUIN number.");
        }
        if (!IsAlphaNumeric(AUIN))
        {
            throw new Exception("AUIN Number Should Be Alphanumeric.");
        }
    }
    public void CheckMerchantCode()
    {
        if (MerchantCode != Convert.ToInt32(Data["MerchantCode"].Value))
        {
            throw new Exception("Merchant Code in plaintext is not matching with post Merchant Code.");
        }
    }

    /// <summary>
    /// Check Division Code Exist Or Not if filler is passed with office value
    /// </summary>
    public virtual void CheckDivisionCode()
    {
        EgIntegrationChallanBL objEgIntegrationChallanBL = new EgBL.EgIntegrationChallanBL();
        objEgIntegrationChallanBL.Location = Data["Location"].Value.ToString();
        objEgIntegrationChallanBL.filler = Data["Filler"].Value.ToString();
        int DivCode = objEgIntegrationChallanBL.GetDivisionCode();
        Data.Add("DivCode", new IntegrationProp() { Value = DivCode, ParameterName = "@DivCode", DbType = SqlDbType.Int, Size = 4 });
        if (DivCode == 0)
        {
            throw new Exception("Division code Not exist For particular office");
        }
    }
    /// <summary>
    /// Check Office Treasury and Department mapping
    /// </summary>
    public virtual void CheckOfcTreasDeptMapping()
    {
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.TreasuryCode = Data["Location"].Value.ToString();
        objEgCheckBudgetHead.OfficeId = Convert.ToInt32(Data["OfficeName"].Value);
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        if (objEgCheckBudgetHead.VarifyOfficeId() != 1)
        {
            throw new Exception("OfficeId not map with Treasury and Department Integration");
        }
    }
    /// <summary>
    /// Check If Discount amount is greater than total Amount
    /// </summary>
    public virtual void CheckDiscountAmt()
    {
        if (Convert.ToDouble(Data["DeductCommission"].Value) > Convert.ToDouble(Data["TotalAmount"].Value))
        {
            throw new Exception("Discount can not be greater than Total Amount");
        }
    }

    public virtual void CheckBudgetHeadConditions(string ChallanType)
    {
        DataTable dt = new DataTable();
        GetBudgetHeadDt();
        HeadsCondition();
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        objEgCheckBudgetHead.ChallanType = ChallanType;
        dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
        if (dt.Rows.Count <= 0)
            throw new Exception("DataTable not found");
    }

    public  void HeadsCondition()
    {
        if (dtCheckHeads.Rows.Count > 0)
        {
            if (dtCheckHeads.Rows[0][1].ToString().Substring(0, 4) != "0030" && Convert.ToDouble(Data["DeductCommission"].Value) > 0)
            {
                throw new Exception("Discount not allowed.");
            }
            if ((Convert.ToInt32(dtCheckHeads.Rows[0][1].ToString().Substring(0, 4)) > 7999) && dtCheckHeads.Rows.Count > 1)
            {
                throw new Exception("Multiple Heads are not allowed.");
            }
            var duplicateHead = dtCheckHeads.AsEnumerable().Select(row => new { BudgetHead = row.Field<string>("BudgetHead"), ScheCode = row.Field<string>("ScheCode") }).Distinct().LongCount();
            if (dtCheckHeads.Rows.Count != duplicateHead)
            {
                throw new Exception("Duplicates BudgetHead Found.");
            }
            var MajorHeadCount = dtCheckHeads.AsEnumerable().Select(row => new { MajorHead = row.Field<string>("BudgetHead").Substring(0, 4) }).Distinct().LongCount();
            if(MajorHeadCount > 1)
            {
                throw new Exception("BudgetHead with multiple MajorHead not allowed.");
            }
            if ((dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001080000" || dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001090000") && ( Data["Filler"].Value.ToString() == "A" || Data["Filler"].Value.ToString().Contains("PD")))  //  Division Code  Compulsory for  This Budgethead  17 dec 2019 Add 109 12 jan 2021
            {
                throw new Exception("Division Code Compulsory With This Head");

            }
        }
    }
    public virtual void CheckAmount()
    {
        if (HeadTotalAmount - Convert.ToDouble(Data["DeductCommission"].Value) != Convert.ToDouble(Data["TotalAmount"].Value))
        {
            throw new Exception("Amount mismatch.");
        }
    }
    #endregion
    protected void createTable()
    {
        dtCheckHeads = new DataTable();
        dtCheckHeads.Columns.Add(new DataColumn("SNo", Type.GetType("System.Int64")));
        dtCheckHeads.Columns.Add(new DataColumn("BudgetHead", Type.GetType("System.String")));
        dtCheckHeads.Columns.Add(new DataColumn("ScheCode", Type.GetType("System.String")));
        dtCheckHeads.Columns.Add(new DataColumn("Amount", System.Type.GetType("System.Double")));
    }
    private void getSchemadt()
    {
        DataTable schemaAmtTable = new DataTable();
        schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
        schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));

        for (int i = 0; i < dtCheckHeads.Rows.Count; i++)
        {
            DataRow dtRow;
            dtRow = schemaAmtTable.NewRow();
            dtRow[0] = DeptCode;
            dtRow[1] = Convert.ToInt32(dtCheckHeads.Rows[i][2]);
            dtRow[2] = Convert.ToDouble(dtCheckHeads.Rows[i][3]);
            dtRow[3] = Convert.ToInt32(Data["UserId"].Value);
            dtRow[4] = dtCheckHeads.Rows[i][1].ToString();
            schemaAmtTable.Rows.Add(dtRow);
            schemaAmtTable.AcceptChanges();
        }
        Data.Add("mytable", new IntegrationProp() { Value = schemaAmtTable, ParameterName = "@mytable", DbType = SqlDbType.Structured, Size = ((sizeof(Int32)*3)+sizeof(double)+26)*9 });
    }

    public abstract void GetBudgetHeadDt();
    public abstract void SplitToProperties(string PlainText);
}
[System.Serializable]
public class IntegrationProp
{
    public string ParameterName { get; set; }
    public SqlDbType DbType { get; set; }
    public int Size { get; set; }
    public object Value { get; set; }
}