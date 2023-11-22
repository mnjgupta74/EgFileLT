using System;
using System.Web.Services;
using EgBL;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

/// <summary>
/// Summary description for EgDepartmentLoginService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EgDepartmentLoginService : System.Web.Services.WebService
{
    public EgDepartmentLoginService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetLogin(string UserName, string IP)
    {

        string ReturnVal;
        string strReturnVal = "";
        try
        {

            string str = Codec.DecryptStringAES(UserName);
        string[] getstr = str.Split('|');

        //List<string> objlogin = new List<string>();
        EgLoginBL objLogin = new EgLoginBL();

        objLogin.LoginID = getstr[0].ToString().Trim();
        objLogin.Password = getstr[1].ToString().Trim();
        objLogin.RND = getstr[2].ToString().Trim();
        objLogin.IPAddress = IP.ToString().Trim();
        objLogin.integration = 1;
        ReturnVal = objLogin.GetLogin(objLogin.integration);

        if (string.IsNullOrEmpty(objLogin.UserType.ToString()) || string.IsNullOrEmpty(objLogin.UserId.ToString()) || (objLogin.UserType.ToString() == "0" || objLogin.UserId.ToString() == "0"))
        {

            strReturnVal = strReturnVal + "|" + ((string.IsNullOrEmpty(objLogin.UserId.ToString())) ? "-5" : objLogin.UserId.ToString());
            strReturnVal = strReturnVal + "|" + ((string.IsNullOrEmpty(objLogin.UserType.ToString())) ? "-5" : objLogin.UserType.ToString());
            strReturnVal = strReturnVal + "|" + ((string.IsNullOrEmpty(objLogin.ErrorCode.ToString())) ? "-5" : objLogin.ErrorCode.ToString());
        }
        else
        {
            strReturnVal = objLogin.UserId.ToString();
            strReturnVal = strReturnVal + "|" + objLogin.UserType.ToString();
            strReturnVal = strReturnVal + "|" + objLogin.ErrorCode.ToString();
        }
      }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            strReturnVal = "Due to Some Technical Issue Please Contact";
        }

        return (strReturnVal);
    }


  

}
public static class Codec
{
    public static string DecryptStringAES(string UserName)
    {

        //AneL6y07KC+dkKGYcMv1himqCZ8lLUNAoICGFBESTjdEvPr0qaKJZSeJdldtL3Z0
        var idKey = ConfigurationManager.AppSettings["Departmentkey"].ToString().Trim();

        //var keybytes = Encoding.UTF8.GetBytes("7061737323313244");
        //var iv = Encoding.UTF8.GetBytes("7061737323313244");
        var keybytes = Encoding.UTF8.GetBytes(idKey);
        var iv = Encoding.UTF8.GetBytes(idKey);
        //c# encrrption
        //var encryptStringToBytes = EncryptStringToBytes("It works", keybytes, iv);

        //// Decrypt the bytes to a string.
        //var roundtrip = DecryptStringFromBytes(encryptStringToBytes, keybytes, iv);

        //DECRYPT FROM CRIPTOJS
        var encrypted = Convert.FromBase64String(UserName);////+Ijpt1GDVgM4MqMAQUwf0Q==
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return decriptedFromJavascript.ToString();
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException("plainText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        byte[] encrypted;
        // Create a RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}



