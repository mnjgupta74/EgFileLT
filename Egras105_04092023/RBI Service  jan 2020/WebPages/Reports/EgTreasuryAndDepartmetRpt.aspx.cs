using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Reports_EgTreasuryAndDepartmetRpt : System.Web.UI.Page
{
    double Totalmoney = 0;
    double TotalmoneySchema = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["UserType"].ToString().Trim() != "2")
            {
                EgEChallanBL objEgEChallanBL = new EgEChallanBL();
                objEgEChallanBL.FillTreasury(ddlTreasury);
                EgBankSoftCopyBL objEgBankSoftCopyBL = new EgBankSoftCopyBL();
                objEgBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserID"]);
                //string Treascode = objEgBankSoftCopyBL.GetBSRCode();// User For Get TreasuryCode 
                ddlTreasury.SelectedValue = objEgBankSoftCopyBL.GetBSRCode().Trim();
                ddlTreasury.Enabled = false;
            }
            rblType.SelectedValue = "1";
        }
        Page.MaintainScrollPositionOnPostBack = true;
    }

    //public void BindTreasury()
    //{
    //    EgEChallanBL objEChallan = new EgEChallanBL();
    //    objEChallan.FillLocation(ddlTreasury);
    //}
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        EgTreasuryAndDepartmetRptBL objTreasuryAndDept = new EgTreasuryAndDepartmetRptBL();
        
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        if (ddlTreasury.SelectedValue != "0")
        {

            objTreasuryAndDept.Tcode = ddlTreasury.SelectedValue;
        }
        if (rblType.SelectedValue == "1")
        {
            dt=objTreasuryAndDept.TreasuryTotalAmount();
            RptTreasury.DataSource = dt;
            RptTreasury.DataBind();
            DivBudgetText.Style.Add("display", "none");
        }
        else
        {
            DivBudgetText.Style.Add("display", "block");
            objTreasuryAndDept.Mcode = txtBudgetHead.Text.Trim();
            dt=objTreasuryAndDept.TreasuryMajorHeadAmount();
            RptTreasury.DataSource = dt;
            RptTreasury.DataBind();
        }
        if (RptTreasury.Items.Count > 0)
        {
            trTreasuryList.Visible = true;
            lblNoRec.Visible = false;

        }
        else
        {
            trTreasuryList.Visible = false;
            lblNoRec.Visible = true;

        }
        SetVisibilityFalse();
        tbldeptDetails.Style.Add("display", "block");
        // Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "DisableTextBox();", true);
        // ClientScript.RegisterStartupScript(typeof(string), "showHideControls","showHide('" + rblType.SelectedValue + "');", true);
        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "showHideControls", "showHide('" + rblType.SelectedValue + "');", true);
    }
    /// <summary>
    /// Show Treasurywise Department  List with Total Amount on  particular Treasury 
    /// </summary>
    /// <param name="source">TreasuryCode</param>
    /// <param name="e"></param>
    protected void RptTreasury_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        EgTreasuryAndDepartmetRptBL objTreasuryAndDept = new EgTreasuryAndDepartmetRptBL();
        string key = "";
        if (e.CommandName == "Tcode")
        {
            key = e.CommandArgument.ToString();

            objTreasuryAndDept.Tcode = key.ToString();
            string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
            objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
            objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

            if (rblType.SelectedValue == "1")
            {    
                dt=  objTreasuryAndDept.TreasuryDepartmentAmount();
                RptDepartment.DataSource = dt;
                RptDepartment.DataBind();
                if (RptDepartment.Items.Count > 0)
                {
                    DivDepartment.Visible = true;
                    DivTreasuryMhead.Visible = false;
                    DivBudgetHead.Visible = false;
                }
                else
                {
                    DivDepartment.Visible = false;
                    DivTreasuryMhead.Visible = false;
                    DivBudgetHead.Visible = false;
                }

                dt.Dispose();
            }
            else
            {
                objTreasuryAndDept.Mcode = txtBudgetHead.Text.ToString();
                dt= objTreasuryAndDept.FillMajorHeadListAmount();
                RptMajorHead.DataSource = dt;
                RptMajorHead.DataBind();
                if (RptMajorHead.Items.Count > 0)
                {
                    DivTreasuryMhead.Visible = true;
                    DivBudgetHead.Visible = false;
                }
                else
                {
                    DivTreasuryMhead.Visible = false;
                    DivBudgetHead.Visible = false;
                }

                dt.Dispose();
            }



            //LinkButton lnkTName = e.Item.FindControl("lnkTName") as LinkButton;
            //lblTreasury.Text = lnkTName.Text;
        }
        else if (e.CommandName == "PopUp")
        {
            if (rblType.SelectedValue == "2")
            {
                string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
                string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
                EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
                string strquery = objEncrypt.Encrypt("FromDate=" + fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString() + "&ToDate=" + todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString() + "&Tcode=" + ddlTreasury.SelectedValue + "&MajorHead=" + txtBudgetHead.Text.ToString());
                string url = "EgTreasuryAndDepartmentRptDetail.aspx";
                string s = "window.open('" + url + "?" + strquery + "', 'popup_window', 'width=1000,height=800,left=252,top=120,center=yes,resizable=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
    }
    /// <summary>
    /// Bind TotalAmount on All Treasury at Footer 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptTreasury_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkTName = e.Item.FindControl("lnkTName") as LinkButton;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double money = Convert.ToDouble(drv.Row["TotalAmount"].ToString());
            Totalmoney = Totalmoney + money;
            if (rblType.SelectedValue == "2")
            { lnkTName.Enabled = true; }
            else
            { lnkTName.Enabled = true; }
        }
        if (Totalmoney > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                LinkButton labelAmount = e.Item.FindControl("lblamt") as LinkButton;
                labelAmount.Text = Totalmoney.ToString("0.00");
            }
        }



    }

    /// <summary>
    /// Show Department wise MajorHead  List with Total Amount on  particular Department 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RptDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        EgTreasuryAndDepartmetRptBL objTreasuryAndDept = new EgTreasuryAndDepartmetRptBL();
        string key = "";
        if (e.CommandName == "DeptCode")
        {
            key = e.CommandArgument.ToString();
        }
        objTreasuryAndDept.DeptCode = Convert.ToInt32(key.ToString().Split('-').First());
        objTreasuryAndDept.Tcode = key.ToString().Split('-').Last();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        dt=objTreasuryAndDept.FillMajorHeadListAmount();
        RptMajorHead.DataSource = dt;
        RptMajorHead.DataBind();
        if (RptDepartment.Items.Count > 0)
        {
            DivTreasuryMhead.Visible = true;
        }
        else
        {
            DivTreasuryMhead.Visible = false;
        }
        DivBudgetHead.Visible = false;
        //LinkButton lnkDepartName = e.Item.FindControl("lnkDeptName") as LinkButton;
        //lblDepartment.Text = lnkDepartName.Text;
    }
    protected void RptDepartment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double money = Convert.ToDouble(drv.Row["Amount"].ToString());
            Totalmoney = Totalmoney + money;
        }
        if (Totalmoney > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblDepatment") as Label;
                labelAmount.Text = Totalmoney.ToString("0.00");
            }
        }
    }
    /// <summary>
    /// Bind TotalAmount on All MajorHead at Footer 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptMajorHead_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double moneySchema = Convert.ToDouble(drv.Row["Amount"].ToString());
            TotalmoneySchema = TotalmoneySchema + moneySchema;
        }
        if (TotalmoneySchema > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblSchemaTotal") as Label;
                labelAmount.Text = TotalmoneySchema.ToString("0.00");
            }
        }
    }
    /// <summary>
    /// Show BudgetHead List with Total Amount on  particular MajorHead 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void RptMajorHead_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        EgTreasuryAndDepartmetRptBL objTreasuryAndDept = new EgTreasuryAndDepartmetRptBL();
        string key = "";
        if (e.CommandName == "MheadCode")
        {
            key = e.CommandArgument.ToString();
        }
        objTreasuryAndDept.Tcode = key.ToString().Split('-').GetValue(0).ToString();
      
        objTreasuryAndDept.Mcode = key.ToString().Split('-').GetValue(1).ToString();
        if (rblType.SelectedValue == "1")
            objTreasuryAndDept.DeptCode = Convert.ToInt32(key.ToString().Split('-').GetValue(2));
        else
            objTreasuryAndDept.DeptCode = 0;
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        dt= objTreasuryAndDept.FillBudgetHeadListAmount();
        RptBudgetHead.DataSource = dt;
        RptBudgetHead.DataBind();
        if (RptBudgetHead.Items.Count > 0)
        {
            DivBudgetHead.Visible = true;
        }
        else
        {
            DivBudgetHead.Visible = false;
        }
    }
    /// <summary>
    ///  Bind TotalAmount on All BudgetHead at Footer 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RptBudgetHead_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double moneySchema = Convert.ToDouble(drv.Row["Amount"].ToString());
            TotalmoneySchema = TotalmoneySchema + moneySchema;
        }
        if (TotalmoneySchema > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblBudgetHeadTotal") as Label;
                labelAmount.Text = TotalmoneySchema.ToString("0.00");
            }
        }
    }
    /// <summary>
    /// Set Visibilty False
    /// </summary>
    public void SetVisibilityFalse()
    {
        DivDepartment.Visible = false;
        DivTreasuryMhead.Visible = false;
        DivBudgetHead.Visible = false;
        DivBudgetHead.Visible = false;
    }
}
