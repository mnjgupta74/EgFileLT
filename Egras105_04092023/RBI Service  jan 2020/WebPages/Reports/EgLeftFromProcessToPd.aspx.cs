using System;
using EgBL;
using System.Web.UI;
using System.Data;

public partial class WebPages_Reports_EgLeftFromProcessToPd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack) { trLeftFromProcessTopd.Visible = false; }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        EgLeftFromProcessToPd objTreasuryAndDept = new EgLeftFromProcessToPd();
        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        dt=objTreasuryAndDept.FillLertFromProcessToPd();
        RptleftFromProcessToPd.DataSource = dt;
        RptleftFromProcessToPd.DataBind();
        if (RptleftFromProcessToPd.Items.Count > 0)
        {
            trLeftFromProcessTopd.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('No Record Found !!')", true);
            return;
        }
        dt.Dispose();
    }
}