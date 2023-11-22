using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System.Web.Services;

public partial class WebPages_Reports_PdAccListTreasuryWise : System.Web.UI.Page
{
    PDAccListTreasuryBL objpdbl;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtTodatebnk.Text = txtfromdatebank.Text;
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "2")
            {

                EgBankSoftCopyBL objEgBankSoftCopyBL = new EgBankSoftCopyBL();
                objEgBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserID"]);
                //PopulateTreasuryList(ddlTreasury);
                ddlTreasury.SelectedValue = objEgBankSoftCopyBL.GetBSRCode().Trim();
                ddlTreasury.Enabled = false;
            }
            objpdbl = new PDAccListTreasuryBL();
            divmjrhead.Visible = true;
            btnReset.Visible = false;
            txtTodatebnk.Enabled = false;
            //objpdbl.TreasuryCode = ddlTreasury.SelectedValue.ToString();
            //objpdbl.PopulateMajorHeadList(ddlMajorHead);
        }
    }
    protected void btnFindResult_Click(object sender, EventArgs e)
    {
        //TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");

        //TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
         string[] fromdate = txtfromdatebank.Text.Trim().Replace("-", "/").Split('/');
        string[] todate = txtTodatebnk.Text.Trim().Replace("-", "/").Split('/');
        //todate = fromdate;
        objpdbl = new PDAccListTreasuryBL();
        objpdbl.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objpdbl.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        DateTime frmdt = DateTime.Parse(objpdbl.FromDate.ToString());
        DateTime todt = DateTime.Parse(objpdbl.ToDate.ToString());
        TimeSpan difference = todt-frmdt;
        var days = difference.TotalDays;
        if ( frmdt.Month <= 3 &&todt.Month >= 4)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data will be displayed for same FinYear only. Select same FinYear.')", true);
            return;
        }
        else
        {
            
            if (days < 31)
            {
                if (ddlMajorHead.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select Major Head')", true);
                    return;
                }
                else
                    objpdbl.MajorHead = Convert.ToInt32(ddlMajorHead.SelectedValue);
                if (((objpdbl.ToDate - objpdbl.FromDate).TotalDays) < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);
                    return;
                }
                if (hdnVal.Value == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select Treasury')", true);
                    return;
                }
                objpdbl.TreasuryCode = (hdnVal.Value).ToString();
                DataTable dt = new DataTable();
                if (rblheadpd.SelectedItem.Value == "1")
                {

                    dt = objpdbl.BindPdListRepeater();
                    if (dt.Rows.Count > 0)
                    {
                        txtfromdatebank.Enabled = false;
                        ddlTreasury.Enabled = false;
                        ddlMajorHead.Enabled = false;
                        rblheadpd.Enabled = false;
                        btnReset.Visible = true;
                        rptpdacctdetail.DataSource = dt;
                        rptpdacctdetail.DataBind();
                        rptpdacctdetail.Visible = true;
                        divrpt1.Visible = true;
                        //divrpt2.Visible = false;
                        LblTotAmt.Text = Math.Round(Convert.ToDecimal(dt.Compute("Sum(Amount)", "")), 2).ToString();
                        LblTotAmt.Visible = true;
                        trlbltotamt.Visible = true;
                        BtnProcessPd.Visible = false;
                    }
                    else
                    {
                        trlbltotamt.Visible = false;
                        BtnProcessPd.Visible = false;
                        rptpdacctdetail.Visible = false;
                        rptheadwisedetail.Visible = false;
                    }
                }
                else if (rblheadpd.SelectedItem.Value == "2")
                {
                    dt = objpdbl.BindHeadWiseRepeater();
                    if (dt.Rows.Count > 0)
                    {
                        txtfromdatebank.Enabled = false;
                        ddlTreasury.Enabled = false;
                        ddlMajorHead.Enabled = false;
                        rblheadpd.Enabled = false;
                        btnReset.Visible = true;
                        rptheadwisedetail.DataSource = dt;
                        rptheadwisedetail.DataBind();
                        rptheadwisedetail.Visible = true;
                        //divrpt1.Visible = false;
                        divrpt2.Visible = true;
                        LblTotAmtHeadWise.Text = Math.Round(Convert.ToDecimal(dt.Compute("Sum(Amount)", "")), 2).ToString();
                        LblTotAmtHeadWise.Visible = true;
                        trlbltotamtHeadWise.Visible = true;
                        if (Session["UserType"].ToString() == "2")
                        {
                            BtnProcessPdHeadWise.Visible = true;
                            BtnProcessPdHeadWise.Enabled = !objpdbl.isAlreadyProcessed;
                        }
                        else
                            BtnProcessPdHeadWise.Visible = false;
                    }
                    else
                    {
                        rptpdacctdetail.Visible = false;
                        rptheadwisedetail.Visible = false;
                        trlbltotamtHeadWise.Visible = false;
                        BtnProcessPdHeadWise.Visible = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Difference b/w From Date and To Date must be less than 31 days')", true);
                return;
            }
        }
    }
    
    protected void rptheadwisedetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "HeadTotlamt")
        {
            objpdbl = new PDAccListTreasuryBL();
            objpdbl.UserId = Convert.ToInt32(Session["UserID"]);
            //TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
            //TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
            string[] fromdate = txtfromdatebank.Text.Split('/');
            objpdbl.FromDate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
            string[] Todate = txtTodatebnk.Text.Split('/');
            objpdbl.ToDate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
            EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
            string strquery = objEncrypt.Encrypt("FromDate=" + fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString() + "&ToDate=" + Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString() + "&BudgetHead=" + e.CommandArgument + "&MajorHead=" + ddlMajorHead.SelectedItem.Value + "&TreasuryCode=" + hdnVal.Value);
            string url = "PDAccListTreasuryWiseDetail.aspx";
            string s = "window.open('" + url + "?" + strquery + "', 'popup_window', 'width=1000,height=800,left=252,top=120,center=yes,resizable=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    protected void rptpdacctdetail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "PDTotlamt")
        {
            objpdbl = new PDAccListTreasuryBL();
            objpdbl.UserId = Convert.ToInt32(Session["UserID"]);
            //TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
            //TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
            string[] fromdate = txtfromdatebank.Text.Split('/');
            objpdbl.FromDate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
            string[] Todate = txtTodatebnk.Text.Split('/');
            objpdbl.ToDate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
            EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
            string strquery = objEncrypt.Encrypt("FromDate=" + fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString() + "&ToDate=" + Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString() + "&PDAcc=" + e.CommandArgument + "&TreasuryCode=" + hdnVal.Value);
            string url = "PDAccListTreasuryWiseDetail.aspx";
            string s = "window.open('" + url + "?" + strquery + "', 'popup_window', 'width=1000,height=800,left=252,top=120,center=yes,resizable=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    //protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblheadpd.SelectedItem.Value == "2")
    //    {
    //        objpdbl = new PDAccListTreasuryBL();
    //        divmjrhead.Visible = true;
    //        objpdbl.TreasuryCode = ddlTreasury.SelectedValue.ToString();
    //        objpdbl.PopulateMajorHeadList(ddlMajorHead);
    //    }
    //    else if (rblheadpd.SelectedItem.Value == "1")
    //    {
    //    }
    //}

    protected void btnReset_Click(object sender, EventArgs e)
    {
     
        ResetValue();

    }

    //protected void rblheadpd_ChangeColor(object sender,EventArgs e)
    //{


    //}

    private void ResetValue()
    {
        //TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        //TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        //txtfromdatebank.Text = "";
        txtfromdatebank.Enabled = true;
        ddlMajorHead.Enabled = true;
        ddlTreasury.Enabled = true;
        rblheadpd.Enabled = true;
        //txtTodatebnk.Text = "";
        //ddlMajorHead.SelectedValue = "0";
        //ddlTreasury.SelectedValue = "0";
        btnReset.Visible = false;
        divrpt1.Visible = false;
        divrpt2.Visible = false;
    }
    [WebMethod]
    public static string GetTreasuryList(string FromDate)
    {
        PDAccListTreasuryBL objPDAccListTreasuryBL = new PDAccListTreasuryBL();
        string[] fromdate = FromDate.Trim().Replace("-", "/").Split('/');
        objPDAccListTreasuryBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objPDAccListTreasuryBL.ToDate = objPDAccListTreasuryBL.FromDate;
        string returnVal = objPDAccListTreasuryBL.GetTreasuryListTransWise();
        
        return returnVal;
    }
    //public static string GetFinancialYear(string FromDate, string ToDate)
    //{
    //    DateTime PreYear = Convert.ToDateTime(FromDate.ToString());
    //    DateTime NexYear =Convert.ToDateTime( ToDate.ToString());
    //    string FinYear = null;
    //    if (NexYear.Month > 3)
    //        FinYear = NexYear.Year.ToString();
    //    else
    //        FinYear = (NexYear.Year-1).ToString();
    //    return FinYear.Trim();
    //}
    protected void BtnProcessPd_Click(object sender, EventArgs e)
    {
        objpdbl = new PDAccListTreasuryBL();
        //TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        //TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        string[] fromdate = txtfromdatebank.Text.Trim().Replace("-", "/").Split('/');
        string[] todate = txtTodatebnk.Text.Trim().Replace("-", "/").Split('/');
        objpdbl = new PDAccListTreasuryBL();
        objpdbl.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objpdbl.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        //objpdbl.ToDate = objpdbl.FromDate;
        DateTime frmdt = DateTime.Parse(objpdbl.FromDate.ToString());
        DateTime todt = DateTime.Parse(objpdbl.ToDate.ToString());
        TimeSpan difference = todt - frmdt;
        var days = difference.TotalDays;
        if (frmdt.Month <= 3 && todt.Month >= 4)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data will be displayed for same FinYear only. Select same FinYear.')", true);
            return;
        }
        if (((objpdbl.ToDate - objpdbl.FromDate).TotalDays) < 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);
            return;
        }
        if (hdnVal.Value == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select Treasury')", true);
            return;
        }
        objpdbl.TreasuryCode = (hdnVal.Value).ToString();
        if (ddlMajorHead.SelectedItem.Value == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select Major Head')", true);
            return;
        }
        else
        { 
            objpdbl.MajorHead = Convert.ToInt32(ddlMajorHead.SelectedValue);

        int res = objpdbl.InsertRajkosh_PDDataTransfer();
            ResetValue();
            if (res == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data Processed to PD Successfully')", true);
            return;
        }
        else if (res == -1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data already Processed')", true);
            return;
        }
        else if (res == -2)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data is UnSufficient')", true);
            return;           
        }
        
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Data Can not be Processed. Try Again')", true);
            return;
        }
            
        }

    }
}