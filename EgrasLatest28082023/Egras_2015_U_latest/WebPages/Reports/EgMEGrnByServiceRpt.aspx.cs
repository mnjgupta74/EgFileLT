using System;
using System.Web.UI;
using EgBL;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Web;
using System.Net.Mime;

public partial class WebPages_Reports_EgMEGrnByServiceRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    public void LoadReport()
    {
        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtFromDate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtToDate.Text.Trim().Split('/');
        param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        SSRS obj = new SSRS();
        obj.LoadSSRS(rptRG, "EgMEGrnByService", param);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        rptSchemaDi.Visible = true;
        EgMEGrnbyServiceBL objMEGrnByServiceBL = new EgMEGrnbyServiceBL();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objMEGrnByServiceBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objMEGrnByServiceBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        objMEGrnByServiceBL.fillrepeater(rptrMEGRN);
        lblAmount.Text = "Total Amount:" + ' ' + objMEGrnByServiceBL.sumofobject.ToString();
        if (objMEGrnByServiceBL.flag == 1)
        {
            lblAmount.Visible = true;
            trrpt.Visible = true;
        }
        else
        {
            lblAmount.Visible = false;
            trrpt.Visible = false;
            rptSchemaDi.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);

            return;
        }
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        LoadReport();
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
        returnValue = rptRG.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=MEGrnByService.pdf");
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();


    }

    protected void lnkgrn_Click(object sender, EventArgs e)
    {
        // Get the reference of the clicked button.
        LinkButton button = (sender as LinkButton);
        //Get the command argument
        string commandArgument = button.CommandArgument;

        byte[] returnValue = null;
        try
        {

            ReportViewer objReport = new ReportViewer();

            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("GRN", commandArgument);
            param[1] = new ReportParameter("DeptName", "Egras");
            param[2] = new ReportParameter("MajorHead", "0040");
            param[3] = new ReportParameter("Mode", "2");
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(objReport, "EgEChallanViewRpt", param);
            //output as PDF
            var format = "PDF";
            var deviceinfo = "";
            string mimeType;
            string encoding;
            string extension;
            string[] streams;
            Warning[] warnings;
            returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);




            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=MEGRN.pdf");
            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();


            // Response.Clear();
            // Response.ContentType = "application/pdf";
            // Response.AppendHeader("Content-Disposition", "inline;filename=data.pdf");
            // Response.BufferOutput = true;

            //// Response.AddHeader("Content-Length", response.Length.ToString());
            // Response.BinaryWrite(returnValue);
            // Response.End();


        }
        catch (Exception ex)
        {
            //returnValue = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }

    }
}