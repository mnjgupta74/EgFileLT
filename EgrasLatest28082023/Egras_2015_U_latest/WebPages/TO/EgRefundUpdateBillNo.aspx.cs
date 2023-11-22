using AjaxControlToolkit;
using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_EgRefundUpdateBillNo : System.Web.UI.Page
{
    EgRefundChallanBL objRefund = new EgRefundChallanBL();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            SetFocus(txtgrn);

        }
        foreach (RepeaterItem item in Repeater1.Items)
        {
            CalendarExtender calFromDate = (CalendarExtender)item.FindControl("calFromDate");
            calFromDate.EndDate = DateTime.Now;
            //calFromDate.StartDate = DateTime.Now;
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();

    }

    private void BindRepeater()
    {
        EgRefundChallanBL objRefund = new EgRefundChallanBL();
        try
        {
            objRefund.Grn = Convert.ToInt32(txtgrn.Text);
            dt = objRefund.GetDetailsbyGrn();
            if (dt.Rows.Count > 0)
            {
                lblRemitterName.Visible = true;
                lblRemitterName.Text = " Remitter Name : " + dt.Rows[0]["RemitterName"].ToString();
                PanelTableShow.Visible = true;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                btnSearch.Enabled = false;
                btnReset.Enabled = true;
                txtgrn.Enabled = false;
            }
            else
            {
                PanelTableShow.Visible = false;
                btnSearch.Enabled = true;
                txtgrn.Enabled = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No Record Found');", true);
            }
        }
        catch (Exception ex)
        {
            txtgrn.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Some Technical error');", true);
        }
    }


    protected void OnEdit(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as Button).Parent as RepeaterItem;
        this.ToggleElements(item, true);
    }

    private void ToggleElements(RepeaterItem item, bool isEdit)
    {
        //Toggle Buttons.
        item.FindControl("lnkEdit").Visible = !isEdit;
        item.FindControl("lnkUpdate").Visible = isEdit;
        item.FindControl("lnkCancel").Visible = isEdit;

        //Toggle Labels.
        item.FindControl("lblBillNo").Visible = !isEdit;
        item.FindControl("lblDate").Visible = !isEdit;
        item.FindControl("lblDefaceAmount").Visible = !isEdit;

        //Toggle TextBoxes.
        item.FindControl("txtBillNo").Visible = isEdit;
        item.FindControl("txtDate").Visible = isEdit;
        item.FindControl("txtDefaceAmount").Visible = isEdit;
    }

    protected void OnCancel(object sender, EventArgs e)
    {
        //Find the reference of the Repeater Item.
        RepeaterItem item = (sender as Button).Parent as RepeaterItem;
        this.ToggleElements(item, false);
    }
    protected void OnUpdate(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as Button).Parent as RepeaterItem;
        objRefund.Sno = int.Parse((item.FindControl("lblSno") as Label).Text);
        string txtBillNo = (item.FindControl("txtBillNo") as TextBox).Text.Trim();
        string lblBillNo = (item.FindControl("lblBillNo") as Label).Text.Trim();
        if (txtBillNo == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('BillNo Cannot be blank.')", true);
            return;
        }
        else
        {
            objRefund.BillNo = Convert.ToInt32(txtBillNo);
        }

        string txtDate = (item.FindControl("txtDate") as TextBox).Text.Trim();
        if (txtDate == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Date Cannot be blank.')", true);
            return;
        }
        else
        {
           // objRefund.BillDate = Convert.ToDateTime(txtDate);
            string[] fromdate = txtDate.Split('/');
            objRefund.BillDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        }

        string lblDefaceAmount = (item.FindControl("lblDefaceAmount") as Label).Text.Trim();
        string txtDefaceAmount = (item.FindControl("txtDefaceAmount") as TextBox).Text.Trim();
        if (txtDefaceAmount == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Amount Cannot be blank.')", true);
            return;
        }
        else
        {
            objRefund.defaceAmount = Convert.ToDouble(txtDefaceAmount);
        }

        EgRefundChallanBL objrefundchallan = new EgRefundChallanBL();
        objrefundchallan.Grn = Convert.ToInt32(txtgrn.Text);
        objrefundchallan.GetPartialAmount();
        double totalDefaceAmount = objrefundchallan.amount + Convert.ToDouble(lblDefaceAmount);

        if (Math.Round(Convert.ToDouble(txtDefaceAmount), 2) > Math.Round(Convert.ToDouble(totalDefaceAmount), 2))
        {
            //ajaxloader.Style.Add("display", "none");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Amount can not be greater than Refund Amount.')", true);
            return;
        }
        objRefund.Grn = Convert.ToInt32(txtgrn.Text);
        if (lblBillNo == txtBillNo)
        {
            int k = objRefund.UpdateRefundBillNo();
            if (k == 1)
            {
                Response.Write(@"<script language='javascript'>alert('Update Successfully.');</script>");
            }
        }
        else
        {
            int J = objRefund.CheckBillNoExistence();
            if (J == 1)
            {
                Response.Write(@"<script language='javascript'>alert('Bill No Already Exist With This Grn .');</script>");
                return;
            }
            else
            {
                int i = objRefund.UpdateRefundBillNo();
                if (i == 1)
                {
                    Response.Write(@"<script language='javascript'>alert('Update Successfully.');</script>");
                }
            }
        }

        this.BindRepeater();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        PanelTableShow.Visible = false;
        txtgrn.Text = "";
        btnSearch.Enabled = true;
        txtgrn.Enabled = true;
        lblRemitterName.Visible = false;
    }
}