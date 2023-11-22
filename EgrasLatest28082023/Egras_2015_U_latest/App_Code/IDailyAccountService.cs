using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDailyAccountService" in both code and config file together.
[ServiceContract]
public interface IDailyAccountService
{
    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "DailyAccountFile")]

   
    //string DailyAccountFile(string RequestParm);
    System.IO.Stream DailyAccountFile(DailyAccountRequest RequestObj);

    [OperationContract]


    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "DailyAccountSummaryFile")]
    
    System.IO.Stream DailyAccountSummaryFile(DailyAccountRequest RequestParm);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "Pay1")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream Pay1(DailyAccountRequest RequestObj);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "Pay2")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream Pay2(DailyAccountRequest RequestObj);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "Pay3")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream Pay3(DailyAccountRequest RequestObj);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "Pay4")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream Pay4(DailyAccountRequest RequestObj);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "DMSFile")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream DMSFile(DailyAccountRequest RequestObj);

    [OperationContract]

    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
         RequestFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "GetAccountFile")]


    //string DailyAccountFile(string RequestParm);
    System.IO.Stream GetAccountFile(DailyAccountRequest RequestObj);
}
