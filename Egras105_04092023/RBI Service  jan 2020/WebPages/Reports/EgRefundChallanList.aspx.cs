using System;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgRefundChallanList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void LoadReport()
    {
        ReportParameter[] param = new ReportParameter[3];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtFromDate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtToDate.Text.Trim().Split('/');
        param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        param[2] = new ReportParameter("TreasuryCode", ddlTreasury.SelectedValue.ToString());
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(EgRefundChallanList, "EgRefundChallanList", param);
        trrpt.Visible = true;
    }
    protected void btnshow_Click(object sender, EventArgs e)
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
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;
        returnValue = EgRefundChallanList.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=EgRefundChallanList.pdf");
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlTreasury.SelectedIndex = 0;
        trrpt.Visible = false;
        btnEnable();
    }
    public void btnEnable()
    {
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        ddlTreasury.Enabled = true;
        btnshow.Enabled = true;
    }
    public void btnDisable()
    {
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        ddlTreasury.Enabled = false;
        btnshow.Enabled = false;
    }

}
