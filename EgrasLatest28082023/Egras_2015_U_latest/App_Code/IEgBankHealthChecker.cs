using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgBankHealthChecker" in both code and config file together.
[ServiceContract]
public interface IEgBankHealthChecker
{
  
    //[WebInvoke(Method = "GET",
    //    ResponseFormat = WebMessageFormat.Json,
    //    BodyStyle = WebMessageBodyStyle.Wrapped,
    //    UriTemplate = "ErrorLog/{ErrorName}/{PageName}")]
    //string InsertErrorLog(string ErrorName, string PageName);
    [OperationContract]
    [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ChkBanks")]
    int Check();
}
