using System;
using System.Web;
using System.Web.Services;
using EgBL;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class WebPages_TO_EgEditTreasury : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        //ViewState["UserId"] = Session["UserID"];
        Random randomclass = new Random();
        hdnRnd.Value = randomclass.Next().ToString();
        HttpContext.Current.Session["PWdRND"] = hdnRnd.Value;
    }
    [WebMethod]
    public static string GetGRNDetail(string GRN, string securePwd)
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
            string JSONGRNString = objEgEditTreasuryBL.GetGRNDetail();
            if (JSONGRNString.Length > 2)
            {
                string JSONGRNAmtString = objEgEditTreasuryBL.GetGRNAmtDetail();
                string JSONTreasuryString = objEgEditTreasuryBL.GetTreasury();
                return JSONGRNString + '|' + JSONGRNAmtString + '|' + JSONTreasuryString;
            }
            else
            {
                return "";
            }
        }
        else
            return "-2";
        
    }

    [WebMethod]
    public static string UpdateTreasury(string GRN, string treasurycode)
    {
        var str = Convert.ToString(HttpContext.Current.Session["HandlerVal"]).Split('|');
        
        var treasury = str[0];
        var grn = str[1];
        if (grn!=GRN || treasury !=treasurycode)
        {
            return "Invalid Request";
        }
        string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
        EgEditTreasuryBL objEgEditTreasuryBL = new EgEditTreasuryBL();
        objEgEditTreasuryBL.GRN = Convert.ToInt64(GRN);
        objEgEditTreasuryBL.TreasuryCode = treasurycode;
        objEgEditTreasuryBL.UserId = Convert.ToInt32(UserID);
        int status = objEgEditTreasuryBL.UpdateTreasury();
        if (status == 1)
        {
            return "Data Insert Successfully.";
        }
        else
        {
            return "Unable to update Treasury.";

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