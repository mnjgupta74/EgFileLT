using EgBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
/// <summary>
/// Summary description for Emitra
/// </summary>
public class Emitra : Banks
{
    private string BankCode;
    public Emitra()
    {
        KeyName = "Emitra";
        BankCode = "9970001";
        Version = "2.0";
    }

    public override string CallManualDataPushService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyManualService(string cipherText)
    {
        try
        {
            var response = string.Empty;
            // string response;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                          
            string URI = "https://emitraapp.rajasthan.gov.in/webServicesRepository/verifyEmitraTransactionForEgras";

            ////string a1 = "jRcIDsbu+OfnOGhomYGMjnK7lo3KslmXUygN9vU0sBo18nmGPeInHUzv0cJgemhp12UzzSNCoKwZh4gzR22dSWcupCfLXhH1RKsjw/5llh9w9aHxpDox90Iyim6mjYoFp3fd4g==";
            //string myParameters = "encdata=" + cipherText.Replace("+", "%2B") + "";
            //string myParameters = "encData=" + cipherText + "";
            //string aa = "LykjI3X7lLeWVz9KJcLxYHpJM5NeFRzCFyjkmI6hR6bDjw2Ua9mEM/qvzv1xWK3eozvJdn0WCL79dBuBD6XOJPrkbmr1r3OkyWanWj+lvXNvcksP59hYT/fbVuHI3w7eK8KmemW/MBhRE4NoVrTMAwXOUO1BkonT43hhG8trJJk=";
            //string myParameters = "encdata=" + aa + "";

            //string myParameters = "encdata=" + a1 + "";
            //string beforesend = myParameters;
            using (WebClient wc = new WebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("encData", cipherText);

                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                byte[] responsebytes = wc.UploadValues(URI, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                return responsebody;
                //response = wc.UploadString(URI, "POST", reqparm);
                //return response;
                //string aa = "LykjI3X7lLeWVz9KJcLxYHpJM5NeFRzCFyjkmI6hR6bDjw2Ua9mEM%2Fqvzv1xWK3eozvJdn0WCL79dBuBD6XOJPrkbmr1r3OkyWanWj%2BlvXNvcksP59hYT%2FfbVuHI3w7eK8KmemW%2FMBhRE4NoVrTMAwXOUO1BkonT43hhG8trJJk%3D";
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Due to technical error, We could not process the request !";
        }

    }

    public override string CallVerifyService(string cipherText)
    {
        throw new NotImplementedException();
    }
    public override string GetRequestString(string plainText)
    {
        return BanksEncryptionDecryption.GetEncryptedString(plainText + "|checkSum=" + checkSum, KeyName, Version);
    }

}