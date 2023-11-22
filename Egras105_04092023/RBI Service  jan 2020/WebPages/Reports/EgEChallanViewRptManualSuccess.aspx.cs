using System;
using System.Data;
using EgBL;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Cryptography.X509Certificates;


public partial class WebPages_Reports_EgEChallanViewRptManualSuccess : System.Web.UI.Page
{
    EgEChallanBL objEgEChallan;
    DataTable dt1;
    string GRN;
    protected void Page_Load(object sender, EventArgs e)
    {
        objEgEChallan = new EgEChallanBL();
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (Request.QueryString.Count == 0)
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\logout.aspx");
        }
        else
        {
            string strReq = Request.Url.ToString();
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
            List<string> strList = ObjEncrcryptDecrypt.Decrypt(strReq);
            if (strList.Count > 0)
            {
                GRN = strList[1].ToString().Trim();
                objEgEChallan.GRNNumber = Convert.ToInt32(GRN);
                
            }
            else
            {
                Server.Transfer("~\\logout.aspx");

            }
        }
        if (!IsPostBack)
        {
             LoadSSRS();
        }

    }

    public void LoadSSRS()
    {
        dt1 = new DataTable();
        objEgEChallan.GRNNumber = Convert.ToInt64(GRN.ToString());
        dt1 = objEgEChallan.EChallanViewSubRptPDF();

        string UName = System.Configuration.ConfigurationManager.AppSettings["UName"];
        string PWD = System.Configuration.ConfigurationManager.AppSettings["PWD"];
        string DOM = System.Configuration.ConfigurationManager.AppSettings["DOM"];
        string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"];
        string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
        string ReportName = "EgEchallanViewRptManualSuccess";

        SSRSreport.ShowCredentialPrompts = false;
        SSRSreport.ServerReport.ReportServerCredentials = new ReportCredentials(UName, PWD, DOM);
        SSRSreport.ProcessingMode = ProcessingMode.Remote;
        SSRSreport.ServerReport.ReportServerUrl = new System.Uri(ReportServerUrl);
        SSRSreport.ServerReport.ReportPath = ReportPath + ReportName;

        ReportParameter[] Param = new ReportParameter[3];
        Param[0] = new ReportParameter("GRN", GRN);

        Param[1] = new ReportParameter("DeptName", dt1.Rows[0]["DeptNameEnglish"].ToString());
        Param[2] = new ReportParameter("MajorHead", dt1.Rows[0]["SchemaName"].ToString().Substring(0, 17));
        SSRSreport.ShowParameterPrompts = false;
        SSRSreport.ServerReport.SetParameters(Param);
        SSRSreport.ServerReport.Refresh();
        //CallManualBankService(Convert.ToInt32(GRN.ToString()));
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

        Response.AddHeader("content-disposition", "attachment; filename=EgEChallanViewManualSuccess.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        loadPDF();
    }
}