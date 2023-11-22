using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgEditTreasuryRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetEditTreasuryData(string FromDate, string ToDate)
    {
        string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
        EgEditTreasuryRpt objEgEditTreasuryRpt = new EgEditTreasuryRpt();
        DataTable dt = new DataTable();



        string[] fromdate = FromDate.Split('/');
        objEgEditTreasuryRpt.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        //objEgFailTransactionsBL.FromDate = Convert.ToDateTime(txtFromDate.Text);
        string[] todate = ToDate.Split('/');
        objEgEditTreasuryRpt.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());




       
       
        if(Convert.ToString(HttpContext.Current.Session["UserType"]) == "3")
        { 
            objEgEditTreasuryRpt.UserId = Convert.ToInt32(UserID);
        }
        string JSONGRNString = objEgEditTreasuryRpt.GetEditTreasuryData();
        return JSONGRNString;
    }

}
