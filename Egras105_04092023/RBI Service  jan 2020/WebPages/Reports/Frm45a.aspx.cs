using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

public partial class WebPages_Reports_Frm45a : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            calendarfromdate.EndDate = DateTime.Now;
            calendartodate.EndDate = DateTime.Now;
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            btnDisable();
            loadreport();
        }
            
        else
            ShowPDF();

    }

    public void loadreport()
    {
        ReportParameter[] param = new ReportParameter[3];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("fromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("toDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        param[2] = new ReportParameter("Budgethead", txtdetaiedheadsearch.Text);
        if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
        {
            btnEnable();
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
            return;
        }
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptHeadWise45SSRS, "Rpt45A", param);
        trrpt.Visible = true;
    }
    protected void ShowPDF()
    {
        if (trrpt.Visible == false)
        {
            loadreport();
        }
        //create PDF
        //if (Response.IsClientConnected) { Response.Flush(); }
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptHeadWise45SSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=Rpt45A.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
        
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        trrpt.Visible = false;
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtdetaiedheadsearch.Text = "";
        btnEnable();
    }
    public void btnEnable()
    {
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        txtdetaiedheadsearch.Enabled = true;
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        txtdetaiedheadsearch.Enabled = false;
    }
}

