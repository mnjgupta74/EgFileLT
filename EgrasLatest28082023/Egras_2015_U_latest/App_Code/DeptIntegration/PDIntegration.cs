using EgBL;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for PDIntegration
/// </summary>
public class PDIntegration : DeptIntegrationChallans
{
    string[][] ChallanValues;
    public PDIntegration()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override void CheckBudgetHeadConditions(string ChallanType)
    {
        DataTable dt = new DataTable();
        GetBudgetHeadDt();
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.BudgetHead = dtCheckHeads.Rows[0][1].ToString();
        objEgCheckBudgetHead.PDAcc = Convert.ToInt32(Data["PdAcc"].Value);
        objEgCheckBudgetHead.PayMode = Data["Paymenttype"].Value.ToString();
        objEgCheckBudgetHead.TreasuryCode = Data["Location"].Value.ToString();
        bool res = objEgCheckBudgetHead.VerifyPdAcc();
        if (!res)
            throw new Exception("PD Account not mapped with BudgetHead and treasury, Or Online Payment Not Allowed.");

        HeadsCondition();
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        objEgCheckBudgetHead.ChallanType = ChallanType;
        dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
        if (dt.Rows.Count <= 0)
            throw new Exception("DataTable not found");
        if (Data["Paymenttype"].Value.ToString().Trim() == "M")
        {
            bool result = GetPDRunMode();
            if (result)
                throw new Exception(" This Pd Account Associate with online Payment Purpose Only !!");
        }
    }
    public override void GetBudgetHeadDt()
    {
        createTable();
        int sno = 1;
        double amount = 0;
        for (int i = 1; i <= 17; i += 2)
        {
            if (ChallanValues[i][1].ToString().Trim() != "0" && Convert.ToDouble(ChallanValues[i + 1][1].ToString().Trim()) > 0 && Convert.ToDouble(ChallanValues[i + 1][1]) > 0)
            {
                DataRow dr = dtCheckHeads.NewRow();
                dr["SNo"] = Convert.ToInt32(sno);
                dr["BudgetHead"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(0, 13));
                dr["ScheCode"] = Convert.ToString(ChallanValues[i][1].ToString().Trim().Substring(13, 5));
                dr["Amount"] = Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                amount = amount + Convert.ToDouble(ChallanValues[i + 1][1].ToString());
                dtCheckHeads.Rows.Add(dr);
                sno++;
            }
        }
        HeadTotalAmount = amount;
    }
    private bool GetPDRunMode()
    {
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.PDAcc = Convert.ToInt32(Data["PdAcc"].Value);
        return objEgCheckBudgetHead.CheckPDRunMode();
    }

    public override void SplitToProperties(string PlainText)
    {
        EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
        Data = new System.Collections.Generic.Dictionary<string, IntegrationProp>();
        if (!PlainText.Contains("PD:"))
        {
            throw new Exception("Invalid PD account data.");
        }
        ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
        Data.Add("MerchantCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.merchantCode(ChallanValues[22][1].ToString()), ParameterName = "@MerchantCode", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("Paymenttype", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pType(ChallanValues[23][1].ToString()), ParameterName = "@Paymenttype", DbType = SqlDbType.Char, Size = 1 });
        Data.Add("Identity", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.identity(ChallanValues[24][1].ToString()), ParameterName = "@Identity", DbType = SqlDbType.VarChar, Size = 15 });
        Data.Add("Location", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.location(ChallanValues[25][1].ToString()), ParameterName = "@Location", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("FullName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.fullName(ChallanValues[19][1].ToString()), ParameterName = "@FullName", DbType = SqlDbType.VarChar, Size = 50 });
        Data.Add("DeductCommission", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.deductCommission(ChallanValues[20][1].ToString()), ParameterName = "@DeductCommission", DbType = SqlDbType.Money });
        objEgDeptIntegrationPropBL.districtCode(ChallanValues[26][1].ToString());
        Data.Add("OfficeName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.officeCode(ChallanValues[27][1].ToString(), false), ParameterName = "@OfficeName", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("ChallanFromMonth", new IntegrationProp() { Value = Convert.ToDateTime(ChallanValues[29][1].ToString()), ParameterName = "@ChallanFromMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("ChallanToMonth", new IntegrationProp() { Value = Convert.ToDateTime((ChallanValues[30][1].ToString())), ParameterName = "@ChallanToMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("Address", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.address(ChallanValues[31][1].ToString()), ParameterName = "@Address", DbType = SqlDbType.VarChar, Size = 100 });
        Data.Add("PINCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pincode(ChallanValues[32][1].ToString()), ParameterName = "@PINCode", DbType = SqlDbType.Char, Size = 6 });
        Data.Add("City", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.city(ChallanValues[33][1].ToString()), ParameterName = "@City", DbType = SqlDbType.VarChar, Size = 40 });
        Data.Add("Remarks", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.remarks(ChallanValues[34][1].ToString()), ParameterName = "@Remarks", DbType = SqlDbType.VarChar, Size = 200 });
        Data.Add("RefNumber", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString()), ParameterName = "@RefNumber", DbType = SqlDbType.VarChar, Size = 50 });
        Data.Add("TotalAmount", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.amount(ChallanValues[21][1].ToString()), ParameterName = "@TotalAmount", DbType = SqlDbType.Money });
        Data.Add("ChallanYear", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[36][1].ToString()), ParameterName = "@ChallanYear", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("Filler", new IntegrationProp() { Value = 'A', ParameterName = "@Filler", DbType = SqlDbType.Int, Size = 4 });
        DeptCode = objEgDeptIntegrationPropBL.departmentCode(ChallanValues[28][1].ToString());
        Data.Add("Profile", new IntegrationProp() { Value = 0, ParameterName = "@Profile", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("UserId", new IntegrationProp() { Value = System.Web.HttpContext.Current.Session["UserId"], ParameterName = "@UserId", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("PdAcc", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.PdAcc(ChallanValues[35][1].ToString().Split(':').GetValue(1).ToString().Trim()), ParameterName = "@PdAcc", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("IpAddress", new IntegrationProp() { Value = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString(), ParameterName = "@IpAddress", DbType = SqlDbType.NVarChar, Size = 50 });
        }
}