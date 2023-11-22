using System;
using EgBL;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

public partial class WebPages_Reports_EgMobileAppSuccessChallan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        EgMobileAppSuccessBL obj = new EgMobileAppSuccessBL();
        object sumofobject;
        string[] fromDate = txtfromDate.Text.Split('/');
        obj.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = txttoDate.Text.Split('/');
        obj.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
        dt= obj.EgAppmobile();
        sumofobject = dt.Compute("Sum(Amount)", "");
        if (dt.Rows.Count > 0)
        {
            rptSchemaDiv.Visible = true;
            rptschema.DataSource = dt;
            rptschema.DataBind();
            lblGrn.Text = Convert.ToString(dt.Rows.Count);
            lblAmount.Text = sumofobject.ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Data Found.')", true);
        }
    }

    protected void btnGeneratePDF_Click(object sender, EventArgs e)
    {
        LoadPDFNew();
    }
    public void LoadPDFNew()
    {
        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromDate.Text.Trim().Split('/');
        param[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttoDate.Text.Trim().Split('/');
        param[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptManualSuccessDivisionWiserpt, "EgMobileAppSuccessChallan", param);
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;
        returnValue = rptManualSuccessDivisionWiserpt.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=EgMobileAppSuccessChallanList.pdf");
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}