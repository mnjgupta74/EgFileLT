using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
public partial class WebPages_EgChallanDateWise : System.Web.UI.Page
{
    // 
    DataTable dt1 = new DataTable();
    EgChallanDateWise objEgChallan = new EgChallanDateWise();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["UserId"]) == 52)
            {
                Departmentlist();
                trmain.Visible = true;
            }
            else
            {
                trmain.Visible = false;
            }
            CurrentPage = 1;
        }

    }
    public void Departmentlist()
    {
        EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
        objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session["UserId"]);
        objEgDeptAmountRptBl.PopulateDepartmentList(ddlDepartment);
    }
    public void ChallanList()
    {
        DataTable dt = new DataTable();
        if (rblTransactionType.SelectedValue.Trim() != "")
        {
            int TotalCount;
            string[] fromDate = txtFromDate.Text.Split('/');
            objEgChallan.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
            string[] ToDate = txtToDate.Text.Split('/');
            objEgChallan.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
            int ItemPerPage = 10;
            if (CurrentPage == 1)
                objEgChallan.StartIdx = 0;
            else
                objEgChallan.StartIdx = ((CurrentPage - 1) * ItemPerPage);

            if (Convert.ToInt32(Session["UserId"]) == 52)
            {
                trmain.Visible = true;
                objEgChallan.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            else
            {
                trmain.Visible = false;
                objEgChallan.DeptCode = 0;
            }
            objEgChallan.UserId = Convert.ToInt32(Session["UserId"]);
            objEgChallan.EndIdx = objEgChallan.StartIdx + ItemPerPage;
            objEgChallan.Type = Convert.ToInt32(rblTransactionType.SelectedValue);
            dt = objEgChallan.ChallanList();
            DLMain.DataSource = dt;
            DLMain.DataBind();
            dt.Dispose();
            TotalCount = Convert.ToInt32(objEgChallan.totalcount);
            lblTotRecord.Text = "Total Records :" + TotalCount;
            lblTotAmt.Text = "Total Amount :" + System.Decimal.Round(Convert.ToDecimal(objEgChallan.TotalAmount), 2).ToString("0.00");
            if (DLMain.Items.Count == 0)
            {
                lnkpre.Visible = false;
                lnknext.Visible = false;
                Label1.Visible = true;
                lnk_first.Visible = false;
                lnk_last.Visible = false;
                lblCurrentPage.Visible = false;
                lblTotAmt.Visible = false;
                lblTotRecord.Visible = false;
                Label1.Text = "No Record Found";
                return;
            }

            else
            {
                Label1.Visible = false;
                lnkpre.Visible = true;
                lnknext.Visible = true;
                lnk_first.Visible = true;
                lnk_last.Visible = true;
                lblCurrentPage.Visible = true;
                lblTotAmt.Visible = true;
                lblTotRecord.Visible = true;
                lblCurrentPage.Text = "Page Number : " + CurrentPage;

            }
            if (CurrentPage == 1)
            {
                lnkpre.Enabled = false;
                lnk_first.Enabled = false;
            }
            else
            {
                lnkpre.Enabled = true;
                lnk_first.Enabled = true;

            }
            if (CurrentPage == ((TotalCount / 10) + 1))
            {
                lnknext.Enabled = false;
                lnk_last.Enabled = false;
                //ClearRepeater();
            }
            else
            {
                lnknext.Enabled = true;
                lnk_last.Enabled = true;

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Your Trasaction Type.!!')", true);
        }
    }
    protected void lnkpre_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        ChallanList();
    }
    protected void lnknext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        ChallanList();
    }
    public int CurrentPage   // maintain indexing with currentpage 
    {
        get
        {
            // look for current page in ViewState
            object o = this.ViewState["_CurrentPage"];
            if (o == null)
                return 0; // default page index of 0
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        CurrentPage = 1;
        ChallanList();
        //dt.Dispose();

    }
    protected void DLMain_ItemDataBound(object sender, DataListItemEventArgs e)
    {


        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            dt1.Clear();
            dt1.Dispose();
            DataRowView dr = (DataRowView)e.Item.DataItem;
            Int32 GRN = Convert.ToInt32(dr["GRN"]);

            objEgChallan.GRN = GRN;
            objEgChallan.BudgeAmountList(dt1);

            Repeater innerRepeter = ((Repeater)e.Item.FindControl("rptBudget"));
            innerRepeter.ItemDataBound += new RepeaterItemEventHandler(innerRepeter_ItemDataBound);
            innerRepeter.DataSource = dt1;

            innerRepeter.DataBind();


        }



    }

    void innerRepeter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblDes = ((Label)e.Item.FindControl("lblded"));
            Label lblTotal = ((Label)e.Item.FindControl("lbltot"));
            lblDes.Text = dt1.Rows[0]["DeductCommission"].ToString();
            lblTotal.Text = dt1.Rows[0]["TotalAmount"].ToString();

        }

    }
    protected void lnk_first_Click(object sender, EventArgs e)
    {
        lnkpre.Enabled = false;
        lnk_first.Enabled = false;
        CurrentPage = 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        ChallanList();
    }
    protected void lnk_last_Click(object sender, EventArgs e)
    {

        string[] Split = lblTotRecord.Text.Split(':');
        CurrentPage = (Convert.ToInt32(Split[1].ToString()) / 10) + 1;
        lblCurrentPage.Text = "Page Number : " + CurrentPage.ToString();
        ChallanList();
        lnknext.Enabled = false;
        lnk_last.Enabled = false;
    }


}