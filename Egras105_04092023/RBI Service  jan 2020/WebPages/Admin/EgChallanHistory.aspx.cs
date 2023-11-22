using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Admin_EgChallanHistory : System.Web.UI.Page
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
    public static string GetData(string Paytype, string BankCode, string FromDate, string ToDate, string Status)
    {
        EgEchallanHistoryBL objEgEchallanHistoryBL = new EgEchallanHistoryBL();

        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgEchallanHistoryBL.PaymentType = Paytype;
        objEgEchallanHistoryBL.BankCode = BankCode;
        objEgEchallanHistoryBL.Status = Status;
        objEgEchallanHistoryBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgEchallanHistoryBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        return objEgEchallanHistoryBL.GetSuccessfulChallanData();
    }
}