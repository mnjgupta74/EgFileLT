using EgBL;
using System;
using System.Data;
using System.Web;
using System.Web.Services;

public partial class WebPages_Reports_TreasuryBudgetHeadRevenue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
    }
    [WebMethod]
    public static string GetTreasury()
    {
        //TreasuryBudgetHeadRevenueBL objEgGetTreasyury = new TreasuryBudgetHeadRevenueBL();
        //return objEgGetTreasyury.GetTreasury();
        string str = string.Empty;
        var objTy11 = new EgTy11BL();
        objTy11.UserId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        return objTy11.FillTreasury();

    }

    [WebMethod]
    public static string GetTreasuryBudgetHead(string FromDate, string ToDate,string Tcode,string Mcode,string DeptCode)
    {
        TreasuryBudgetHeadRevenueBL objEgGetBudgetHead = new TreasuryBudgetHeadRevenueBL();
        string[] fromdate = FromDate.Split('/');
        objEgGetBudgetHead.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = ToDate.Split('/');
        objEgGetBudgetHead.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgGetBudgetHead.Tcode = Tcode;

        objEgGetBudgetHead.Mcode = Mcode.Replace("-","");
        objEgGetBudgetHead.DeptCode = Convert.ToInt32(DeptCode);
        string JSONGRNString = objEgGetBudgetHead.FillBudgetHeadListAmount();
        return JSONGRNString;
    }
    }