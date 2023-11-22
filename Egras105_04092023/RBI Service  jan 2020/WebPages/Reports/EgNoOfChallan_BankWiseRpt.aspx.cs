using System;
using System.Web.UI;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgNoOfChallan_BankWiseRpt : System.Web.UI.Page
{
    string date;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //date = ddlDate.SelectedDate;
        string[] dateformat = ddlDate.SelectedDate.Trim().Replace("-", "/").Split('/');
        date = dateformat[1].ToString() + "/" + dateformat[0].ToString() + "/" + dateformat[2].ToString();

        if (DateTime.Parse(date) <= DateTime.Now)
        {
            LoadSSRS();
            btnPDF.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Date Cannot be greater than Current Date.')", true);
        }
    }
    public void LoadSSRS()
    {
        string UName = System.Configuration.ConfigurationManager.AppSettings["UName"];
        string PWD = System.Configuration.ConfigurationManager.AppSettings["PWD"];
        string DOM = System.Configuration.ConfigurationManager.AppSettings["DOM"];
        string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"];
        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
        string ReportName = "BankWiseNumberOfChallan";

        SSRSreport.ShowCredentialPrompts = false;
        SSRSreport.ServerReport.ReportServerCredentials = new ReportCredentials(UName, PWD, DOM);
        SSRSreport.ProcessingMode = ProcessingMode.Remote;
        SSRSreport.ServerReport.ReportServerUrl = new System.Uri(ReportServerUrl);
        SSRSreport.ServerReport.ReportPath = ReportPath + ReportName;

        ReportParameter[] Param = new ReportParameter[1];
        Param[0] = new ReportParameter("BankChallandate",Convert.ToDateTime(date).ToString("dd/MM/yyyy"));
        SSRSreport.ShowParameterPrompts = false;
        SSRSreport.ServerReport.SetParameters(Param);
        SSRSreport.ServerReport.Refresh();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        //string[] dateformat = ddlDate.SelectedDate.Split('/');
        string[] dateformat = ddlDate.SelectedDate.Trim().Replace("-", "/").Split('/');
        date = dateformat[1].ToString() + "/" + dateformat[0].ToString() + "/" + dateformat[2].ToString();

        loadPDF();
    }
    public void loadreport()
    {

        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("BankChallandate", date);
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(SSRSreport, "BankWiseNumberOfChallan", param);
        ////create PDF
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;
        returnValue = SSRSreport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=NumberOfChallanReport.pdf");
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();


    }

    public void loadPDF()
    {
        //output as PDF
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = SSRSreport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=NumberOfChallanReport.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}