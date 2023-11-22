using System;
using System.Web.UI;
using EgBL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Security.Cryptography.X509Certificates;
public partial class WebPages_EgEditUserProfile : System.Web.UI.Page
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
        //Session["UserID"] = 988;
        //Session["UserType"] = 4;
        //if ((Session["UserID"] == null  && Session["DeActive"] == null) || (Session["UserID"].ToString() == "" && Session["DeActive"].ToString()==""))
        if ((Session["UserID"] == null && Session["DeActive"] == null))
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {


            EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
            //objUserReg.FillDistrictList(ddlcity);
            objUserReg.GetSecurityQuestion(ddlsecQuestion);
            objUserReg.QuestionId = RandomNumber(1, 20);
            string[] QuestionAndAnswer = objUserReg.GetQuestionAndAnswer().Split('_');
            lblpass.Text = QuestionAndAnswer[0].ToString();
            if (QuestionAndAnswer[0].ToString() != "" && QuestionAndAnswer[0] != null)
            {
                ViewState["Answer"] = QuestionAndAnswer[1].ToString();
            }

            FillEditForm();
            objUserReg.FillStateList(ddlstate);


        }
    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public void btnUpdate_Click(object sender, EventArgs e)
    {


        if (ViewState["Answer"].ToString() == txtpass.Text)
        {



            if (txtfirst.Text != "" && txtlast.Text != "")
            {
                if (ViewState["MobNo"] == null || ViewState["MobNo"].ToString() != txtmobile.Text.Trim())
                {
                    //EgLoginBL objLogin = new EgLoginBL();
                    EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
                    objUserReg.MobilePhone = txtmobile.Text.Trim();
                    if (Session["UserID"] == null || Session["UserID"].ToString() == "")
                    {
                        objUserReg.UserId = Convert.ToInt32(Session["DeActive"]);
                    }
                    else
                    {
                        objUserReg.UserId = Convert.ToInt32(Session["UserID"]);
                    }
                    objUserReg.UserType = Session["UserType"].ToString();
                    string S1 = objUserReg.ExistMobileNo();
                    if (S1 != "")
                    {
                        if (S1 != "0")
                        {
                            //bool smsSend = false;

                            //AndroidAppBL androidAppBL = new AndroidAppBL();
                            //androidAppBL.MobileNo = txtmobile.Text.Trim();

                            //if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null))
                            //{
                            //    string cipherText = androidAppBL.send("egras.auth", "Jh*$23et", S1.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ");
                            //    //EgTrgService SMSservice = new EgTrgService();
                            //    SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
                            //    string responseString = SMSservice.GetSMSDetails(cipherText);
                            //    if (responseString != "" && responseString != null)
                            //        smsSend = true;
                            //}
                            //else
                            //    smsSend = false;
                            //send("egras.auth", "T3%23uB7%24xD0", S1.Trim(), "91" + txtmobile.Text.Trim().ToString(), "EGRASJ");
                            send("egrasraj.otp", "T3%23uB7%24xD0", S1.Trim(), "91" + txtmobile.Text.Trim().ToString(), "EGRASJ");

                            //if (smsSend)
                            //{
                            MpeOTP.Show();
                            lblermsg.Text = "";
                            txtmob.Enabled = false;
                            txtmob.Text = txtmobile.Text;
                            //}
                            //else
                            //{
                            //     ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Otp could Not be Send')", true);

                            //    return;
                            //}

                        }
                        else
                        {
                            // MpeOTP.Show();
                            lblerrormsg.Text = "Mobile no already exist.";

                            //lblermsg.Text = "OTP could not be generated.";
                        }
                    }
                }
                else
                if (ViewState["MobNo"].ToString() == txtmobile.Text.Trim())
                {
                    EditProfile();
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('FirstName And LastName is Not Valid')", true);
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Please Give your Answer')", true);
        }

    }
    public void FillEditForm()
    {
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();

        if (Session["DeActive"] == null)
            objUserReg.UserId = Convert.ToInt32(Session["userid"]);
        else
            objUserReg.UserId = Convert.ToInt32(Session["DeActive"]);

        objUserReg.UserType = Session["UserType"].ToString();
        objUserReg.EditData();
        txtfirst.Text = objUserReg.FirstName;
        txtlast.Text = objUserReg.LastName;
        ddlusertype.SelectedValue = objUserReg.UserType;
        ddlusertype.Enabled = false;

        txtBirthDate.Text = objUserReg.DateOfbirth;

        txtEmailId.Text = objUserReg.Email;
        txtaddress.Text = objUserReg.Address;
        txtCity.Text = objUserReg.City.Trim();
        ddlstate.SelectedValue = Convert.ToString(objUserReg.State);
        ddlcountry.SelectedValue = Convert.ToString(objUserReg.Country);
        ddlsecQuestion.SelectedValue = Convert.ToString(objUserReg.QuestionId);
        txtsecAnswer.Text = objUserReg.Question;
        txtmobile.Text = objUserReg.MobilePhone;

        if (Session["MobileUpdate"] != null)
        {
            if (Session["MobileUpdate"].ToString() == "1")
            {
                ViewState["MobNo"] = null;
                txtmobile.Text = "";
                Session["MobileUpdate"] = null;
                hpSignin.Visible = true;
            }
            else if (Session["MobileUpdate"].ToString() == "2")
            {
                MpeOTP.Show();
                hpSignin.Visible = true;
                lblermsg.Text = "";
                if (Session["DeActive"] != null && Session["DeActive"] != "")
                {
                    // Session["userid"] = Session["DeActive"];
                    txtmob.Enabled = false;
                    txtmob.Text = objUserReg.MobilePhone.Trim();
                }
            }
        }
        else
        {
            ViewState["MobNo"] = objUserReg.MobilePhone.Trim();
        }
        txtpincode.Text = objUserReg.PinCode;
        txtTIN.Text = objUserReg.Identity;
        if (objUserReg.UserType == "6" || objUserReg.UserType == "4")
        {
            txtTIN.Text = "";
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        objUserReg.MobilePhone = txtmob.Text;
        if (Session["DeActive"] != null)
        {
            objUserReg.UserId = Convert.ToInt32(Session["DeActive"].ToString());
        }
        else
            objUserReg.UserId = Convert.ToInt32(Session["userid"].ToString());

        objUserReg.OTP = txtOTP.Text;
        string res = objUserReg.VerifyMobile();
        if (res == "1")
        {
            MpeOTP.Hide();
            lblermsg.Text = "";
            Session["MobileUpdate"] = null;
            EditProfile();
            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
        }
        else if (res == "2")
        {
            MpeOTP.Show();
            lblermsg.Text = "OTP Expired.";
        }
        else if (res == "3")
        {
            MpeOTP.Show();
            lblermsg.Text = "Mobile no already associated with another user.";
            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Fail();", true);
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

        if (txtmob.Text.Trim() != "" && txtmob.Text.Trim() != null)
        {
            objUserReg.MobilePhone = txtmob.Text;
            if (Session["DeActive"] == null)
                objUserReg.UserId = Convert.ToInt32(Session["userid"]);
            else
                objUserReg.UserId = Convert.ToInt32(Session["DeActive"]);
            //objUserReg.UserId = Convert.ToInt32(Session["UserID"].ToString());
            objUserReg.UserType = Session["UserType"].ToString();
            string S = objUserReg.ExistMobileNo();
            txtpass.Text = "";
            txtsecAnswer.Text = "";
            if (S != "")
            {
                if (S != "0")
                {
                    //bool smsSend = false;

                    //AndroidAppBL androidAppBL = new AndroidAppBL();
                    //androidAppBL.MobileNo = txtmob.Text.Trim();

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
                    send("egrasraj.otp", "T3%23uB7%24xD0", S.Trim(), "91" + txtmob.Text.Trim().ToString(), "EGRASJ");
                    // if (smsSend)
                    //{
                    MpeOTP.Show();
                    txtmob.Enabled = false;
                    lblermsg.Text = "";
                    //txtmob.Text = txtmobile.Text;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Otp could Not be Send')", true);

                    //    return;
                    //    //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "Otp Could Not Be Send", true);

                    //}
                }
                else
                {
                    MpeOTP.Show();
                    lblermsg.Text = "This mobile no already associated with another user";
                }
            }
        }
        else
        {
            MpeOTP.Show();
            lblermsg.Text = "Enter Mobile No.";
        }
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
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        //string responseString = SMSservice.GetSMSDetails(cipherText);
        HttpWebRequest myReq =
            //(HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + "Egras Password Reset Code:" + message + "&mnumber=" + no + "&signature=" + SENDERID + "&dlt_entity=" + dlt_entity + "&dlt_tempate_id=" + dlt_tempate_id);
            (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + "Egras Password Reset Code:loginID:"+ "***" + " and OTP:" + message + "&mnumber=" + no + "&signature=" + SENDERID + "&dlt_entity_id=" + dlt_entity + "&dlt_template_id=" + dlt_tempate_id);

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
    public void EditProfile()
    {
        int S;
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        EgLoginBL objLogin = new EgLoginBL();
        objUserReg.FirstName = txtfirst.Text;
        objUserReg.LastName = txtlast.Text;

        string[] fromdate = txtBirthDate.Text.Trim().Replace("-", "/").Split('/');
        objUserReg.DOB = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

        objUserReg.Email = txtEmailId.Text;
        objUserReg.Address = txtaddress.Text;
        objUserReg.Country = int.Parse(ddlcountry.SelectedValue);
        objUserReg.State = int.Parse(ddlstate.SelectedValue);
        objUserReg.City = txtCity.Text.Trim();
        if (txtmobile.Text.CompareTo("") == 0)
        {
            objUserReg.MobilePhone = "";
        }
        else
        {
            objUserReg.MobilePhone = txtmobile.Text.Trim();
        }
        objUserReg.Identity = txtTIN.Text;
        objUserReg.PinCode = txtpincode.Text;
        objLogin.UserId = Convert.ToInt32(Session["DeActive"]);
        objLogin.SessionActive(2);
        objUserReg.UserId = Convert.ToInt32(Session["UserID"]);
        objUserReg.UserType = Session["UserType"].ToString();  //new Add
        objUserReg.QuestionId = Convert.ToInt32(ddlsecQuestion.SelectedValue);
        objUserReg.Question = txtsecAnswer.Text;
        S = objUserReg.UpdateUserData();
        if (S == 1)
        {
            if (Session["UserType"].ToString() == "3" || Session["UserType"].ToString() == "4")
            {

                Page page = System.Web.HttpContext.Current.Handler as Page;
                string pageName = "EgDepartment";
                ScriptManager.RegisterStartupScript(page, page.GetType(),
                        "MessageThenRedirect", "alert('" + "Profile Updated successfully" +
                        "');window.location='" + pageName + ".aspx';", true);
            }
            else
            {
                FillEditForm();
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.Page.GetType(),
             "MessageThenRedirect", "alert('Profile Successfully Updated.');window.location='../default.aspx';", true);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Profile Successfully Updated.')", true);
                //Response.Redirect("~\\logout.aspx");

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Profile not Updated.')", true);
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

}
