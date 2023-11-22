using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using Update;
using EgBL;
using Newtonsoft.Json;

[WebService(Namespace = "Egras")]

[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Service : System.Web.Services.WebService
{
    public AuthSoapHd spAuthenticationHeader;
    UpdateGRN objUpdate = new UpdateGRN();

    public Service()
    {
    }
    public class AuthSoapHd : SoapHeader
    {
        public string strUserName;
        public string strPassword;
        public string strBsrCode;
    }
    [WebMethod, SoapHeader("spAuthenticationHeader")]

    public string UpdateGRN(string encData, string BsrCode)
    {
        string result = string.Empty;
        try
        {
            //checking User Access/////////////////////////////////
            result = Checkaudit();
            ///////////////////////////////////////////////////////

            if (result.ToString() == "1")
            {
                string PlainText = "";
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();

                switch (BsrCode)
                {
                    case "0200113":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOB.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;

                    case "0292861":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "Union_Bank.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0171051":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0006326":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0304017":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PNB.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0001234":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "HDFC.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "6910213":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "IDBI.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0361193":
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "OBC.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0220123":  // BOI
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOI.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "0240539":  // canara
                        PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "Canara.key");
                        result = UpdateStatus(PlainText, encData, BsrCode);
                        break;
                    case "9920001": //  Pine Lab
                        result = UpdatePineLabStatus(encData, BsrCode);
                        break;

                    case "1000132": //  SBI ePay 14/09/2020
                        UpdateStatus objUpdate = new UpdateStatus();
                        objUpdate.CipherText = encData;
                        objUpdate.KeyName = "BwmHPemeQsQhpwEGWmyQtQ==";
                        bool a = objUpdate.UpdateGRNStatus();
                        result = a == true ? "1" : "0";
                        break;

                    default:
                        result = "0";//BsrCode not matched
                        break;
                }
            }
        }
        catch (Exception ex)
        {

            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + '-' + BsrCode + '-' + encData);

        }
        return result;
    }
    private string DecryptString(string strQueryString, string filePath)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        return objEncry.Decrypt(strQueryString, filePath);
    }

    private string UpdateStatus(string palinText, string encdata, string BsrCode)
    {
        string rv;

        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = palinText.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }
        InsertAuditTransaction(encdata, BsrCode, Convert.ToInt64(lstPlainText[1].ToString()));
        objUpdate.GRN = Convert.ToInt64(lstPlainText[1].ToString());
        objUpdate.BSRCode = lstPlainText[3].ToString();
        objUpdate.Ref = lstPlainText[5].ToString();
        if (lstPlainText[13].ToString() == "S" && (lstPlainText[7].ToString().Length <= 20 || lstPlainText[7].ToString() == null || lstPlainText[7].ToString() == ""))
        {
            return rv = "0";
        }
        else
        {
            objUpdate.CIN = lstPlainText[7].ToString();
        }
        if (lstPlainText[13].ToString() == "S" && (lstPlainText[9].ToString() == null || lstPlainText[9].ToString() == ""))
        {
            return rv = "0";
        }
        else
        {
            objUpdate.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
        }
        objUpdate.Amount = Convert.ToDouble(lstPlainText[11].ToString());
        objUpdate.Status = lstPlainText[13].ToString();

        rv = Convert.ToString(objUpdate.update());


        return rv;
    }


    private string UpdatePineLabStatus(string Jsonencdata, string BsrCode)
    {
        //string Jsonencdata = "{\"GRN\":\"27672869\",\"Bank_Code\": \"9920001\",\"BankReferenceNo\": \"CK58102178\",\"CIN\": \" 992000106494921012019\",\"Paid_Date\": \"2019/11/27 13:27:00\",\"Paid_Amt\":\"259.00\",\"TRANS_STATUS\":\"S\",\"GT_BSRCode\":\"eChallan\",\"BankRefNo\": \"XXXXX\",\"PayMode\": \"CC\",\"SecurityToken\": \"<value created using attached logic > \"}";
        string rv = string.Empty;
        var myDetails = JsonConvert.DeserializeObject<PineLabs>(Jsonencdata);
        SortedList<string, string> postRequestData = new SortedList<string, string>();
        postRequestData["GRN"] = myDetails.GRN.ToString();
        postRequestData["Bank_Code"] = myDetails.Bank_Code;
        postRequestData["BankReferenceNo"] = myDetails.BankReferenceNo;
        postRequestData["CIN"] = myDetails.CIN;
        postRequestData["Paid_Date"] = myDetails.Paid_Date.ToString();
        postRequestData["Paid_Amt"] = String.Format("{0:0.00}", myDetails.Paid_Amt);
        postRequestData["TRANS_STATUS"] = myDetails.TRANS_STATUS;
        postRequestData["GT_BSRCode"] = myDetails.GT_BSRCode;
        postRequestData["BankRefNo"] = myDetails.BankRefNo;
        postRequestData["PayMode"] = myDetails.PayMode;
        PineLab objPineLab = new PineLab();
        var EncrypedString = objPineLab.GetPineLabRequestString(postRequestData);
        if (myDetails.SecurityToken.Trim() == EncrypedString.Trim())
        {
            objUpdate.GRN = myDetails.GRN;
            objUpdate.Amount = Convert.ToDouble(myDetails.Paid_Amt);
            objUpdate.BSRCode = myDetails.Bank_Code;
            objUpdate.Ref = myDetails.BankReferenceNo;
            objUpdate.CIN = myDetails.CIN;
            objUpdate.timeStamp = Convert.ToDateTime(myDetails.Paid_Date);
            objUpdate.Status = myDetails.TRANS_STATUS;
            rv = Convert.ToString(objUpdate.update());

            if (objUpdate.BSRCode == "9920001")    // Add Update Method For Pine Lab
            {
                objUpdate.epayBSRCode = (myDetails.GT_BSRCode);
                objUpdate.bankRefNo = (myDetails.BankRefNo);
                objUpdate.payMode = myDetails.PayMode;
                objUpdate.Reason = myDetails.Reason;
                objUpdate.updatePineLabStatus();
            }
        }
        else
        {
            rv = "Security Token Does Not Match !";
        }

        return rv;
    }
    private void InsertAuditTransaction(String encdata, string bsrcode, Int64 GRN)
    {

        string url = System.Web.HttpContext.Current.Request.UserHostName;
        // string url = Path.GetDirectoryName(Application.ExecutablePath);
        //string url = Request.UrlReferrer.AbsoluteUri.ToString(); //HttpContext.Current.Request.ApplicationPath;
        // string address = url[0] + url[1] + url[2];
        //   IPHostEntry ipHostInfo = Dns.GetHostByName(url[2].ToString());
        // IPAddress ipAddress = ipHostInfo.AddressList[0];

        objUpdate.UserID = spAuthenticationHeader.strUserName;
        objUpdate.Password = spAuthenticationHeader.strPassword;
        objUpdate.BSRCode = bsrcode.ToString();
        objUpdate.encdata = encdata.ToString();
        objUpdate.TransDate = DateTime.Now.Date;
        objUpdate.URL = url.ToString();
        objUpdate.GRN = GRN;
        //objUpdate.IPAddress = Request.UserHostAddress;//ipAddress.ToString();
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

class PineLabs
{
    private Int32 grn;

    public Int32 GRN
    {
        get
        {
            return grn;
        }
        set
        {
            if (value <= 0)
            {
                throw new Exception("GRN is Null or 0 !!");
            }
            grn = value;
        }
    }
    private string bank_Code;

    public string Bank_Code
    {
        get
        {
            return bank_Code;
        }
        set
        {
            if (string.IsNullOrEmpty(value) || value.Trim() == "0".Trim())
            {
                throw new Exception("Bank_Code is Null or 0 !!");
            }
            bank_Code = value;
        }
    }
    private string bankReferenceNo;
    public string BankReferenceNo
    {
        get
        {
            return bankReferenceNo;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("BankReferenceNo is Null");
            }
            bankReferenceNo = value;
        }
    }
    private string cin;

    public string CIN
    {
        get
        {
            return cin;
        }
        set
        {
            if (string.IsNullOrEmpty(value) || value.Length <= 20)
            {
                throw new Exception("CIN is Invalid ");
            }
            cin = value;
        }
    }
    private string paid_Date;

    public string Paid_Date
    {
        get
        {
            return paid_Date;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Paid_Date is Null ");
            }
            paid_Date = value;
        }
    }
    private decimal paid_Amt;

    public decimal Paid_Amt
    {
        get
        {
            return paid_Amt;
        }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Paid_Amt is Null ");
            }
            paid_Amt = value;
        }
    }
    private string trans_status;

    public string TRANS_STATUS
    {
        get
        {
            return trans_status;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("TRANS_STATUS is Null ");
            }
            trans_status = value;
        }
    }
    private string gt_bsrcode;

    public string GT_BSRCode
    {
        get
        {
            return gt_bsrcode;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("GT_BSRCode is Null ");
            }
            gt_bsrcode = value;
        }
    }
    private string bankrefno;

    public string BankRefNo
    {
        get
        {
            return bankrefno;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("BankRefNo is Null");
            }
            bankrefno = value;
        }
    }
    private string payMode;

    public string PayMode
    {
        get
        {
            return payMode;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("PayMode is Null ");
            }
            payMode = value;
        }
    }
    private string reason;

    public string Reason
    {
        get
        {
            return reason;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("Reason is Null ");
            }
            reason = value;
        }
    }
    private string securityToken;

    public string SecurityToken
    {
        get
        {
            return securityToken;
        }
        set
        {
            if (string.IsNullOrEmpty(value) || value.Trim() == "0".Trim())
            {
                throw new Exception("SecurityToken is Null or 0 !!");
            }
            securityToken = value;
        }
    }
}

