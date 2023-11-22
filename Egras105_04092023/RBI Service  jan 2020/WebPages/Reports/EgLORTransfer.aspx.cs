using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Reports_EgLORTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            CalenderYear.FinYearDropdown();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EgReports objReport = new EgReports();
        objReport.month = Convert.ToInt16(ddlMonth.SelectedValue);
        objReport.year = Convert.ToInt16(CalenderYear.SelectedValue);

        if (objReport.LORTransfer() == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Trasnfer SuccessFuly')", true);
        }
    }

}
