using EgBL;
using System;
using System.Data;
using System.Web;
using System.Web.Services;

public partial class WebPages_Reports_EgGrnServiceRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
    }
    [WebMethod]
    public static string GetServices(string FromDate, string ToDate)
    {
        EgGetStatusLog objEgGetServices = new EgGetStatusLog();
        string[] fromdate = FromDate.Split('/');
        objEgGetServices.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = ToDate.Split('/');
        objEgGetServices.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        string JSONGRNString = objEgGetServices.GetServicesBL();
        return JSONGRNString;
    }

    [WebMethod]
    public static string GetEncryptParam(string id)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        string res = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&deptcode={3}", Convert.ToInt32(id).ToString(), HttpContext.Current.Session["UserId"].ToString(), HttpContext.Current.Session["UserType"].ToString(), "1"));
        return System.Text.RegularExpressions.Regex.Unescape(res);
    }
}