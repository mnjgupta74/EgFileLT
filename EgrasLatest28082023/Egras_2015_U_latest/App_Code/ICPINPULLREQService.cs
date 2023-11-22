using EgBL;
using System.ServiceModel;
using System.ServiceModel.Web;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICPINPULLREQService" in both code and config file together.
[ServiceContract]
public interface ICPINPULLREQService
{
    [OperationContract]
    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "CPINPullRequest")]
    System.IO.Stream CPINPullRequest(CPINPullRequest GRNDetail);

    [OperationContract]
    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "CPINNotificationRequest")]
    //string CPINNotificationRequest(CPINNotificationRequest GRNDetail);
    System.IO.Stream CPINNotificationRequest(CPINNotificationRequestr GRNDetail);

    [OperationContract]
    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "CPINReturnNotificationRequest")]
    System.IO.Stream CPINReturnNotificationRequest(CPINReturnNotificationRequest ReturnNotifyDetail);
}
