using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using EgBL;
/// <summary>
/// Summary description for SendSMS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SendSMS : System.Web.Services.WebService
{

    public SendSMS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void SendBulkSMS()
    {
        EgSendMessage ObjSendMessage =new EgSendMessage();
        ObjSendMessage.GetPendingGrn();
      
    
    }

    [WebMethod]
    public void BankResponseReceiveSendSms(String Grn)
    {
        Int64 GRN = Convert.ToInt64(Grn);
        EgSendMessage ObjSendMessage = new EgSendMessage();
        ObjSendMessage.GetGrnSmsDetail(GRN);
    }
}

