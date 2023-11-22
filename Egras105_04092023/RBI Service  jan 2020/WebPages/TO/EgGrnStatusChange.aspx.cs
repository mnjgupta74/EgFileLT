using EgBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class WebPages_TO_EgGrnStatusChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        ViewState["UserId"] = Session["UserID"];
        Random randomclass = new Random();
        hdnRnd.Value = randomclass.Next().ToString();
        HttpContext.Current.Session["PWdRND"] = hdnRnd.Value;

    }

    [WebMethod]
    public static string GetGRNDetail(string GRN, string securePwd, string StatusVal)
    {

        //string value = "";
        //XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/SecurePassword.xml"));
        //foreach (XElement element in doc.XPathSelectElement("//Detail").Descendants())
        //{
        //    value = element.Value;
        //}
         EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        if (securePwd.Trim() == objEncryption.GetMD5Hash(System.Configuration.ConfigurationManager.AppSettings["SecurePassword"].ToString().Trim() + HttpContext.Current.Session["PWdRND"].ToString()))
 
        {
            if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != GRN)
            {
                return "-1";
            }
            string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            EgEditTreasuryBL objEgEditTreasuryBL = new EgEditTreasuryBL();
            objEgEditTreasuryBL.GRN = Convert.ToInt64(GRN);
            objEgEditTreasuryBL.UserId = Convert.ToInt32(UserID);
            objEgEditTreasuryBL.StatusValue = StatusVal;
            string JSONGRNString = objEgEditTreasuryBL.GetGRNDetailForChangeStatus();
            if (JSONGRNString.Length > 2)
            {
                string JSONGRNAmtString = objEgEditTreasuryBL.GetGRNAmtDetail();
                string JSONTreasuryString = objEgEditTreasuryBL.GetTreasury();
                return JSONGRNString + '|' + JSONGRNAmtString;
            }
            else
            {
                return "";
            }
        }
        else
            return "-2";

    }

   
    /// <summary>
    ///  Grn Update Status 26 march 2019 
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>

    [WebMethod]
    public static string UpdateStatus(string GRN, string StatusValue)
    {
        var str = Convert.ToString(HttpContext.Current.Session["HandlerVal"]);
        var grn = str;
        if (grn != GRN )
        {
            return "Invalid Request";
        }
        string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
        EgEditTreasuryBL objEgEditTreasuryBL = new EgEditTreasuryBL();
        objEgEditTreasuryBL.GRN = Convert.ToInt64(GRN);
        objEgEditTreasuryBL.UserId = Convert.ToInt32(UserID);
        objEgEditTreasuryBL.StatusValue = StatusValue;
        int status = objEgEditTreasuryBL.UpdateStatus();
        if (status == 1)
        {
            return "Update Successfully.";
        }
        else
        {
            return "Unable to Update Status.";

        }
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

            try
            {
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
            catch
            {
                plaintext = "keyError";
            }
        }

        return plaintext;
    }
}