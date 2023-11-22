using EgBL;
using System.Collections.Generic;
using System.Web;
using System;

/// <summary>
/// Summary description for IntegrationClass
/// </summary>
public class IntegrationClass : IntegrationInterface
{
    public Dictionary<string, IntegrationProp> Data { get; set; }
    string EncData, AUIN, ChallanType, PlainText, PlainChlnType;
    int MerchantCode;
    public static string errorName { get; set; }//sandeep for error show in IntegrationErrorPage

    public int ManualBanksType { get; set; }

    public IntegrationClass()
    { }
    public IntegrationClass(string EncData, string AUIN, int MerchantCode, string ChallanType)
    {
        this.EncData = EncData;
        this.AUIN = AUIN;
        this.MerchantCode = MerchantCode;
        this.ChallanType = ChallanType;
    }

    public bool IntegrationInterface()
    {
        try
        {
            SbiEncryptionDecryption objEncryptDecrypt = new SbiEncryptionDecryption();
            PlainText = objEncryptDecrypt.Decrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]  + MerchantCode + ".key");
            PlainChlnType = ChallanType == "" || ChallanType == null ? "0|0" : objEncryptDecrypt.Decrypt(ChallanType, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            DeptIntegrationChallans objDeptIntegrationChallans = DeptIntegrationChallans.SelectChallanType(PlainChlnType.Split('|').GetValue(0).ToString().Trim());
            objDeptIntegrationChallans.AUIN = AUIN;
            objDeptIntegrationChallans.MerchantCode = MerchantCode;
            objDeptIntegrationChallans.ChallanType = PlainChlnType.Split('|').GetValue(0).ToString().Trim().ToUpper() == "OFFICEXML" ? PlainChlnType.Split('|').GetValue(1).ToString().Trim() : PlainChlnType.Split('|').GetValue(0).ToString().Trim();
            bool result = objDeptIntegrationChallans.CheckIntegrationData(PlainText);
            Data = objDeptIntegrationChallans.Data;
            ManualBanksType = objDeptIntegrationChallans.ManualBanksType;
            return result;
        }
        catch (System.Exception ex)
        {
            errorName = ex.Message + "*" + MerchantCode+"*"+AUIN;
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(errorName);
            return false;
        }
    }

    public System.Int64 InsertChallan()
    {
        DeptIntegrationChallans objDeptIntegrationChallans = DeptIntegrationChallans.SelectChallanType("0");
        objDeptIntegrationChallans.Data = Data;
        return objDeptIntegrationChallans.InsertChallan();
    }
}