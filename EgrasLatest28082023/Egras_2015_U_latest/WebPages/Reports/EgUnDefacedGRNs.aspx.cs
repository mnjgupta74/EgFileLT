using System;
using EgBL;
using System.Web.Services;
using System.Web;

public partial class WebPages_Reports_EgUnDefacedGRNs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetUndefacedGrn(string FromDate, string ToDate, string userid)
    {
        try
        {
            EgReports objEgReports = new EgReports();

            string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
            string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
            objEgReports.Fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            objEgReports.Todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
            objEgReports.userid = Convert.ToInt32(userid);
            string str = objEgReports.GetUnDefacedGRNs();
            return str;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            throw new Exception("Sorry!!Please Try Again.");
        }
    }
    [WebMethod]
    public static string GetEncryptParam(string officeid, string fromdate, string todate)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        string res = ObjEncryptDecrypt.Encrypt(string.Format("OfficeId={0}&userId={1}&usertype={2}&deptcode={3}&fromDate={4}&toDate={5}", Convert.ToInt32(officeid).ToString(), HttpContext.Current.Session["UserId"].ToString(), HttpContext.Current.Session["UserType"].ToString(), "1", fromdate, todate));
        return System.Text.RegularExpressions.Regex.Unescape(res);
    }

}
