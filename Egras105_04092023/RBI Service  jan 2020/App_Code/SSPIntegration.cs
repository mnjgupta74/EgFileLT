using EgBL;
using System;
using System.Data;
using System.ServiceModel.Activation;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SSPIntegration" in code, svc and config file together.
public class SSPIntegration : ISSPIntegration
{
    public string GenerateGRN(string RequestParm)
    {

        try
        {
            EgSSPIntegrastion objEgSnaService = new EgSSPIntegrastion();
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var a = objEgSnaService.GenerateGRN();
            return a;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message);
            return "Some Technical Error !";
        }
    }
    

    public string GeneratePDGrn(string RequestParm, string auinno)
    {
        try
        {
            EgGeneratePDGrnBL objEgSnaService = new EgGeneratePDGrnBL();
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var a = objEgSnaService.GenerateGRN();
            return a;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message);
            return "Some Technical Error !";
        }
    }

    public string GenerateRevenueGrn(string RequestParm, string auinno)
    {
        try
        {
            EgGenerateRevenueGrnBL objEgSnaService = new EgGenerateRevenueGrnBL();
            objEgSnaService.enctype = RequestParm;
            objEgSnaService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var a = objEgSnaService.GenerateGRN();
            return a;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message);
            return "Some Technical Error !";
        }
    }
}
