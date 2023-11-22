using EgBL;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

using System.Web.Script.Serialization;
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgSNABankDataPush" in code, svc and config file together.
public class EgSNABankDataPush : IEgSNABankDataPush
{
    public string PushSnaData(string RequestParm)
    {
        try
        {
            EgSNABankServiceBL objEgSnaService = new EgSNABankServiceBL();
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];//header =>SHA256 Encrypted( UID(AU)+Password(Egras@123)+CreateTime(CreatedDate 2021/05/31 11:53:35.460))|Merchant Key with MerchantKey 
            return objEgSnaService.ProcessData();
        }
        catch (Exception ex)
        {
            return new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = "035", Message = ex.Message.ToString() });

        }
    }
}
