using System;
using System.Web.UI;
using EgBL;
using System.Net;
using System.Web;
using System.Text;
using System.Security.Cryptography.X509Certificates;
public partial class WebPages_EgForgotPassword : System.Web.UI.Page
{

    //ViewState ["MobileNumber"];
    //static int TotalHit = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.SetFocus(txt_Login);
        if (!IsPostBack)
        {
            lblLogin.Text = "Login ID/Mobile No";

            if (Request.QueryString.Count > 0)
            {
                string id = Request.QueryString[0].ToString();
                StringBuilder inSb = new StringBuilder(id);
                StringBuilder outSb = new StringBuilder(id.Length);

                char c;
                for (int i = 0; i < id.Length; i++)
                {
                    c = inSb[i];
                    c = (char)(c ^ 13); /// remember to use the same XORkey value you used in javascript
                    outSb.Append(c);
                }
                string idvalue = outSb.ToString();
                if (idvalue == "ForgetPassword" || idvalue == "ResetAttempt")
                {
                    ViewState["idValue"] = idvalue.ToString();
                }
                else
                {
                    Response.Redirect("~\\logout.aspx");
                }
            }
            else
            {
                Response.Redirect("~\\logout.aspx");
            }
        }
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~\\Default.aspx");
    //}
    protected void btnSub_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            EgForgotPasswordBL objForgotPassword = new EgForgotPasswordBL();
            //string imageValue = inpHide.Value;
            string imageValue = inpHide.Text;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            if (HttpContext.Current.Session["captcha"] == null || HttpContext.Current.Session["captcha"] == "")
            {
                GeneralClass.ShowMessageBox("Please arrange Alphabets in Order!");
                return;
            }
            if (HttpContext.Current.Session["captcha"].ToString().ToLower() != "" && imageValue != "" && imageValue != "0")
            {
                if (HttpContext.Current.Session["captcha"].ToString().ToLower() == imageValue)
                {
                    Session["captcha"] = null;
                    lblMsg.Visible = true;
                    inpHide.Text = "0";
                    objForgotPassword.LoginID = txt_Login.Text.Trim();
                    string a = Convert.ToString(objForgotPassword.CheckExistingLogin());
                   
                    if (a == "" || a == null)
                    {

                        lblMsg.Text = "<h5>We Couldn't Find Your Account</h5> <br/>The username you entered does not belong to any account. Make sure that it is typed correctly.";
                    }
                    else
                    {
                       objForgotPassword.UserId = Convert.ToInt32(a);

                        ViewState["LoginId"] = objForgotPassword.GetLoginByUserId();
                        ViewState["UserID"] = a.ToString();
                        if (rdblReset.SelectedValue == "01")
                        {
                            lstrecord.Visible = true;
                            divVerify.Visible = false;

                            EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
                            objUserReg.UserId = Convert.ToInt32(ViewState["UserID"]);
                            objUserReg.GetUserVerifyDetails();
                            lblName.Text = objUserReg.FirstName;
                            ViewState["MobileNumber"] = objUserReg.MobilePhone.ToString();
                            lblMobile.Text = "+**********" + objUserReg.MobilePhone.Substring(8, 2);
                        }
                        else
                        {
                            ViewState["MobileNumber"] = txt_Login.Text;
                            if (GetVerificationCodeOnMobile() == true)
                            {
                               
                                if (rdblReset.SelectedValue == "01")
                                {
                                    Message("OTP Send At Your Mobile No.");
                                    lblloginmsg.Visible = false;
                                }
                                else
                                {
                                    Message("Login ID and OTP Send At Your Mobile No.");
                                    lblloginmsg.Visible = true;
                                    lstrecord.Visible = false;
                                    FieldsetCode.Visible = true;
                                    divVerify.Visible = false;
                                    txtcode.Text = "";
                                }
                            }
                            else
                            {
                                Message("Message not send.Please try again.!");

                            }
                        }
                    }

                }
            }
            else
            {
                GeneralClass.ShowMessageBox("Please arrange Alphabets in Order !");
            }

        }
    }
    protected void Cancelbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Default.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Default.aspx");
    }
    protected void lknResendCode_Click(object sender, EventArgs e)
    {
        if (ViewState["MobileNumber"] != null)
        {
            if (GetVerificationCodeOnMobile() == true)
            {
                Message("Confirmation text Sent Successfully.!");
                lknResendCode.Enabled = false;
                lknResendCode.Text = "Confirmation text sent at your mobile.";
            }
            else
            {
                Message("Sorry! Could not process your request.Please try again.!");
            }
        }
        else
        {
            Response.Redirect("EgForgotPassword.aspx");
        }
    }
    /// <summary>
    /// Check User  Verifiaction Code is Valid and Not if it's valid then he can change Password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnmobilecode_Click(object sender, EventArgs e)
    {
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = Convert.ToInt32(ViewState["UserID"]);
        objEgForgotPasswordBL.VerificationCode = txtcode.Text.Trim();
        string result = objEgForgotPasswordBL.VerifyCodeSMS();
        switch (result)
        {
            case "1":
                if (ViewState["idValue"].ToString().Trim() == "ForgetPassword")
                {
                    divchangePass.Visible = true;
                    divBtnReset.Visible = false;
                    tblPasswordDetails.Visible = true;
                }
                else
                {
                    divchangePass.Visible = false;
                    divResetAttempt.Visible = true;
                    tblPasswordDetails.Visible = false;
                }
                FieldsetCode.Visible = false;

                break;
            case "2":
                Message("Verification Code expires.Please Request again.!");
                lstrecord.Visible = true;
                FieldsetCode.Visible = false;
                break;
            case "0":
                Message("Invalid Verification Code.!");
                txtcode.Text = "";
                break;
        }

    }
    /// <summary>
    ///  Reset User  Password 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReSetPass_Click(object sender, EventArgs e)
    {
        EgChangePasswordBL objChangePassword = new EgChangePasswordBL();
        //EgForgotPasswordBL objForgotPassword = new EgForgotPasswordBL();
        if (txtNewPassword.Text != "" && txtCnfPassword.Text != "")
        {

            if (txtNewPassword.Text == txtCnfPassword.Text)
            {

                objChangePassword.Password = txtNewPassword.Text;
                //GeneralClass.Md5AddSecret(txtNewPassword.Text.Trim());
                objChangePassword.UserId = Convert.ToInt32(ViewState["UserID"]);
                string output = objChangePassword.AdminChangeUserPassword();

                if (output.Trim() == "2")
                {
                    Message("New Password is match with one of your previous 3 passwords!");
                }
                if (output.Trim() == "3")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
                }

            }
            else
            {
                Message("New password and confirm password should be same.!");
            }
        }
        else
        {
            Message("Password and Confirm Password can't be Blank.!");
        }

    }
    /// <summary>
    /// Call Mobile Service
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnverify_Click(object sender, EventArgs e)
    {
        if (ViewState["MobileNumber"] != null)
        {
            if (GetVerificationCodeOnMobile() == true)
            {
                Message("Message Send Successfully.!");
                lstrecord.Visible = false;
                FieldsetCode.Visible = true;
                txtcode.Text = "";
            }
            else
            {
                Message("Message not send.Please try again.!");

            }
        }
        else
        {
            Response.Redirect("EgForgotPassword.aspx");
        }
    }

    /// <summary>
    /// Check User Request Time is less than 24 hr then return old verification code 
    /// else delete old verif. code and generate new Verification code and Insert 
    /// </summary>
    public bool GetVerificationCodeOnMobile()
    {
        bool smsSend = false;
        string Vcode = "";
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = Convert.ToInt32(ViewState["UserID"]);

        string Str = txt_Login.Text.Trim();

        double Num;

        bool isNum = double.TryParse(Str, out Num);

        Vcode = ((rdblReset.SelectedValue == "01" || FieldsetCode.Visible == true) && !isNum) ? objEgForgotPasswordBL.VerifyCode().ToString().Trim() : objEgForgotPasswordBL.GetLoginId().ToString().Trim();
      
        string mobileNo = rdblReset.SelectedValue == "01" ? ViewState["MobileNumber"].ToString() : txt_Login.Text.Trim();
        if ((mobileNo != "" || mobileNo != null) && (Vcode.Trim() != "" || Vcode != null))
        {
          smsSend = send("egrasraj.otp", "T3%23uB7%24xD0", Vcode.Trim(), "91" + mobileNo.Trim(), "EGRASJ", "1001524671154484790" , "1007056277014110427");
         }
         else
         smsSend= false;
           
        
        return smsSend;
    }
    
    /// <summary>
    /// Call SMS Utility for send code on mobile number 
    /// </summary>
    /// <param name="uid">User Name </param>
    /// <param name="password">Password</param>
    /// <param name="message">VerificationCode</param>
    /// <param name="no">MobileNumber</param>
    /// <param name="SENDERID">Unique User SEnderID</param>
    public bool send(string uid, string password, string message, string no, string SENDERID , string dlt_entity, string dlt_tempate_id)
    {
       // EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
       // SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
       // RemoteClass myremotepost = new RemoteClass();
       // string plainText = string.Format("Vcode={0}|Mobile={1}", message, no);
       // string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
       //// string CheckSum = ObjEncrcryptDecrypt.GetSHA256(plainText);
       // plainText = plainText + "|checkSum=" + CheckSum;

       // string cipherText = objEncry.Encrypt(plainText, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));
       // //SMSservice.Service SMSservice = new SMSservice.Service();
       // SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();

        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();

       // //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
       // string responseString = SMSservice.GetSMSDetails(cipherText);
        HttpWebRequest myReq =
            (HttpWebRequest)WebRequest.Create("http://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + "Egras Password Reset Code:loginID:" + ViewState["LoginId"].ToString() + " and OTP:" + message + "&mnumber=" + no + "&signature=" + SENDERID + "&dlt_entity_id=" + dlt_entity+ "&dlt_template_id=" + dlt_tempate_id);


     

        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        string responseString = respStreamReader.ReadToEnd();// "Message Accepted for Request ID=12313946898506681731361~code=API000 & info=Platform accepted & Time =2014/03/13/11/20";//
        respStreamReader.Close();
        myResp.Close();
        if (responseString.ToString().Contains("API000") == true)
        {
            objEgForgotPasswordBL.SMSCount();
            return true;
        }
        else
            return false;
        //return responseString.ToString();
    }
    //public string send1(string uid, string password, string message, string no, string SENDERID)
    //{
    //    EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
    //    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
    //    RemoteClass myremotepost = new RemoteClass();
    //    string plainText = string.Format("Vcode={0}|Mobile={1}", message, no);
    //    string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
    //    plainText = plainText + "|checkSum=" + CheckSum;

    //    string cipherText = objEncry.Encrypt(plainText, System.Web.HttpContext.Current.Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));

    //    return cipherText;
    //}
    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
    }

    protected void btnResetAttempt_Click(object sender, EventArgs e)
    {
        EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
        if (txt_Login.Text != "")
        {
            objEgChangePasswordBL.LoginID = txt_Login.Text;
            int returnvalue = objEgChangePasswordBL.UpdateLoginAttempt();
            if (returnvalue == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "MessageThenRedirect", "alert('Login Attempt Reset Successfully');window.location='../../default.aspx';", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg",
                 "alert('Login attempt reset failed.');", true);
            }

        }
    }
    protected void rdblReset_click(object sender, EventArgs e)
    {
        if (rdblReset.SelectedItem.Value == "01")
        {
            lblLogin.Text = "Login ID/Mobile No ";

        }
        else
        {
            if ((txt_Login.Text).Length > 10)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg",
                                 "alert('Mobile Number Must Have 10 Digits.');", true);
            }
            lblLogin.Text = "Mobile No ";
        }
        lblMsg.Visible = false;
        txt_Login.Text = "";
    }
}
public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
    public TrustAllCertificatePolicy() { }
    public bool CheckValidationResult(ServicePoint sp,
        X509Certificate cert,
        WebRequest req,
        int problem)
    {
        return true;
    }
}
