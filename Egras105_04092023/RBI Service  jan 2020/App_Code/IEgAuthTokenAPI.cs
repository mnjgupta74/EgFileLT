using System.ServiceModel;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgTokenService" in both code and config file together.
[ServiceContract]
public interface IEgAuthTokenAPI
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetToken")]

    System.IO.Stream GetToken(string PassCode);
}
