using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Paytm
/// </summary>
public class PaytmBank : Banks
{
    private string BankCode;
    public PaytmBank()
    {
        KeyName = "PayTMKey";
        BankCode = "9930001";
        Version = "2.0";
        isPG = true;
    }

    public override string CallManualDataPushService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyManualService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyService(string cipherText)
    {
        double Amount = 0.0;
        Int64 GRN = 0;

        SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
        string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(cipherText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
        List<string> GRNvalue = plaintext.Split('|').ToList();
        if (GRNvalue != null && GRNvalue.Count > 0)
        {
            string[] aa = GRNvalue[0].ToString().Split('=');
            GRN = Convert.ToInt64(aa[1].ToString());
            string[] ab = GRNvalue[1].ToString().Split('=');
            Amount = Convert.ToDouble(ab[1].ToString());
        }
        //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
        //                             GRN.ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Amount.ToString(), "S", "PayTM", "201908971735173", "DC", "SBIIB");



        //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //RemoteClass myremotepost = new RemoteClass();
        //plainText = plainText + "|checkSum=" + CheckSum;
        ////string cipherText = objEncry.Encrypt(plainText, Server.MapPath("../WebPages/Key/RAJASTHAN_EGRASS.key"));
        //string cipherText1 = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key");

        //return cipherText1;


        try
        {
            Dictionary<string, string> data_raw = new Dictionary<string, string>();

            data_raw.Add("MID", "IFMSRA96708754236819");
            //data_raw.Add("ORDERID", "" + Session["GrnNumber"].ToString() + "");
            data_raw.Add("ORDERID", "" + GRN.ToString() + "");
            data_raw.Add("checksum", GetMD5Hash("MID=IFMSRA96708754236819|ORDERID=" + GRN.ToString()));
            //data_raw.Add("checksum", GetSHA256("MID=IFMSRA96708754236819|ORDERID=" + GRN.ToString()));
            string post_data = JsonConvert.SerializeObject(data_raw);

            //For  Staging
            string url = "https://securegw-stage.paytm.in/merchant-status/HANDLER_INTERNAL/TXNSTATUS?MID=IFMSRA96708754236819";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();

                var STATUS = JsonConvert.DeserializeObject<PayTmTransactionStauts>(responseData);
                var resultStatus = STATUS.encData;
                string plaintext1 = ObjEncrytDecrypt.DecryptSBIWithKey256(resultStatus, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
                List<string> GRNvalue1 = plaintext1.Split('|').ToList();

                responseData = resultStatus;
                //responseData = ObjEncrytDecrypt.EncryptSBIWithKey256("GRN=7335057|BANK_CODE=9930001|BankReferenceNo=20220615111212800110168989303787858|CIN=null|PAID_DATE=2022/06/15 05:56:43|PAID_AMT=22.00|TRANS_STATUS=S|DebitBankCode=null|BankRefNo=5210039|PayMode=UPI|Reason=Txn Success|checksum=ac14ca3914ea9bca2e2a49303a1db49c", System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key");
                //responseData = ObjEncrytDecrypt.EncryptSBIWithKey256("GRN=7335051|BANK_CODE=9930001|BankReferenceNo=20220613111212800110168921703787398|CIN=null|PAID_DATE=Null|PAID_AMT=55.00|TRANS_STATUS=P|DebitBankCode=null|BankRefNo=|PayMode=null|Reason=Looks like the payment is not complete. Please wait while we confirm the status with your bank.|checksum=0cb9c625417c279dfa50500e124e6641", System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key");
            }
            return responseData;
            //return ;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Due to technical error, We could not process the request !";
        }
    }

    public override Dictionary<string, string> GetBankForwardString()
    {
        string plainText = string.Format("GRN={0}|HEAD_OF_ACC={1}|AMT={2}|REMITTER_NAME={3}|TOTALAMOUNT={4}|PayMode={5}|REG-TIN-NO={6}|LocationCode={7}|Filler={8}", GRN.ToString(), Head_Name[0], Head_Amount[0], RemitterName, TotalAmount.ToString(), PaymentMode, TIN, LocationCode, filler);
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        checkSum = objEncryption.GetMD5Hash(plainText);
        plainText = plainText + "|checksum=" + checkSum;
        string EncData = BanksEncryptionDecryption.GetEncryptedString(plainText, KeyName, Version);
        return GetforwardDictionary(EncData);


    }
    /// <summary>
    /// Add SHA 256 Method 21/12/2021  For SBI Integration
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetSHA256(string name)
    {

        SHA256 SHA256 = new SHA256CryptoServiceProvider();
        byte[] ba = SHA256.ComputeHash(Encoding.Default.GetBytes(name));
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    public string GetMD5Hash(string name)
    {

        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] ba = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}