using EgBL;
using System;
using System.Data;
using System.Web;
using System.Web.Services;
public partial class WebPages_Reports_EgChangeStatusLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetChangeStatuslog(string FromDate, string ToDate)
    {
        EgGetStatusLog objEgGetStatusLog = new EgGetStatusLog();
        string[] fromdate = FromDate.Split('/');
        objEgGetStatusLog.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = ToDate.Split('/');
        objEgGetStatusLog.ToDate  = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        string JSONGRNString = objEgGetStatusLog.GetStatusLog();
        return JSONGRNString;
    }

}