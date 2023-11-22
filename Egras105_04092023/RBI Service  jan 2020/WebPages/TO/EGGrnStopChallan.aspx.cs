using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EGGrnStopChallan : System.Web.UI.Page
{
    EgStopChallan objEgStopChallan = new EgStopChallan();
    int SNo = 1;
    decimal AmountTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            EgEchallanHistoryBL objEgEChallanBL = new EgEchallanHistoryBL();
            objEgEChallanBL.PopulateBankList(ddlBank);
        }
    }
    public void BindGrid()
    {
        if (rdl_Active_Deactive.SelectedValue == "S")
        {
            objEgStopChallan.Grn = Convert.ToInt64(txtGrn.Text);
            objEgStopChallan.PaymentType = Online_ManualRadioButton.SelectedValue;
            objEgStopChallan.GetGrnData(grdGrnChallan);
        }
        else
        {
            SetData();
            objEgStopChallan.GetGRNlistDeActive(grdGrnChallan);
        }
        if (grdGrnChallan.Rows.Count > 0)
        {

            brnActiveDactive.Visible = true;
            brnActiveDactive.Enabled = true;
        }
        else
        {

            brnActiveDactive.Visible = false;
        }
    }
    public void SetData()
    {
        objEgStopChallan.BankCode = ddlBank.SelectedValue;
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgStopChallan.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgStopChallan.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        grdGrnChallan.DataSource = null;
        grdGrnChallan.DataBind();
        tr_ShowRecord.Visible = true;
        BindGrid();
    }

    protected void brnActiveDactive_Click(object sender, EventArgs e)
    {
        if (grdGrnChallan.Rows.Count != 0)
        {

            foreach (GridViewRow row in grdGrnChallan.Rows)
            {
                CheckBox CheckBox1 = (CheckBox)row.FindControl("chkBox");
                if (CheckBox1.Checked == true)
                {
                    objEgStopChallan.Grn = Convert.ToInt32(row.Cells[1].Text);
                    objEgStopChallan.Amount = Convert.ToDecimal(row.Cells[2].Text);
                    objEgStopChallan.UserID = Convert.ToInt64(Session["UserId"].ToString());
                    objEgStopChallan.BankCode = grdGrnChallan.DataKeys[row.RowIndex][2].ToString();
                    objEgStopChallan.type = (CheckBox1.Checked == true && row.Cells[5].Text.ToString() == "Active") ? "1" : "2";
                    objEgStopChallan.BankDate=Convert.ToDateTime(row.Cells[4].Text);
                    int ret = objEgStopChallan.InsertSelectedGrnData();
                }
            }
            BindGrid();
        }
    }

    protected void rdl_Active_Deactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_ShowRecord.Visible = false;
        grdGrnChallan.DataSource = null;
        brnActiveDactive.Visible = false;
       
        if (rdl_Active_Deactive.SelectedValue == "S")
        {
            tr_GrnRelease.Visible = false;
            tr_GrnStopChallan.Visible = true;
        }
        else
        {
            tr_GrnRelease.Visible = true;
            tr_GrnStopChallan.Visible = false;
        }
    }
    protected void grdGrnChallan_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("lblSerial");
            lblSerial.Text = SNo.ToString();
            SNo = SNo + 1;
            AmountTotal = AmountTotal+ Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));

            string flag = e.Row.Cells[5].Text;
            e.Row.Cells[5].Text = flag == "D" ? "Stopped" : "Active";
            brnActiveDactive.ForeColor = flag == "D" ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            brnActiveDactive.Text = flag == "D" ? "Active" : "Stop";
            e.Row.Cells[5].BackColor = flag == "D" ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            e.Row.Cells[5].Font.Bold = true;
            e.Row.Cells[5].ForeColor = System.Drawing.Color.White;

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total:";
            e.Row.Cells[5].Text = AmountTotal.ToString("0.00");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;

        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (grdGrnChallan.Rows.Count > 0)
        {
            CheckBox chHeader = (CheckBox)grdGrnChallan.HeaderRow.Cells[5].FindControl("chkAll");

            if (chHeader.Checked == true)
            {
                foreach (GridViewRow row in grdGrnChallan.Rows)
                {

                    CheckBox CheckBox1 = (CheckBox)row.FindControl("chkBox");
                    CheckBox1.Checked = true;
                    CheckBox1.Enabled = false;
                }
                chHeader.Text = "Select ALL";
            }
            else
            {
                if (chHeader.Checked == false)
                {
                    foreach (GridViewRow row in grdGrnChallan.Rows)
                    {
                        CheckBox CheckBox1 = (CheckBox)row.FindControl("chkBox");
                        CheckBox1.Checked = false;
                        CheckBox1.Enabled = true;

                    }
                    chHeader.Text = "UnSelect ALL";
                }
            }

        }

    }
    protected void grdGrnChallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SetData();
        grdGrnChallan.PageIndex = e.NewPageIndex;
        grdGrnChallan.DataBind();
    }
}
