using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Configuration;

namespace EgBL
{
    class AuthorizationHeaderClass : ServiceAuthorizationManager
    {
        SbiEncryptionDecryption objDecrypt;
        AndroidAppBL objAndroidAppBL;
        AppEncryptionDecryption objAppEncDec;
        string Key = "fd876698-89a7-46f5-bf5a-70a64f65d323";
            /// <summary>  

            /// </summary>  
            protected override bool CheckAccessCore(OperationContext operationContext)
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                objAndroidAppBL = new AndroidAppBL();
                if ((authHeader != null) && (authHeader != string.Empty))
                {
                    objAppEncDec = new AppEncryptionDecryption();
                    var svcCredentials = objAppEncDec.DecryptText(System.Text.ASCIIEncoding.ASCII
                        .GetString(Convert.FromBase64String(authHeader.Substring(6))), Key)
                        .Split(':');
                    objDecrypt = new SbiEncryptionDecryption();
                    objAndroidAppBL.UserID = objDecrypt.DecryptString(svcCredentials[0], ConfigurationManager.AppSettings["AppKey"].ToString());
                    objAndroidAppBL.Mpin = svcCredentials[1].Substring(10);
                    objAndroidAppBL.RND = svcCredentials[1].Substring(0, 10);
                    string result = objAndroidAppBL.CheckAuthorizationHeader();
                    if (result.Trim() == "1")
                    {
                        //User is authrized and originating call will proceed  
                        return true;
                    }
                    else
                    {
                        throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    throw new WebFaultException(System.Net.HttpStatusCode.Unauthorized);
                }
        }  
    }
}
