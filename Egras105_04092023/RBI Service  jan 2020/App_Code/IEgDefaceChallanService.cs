using System.ServiceModel;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgDefaceChallanService" in both code and config file together.
[ServiceContract]
public interface IEgDefaceChallanService
{
    [OperationContract]
    [WebInvoke(Method = "POST",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "Deface")]
    string DefaceChallan(string Parameter);



    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Verify")]
    string VerifyReferenceNo(string Parameter);
}
