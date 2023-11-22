using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_TO_EgMapBudgethead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            //if (HttpContext.Current.Cache["DepartmentList"] == null)
            //{
                EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
                objUserProfileBL.PopulateDepartmentList(ddldepartment);
            //}
            //else
            //{
            //    ddldepartment.DataSource = HttpContext.Current.Cache["DepartmentList"];
            //    ddldepartment.DataTextField = "deptnameEnglish";
            //    ddldepartment.DataValueField = "deptcode";
            //    ddldepartment.DataBind();
            //    ddldepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            //}
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            EgDeptAmountRptBL objDept = new EgDeptAmountRptBL();
            objDept.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
            objDept.BudgetHead = txtBudgetHead.Text;
            objDept.UserId = Convert.ToInt32(Session["UserID"]);
            string Result = objDept.CheckBudgetHeadMapping();
            if (Result == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Invalid BudgetHead.')", true);
            }
            else if (Result == "2")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('BudgetHead already Mapped.')", true);
            }
            else if (objDept.MapBudgetHead() == 1)

            {
                objDept.InsertBudgetHeadMappingLog();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('BudgetHead added.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('BudgetHead Not added.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this , this.GetType(), "PopupScript", "alert('Entry is Not Valid.!')", true);
        }
    }
}
