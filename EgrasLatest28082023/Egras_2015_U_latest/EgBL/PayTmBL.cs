using Newtonsoft.Json;
using Paytm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EgBL
{
    public class PayTmBL
    {
        public string NetAmount { get; set; }
        public string UserId { get; set; }
        public string[] BudgetHead { get; set; }
        public string GRN { get; set; }
        public string UpiId { get; set; }

        public string paytmChecksum { get; set; }
       
        
        public string InitiateTransactionAPI()
        {
            try
            {
               
               
                Dictionary<string, object> body = new Dictionary<string, object>();
                Dictionary<string, string> head = new Dictionary<string, string>();
                Dictionary<string, object> requestBody = new Dictionary<string, object>();
                Dictionary<string, string> txnAmount = new Dictionary<string, string>();

                txnAmount.Add("value", NetAmount);
                txnAmount.Add("currency", "INR");

                Dictionary<string, string> userInfo = new Dictionary<string, string>();
                Dictionary<string, string> extendInfo = new Dictionary<string, string>();
                userInfo.Add("custId", UserId);
                //ExtendedInfo.Add("udf1", ((string[])(BudgetHead))[0].ToString());
                extendInfo.Add("headAccount", ((string[])(BudgetHead))[0].ToString());
                extendInfo.Add("remitterName", "Egras");
                body.Add("requestType", "Payment");
                //body.Add("mid", "IFMSRA73364897578748");
                body.Add("mid", "IFMSRA96708754236819");
                body.Add("websiteName", "WEBSTAGING");
                body.Add("orderId", GRN);
                body.Add("txnAmount", txnAmount);
                body.Add("userInfo", userInfo);
                body.Add("extendInfo", extendInfo);
                body.Add("callbackUrl", "https://merchant.com/callback");

                /*
                * Generate checksum by parameters we have in body
                * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
                */
                //string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "549BJn4fTp8uKpPm");
                //string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "ux0WrnO4#YT#@vYF");
                //paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "ux0WrnO4#YT#@vYF");
                paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "549BJn4fTp8uKpPm");

                head.Add("signature", paytmChecksum);

                requestBody.Add("body", body);
                requestBody.Add("head", head);

                string post_data = JsonConvert.SerializeObject(requestBody);

                //For  Staging
                string url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=IFMSRA96708754236819&orderId=" + GRN;

                //For  Production 
                //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = post_data.Length;

                //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(post_data);
                }

                string responseData = string.Empty;
                string validateResponseData = string.Empty;

                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    responseData = responseReader.ReadToEnd();
                    InsertLog("InitiateTransactionAPI : " + responseData);
                    var key = JsonConvert.DeserializeObject<PayTm>(responseData);
                    var resultStatus = key.body.resultInfo.resultStatus;
                    if (resultStatus.ToUpper() == "S")
                    {
                        paytmChecksum = key.body.txnToken;
                        validateResponseData = ValidateVPAAPI(key.body.txnToken);
                    }
                    else
                    {
                        validateResponseData = "Service Is Currently Down.";
                    }
                }
                return validateResponseData;
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                return "Due to technical error, We could not process the request !";
            }
        }
        public string ValidateVPAAPI(string paytmChecksum)
        {
            try
            {
                Dictionary<string, string> body = new Dictionary<string, string>();
                Dictionary<string, string> head = new Dictionary<string, string>();
                Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

                //body.Add("vpa", "7777777777@paytm");
                //body.Add("vpa", txtUpi.Text);
                body.Add("vpa", UpiId);

                head.Add("tokenType", "TXN_TOKEN");
                head.Add("token", paytmChecksum);


                requestBody.Add("body", body);
                requestBody.Add("head", head);

                string post_data = JsonConvert.SerializeObject(requestBody);

                //For  Staging
                string url = "https://securegw-stage.paytm.in/theia/api/v1/vpa/validate?mid=IFMSRA96708754236819&orderId=" + GRN;

                //For  Production 
                //string  url  =  "https://securegw.paytm.in/theia/api/v1/vpa/validate?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

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
                    InsertLog("ValidateVPAAPI : " + responseData);
                    var key = JsonConvert.DeserializeObject<PayTm>(responseData);
                    var resultStatus = key.body.resultInfo.resultStatus;
                    responseData = resultStatus.ToUpper() == "S" ? ProcessTransactionAPI(paytmChecksum) : "Sorry ! Could not verify the request.";
                }

                return responseData;
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                return "Due to technical error, We could not process the request !";

            }
        }


        public string ProcessTransactionAPI(string paytmChecksum)
        {
            try
            {
                Dictionary<string, string> body = new Dictionary<string, string>();
                Dictionary<string, string> head = new Dictionary<string, string>();
                Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

                body.Add("requestType", "NATIVE");
                body.Add("mid", "IFMSRA96708754236819");
                //body.Add("orderId", Session["GrnNumber"].ToString());
                body.Add("orderId", GRN);
                //body.Add("paymentMode", "UPI");
                body.Add("paymentMode", "UPI");
                //body.Add("channelCode", "collect");
                //body.Add("payerAccount", "7777777777@paytm");
                //body.Add("payerAccount", txtUpi.Text);
                body.Add("payerAccount", UpiId);

                head.Add("txnToken", paytmChecksum);


                requestBody.Add("body", body);
                requestBody.Add("head", head);

                string post_data = JsonConvert.SerializeObject(requestBody);

                //For  Staging
                string url = "https://securegw-stage.paytm.in/theia/api/v1/processTransaction?mid=IFMSRA96708754236819&orderId=" + GRN;

                //For  Production 
                //string  url  =  "https://securegw.paytm.in/theia/api/v1/processTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = post_data.Length;

                using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(post_data);
                }

                string responseData = string.Empty;
                string transResponseData = string.Empty;

                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    responseData = responseReader.ReadToEnd();
                    InsertLog("ProcessTransactionAPI : " + responseData);
                    var key = JsonConvert.DeserializeObject<PayTm>(responseData);
                    var resultStatus = key.body.resultInfo.resultStatus;
                    transResponseData = resultStatus.ToUpper() == "S" ? "S" : "Sorry ! Could not process the data.";

                }

                return transResponseData;
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                return "Due to technical error, We could not process the request !";
            }
        }
        int count = 0;
        public string TransactionStatusAPI()
        {
          
            count += 1;
            try
            {
                Dictionary<string, string> data_raw = new Dictionary<string, string>();

                data_raw.Add("MID", "IFMSRA96708754236819");
                //data_raw.Add("ORDERID", "" + Session["GrnNumber"].ToString() + "");
                data_raw.Add("ORDERID", "" + GRN + "");
                data_raw.Add("checksum", GetMD5Hash("MID=IFMSRA96708754236819|ORDERID=" + GRN));
                string post_data = JsonConvert.SerializeObject(data_raw);

                //For  Staging
                string url = "https://securegw-stage.paytm.in/merchant-status/HANDLER_INTERNAL/TXNSTATUS?MID=IFMSRA96708754236819";


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
                    InsertLog("TransactionStatusAPI: " + responseData);
                    var STATUS = JsonConvert.DeserializeObject<PayTmTransactionStauts>(responseData);
                    //var resultStatus = STATUS.STATUS;
                    //var resultStatus = STATUS.encData;
                    //responseData = resultStatus.ToUpper() == "PENDING" ? "P" : "S";
                    //responseData = resultStatus.ToUpper() == "PENDING" ? "P" : "S";
                    var resultStatus = STATUS.encData;
                    SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
                    string plaintext = ObjEncrytDecrypt.DecryptAES256(resultStatus, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
                    List<string> GRNvalue = plaintext.Split('|').ToList();
                    responseData = GRNvalue[6].Split('=')[1].ToUpper().Trim();
                    if (responseData == "P" && count < 5)
                    {
                        System.Threading.Thread.Sleep(1 * 10000);
                        TransactionStatusAPI();
                    }
                }
                return responseData;
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                return "Due to technical error, We could not process the request !";
            }
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

        public int InsertLog(string response)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@response", SqlDbType.VarChar, 500) { Value = response };
            PM[1] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = GRN };
            return gf.UpdateData(PM, "EGPayTmUPILogSp");
        }
    }

    public class PayTm
    {
        public head head { get; set; }
        public body body { get; set; }
    }
    public class head
    {
        public string responseTimestamp { get; set; }
        public string version { get; set; }
        public string signature { get; set; }
    }
    public class body
    {
        public resultInfo resultInfo { get; set; }
        public string txnToken { get; set; }
        public string isPromoCodeValid { get; set; }
        public string authenticated { get; set; }
    }
    public class resultInfo
    {
        public string resultStatus { get; set; }
        public string resultCode { get; set; }
        public string resultMsg { get; set; }
    }
    public class PayTmTransactionStauts
    {
        //public string TXNID { get; set; }
        //public string BANKTXNID { get; set; }
        //public string ORDERID { get; set; }
        //public string TXNAMOUNT { get; set; }
        //public string STATUS { get; set; }
        //public string TXNTYPE { get; set; }
        //public string RESPCODE { get; set; }
        //public string RESPMSG { get; set; }
        //public string MID { get; set; }
        //public string REFUNDAMT { get; set; }
        //public string TXNDATE { get; set; }
        public string encData { get; set; }
    }
}