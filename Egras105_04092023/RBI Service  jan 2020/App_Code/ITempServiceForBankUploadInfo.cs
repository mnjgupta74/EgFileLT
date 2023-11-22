using System.ServiceModel;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITempServiceForBankUploadIngo" in both code and config file together.
[ServiceContract]
public interface ITempServiceForBankUploadInfo
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "ProcessGRN")]
    string ProcessGRN(string bsrcode);
}
