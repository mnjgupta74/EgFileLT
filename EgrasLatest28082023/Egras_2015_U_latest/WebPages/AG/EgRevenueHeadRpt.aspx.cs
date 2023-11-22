using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
public partial class WebPages_EgRevenueHeadRpt : System.Web.UI.Page
{

    EgDeptAmountRptBL objRevenueHeadRpt;
    double Totalmoney = 0;
    double TotalmoneySchema = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            Departmentlist();
            calFromDate.EndDate = DateTime.Now;
            calToDate.EndDate = DateTime.Now;
            //txtFromDate.Text = "01/12/2012";
            //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        EgDeptAmountRptBL objTreasuryAndDept = new EgDeptAmountRptBL();
        objTreasuryAndDept.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objTreasuryAndDept.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objTreasuryAndDept.BudgetHead = txtBudgetHead.Text.Trim();
        objTreasuryAndDept.type = Convert.ToInt16(rblType.SelectedValue);
        if ((objTreasuryAndDept.ToDate - objTreasuryAndDept.FromDate).TotalDays > 180)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
            return;
        }

        //objTreasuryAndDept.UserId = Convert.ToInt32(Session["UserId"]);
        //objTreasuryAndDept.UserType = Convert.ToInt32(Session["UserType"]);
           dt= objTreasuryAndDept.TreasuryTotalAmount();
           RptTreasury.DataSource = dt;
           RptTreasury.DataBind();
           lblTotalTreasuryWise.Visible = true;
           lblTotalTreasuryWiseAmount.InnerText = (Totalmoney / 10000000).ToString("0.00");

        if (RptTreasury.Items.Count > 0)
        {
            trTreasuryList.Visible = true;

        }
        else
        {
            trTreasuryList.Visible = false;
            rptSchemaDiv.Visible = false;
            DivDepartment.Visible = false;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('No Record Found !!')", true);
            return;
        }
        rptSchemaDiv.Visible = false;
        DivDepartment.Visible = false;
        dt.Dispose();
    }

    public void Departmentlist()
    {
        EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
        objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session["UserId"]);
        objEgDeptAmountRptBl.PopulateDepartmentList(ddlDepartment);
        if (Session["UserType"].ToString().Trim() == "5")
        {
            ddlDepartment.SelectedIndex = 1;
            ddlDepartment.Enabled = false;
        }
    }
    protected void RptTreasury_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
        objRevenueHeadRpt = new EgDeptAmountRptBL();
        string key = "";
        if (e.CommandName == "Tcode")
        {
            key = e.CommandArgument.ToString();
        }
        objRevenueHeadRpt.TreasuryCode = key.ToString();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objRevenueHeadRpt.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');

        objRevenueHeadRpt.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objRevenueHeadRpt.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
        objRevenueHeadRpt.BudgetHead = txtBudgetHead.Text.Trim();
        DataTable dt = new DataTable();
        dt=objRevenueHeadRpt.ShowRptHeadWise();
        rpt.DataSource = dt;
        rpt.DataBind();

        if (rpt.Items.Count > 0)
        {
            DivDepartment.Visible = true;

        }
        else
        {
            DivDepartment.Visible = false;
            rptSchemaDiv.Visible = false;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('No Record Found !!')", true);
            return;
        }
        rptSchemaDiv.Visible = false;

        //  DivTreasuryMhead.Visible = false;
        /// DivBudgetHead.Visible = false;
        LinkButton lnkTName = e.Item.FindControl("lnkTName") as LinkButton;
        lblTreasury.Text = lnkTName.Text;
        dt.Dispose();
    }
    protected void RptTreasury_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        LinkButton lnkTName = e.Item.FindControl("lnkTName") as LinkButton;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double money = Convert.ToDouble(drv.Row["TotalAmount"].ToString());
            Totalmoney = Totalmoney + money;

        }
        if (Totalmoney > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblamt") as Label;
                labelAmount.Text = Totalmoney.ToString("0.00");
            }
        }



    }
    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        objRevenueHeadRpt = new EgDeptAmountRptBL();
        string key = "";
        if (e.CommandName == "Show")
        {
            key = e.CommandArgument.ToString();
        }
        objRevenueHeadRpt.BudgetHead = key.Replace("-", "").Substring(4, 13);
        objRevenueHeadRpt.TreasuryCode = key.Replace("-", "").Substring(0, 4);
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objRevenueHeadRpt.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');

        objRevenueHeadRpt.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        objRevenueHeadRpt.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);

        dt=objRevenueHeadRpt.shqpSchemaRpt();
        rptschema.DataSource = dt;
        rptschema.DataBind();
        if (dt.Rows.Count > 0)
        {
            //lblBudgethead.Text = key.ToString();
            //lblBudgethead.Visible = true;
            rptSchemaDiv.Visible = true;
        }
        else
        {
            //lblBudgethead.Visible = false;
            rptSchemaDiv.Visible = false;

            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('No Record Found !!')", true);
            return;
        }
        LinkButton lnkDepartName = e.Item.FindControl("lnkBudgethead") as LinkButton;
        lblHead.Text = lnkDepartName.Text;
        dt.Dispose();

    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double money = Convert.ToDouble(drv.Row["TotalAmt"].ToString());
            Totalmoney = Totalmoney + money;
        }
        if (Totalmoney > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblamt") as Label;
                labelAmount.Text = Totalmoney.ToString("0.00");
            }
        }
    }
    protected void rptschema_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;
            double moneySchema = Convert.ToDouble(drv.Row["TotAmt"].ToString());
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
}
