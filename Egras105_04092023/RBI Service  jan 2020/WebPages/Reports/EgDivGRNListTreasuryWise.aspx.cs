using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgDivGRNListTreasuryWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void LoadReport()
    {

        ReportParameter[] param = new ReportParameter[3];
        string[] revdateFrom, revdateTo;
        revdateFrom =  txtFromDate.Text.Trim().Split('/');
        param[0] = new ReportParameter("TreasuryCode", ddlTreasury.SelectedValue);
        param[1] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtToDate.Text.Trim().Split('/');
        param[2] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));

        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptLORSSRS, "Eg_DivGRNListTreasuryWise", param);
        trrpt.Visible = true;

    }
}