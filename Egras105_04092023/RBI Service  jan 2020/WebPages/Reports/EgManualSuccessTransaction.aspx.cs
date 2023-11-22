using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgManualSuccessTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserID"] = 46;
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEgEChallanBL = new EgEChallanBL();
            objEgEChallanBL.GetChallanBanks(ddlbankgrnstatus);
        }
    }
    protected void btnFindResult_Click(object sender, EventArgs e)
    {
        LoadReport();
        
    }
    protected void LoadReport()
    {

        ReportParameter[] param = new ReportParameter[3];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromdatebank.Text.Trim().Split('/');
        param[0] = new ReportParameter("BankCode", ddlbankgrnstatus.SelectedValue);
        param[1] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtTodatebnk.Text.Trim().Split('/');
        param[2] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
       
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptLORSSRS, "Eg_ManualSuccessChallan", param);
        trrpt.Visible = true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
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

        returnValue = rptLORSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=LOR.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}