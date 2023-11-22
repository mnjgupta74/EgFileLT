using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgTreasuryAndDepartmentRptDetail : System.Web.UI.Page
{
    List<string> strList;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
            string strReqq = Request.Url.ToString();
            strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
            ViewState["strList"] = ObjEncrytDecrypt.Decrypt(strReqq);
            BindBudgetHead();
        }
        var menu = Page.Master.FindControl("vmenu1") as UserControl;
        menu.Visible = false;
        var lnk = Page.Master.FindControl("lnkLogout") as Control;
        lnk.Visible = false;
        UserControl uc = (UserControl)this.Page.Master.FindControl("hmenu1");
        uc.Visible = false;
    }
    public void BindBudgetHead()
    {
        strList = (List<string>)ViewState["strList"];
        EgTreasuryAndDepartmetRptBL objEgTreasuryAndDepartmetRptBL = new EgTreasuryAndDepartmetRptBL();
        objEgTreasuryAndDepartmetRptBL.Mcode = strList[7];
        objEgTreasuryAndDepartmetRptBL.FromDate = Convert.ToDateTime(strList[1]);
        objEgTreasuryAndDepartmetRptBL.ToDate = Convert.ToDateTime(strList[3]);
        objEgTreasuryAndDepartmetRptBL.Tcode = strList[5];
        objEgTreasuryAndDepartmetRptBL.TreasuryBudgetHeadDetail(grdBudgetHead);

        lblTotal.Text = "Total Amount" + '-' + objEgTreasuryAndDepartmetRptBL.TotalAmount.ToString();
    }

    protected void grdBudgetHead_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        grdBudgetHead.PageIndex = e.NewPageIndex;
        BindBudgetHead();
    }

    protected void grdBudgetHead_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    decimal rowTotal = Convert.ToDecimal
        //   (DataBinder.Eval(e.Row.DataItem, "Amount"));
        //    grdTotal += rowTotal;
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[5].Text = "Total:" + "  " + grdTotal.ToString("0.00"); ;
        //    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
        //    e.Row.Font.Bold = true;
        //}
    }

    protected void grdBudgetHead_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));

            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";



            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);

        }
    }

    protected void grdBudgetHead_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {

    }

    protected void btnDownloadPDF_Click(object sender, EventArgs e)
    {
        strList = (List<string>)ViewState["strList"];
        if (strList[1] != "" && strList[3] != "")
        {
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("FromDate", strList[1].Trim());
            param[1] = new ReportParameter("ToDate", strList[3].Trim());
            param[2] = new ReportParameter("TreasuryCode", strList[5]);
            param[3] = new ReportParameter("MajorHead", strList[7]);
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptManualSuccessDivisionWiserpt, "EgTreasuryAndDepartmentRptDetail", param);

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

        returnValue = rptManualSuccessDivisionWiserpt.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=EgTreasuryAndDepartmentRptDetail.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}