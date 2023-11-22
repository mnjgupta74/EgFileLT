using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_Account_EgChangePassword : System.Web.UI.Page
{
    EgChangePasswordBL objChangePassword = new EgChangePasswordBL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            //EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            ////string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            //Response.Redirect("~\\logout.aspx");
            Response.Redirect("~\\logout.aspx");
        }
        HttpCookie returnVal = Request.Cookies["retVal"];
        returnVal.HttpOnly = true;
        returnVal.Domain = HttpContext.Current.Request.Url.Host;
        if (Convert.ToInt16(returnVal.Value) == -1)
        {

            var menu = Page.Master.FindControl("vmenu1") as UserControl;
            menu.Visible = false;
            var lnk = Page.Master.FindControl("lnkLogout") as Control;
            lnk.Visible = false;
            UserControl uc = (UserControl)this.Page.Master.FindControl("hmenu1");
            uc.Visible = false;

        }

        //if (!Page.IsPostBack)
        //{
        //    Random randomclass = new Random();
        //    Session.Add("Rnd", randomclass.Next().ToString());
        //    btnSubmit.Attributes.Add("onclick", "javascript:return clickme(" + Session["Rnd"] + ");");
        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region old

        if (Page.IsValid)
        {
            string OldPass = txtOldPassword.Text;
            string NewPass = txtNewPassword.Text;
            objChangePassword.OldPassword = txtOldPassword.Text;
            objChangePassword.Password = txtNewPassword.Text;
            objChangePassword.UserId = Convert.ToInt32(Session["UserId"]);
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
                        
                        //HttpCookie returnVal = Request.Cookies["retVal"];
                        //if (Convert.ToInt16(returnVal.Value) == -1)
                        //{
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                             "MessageThenRedirect", "alert('Password Changed Successfully');window.location='../../default.aspx';", true);

                            
                            

                        //}
                       
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
  
}