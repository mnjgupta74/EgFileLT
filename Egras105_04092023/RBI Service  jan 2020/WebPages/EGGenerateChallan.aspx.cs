using System;
using EgBL;
using System.Web.Services;

public partial class WebPages_EGGenerateChallan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetBanks()
    {
        EgEchallanHistoryBL objEgEchallanHistoryBL = new EgEchallanHistoryBL();
        string result = objEgEchallanHistoryBL.GetBanks();
        return result;
    }

    [WebMethod]
    public static string GetData(string Paytype, string BankCode, string FromDate, string ToDate)
    {
        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        EgGenerateChallan objEgEchallanHistoryBL = new EgGenerateChallan();
        objEgEchallanHistoryBL.type = "1";
        objEgEchallanHistoryBL.PaymentType = Paytype;
        objEgEchallanHistoryBL.BankCode = BankCode;
        objEgEchallanHistoryBL.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgEchallanHistoryBL.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        string result = objEgEchallanHistoryBL.GetGRNlistforChallan();
        return result;
    }
    [WebMethod]
    public static string GetSummeryData(string Paytype, string BankCode, string FromDate, string ToDate)
    {
        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        EgGenerateChallan objEgEchallanHistoryBL = new EgGenerateChallan();
        objEgEchallanHistoryBL.type = "2";
        objEgEchallanHistoryBL.PaymentType = Paytype;
        objEgEchallanHistoryBL.BankCode = BankCode;
        objEgEchallanHistoryBL.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgEchallanHistoryBL.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        string result = objEgEchallanHistoryBL.GetGRNlistforChallan();
        return result;
    }
    [WebMethod]
    public static string GenerateGrnChallanNo(string Paytype, string BankCode, string FromDate, string ToDate, string transaction, string totalAmount)
    {
        if (Convert.ToInt64(totalAmount) > 0 || Convert.ToInt64(transaction) > 0)
        {
            string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
            string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
            EgGenerateChallan objEgEchallanHistoryBL = new EgGenerateChallan();
            objEgEchallanHistoryBL.PaymentType = Paytype;
            objEgEchallanHistoryBL.BankCode = BankCode;
            objEgEchallanHistoryBL.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            objEgEchallanHistoryBL.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

            string result = objEgEchallanHistoryBL.InsertAllSelectedGrnData();
            return result;
        }
        else
        {
            return "0";
        }
    }
}