using EgBL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for PAYU
/// </summary>
public class PAYU : Banks
{
    private string BankCode;
    private string ProductInfo;
    private string EmailId;
    private string HashText;
    public string RESTServiceURL { get; private set; }
    public string ResultString { get; private set; }
    public string URITemplateMethodName { get; private set; }


    public PAYU()
    {
        ProductInfo = "XYZ";
        EmailId = "xyz@mail.com";
        BankCode = "9910001";
        KeyName = "PayU";
    }
    public override string CallManualDataPushService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, string> GetforwardDictionary(string EncData)
    {
        string AmountForm = Convert.ToDecimal(TotalAmount).ToString("g29");// eliminating trailing zeros
        Dictionary<string, string> GetforwardDictionary = new Dictionary<string, string>()
        {
            { "hash", EncData },
            { "txnid", GRN.ToString()},
            { "key", ConfigurationManager.AppSettings["PayUKey"].ToString() },
            { "amount", TotalAmount.ToString()},
            {"firstname", RemitterName.ToString() },
            {"email", EmailId},
            { "phone", "2222222222"},
            {"productinfo", ProductInfo},
            {"surl", ConfigurationManager.AppSettings["SuccessURL"].ToString() },
            { "furl", ConfigurationManager.AppSettings["FailURL"].ToString() },
            { "lastname", "xyz"},
            { "udf1", BankCode},
            { "udf2", Head_Name[0].ToString()},
            { "url",URL}
        };
        return GetforwardDictionary;

    }


