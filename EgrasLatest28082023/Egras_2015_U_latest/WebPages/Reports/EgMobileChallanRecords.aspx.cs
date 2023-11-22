using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_Reports_EgMobileChallanRecords : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetMobileData(string FromDate, string ToDate)
    {
        EgMobileChallanBL objEgMobileChallanBL = new EgMobileChallanBL();

        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
       
        objEgMobileChallanBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgMobileChallanBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        return objEgMobileChallanBL.GetMobileChallanData();
    }
}