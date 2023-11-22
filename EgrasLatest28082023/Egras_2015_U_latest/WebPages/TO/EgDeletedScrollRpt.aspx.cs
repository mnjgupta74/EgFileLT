using System;
using System.Web.UI;
using EgBL;
using Microsoft.Reporting.WebForms;
public partial class WebPages_TO_EgDeletedScrollRpt : System.Web.UI.Page
{
    EgEChallanBL objEChallan = new EgEChallanBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            objEChallan.GetChallanBanks(ddlbankname);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
       
            LoadReport();
       
    }
    private void LoadReport()
    {
        if (txtFromDate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[2];
            string[] revdateFrom;
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            param[0] = new ReportParameter("Bankdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            param[1] = new ReportParameter("Bfcode", ddlbankname.SelectedValue.Trim());
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptDeletedScroll, "DeletedScrollRpt", param);
            rptDeletedScroll.Visible = true;
        }
    }
}
