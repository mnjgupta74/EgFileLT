using System;
using Microsoft.Reporting.WebForms;
using EgBL;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

public partial class WebPages_EgClosingAbstract : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            calendarfromdate.EndDate = DateTime.Now;
            calendartodate.EndDate = DateTime.Now;
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
        objssrs.LoadSSRS(rptClosingAbstruct, "ClosingAbstract", param);
        trrpt.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadReport();
        btnDisable();
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

        returnValue = rptClosingAbstruct.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=ClosingAbstract.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        btnEnable();
        trrpt.Visible = false;
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
    protected void btnSignPdf_Click(object sender, EventArgs e)
    {
        EgDigitalSignPdf Objdigitalsign = new EgDigitalSignPdf();

        if (trrpt.Visible == false)
        {
            LoadReport();
        }
        // create PDF
        // if (Response.IsClientConnected) { Response.Flush(); }
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptClosingAbstruct.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
        //X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
        X509Certificate2 cert = new X509Certificate2((Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString())), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
        PDFSign objpdfsign = new PDFSign();
        byte[] signedData = objpdfsign.SignDocument(returnValue, cert, Server.MapPath("../../Image/right.jpg"));
        Objdigitalsign.PageName = "ClosingAbstract";
        Objdigitalsign.SignData = signedData;
        Objdigitalsign.Duration = txtfromdate.Text + '-' + txttodate.Text;
        // Objdigitalsign.InsertSignData();
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=ClosingAbstract.pdf");

        Response.BinaryWrite(signedData);
        Response.Flush();
        Response.End();
    }
}
