using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using EgBL;
using System.Collections.Generic;

public partial class WebPages_MasterPages_EgMasterSchema : System.Web.UI.Page
{
    EgUserProfileBL objEgUserProfile = new EgUserProfileBL();
    EgMasterSchemaBL objMasterschema = new EgMasterSchemaBL();
    EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEgDeptAmountRptBL.PopulateDepartmentList(ddldepartment);
            ddlbudgethead.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    /// <summary>
    /// Bind BudgetHeadList according Department
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objEgUserProfile.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objEgUserProfile.filldropdownbudgetheadlist(ddlbudgethead);
        grdbudgetschema.Visible = false;
    }  

    /// <summary>
    /// Used to save schema data into datatbse
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {        
        if (CheckDuplicateSchema(txtschemaname.Text) == 1)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored Already Exist ')", true);
        else
        {
             
            if (btnsave.Text == "Save")
            {
                string a = ddlbudgethead.SelectedItem.Text;
                objMasterschema.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                objMasterschema.BudgetHead = a.Replace("-", "").ToString();
                objMasterschema.SchemaName = txtschemaname.Text;
               
                int output = objMasterschema.insertbudgetschema();
                if (output == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:save();", true);
                    txtschemaname.Text = "";
                    grdbudgetschema.Visible = true;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:save1();", true);
                }
            }

            else if (btnsave.Text == "Update")
            {
                string a = ddlbudgethead.SelectedItem.Text;
              
                objMasterschema.BudgetHead = a.Replace("-", "").ToString();
                objMasterschema.SchemaName = txtschemaname.Text;
                objMasterschema.ScheCode =  int.Parse(ViewState["Code"].ToString());
                             
                int output = objMasterschema.Updatebudgetschema();
                if (output == 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
                    grdbudgetschema.Visible = true;
                    setUpdate();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success1();", true);
                }
            }
            bindgrid();


        }
    }   

    /// <summary>
    /// Bind Schemas according to budgethead
    /// </summary>
    public void bindgrid()
    {
        DataTable dt = new DataTable();
        objMasterschema.BudgetHead = ddlbudgethead.SelectedItem.Text.Replace("-", "").ToString();

        dt=  objMasterschema.fillbudgetschemagrid();
        if (dt.Rows.Count > 0)
        {
            grdbudgetschema.DataSource = dt;
            grdbudgetschema.DataBind();
        }
        dt.Dispose();

    }

    /// <summary>
    /// resets all controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Reset();
    }

    /// <summary>
    ///  Bind Schemas according to budgethead
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlbudgethead_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdbudgetschema.Visible = true;
        bindgrid();
        if (grdbudgetschema.Rows.Count != 0)
        {
            grdbudgetschema.Visible = true;
        }
        else
        {
            grdbudgetschema.Visible = false;
        }
    }     

    /// <summary>
    /// resets all controls
    /// </summary>
    protected void Reset()
    {
        ddlbudgethead.SelectedIndex = 0;
        ddldepartment.SelectedIndex = 0;
        txtschemaname.Text = "";
        btnsave.Text = "Save";
        grdbudgetschema.Visible = false;
    }

 

    /// <summary>
    /// set values for updation of all controls
    /// </summary>
    protected void setUpdate()
    {
        ddldepartment.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        ddlbudgethead.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        txtschemaname.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        txtschemaname.Text = "";
        btnsave.Text = "Save";
    }

    /// <summary>
    /// Check if same Schema name exists
    /// </summary>
    /// <param name="Schema"></param>
    /// <returns></returns>
    private int CheckDuplicateSchema(string Schema)  // to verify the ddo in gridview for duplicacite  
    {
        int x = 0;
        for (int i = 0; i < grdbudgetschema.Rows.Count; i++)
        {
            string Schemaname = grdbudgetschema.Rows[i].Cells[2].Text.Trim();
           
            if (Schemaname.Equals(Schema.ToString().Trim()))
            {
                x = 1;
                break;
            }
        }

        return x;
    }

    /// <summary>
    /// used ot edit the schema records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGvEdit_Click(object sender, EventArgs e)
    {      
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        string dept = grdbudgetschema.DataKeys[row.RowIndex]["DeptName"].ToString();//row.Cells[1].Text;
        string[] deptCode = dept.Trim().Replace("-", "/").Split('/');
        ddldepartment.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
        ddlbudgethead.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
        txtschemaname.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F0E1");
        ddldepartment.SelectedValue = deptCode[0].ToString();
        ddlbudgethead.SelectedItem.Text = row.Cells[1].Text;
        txtschemaname.Text = row.Cells[2].Text.Trim();
        btnsave.Text = "Update";
        ViewState["Code"] = grdbudgetschema.DataKeys[row.RowIndex]["ScheCode"].ToString();       

    }
}