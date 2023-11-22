using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_Reports_EgDeptAmount : System.Web.UI.Page
{
    int SNo = 1;
    decimal AmountTotal = 0;
    double Totalmoney = 0;
    double TotalmoneySchema = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {

            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
            objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEgDeptAmountRptBL.PopulateDepartmentList(ddldepartment);
            if (Session["UserType"].ToString().Trim() == "5")
            {
                ddldepartment.SelectedIndex = 1;
                ddldepartment.Enabled = false;
            }
        }
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        //if (rblTransactionType.SelectedValue.Trim() != "")
        //{
            EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
            string[] fromDate = txtFromDate.Text.Split('/');
            objEgDeptAmountRptBL.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
            string[] ToDate = txtToDate.Text.Split('/');
            objEgDeptAmountRptBL.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
            objEgDeptAmountRptBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        // objEgDeptAmountRptBL.TransactionType = Convert.ToInt32(rblTransactionType.SelectedValue);

                try
                    {
                          dt= objEgDeptAmountRptBL.ShowRptDeptWise();
                          rpt.DataSource = dt;
                          rpt.DataBind();

                     }
                catch (Exception ex)
                    {
                        EgErrorHandller obj = new EgErrorHandller();
                         obj.InsertError(ex.StackTrace.ToString());
                    }
            if (rpt.Items.Count > 0)
            {
                RptDiv.Visible = true;

            }
            else
            {
                RptDiv.Visible = false;
                rptSchemaDiv.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('No Record Found !!')", true);
                return;
            }
            rptSchemaDiv.Visible = false;
        // }
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Your Trasaction Type.!!')", true);
        //}
        dt.Dispose();
    }

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        //if (rblTransactionType.SelectedValue.Trim() != "")
        //{
            string key = "";
            if (e.CommandName == "Show")
            {
                key = e.CommandArgument.ToString();
            }
            EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
            string[] fromDate = txtFromDate.Text.Split('/');
            objEgDeptAmountRptBL.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
            string[] ToDate = txtToDate.Text.Split('/');
            objEgDeptAmountRptBL.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
            objEgDeptAmountRptBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
            objEgDeptAmountRptBL.ScheCode = Convert.ToInt32(key);
            LinkButton btn = (LinkButton)e.Item.FindControl("lnkBudgethead");
            objEgDeptAmountRptBL.BudgetHead = btn.Text.ToString().Split('-')[0].ToString();
        //  objEgDeptAmountRptBL.TransactionType = Convert.ToInt32(rblTransactionType.SelectedValue);

             try
              {
                         dt=   objEgDeptAmountRptBL.showSchemaRpt();
                         rptschema.DataSource = dt;
                         rptschema.DataBind();
              }
             catch(Exception ex)
                 {
                             EgErrorHandller obj = new EgErrorHandller();
                             obj.InsertError(ex.StackTrace.ToString());
                 }
            if (rptschema.Items.Count > 0)
            {
                lblBudgethead.Text = btn.Text;
                lblBudgethead.Visible = true;
                rptSchemaDiv.Visible = true;

            }
            else
            {
                lblBudgethead.Visible = false;
                rptSchemaDiv.Visible = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('No Record Found !!')", true);
                return;
            }
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Your Trasaction Type.!!')", true);
        //}
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


    protected void rblTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddldepartment.SelectedValue = "0";
        RptDiv.Visible = false;
        rptSchemaDiv.Visible = false;
    }
}