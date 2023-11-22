using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgGrnRemitterWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetRemitterWiseReport(string FromDate, string ToDate, string RemitterName)
    {
        EgGrnAmountDetailRptBL objEgGrnAmountDetailRptBL = new EgGrnAmountDetailRptBL();
        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        objEgGrnAmountDetailRptBL.Fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgGrnAmountDetailRptBL.Todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgGrnAmountDetailRptBL.RemitterName = RemitterName.ToString();
        string str = objEgGrnAmountDetailRptBL.AmountDetailsRemmitterWise();
        return str;
    }
}