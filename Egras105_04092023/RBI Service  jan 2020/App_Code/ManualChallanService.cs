using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using EgBL;

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
                    PlainText = objEncry.DecryptSBIWithKey256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB1.key",null);
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.EncryptSBIWithKey256(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB1.key",null);
                    break;
                case "029":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                    break;
                case "017":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    break;
                case "000":
                case "001":    // Get Sbi Transaction GBSS 27 March 2019
                case "002":    // get Sbi Transaction GanagaNager Treasury
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    break;
                case "030":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                    break;
                case "691":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                    break;
                case "036":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                    break;
                case "028":
                    PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                    break;

                //E- mistra 13/10 2022  Manual  challan Creation

                case "100":
                    PlainText = objEncry.DecryptSBIWithKey256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "EMitra.key", null);
                    result = GetGRNDetails(PlainText, BsrCode);
                    cipherText = objEncry.EncryptSBIWithKey256(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "EMitra.key", null);
                    break;


                default:
                    result = "Data Does Not Exist !!";
                   //BsrCode not matched
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



}

