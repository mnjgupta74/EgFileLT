using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System.Globalization;

public partial class UserControls_wucScrollControl : System.Web.UI.UserControl
{
    decimal BAmountTotal = 0;
    decimal TAmountTotal = 0;
    double SumEAmount;
    double SumBAmount;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserID"] = "46";
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            //divTooltip.Visible = false;
            EgEchallanHistoryBL objEChallanhistory = new EgEchallanHistoryBL();
            objEChallanhistory.PopulateBankList(ddlbankname);
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DateTime? dtFrom = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : DateTime.ParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime? dtTo = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : DateTime.ParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //string strMsg = string.Empty;
        //if (dtFrom == null)
        //{
        //    strMsg += "Please enter from date";
        //}
        //else if (dtTo == null)
        //{
        //    strMsg += "Please enter To date";
        //}
        //else if (dtFrom > DateTime.Now)
        //{
        //    strMsg += "From date cannot be greator then current date";
        //}
        //else if (dtTo > DateTime.Now)
        //{
        //    strMsg += "To date cannot be greator then current date";
        //}
        //else if (dtFrom > dtTo)
        //{
        //    strMsg += "From date cannot be greator then To date";
        //}
        //else if (dtTo.Value.Subtract(dtFrom.Value).TotalDays > 4)
        //{
        //    strMsg += "Date difference cannot be more then 5 Days";
        //}
        //if (strMsg.Length > 0)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "tmp", "<script type='text/javascript' language='javascript'>alert('" + strMsg + "');</script>", false);
        //    return;
        //}
        if (dtFrom != null && dtTo != null)
        {
            //divTooltip.Visible = true;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript' language='javascript'>jquery();</script>", false);
            BindScrollData();
        }
        //lstrecord.Visible = true;

    }
    public void BindScrollData()
    {
        DataTable dt = new DataTable();
        EgAdvanceScrollRptBL objEgGRNScrollRpt = new EgAdvanceScrollRptBL();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        string[] Todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');

        objEgGRNScrollRpt.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgGRNScrollRpt.ToDate = Convert.ToDateTime(Todate[2].ToString() + "/" + Todate[1].ToString() + "/" + Todate[0].ToString());

        objEgGRNScrollRpt.BSRcode = ddlbankname.SelectedValue;
        objEgGRNScrollRpt.StatusType = Convert.ToInt16(rblstatus.SelectedValue);
        dt= objEgGRNScrollRpt.FillGRNScrollGrid();
        grdScroll.DataSource = dt;
        grdScroll.DataBind();
        //dt.Dispose();
        if (dt.Rows.Count  > 0)
        {
            divRecord.Visible = true;
            GrandTotalRow.Visible = true;

           // DataSet dataTable = (DataSet)grdScroll.DataSource;
           // DataTable dt = new DataTable();
            //dt = (DataTable)dataTable.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                SumEAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["TotalTreasuryAmount"]).Trim()) ? Convert.ToDouble("0.00") : Convert.ToDouble(dt.Rows[0]["TotalTreasuryAmount"]); //Convert.ToDouble(dt.Compute("Sum(BankAmount)", ""));
                SumBAmount = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["TotalBankAmount"]).Trim()) ? Convert.ToDouble("0.00") : Convert.ToDouble(dt.Rows[0]["TotalBankAmount"]); //Convert.ToDouble(dt.Compute("Sum(TreasuryAmount)", ""));
            }
            else
            {
                SumEAmount = 0;
                SumBAmount = 0;
            }

            lblEAmount.Text = SumEAmount.ToString("0.00");
            lblBAmount.Text = SumBAmount.ToString("0.00");
        }
        else
        {
            divRecord.Visible = false;
            GrandTotalRow.Visible = false;
        }
        dt.Dispose();
    }
    protected void grdScroll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Flag = grdScroll.DataKeys[e.Row.RowIndex][0].ToString();
            ImageButton img = (ImageButton)e.Row.FindControl("imgbtn");

            Label lblETreasuryAmount = (Label)e.Row.FindControl("lblETreasuryAmount");
            Label lblBankAmount = (Label)e.Row.FindControl("lblBankAmount");

            Label lblBankChallanDate = (Label)e.Row.FindControl("lblBankChallanDate");
            Label lblETreasuryDate = (Label)e.Row.FindControl("lblETreasuryDate");
            Label lblBankDate = (Label)e.Row.FindControl("lblBankDate");
            if (Flag == "N")
            {
                img.ImageUrl = "~/Image/delete.png";
                img.ToolTip = "GRN is not Successful";
                img.Enabled = false;
            }
            if (Flag == "D")
            {
                img.ImageUrl = "~/Image/doubt_icon.png";
                img.ToolTip = "GRN has Doubt";
                img.Enabled = false;
            }

            if (Flag == "Y")
            {
                img.ImageUrl = "~/Image/success.png";
                img.ToolTip = "GRN is Successful";
                img.Enabled = false;
            }

            lblETreasuryAmount.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TreasuryAmount")).Trim()) ? "N/A" : Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TreasuryAmount")).ToString("0.00");
            lblBankAmount.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankAmount")).Trim()) ? "N/A" : Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BankAmount")).ToString("0.00");

            lblBankChallanDate.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankChallanDate")).Trim()) ? "N/A" : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "BankChallanDate")).ToString("dd/MM/yyyy");
            lblETreasuryDate.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TreasuryDate")).Trim()) ? "N/A" : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "TreasuryDate")).ToString("dd/MM/yyyy hh:mm tt");
            lblBankDate.Text = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankDate")).Trim()) ? "N/A" : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "BankDate")).ToString("dd/MM/yyyy HH:mm:ss");


         
            //lblTotalAmount.Text = sumObject.ToString();

            lblETreasuryAmount.ForeColor = System.Drawing.Color.Black;
            lblBankAmount.ForeColor = System.Drawing.Color.Black;

            lblETreasuryDate.ForeColor = System.Drawing.Color.Black;
            lblBankDate.ForeColor = System.Drawing.Color.Black;

            if (rblstatus.SelectedValue == "1")
            {
                lblETreasuryAmount.ForeColor = System.Drawing.Color.LightGreen;
                lblBankAmount.ForeColor = System.Drawing.Color.LightSalmon;
            }
            else if (rblstatus.SelectedValue == "4")
            {
                lblETreasuryDate.ForeColor = System.Drawing.Color.LightGreen;
                lblBankDate.ForeColor = System.Drawing.Color.LightSalmon;
            }
            //else if (rblstatus.SelectedValue == "2")
            //{
            //    lblETreasuryAmount.ForeColor = "#00FF00";
            //    lblBankAmount.ForeColor = "#FF0000";
            //}
            //else if (rblstatus.SelectedValue == "3")
            //{

            //}


            TAmountTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BankAmount"));
            BAmountTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TreasuryAmount"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total:";
            e.Row.Cells[2].Text = BAmountTotal.ToString("0.00");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = TAmountTotal.ToString("0.00");
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
            TAmountTotal = 0;
            BAmountTotal = 0;
        }
    }
    protected void grdScroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindScrollData();
        grdScroll.PageIndex = e.NewPageIndex;
        grdScroll.DataBind();
    }
    protected void grdScroll_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName.Equals("View"))
        {
            int GRNnumber = Convert.ToInt32(e.CommandArgument);
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&deptcode={3}", GRNnumber.ToString(), Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));

            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }
    }
}
