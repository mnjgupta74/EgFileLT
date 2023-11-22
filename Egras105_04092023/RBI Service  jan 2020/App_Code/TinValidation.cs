using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EgBL;

/// <summary>
/// Summary description for TinValidation
/// </summary>
public class TinValidation
{
	public TinValidation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string TinNo(string TinNo,int GroupCode,DateTime FromDate)
    {

        try{

            CTDWebServhttps.IFMSValidationService objWeb = new CTDWebServhttps.IFMSValidationService();
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();


        string EncData = "User=IFMSAdmin|Password=IFMSPassword|Tin=" + TinNo+ "|GroupCode=" + GroupCode + "|FromDate= " + FromDate;
        string cipherText = objEncry.Encrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"aes.key");
        string returnData = "RemitterName=Jai|Address=jaipur|CtdZone=0501|CtdCircle=1018|CtdWard=3067";
        return returnData;
        }
        catch (Exception ex)
        {
           return  "Enable to connect";
        }
    }
}