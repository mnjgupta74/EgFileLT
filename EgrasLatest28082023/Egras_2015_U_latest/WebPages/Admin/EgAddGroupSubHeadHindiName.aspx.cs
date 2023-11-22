using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
public partial class WebPages_Admin_EgAddGroupSubHeadHindiName : System.Web.UI.Page
{
    EgAddGroupSubHeadHindiNameBL objEgAddGroupSubHeadHindiNameBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindData();
        
    }
    public void BindData()
    {
        objEgAddGroupSubHeadHindiNameBL = new EgAddGroupSubHeadHindiNameBL();
        if (rblType.SelectedValue == "1")
        {
            objEgAddGroupSubHeadHindiNameBL.GetHeads(chkhead);
            DivBudgetText.Style.Add("display", "none");
        }
        else
        {
            objEgAddGroupSubHeadHindiNameBL.BudgetHead = txtBudgetHead.Text.Trim();
            objEgAddGroupSubHeadHindiNameBL.GetMajorheadAndBudgetHeadWise(chkhead);
            DivBudgetText.Style.Add("display", "block");
        }
        DivHeadlist.Style.Add("display", "block");
    }
    protected void chkhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = chkhead.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
        trheadDetails.Visible = true;
        lblhead.Text = selected[0].ToString();
        txtName.Text = "";
        chkhead.Enabled = false;
        btnShow.Enabled = false;
        rblType.Enabled = false;
        txtBudgetHead.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objEgAddGroupSubHeadHindiNameBL = new EgAddGroupSubHeadHindiNameBL();
        objEgAddGroupSubHeadHindiNameBL.BudgetHead = lblhead.Text.Replace("-", "").Trim();
        objEgAddGroupSubHeadHindiNameBL.HeadName = txtName.Text.Trim();
        if (objEgAddGroupSubHeadHindiNameBL.UpdateHeadName() == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Record Saved Successfully!');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Record Not Saved!');", true);
        }
        Reset();
        BindData();
       
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Reset();
    }
    public void Reset()
    {
        trheadDetails.Visible = false;
        chkhead.Enabled = true;
        btnShow.Enabled = true;
        chkhead.SelectedItem.Selected = false;
        rblType.Enabled = true;
        txtBudgetHead.Enabled = true;
    }
}
