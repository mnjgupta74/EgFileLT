using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_Reports_EgDeptOfficewiseAmountrpt : System.Web.UI.Page
{
    EgDeptOfficewiseAmount objEgDeptOfficewiseAmount;
    double RunningTotal = 0.0;
    int TotalTrans = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetDeptOfficeWiseAmount(string BudgetHead, Int16 Type, string FromDate, string ToDate,string userid)
    {
        EgDeptOfficewiseAmount objEgDeptOfficewiseAmount = new EgDeptOfficewiseAmount();

        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgDeptOfficewiseAmount.BudgetHead = BudgetHead.Replace("-","").Trim();
        objEgDeptOfficewiseAmount.Type = Type;
        objEgDeptOfficewiseAmount.UserId = int.Parse(userid);
        objEgDeptOfficewiseAmount.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgDeptOfficewiseAmount.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        return objEgDeptOfficewiseAmount.EgDeptOfficeWiseAmountRpt();
    }
    
}
