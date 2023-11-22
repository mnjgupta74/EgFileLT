using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_Reports_EgChallanWithORWithOutDivision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetChallanWithORWithOutDivision(string BudgetHead, Int16 Type,string FromDate, string ToDate)
    {
        EgChallanWithORWithOutDivisionBL objEgChallanWithORWithOutDivisionBL = new EgChallanWithORWithOutDivisionBL();

        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgChallanWithORWithOutDivisionBL.BudgetHead = BudgetHead;
        objEgChallanWithORWithOutDivisionBL.Type = Type;
        objEgChallanWithORWithOutDivisionBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgChallanWithORWithOutDivisionBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        return objEgChallanWithORWithOutDivisionBL.GetChallanWithORWithOutDivision();
    }
}