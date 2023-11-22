using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
public partial class WebPages_Admin_EgAddDeptBudgetHead : System.Web.UI.Page
{

    EgAddDeptBudgetHead objEgAddDeptBudgetHead;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            BindRepeater();

        }

    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        objEgAddDeptBudgetHead = new EgAddDeptBudgetHead();
        if (txtBudgetHead.Text.Length > 3)
        {

            DataTable dt = new DataTable();
            dt = objEgAddDeptBudgetHead.FillMapBudgetHeadRpt();


            dt.PrimaryKey = new DataColumn[1] { dt.Columns[0] };  // set your primary key
            DataRow dRow = dt.Rows.Find(txtBudgetHead.Text.Substring(0, 4) + "-" + "00" + "-" + "000"
                                        + "-" + "00" + "-" + "00");

            if (dRow != null)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Already Exist.!')", true);
            }
            else
            {
                objEgAddDeptBudgetHead.MajorHead = txtBudgetHead.Text.Substring(0, 4);
                objEgAddDeptBudgetHead.BudgetHeadName = txtbudgetheadName.Text;
                int i = objEgAddDeptBudgetHead.InsertMapBudgetHead();
                if (i == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Saved Successfully')", true);
                    BindRepeater();
                    txtBudgetHead.Text = "";
                    txtbudgetheadName.Text = "";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Could Not be Save')", true);

                }
            }

            dt.Dispose();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Please fill correct BudgetHead')", true);
        }
        
    }
    public void BindRepeater()
    {
        objEgAddDeptBudgetHead = new EgAddDeptBudgetHead();
        DataTable dt = new DataTable();
        dt = objEgAddDeptBudgetHead.FillMapBudgetHeadRpt();
        //PagedDataSource objpds = new PagedDataSource();
        //DataView objdv = new DataView(dt);
        //objpds.DataSource = objdv;
        rptMapbudgethead.DataSource = dt;
        rptMapbudgethead.DataBind();
        dt.Dispose();
    }


}
