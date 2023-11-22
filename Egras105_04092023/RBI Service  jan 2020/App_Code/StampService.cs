using System;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using DL;
using EgBL;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Newtonsoft.Json;
using Update;

/// <summary>
/// Summary description for StampService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class StampService : System.Web.Services.WebService
{   
    public AuthSoapHd spAuthenticationHeader;
    string Result = "";
    string cipherText = "";
    string CheckSum = "";
    int version = 1;
    public StampService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class AuthSoapHd : SoapHeader
    {
        public string strUserName;
        public string strPassword;
        public string strBsrCode;
    }
    /// <summary>
    /// Get GRN Details on GRN number
    /// </summary>
    /// <param name="encGRN">GRN|Amount|MerchantCode</param>
    /// <param name="MerchantCode">MerchantCode</param>
    /// <returns>GRN Details</returns>
    [WebMethod]
    public string GRN_Verify_Data(string encGRN, int MerchantCode)
    {
        try
        {
            GenralFunction gf = new GenralFunction();
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
            objEgEChallanBankBL.Mcode = MerchantCode;
            version = objEgEChallanBankBL.GetVersioninfo();

            cipherText = version == 1 ? objEncry.Decrypt(encGRN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
                objEncry.DecryptSBIWithKey256(encGRN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key", null);

            string[] Ivalues = cipherText.Split('|');
            Int64 VID = Convert.ToInt64(Ivalues[0].Split('=').GetValue(1).ToString());
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
            double Amount = Convert.ToDouble(Ivalues[2].Split('=').GetValue(1));
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = VID };
            PM[1] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = null };
            PM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = Amount };
            PM[3] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = MerchantCode };
            // Result = gf.ExecuteScaler(PM, "GetGrnMerchantDetails_ws");
            Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
            if (Result != "")
            {
                List<string> lstValues = GetValues(Result);
                if (lstValues[15].ToString() == "P")
                {
                    string returnData = VerifyGRN(lstValues[11], Convert.ToDouble(lstValues[13].ToString()), lstValues[7]);
                    if (returnData != "" && returnData != null)
                    {
                        if (lstValues[7] == "1000132")
                        {
                            string[] lstPlainText = returnData.Split('|');
                            if (lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "S")
                            {
                                //

                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                                //gf.UpdateData(PARM, "EgInsertMapBudgetHead");
                                // Result =  gf.ExecuteScaler(PM, "GetGrnMerchantDetails_ws");
                            }
                        }
                        else
                        {
                            List<string> lstReturnData;
                            try
                            {
                                lstReturnData = GetValues(returnData);
                            }
                            catch (Exception ex)
                            {
                                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash("AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=" + Amount.ToString("F") + "|Status=P") :
                                      objEncryptDecryptionBL.GetSHA256("AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=" + Amount.ToString("F") + "|Status=P");
                                Result = "AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=" + Amount.ToString("F") + "|Status=P" + "|checkSum=" + CheckSum;
                                cipherText = version == 1 ? objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
                                    objEncry.EncryptSBIWithKey256(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
                                return cipherText;

                            }
                            if (lstReturnData[13].ToString() == "S")
                            {
                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                            }
                        }
                    }
                }

                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash(Result) : objEncryptDecryptionBL.GetSHA256(Result);
                Result = Result + "|checkSum=" + CheckSum;

            }
            else
            {
                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash("AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=0.00|Status=F") :
                       objEncryptDecryptionBL.GetSHA256("AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=0.00|Status=F");
                Result = "AUIN=0|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=" + VID.ToString() + "|Amount=0.00|Status=F" + "|checkSum=" + CheckSum;

            }

            cipherText = version == 1 ? objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
             objEncry.EncryptSBIWithKey256(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");

        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return cipherText;
    }
    /// <summary>
    /// Get GRN Details on AUIN number
    /// </summary>
    /// <param name="encAUIN">AUIN|Amount|MerchantCode</param>
    /// <param name="MerchantCode">MerchantCode</param>
    /// <returns>GRN Details</returns>
    [WebMethod]
    public string AUIN_Verify_Data(string encAUIN, int MerchantCode)
    {
        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
            objEgEChallanBankBL.Mcode = MerchantCode;
            version = objEgEChallanBankBL.GetVersioninfo();

            cipherText = version == 1 ? objEncry.Decrypt(encAUIN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
                 objEncry.DecryptSBIWithKey256(encAUIN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key", null);

            string[] Ivalues = cipherText.Split('|');
            //EgErrorHandller stampLog = new EgErrorHandller();
            //stampLog.InsertStampLog(MerchantCode, encAUIN, Convert.ToString(Ivalues[0].Split('=').GetValue(1)), 0, "IN");
            string VID = Ivalues[0].Split('=').GetValue(1).ToString();
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
            double Amount = Convert.ToDouble(Ivalues[2].Split('=').GetValue(1));
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = null };
            PM[1] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = VID };
            PM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = Amount };
            PM[3] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = MerchantCode };
            Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
            if (Result != "")
            {
                List<string> lstValues = GetValues(Result);
                if (lstValues[15].ToString() == "P")
                {
                    string returnData = VerifyGRN(lstValues[11], Convert.ToDouble(lstValues[13].ToString()), lstValues[7]);
                    if (returnData != "" && returnData != null)
                    {
                        if (lstValues[7] == "1000132")
                        {
                            string[] lstPlainText = returnData.Split('|');
                            if (lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "S")
                            {
                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                            }
                        }
                        else
                        {
                            List<string> lstReturnData;
                            try
                            {
                                lstReturnData = GetValues(returnData);
                            }
                            catch (Exception ex)
                            {
                                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=" + Amount.ToString("F") + "|Status=P") :
                                        objEncryptDecryptionBL.GetSHA256("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=" + Amount.ToString("F") + "|Status=P");
                                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=" + Amount.ToString("F") + "|Status=P" + "|checkSum=" + CheckSum;
                                cipherText = version == 1 ? objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
                                    objEncry.EncryptSBIWithKey256(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
                                return cipherText;

                            }
                            if (lstReturnData[13].ToString() == "S")
                            {
                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                            }
                        }
                    }
                }

                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash(Result) : objEncryptDecryptionBL.GetSHA256(Result);
                Result = Result + "|checkSum=" + CheckSum;

            }
            else
            {
                CheckSum = version == 1 ? objEncryptDecryptionBL.GetMD5Hash("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=0.00|Status=F") :
                    objEncryptDecryptionBL.GetSHA256("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=0.00|Status=F");
                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=0.00|Status=F" + "|checkSum=" + CheckSum;

            }
            cipherText = version == 1 ? objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key") :
               objEncry.EncryptSBIWithKey256(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");

        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }


        return cipherText;
    }
    /// <summary>
    /// Get GRN Details on AUIN number RLA Version
    /// </summary>
    /// <param name="encAUIN">AUIN|Amount|MerchantCode</param>
    /// <param name="MerchantCode">MerchantCode</param>
    /// <returns>GRN Details</returns>
    [WebMethod]
    public string RLA_Verify_Data(string encAUIN, int MerchantCode)
    {
        try
        {

            if (MerchantCode != 54)
            {
                return "Invalid Request";
            }
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            cipherText = objEncry.Decrypt(encAUIN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            string[] Ivalues = cipherText.Split('|');
            string VID = Ivalues[0].Split('=').GetValue(1).ToString();
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
            double Amount = Convert.ToDouble(Ivalues[2].Split('=').GetValue(1));
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = null };
            PM[1] = new SqlParameter("@AUIN", SqlDbType.VarChar, 50) { Value = VID };
            PM[2] = new SqlParameter("@Amount", SqlDbType.Money) { Value = Amount };
            PM[3] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = MerchantCode };
            Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
            if (Result != "")
            {
                List<string> lstValues = GetValues(Result);
                if (lstValues[15].ToString() == "P")
                {
                    string returnData = VerifyGRN(lstValues[11], Convert.ToDouble(lstValues[13].ToString()), lstValues[7]);
                    if (returnData != "" && returnData != null)
                    {
                        if (lstValues[7] == "1000132")
                        {
                            string[] lstPlainText = returnData.Split('|');
                            if (lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "S")
                            {
                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                            }
                            else if (lstPlainText[0].ToString().ToLower() == "na")
                            {
                                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=" + lstValues[7] + "|BankDate=0|GRN=" + lstValues[11] + "|Amount=" + Amount.ToString("F") + "|Status=F";
                            }
                        }
                        else
                        {
                            List<string> lstReturnData;
                            try
                            {
                                lstReturnData = GetValues(returnData);
                            }
                            catch (Exception ex)
                            {
                                CheckSum = objEncryptDecryptionBL.GetMD5Hash("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=" + Amount.ToString("F") + "|Status=P");
                                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=" + Amount.ToString("F") + "|Status=P" + "|checkSum=" + CheckSum;
                                cipherText = objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
                                return cipherText;
                            }
                            if (lstReturnData[13].ToString() == "S")
                            {
                                Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetGrnMerchantDetails_ws", PM));
                            }
                            else if (lstReturnData[13].ToString() == "F")
                            {
                                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=" + lstValues[7] + "|BankDate=0|GRN=" + lstValues[11] + "|Amount=" + Amount.ToString("F") + "|Status=F";
                            }
                        }
                    }
                }

                CheckSum = objEncryptDecryptionBL.GetMD5Hash(Result);
                Result = Result + "|checkSum=" + CheckSum;
            }
            else
            {
                CheckSum = objEncryptDecryptionBL.GetMD5Hash("AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=0.00|Status=F");
                Result = "AUIN=" + VID.ToString() + "|CIN=0|BankReferenceNo=0|BANK_CODE=0|BankDate=0|GRN=0|Amount=0.00|Status=F" + "|checkSum=" + CheckSum;
            }
            cipherText = objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }

        return cipherText;
    }
    // <summary>
    /// Get Amount detail with date wise and particular month
    /// </summary>
    /// <param name="encAUIN">Date|MerchantCode</param>
    /// <param name="MerchantCode">MerchantCode</param>
    /// <returns>amount Details</returns>
    [WebMethod]
    public string Merchant_TransactionData(string encAUIN, int MerchantCode)
    {
        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            cipherText = objEncry.Decrypt(encAUIN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            string[] Ivalues = cipherText.Split('|');
            string Date = Ivalues[0].Split('=').GetValue(1).ToString();
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PM[1] = new SqlParameter("@MCode", SqlDbType.BigInt) { Value = MerCode };
            Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "egAmountDetailforIntegration", PM));
            if (Result != "")
            {
                CheckSum = objEncryptDecryptionBL.GetMD5Hash(Result);
                Result = Result + "|checkSum=" + CheckSum;
            }
            else
            {
                CheckSum = objEncryptDecryptionBL.GetMD5Hash("Dayamount=0.00|MonthAmount=0.00");
                Result = "Dayamount=0.00|MonthAmount=0.00" + "|checkSum=" + CheckSum;
            }
            cipherText = objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return cipherText;
    }
    /// <summary>
    /// Merchant_Transaction detail 22 Nov 2021
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string Merchant_TransactionDetail(string encAUIN, int MerchantCode)
    {
        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            cipherText = objEncry.Decrypt(encAUIN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            string[] Ivalues = cipherText.Split('|');
            string Date = Ivalues[0].Split('=').GetValue(1).ToString();
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PM[1] = new SqlParameter("@MCode", SqlDbType.Int) { Value = MerCode };
            Result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "ws_ecourt_data", PM));
            if (Result != "")
            {

                cipherText = objEncry.Encrypt(Result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            }
            else
            {

                cipherText = "Record Not Found";
            }

        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return cipherText;
    }

    private string Checkaudit()
    {
        UpdateGRN objUpdate = new UpdateGRN();
        objUpdate.UserID = spAuthenticationHeader.strUserName;
        objUpdate.Password = spAuthenticationHeader.strPassword;
        string rv = objUpdate.CheckLogin();

        return rv;
    }
    [WebMethod]
    public string WAMData(string encData)
    {
        try
        {
            string result = "";
            string PlainText = "";
            string cipherText = "";

            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            PlainText = objEncry.Decrypt(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "WAM.key");
            result = GetGRNDetails(PlainText);
            cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "WAM.key");
        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return cipherText;
    }

    private string GetGRNDetails(string plaintext)
    {


        string result = "";

        try
        {
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = plaintext.Split('|');
            string[] arrIndMsg;
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');

                lstPlainText.Add(arrIndMsg[0]);
                lstPlainText.Add(arrIndMsg[1]);
            }

            EgWAMDataServBL objEgWamData = new EgWAMDataServBL();
            objEgWamData.GRNNumber = Convert.ToInt64(lstPlainText[1].ToString());
            result = objEgWamData.GetWAMData();
        }
        catch (Exception ex)

        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return result;
    }
    
    protected List<string> GetValues(string palinText)
    {
        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = palinText.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }
        return lstPlainText;
    }

    /// <summary>
    /// Calling bank Service for verifying GRN
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected string VerifyGRN(string GRN, double amt, string Bank_Code)
    {
        VerifiedClass objVerified = new VerifiedClass();
        string plainText = "";
        string message = "";
        objVerified.GRN = Convert.ToInt64(GRN);
        objVerified.amt = amt;
        objVerified.flag = "D";
        objVerified.BankCode = Bank_Code;
        message = objVerified.Verifieddetails();
        plainText = objVerified.PlainText;
        return plainText;
    }


    // get  data from  dat todate  merchant code   wse 28 may 2021

    [WebMethod]
    public string GetData(string parameter)
    {
        try
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                EgGetDataBL objGetData = new EgGetDataBL();
                SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();

                List<string> ParametersReceived = new List<string>();
                ParametersReceived = parameter.Split('|').ToList();
                List<string> Data = new List<string>();
                if (!string.IsNullOrEmpty(ParametersReceived[1]))
                {

                    string PlainText = objDecrypt.Decrypt(ParametersReceived[0], System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                    Data = PlainText.Split('|').ToList();

                    string[] revdateFrom, revdateTo;
                    revdateFrom = Data[0].Trim().Split('/');
                    revdateTo = Data[1].Trim().Split('/');
                    if (!(string.IsNullOrEmpty(Data[0])) && !(string.IsNullOrEmpty(Data[1])))
                    {
                        objGetData.FromDate = Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString());
                        objGetData.ToDate = Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString());
                        objGetData.OfficeId=Convert.ToInt32(Data[2].ToString());

                        //objGetData.FromDate = DateTime.Parse(Data[0].Trim().ToString());
                        //objGetData.ToDate = DateTime.Parse(Data[1].Trim());
                        objGetData.MerchantCode = Convert.ToInt64(ParametersReceived[1]);


                        DataTable dt = objGetData.GetData();
                        string JSONString = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            JSONString = JsonConvert.SerializeObject(dt);
                            JSONString = objDecrypt.Encrypt(JSONString, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                        }
                        else
                        {
                            JSONString = JsonConvert.SerializeObject("Record Not Found");
                            JSONString = objDecrypt.Encrypt(JSONString, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");

                        }

                            return JSONString;
                    }
                    else
                    {
                        return "From Date OR To Date can not be blank !";
                    }
                }
                else
                {
                    return "Merchant code can not be found !";
                }
            }
            else
            {
                return "String can not be blank !";
            }
        }
        catch (Exception ex)
        {
           
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "There String is not Proper Format  !";
        }
    }
}


public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
    public TrustAllCertificatePolicy() { }
    public bool CheckValidationResult(ServicePoint sp,
        X509Certificate cert,
        WebRequest req,
        int problem)
    {
        return true;
    }
}

