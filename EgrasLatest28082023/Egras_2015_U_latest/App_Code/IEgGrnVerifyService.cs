using System.ServiceModel;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgGrnVerifyService" in both code and config file together.
[ServiceContract]
public interface IEgGrnVerifyService
{
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GrnVerify")]
    void GrnVerify();


}
