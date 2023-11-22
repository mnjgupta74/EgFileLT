using System;
using System.Collections.Generic;

using System.Web.Services;
using System.Web.Services.Protocols;
using EgBL;
using Update;

/// <summary>
/// Summary description for EgUpdateManualChallan
/// </summary>
[WebService(Namespace = "Egras")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EgUpdateManualChallan : System.Web.Services.WebService
{
    public EgUpdateManualChallan()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public AuthSoapHd spAuthenticationHeader;
    UpdateGRN objUpdate = new UpdateGRN();

    public class AuthSoapHd : SoapHeader
    {
        public string strUserName;
        public string strPassword;
        public string strBsrCode;
    }
    [WebMethod, SoapHeader("spAuthenticationHeader")]

    public string UpdateGRN(string encData, string BsrCode)
    {
        String result = string.Empty;
        try
        {
            InsertAuditTransaction(encData, BsrCode);

            //checking User Access/////////////////////////////////
            result = Checkaudit();
            ///////////////////////////////////////////////////////

            List<string> lstPlainText;
            if (result.ToString() == "1")
            {
                string PlainText = "";
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();

                switch (BsrCode)
                {
                    case "0200113":
                        PlainText = objEncry.DecryptSBIWithKey256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOB1.key", null);
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0292861":

                        
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0171051":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0006326":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0304017":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0280429":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "6910213":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    case "0361193":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                        lstPlainText = GetList(PlainText);
                        result = CheckGRNDetail(lstPlainText);
                        if (result.ToString() == "1")
                        {
                            result = UpdateStatus(lstPlainText);
                        }
                        break;
                    default:
                        result = "Data Does Not Exist !!";  //BsrCode not matched
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }

        return result;
    }

    private string DecryptString(string strQueryString, string filePath)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        return objEncry.Decrypt(strQueryString, filePath);
    }

    private string CheckGRNDetail(List<string> lstPlainText)
    {
        objUpdate.GRN = Convert.ToInt32(lstPlainText[1].ToString());
        objUpdate.BSRCode = lstPlainText[3].ToString();
        objUpdate.Amount = Convert.ToDouble(lstPlainText[11].ToString());
        objUpdate.Status = lstPlainText[13].ToString();

        return Convert.ToString(objUpdate.CheckUserBankStatus());
    }

    private List<string> GetList(string palinText)
    {
        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = palinText.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }
        return lstPlainText;
    }

    private string UpdateStatus(List<string> lstPlainText)
    {
        objUpdate.GRN = Convert.ToInt32(lstPlainText[1].ToString());
        objUpdate.BSRCode = lstPlainText[3].ToString();
        objUpdate.Ref = lstPlainText[5].ToString();
        //objUpdate.CIN = lstPlainText[7].ToString();
        //objUpdate.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
        if (lstPlainText[13].ToString() == "S" && (lstPlainText[7].ToString().Length <= 20 || lstPlainText[7].ToString() == null || lstPlainText[7].ToString() == ""))
        {
            return   "0";
        }
        else
        {
            objUpdate.CIN = lstPlainText[7].ToString();
        }
        if (lstPlainText[13].ToString() == "S" && (lstPlainText[9].ToString() == null || lstPlainText[9].ToString() == ""))
        {
            return   "0";
        }
        else
        {
            objUpdate.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
        }
        objUpdate.Amount = Convert.ToDouble(lstPlainText[11].ToString());
        objUpdate.Status = lstPlainText[13].ToString();

        return Convert.ToString(objUpdate.updateManual());
    }

    private void InsertAuditTransaction(String encdata, string bsrcode)
    {

        string url = System.Web.HttpContext.Current.Request.UserHostName;
        objUpdate.UserID = spAuthenticationHeader.strUserName;
        objUpdate.Password = spAuthenticationHeader.strPassword;
        objUpdate.BSRCode = bsrcode.ToString();
        objUpdate.encdata = encdata.ToString();
        objUpdate.TransDate = DateTime.Now.Date;
        objUpdate.URL = url.ToString();
        objUpdate.InsertAudit();
    }

    private string Checkaudit()
    {
        objUpdate.UserID = spAuthenticationHeader.strUserName;
        objUpdate.Password = spAuthenticationHeader.strPassword;
        objUpdate.BSRCode = spAuthenticationHeader.strBsrCode;

        string rv = objUpdate.CheckData();

        return rv;
    }


}

