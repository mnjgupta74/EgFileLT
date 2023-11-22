using System;
using System.ServiceModel;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISSPIntegration" in both code and config file together.
[ServiceContract]
public interface ISSPIntegration
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,RequestFormat =WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GenerateGRN")]
    string GenerateGRN(string RequestParm);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GenerateRevenueGrn")]
    string GenerateRevenueGrn(string RequestParm, String auinno);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GeneratePDGrn")]
    string GeneratePDGrn(string RequestParm, String auinno);
}
