﻿using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using EgBL;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

public partial class WebPages_Reports_frmListofPayment : System.Web.UI.Page
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
    protected void LoadReport()
    {
        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        SSRS objssrs = new SSRS();
        if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
        {
            btnEnable();
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
            return;
        }
        objssrs.LoadSSRS(rptLOPSSRS, "PaymentReport", param);
        trrpt.Visible = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {            
            btnDisable();
            LoadReport();
        }
            
        else
            ShowPDF();
    }
    protected void ShowPDF()
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

        returnValue = rptLOPSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=PaymentReport.pdf");

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
        
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        
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

        returnValue = rptLOPSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
        // X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
        X509Certificate2 cert = new X509Certificate2(System.Web.Configuration.WebConfigurationManager.AppSettings["SecureCertificate"] + ConfigurationManager.AppSettings["Certificate"].ToString(), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
        PDFSign objpdfsign = new PDFSign();
        byte[] signedData = objpdfsign.SignDocument(returnValue, cert, Server.MapPath("../../Image/right.jpg"));
        Objdigitalsign.PageName = "PaymentReport";
        Objdigitalsign.SignData = signedData;
        Objdigitalsign.Duration = txtfromdate.Text + '-' + txttodate.Text;
        // Objdigitalsign.InsertSignData();
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=LOP.pdf");

        Response.BinaryWrite(signedData);
        Response.Flush();
        Response.End();
    }
}
