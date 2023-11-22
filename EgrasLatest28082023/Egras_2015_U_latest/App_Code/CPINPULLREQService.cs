using EgBL;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using EgBL;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Configuration;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CPINPULLREQService" in code, svc and config file together.
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class CPINPULLREQService : ICPINPULLREQService
{
    public System.IO.Stream CPINPullRequest(CPINPullRequest CPINString)
    {
        string IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        try
        {
            egNeftRtgsBL objegNeftRtgsBL = new egNeftRtgsBL();
            //CPINString ="{ \"System\":\"GOBH\",\"ServiceType\":\"CPINPULLREQ\",\"Signature\":\"DnVKt+NM+w/LCFUh89hLp1NxIIIvgyTyaBt/POwwMniBMCmMKbrh8HYScRpTBhc6j1Yx1vVv6R5tUawbEk5Ythsh5UuN1KgyWUd6Wb2oVEuHpg4V+KUZFzKGJsCQTUZOkzc+frAMnus+qZpTOWhFRsqIMQVA+ShFkhKLZGDfDep/pE1e+6XOUtz5/Y4aXVIbbZU07C4rj/fPskgS7JolYxWYDz6s457dhg23p/UDsb/zu7fuDQrlAwNa9l5dteZgKRqKm1jXhLa4LN+KHeFjLR3Dy5vY2eemUmmnVoVAENhI49WsmaCL1yAzw/tHygZs9b1fWevisOWZloEvAWAhRA==\",\"Payload\":{ \"CPIN\":\"7160862\"} }";
            //CPINString = "{ \"System\":\"GOBH\",\"ServiceType\":\"CPINPULLREQ\",\"Signature\":\"X2O6tLFm3HReZUFDXv/97TfNYqBhTs+/AA/RwK78XX0nr9aGN5GP4K8Mn1Trf69O1HMN6PYyfhVOKtyKxNyWlYrWaovP7PXXnC65ZfHd9FN1SRFbFZRfGbn4fzO1l/4Q9Aj+s/iVYwiThCZDRRkuhZ6jqI6hA4RgmUmwYaW0vDSQNf/LtLLbk6zahc/t0Ivd86pu4cPq1efgmzA1KKo7rRIcfMpy3NAGlBsluxLTXluD8MlZ5c3hugt/3VNTMi4fI60a0QiMAUjvu5WRwl0323tXoVz3do+ym6epKFFP7vYMhYdLoF2+y+A+GSE4nG7b93FO+zqFapdfD0kDOLEBlw==\",\"Payload\":{ \"CPIN\":\"7160858\",\"RESP_CODE\":\"\",\"RJCT_DESC\":\"\"} }";
            //CPINString = "{\"System\":\"GOBH\",\"ServiceType\":\"CPINPULLREQ\",\"Signature\":\"BXJDBCHDBHD\",\"Payload\":[{\"CPIN\":\"1234\"}]}";
            string jsonStr = string.Empty;
            objegNeftRtgsBL.IPAddress = IPAddress;
            jsonStr = objegNeftRtgsBL.CPINPULLREQ(CPINString);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + CPINString.Payload.CPIN + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }

    //public string CPINNotificationRequest(CPINNotificationRequest NotifyDetail)
    public System.IO.Stream CPINNotificationRequest(CPINNotificationRequestr NotifyDetail)
    {
        string IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        try
        {
            
            egNeftRtgsBL objegNeftRtgsBL = new egNeftRtgsBL();

            //CPINString = "{\"System\":\"GOBH\",\"ServiceType\":\"CPINPUSHRES\",\"Signature\":\"BXJDBCHDBHD\",\"Payload\":[{\"CPIN\":\"1234\"}]}";
            //NotifDetail = "{\"Payload\":{ \"CPIN\":\"7355447\",\"TotalAmt\":\"11\",\"UTR\":\"26122022072701\",\"BankRefNo\":\"27072022170521015450\",\"PaymentDtTm\":\"20220727170521\"},\"ServiceType\":\"CPINNTFNREQ\",\"System\":\"GORJ\",\"Signature\":\"cOIHRPVrbfrDrJLjN700vtq8h2Sr7AprIthclj+nxRgfTc+1P/vW01H4yp9Ve/8qtWzZCesFPWUcJpqx0khyqZ9BlyVhvLgIE5aKcoYT8KoUoG6es5h70SNGQwjx8O6oFWCAlywZzIeM+zLgwl6AVl5a0vKJjzGXtRYanhgme7JlPT1FOfauDo9RzZXe95k2XJdFmvFgQYxxhvMQvpTW6ItjmHUcPUkT8u60cO6j8NpGsLvKSgabdeNAknkFmJk9qNpJEEIGSoSWrAMYpl/vuaIoxGgDvs80okUl64tuuM0Mo+R+LmB9B3V2t78Xmm5G/aGVEIBtH21/UJXpW8Uz+w==\"}";
            //string jsonStr = string.Empty;
            //var jsonStr = JsonConvert.SerializeObject(objegNeftRtgsBL.CPINNotifyReq(NotifyDetail));
            objegNeftRtgsBL.IPAddress = IPAddress;
            var jsonStr = objegNeftRtgsBL.CPINNotifyReq(NotifyDetail);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + NotifyDetail.Payload.CPIN + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }
    public System.IO.Stream CPINReturnNotificationRequest(CPINReturnNotificationRequest ReturnNotifyDetail)
    {
        string IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        try
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("CPINReturnNotificationRequest String : - Payload={ cpin=" + ReturnNotifyDetail.Payload.CPIN + ", UTR=" + ReturnNotifyDetail.Payload.UTR + ", Total Amount=" + ReturnNotifyDetail.Payload.TotalAmt + ",RequestDtTm=" + ReturnNotifyDetail.Payload.RequestDtTm + ", BankRefrenceNo =" + ReturnNotifyDetail.Payload.BankRefNo + ", RTN_CODE =" + ReturnNotifyDetail.Payload.RTN_CODE + ", RTN_DESC =" + ReturnNotifyDetail.Payload.RTN_DESC + "}, System=" + ReturnNotifyDetail.System + ",ServiceType=" + ReturnNotifyDetail.ServiceType + " Signature=" + ReturnNotifyDetail.Signature + " , IPAddress =" + IPAddress);

            egNeftRtgsBL objegNeftRtgsBL = new egNeftRtgsBL();

            string jsonStr = string.Empty;
            objegNeftRtgsBL.IPAddress = IPAddress;
            jsonStr = objegNeftRtgsBL.CPINReturnNotificationReq(ReturnNotifyDetail);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + ReturnNotifyDetail.Payload.CPIN + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }




    //[DataContract]
    //public class CPINNotificationRequestr
    //{
    //    [DataMember]
    //    public string System { get; set; }

    //    [DataMember]
    //    public string ServiceType { get; set; }

    //    [DataMember]
    //    public string Signature { get; set; }

    //    [DataMember]
    //    public RequestPayloadr Payload { get; set; }
    //}

    //[DataContract]
    //public class RequestPayloadr
    //{
    //    [DataMember]
    //    public string CPIN { get; set; }

    //    [DataMember]
    //    public string UTR { get; set; }

    //    [DataMember]
    //    public string TotalAmt { get; set; }

    //    [DataMember]
    //    public string paymentDtTm { get; set; }

    //    [DataMember]
    //    public string BankRefNo { get; set; }
    //}

}
