using System;
using System.Collections.Generic;
using System.Linq;
using EgBL;
using System.Xml.Linq;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class EgTestBank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tblTestBank.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cipherText = String.Empty;
        string GRN = txtGRN.Text;
        string BankCode = txtBankCode.Text;
        string BankReferenceNo = txtBankReferenceNo.Text;
        string CIN = txtCIN.Text;
        string PaidDate = txtPaidDate.Text;
        string PaidAmt = txtPaidAmt.Text;
        string TransStatus = txtTransStatus.Text;


        if (BankCode== "9930001")
        {
            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6},DebitBankCode={7},BankRefNo={8},PayMode={9},Reason={10}",
                                    GRN.ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, PaidAmt.ToString(), txtTransStatus.Text.ToString(), txtdebtbankcode.Text.ToString(), txtBakRefCode.Text.ToString(), txtPaymode.Text.ToString(), txtReason.Text.ToString());


            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            string cipherText1 = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);

            // string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx"; //ConfigurationManager.AppSettings["SuccessURL"].ToString();// 
            myremotepost.Add("encdata", cipherText1); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "9930001");
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();


        }
       else  if (BankCode == "0006326")
        {
            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                                 GRN.ToString(), "6390013", "ICICI12345", "036005714654654000010", DateTime.Now, PaidAmt.ToString(), txtTransStatus.Text.ToString());


            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetSHA256(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            string cipherText1 = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);

            // string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// 
            myremotepost.Add("encdata", cipherText1); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "0006326");
          
            myremotepost.Url = Address;
            myremotepost.Post();


        }


        else
        {





            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                             GRN, BankCode, BankReferenceNo, CIN, PaidDate, PaidAmt, TransStatus);
            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;
            Dictionary<string, string> bankCodeList = new Dictionary<string, string>();
            XDocument ooo = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/Webpages/BankNames_BSRCode.xml"));
            foreach (XElement rootElement in ooo.Descendants().Where(p => p.HasElements == false))
            {
                string value = rootElement.Value.ToString().Trim();
                String KName = value.Split('|').GetValue(0).ToString();
                string Kvalue = value.Split('|').GetValue(1).ToString();
                bankCodeList.Add(KName, Kvalue);
            }

            if (BankCode == "1000132")
            {
                ePayEncryptionDecryptionBL objenc = new ePayEncryptionDecryptionBL();
                cipherText = objenc.Encrypt(plainText, "BwmHPemeQsQhpwEGWmyQtQ==", 128);
            }
            else
            {
                cipherText = objEncry.Encrypt(plainText, Server.MapPath("Key/" + bankCodeList[BankCode].Trim() + ".key"));
            }
            //string Address = "http://localhost:1948/Server%20-%20Server/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString(); //"https://egras.raj.nic.in/WebPages/BankResponseReceived.aspx";
            myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", BankCode);
            myremotepost.Url = Address;
            myremotepost.Post();

        }
    }
    protected void MobileVerification_TextChanged(object sender, EventArgs e)
    {
        if (VerifyNumericVal(txtMobile.Text.Trim()))
        {
            string str = string.Empty;
            ViewState["UserID"] = "52";
            ViewState["MobileNumber"] = txtMobile.Text.Trim();
            EgMobileAuthentication objEgMobileVerification = new EgMobileAuthentication();
            objEgMobileVerification.mobileno = txtMobile.Text.Trim();
            objEgMobileVerification.usreid = "52";
            string res = objEgMobileVerification.MobileNumberVerify();
            if (res == "1" && GetVerificationCodeOnMobile() == true)
            {
                lblMsgMobileVerify.Visible = true;
                lblMsgOtp.Visible = false;
                lblMsgMobileVerify.Text = "Message Send Successfully.!";
            }
            else
            {
                lblMsgMobileVerify.Visible = true;
                lblMsgOtp.Visible = false;
                lblMsgMobileVerify.Text = "Mobile No is Not Authenticated.!";
            }
        }
        else
        {
            lblMsgMobileVerify.Visible = true;
            lblMsgOtp.Visible = false;
            lblMsgMobileVerify.Text = "Wrong Mobile No !";
        }
        //return str;
    }
    protected void OTPVerification_TextChanged(object sender, EventArgs e)
    {
        string str = string.Empty;
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = Convert.ToInt32("52");
        objEgForgotPasswordBL.VerificationCode = txtOtp.Text.Trim();
        string result = objEgForgotPasswordBL.VerifyCodeSMS();
        switch (result)
        {
            case "1":
                tblTestBank.Visible = true;
                divVerify.Visible = false;
                break;
            case "2":
                lblMsgMobileVerify.Visible = true;
                lblMsgOtp.Visible = true;
                lblMsgOtp.Text = "Verification Code expires.Please Request again.!";
                break;
            case "0":
                lblMsgMobileVerify.Visible = true;
                lblMsgOtp.Visible = true;
                lblMsgOtp.Text = "Invalid Verification Code.!";
                break;
        }
        //return str;
    }

    public bool GetVerificationCodeOnMobile()
    {
        string Vcode = "";
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = Convert.ToInt32(ViewState["UserID"]);
        Vcode = objEgForgotPasswordBL.VerifyCode().ToString().Trim();

        if ((ViewState["MobileNumber"].ToString() != "" || ViewState["MobileNumber"] != null) && (Vcode != "" || Vcode != null))
        {
            return send("egras.auth", "Jh*$23et", Vcode.Trim(), "91" + ViewState["MobileNumber"].ToString(), "EGRASJ");

        }
        else
            return false;
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
        EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        RemoteClass myremotepost = new RemoteClass();
        string plainText = string.Format("Vcode={0}|Mobile={1}", message, no);
        string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //string CheckSum = ObjEncrcryptDecrypt.GetSHA256(plainText);
        plainText = plainText + "|checkSum=" + CheckSum;

        string cipherText = objEncry.Encrypt(plainText, Server.MapPath("Key/RAJASTHAN_EGRASS.key"));
        //SMSservice.Service SMSservice = new SMSservice.Service();
        SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();

        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();

        //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
        string responseString = SMSservice.GetSMSDetails(cipherText);
        //HttpWebRequest myReq =
        //    (HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + "Egras Password Reset Code:" + message + "&mnumber=" + no + "&signature=" + SENDERID);

        //HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
        //System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
        //string responseString = respStreamReader.ReadToEnd();// "Message Accepted for Request ID=12313946898506681731361~code=API000 & info=Platform accepted & Time =2014/03/13/11/20";//
        //respStreamReader.Close();
        //myResp.Close();
        if (responseString.ToString().Contains("API000") == true)
        {
            objEgForgotPasswordBL.SMSCount();
            return true;
        }
        else
            return false;
        //return responseString.ToString();
    }
    public bool VerifyNumericVal(string number)
    {
        string expresion;
        expresion = "^[\\d]{10,10}";
        if (Regex.IsMatch(number, expresion))
        {
            if (Regex.Replace(number, expresion, string.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}