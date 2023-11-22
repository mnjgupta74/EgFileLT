using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Reports_EgGRNScrollRpt : System.Web.UI.Page
{
    decimal BAmountTotal = 0;
    decimal TAmountTotal = 0;
    double SumEAmount;
    double SumBAmount;
    int TotalENoOfTransactions;
    int TotalBNoOfTransactions;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserID"] = 46;
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            divTooltip.Visible = false;
            EgEchallanHistoryBL objEChallan = new EgEchallanHistoryBL();
            objEChallan.PopulateBankList(ddlbankname);
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            GrandTotalRow.Visible = false;
            TotalTransRow.Visible = false;
            lblemsg.Visible = false;
        }

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        divTooltip.Visible = true;
        TotalTransRow.Visible = false;
        GrandTotalRow.Visible = false;
        lblemsg.Visible = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript' language='javascript'>jquery();</script>", false);
        BindScrollData();

        //lstrecord.Visible = true;

    }
    public void BindScrollData()
    {
        
        EgGRNScrollRpt objEgGRNScrollRpt = new EgGRNScrollRpt();
        string[] fromdate = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objEgGRNScrollRpt.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
      
        objEgGRNScrollRpt.PaymentType = Online_ManualRadioButton.SelectedValue;
        objEgGRNScrollRpt.BSRcode = ddlbankname.SelectedValue;
        objEgGRNScrollRpt.StatusType = Convert.ToInt16(rblstatus.SelectedValue);
        dt = objEgGRNScrollRpt.FillGRNScrollGrid();
        grdScroll.DataSource = dt;
        grdScroll.DataBind();
        if (dt.Rows.Count > 0)
        {
            RadioButtonList radio = (RadioButtonList)this.Online_ManualRadioButton.FindControl("rdBtnList");
            divRecord.Visible = true;
            GrandTotalRow.Visible = true;
            grdScroll.Visible = true;
            TotalTransRow.Visible = true;
            lblEAmount.Text = SumEAmount.ToString("0.00");
            lblBAmount.Text = SumBAmount.ToString("0.00");
            lblETotalTransactions.Text = TotalENoOfTransactions.ToString();
            lblBTotalTransactions.Text = TotalBNoOfTransactions.ToString();
           

        }
        else
        {
            divRecord.Visible = false;
            GrandTotalRow.Visible = false;
            grdScroll.Visible = false;
            lblemsg.Visible = true;
        }
       
    }
    protected void grdScroll_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        EgGRNScrollRpt objEgGRNScrollRpt = new EgGRNScrollRpt();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Flag = grdScroll.DataKeys[e.Row.RowIndex][0].ToString();
            ImageButton img = (ImageButton)e.Row.FindControl("imgbtn");
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
            // DataSet dataTable = (DataSet)grdScroll.DataSource;
            
            //DataTable dt = new DataTable();

            //string[] fromdate = txtDate.Text.Trim().Replace("-", "/").Split('/');
            //objEgGRNScrollRpt.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

            //objEgGRNScrollRpt.PaymentType = Online_ManualRadioButton.SelectedValue;
            //objEgGRNScrollRpt.BSRcode = ddlbankname.SelectedValue;
            //objEgGRNScrollRpt.StatusType = Convert.ToInt16(rblstatus.SelectedValue);

            //dt = objEgGRNScrollRpt.FillGRNScrollGrid();
            //dt = (DataTable)dataTable.Tables[0];
            
            if (dt.Rows.Count > 0)
            {
                SumEAmount = Convert.ToDouble(dt.Compute("Sum(GbankAmount)", ""));
                SumBAmount = Convert.ToDouble(dt.Compute("Sum(UploadAmount)", ""));
                TotalENoOfTransactions = dt.Select("GbankAmount > 0").Length;
                TotalBNoOfTransactions = dt.Select("UploadAmount > 0").Length;
                grdScroll.DataSource = dt;
                
            }
                //dt = (DataTable)dataTable.Tables[0];
            
            //lblTotalAmount.Text = sumObject.ToString();

            TAmountTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "UploadAmount"));
            BAmountTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GbankAmount"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total:";
            e.Row.Cells[4].Text = BAmountTotal.ToString("0.00");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].Text = TAmountTotal.ToString("0.00");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
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
        //grdScroll.Rows
        GridView gd = (GridView)sender;
        //gd.EditRowStyle.BackColor = System.Drawing.Color.Transparent;
        foreach (GridViewRow gg in gd.Rows)
        {
            gg.BackColor = System.Drawing.Color.Transparent;
            gg.ForeColor = System.Drawing.Color.Black;
        }
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName.Equals("View"))
        {
            GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
            clickedRow.BackColor = System.Drawing.Color.DarkBlue;
            clickedRow.ForeColor = System.Drawing.Color.White;

            int GRNnumber = Convert.ToInt32(e.CommandArgument);
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&deptcode={3}", GRNnumber.ToString(), Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));

            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }
        //else
        //{
        //    GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
        //    clickedRow.BackColor = System.Drawing.Color.Cornsilk;
        //}
    }
    protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        TotalTransRow.Visible = false;
        grdScroll.Dispose();
        grdScroll.Visible = false;
        divTooltip.Visible = false;
        GrandTotalRow.Visible = false;

        lblemsg.Visible = false;
        RadioButtonList radio = (RadioButtonList)this.Online_ManualRadioButton.FindControl("rdBtnList");
        radio.Enabled = true;
        ddlbankname.Enabled = true;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        TotalTransRow.Visible = false;
        divRecord.Visible = false;
        GrandTotalRow.Visible = false;
        grdScroll.Visible = false;
        lblemsg.Visible = false;
        RadioButtonList radio = (RadioButtonList)this.Online_ManualRadioButton.FindControl("rdBtnList");
        radio.Enabled = true;
        divTooltip.Visible = false;
        ddlbankname.Enabled = true;
        ddlbankname.SelectedValue = "0";
    }
}
