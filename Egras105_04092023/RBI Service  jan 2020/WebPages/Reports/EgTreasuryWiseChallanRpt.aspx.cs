using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgTreasuryWiseChallanRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            var objEChallan = new EgEChallanBL();
            objEChallan.FillTreasury(ddlTreasury);

            if ((Session["UserType"].ToString().Trim() == "3".Trim()))
            {
                var objEgBankSoftCopyBl = new EgBankSoftCopyBL { UserId = Convert.ToInt32(Session["UserID"]) };
                ddlTreasury.SelectedValue = objEgBankSoftCopyBl.GetBSRCode().Trim();
                ddlTreasury.Enabled = false;
            }
        }
    }
    public void Loadreport()
    {
        DataTable dt = new DataTable();
        var objTy11 = new EgTy11BL();
        string[] revdateFrom = txtFromDate.Text.Trim().Split('/');
        objTy11.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
        string[] revdateTo = txtToDate.Text.Trim().Split('/');
        objTy11.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
        objTy11.tcode = ddlTreasury.SelectedValue.Trim();
        if (txtMajorHead.Text != "")
        {
            objTy11.majorHead = txtMajorHead.Text.Trim();
        }
        dt = objTy11.BindTreasuryWiseChallanGrid();
        if (dt.Rows.Count > 0)
        { 
        grdTy11Rpt.DataSource = dt;
        grdTy11Rpt.DataBind();
        }
        dt.Dispose();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        Loadreport();
    }
    protected void grdTy11Rpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var objEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            var lb = (LinkButton)e.CommandSource;
            var strUrlWithData = objEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            var script = "window.open('../EgDefaceDetailNew.aspx?" + strUrlWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "PopupScript", script, true);
        }
    }

    protected void grdTy11Rpt_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        Loadreport();
        grdTy11Rpt.PageIndex = e.NewPageIndex;
        grdTy11Rpt.DataBind();
    }
    public void LoadReport()
    {
        ReportParameter[] para = new ReportParameter[4];
        string[] revdateFrom, revdateTo;
        string rpbudgethead, rptreasury;
        revdateFrom = txtFromDate.Text.Trim().Split('/');
        para[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtToDate.Text.Trim().Split('/');
        para[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        rpbudgethead = txtMajorHead.Text.ToString();
        para[2] = new ReportParameter("BudgetHead", (rpbudgethead));
        rptreasury = ddlTreasury.SelectedValue.Trim();
        para[3] = new ReportParameter("tcode", (rptreasury));
        SSRS obj = new SSRS();
        obj.LoadSSRS(SSRSreport, "EgTreasuryWiseChallan", para);

    }

    protected void btnPDFDownload_Click(object sender, EventArgs e)
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
        returnValue = SSRSreport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename=EgTreasuryWiseChallan.pdf");
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        var objTy11 = new EgTy11BL();
        string[] revdateFrom = txtFromDate.Text.Trim().Split('/');
        objTy11.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
        string[] revdateTo = txtToDate.Text.Trim().Split('/');
        objTy11.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
        objTy11.tcode = ddlTreasury.SelectedValue.Trim();
        if (txtMajorHead.Text != "")
        {
            objTy11.majorHead = txtMajorHead.Text.Trim();
        }
        ExporttoExcel(objTy11.GetTreasuryWiseChallanDt(), "EgTreasuryWiseChallan");
    }
    private void ExporttoExcel(DataTable table, string ReportName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename="+ ReportName +".xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
}