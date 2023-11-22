using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgBankBranchMasterService" in code, svc and config file together.
public class EgBankBranchMasterService : IEgBankBranchMasterService
{
    public System.IO.Stream GetBank(BankByTreasury RequestParm)
    {
        string IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        try
        {
            EgBankBranchMaster objEgSnaService = new EgBankBranchMaster();
            objEgSnaService.enctype = RequestParm.encdata;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            objEgSnaService.IpAddress = IPAddress;
            var jsonStr = objEgSnaService.GetBankByTreasury();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("EgBankBranchMasterService IPAddress= " + IPAddress + " and encdata=" + RequestParm.encdata + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }
}
