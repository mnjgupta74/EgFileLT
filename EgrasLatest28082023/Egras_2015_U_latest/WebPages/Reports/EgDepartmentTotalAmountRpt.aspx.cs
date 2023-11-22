using System;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgDepartmentTotalAmountRpt : System.Web.UI.Page
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
        if (RadioButtonList1.SelectedValue == "1")
        {
            LoadReport();
            rptDepartmentTotalAmount.Visible = true;
            btnDisabled();
        }
        else
        ShowPDF();
    }


    private void LoadReport()
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[3];
            string[] revdateFrom, revdateTo;
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txtToDate.Text.Trim().Split('/');
            param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("OrderBy", rblOrderType.SelectedValue.Trim());
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptDepartmentTotalAmount, "DepartmentTotalAmount", param);

        }
    }
    protected void ShowPDF()
    {

        if (rptDepartmentTotalAmount.Visible == false)
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

        returnValue = rptDepartmentTotalAmount.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=DepartmentTotalAmount.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        btnEnabled();
        rptDepartmentTotalAmount.Visible = false;
    }
    public void btnEnabled()
    {
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        rblOrderType.Enabled = true;
    }
    public void btnDisabled()
    {
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        rblOrderType.Enabled = false;
    }
}
