using EgBL;
using System;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for MultipleOfficeIntegration
/// </summary>
public class MultipleOfficeIntegration : DeptIntegrationChallans
{
    string[][] ChallanValues;
    public MultipleOfficeIntegration()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override void CheckOfcTreasDeptMapping()
    {
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.TreasuryCode = Data["Location"].Value.ToString();
        objEgCheckBudgetHead.OfficeId = Convert.ToInt32(Data["OfficeName"].Value);
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        int res = objEgCheckBudgetHead.VerifyMultipleOfficeId(ChallanType);
        if (res == -2)
        {
            throw new Exception("OfficeId not map with Treasury and Department Integration");
        }
        else if (res == -3)
        {
            throw new Exception("Department Mismatch");
        }
        else if (res == -1)
        {
            throw new Exception("Division Code does not exist");
        }
        else
        {
            getXmlDt(ChallanType);
           
        }
    }
    public override void CheckBudgetHeadConditions(string ChallanType)
    {
        DataTable dt = new DataTable();
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        GetBudgetHeadDt();
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        //int res = objEgCheckBudgetHead.CheckMultiOfcHeads(ChallanType, dtCheckHeads);
        //if (res > 0)
        //{
        //    throw new Exception("BudgetHead Mismatch");
        //}
        HeadsCondition();
        dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
        if (dt.Rows.Count <= 0)
            throw new Exception("DataTable not found");

    }
    public override void CheckAmount()
    {
        if (HeadTotalAmount - Convert.ToDouble(Data["DeductCommission"].Value) != Convert.ToDouble(Data["TotalAmount"].Value))
        {
            throw new Exception("Amount mismatch.");
        }
        else
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            int Res = objEgCheckBudgetHead.CheckOfficeAmountMismatch(ChallanType, HeadTotalAmount);
            if (Res == 1)
            {
                throw new Exception("Office Amount mismatch.");
            }
        }
    }
    public override void CheckDivisionCode()
    {}
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

    public override void SplitToProperties(string PlainText)
    {
        EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
        Data = new System.Collections.Generic.Dictionary<string, IntegrationProp>();
        ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
        Data.Add("MerchantCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.merchantCode((ChallanValues[22][1].ToString())), ParameterName = "@MerchantCode", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("Paymenttype", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pType(ChallanValues[23][1].ToString()), ParameterName = "@Paymenttype", DbType = SqlDbType.Char, Size = 1 });
        Data.Add("Identity", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.identity(ChallanValues[24][1].ToString()), ParameterName = "@Identity", DbType = SqlDbType.VarChar, Size = 15 });
        Data.Add("Location", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.location(ChallanValues[25][1].ToString()), ParameterName = "@Location", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("FullName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.fullName(ChallanValues[19][1].ToString()), ParameterName = "@FullName", DbType = SqlDbType.VarChar, Size = 50 });
        Data.Add("DeductCommission", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.deductCommission((ChallanValues[20][1].ToString())), ParameterName = "@DeductCommission", DbType = SqlDbType.Money, Size = 8 });
        //Data.Add("DistrictCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.districtCode(Convert.ToInt32(ChallanValues[26][1].ToString())), ParameterName = "@DistrictCode", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("OfficeName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.officeCode((ChallanValues[27][1].ToString()), true), ParameterName = "@OfficeName", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("ChallanFromMonth", new IntegrationProp() { Value = Convert.ToDateTime(ChallanValues[29][1].ToString()), ParameterName = "@ChallanFromMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("ChallanToMonth", new IntegrationProp() { Value = Convert.ToDateTime((ChallanValues[30][1].ToString())), ParameterName = "@ChallanToMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("Address", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.address(ChallanValues[31][1].ToString()), ParameterName = "@Address", DbType = SqlDbType.VarChar, Size = 100 });
        Data.Add("PINCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pincode(ChallanValues[32][1].ToString()), ParameterName = "@PINCode", DbType = SqlDbType.Char, Size = 6 });
        Data.Add("City", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.city(ChallanValues[33][1].ToString()), ParameterName = "@City", DbType = SqlDbType.VarChar, Size = 40 });
        Data.Add("Remarks", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.remarks(ChallanValues[34][1].ToString()), ParameterName = "@Remarks", DbType = SqlDbType.VarChar, Size = 200 });
        Data.Add("RefNumber", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString()), ParameterName = "@RefNumber", DbType = SqlDbType.VarChar, Size = 50 });
        Data.Add("TotalAmount", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.amount((ChallanValues[21][1].ToString())), ParameterName = "@TotalAmount", DbType = SqlDbType.Money, Size = 8 });
        Data.Add("ChallanYear", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[36][1].ToString()), ParameterName = "@ChallanYear", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("Filler", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.filler(ChallanValues[35][1].ToString()), ParameterName = "@Filler", DbType = SqlDbType.Int, Size = 4 });
        DeptCode = objEgDeptIntegrationPropBL.departmentCode((ChallanValues[28][1].ToString()));
        Data.Add("Profile", new IntegrationProp() { Value = 0, ParameterName = "@Profile", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("UserId", new IntegrationProp() { Value = System.Web.HttpContext.Current.Session["UserId"], ParameterName = "@UserId", DbType = SqlDbType.Int, Size = 4 });
        //Data.Add("xmlOffices", new IntegrationProp() { Value = ChallanType, ParameterName = "@xmlOffices", DbType = SqlDbType.Xml, Size = 100000 });
        if (Data["Filler"].Value.ToString().Contains("PD"))
        {
            throw new Exception("PD Not Allowed.");
        }
        if (Data["OfficeName"].Value.ToString().Trim() != "0")
        {
            throw new Exception("Invalid Data in Office of Plaintext");
        }
    }
    private void getXmlDt(string ChallanType)
    {
        DataTable schemaXmlTable = new DataTable();
        schemaXmlTable.Columns.Add(new DataColumn("OfficeId", System.Type.GetType("System.Int32")));
        schemaXmlTable.Columns.Add(new DataColumn("Treasury", System.Type.GetType("System.String")));
        schemaXmlTable.Columns.Add(new DataColumn("Amount", System.Type.GetType("System.Double")));
        schemaXmlTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));
        schemaXmlTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
        schemaXmlTable.Columns.Add(new DataColumn("DivCode", System.Type.GetType("System.Int32")));
        schemaXmlTable.Columns.Add(new DataColumn("Filler", System.Type.GetType("System.Int32")));

        DataTable xmldt = new DataTable();
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        xmldt = objEgCheckBudgetHead.GetXmlDatatable(ChallanType);

        for (int i = 0; i < xmldt.Rows.Count; i++)
        {
            DataRow dtRow;
            dtRow = schemaXmlTable.NewRow();
            dtRow[0] = Convert.ToInt32(xmldt.Rows[i][0]); ;
            dtRow[1] = xmldt.Rows[i][1];
            dtRow[2] = Convert.ToDouble(xmldt.Rows[i][2]);
            dtRow[3] = xmldt.Rows[i][3].ToString();
            dtRow[4] = Convert.ToInt32(xmldt.Rows[i][4].ToString());
            dtRow[5] = Convert.ToInt32(xmldt.Rows[i][5].ToString());
            dtRow[6] = Convert.ToInt32(xmldt.Rows[i][6].ToString());

            schemaXmlTable.Rows.Add(dtRow);
            schemaXmlTable.AcceptChanges();
        }
        Data.Add("xmlDatatable", new IntegrationProp() { Value = schemaXmlTable, ParameterName = "@xmlDatatable", DbType = SqlDbType.Structured, Size = ((sizeof(Int32) * 3) + sizeof(double) + 26) * 9 });
    }
    public new void HeadsCondition()
    {
        if (dtCheckHeads.Rows.Count > 0)
        {
            if (dtCheckHeads.Rows[0][1].ToString().Substring(0, 4) != "0030" && Convert.ToDouble(Data["DeductCommission"].Value) > 0)
            {
                throw new Exception("Discount not allowed.");
            }
            //if ((dtCheckHeads.Rows[0][1].ToString().Substring(0, 4) == "0030" || Convert.ToInt32(dtCheckHeads.Rows[0][1].ToString().Substring(0, 4)) > 7999) && dtCheckHeads.Rows.Count > 1)
            //{
            //    throw new Exception("Multiple Heads are not allowed.");
            //}
            //var duplicateHead = dtCheckHeads.AsEnumerable().Select(row => new { BudgetHead = row.Field<string>("BudgetHead"), ScheCode = row.Field<string>("ScheCode") }).Distinct().LongCount();
            //if (dtCheckHeads.Rows.Count != duplicateHead)
            //{
            //    throw new Exception("Duplicates BudgetHead Found.");
            //}
            //if ((dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001080000" || dtCheckHeads.Rows[0][1].ToString().Substring(0, 13) == "8443001090000") && (Data["Filler"].Value.ToString() == "A" || Data["Filler"].Value.ToString().Contains("PD")))  //  Division Code  Compulsory for  This Budgethead  17 dec 2019
            //{
            //    throw new Exception("Division Code Compulsory With This Head");

            //}
        }
    }
}