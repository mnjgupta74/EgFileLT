using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgUpdateStatusOnlineBankingGrn" in both code and config file together.
[ServiceContract]
public interface IEgTransStatusUpdate
{
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "UpdateGRNStatus")]
    string UpdateGRNStatus(string Parameter);
}
