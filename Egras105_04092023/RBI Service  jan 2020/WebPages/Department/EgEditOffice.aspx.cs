using System;
using System.Web;
using System.Web.Services;
using EgBL;
using System.IO;
using System.Security.Cryptography;

public partial class WebPages_Department_EgEditOffice : System.Web.UI.Page
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
    public static string GetGRNDetail(string GRN)
    {
        string value = "";
        //value = System.Web.Configuration.WebConfigurationManager.AppSettings["SecurePassword"];
       // EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
       // if (securePwd.Trim() == objEncryption.GetMD5Hash(value.ToString().Trim() + HttpContext.Current.Session["PWdRND"].ToString()))
       // {
            string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            EgEditTreasuryBL objEgEditTreasuryBL = new EgEditTreasuryBL();
            objEgEditTreasuryBL.GRN = Convert.ToInt64(GRN);
            objEgEditTreasuryBL.UserId = Convert.ToInt32(UserID);
            string JSONGRNString = objEgEditTreasuryBL.GetGRNFromOfficeTransfer();
            if (JSONGRNString.Length > 2)
            {
                string JsonOffice = objEgEditTreasuryBL.GetOfficeList();
                string JSONDistrict = objEgEditTreasuryBL.GetTreasury();
                return  JsonOffice + '|' + JSONDistrict + '|' + JSONGRNString;
            }
            else
            {
                return "-1";
            }
    //    }
      //  else
          //  return "-2";

    }
    [WebMethod]
    public static string GetOfficeDetails(string DeptCode, string TreasuryCode)
    {
        EgEditTreasuryBL objEgEditOfficeBL = new EgEditTreasuryBL();
        objEgEditOfficeBL.DeptCode = Convert.ToInt32(DeptCode);
        objEgEditOfficeBL.DistrictId = TreasuryCode;
        string JsonOffice = objEgEditOfficeBL.GetOfficeList();
        return JsonOffice;
    }

    [WebMethod]
    public static string UpdateTreasury(string GRN, string OfficeId,string TreasuryCode)
    {
        string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
        EgEditTreasuryBL objEgEditTreasuryBL = new EgEditTreasuryBL();
        objEgEditTreasuryBL.GRN = Convert.ToInt64(GRN);
        objEgEditTreasuryBL.OfficeId = Convert.ToInt32(OfficeId);
        objEgEditTreasuryBL.UserId = Convert.ToInt32(UserID);
        objEgEditTreasuryBL.TreasuryCode = TreasuryCode;
        int status = objEgEditTreasuryBL.UpdateOffice();
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