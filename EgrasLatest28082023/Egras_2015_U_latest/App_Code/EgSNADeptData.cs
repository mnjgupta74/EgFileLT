using EgBL;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

using System.Web.Script.Serialization;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SnaService" in code, svc and config file together.
public class EgSNADeptData : IEgSNADeptData
{
    
    public string PushSnaData(string RequestParm)
    {
        try
        {
            EgSnaServiceBL objEgSnaService = new EgSnaServiceBL();
            objEgSnaService.type = "W";
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var a = objEgSnaService.ProcessData();
            return a;
        }
        catch (Exception ex)
        {
            return new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = "999", Message = ex.Message.ToString() });
        }
    }

    public string PullData(string RequestParm)
    {
        try
        {
            EgSnaServiceBL objEgSnaService = new EgSnaServiceBL();
            objEgSnaService.type = "R";
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var a = objEgSnaService.ProcessData();
            return a;
        }
        catch (Exception ex)
        {
            return new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = "999", Message = ex.Message.ToString() });
        }
    }
}

class SnaServiceErrorResponse
{
    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
