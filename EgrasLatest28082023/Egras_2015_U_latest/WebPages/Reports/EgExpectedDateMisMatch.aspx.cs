using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Reports_EgExpectedDateMisMatch : System.Web.UI.Page
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
            objEgGRNBankStatus.PopulateBankList(ddlbankgrnstatus);
        }
    }
    protected void btnFindResult_Click(object sender, EventArgs e)
    {
        objEgGRNBankStatus.BankCode = ddlbankgrnstatus.SelectedItem.Value;
        //objEgGRNBankStatus.FromDate = DateTime.ParseExact((txtfromdatebank.Text).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //objEgGRNBankStatus.ToDate = DateTime.ParseExact((txtTodatebnk.Text).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

        string[] fromdate = txtfromdatebank.Text.Trim().Replace("-", "/").Split('/');
        objEgGRNBankStatus.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtTodatebnk.Text.Trim().Replace("-", "/").Split('/');
        objEgGRNBankStatus.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        if (((objEgGRNBankStatus.ToDate - objEgGRNBankStatus.FromDate).TotalDays) < 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);
            return;
        }
        else
        {
            bool res = objEgGRNBankStatus.BankdetailRptBind(rptgrndetailbankstatus);
            if (res == false)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                return;
            }
        }
    }
}