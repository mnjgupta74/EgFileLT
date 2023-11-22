using EgBL;
using System;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for MEIntegration
/// </summary>
public class MEIntegration : DeptIntegrationChallans
{
    string[][] ChallanValues;
    public MEIntegration()
    {
        ManualBanksType = 0;
    }
    public override void CheckDiscountAmt()
    {
        if (Convert.ToDouble(Data["TotalAmount"].Value) <= 0)
        {
            throw new Exception("Total amount cannot be zero or less than zero.");
        }
    }
    public override void CheckBudgetHeadConditions(string ChallanType)
    {
        DataTable dt = new DataTable();
        GetBudgetHeadDt();
        if(!CheckBudgetHeadME())
            throw new Exception("Check BudgetHead");
        EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
        objEgCheckBudgetHead.DepartmentCode = DeptCode;
        objEgCheckBudgetHead.ChallanType = ChallanType;
        dt = objEgCheckBudgetHead.GetBudgetHeadDetail(dtCheckHeads);
        if (dt.Rows.Count <= 0)
            throw new Exception("DataTable not found");
    }
    private bool CheckBudgetHeadME()
    {
        EgEMinusChallanBL objEgEMinusChallanBL = new EgEMinusChallanBL();
        objEgEMinusChallanBL.BudgetHead = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
        objEgEMinusChallanBL.ObjectHead = Convert.ToString(ChallanValues[3][1].ToString().Trim());
        objEgEMinusChallanBL.PNP = Convert.ToString(ChallanValues[4][1].ToString().Trim());
        objEgEMinusChallanBL.VNC = Convert.ToString(ChallanValues[5][1].ToString().Trim());
        return objEgEMinusChallanBL.CheckBudgetHead() == 1 ? true : false;
    }
    public override void CheckAmount()
    {
        if (HeadTotalAmount != Convert.ToDouble(Data["TotalAmount"].Value))
        {
            throw new Exception("Amount mismatch.");
        }
    }
    public override void GetBudgetHeadDt()
    {
        createTable();
        int sno = 1;
        double amount = 0;
        if (ChallanValues[1][1].ToString().Trim() != "0" && Convert.ToDouble(ChallanValues[2][1].ToString().Trim()) > 0)
        {
            DataRow dr = dtCheckHeads.NewRow();
            dr["SNo"] = Convert.ToInt32(sno);
            dr["BudgetHead"] = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(0, 13));
            dr["ScheCode"] = Convert.ToString(ChallanValues[1][1].ToString().Trim().Substring(13, 5));
            dr["Amount"] = Convert.ToDouble(ChallanValues[2][1].ToString());
            amount = amount + Convert.ToDouble(ChallanValues[2][1].ToString());
            dtCheckHeads.Rows.Add(dr);
        }
        HeadTotalAmount = amount;
    }

    public override void SplitToProperties(string PlainText)
    {
        if(PlainText.Contains("PD:"))
            throw new Exception("Invalid Value for Filler.");
        EgDeptIntegrationPropBL objEgDeptIntegrationPropBL = new EgDeptIntegrationPropBL();
        Data = new System.Collections.Generic.Dictionary<string, IntegrationProp>();
        ChallanValues = PlainText.Trim().Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).Select(splitequal => splitequal.Split('=')).ToArray();
        Data.Add("MerchantCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.merchantCode(ChallanValues[8][1].ToString()), ParameterName = "@MerchantCode", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("Paymenttype", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pType(ChallanValues[9][1].ToString()), ParameterName = "@Paymenttype", DbType = SqlDbType.Char, Size = 1 });
        Data.Add("Identity", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.identity(ChallanValues[10][1].ToString()), ParameterName = "@Identity", DbType = SqlDbType.VarChar, Size = 15 });
        Data.Add("Location", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.location(ChallanValues[11][1].ToString()), ParameterName = "@Location", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("FullName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.fullName(ChallanValues[6][1].ToString()), ParameterName = "@FullName", DbType = SqlDbType.VarChar, Size = 50 });//
        Data.Add("ObjectHead", new IntegrationProp() { Value = ChallanValues[3][1], ParameterName = "@ObjectHead", DbType = SqlDbType.Char, Size = 2 });
        Data.Add("VNC", new IntegrationProp() { Value = ChallanValues[5][1], ParameterName = "@VNC", DbType = SqlDbType.Char, Size = 1 });
        Data.Add("PNP", new IntegrationProp() { Value = ChallanValues[4][1], ParameterName = "@PNP", DbType = SqlDbType.Char, Size = 1 });
        //Data.Add("DistrictCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.districtCode(Convert.ToInt32(ChallanValues[12][1].ToString())), ParameterName = "@DistrictCode", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("OfficeName", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.officeCode(ChallanValues[13][1].ToString(), false), ParameterName = "@OfficeName", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("ddo", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.officeCode(ChallanValues[13][1].ToString(), false), ParameterName = "@ddo", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("DeductCommission", new IntegrationProp() { Value = 0.00, ParameterName = "@DeductCommission", DbType = SqlDbType.Money, Size = 8 });
        Data.Add("ChallanFromMonth", new IntegrationProp() { Value = Convert.ToDateTime(ChallanValues[15][1].ToString()), ParameterName = "@ChallanFromMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("ChallanToMonth", new IntegrationProp() { Value = Convert.ToDateTime((ChallanValues[16][1].ToString())), ParameterName = "@ChallanToMonth", DbType = SqlDbType.SmallDateTime, Size = 4 });
        Data.Add("Address", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.address(ChallanValues[17][1].ToString()), ParameterName = "@Address", DbType = SqlDbType.VarChar, Size = 100 });
        Data.Add("PINCode", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.pincode(ChallanValues[18][1].ToString()), ParameterName = "@PINCode", DbType = SqlDbType.Char, Size = 6 });
        Data.Add("City", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.city(ChallanValues[19][1].ToString()), ParameterName = "@City", DbType = SqlDbType.VarChar, Size = 40 });
        Data.Add("Remarks", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.remarks(ChallanValues[20][1].ToString()), ParameterName = "@Remarks", DbType = SqlDbType.VarChar, Size = 200 });
        Data.Add("RefNumber", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.refNo(ChallanValues[0][1].ToString()), ParameterName = "@RefNumber", DbType = SqlDbType.VarChar, Size = 50 });//
        Data.Add("TotalAmount", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.amount(ChallanValues[7][1].ToString()), ParameterName = "@TotalAmount", DbType = SqlDbType.Money, Size = 8 });
        Data.Add("ChallanYear", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.ChallanYear(ChallanValues[22][1].ToString()), ParameterName = "@ChallanYear", DbType = SqlDbType.Char, Size = 4 });
        Data.Add("Filler", new IntegrationProp() { Value = objEgDeptIntegrationPropBL.filler(ChallanValues[21][1].ToString()), ParameterName = "@Filler", DbType = SqlDbType.Int, Size = 4 });
        DeptCode = objEgDeptIntegrationPropBL.departmentCode(ChallanValues[14][1].ToString());
        Data.Add("Profile", new IntegrationProp() { Value = 0, ParameterName = "@Profile", DbType = SqlDbType.Int, Size = 4 });
        Data.Add("UserId", new IntegrationProp() { Value = System.Web.HttpContext.Current.Session["UserId"], ParameterName = "@UserId", DbType = SqlDbType.Int, Size = 4 });
        if(Data["Paymenttype"].Value.ToString().Trim() != "M")
            throw new Exception("Online Payment Mode Not Allowed.");
        Data.Add("IpAddress", new IntegrationProp() { Value = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), ParameterName = "@IpAddress", DbType = SqlDbType.NVarChar, Size = 50 });
    }
}