using System;
using System.Web.UI;
using EgBL;
using System.Data;
public partial class WebPages_Admin_EgAdminChangeUserPassword : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            //objEgChangePasswordBL.FillLoginId(ddlLoginID);
            ChangePasswordTable.Visible = false;
            lblLegend.Visible = false;
           
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region ChangePassword
       
            EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();

            if (Page.IsValid)
            {

                string NewPass = txtNewPassword.Text;
                objEgChangePasswordBL.Password = txtNewPassword.Text;
                objEgChangePasswordBL.UserId = Convert.ToInt32(lbluserid.Text);

                if (txtNewPassword.Text.Trim() == txtCnfPassword.Text.Trim())
                {
                    string output = objEgChangePasswordBL.AdminChangeUserPassword();

                    if (output.Trim() == "2")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New Password Already Used !!')", true);
                    }
                    if (output.Trim() == "3")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('changed Successfully.')", true);
                        ResetFields();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New password and confirm password could not be same.')", true);
                }
            }
        
        #endregion
    }
    private void ResetFields()
    {
        txtlogin.Enabled = true;
        MainTable.Visible = false;
        Image1.Visible = false;
        txtlogin.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetFields();
    }

    protected void txtlogin_TextChanged(object sender, EventArgs e)
    {

        
        if (Convert.ToInt32(rblList.SelectedValue) == 0)                                                // Checks the value of selected radiobutton 
        {
            Fieldset1.Visible = true;
            EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
            DataTable dt = new DataTable();
            if (txtlogin.Text != "")
            {
                objEgChangePasswordBL.LoginID = txtlogin.Text;
                dt = objEgChangePasswordBL.GetChangeUserPasswordDetails();
                if (dt.Rows.Count > 0)
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = "~/Image/success.png";
                    Fieldset1.Visible = true;
                    lblDOB.Text = dt.Rows[0][0].ToString();
                    lblEmail.Text = dt.Rows[0][1].ToString();
                    lblMobile.Text = dt.Rows[0][2].ToString();
                    lblAddress.Text = dt.Rows[0][3].ToString();
                    LabelQuestion.Text = dt.Rows[0][5].ToString();
                    LabelAnswer.Text = dt.Rows[0][6].ToString();
                    lbluserid.Text = dt.Rows[0][4].ToString();
                    Image1.ToolTip = "LoginID is Valid.!";
                    lnkChangePassword.Text = "Click For Change Password";
                }
                else
                {
                    Fieldset1.Visible = false;
                    Image1.Visible = true;
                    Image1.ImageUrl = "~/Image/delete.png";
                    Image1.ToolTip = "LoginID is invalid.!";
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Enter LoginID.!')", true);
            }
            dt.Dispose();

        }
        else
        {
            // do if selected value is 1
          
            Fieldset1.Visible = true;
            EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
            DataTable dt = new DataTable();
            if (txtlogin.Text != "")
            {
                objEgChangePasswordBL.LoginID = txtlogin.Text;
                dt = objEgChangePasswordBL.GetChangeUserPasswordDetails();
                if (dt.Rows.Count > 0)
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = "~/Image/success.png";
                    Fieldset1.Visible = true;
                    lblDOB.Text = dt.Rows[0][0].ToString();
                    lblEmail.Text = dt.Rows[0][1].ToString();
                    lblMobile.Text = dt.Rows[0][2].ToString();
                    lblAddress.Text = dt.Rows[0][3].ToString();
                    LabelQuestion.Text = dt.Rows[0][5].ToString();
                    LabelAnswer.Text = dt.Rows[0][6].ToString();
                    lbluserid.Text = dt.Rows[0][4].ToString();
                    Image1.ToolTip = "LoginID is Valid.!";
                }
                else
                {
                    Fieldset1.Visible = false;
                    Image1.Visible = true;
                    Image1.ImageUrl = "~/Image/delete.png";
                    Image1.ToolTip = "LoginID is invalid.!";
                }

            }
            MainTable.Visible = false;
            lnkChangePassword.Text = "Reset-Login Attempt";


        }
    }


    protected void lnkChangePassword_Click(object sender, EventArgs e)
    {
        EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
        Fieldset1.Visible = false;
        txtlogin.Enabled = false;
        MainTable.Visible = true;
        if (lnkChangePassword.Text == "Reset-Login Attempt")
        {
            MainTable.Visible = false;
            objEgChangePasswordBL.LoginID = txtlogin.Text;
            int returnvalue = objEgChangePasswordBL.UpdateLoginAttempt();
            if (returnvalue == 1)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Login attempts Reset Successfully.!')", true);
                txtlogin.Text = "";
                txtlogin.Enabled = true;
                Image1.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Login Attempts Cannot be Reset.!')", true);
            }
        }

    }
    void Showdiv()
    {
        Fieldset1.Visible = false;
        MainTable.Visible = false;
        lblLegend.Visible = true;
        txtlogin.Text = "";
        txtlogin.Enabled = true;
        Image1.Visible = false;
    }

    protected void rblList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Showdiv();

        if (Convert.ToInt32(rblList.SelectedValue) == 0)
        {
            // do all when change password is selected
            
            ChangePasswordTable.Visible = true;
            lblLegend.Text = "Change-User Password";

        }
        else
        {
            ChangePasswordTable.Visible = true;
            lblLegend.Text = "Change-Login Attempt";


           // lnkChangePassword.Text = "Reset Login Attempts";
           // MainTable.Visible = false;
           // EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
           // objEgChangePasswordBL.LoginID = txtlogin.Text;
           //int returnvalue = objEgChangePasswordBL.UpdateLoginAttempt();
           //if (returnvalue == 1)
           //{
           //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Login attempts Reset Successfully.!')", true);
           //}
           //else
           //{
           //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Login Attempts Cannot be Reset.!')", true);
           //}
        }

    }
}
