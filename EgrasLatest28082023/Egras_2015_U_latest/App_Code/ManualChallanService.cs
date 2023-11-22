using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using EgBL;
using System.Configuration;

/// <summary>
/// Summary description for ManualChallanService
/// </summary>
[WebService(Namespace = "Egras")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class ManualChallanService : System.Web.Services.WebService
{

    public ManualChallanService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetGRNManualDetail(string encData, string BsrCode)
    {
        string result = "";
        string PlainText = "";
        string cipherText = "";
        try
        {
            
            ///////////////////////////////////////////////////////
            //string plainText = string.Format("GRN={0}", "35756");

            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            //encData = objEncry.Encrypt(plainText, Server.MapPath("~/WebPages/Key/RAJASTHAN_EGRASS.key"));

            switch (BsrCode.Substring(0, 3))
            {
                case "020":
                    //PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                    PlainText = objEncry.DecryptAES256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB1.key",null);
                    result = GetGRNDetails(PlainText, BsrCode);
                    //cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                    cipherText = objEncry.EncryptAES256(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB1.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "029":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "017":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "000":
                case "001":    // Get Sbi Transaction GBSS 27 March 2019
                case "002":    // get Sbi Transaction GanagaNager Treasury
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+ "RAJASTHAN_EGRASS_Manual.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+ "RAJASTHAN_EGRASS_Manual.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "030":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "691":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "036":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;
                case "028":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                    InsertAudit( BsrCode, ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + cipherText, "M");// M- Manual challan service
                    break;

                //E- mistra 13/10 2022  Manual  challan Creation

                case "997":
                    PlainText = objEncry.DecryptAES256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "EMitra.key", null);
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.EncryptAES256(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "EMitra.key", null);
                    break;

                default:
                    result = "0";//BsrCode not matched
                    break;
            }

           
        }
        catch (Exception ex)
        {

            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        return cipherText;
    }

    private string GetGRNDetails(string plaintext, string BankCode)
    {
        string result;

        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = plaintext.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }

        EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
        objEgManualBankServiceBL.GRNNumber = Convert.ToInt64(lstPlainText[1].ToString());
        objEgManualBankServiceBL.BankCode = BankCode.Trim();
        result = objEgManualBankServiceBL.GetGrnManualDetails();
        return result;
    }

    private string DecryptString(string strQueryString, string filePath)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        return objEncry.Decrypt(strQueryString, filePath);
    }

    private void InsertAudit( string BankCode, string cipherText, string flag)
    {
        EgManualBankServiceBL ObjFrm = new EgManualBankServiceBL();
        ObjFrm.BankCode = BankCode;
        ObjFrm.CipherText = cipherText;
       // ObjFrm.PlainText = plainText;
        ObjFrm.flag = flag;
        ObjFrm.IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        ObjFrm.ManualBankServiceAuditData();
    }

}

