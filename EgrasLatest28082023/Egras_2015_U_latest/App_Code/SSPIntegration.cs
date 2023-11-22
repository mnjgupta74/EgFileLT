using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Script.Serialization;

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

            if (objEgSnaService.statusCode.Trim() == "200")
            {
                try { 
                EgManualBankServiceBL objEgManualBL = new EgManualBankServiceBL();
                objEgManualBL.BankCode = "0028810";
                objEgManualBL.GRNNumber = Convert.ToInt64(a.Split('=')[1]);
                var challanDetails = objEgManualBL.GetGrnManualDetails();
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                // SBIManualServ.RAJASTHANWS objsbiPush = new SBIManualServ.RAJASTHANWS();

                var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS_Manual.key");
                //objsbiPush.challanposting(cipherText);// PUSH Service SBI Manual Challan anywhere
                //cipherText = objEncry.Encrypt(ChallanDetails, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));

                Ojpost.ProcessRajasthanData(cipherText);
                }
                catch (Exception ex)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message);
                    return "Bank Side Service Issue!";
                }
            }
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
        var a="";
        try
        {
            EgGenerateRevenueGrnBL objEgRevenueService = new EgGenerateRevenueGrnBL();
            objEgRevenueService.enctype = RequestParm;
            objEgRevenueService.Auth = System.Web.HttpContext.Current.Request.Headers["Token"];
             a = objEgRevenueService.GenerateGRN();
            

                if (objEgRevenueService.statusCode.Trim() == "200")

                {
                    try
                    {
                        EgManualBankServiceBL objEgManualBL = new EgManualBankServiceBL();
                        objEgManualBL.BankCode = "0006326";
                        objEgManualBL.GRNNumber = Convert.ToInt64(a.Split('=')[1]);
                        var challanDetails = objEgManualBL.GetGrnManualDetails();
                        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                        SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                        // SBIManualServ.RAJASTHANWS objsbiPush = new SBIManualServ.RAJASTHANWS();

                        var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS_Manual.key");
                        //objsbiPush.challanposting(cipherText);// PUSH Service SBI Manual Challan anywhere
                        //cipherText = objEncry.Encrypt(ChallanDetails, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));

                        Ojpost.ProcessRajasthanData(cipherText);
                    }
                    catch (Exception ex)
                    {
                        EgErrorHandller obj = new EgErrorHandller();
                        obj.InsertError(ex.Message);
                        return "Bank Side Service Issue!";
                    }
                }
            
           

            return a;
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message);
            return a;
        }
    }
}
