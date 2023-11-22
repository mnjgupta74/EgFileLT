using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace EgBL
{


    public class EgSendMessage
    {
        GenralFunction gf;
        DataTable UserInfo;
        static int Count = 0;
        public bool send(string uid, string password, string message, string no, string SENDERID)
        {
           
            string dlt_entity = "1001524671154484790";
            string dlt_tempate_id = "1007456740740545238";
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest myReq =
            (System.Net.HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + message + "&mnumber=" + no + "&signature=" + SENDERID + "&dlt_entity_id=" + dlt_entity + "&dlt_template_id=" + dlt_tempate_id);
            //(System.Net.HttpWebRequest)WebRequest.Create("https://smsgw.sms.gov.in/failsafe/HttpLink?username=" + uid + "&pin=" + password + "&message=" + message + "&mnumber=" + no + "&signature=" + SENDERID);

            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            string responseString = string.Empty;
            using (System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream()))
            {
                responseString = respStreamReader.ReadToEnd();// "Message Accepted for Request ID=12313946898506681731361~code=API000 & info=Platform accepted & Time =2014/03/13/11/20";//
                respStreamReader.Close();
            }
            myResp.Close();
            if (responseString.ToString().Contains("API000") == true)
            {
                return true;
            }
            else
                return false;
            //return responseString.ToString();
        }
        public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public TrustAllCertificatePolicy() { }
            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }


        /// <summary>
        /// get  Grn from  Table 
        /// </summary>
        /// <returns></returns>
        public void GetPendingGrn()
        {
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            UserInfo = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[0];
            gf.Filldatatablevalue(null, "EgGetPendingGrn", UserInfo, null);
            foreach (DataRow row in UserInfo.Rows)
            {
                int i = 0;
                Int64 grn = Convert.ToInt64(row[i].ToString());
                string MobileNumber = row[i + 1].ToString();
                double Amount = Convert.ToDouble(row[i + 2].ToString());
                string Status = row[i + 3].ToString();
                bool a = send("egras.auth", "Jh*$23et", "GRnNUmber" + '-' + grn + "Amount" + '-' + Amount + '-' + "Status" + '-' + Status, "91" + MobileNumber.ToString(), "EGRASJ");
                Count++;
            }
            BulkSMSCount();
        }

        public void BulkSMSCount()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@Count", SqlDbType.BigInt) { Value = Count };
            gf.UpdateData(PM, "BulkSMSCountFinYearWise");

        }

        public void GetGrnSmsDetail(Int64 Grn)
        {
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            UserInfo = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            gf.Filldatatablevalue(PM, "EgGRnSmsDetail", UserInfo, null);
            foreach (DataRow row in UserInfo.Rows)
            {
                int i = 0;
                Int64 grn = Convert.ToInt64(row[i].ToString());
                string MobileNumber = row[i + 1].ToString();
                double Amount = Convert.ToDouble(row[i + 2].ToString());
                string Status = row[i + 3].ToString();
                //string TransDate =(row[i + 5].ToString());
                string CIN = row[i + 6].ToString();
                string time = row[i + 5].ToString().Substring(11, 9);//TransDate.Hour.ToString() + ":" + TransDate.Minute.ToString() + ":" + TransDate.Second.ToString(); 
                string date = row[i + 5].ToString().Substring(0, 11);
                bool a = send("egras.auth", "Jh*$23et", ConfigurationManager.AppSettings["SMSMessageFormat"] + " Rs. " + Amount + " was successful at " + time + " on " + date + ". GRN: " + grn + " CIN: " + CIN, "91" + MobileNumber.ToString(), "EGRASJ");
                //bool a = send("egras.auth", "Jh*$23et", "GRnNUmber" + '-' + grn + "Amount" + '-' + Amount + '-' + "Status" + '-' + Status, "91" + MobileNumber.ToString(), "EGRASJ");
                //Count = Convert.ToInt32(Amount);
            }
            //BulkSMSCount();

        }
    }


}


