using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class A : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}", "31623619", "0220123","0","0", "2023/06/15 00:18:23", "980.00","P");
        EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        RemoteClass myremotepost = new RemoteClass();
        plainText = plainText + "|checkSum=" + CheckSum;

        string cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOI.key");
        //string plainToken = "BOI|Boi#22";
        //string encToken = objEncry.Encrypt(plainToken, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOI.key");

        //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
        string Address = "http://localhost:56933/PaymentVerifyURL.aspx";
        //string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
        myremotepost.Add("encData", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
        myremotepost.Add("BankCode", "0220123");
        myremotepost.Add("URL", Address);
        myremotepost.Url = Address;
        myremotepost.Post();
    }
}