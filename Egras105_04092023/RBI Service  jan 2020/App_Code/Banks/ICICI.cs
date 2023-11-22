using EgBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for ICICI
/// </summary>
public class ICICI : Banks
{  /// <summary>
   /// version 2.0 Payment , Verification, corporateService, Scroll SHA256
   /// </summary>
    private string BankCode;
    public ICICI()
    {
        KeyName = "ICICI";
        BankCode = "6390013";
        Version = "2.0";
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
         var response = string.Empty;
        // string response;
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //ServicePointManager.Expect100Continue = true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //byte[] byteArray = Encoding.UTF8.GetBytes(cipherText);
        ////WebRequest request = WebRequest.Create("http://182.74.238.200/API/CloudBasedIntegration/V2/GetChallanDetails");
        //WebRequest request = WebRequest.Create("https://apibankingonesandbox.icicibank.com/api/v1/GIB/RajasthanDoubleVerification");
        //request.Method = "POST";
        //request.ContentLength = byteArray.Length;
        //request.Headers.Add("apikey", "HImIO9hOkw3cLAwj3oBbtPi0maWHP0b6");
        //request.ContentType = "application/json";

        ////IpAddress = "10.130.34.152"; //Dns.GetHostByName("https://www.plutuscloudserviceuat.in").AddressList[0].ToString();
        //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //{
        //    streamWriter.Write(cipherText);
        //    streamWriter.Flush();
        //    streamWriter.Close();

        //    var httpResponse = (HttpWebResponse)request.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        response = streamReader.ReadToEnd();
        //    }
        //}
        //  return returnData;


        //byte[] byteArray = Encoding.UTF8.GetBytes(cipherText);
        //WebRequest request = WebRequest.Create("https://apibankingonesandbox.icicibank.com/api/v1/GIB/RajasthanDoubleVerification");
        //request.Method = "POST";
        //request.Headers.Add("apikey", "HImIO9hOkw3cLAwj3oBbtPi0maWHP0b6");
        //request.ContentLength = byteArray.Length;
        //request.ContentType = "application/x-www-form-urlencoded";
        //Stream dataStream = request.GetRequestStream();
        //dataStream.Write(byteArray, 0, byteArray.Length);
        //dataStream.Close();
        ////IpAddress = Dns.GetHostByName("sbiepay.sbi").AddressList[0].ToString();
        //WebResponse response = request.GetResponse();
        //Stream data = response.GetResponseStream();
        //Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        //StreamReader readStream = new StreamReader(data, encode);
        //Char[] read = new Char[256];
        //int count = readStream.Read(read, 0, 256);
        //string returnData = "";
        //while (count > 0)
        //{
        //    String str = new String(read, 0, count);
        //    returnData = returnData + "" + str;
        //    count = readStream.Read(read, 0, 256);
        //}
        ////response.Close();
        //return returnData;



        string URI = "https://apibankingonesandbox.icicibank.com/api/v1/GIB/RajasthanDoubleVerification";

        ////string a1 = "jRcIDsbu+OfnOGhomYGMjnK7lo3KslmXUygN9vU0sBo18nmGPeInHUzv0cJgemhp12UzzSNCoKwZh4gzR22dSWcupCfLXhH1RKsjw/5llh9w9aHxpDox90Iyim6mjYoFp3fd4g==";
        //string myParameters = "encdata=" + cipherText.Replace("+", "%2B") + "";
        string myParameters = "encdata=" + cipherText + "";
        //string aa = "LykjI3X7lLeWVz9KJcLxYHpJM5NeFRzCFyjkmI6hR6bDjw2Ua9mEM/qvzv1xWK3eozvJdn0WCL79dBuBD6XOJPrkbmr1r3OkyWanWj+lvXNvcksP59hYT/fbVuHI3w7eK8KmemW/MBhRE4NoVrTMAwXOUO1BkonT43hhG8trJJk=";
        //string myParameters = "encdata=" + aa + "";

        //string myParameters = "encdata=" + a1 + "";
        string beforesend = myParameters;
        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add("apikey", "HImIO9hOkw3cLAwj3oBbtPi0maWHP0b6");
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
           // wc.Headers.Add("Content-Type", "application/multipart/form-data");


             response = wc.UploadString(URI, "POST", myParameters);
            //string aa = "LykjI3X7lLeWVz9KJcLxYHpJM5NeFRzCFyjkmI6hR6bDjw2Ua9mEM%2Fqvzv1xWK3eozvJdn0WCL79dBuBD6XOJPrkbmr1r3OkyWanWj%2BlvXNvcksP59hYT%2FfbVuHI3w7eK8KmemW%2FMBhRE4NoVrTMAwXOUO1BkonT43hhG8trJJk%3D";
        }
        return response;
    }
    // byte[] byteArray = Encoding.UTF8.GetBytes(aa);
    // WebRequest request = WebRequest.Create("https://apibankingonesandbox.icicibank.com/api/v1/GIB/RajasthanDoubleVerification");
    // request.Method = "POST";
    // request.Headers.Add("apikey", "HImIO9hOkw3cLAwj3oBbtPi0maWHP0b6");
    // request.ContentLength = byteArray.Length;
    // request.ContentType = "application/json";
    // Stream dataStream = request.GetRequestStream();
    // dataStream.Write(byteArray, 0, byteArray.Length);
    // dataStream.Close();
    //// IpAddress = Dns.GetHostByName("sbiepay.sbi").AddressList[0].ToString();
    // WebResponse response = request.GetResponse();
    // Stream data = response.GetResponseStream();
    // Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
    // StreamReader readStream = new StreamReader(data, encode);
    // Char[] read = new Char[256];
    // int count = readStream.Read(read, 0, 256);
    // string returnData = "";
    // while (count > 0)
    // {
    //     String str = new String(read, 0, count);
    //     returnData = returnData + "" + str;
    //     count = readStream.Read(read, 0, 256);
    // }
    // response.Close();
    // return returnData;





    //      Dim objWebRequest As Net.HttpWebRequest

    //Dim objWebResponse As Net.HttpWebResponse

    //Dim srReader As IO.StreamReader

    //Dim strHTML As String

    //objWebRequest = CType(Net.WebRequest.Create(URL), Net.HttpWebRequest)

    // objWebRequest.Method = "GET"

    // objWebRequest.ContentType = "application/json"

    // objWebRequest.Credentials = CredentialCache.DefaultCredentials

    //  objWebRequest.Accept = "application/json"



    //var data = wc.UploadValues(url, "POST", wc.QueryString);

    // data here is optional, in case we recieve any string data back from the POST request.
    //  responseString = UnicodeEncoding.UTF8.GetString(response);


    //DataTable a1 = JsonConvert.DeserializeObject<DataTable>(response);
    //var result = JsonConvert.DeserializeObject<eChallanPushResParent>(response);
    // return result.eChallanPaymentInqResp.value;
    //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
    // }


    //double Amount = 0.0;
    //Int64 GRN = 0;

    //SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
    //string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(cipherText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key", null);
    //List<string> GRNvalue = plaintext.Split('|').ToList();
    //if (GRNvalue != null && GRNvalue.Count > 0)
    //{
    //    string[] aa = GRNvalue[0].ToString().Split('=');
    //    GRN = Convert.ToInt64(aa[1].ToString());
    //    string[] ab = GRNvalue[1].ToString().Split('=');
    //    Amount = Convert.ToDouble(ab[1].ToString());
    //}
    ////string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
    ////                             GRN.ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Amount.ToString(), "S", "HDFC", "201908971735173", "DC", "SBIIB");

    //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
    //                             GRN.ToString(), "6390013", "ICICI12345", "036005714654654000010", DateTime.Now, Amount.ToString(), "");

    //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
    //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
    //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
    //RemoteClass myremotepost = new RemoteClass();
    //plainText = plainText + "|checkSum=" + CheckSum;
    ////string cipherText = objEncry.Encrypt(plainText, Server.MapPath("../WebPages/Key/RAJASTHAN_EGRASS.key"));
    //string cipherText1 = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "ICICI.key");

    //return response;
    // }

    public override Dictionary<string, string> GetBankForwardString()
    {
        //string plainText = string.Format("GRN={0}|HEAD_OF_ACC={1}|AMT={2}|REMITTER_NAME={3}|UDF1={4}|Filler={5}", GRN.ToString(), Head_Name[0], Head_Amount[0], RemitterName, BsrCode.ToString(), filler);
        string plainText = string.Format("GRN={0}|HEAD_OF_ACC={1}|AMT={2}|REMITTER_NAME={3}|TOTALAMOUNT={4}|PayMode={5}|REG-TIN-NO={6}|LocationCode={7}|Filler={8}", GRN.ToString(), Head_Name[0].Substring(0, 4), Head_Amount[0], RemitterName, TotalAmount.ToString(), PaymentMode, TIN, LocationCode, filler);
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        checkSum = objEncryption.GetSHA256(plainText);
        plainText = plainText + "|checksum=" + checkSum;
        string EncData = BanksEncryptionDecryption.GetEncryptedString(plainText, KeyName, Version);
        return GetforwardDictionary(EncData);


    }

    public override string GetRequestString(string plainText)
    {
        return BanksEncryptionDecryption.GetEncryptedString(plainText + "|checkSum=" + checkSum, KeyName, Version);
    }
}