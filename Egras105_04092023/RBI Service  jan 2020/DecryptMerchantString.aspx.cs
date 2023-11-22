using EgBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class DecryptMerchantString : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //string Merchant_Code = txtMerchantCode.Text;
        //string textToDecrypt = txtEncrypt.Text.ToString().Trim();
        //string result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + Merchant_Code + ".key"));
        string result = GetPlainString();
        txtDecrypt.Text = result;
    }
    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //string Merchant_Code = txtMerchantCode.Text;
        //string textToEncrypt = txtPlainText.Text.ToString().Trim();
        //string result = objEncry.Encrypt(textToEncrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + Merchant_Code + ".key"));
        string result = GetCipherText();
        txtEncryptedText.Text = result;

    }
    protected void btnEncrypt_WithoutKey_Click(object sender, EventArgs e)
    {
        EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
        txtEncryptedText.Text = ObjEncrytDecrypt.Encrypt(txtPlainText.Text.ToString().Trim());
    }
    protected void btnShow_WithoutKey_Click(object sender, EventArgs e)
    {
        EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
        string textToDecrypt = txtEncrypt.Text.ToString().Trim();
        List<string> strList = new List<string>();
        strList = ObjEncrytDecrypt.Decrypt(textToDecrypt);
        try
        {
            if (strList.Count > 0)
            {
                txtDecrypt.Text = strList[1].ToString().Trim();

            }
            else
            {
                Server.Transfer("~\\logout.aspx");

            }
        }
        catch (Exception ex)
        {

        }
    }
    private string GetPlainString()
    {
        string MCode = txtMerchantCode.Text.Trim();
        SbiEncryptionDecryption obj = new SbiEncryptionDecryption();
        string plainstring = string.Empty;
        switch (rblTransaction.SelectedValue)
        {

            case "0":
                plainstring =  obj.Decrypt(txtEncrypt.Text.Trim(), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key");
                break;
            case "1":
                plainstring = obj.DecryptSBIWithKey256(txtEncrypt.Text.Trim(), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key", null);
                break;
            case "2":
                //plainstring = GetePayString();
                plainstring = obj.DecryptePay(txtEncrypt.Text.Trim(), "BwmHPemeQsQhpwEGWmyQtQ==");
                break;
            
        }
        return plainstring;
    }
    protected string GetCipherText()
    {
        string MCode = txtMerchantCode.Text.Trim();
        SbiEncryptionDecryption obj = new SbiEncryptionDecryption();
        string plainstring = string.Empty;
        switch (rblTransaction.SelectedValue)
        {

            case "0":
                plainstring = obj.Encrypt(txtPlainText.Text.Trim(), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key");
                break;

            case "1":
                plainstring = obj.EncryptSBIWithKey256(txtPlainText.Text.Trim(), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key");
                break;
            case "2":
               // plainstring = GetePayString();
                plainstring = obj.Encrypt(txtEncrypt.Text.Trim(), "BwmHPemeQsQhpwEGWmyQtQ==");
               
                break;
           
        }
        return plainstring;
    }
    //public string GetePayString()
    //{
    //    string test = "4KKGCRJQY+7LwUSIzvxkqT/jNJL1fs8kPEbpdGrzFa+awDCLObmoI+9Ix3VTEzYERKj/3MtX5K0WYI31p2O975CKM8/TRIbmFhOKznYZ1DDAj8sk232AYAYqTcwuKbd0XPi62cWd0CTXKOtVRVxJF1kJZI6VhMi3FvTS3cRe5YmwdWbuQgoxLhlfOAihHWWi/fe5ZLVxzyMW0IuqnZbAqm0IV3jCP0MGAf9AwlJwjAPTfoUBjjzkXGaZhj1yRfDei4gcX9Yi6LY8q6VvEBvyNjhZVEolz1p3rcIz+1lkOkS1Vf/jwrrUHglnG9OQDepe";
    //    SbiEncryptionDecryption obj = new SbiEncryptionDecryption();
    //    // convert string to stream
    //    byte[] byteArray = Encoding.ASCII.GetBytes(test);
    //    MemoryStream stream = new MemoryStream(byteArray);

    //    // convert stream to string
    //    // StreamReader reader = new StreamReader( stream );
    //    //string text = reader.ReadToEnd();


    //    Stream data = stream;
    //    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
    //    StreamReader readStream = new StreamReader(data, encode);
    //    Char[] read = new Char[256];
    //    //read = "4KKGCRJQY+7LwUSIzvxkqT/jNJL1fs8kPEbpdGrzFa+awDCLObmoI+9Ix3VTEzYERKj/3MtX5K0WYI31p2O975CKM8/TRIbmFhOKznYZ1DDAj8sk232AYAYqTcwuKbd0XPi62cWd0CTXKOtVRVxJF1kJZI6VhMi3FvTS3cRe5YmwdWbuQgoxLhlfOAihHWWi/fe5ZLVxzyMW0IuqnZbAqm0IV3jCP0MGAf9AwlJwjAPTfoUBjjzkXGaZhj1yRfDei4gcX9Yi6LY8q6VvEBvyNjhZVEolz1p3rcIz+1lkOkS1Vf/jwrrUHglnG9OQDepe";
    //    int count = readStream.Read(read, 0, 256);
    //    string returnData = "";
    //    while (count > 0)
    //    {
    //        String str = new String(read, 0, count);
    //        returnData = returnData + "" + str;
    //        count = readStream.Read(read, 0, 256);
    //    }
    //    //response.Close();
    //    returnData = obj.DecryptePay(returnData, "BwmHPemeQsQhpwEGWmyQtQ==");
    //    return returnData;
    //}
}
