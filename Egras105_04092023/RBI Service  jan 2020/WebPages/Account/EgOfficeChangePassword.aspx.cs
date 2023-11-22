using System;
using System.Web.UI;
using EgBL;

public partial class WebPages_Account_EgOfficeChangePassword : System.Web.UI.Page
{
    EgChangePasswordBL objEgChangePasswordBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEgEChallanBL = new EgEChallanBL();
            objEgEChallanBL.FillTreasury(ddlTreasury);
        }
    }
    protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOffice();
        txtlogin.Text = "";
        Image1.Visible = false;
    }
    public void BindOffice()
    {
        // EgOfficeChangePasswordBL objEgOfficeChangePasswordBL = new EgOfficeChangePasswordBL();
        objEgChangePasswordBL = new EgChangePasswordBL();
        objEgChangePasswordBL.UserId = Convert.ToInt32(Session["UserID"]);
        objEgChangePasswordBL.TreasuryCode = ddlTreasury.SelectedValue;
        objEgChangePasswordBL.BindOfficeList(ddlOfficeName);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region ChangePassword
        objEgChangePasswordBL = new EgChangePasswordBL();
        if (Page.IsValid)
        {

            string NewPass = txtNewPassword.Text;
            objEgChangePasswordBL.LoginID = txtlogin.Text.Trim();
            objEgChangePasswordBL.Password = txtNewPassword.Text;


            if (txtNewPassword.Text.Trim() == txtCnfPassword.Text.Trim())
            {
                string output = objEgChangePasswordBL.AdminChangeUserPassword();

                switch (output)
                {

                    case "2":
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New Password Already Used !!')", true);
                        break;

                    case "3":
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Password Changed Successfully.')", true);
                        ResetData();
                        break;

                    case "4":
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('User Does Not Exits !!')", true);
                        break;

                    default:
                        
                        break;
                }
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New password and confirm password could not be same.')", true);

            }
        }
        #endregion
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetData();
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //txtlogin.Text = ddlOfficeName.SelectedValue.ToString().Trim();
        txtlogin.Enabled = false;
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        EgForgotPasswordBL objForgot = new EgForgotPasswordBL();
        objUserReg.LoginID = ddlOfficeName.SelectedValue.ToString().Trim();
        int user = objUserReg.CheckExistingLogin();
        if (user != 0)
        {
            objForgot.UserId = user;
          
            
            txtlogin.Text= objForgot.GetLoginByUserId().Trim();
            
            Image1.Visible = true;
            Image1.ImageUrl = "~/Image/success.png";
            btnSubmit.Enabled = true;
        }
        else
        {
            Image1.Visible = true;
            Image1.ImageUrl = "~/Image/delete.png";
            btnSubmit.Enabled = false;
        }
    }
    public void ResetData()
    {
        ddlTreasury.SelectedValue = "0";
        BindOffice();
        txtlogin.Text = "";
        txtNewPassword.Text = "";
        txtCnfPassword.Text = "";
        Image1.Visible = false;
        btnSubmit.Enabled = true;
    }
}
