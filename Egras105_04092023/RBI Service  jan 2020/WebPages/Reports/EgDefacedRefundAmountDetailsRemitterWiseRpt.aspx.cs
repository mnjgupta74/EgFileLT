using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_DefacedRefundAmountDetailsRemitterWiseRpt : System.Web.UI.Page
{
    DateTime fromDate;
    DateTime toDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "2")
            {
                EgBankSoftCopyBL objEgBankSoftCopyBL = new EgBankSoftCopyBL();
                objEgBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserID"]);
                ddlTreasury.SelectedValue = objEgBankSoftCopyBL.GetBSRCode().Trim();
                ddlTreasury.Enabled = false;
            }
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        fromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        toDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        LoadSSRS();
        btnPDF.Visible = true;
        btnDisable();
    }
    public void LoadSSRS()
    {
        string UName = System.Configuration.ConfigurationManager.AppSettings["UName"];
        string PWD = System.Configuration.ConfigurationManager.AppSettings["PWD"];
        string DOM = System.Configuration.ConfigurationManager.AppSettings["DOM"];
        string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"];
        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
        string ReportName = "EgDefaceRefundChallanRemitterWise";

        SSRSreport.ShowCredentialPrompts = false;
        SSRSreport.ServerReport.ReportServerCredentials = new ReportCredentials(UName, PWD, DOM);
        SSRSreport.ProcessingMode = ProcessingMode.Remote;
        SSRSreport.ServerReport.ReportServerUrl = new System.Uri(ReportServerUrl);
        SSRSreport.ServerReport.ReportPath = ReportPath + ReportName;

        ReportParameter[] Param = new ReportParameter[3];
        Param[0] = new ReportParameter("FromDate", fromDate.ToString());
        Param[1] = new ReportParameter("ToDate", toDate.ToString());
        Param[2] = new ReportParameter("Treasrycode", ddlTreasury.SelectedValue);
        SSRSreport.ShowParameterPrompts = false;
        SSRSreport.ServerReport.SetParameters(Param);
        SSRSreport.ServerReport.Refresh();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        loadPDF();
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

        Response.AddHeader("content-disposition", "attachment; filename=DefacedChallanReport.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlTreasury.SelectedIndex = 0;
        txtToDate.Text = "";
        txtFromDate.Text = "";
        btnPDF.Visible = false;
        btnEnable();
        SSRSreport.Visible = false;
    }
    public void btnEnable()
    {
        ddlTreasury.Enabled = true;
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        btnShow.Enabled = true;
        
    }
    public void btnDisable()
    {
        ddlTreasury.Enabled = false;
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        btnShow.Enabled = false;
         
    }
}