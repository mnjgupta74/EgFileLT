using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgTokenService" in code, svc and config file together.
public class EgAuthTokenAPI : IEgAuthTokenAPI
{
    public System.IO.Stream GetToken(string tokenEnc)
    {
        string IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        try
        {
            EgTokenServiceBL objToken = new EgTokenServiceBL();
            var merchantCode = System.Web.HttpContext.Current.Request.Headers["Merchant_Code"];
            objToken.merchantcode = merchantCode;
            objToken.IPAddress = IPAddress;
            objToken.encdata = tokenEnc;

            var Token = objToken.VeryfyTokenData();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(Token));
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("EgAuthTokenAPI IPAddress= " + IPAddress + " and tokenEnc=" + tokenEnc + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }
}
