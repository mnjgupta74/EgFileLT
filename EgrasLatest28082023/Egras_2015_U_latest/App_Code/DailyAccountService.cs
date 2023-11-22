using EgBL;
using System;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DailyAccountService" in code, svc and config file together.
public class DailyAccountService : IDailyAccountService
{
    public System.IO.Stream DailyAccountFile(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetFileData();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }

        
    }

    public System.IO.Stream DailyAccountSummaryFile(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetIMServerData();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }
    }
    public System.IO.Stream Pay1(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetPay1Data();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }
    public System.IO.Stream Pay2(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetPay2Data();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }
    public System.IO.Stream Pay3(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetPay3Data();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }
    public System.IO.Stream Pay4(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetPay4Data();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }
    
    public System.IO.Stream DMSFile(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetDMSData();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }

    public System.IO.Stream GetAccountFile(DailyAccountRequest RequestObj)
    {
        try
        {
            EgDailyAccountBL objAccountService = new EgDailyAccountBL();
            //DailyAccountResponse1
            objAccountService.enctype = RequestObj.RequestParam;
            objAccountService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
            var jsonStr = objAccountService.GetAGData();
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("IPAddress= " + IPAddress + " and CPIN=" + RequestParm + ":- " + ex.Message.ToString());
            return new MemoryStream(Encoding.UTF8.GetBytes("Request Unable To Process !"));
        }


    }

}
