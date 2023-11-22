using EgBL;
using System;
using System.Data;
using System.Web.UI;

public partial class WebPages_Reports_EgDMFTGRNSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack) { trDmft.Visible = false; }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        EgDMFTGrnList objTreasuryAndDept = new EgDMFTGrnList();
        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        dt=objTreasuryAndDept.FillDMFTSummaryRepeter();
        RptDMFTGrnSummary.DataSource = dt;
        RptDMFTGrnSummary.DataBind();
        if (dt.Rows.Count > 0)
        {
            trDmft.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('No Record Found !!')", true);
            return;
        }
        dt.Dispose();
    }
}