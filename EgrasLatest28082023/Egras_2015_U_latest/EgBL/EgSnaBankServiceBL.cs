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

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return SCode = "003";// Get token 

                key = Auth.Split('|')[1];
                if (string.IsNullOrEmpty(key))
                    return SCode = "004";// Get Merchant Key

                SCode = VerifyToken();
                if (SCode != "200T")
                    return SCode;

                SnaBankjson = objDecrypt.DecryptAES256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(key) + ".key", null);
                try
                {

                    if (SCode == "200T")// Token verified
                        SCode = InsertSnaBankData().ToString();
                    else
                        return SCode; //Token verificaton failed
                }
                catch (Exception ex) { SCode = ex.Message; }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return SCode;
            //=======================================
        }
        private string Validation(string SCode)
        {
            string Status = string.Empty;
            string Message = string.Empty;
            string retMsg = string.Empty;

            switch (SCode)
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
                case "040":
                    Status = "HSPAYF"; Message = "PaymentDate can not be null or blank";
                    break;
                case "200T":
                    Status = "HSTS"; Message = "Token verified";
                    break;
                case "200F":
                    Status = "SNAF"; Message = "Some technical issue";
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
            return retMsg;
        }
        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 8) { Value = key };
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
        }
        //public string getdata()
        //{
        //    GenralFunction gf = new GenralFunction();
        //    //SqlParameter[] PARM = new SqlParameter[3];
        //    SqlParameter[] PARM = new SqlParameter[0];
        //    DataTable dt = new DataTable();
        //    gf = new GenralFunction();
        //    dt = gf.Filldatatablevalue(PARM, "adcc", dt, null);
        //    var returndata = JsonConvert.SerializeObject(dt);
        //    return returndata;
        //}
        public int InsertSnaBankData()
        {
            int Rv = -1;
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[1];
                DataTable SnaBankDataTable = new DataTable();
                SnaBankDataTable = GetDataTableFromjsonString(SnaBankjson);
                PM[0] = new SqlParameter("@SnaBankDataTable", SqlDbType.Structured) { Value = SnaBankDataTable };

                Rv = gf.UpdateData(PM, "EgSNAInsertBankData");
                Rv = Rv == 1 ? 200 : -1;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return Rv;
        }

        private DataTable GetDataTableFromjsonString(string jsonString)
        {
            EgSnaIntegrationPropBL objEgSnaIntegrationPropBL = new EgSnaIntegrationPropBL();
            int rowno = 1;
            string columnname = string.Empty;
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
    }
}
