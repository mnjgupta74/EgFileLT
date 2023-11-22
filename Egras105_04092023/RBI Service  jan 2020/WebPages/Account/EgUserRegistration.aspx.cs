using System;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Net;

public partial class WebPages_EgUserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string pwd = txtpassward.Text;
        txtpassward.Attributes.Add("value", pwd);
        string Repwd = txtrepassward.Text;
        txtrepassward.Attributes.Add("value", Repwd);
        string ans = txtsecAnswer.Text;
        txtsecAnswer.Attributes.Add("value", ans);
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["a"] == "1")
            {
                MpeOTP.Show();

            }
            EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
            Page.SetFocus(txtlogin);
            objUserReg.QuestionId = RandomNumber(1, 20);
            //string[] QuestionAndAnswer = objUserReg.GetQuestionAndAnswer().Split('_');
            //if (QuestionAndAnswer[0].ToString() != "" && QuestionAndAnswer[0] != null)
            //{
            //    ViewState["Answer"] = QuestionAndAnswer[1].ToString();
            //}

            //lblpass.Text = QuestionAndAnswer[0].ToString();
            //ViewState["Answer"] = QuestionAndAnswer[1].ToString();
            objUserReg.GetSecurityQuestion(ddlsecQuestion);
            objUserReg.FillStateList(ddlstate);
            //  objUserReg.FillDistrictList(ddlcity);
        }
    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        //{
        //    string imageValue = inpHide.Text.Trim();
        //    if (HttpContext.Current.Session["captcha"] == null || HttpContext.Current.Session["captcha"].ToString() == "")
        //    {
        //        return;
        //    }
        //    if (HttpContext.Current.Session["captcha"].ToString().ToLower() != "" && imageValue != "" && imageValue != "0")
        //    {
        //        if (HttpContext.Current.Session["captcha"].ToString().Trim() == imageValue.ToString().Trim())
        //        {


        try
        {
            Convert.ToDateTime(txtBirthDate.Text.Trim().Split('/').GetValue(2) + "/" + txtBirthDate.Text.Trim().Split('/').GetValue(1) + "/" + txtBirthDate.Text.Trim().Split('/').GetValue(0));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:alert('Invalid Date of Birth');", true);
            return;
        }

        //Font defaultFont = SystemFonts.DefaultFont;
        //txtmobile.Font = FontStyle.Bold;
        //txtmobile.Font
        string a = txtmobile.Text.Trim();
        lblmsgdis.Text = "Verification Code Will be Sent on Mobile no <b>" + a + "</b>. Are You Sure to Continue With This Number ?? ";
        ModalPopupExtender1.Show();
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Call SMS Utility for send code on mobile number 
    /// </summary>
    /// <param name="uid">User Name </param>
    /// <param name="password">Password</param>
    /// <param name="message">VerificationCode</param>
    /// <param name="no">MobileNumber</param>
    /// <param name="SENDERID">Unique User SEnderID</param>
    public bool send(string uid, string password, string message, string no, string SENDERID)
    {
        string dlt_entity = "1001524671154484790";
        string dlt_tempate_id = "1007056277014110427";
        //EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //RemoteClass myremotepost = new RemoteClass();
        //string plainText = string.Format("Vcode={0}|Mobile={1}", message, no);
        //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //plainText = plainText + "|checkSum=" + CheckSum;

        //string cipherText = objEncry.Encrypt(plainText, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));
        ////SMSservice.Service SMSservice = new SMSservice.Service();
        //SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();

        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();

        ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        //string responseString = SMSservice.GetSMSDetails(cipherText);
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; // added 16/08/2018 -- For Certification expired problem
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        HttpWebRequest myReq =
            (HttpWebRequest)WebRequest.Create("http://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + "Egras Password Reset Code:loginID:" + "***" + " and OTP:" + message + "&mnumber=" + no + "&signature=" + SENDERID + "&dlt_entity_id=" + dlt_entity + "&dlt_template_id=" + dlt_tempate_id);
        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        string responseString = string.Empty;
        using (System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream()))
        {
            responseString = respStreamReader.ReadToEnd();// "Message Accepted for Request ID=12313946898506681731361~code=API000 & info=Platform accepted & Time =2014/03/13/11/20";//
            respStreamReader.Close();
        }
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


    public void ChechExistingLogin(object sender, EventArgs e)
    {
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        if (txtlogin.Text != "")
        {
            objUserReg.LoginID = txtlogin.Text;
            int user = objUserReg.CheckExistingLogin();
            if (user != 0)
            {

                Image1.Visible = true;
                Image1.ImageUrl = "~/Image/delete.png";
            }
            else
            {
                Image1.Visible = true;
                Image1.ImageUrl = "~/Image/success.png";
            }
        }
        else
        {
            lblMsg.Text = "Enter Login Id";
        }
    }
    public void clearControl()
    {
        txtlogin.Text = "";
        txtfirst.Text = "";
        txtlast.Text = "";
        txtTIN.Text = "";
        txtaddress.Text = "";
        txtCity.Text = "";
        ddlstate.SelectedValue = "0";
        ddlcountry.SelectedValue = "0";
        txtBirthDate.Text = "";
        txtpincode.Text = "";
        txtEmailId.Text = "";
        txtmobile.Text = "";
        lblMsg.Text = "";
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        objUserReg.UserId = Convert.ToInt32(hdnuserid.Value);
        objUserReg.OTP = txtOTP.Text;
        string res = objUserReg.VerifyMobile();
        txtpassward.Attributes.Remove("value");
        txtsecAnswer.Attributes.Remove("value");
        txtrepassward.Attributes.Remove("value");
        if (res == "1")
        {
            MpeOTP.Hide();

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
        }
        else
        {
            MpeOTP.Show();
            lblermsg.Text = "Invalid OTP.";
        }
    }


    protected void lnkresendcode_Click(object sender, EventArgs e)
    {
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        EgForgotPasswordBL objforgotbl = new EgForgotPasswordBL();
        if (txtmob.Text.Trim() != "" && txtmob.Text.Trim() != null)
        {
            objforgotbl.UserId = Convert.ToInt32(hdnuserid.Value);

            string S = objforgotbl.VerifyCode();
            if (S != "")
            {
                if (S != "0")
                {
                    send("egrasraj.otp", "T3%23uB7%24xD0", S.Trim(), "91" + txtmob.Text.Trim().ToString(), "EGRASJ");
                    MpeOTP.Show();
                    txtmob.Enabled = false;
                }
                else
                {
                    MpeOTP.Show();
                    lblermsg.Text = "OTP could not be generated.";
                }
            }
        }
        else
        {
            MpeOTP.Show();
            lblermsg.Text = "Enter Mobile No.";
        }
    }
    protected void Btnconfirm_Click(object sender, EventArgs e)
    {
        EgForgotPasswordBL objforgotbl = new EgForgotPasswordBL();
        if (Page.IsValid)
        {
            string imageValue = inpHide.Text.Trim();
            if (HttpContext.Current.Session["captcha"] == null || HttpContext.Current.Session["captcha"].ToString() == "")
            {
                return;
            }
            if (HttpContext.Current.Session["captcha"].ToString().ToLower() != "" && imageValue != "" && imageValue != "0")
            {
                if (HttpContext.Current.Session["captcha"].ToString().Trim() == imageValue.ToString().Trim())
                {
                    ModalPopupExtender1.Hide();
                    if (txtlogin.Text != "")
                    {
                        //if (ViewState["Answer"].ToString() == txtpass.Text)
                        //{
                        if (txtpassward.Text == txtrepassward.Text)
                        {
                            if (txtfirst.Text != "" && txtlast.Text != "")
                            {
                                EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
                                objUserReg.Type = 1;

                                objUserReg.LoginID = txtlogin.Text;
                                int user = objUserReg.CheckExistingLogin();

                                if (user != 0)
                                {
                                    txtpassward.Attributes.Remove("value");
                                    txtrepassward.Attributes.Remove("value");
                                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Login Id Exist.!')", true);
                                }
                                else
                                {
                                    objUserReg.FirstName = txtfirst.Text;
                                    objUserReg.LastName = txtlast.Text;

                                    string[] fromdate = txtBirthDate.Text.Trim().Replace("-", "/").Split('/');
                                    objUserReg.DOB = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

                                    objUserReg.UserType = "10";
                                    objUserReg.Address = txtaddress.Text;
                                    objUserReg.City = txtCity.Text.Trim();
                                    objUserReg.State = int.Parse(ddlstate.SelectedValue);
                                    objUserReg.Country = int.Parse(ddlcountry.SelectedValue);
                                    objUserReg.Identity = txtTIN.Text;
                                    objUserReg.Password = txtpassward.Text;
                                    objUserReg.VerificationCode = null;
                                    objUserReg.AttemptNumber = Convert.ToString(0);

                                    if (txtmobile.Text.CompareTo("") == 0)
                                    {
                                        objUserReg.MobilePhone = "";
                                    }
                                    else
                                    {
                                        objUserReg.MobilePhone = txtmobile.Text.Trim();
                                    }

                                    objUserReg.PinCode = txtpincode.Text;
                                    objUserReg.Email = txtEmailId.Text;
                                    objUserReg.QuestionId = Convert.ToInt32(ddlsecQuestion.SelectedValue);
                                    objUserReg.Question = txtsecAnswer.Text;
                                    string S = objUserReg.InsertUserData1();
                                    string a = txtpassward.Text;
                                    txtpassward.Attributes.Remove("value");
                                    txtsecAnswer.Attributes.Remove("value");
                                    txtrepassward.Attributes.Remove("value");
                                    //txtpass.Text = "";

                                    if (S != "")
                                    {
                                        if (S != "0")
                                        {
                                            hdnuserid.Value = objUserReg.UserId.ToString();
                                            //objforgotbl.UserId= objUserReg.UserId;
                                            //ViewState["LoginId"] = objforgotbl.GetLoginByUserId();
                                            //bool smsSend = false;

                                            //AndroidAppBL androidAppBL = new AndroidAppBL();
                                            //androidAppBL.MobileNo = txtmobile.Text.Trim();

                                            //if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null) && (S.Trim() != "" && S.Trim() != null))
                                            //{
                                            //    string cipherText = androidAppBL.send("egras.auth", "Jh*$23et", S.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ");
                                            //    //EgTrgService SMSservice = new EgTrgService();
                                            //    SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
                                            //    string responseString = SMSservice.GetSMSDetails(cipherText);
                                            //    if (responseString != "" && responseString != null)
                                            //        smsSend = true;
                                            //}
                                            //else
                                            //    smsSend = false;
                                            send("egrasraj.otp", "T3%23uB7%24xD0", S.Trim(), "91" + txtmobile.Text.Trim().ToString(), "EGRASJ");

                                            MpeOTP.Show();
                                            txtmob.Enabled = false;
                                            txtmob.Text = txtmobile.Text;
                                            int Uid = objUserReg.GetUserId();
                                            Session["UserId"] = Uid.ToString();
                                            Session["userName"] = txtlogin.Text;

                                            Session["UserType"] = "10";
                                            Random randomclass = new Random();

                                            Session.Add("Rnd1", randomclass.Next().ToString());
                                            Session["RND"] = Session["RND1"].ToString();
                                            HttpCookie appcookie = new HttpCookie("Appcookie");
                                            appcookie.HttpOnly = true;
                                            appcookie.Domain = HttpContext.Current.Request.Url.Host;
                                            appcookie.Value = Session["RND"].ToString();
                                            appcookie.Expires = DateTime.Now.AddDays(1);

                                            Response.Cookies.Add(appcookie);

                                            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);

                                            clearControl();
                                        }
                                        else
                                        { //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Mobile no already exist.')", true);
                                            lblerrormsg.Text = "MobileNo. Already Associated With Another User.";
                                            //txtpass.Text = "";
                                            txtpassward.Attributes.Remove("value");
                                            txtsecAnswer.Attributes.Remove("value");
                                            txtrepassward.Attributes.Remove("value");
                                            MpeOTP.Hide();
                                        }


                                    }

                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Data not inserted.')", true);

                                    }

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('FirstName And LastName is Not Valid')", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('password and confirm password not match.')", true);
                        }
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Please give correct answer.!')", true);
                        //}
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('LoginID can not be Blank.!')", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('InValid Captcha Code.')", true);
                }
            }
        }
    }
}