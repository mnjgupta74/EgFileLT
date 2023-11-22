using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Security.Cryptography.X509Certificates;
/// <summary>
/// Summary description for NEFTRTGS
/// </summary>
public class NEFTRTGS
{

    public string CallVerifyService( Int64 grn)
    {
        ServicePointManager.ServerCertificateValidationCallback +=
     (sender, cert, chain, sslPolicyErrors) => { return true; };
        ServicePointManager.Expect100Continue = true;

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        egNeftRtgsBL objegNeftRtgsBL = new egNeftRtgsBL();
        objegNeftRtgsBL.CPIN = grn.ToString();
        //objegNeftRtgsBL.certificatePath = Server.MapPath(ConfigurationManager.AppSettings["SecureCertificate"].ToString());
        //new System.Security.Cryptography.X509Certificates.X509Certificate2( + bankcode + ".cer.txt");
        string JSONString = objegNeftRtgsBL.CPINPUSHREQ(grn);
        string jsonResponse = string.Empty;
        using (WebClient webClient1 = new WebClient())
        {
            var url = System.Web.Configuration.WebConfigurationManager.AppSettings["NEFTRTGS"];
            webClient1.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            webClient1.Headers[HttpRequestHeader.ContentType] = "application/json";
           
            //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            jsonResponse = webClient1.UploadString(url, "POST", JSONString);
        }

        string insertFlag = objegNeftRtgsBL.CPINPUSHRES(jsonResponse);

        return insertFlag;
    }

    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy() { }
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert,
            WebRequest req,
            int problem)
        {
            return true;
        }
    }
}