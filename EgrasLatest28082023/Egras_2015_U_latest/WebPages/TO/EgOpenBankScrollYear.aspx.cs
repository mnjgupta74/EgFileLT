using EgBL;
using System;
using System.Web.UI;

public partial class WebPages_TO_EgOpenBankScrollYear : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["Page"] != null && (Request.QueryString["Page"] == "" || Request.QueryString["Page"] != ""))
        //{
        //    GeneralClass.ShowMessageBox("You are not authorized to access the page");
        //}

        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
            objEgGRNBankStatus.PopulateBankList(ddlbankgrnstatus);
            CalenderYear.FinYearDropdown();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlbankgrnstatus.SelectedValue = "0";
        txtDays.Text = "";
        CalenderYear.SelectedIndex = 0;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlbankgrnstatus.SelectedValue != "0" && !string.IsNullOrEmpty(txtDays.Text))
            {
                EgOpenYearForBank_BL objEgOpenBankScroll = new EgOpenYearForBank_BL();
                objEgOpenBankScroll.userid = Convert.ToInt32(Session["UserId"]);
                objEgOpenBankScroll.year = CalenderYear.SelectedValue;
                objEgOpenBankScroll.bsrcode = ddlbankgrnstatus.SelectedValue;
                objEgOpenBankScroll.duration = Convert.ToInt16(txtDays.Text);
                string a = objEgOpenBankScroll.UpdateBankStatus();
                if (a == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "onload", "alert('Record Updated Successfully !!');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "onload", "alert('Record Not Updated Successfully !!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "onload", "alert('Please Select Bank Or Fill Duration !!');", true);
            }
        }
        catch { }
    }
}