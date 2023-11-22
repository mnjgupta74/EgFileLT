using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Web.UI;

public partial class WebPages_Reports_EgtransactionDepartmentWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblList.SelectedValue == "1")
                loadreport();
            else
                ShowPDF();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Sorry!!Please Try Again.')", true);
        }
    }
    public void loadreport()
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[3];
            string[] revdateFrom, revdateTo;
            param[0] = new ReportParameter("userid", Session["UserId"].ToString());
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            param[1] = new ReportParameter("fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txtToDate.Text.Trim().Split('/');
            param[2] = new ReportParameter("todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            string rptname = "TransactionDepartmentWise";
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptTransactionDepartmentWise, rptname, param);
            trrpt.Visible = true;
        }
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

        returnValue = rptTransactionDepartmentWise.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=TransactionDepartmentWise.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}