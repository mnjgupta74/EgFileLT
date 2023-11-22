using System;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.UI;
using System.Data;
using System.Web.Services;

public partial class WebPages_Reports_EgMonthlyScrollRpt : System.Web.UI.Page
{
    double Totalmoney = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
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
    public void BindScroll()
    {
       
    }

    [WebMethod]
    public static string GetDMSReport(string SMonth, string SYear, string SBank,string SrblType)
    {
        EgBankMonthlyReportBL objEgBankMonthlyReportBL = new EgBankMonthlyReportBL();
        objEgBankMonthlyReportBL.Month = Convert.ToInt32(SMonth);
        objEgBankMonthlyReportBL.Year = Convert.ToInt32(SYear);
        objEgBankMonthlyReportBL.BSRCode = Convert.ToString(SBank);
        objEgBankMonthlyReportBL.PaymentType = SrblType;
        string jsonString = objEgBankMonthlyReportBL.BindScollRepeaterJson();
        return jsonString;
    }
}
