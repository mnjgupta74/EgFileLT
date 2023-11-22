using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_ABC1 : System.Web.UI.Page
{
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       // string a = TransactionStatusAPI();
       // System.Threading.Thread.Sleep(10000);


        //TransactionStatusAPI123();

        //lblText.Text = "Processing completed";
        btnInvoke_Click(btnInvoke, null);
        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Submit1_onclick();</script>", false);
        //for (int i = 0; i < 10000; i++)
        //{
        //    if (i == 9999)
        //    {

        //       // btn1_Click(btn1, null);
        //    }
        //    else
        //    {
        // ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ShowProgressBar(); ", true);
        // }


        //  }






    }

   

    protected void btnInvoke_Click(object sender, EventArgs e)
    {
        
        string a= TransactionStatusAPI();
        //System.Threading.Thread.Sleep(50000);
       

        //TransactionStatusAPI123();
        
        lblText.Text = "Processing completed";
    }

    public void TransactionStatusAPI123(string plainText)
    {


        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:ShowProgressBar(); ", true);
        PayTmBL PBL = new PayTmBL();
        try
        {
            //plainText = "GRN=7335057|BANK_CODE=null|BankReferenceNo=20220615111212800110168989303787858|CIN=null|PAID_DATE=2022/06/15 05:56:43|PAID_AMT=22.00|TRANS_STATUS=S|DebitBankCode=null|BankRefNo=5210039|PayMode=UPI|Reason=Txn Success|checksum=ac14ca3914ea9bca2e2a49303a1db49c";
            //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
            //                             Session["GrnNumber"].ToString(), "9930001", "PayTmUPI12345", "993000112345605012022", DateTime.Now, Session["NetAmount"].ToString(), "S", "PAYTMUPI", "201908971735173", "DC", "SBIIB");
            EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            plainText = plainText + "|checkSum=" + CheckSum;
            string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
            //string Address = "http://localhost:56933/WebPages/BankResponseReceived.aspx";
            string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
            myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
            myremotepost.Add("BankCode", "9930001");
            myremotepost.Add("URL", Address);
            myremotepost.Url = Address;
            myremotepost.Post();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            //return "Due to technical error, We could not process the request !";
        }
        ////string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
        //                                   //Session["GrnNumber"].ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Session["NetAmount"].ToString(), "S", "HDFC", "201908971735173", "DC", "SBIIB");
        ////string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
        ////Session["GrnNumber"].ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Session["NetAmount"].ToString(), "S", "HDFC", "201908971735173", "DC", "SBIIB");
        ////string plainText = "abc";
        //EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        //string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //RemoteClass myremotepost = new RemoteClass();
        //plainText = plainText + "|checkSum=" + CheckSum;
        //string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<form method = \"POST\" action = \"https://securegw-stage.paytm.in/theia/customProcessTransaction\" name = \"submitpayment\" >");
        //sb.Append("<input type = \"hidden\" value = \"" + Request.Form["cipherText"] + "\" name = \"encdata\" placeholder = \"Enter Data here\" />   < br >");
        //sb.Append("<input type = \"hidden\" value = \"IFMSRA96708754236819\" name = \"MID\" /> < br >");
        //sb.Append("<input type = \"submit\" name = \"raj_submit\" value = \"Pay Now\" />");
        //sb.Append("</form>");

        //sb.Append("<script language='javascript'>");
        //sb.Append("var vsubmitpayment = document.submitpayment;");
        //sb.Append("vsubmitpayment.submit();");
        //sb.Append("</script>");


        //PayTmBL PBL = new PayTmBL();
        //try
        //{
        //    string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
        //                                 Session["GrnNumber"].ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Session["NetAmount"].ToString(), "S", "HDFC", "201908971735173", "DC", "SBIIB");
        //    EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        //    string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        //    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        //    RemoteClass myremotepost = new RemoteClass();
        //    plainText = plainText + "|checkSum=" + CheckSum;
        //    string cipherText = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
        //    // string Address = "http://10.130.34.152/Egras105/default.aspx";
        //    Response.Redirect("http://10.130.34.152/Egras105/default.aspx");
        //    //string Address = ConfigurationManager.AppSettings["SuccessURL"].ToString();// "http://164.100.153.101/EgrasSECTEST/WebPages/BankResponseReceived.aspx";
        //    //myremotepost.Add("encdata", cipherText); //"2SpgkitxtLzbKbS0QgkZQv1SGtDsf/8QxQdiU6EYkU9aH+g1b1gmNG2lrqoaAIb3YWZWwygtBE6PS1G9agB8Qy/uRzEFrOMmrNeoLQXVjZE=");
        //    //myremotepost.Add("BankCode", "9930001");
        //    //myremotepost.Add("URL", Address);
        //    //myremotepost.Url = Address;
        //    //myremotepost.Post();
        //}

        //    i++;
        //    Dictionary<string, string> data_raw = new Dictionary<string, string>();

        //    data_raw.Add("MID", "IFMSRA73364897578748");
        //    //data_raw.Add("ORDERID", "" + Session["GrnNumber"].ToString() + "");
        //    data_raw.Add("ORDERID", "" + Convert.ToInt64(Session["GrnNumber"].ToString()) + "");
        //    data_raw.Add("checksum", PBL.GetMD5Hash("MID=IFMSRA73364897578748|ORDERID=" + Convert.ToInt64(Session["GrnNumber"].ToString())));
        //    string post_data = JsonConvert.SerializeObject(data_raw);

        //    //For  Staging
        //    string url = "https://securegw-stage.paytm.in/merchant-status/HANDLER_INTERNAL/TXNSTATUS?MID=IFMSRA73364897578748";


        //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

        //    webRequest.Method = "POST";
        //    webRequest.ContentType = "application/json";
        //    webRequest.ContentLength = post_data.Length;

        //    using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
        //    {
        //        requestWriter.Write(post_data);
        //    }

        //    string responseData = string.Empty;

        //    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
        //    {
        //        responseData = responseReader.ReadToEnd();

        //        PBL.InsertLog("TransactionStatusAPI: " + responseData);
        //        var STATUS = JsonConvert.DeserializeObject<PayTmTransactionStauts>(responseData);
        //        var resultStatus = STATUS.STATUS;
        //        //responseData = resultStatus.ToUpper() == "PENDING" ? "P" : "S";
        //        responseData = "P";
        //        if (responseData == "P")
        //        {

        //            while (i <= 3)
        //            {
        //                Thread.Sleep(5000);
        //                TransactionStatusAPI();
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("ABC1.aspx");
        //            //return responseData;
        //        }
        //    }
        //    Response.Redirect("ABC1.aspx");
        //    //return responseData;
        //}

        //}
        //    catch (Exception ex)
        //    {
        //        EgErrorHandller obj = new EgErrorHandller();
        //        obj.InsertError(ex.Message.ToString());
        //        //return "Due to technical error, We could not process the request !";
        //    }
    }
    public string TransactionStatusAPI()
    {
        count += 1;
        try
        {
            Dictionary<string, string> data_raw = new Dictionary<string, string>();

            data_raw.Add("MID", "IFMSRA96708754236819");
            //data_raw.Add("ORDERID", "" + Session["GrnNumber"].ToString() + "");
            data_raw.Add("ORDERID", "" + Session["GrnNumber"].ToString() + "");
            data_raw.Add("checksum", GetMD5Hash("MID=IFMSRA96708754236819|ORDERID=" + Session["GrnNumber"].ToString()));
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
               // InsertLog("TransactionStatusAPI: " + responseData);
                var STATUS = JsonConvert.DeserializeObject<PayTmTransactionStauts>(responseData);
                //var resultStatus = STATUS.STATUS;
                var resultStatus = STATUS.encData;
               // responseData = resultStatus.ToUpper() == "PENDING" ? "P" : "S";

                SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
                string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(resultStatus, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PayTMKey.key", null);
                List<string> GRNvalue = plaintext.Split('|').ToList();

                responseData = GRNvalue[6].Split('=')[1].ToUpper().Trim();
                if (responseData == "P" && count < 5)
                {
                    System.Threading.Thread.Sleep(1 * 10000);
                    TransactionStatusAPI();
                }
                else
                {
                    TransactionStatusAPI123(plaintext);
                }

                //if (responseData == "P")
                //    TransactionStatusAPI();
                //else
                //    TransactionStatusAPI123();
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
//    public int InsertLog(string response)
//    {
//        GenralFunction gf = new GenralFunction();
//        SqlParameter[] PM = new SqlParameter[2];
//        PM[0] = new SqlParameter("@response", SqlDbType.VarChar, 500) { Value = response };
//        PM[1] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = GRN };
//        return gf.UpdateData(PM, "EGPayTmUPILogSp");
    
//}
}