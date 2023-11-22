using EgBL;
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for EgDeptIntegrationService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class EgDeptIntegrationService : System.Web.Services.WebService
{
    public AuthSoapHd spAuthenticationHeader;
    public class AuthSoapHd : SoapHeader
    {
        public string MCode;
    }
    public EgDeptIntegrationService()
    {
    }

    /// <summary>
    /// <param name="EncData"></param>passed as parameter
    /// <param name="AUIN"></param> passed as parameter used for refno in database
    /// <param name="Mcode">Merchent Code</param> passed in header
    /// <param name="ChallanType">""</param> challanType should be passed "" in place of 0 because 0 cannot be passed in CheckData()
    /// <returns></returns>
    /// </summary>
    [WebMethod(EnableSession = true), SoapHeader("spAuthenticationHeader")]
    public string GetGRN(string EncData, string AUIN)
    {
        string grn = "-1";
        try
        {
            if ((string.IsNullOrEmpty(EncData)) && (string.IsNullOrEmpty(AUIN)))
            {
                throw new Exception("EncData or AUIN Can Not Be Empty.");
            }
            else
            {
                EgETrafficBL objegetraffic = new EgETrafficBL();
                string Mcode = spAuthenticationHeader.MCode;
                objegetraffic.Mcode = Convert.ToInt16(Mcode);
                objegetraffic.AUIN = AUIN;
                objegetraffic.EncData = EncData;
                if (objegetraffic.GetTrafficMerchantinfo() == 1)
                {
                    HttpContext.Current.Session["UserId"] = "73";
                    objegetraffic.AUIN = AUIN.Trim();
                    objegetraffic.EncData = EncData.Trim();
                    objegetraffic.Mcode = Convert.ToInt32(Mcode);
                    grn = objegetraffic.IntegrationInterface().ToString();
                }
                else
                {
                    throw new Exception("AUIN Number Already Exists.");
                }
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            grn = "Due to Some Technical Issue";
        }
        return grn;
    }
}
