using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EgDeptChangePassword : System.Web.UI.Page
{
    public static UserControl Umenu;

    protected void page_preInit(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || Session["UserID"].ToString() == "")
            this.Page.MasterPageFile = "~/Masterpage/MasterPage4.master";
        else
            this.Page.MasterPageFile = "~/Masterpage/MasterPage5.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null && Session["DeActive"] == null))
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EgLoginBL ObjLogin = new EgLoginBL();
        EgChangePasswordBL objChangePassword = new EgChangePasswordBL();
        #region old
        if (Page.IsValid)
        {   
            string OldPass = txtOldPassword.Text;
            string NewPass = txtNewPassword.Text;
            objChangePassword.OldPassword = txtOldPassword.Text;
            objChangePassword.Password = txtNewPassword.Text;
            objChangePassword.UserId = Convert.ToInt32(Session["DeActive"]);
            ObjLogin.UserId = Convert.ToInt32(Session["DeActive"]);
            ObjLogin.SessionActive(2);
            if (OldPass.ToString() != NewPass.ToString())
            {
                if (txtNewPassword.Text.Trim() == txtCnfPassword.Text.Trim())
                {
                    objChangePassword.UpdateNewPassword();
                    string output = objChangePassword.rslt;

                    if (output == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Old Password not matched.')", true);
                    }
                    if (output == "2")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New Password Already Used !!')", true);
                    }
                    if (output == "3")
                    {
                        objChangePassword.UserId = Convert.ToInt32(Session["UserId"]);
                        objChangePassword.EgUpdateDeptFlag();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                               "MessageThenRedirect", "alert('Password Changed Successfully');window.location='../default.aspx';", true);
                       
                       
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New password and confirm password could not be same.')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('New password and Old password could not be same.')", true);
            }
        }
        #endregion


    }

    private void ResetFields()
    {

        txtNewPassword.Text = "";
        txtCnfPassword.Text = "";
        txtOldPassword.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetFields();
    }
    protected void lnklogin_Click(object sender, EventArgs e)
    {
        Response.Write("<Script>alert('Session Expired')</Script>");
        Response.Redirect("~\\Default.aspx");

    }
}
