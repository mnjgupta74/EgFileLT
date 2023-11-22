using System.ServiceModel;
using System.ServiceModel.Web;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISnaService" in both code and config file together.
[ServiceContract]
public interface IEgSNADeptData
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "PushSnaData")]
    string PushSnaData(string RequestParm);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "PullData")]
    string PullData(string RequestParm);
}