    public override string CallVerifyService(string cipherText)
    {
        try
        {
            var responseString = "";
            using (var client = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var values = new NameValueCollection();
                values["key"] = cipherText.Split('|').GetValue(0).ToString();
                values["var1"] = cipherText.Split('|').GetValue(1).ToString();
                values["command"] = cipherText.Split('|').GetValue(2).ToString();
                values["hash"] = cipherText.Split('|').GetValue(3).ToString();
                var response = client.UploadValues(ConfigurationManager.AppSettings["PayuVerifyURL"].ToString(), values);
                //var  response = "MihPayid=16835296029&CIN=PNBG7229895420022023&mode=NB&status=success&txnid=72298954&amount=166894.00&addedon=2023-02-20 18:30:12&udf1=9910001&udf2=8443&bank_ref_num=PG-20230220182907025000000-719&bankcode=AUSFNB&error=E000&hash=31e482f5d685d9d8fee267d642d0c889dc222a7ce10d1f5de99ae0e149027005ce4ea657c24dafc891a8fa5f7c99617f8cf6ff7675aadfefadf977d7bcd773e4";
                //var response = "mihpayid=16892875772&cin=PNBG7267086501032023&mode=CASH&status=success&txnid=72670865&amount=10.00&addedon=2023-03-01 16:46:09&udf1=9910001&udf2=0029&bank_ref_num=20230301010410000837731066167974505&bankcode=PAYTM&error=E000&hash=6678078fa57949b25d7ff93848bda8a9b653fd8d751f7b421b66c13fffcd64fb89320bc641b5e0b4c7b3ccc5d5538813b159a7ae3b235c219aaf8f25066b48f1";
                responseString = Encoding.Default.GetString(response);
                IpAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["PayuVerifyURL"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                UploadData(BankCode, responseString, Convert.ToInt64(cipherText.Split('|').GetValue(1).ToString()), IpAddress, "N");
            }
            if (responseString.Split('&').GetValue(0).ToString().Split('=').GetValue(1).ToString() == "InvalidRequest" || responseString.Split('&').GetValue(0).ToString().Split('=').GetValue(1).ToString() == "hashIncorrect")
            {
                return null;
            }
            else
            {
                List<string> lstPlainText = new List<string>();
                lstPlainText = responseString.Split('&').ToList();
                Dictionary<string, string> dict = new Dictionary<string, string>();
                for (int i = 0; i < lstPlainText.Count; i++)
                {
                    dict.Add(lstPlainText[i].Split('=').GetValue(0).ToString().ToLower(), lstPlainText[i].Split('=').GetValue(1).ToString());
                }
                string Responsehash = dict["hash"];

                if (responseString.Split('&').GetValue(0).ToString().Split('=').GetValue(1).ToString() == "TransactionIdNotFound")
                {
                    //HashText = responseString.Split('&').GetValue(1).ToString().Split('=').GetValue(1).ToString();
                    HashText = dict["txnid"];
                }
                else
                {

                    HashText = ConfigurationManager.AppSettings["PayuSalt"].ToString() +
                                    "|" + dict["status"] +//status
                                    "|" + dict["amount"] +//amount
                                    "|" + dict["udf1"] +//udf1
                                    "|" + dict["txnid"] +//txnid
                                    "|" + ConfigurationManager.AppSettings["PayUKey"].ToString();

                }
                HashText = GenerateSHA512String(HashText).ToLower();
                if (HashText != Responsehash)
                {
                    responseString = "InCorrectHash&" + responseString;
                    //responseString = "CorrectHash&" + responseString;
                }
                else
                {
                    if (responseString.Split('&').GetValue(0).ToString().Split('=').GetValue(1).ToString() == "TransactionIdNotFound")
                    {
                        responseString = "status=Failure&" + responseString.Split('&').GetValue(1).ToString() + "&hash=0&mode=0&amount=" + TotalAmount + "&mihpayid=0&cin=NA&bank_ref_num=NA&error=0&bankcode=0&addedon=" + DateTime.Now + "&udf1=" + BankCode + "&udf2=0";
                        responseString = "CorrectHash&" + responseString;
                    }
                    else
                    {
                        responseString = "CorrectHash&" + responseString;
                    }
                }

            }
            return responseString;
        }

        catch (Exception ex)
        {
            InsertErrorLog(ex.Message, "9910001", ConfigurationManager.AppSettings["PayuVerifyURL"].ToString(), 1);
            return null;
        }
    }

    //protected override string DecryptResponseString(string cipherText)
    //{
    //    ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
    //    return objEpay.Decrypt(cipherText, KeyName, 128);
    //}
    //(C0Dr8m|12345|10|Shopping|Test|test @test.com||abc||15|||||||3sf0jURk)
    public override Dictionary<string, string> GetBankForwardString()
    {
        //string plainText = string.Format(ConfigurationManager.AppSettings["PayUKey"].ToString()+ "|"+GRN.ToString()+ "|"+ TotalAmount.ToString() + "|" +ProductInfo + "|" +RemitterName.ToString().Trim()+"|"+EmailId+"|||||||||||" + ConfigurationManager.AppSettings["PayuSalt"].ToString());
        string plainText = string.Format(ConfigurationManager.AppSettings["PayUKey"].ToString()+"|" +GRN.ToString()+"|"+ TotalAmount.ToString()+ "|"+ProductInfo+"|"+RemitterName.ToString()+ "|"+EmailId+ "|"+BankCode+"|"+Head_Name[0].ToString()+"|||||||||" + ConfigurationManager.AppSettings["PayuSalt"].ToString());
        plainText = GenerateSHA512String(plainText).ToLower();
        if (Convert.ToInt64(GRN.ToString()) != Convert.ToInt64(HttpContext.Current.Session["GrnNumber"]) || TotalAmount != Convert.ToDouble(HttpContext.Current.Session["NetAmount"]))
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("ErrorAgencyBank" + '-' + Convert.ToString(GRN) + '-' + HttpContext.Current.Session["GrnNumber"].ToString());
            HttpContext.Current.Response.Redirect("~/ErrorPage.aspx");
        }
        return GetforwardDictionary(plainText);
    }

    public static string GenerateSHA512String(string inputString)
    {
        SHA512 sha512 = SHA512Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha512.ComputeHash(bytes);
        return GetStringFromHash(hash);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }

    public override string GetRequestString(string plainText)
    {
        plainText = string.Format(ConfigurationManager.AppSettings["PayUKey"].ToString()+"|"+ plainText.Split('|').GetValue(0).ToString().Split('=').GetValue(1).ToString() + "|"+"verify_payment");
        HashText = plainText.Split('|').GetValue(0).ToString() + "|" + plainText.Split('|').GetValue(2).ToString() + "|" + plainText.Split('|').GetValue(1).ToString() + "|" + ConfigurationManager.AppSettings["PayuSalt"].ToString();
        HashText = GenerateSHA512String(HashText).ToLower();
        return plainText+"|"+HashText;
    }
    private void UploadData(string BankCode, string encData, Int64 GRn, string IpAddress, string Paymenttype)
    {
        EgFrmTOVerified ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        ObjFrm.encData = encData;
        ObjFrm.Ipaddress = IpAddress;
        ObjFrm.GRN = GRn;
        ObjFrm.paymentType = Paymenttype;
        ObjFrm.BankResponseAudit();
    }
    public override string CallVerifyManualService(string cipherText)
    {
        throw new NotImplementedException();
    }
}