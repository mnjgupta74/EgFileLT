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

        EgETrafficBL objegetraffic = new EgETrafficBL();
        string grn = "";

        try
        {
            if ((string.IsNullOrEmpty(EncData)) && (string.IsNullOrEmpty(AUIN)))
            {
                grn = "EncData or AUIN Can Not Be Empty.";

            }
            else
            {

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
                    if (grn == "-1" || grn == "0")
                    {
                        grn = "Input String was not in Correct Format";
                        insertExceptionLog(AUIN.Trim(), Mcode, grn);
                        //objegetraffic.AUIN = AUIN.Trim();
                        //objegetraffic.EncData = EncData.Trim();
                        //objegetraffic.Mcode = Convert.ToInt32(Mcode);
                        //objegetraffic.Response = grn.Trim();
                        //objegetraffic.insertResponseLog();
                    }
                    insertExceptionLog(AUIN.Trim(), Mcode, grn);
                }
                else
                {

                    grn = "AUIN Number Already Exists.";
                    insertExceptionLog(AUIN.Trim(), Mcode, grn);
                    //objegetraffic.AUIN = AUIN.Trim();
                    //objegetraffic.EncData = EncData.Trim();
                    //objegetraffic.Mcode = Convert.ToInt32(Mcode);
                    //objegetraffic.Response = grn.Trim();
                    //objegetraffic.insertResponseLog();
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().Contains("stored procedure") || ex.Message.ToString().Contains("columns specified in the INSERT statement"))
            {
                grn = "Due to Some Technical Issue";
                insertException(ex.Message.ToString());
            }
            else
            {
                string Mcode = spAuthenticationHeader.MCode;
                grn = ex.Message.ToString();//"Due to Some Technical Issue";
                insertExceptionLog(AUIN.Trim(), Mcode, ex.Message.ToString());
                //objegetraffic.AUIN = AUIN.Trim();
                //objegetraffic.EncData = EncData.Trim();
                //objegetraffic.Mcode = Convert.ToInt32(Mcode);
                //objegetraffic.Response = ex.Message.ToString();
                //objegetraffic.insertResponseLog();
            }
            

        }
        return grn;
    }

    //insertResponseLog
    public void insertException(string grn)
    {
        EgErrorHandller obj = new EgErrorHandller();
        obj.InsertError(grn);
    }
    public void insertExceptionLog(string AUIN,string Mcode,string grn)
    {
        EgETrafficBL objegetraffic = new EgETrafficBL();
        objegetraffic.AUIN = AUIN.Trim();
        objegetraffic.Mcode = Convert.ToInt32(Mcode);
        objegetraffic.Response = grn;
        objegetraffic.insertResponseLog();
    }
}
