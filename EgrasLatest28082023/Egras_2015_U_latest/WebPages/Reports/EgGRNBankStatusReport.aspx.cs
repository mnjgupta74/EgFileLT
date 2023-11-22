using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_EgGRNBankStatusReport : System.Web.UI.Page
{
    EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
            if (!IsPostBack)
        {
            lblbank.Visible = true;
            ddlbankgrnstatus.Visible = true;
            objEgGRNBankStatus.PopulateBankList(ddlbankgrnstatus);
          
        }
    }

    public void loadReportBankwise()
    {
        ReportParameter[] param = new ReportParameter[3];
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        string[] revdateFrom, revdateTo;
        string rptbankcode; //rptnoOfdays;
        revdateFrom = txtfrmdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        rptbankcode = ddlbankgrnstatus.SelectedItem.Value;
        param[2] = new ReportParameter("BankCode", (rptbankcode));
        // rptnoOfdays = ddlDayWiseddl.SelectedItem.Value;
        // param[3] = new ReportParameter("NoofDays", rptnoOfdays);
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptBSRBW, "GrnBankStatusReport", param);
    }

    public void DayWiseloadReportBankwise()
    {
        ReportParameter[] param = new ReportParameter[4];
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        string[] dateFrom, dateTo;
        string bankcode, type;
        dateFrom = txtfrmdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (dateFrom[2] + "/" + dateFrom[1] + "/" + dateFrom[0]));
        dateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("Todate", (dateTo[2] + "/" + dateTo[1] + "/" + dateTo[0]));
        bankcode = ddlbankgrnstatus.SelectedItem.Value;
        param[2] = new ReportParameter("BankCode", (bankcode));
        type = rblbankdatewise.SelectedValue = "2";
        param[3] = new ReportParameter("Type", (type));
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptBSRDW, "DayWiseGrnBankStatusReport", param);
    }
    public void ScrollWiseloadReportBankwise()
    {
        ReportParameter[] param = new ReportParameter[4];
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        string[] dateFromto, dateTo;
        string bankcode, type;
        dateFromto = txtfrmdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (dateFromto[2] + "/" + dateFromto[1] + "/" + dateFromto[0]));
        dateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("Todate", (dateTo[2] + "/" + dateTo[1] + "/" + dateTo[0]));
        bankcode = ddlbankgrnstatus.SelectedItem.Value;
        param[2] = new ReportParameter("BankCode", (bankcode));
        type = rblbankdatewise.SelectedValue = "3";
        param[3] = new ReportParameter("Type", (type));
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(rptBSRSW, "ScrollWiseBankStatusReport", param);
    }
    protected void btnFindResult_Click(object sender, EventArgs e)
    {
        if (ddlbankgrnstatus.SelectedItem.Value != "0")
        {
            objEgGRNBankStatus.BankCode = ddlbankgrnstatus.SelectedItem.Value;
            TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
            TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
            string[] fromdate = txtfrmdate.Text.Trim().Replace("-", "/").Split('/');
            objEgGRNBankStatus.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
            objEgGRNBankStatus.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
            objEgGRNBankStatus.rblval = Convert.ToInt16(rblbankdatewise.SelectedValue);

            if (((objEgGRNBankStatus.ToDate - objEgGRNBankStatus.FromDate).TotalDays) < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);
                return;
            }
            else
            {
                if (rblbankdatewise.SelectedValue == "1")
                {
                    DataTable dt = new DataTable();
                    bool res = objEgGRNBankStatus.BankdetailRptBind(rptgrndetailbankstatus);
                    divrpt1.Visible = true;
                    divrpt2.Visible = false;
                    btnDisable();
                    if (res == false)
                    {
                        btnEnable();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                        return;
                    }
                }
                else if (rblbankdatewise.SelectedValue == "2")
                {
                    bool res = objEgGRNBankStatus.BankdetailRptBind(rptgrnbankstatus);
                    divrpt1.Visible = false;
                    divrpt2.Visible = true;
                    btnDisable();
                    if (res == false)
                    {
                        btnEnable();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                        return;
                    }
                }
                else if (rblbankdatewise.SelectedValue == "3")
                {
                    DataTable dt = new DataTable();

                    bool res = objEgGRNBankStatus.BankdetailRptBind(rptgrndetailbankstatus);
                    divrpt1.Visible = true;
                    divrpt2.Visible = false;
                    btnDisable();
                    if (res == false)
                    {
                        btnEnable();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                        return;
                        
                    }
                }

            }
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select Bank.')", true);
        }
    }
    protected void rptgrnbankstatus_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
        EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
        if (e.CommandName == "Tamt")
        {

            objEgGRNBankStatus.BankCode = ddlbankgrnstatus.SelectedItem.Value;
            TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
            TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
            string[] fromdate = txtfrmdate.Text.Split('/');
            objEgGRNBankStatus.FromDate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
            string[] Todate = txttodate.Text.Split('/');
            objEgGRNBankStatus.ToDate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
            EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
            string strquery = objEncrypt.Encrypt("FromDate=" + fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString() + "&ToDate=" + Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString() + "&BankCode=" + ddlbankgrnstatus.SelectedItem.Value + "&NoofDays=" + e.CommandArgument);
            string url = "EgGRNBankStatusDetail.aspx";
            string s = "window.open('" + url + "?" + strquery + "', 'popup_window', 'width=1000,height=800,left=252,top=120,center=yes,resizable=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                       
        }
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {

        if (rblbankdatewise.SelectedValue == "1")
        {
            loadReportBankwise();

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

            returnValue = rptBSRBW.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            Response.Buffer = true;
            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attachment; filename=GRNBankStatusReport(BankWise).pdf");

            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
        //    return;
        //}     

        else if (rblbankdatewise.SelectedValue == "2")
        {
            DayWiseloadReportBankwise();

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

            returnValue = rptBSRDW.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            Response.Buffer = true;
            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attachment; filename=DayWiseGRNBankStatusReport.pdf");

            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
        else
        {
            ScrollWiseloadReportBankwise();

            byte[] returnValue = null;
            string format = "PDF";
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string extension = "pdf";
            string[] streams = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            returnValue = rptBSRSW.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            Response.Buffer = true;
            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attachment; filename=ScrollWiseBankStatusReport.pdf");

            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }

        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
        //    return;
        //}
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        divrpt1.Visible = false;
        divrpt2.Visible = false;        
        txtfrmdate.Text = "";
        txttodate.Text = "";
        btnEnable();
    }
    public void btnDisable()
    {
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");        
        //ddlbankgrnstatus.SelectedIndex = 0;
        rblbankdatewise.Enabled = false;
        btnFindResult.Enabled = false;
        ddlbankgrnstatus.Enabled = false;
        txtfrmdate.Enabled = false;
        txttodate.Enabled = false;
    }
    public void btnEnable()
    {
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        rblbankdatewise.Enabled = true;
        btnFindResult.Enabled = true;
        ddlbankgrnstatus.Enabled = true;
        txtfrmdate.Enabled = true;
        txttodate.Enabled = true;
    }
}