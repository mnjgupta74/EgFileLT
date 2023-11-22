using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Reports_EgLOPTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EgReports objReport = new EgReports();
        string[] revdateFrom = txtfromdate.Text.Trim().Split('/');
        objReport.Fromdate =  Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
        string[] revdateTo = txttodate.Text.Trim().Split('/');
        objReport.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
        if (objReport.LOPTransfer() == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Transfer Successfully')", true);
        }
    }
}
