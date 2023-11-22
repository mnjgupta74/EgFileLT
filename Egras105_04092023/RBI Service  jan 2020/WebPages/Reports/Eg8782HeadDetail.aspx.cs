using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

public partial class WebPages_Reports_Eg8782HeadDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            calFromDate.EndDate = DateTime.Now;
            calToDate.EndDate = DateTime.Now;
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            btnDisable();
            LoadReport();
            rpt8782Head.Visible = true;
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
            if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
            {
                btnEnable();
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
                return;
            }
            param[2] = new ReportParameter("TreasuryCode", ddlTreasury.SelectedValue.Substring(0, 2).Trim());
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rpt8782Head, "8782Head", param);
        }
    }
    protected void ShowPDF()
    {
        if (rpt8782Head.Visible == false)
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

        returnValue = rpt8782Head.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=8782Head.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        rpt8782Head.Visible = false;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        btnEnable();
    }
    public void btnEnable()
    {
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        ddlTreasury.Enabled = true;
    }
    public void btnDisable()
    {
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        ddlTreasury.Enabled = false;
    }
}
