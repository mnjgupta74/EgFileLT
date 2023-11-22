using System;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgClosingAbstractETO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

    }
    public void LoadReport()
    {
        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptClosingAbstructETO, "ClosingAbstract-ETO", param);
        trrpt.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnDisable();
        LoadReport();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (trrpt.Visible == false)
        {
            LoadReport();
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

        returnValue = rptClosingAbstructETO.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=ClosingAbstractETO.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        trrpt.Visible = false;
        txtfromdate.Text = "";
        txttodate.Text = "";
        btnEnable();
    }
    public void btnEnable()
    {
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        btnSubmit.Enabled = true;
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        btnSubmit.Enabled = false;
    }
}

