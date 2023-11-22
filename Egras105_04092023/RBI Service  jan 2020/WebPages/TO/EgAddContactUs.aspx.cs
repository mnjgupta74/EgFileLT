using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;

public partial class WebPages_TO_EgAddContactUs : System.Web.UI.Page
{

    EgAddContactUsBL objEgAddContactUs;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserID"] = "46";
        //Session["userName"] = "TO";
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEgAddContactUs = new EgAddContactUsBL();
            EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            BindGrid();
        }
        chkactive.Visible = false;
        LblActive.Visible = false;
        LblActdeactMsg.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            objEgAddContactUs = new EgAddContactUsBL();
            objEgAddContactUs.Name = txtFirstName.Text.Trim();
            if (txtNumber2.Text != "")
                objEgAddContactUs.ContactNo = txtNumber.Text.Trim() + ',' + txtNumber2.Text.Trim();
            else
                objEgAddContactUs.ContactNo = txtNumber.Text.Trim();
            objEgAddContactUs.EmailAddress = txtEmail.Text.Trim();

            if (chkactive.Checked == true)
                objEgAddContactUs.IsDisabled = false;
            else
                objEgAddContactUs.IsDisabled = true;


            objEgAddContactUs.Priority = Convert.ToInt32(ddlPriority.SelectedValue);

            if (btnSubmit.Text == "Submit")
            {
                int result = objEgAddContactUs.InsertContactDetail();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Saved Successfully.! ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not saved.! ')", true);
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                objEgAddContactUs.ContactId = Convert.ToInt32(ViewState["Code"]);
                int result = objEgAddContactUs.UpdateContactDetail();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Updated Successfully.! ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not Updated.! ')", true);
                }

            }
            ResetControl();
            BindGrid();
        }
    }
    public void ResetControl()
    {
        //ddlDepartment.SelectedValue = "0";
        txtFirstName.Text = "";
        txtNumber.Text = "";
        txtNumber2.Text = "";
        txtEmail.Text = "";
        btnSubmit.Text = "Submit";
        ddlPriority.SelectedIndex = 0;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControl();
        BindGrid();
    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        objEgAddContactUs = new EgAddContactUsBL();

        dt= objEgAddContactUs.gridContactUs();
        gridContactUs.DataSource = dt;
        gridContactUs.DataBind();
        ResetControl();
    }
    public void BindGridWithNoReset()
    {
        DataTable dt = new DataTable();
        objEgAddContactUs = new EgAddContactUsBL();
        dt=objEgAddContactUs.gridContactUs();
        gridContactUs.DataSource = dt;
        gridContactUs.DataBind();
    }

    protected void btnGvEdit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        int ContactId = Convert.ToInt32(gridContactUs.DataKeys[row.RowIndex]["ContactId"].ToString());//row.Cells[1].Text;
        txtFirstName.Text = row.Cells[1].Text;
        if (row.Cells[2].Text.Contains(',') == true)
        {
            txtNumber.Text = row.Cells[2].Text.Split(',').First();
            txtNumber2.Text = row.Cells[2].Text.Split(',').Last();
        }
        else
        {
            txtNumber.Text = row.Cells[2].Text;
            txtNumber2.Text = "";
        }
        txtEmail.Text = row.Cells[3].Text;
        ddlPriority.SelectedValue = (row.Cells[4].Text);
        chkactive.Visible = true;
        LblActive.Visible = true;
        
        
        if (gridContactUs.DataKeys[row.RowIndex]["IsDisabled"].ToString() == "True")
        { chkactive.Checked = false; }
        else
        { chkactive.Checked = true; }
        btnSubmit.Text = "Update";
        ViewState["Code"] = Convert.ToString(ContactId);

    }
    protected void btnGvDelete_Click(object sender, EventArgs e)
    {
        objEgAddContactUs = new EgAddContactUsBL();
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        objEgAddContactUs.ContactId = Convert.ToInt32(gridContactUs.DataKeys[row.RowIndex]["ContactId"].ToString());//row.Cells[1].Text;
        int result = objEgAddContactUs.DeleteContactDetail();
        if (result == 1)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record deleted successfully.! ')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not deleted.! ')", true);
        }
        BindGrid();
    }

    protected void gridContactUs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkListDisable = e.Row.FindControl("chkDisablePriority") as CheckBox;
            if (gridContactUs.DataKeys[e.Row.RowIndex]["IsDisabled"].ToString() == "True")
            { chkListDisable.Enabled = false;
            chkListDisable.Checked = false;
            
            }
            else
            { chkListDisable.Enabled = false;
            chkListDisable.Checked = true;
            }
        }
    }
    protected void chkactive_CheckedChanged(Object sender, EventArgs args)
    {
        LblActdeactMsg.Visible = true;
        if (chkactive.Checked == false)
        {
            LblActdeactMsg.Text = "Contact name will be deactivated";
            LblActdeactMsg.BackColor = System.Drawing.Color.Red;
            LblActdeactMsg.ForeColor = System.Drawing.Color.FloralWhite;
            chkactive.Visible = true;
            LblActive.Visible = true;
            //chkactive.Checked = false;
        }
        else
        {
            LblActdeactMsg.Text = "Contact name will be activated";
            LblActdeactMsg.BackColor = System.Drawing.Color.Green;
            chkactive.Visible = true;
            LblActive.Visible = true;
            //chkactive.Checked = true;
        }
    }
}
