using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace EgBL
{

    public class EgSNABankServiceBL
    {
        public string token { get; set; }
        public string SnaBankjson { get; set; }
        public string Auth { get; set; }
        public string SCode { get; set; }
        public string key { get; set; }
        public string exceptionMessage { get; set; }
        public string enctype { get; set; }
        public string location { get; set; }
        public string filler { get; set; }
        public string OfficeId { get; set; }
        public string DeptCode { get; set; }
        public string MerchantCodeId { get; set; }
        public string merchantcode { get; set; }
        public string StatusCode { get; set; }
        public string message { get; set; }
        public string BankRefrenceNo { get; set; }
        public string ReturnCode { get; set; }

        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        EgSnaIntegrationPropBL objEgSnaIntegrationPropBL = new EgSnaIntegrationPropBL();
        SNAMerchantCode ObjSNAMerchantCode = new SNAMerchantCode();
        public string ProcessData()
        {
            return Validation(GetStatusCode());
        }
        private string GetStatusCode()
        {
            try
            {

                //=======================================
                if (string.IsNullOrEmpty(Auth))
                    return SCode = "001";//Header Blank

                if (string.IsNullOrEmpty(enctype))
                    return SCode = "002";//  Parameter Empty

                if (Auth.Length <= 0)
                    return SCode = "041";

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return SCode = "003";// Get token 

                merchantcode = Auth.Split('|')[1];
                if (string.IsNullOrEmpty(merchantcode))
                    return SCode = "004";// Get Merchant Code

                InsertSNABankDataLog();

                key = ObjSNAMerchantCode.GetKey(merchantcode);

                if (string.IsNullOrEmpty(key))
                    return SCode = "004A";// Get Merchant Key

                SCode = VerifyToken();
                if (SCode != "200T")
                    return SCode;

                SnaBankjson = objDecrypt.DecryptSBIWithKey256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);
                try
                {
                    if (SnaBankjson.Length <= 0)
                        return SCode = "042";


                    if (SCode == "200T")// Token verified
                        SCode = InsertSnaBankData();
                    else
                        return SCode; //Token verificaton failed
                }
                catch (Exception ex) { SCode = ex.Message; }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message.ToString());
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                throw new Exception("Request Unable To Process !");
            }
            return SCode;
            //=======================================
        }
        private string Validation(string SCode)
        {
            string Status = string.Empty;
            string Message = string.Empty;
            string retMsg = string.Empty;

            switch (SCode.Trim())
            {
                case "001":
                    Status = "HF"; Message = "Header empty";
                    break;
                case "002":
                    Status = "PF"; Message = "Parameter empty";
                    break;
                case "003":
                    Status = "HFT"; Message = "Token in header missing";
                    break;
                case "004":
                    Status = "HFM"; Message = "Merchant key in header missing";
                    break;
                case "004A":
                    Status = "HFK"; Message = "Merchant key in header missing";
                    break;
                case "005":
                    Status = "HFTM"; Message = "No token match";
                    break;
                case "006":
                    Status = "HFTE"; Message = "Token expired";
                    break;
                case "007":
                    Status = "HSRF"; Message = "Renference no can not be null or blank";
                    break;
                case "007A":
                    Status = "HSRFIV"; Message = "Invalid renference no and minimum length 1";
                    break;
                case "008":
                    Status = "HSAF"; Message = "Amount is invalid";
                    break;
                case "009":
                    Status = "HSAFNP"; Message = "Amount should be only in rupees not in paise";
                    break;
                case "010":
                    Status = "HSAFGZ"; Message = "Amount must be greater than 0";
                    break;
                case "023":
                    Status = "HSACF"; Message = "Account No can not be blank";
                    break;
                case "024":
                    Status = "HSACFIV"; Message = "Invalid Account No";
                    break;
                case "027":
                    Status = "HSMF"; Message = "Merchant Code can not be blank";
                    break;
                case "028":
                    Status = "HSMFIV"; Message = "Invalid Merchant Code !";
                    break;
                case "029":
                    Status = "HSMFWH"; Message = "Merchant code not matched with Header";
                    break;
                case "030":
                    Status = "HSRFD"; Message = "Duplicate referenceno found";
                    break;
                case "034":
                    Status = "HSRIF"; Message = "Record not processed";//Record Insert Fail
                    break;
                case "036":
                    Status = "HSBSRF"; Message = "BSRCode cannot be null and length should be 7";
                    break;
                case "037":
                    Status = "HSBSRFIV"; Message = "Invalid BSRCode and length should be 7";
                    break;
                case "038":
                    Status = "HSBRF"; Message = "Bank Renference no can not be null or blank";
                    break;
                case "039":
                    Status = "HSBRFIV"; Message = "Invalid Bank Renference No and minimum length 1";
                    break;
                case "039A":
                    Status = "HSMBRN"; Message = "Multiple Bank Refrence No Not Allowed";
                    break;
                case "039B":
                    Status = "HSDBRN"; Message = "Duplicate Bank Refrence No Not Allowed";
                    break;
                case "039C":
                    Status = "HSDSNARN"; Message = "Multiple SNA Refrence No Not Allowed";
                    break;
                case "039D":
                    Status = "HSDSNARN"; Message = "Duplicate SNA Refrence No Not Allowed";
                    break;
                case "040":
                    Status = "HSPAYF"; Message = "PaymentDate can not be null or blank";
                    break;
                case "041":
                    Status = "HSAF"; Message = "Token Can Not Be Blank";
                    break;
                case "042":
                    Status = "HSPF"; Message = "Parameter String Can Not Be Blank";
                    break;
                case "200T":
                    Status = "HSTS"; Message = "Token verified";
                    break;
                case "200F":
                    Status = "SNAF"; Message = "Some technical issue";
                    break;
                case "200B":
                    Status = "SNABRS"; Message = "Bank Refrence No Allowed";
                    break;
                case "200":
                    Status = "OK"; Message = "Record processed successfully";
                    break;
                case "999":
                    Status = "SNABEX"; Message = "Exception" + exceptionMessage;
                    break;

                default:
                    Status = "000"; Message = "No Status Code Matched";
                    break;
            }

            //retMsg = SCode == "200" && type == "R" ?
            //     objDecrypt.EncryptSBIWithKey256(new JavaScriptSerializer().Serialize(new SnaServiceResponse()
            //     {
            //         StatusCode = SCode,
            //         Status = Status,
            //         Message = Message,
            //         ReturnData = returndata
            //     }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key") :
            //     new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message });
            retMsg = new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message + " " + exceptionMessage });
            message = retMsg;
            UpdateSNADeptDataStatusMessage();
            return retMsg;
        }
        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 8) { Value = merchantcode };
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
        }
        //public string CheckDuplicateBankRefrenceNo()
        //{
        //    GenralFunction gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[1];
        //    gf = new GenralFunction();
        //    PARM[0] = new SqlParameter("@BankRefrenceNo", SqlDbType.NVarChar, 20) { Value = BankRefrenceNo };
        //    string result = gf.ExecuteScaler(PARM, "EgSNACheckDuplicateBankRefNo");
        //    SCode = result == "1" ? "039B" : "200B";//200B  Bank Refrence No OK
        //    return result;
        //}
        public string VerifyCondition()
        {

            return SCode;
        }
        public string InsertSnaBankData()
        {
            string Rv = "-1";
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[3];
                DataTable SnaBankDataTable = new DataTable();
                SnaBankDataTable = GetDataTableFromjsonString(SnaBankjson);

                PM[0] = new SqlParameter("@SnaBankDataTable", SqlDbType.Structured) { Value = SnaBankDataTable };
                PM[1] = new SqlParameter("@MerchantCodeId", SqlDbType.VarChar, 11) { Value = MerchantCodeId };
                PM[2] = new SqlParameter("@ReturnCode", SqlDbType.VarChar, 10) { Value = ReturnCode };
                PM[2].Direction = ParameterDirection.Output;
                
                Rv = gf.UpdateData(PM, "EgSNAInsertBankData").ToString();
                ReturnCode = PM[2].Value.ToString();
                SCode = ReturnCode;//== "1" ? "200" : ReturnCode;
            }
            catch (Exception ex)
            {
                SCode = "200F";
                throw new Exception(ex.Message);
            }
            return SCode;
        }
        public string InsertSNABankDataLog()
        {
            string Rv = "-1";
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[4];

                PM[0] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 10) { Value = merchantcode };
                PM[1] = new SqlParameter("@EncData", SqlDbType.VarChar, 500) { Value = enctype };
                PM[2] = new SqlParameter("@MerchantCodeId", SqlDbType.VarChar, 10) { Value = MerchantCodeId };
                PM[2].Direction = ParameterDirection.Output;
                PM[3] = new SqlParameter("@Token", SqlDbType.VarChar, 100) { Value = token };

                Rv = gf.UpdateData(PM, "InsertSNABankDataLog").ToString();
                MerchantCodeId = PM[2].Value.ToString();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            //InsertSNADeptDataLog
            return "";
        }
        private DataTable GetDataTableFromjsonString(string jsonString)
        {
            EgSnaIntegrationPropBL objEgSnaIntegrationPropBL = new EgSnaIntegrationPropBL();
            int rowno = 1;
            string columnname = string.Empty;
            BankRefrenceNo = "0";
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {

                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    string RowColumns = string.Empty;
                    string RowDataString = string.Empty;
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        //if (BankRefrenceNo == "0" && RowColumns.Equals("BankRefNo"))
                        //{
                        //    BankRefrenceNo = RowDataString;
                        //    string refStatus = CheckDuplicateBankRefrenceNo();

                        //    if (refStatus == "1")
                        //        return dt;
                        //    objEgSnaIntegrationPropBL.BankRefrenceNo = RowDataString;
                        //}
                        //nr[RowColumns] = objEgSnaIntegrationPropBL.SNArefNo(RowDataString);
                        nr[RowColumns] =
                            RowColumns.Equals("ReferenceNo") ? objEgSnaIntegrationPropBL.SNArefNo(RowDataString) :
                            RowColumns.Equals("Amount") ? objEgSnaIntegrationPropBL.amount(RowDataString).ToString() :
                            RowColumns.Equals("PaymentDate") ? objEgSnaIntegrationPropBL.PaymentDate(RowDataString) :
                            RowColumns.Equals("BankRefNo") ? objEgSnaIntegrationPropBL.BankRefNo(RowDataString) :
                            RowColumns.Equals("BSRCode") ? objEgSnaIntegrationPropBL.BSRCode(RowDataString) :
                            RowColumns.Equals("Accountno") ? objEgSnaIntegrationPropBL.AccountNo(RowDataString) :
                            RowColumns.Equals("Merchantcode") ? objEgSnaIntegrationPropBL.merchantCode(RowDataString) :
                            RowDataString;
                    }
                    catch (Exception ex)
                    {
                        exceptionMessage = "RecordNo:" + rowno + " ColumnName :" + RowColumns;
                        throw new Exception(ex.Message);
                    }
                    rowno++;
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }
        private void UpdateSNADeptDataStatusMessage()
        {
            string Rv = "-1";
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[2];

                PM[0] = new SqlParameter("@message", SqlDbType.NVarChar, 500) { Value = message };
                PM[1] = new SqlParameter("@MerchantCodeId", SqlDbType.VarChar, 10) { Value = MerchantCodeId };

                Rv = gf.UpdateData(PM, "EgUpdateBankStatusMessage").ToString();
            }
            catch { }
        }




    }
}
