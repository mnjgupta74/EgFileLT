using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_TO_EgAddBudgethead : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtBudgetHead.Text.Length == 13)
            {
                EgAddBudgetHead objAddBH = new EgAddBudgetHead();
                objAddBH.BudgetHead = txtBudgetHead.Text;
                int val = objAddBH.InsertBudgetHead();
                string msg = val == 1 ? "BudgetHead Add SuccessFully." : "BudgetHead Could not be Add.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('" + msg + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Please Enter Valid Budget Head')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Entry is Not Valid.!')", true);
        }
        txtBudgetHead.Text = string.Empty;
    }

}
