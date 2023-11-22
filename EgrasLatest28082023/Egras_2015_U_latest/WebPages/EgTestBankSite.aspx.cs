using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EgBL;
using System.Configuration;

public partial class WebPages_EgTestBankSite : System.Web.UI.Page
{
    int GRN;
    double Amount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["AbC"] = Request.Form["encdata"].ToString();
            //Session["merchant"] = Request.Form["merchant_code"].ToString();

            Session["AbC"] = Request.Form["EncryptTrans"].ToString();   // epay Neft
            Session["merchant"] = Request.Form["merchIdVal"].ToString();
            //Session["EncryptpaymentDetails"] = Request.Form["EncryptpaymentDetails"].ToString();
        }

    }
    public void SetValue(string status)
    {
        // Test Bank set with OBC Vabj
        string strReqq = Session["AbC"].ToString();
       // string strReqq1 = Session["EncryptpaymentDetails"].ToString();
        SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
        BanksEncryptionDecryption obj = new BanksEncryptionDecryption();
        string plaintext;
        if (Session["merchant"].ToString() == "CANRAJGRAS")
        {
            plaintext = ObjEncrytDecrypt.Decrypt(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "CANARA.key");
            List<string> GRNvalue = plaintext.Split('|').ToList();
            if (GRNvalue != null && GRNvalue.Count > 0)
            {
                string[] aa = GRNvalue[0].ToString().Split('=');
                GRN = Convert.ToInt32(aa[1].ToString());
                string[] ab = GRNvalue[20].ToString().Split('=');
                Amount = Convert.ToDouble(ab[1].ToString());
            }
        }
      else  if (Session["merchant"].ToString() == "RAJASTHAN_EGRAS")

        {
            plaintext = ObjEncrytDecrypt.DecryptAES256(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);
            List<string> GRNvalue = plaintext.Split('|').ToList();
            if (GRNvalue != null && GRNvalue.Count > 0)
            {
                string[] aa = GRNvalue[0].ToString().Split('=');
                GRN = Convert.ToInt32(aa[1].ToString());
                string[] ab = GRNvalue[4].ToString().Split('=');
                Amount = Convert.ToDouble(ab[1].ToString());
            }

        }
        else if (Session["merchant"].ToString() == "1000132")

        {
            string version = null;
            string KeyName = "BwmHPemeQsQhpwEGWmyQtQ==";
            string str = strReqq;
            plaintext = DecryptString(str, KeyName, version);            
            List<string> GRNvalue = plaintext.Split('|').ToList();
            if (GRNvalue != null && GRNvalue.Count > 0)
            {
                //string[] aa = GRNvalue[0].ToString().Split('=');
                GRN = 34837493;//Convert.ToInt32(GRNvalue[0].ToString());
                //string[] ab = GRNvalue[4].ToString().Split('=');
                Amount = 1.00;//Convert.ToDouble(GRNvalue[3]);
            }

        }
        else
        {
            plaintext = ObjEncrytDecrypt.DecryptAES256(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key", null);
            List<string> GRNvalue = plaintext.Split('|').ToList();
            if (GRNvalue != null && GRNvalue.Count > 0)
            {
                string[] aa = GRNvalue[12].ToString().Split('=');
                GRN = Convert.ToInt32(aa[1].ToString());
                string[] ab = GRNvalue[9].ToString().Split('=');
                Amount = Convert.ToDouble(ab[1].ToString());
            }
        }


       
       
        







        if (Session["merchant"].ToString() == "CANRAJGRAS")
        { // CANARA
            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                         GRN.ToString(), "0240539", "Canara12345", "024053914654654000010", DateTime.Now, Amount.ToString(), status);


            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            string cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "CANARA.key");

            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
            myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "0240539");
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();
        }
        else if(Session["merchant"].ToString() == "RAJASTHAN_EGRASS")
        {

            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                                 GRN.ToString(), "0006326", "ICICI12345", "036005714654654000010", DateTime.Now, Amount.ToString(), status);

            //BANK_CODE = 6390013 | PAID_AMT = 1.00 | GRN = 55504032022003 | CIN = 639001300000604032022 | PAID_DATE = 2022 / 03 / 04 13:53:32 | BankReferenceNo = 159140573 | TRANS_STATUS = S | checksum = 60ea92939307dd58f4cf59a5daf5b4cc33b7111a6088a726176dab32ea3e4a7b���
            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetSHA256(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            //string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key", null);
            string cipherText = objEncry.EncryptAES256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key", null);
            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
           string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
         //   "WRf0fFhozOQ0NIGNIUerIGI0oKEkhL0ySy0RHZvGA6IMZuApnOVa/w/XlizOTmEr3s4bNectB1ucqu++kqq/+1hIGCmY8gGTSMoQyeutV2y5t7opd3Jfjqs7zo6/YY42yTFswyrNSyjKVczWIyapo8EeBxYvmDD4VcuGPeX+uNzONLW+uoR6t9R7EG4JROwxy/xKhPkj37PzO4Xr+P70zKO4OgSiru/c5UFZJioHFqOUgZcFEsT85bFE99xDYZzHjJIU8THNGRm2f0yhjDBG75MEMGO/BvYR7vDranuOjoqYx/oGM1SZQy+w/TJOa9YL7Pp1xEUu08LLxMRiUiKGpw=="
            myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "0006326");
            //myremotepost.Add("BankCode", "0361193");
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();



        }
        else if (Session["merchant"].ToString() == "RAJASTHAN_EGRAS")
        {

            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                                 GRN.ToString(), "6390013", "ICICI12345", "ICIC12345619072022", DateTime.Now, Amount.ToString(), status);

            //BANK_CODE = 6390013 | PAID_AMT = 1.00 | GRN = 55504032022003 | CIN = 639001300000604032022 | PAID_DATE = 2022 / 03 / 04 13:53:32 | BankReferenceNo = 159140573 | TRANS_STATUS = S | checksum = 60ea92939307dd58f4cf59a5daf5b4cc33b7111a6088a726176dab32ea3e4a7b���
            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetSHA256(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            //string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key", null);
            string cipherText = objEncry.EncryptAES256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);
            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
                                                                                       //   "WRf0fFhozOQ0NIGNIUerIGI0oKEkhL0ySy0RHZvGA6IMZuApnOVa/w/XlizOTmEr3s4bNectB1ucqu++kqq/+1hIGCmY8gGTSMoQyeutV2y5t7opd3Jfjqs7zo6/YY42yTFswyrNSyjKVczWIyapo8EeBxYvmDD4VcuGPeX+uNzONLW+uoR6t9R7EG4JROwxy/xKhPkj37PzO4Xr+P70zKO4OgSiru/c5UFZJioHFqOUgZcFEsT85bFE99xDYZzHjJIU8THNGRm2f0yhjDBG75MEMGO/BvYR7vDranuOjoqYx/oGM1SZQy+w/TJOa9YL7Pp1xEUu08LLxMRiUiKGpw=="
            myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "6390013");
            //myremotepost.Add("BankCode", "0361193");
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();



        }
        else if(Session["merchant"].ToString() == "1000132")

        {

            //string plainText = string.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}^{22}^{23}^{24}", GRN.ToString(), Head_Name[0], Head_Amount[0], Head_Name[1], Head_Amount[1], Head_Name[2], Head_Amount[2], Head_Name[3], Head_Amount[3], Head_Name[4], Head_Amount[4], Head_Name[5], Head_Amount[5], Head_Name[6], Head_Amount[6], Head_Name[7], Head_Amount[7], Head_Name[8], Head_Amount[8], RemitterName, TotalAmount, PaymentMode, TIN, LocationCode, filler);
            EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
            objEgManualBankServiceBL.GRNNumber = Convert.ToInt32(GRN.ToString());
            objEgManualBankServiceBL.BankCode = "1000132";
            string plainText = objEgManualBankServiceBL.GetEPayChallanDetail();

            plainText = GRN.ToString() + "|4394608526110|SUCCESS|" + Amount.ToString() + "|INR|DC|XXXXXX|" + "Transaction Paid Out |SBIEPAY|201928308904521|" + DateTime.Now + "| IN | 10001322019101002980 | 1000132 | 0.00 ^ 0.00 |||||||||| ";
            //plainText = plainText + "|checkSum=" + checkSum;
            string cipherText = BanksEncryptionDecryption.GetEncryptedString(plainText, "BwmHPemeQsQhpwEGWmyQtQ==");


            //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            //plainText = plainText + "|checkSum=" + CheckSum;

            //string cipherText = objEncry.EncryptAES256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "OBC.key", null);

            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
                                                                                       //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("encdata", cipherText);
            myremotepost.Add("BankCode", "1000132");

            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();


        }

        else

        {
            string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
                                         GRN.ToString(), "0361193", "OBC12345", "036005714654654000010", DateTime.Now, Amount.ToString(), status);


            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;

            string cipherText = objEncry.EncryptAES256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "OBC.key", null);

            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
                                                                                       //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("encdata", cipherText);
            myremotepost.Add("BankCode", "0361193");
          
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();


        }

        ///// Test Bank Set With Paytm   5 jan 2022
        //string strReqq = Session["AbC"].ToString();
        //SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
        //string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
        //List<string> GRNvalue = plaintext.Split('|').ToList();
        //if (GRNvalue != null && GRNvalue.Count > 0)
        //{
        //    string[] aa = GRNvalue[0].ToString().Split('=');
        //    GRN = Convert.ToInt32(aa[1].ToString());
        //    string[] ab = GRNvalue[4].ToString().Split('=');
        //    Amount = Convert.ToDouble(ab[1].ToString());
        //}
        //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6},DebitBankCode={7},BankRefNo={8},PayMode={9},Reason={10}",
        //                             GRN.ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Amount.ToString(), status,"HDFC","201908971735173","DC","SBIIB");


        //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //RemoteClass myremotepost = new RemoteClass();
        //plainText = plainText + "|checkSum=" + CheckSum;

        //string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);

        //// string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
        //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx"; //ConfigurationManager.AppSettings["SuccessURL"].ToString();// 
        //myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
        //myremotepost.Add("BankCode", "9930001");
        //myremotepost.Add("URL", Address);
        //myremotepost.Url = Address;
        //myremotepost.Post();
        /// Test Bank Set With Paytm   5 jan 2022
        //string strReqq = Session["AbC"].ToString();
        //SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
        //string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(strReqq, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);
        //List<string> GRNvalue = plaintext.Split('|').ToList();
        //if (GRNvalue != null && GRNvalue.Count > 0)
        //{
        //    string[] aa = GRNvalue[0].ToString().Split('=');
        //    GRN = Convert.ToInt32(aa[1].ToString());
        //    string[] ab = GRNvalue[2].ToString().Split('=');
        //    Amount = Convert.ToDouble(ab[1].ToString());
        //}
        //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
        //                                GRN.ToString(), "6390013", "ICICI12345", "639001314654654000010", DateTime.Now, Amount.ToString(), status);


        //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //RemoteClass myremotepost = new RemoteClass();
        //plainText = plainText + "|checkSum=" + CheckSum;

        //string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);

        //// string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
        //string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// 
        //myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
        //myremotepost.Add("BankCode", "6390013");
        //myremotepost.Add("URL", Address);
        //myremotepost.Url = Address;
        //myremotepost.Post();


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SetValue("f");

    }
    protected void btnsuccess_Click(object sender, EventArgs e)
    {
        SetValue("S");
    }
    protected void btnpending_Click(object sender, EventArgs e)
    {
        SetValue("P");
    }
    private string DecryptString(string CipherText, string KeyName, string Version)
    {
        return BanksEncryptionDecryption.GetDecryptedString(CipherText, KeyName, Version);
    }
}
