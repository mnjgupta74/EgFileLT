using EgBL;
using System;
using System.Web.Services;
using System.Web;
public partial class WebPages_Reports_EgBudgetHeadWiseReport : System.Web.UI.Page
{

    decimal grdTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");

        }
    }
    [WebMethod]
    public static string GetBudgetHeadWiseReport(string BudgetHead, string Type, string FromDate, string ToDate)
    {
        EgBudgetHeadWiseReportBL objEgBudgetHeadWiseReportBL = new EgBudgetHeadWiseReportBL();
        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgBudgetHeadWiseReportBL.BudgetHead = BudgetHead.Replace("-", "");
        objEgBudgetHeadWiseReportBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgBudgetHeadWiseReportBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgBudgetHeadWiseReportBL.type = Convert.ToInt16(Type);
        string str = string.Empty;
        if (Type == "1" || Type == "3")
            str = objEgBudgetHeadWiseReportBL.FillBudgetHeadAllDept();
        else
            str = objEgBudgetHeadWiseReportBL.FillBudgetHeadWiseGrd();
        return str;
    }
    [WebMethod]
    public static string GetEncryptParam(string id)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        string res = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&deptcode={3}", Convert.ToInt32(id).ToString(), HttpContext.Current.Session["UserId"].ToString(), HttpContext.Current.Session["UserType"].ToString(), "1"));
        return System.Text.RegularExpressions.Regex.Unescape(res);
    }
}
