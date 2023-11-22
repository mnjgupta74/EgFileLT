using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace EgBL
{
    public class EgSnaServiceBL
    {
        public string budgethead { get; set; }
        public string amount { get; set; }
        public string location { get; set; }
        public string merchantcode { get; set; }
        public string accountno { get; set; }
        public string OfficeId { get; set; }
        public string deptcode { get; set; }
        public string token { get; set; }
        public string pd { get; set; }
        public string remittername { get; set; }
        public string returndata { get; set; }
        public string enctype { get; set; }
        public string SnaRefNo { get; set; }
        public string Auth { get; set; }
        public string type { get; set; }
        public string SCode { get; set; }
        public string key { get; set; }
        public string IFSCCode { get; set; }
        public string exceptionMessage { get; set; }
        public DateTime SnaDate { get; set; }

        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        EgSnaIntegrationPropBL objEgSnaIntegrationPropBL = new EgSnaIntegrationPropBL();


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
                var PlainString = objDecrypt.DecryptAES256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);

                if (type == "W")
                {
                    try
                    {
                        SnaRefNo = objEgSnaIntegrationPropBL.SNArefNo(PlainString.Split('|')[0]);// SnaRefrence no  007,007A
                        amount = objEgSnaIntegrationPropBL.amount(PlainString.Split('|')[1]).ToString();// Amount   008,009,010
                        budgethead = objEgSnaIntegrationPropBL.BudgetHead(PlainString.Split('|')[2]);// BudgetHead  011,012,013
                        pd = objEgSnaIntegrationPropBL.PdAcc(PlainString.Split('|')[3]);// Pd Account No 014,015 
                        remittername = objEgSnaIntegrationPropBL.fullName(PlainString.Split('|')[4]);// Remitter name 016,017,018
                        OfficeId = objEgSnaIntegrationPropBL.officeCode(PlainString.Split('|')[5]).ToString();// Office Id 019,020
                        location = objEgSnaIntegrationPropBL.location(PlainString.Split('|')[6]);// Location 021,022
                        accountno = objEgSnaIntegrationPropBL.AccountNo(PlainString.Split('|')[7]);// Account No 023,024
                        deptcode = objEgSnaIntegrationPropBL.departmentCode(PlainString.Split('|')[8]).ToString();// Department code    025,026
                        IFSCCode = PlainString.Split('|')[9].ToString();// Merchant Code    027,028

                        //if (key != PlainString.Split('|')[9])
                        //    return SCode = "029";// Merchant code not matched with Header

                        SCode = CheckRefNo();//  030,200R  Check Duplicate Reference No
                        if (SCode != "200R")
                            return SCode;

                        SCode = CheckOfcTreasDeptMapping();//  031  OfficeId not map with Treasury and Department Integration ,200O( Office verified)
                        if (SCode != "200O")
                            return SCode;

                        SCode = MapBudgetHead();//  032,200B  BudgetHead not mapped with department
                        if (SCode != "200B")
                            return SCode;//  032,200B  BudgetHead not mapped with department

                        SCode = VerifyPdAcc();
                        if (SCode == "200P")
                            SCode = InsertSnaData().ToString();
                        else
                            return SCode;//  033,200P  Pd Account not mapped with BudgetHead
                    }
                    catch (Exception ex) { SCode = ex.Message; }
                }
                else if (type == "R")
                {
                    try
                    {
                        SnaDate = Convert.ToDateTime(PlainString);// SnaRefrence no
                        SCode = getdata();
                    }
                    catch (Exception ex) { SCode = ex.Message; }
                }
                else
                    SCode = "200F";//Record not processed
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
                case "011":
                    Status = "HSBHF"; Message = "BudgetHead can not be blank";
                    break;
                case "012":
                    Status = "HSBHFL"; Message = "BudgetHead must contain BudgetHead(13) + ScheCode(5)";
                    break;
                case "013":
                    Status = "HSBHFIV"; Message = "Invalid BudgetHead";
                    break;
                case "014":
                    Status = "HSPDF"; Message = "Pd can not be blank";
                    break;
                case "015":
                    Status = "HSPDFIV"; Message = "Invalid Pd account";
                    break;
                case "016":
                    Status = "HSRNF"; Message = "Remitter name cannot be null";
                    break;
                case "017":
                    Status = "HSRNFL"; Message = "Remitter name Length must be less then or equal to 50";
                    break;
                case "018":
                    Status = "HSRNFIV"; Message = "Invalid Remitter Name";
                    break;
                case "019":
                    Status = "HSOCF"; Message = "Office code can not be blank";
                    break;
                case "020":
                    Status = "HSOCFIV"; Message = "Invalid Office Code";
                    break;
                case "021":
                    Status = "HSLFL"; Message = "Location cannot be null and length should be 4";
                    break;
                case "022":
                    Status = "HSLFIV"; Message = "Invalid Location";
                    break;
                case "023":
                    Status = "HSACF"; Message = "Account No can not be blank";
                    break;
                case "024":
                    Status = "HSACFIV"; Message = "Invalid Account No";
                    break;
                case "025":
                    Status = "HSDCF"; Message = "Department Code can not be blank";
                    break;
                case "026":
                    Status = "HSDCFIV"; Message = "Invalid Department Code";
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
                case "031":
                    Status = "HSOTFM"; Message = "OfficeId not map with Treasury and Department";
                    break;
                case "032":
                    Status = "HSBHFM"; Message = "BudgetHead not mapped with department";
                    break;
                case "033":
                    Status = "HSPDFM"; Message = "PDAccount not mapped with BudgetHead";
                    break;
                case "034":
                    Status = "HSRIF"; Message = "Record not processed";//Record Insert Fail
                    break;
                case "035":
                    Status = "HSRNF"; Message = "No record found";//During Pull Request
                    break;
                case "200T":
                    Status = "HSTS"; Message = "Token verified";
                    break;
                case "200R":
                    Status = "HSRS"; Message = "Reference no virified";
                    break;
                case "200O":
                    Status = "HSOS"; Message = "Office code mapping verified";
                    break;
                case "200B":
                    Status = "HSBS"; Message = "BudgetHead mapping verified";
                    break;
                case "200P":
                    Status = "HSPS"; Message = "PDAccount mapping verified";
                    break;
                case "200F":
                    Status = "SNAF"; Message = "Some technical issue";
                    break;
                case "200":
                    Status = "OK"; Message = "Record processed successfully";
                    break;
                case "999":
                    Status = "SNAEX"; Message = "Exception" + exceptionMessage;
                    break;

                default:
                    Status = "000"; Message = "No Status Code Matched";
                    break;
            }

            retMsg = SCode == "200" && type == "R" ?
                 objDecrypt.EncryptAES256( returndata
                 , System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key",null) :
                 new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message });
            //retMsg = new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message });
            return retMsg;
        }

        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode",SqlDbType.VarChar,8) { Value = key };
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
        }

        public string CheckRefNo()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@SnaRefNo", SqlDbType.NVarChar, 10) { Value = SnaRefNo };
            string result = gf.ExecuteScaler(PARM, "EgSNACheckRefNo");
            return result;
        }
        public string InsertSnaData()
        {
            string Rv = "-1";
            try
            {
                EgSnaIntegrationPropBL objEgSnaIntegrationPropBL = new EgSnaIntegrationPropBL();
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[11];

                PM[0] = new SqlParameter("@SnaRefNo", SqlDbType.VarChar, 50) { Value = SnaRefNo };
                PM[1] = new SqlParameter("@Amount", SqlDbType.Money) { Value = amount };
                PM[2] = new SqlParameter("@BudgetHead", SqlDbType.VarChar, 15) { Value = budgethead };
                PM[3] = new SqlParameter("@Pd", SqlDbType.Int) { Value = pd };
                PM[4] = new SqlParameter("@RemitterName", SqlDbType.VarChar, 50) { Value = remittername };
                PM[5] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
                PM[6] = new SqlParameter("@Location", SqlDbType.Char, 4) { Value = location };
                PM[7] = new SqlParameter("@AccountNo", SqlDbType.VarChar, 40) { Value = accountno };
                PM[8] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = deptcode };
                PM[9] = new SqlParameter("@merchantcode", SqlDbType.VarChar, 4) { Value = key };
                PM[10] = new SqlParameter("@IFSCCode", SqlDbType.VarChar,10) { Value = IFSCCode };
                Rv = gf.UpdateData(PM, "EgSNAInsertDeptData").ToString();
                Rv = Rv == "1" ? "200" : "034";
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return Rv;
        }
        public DataTable GetDataTableFromJsonString(string jsonString)
        {
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
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public string getdata()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@SnaDate", SqlDbType.DateTime) { Value = SnaDate };
            dt = gf.Filldatatablevalue(PARM, "EgSNADeptPullData", dt, null);
            returndata = JsonConvert.SerializeObject(dt);
            return dt.Rows.Count > 0 ? "200" : "035";
        }

        /// <summary>
        /// Check Office Treasury and Department mapping
        /// </summary>
        public string CheckOfcTreasDeptMapping()
        {
            EgCheckBudgetHead objEgCheckBudgetHead = new EgCheckBudgetHead();
            objEgCheckBudgetHead.TreasuryCode = location;
            objEgCheckBudgetHead.OfficeId = Convert.ToInt32(OfficeId);
            objEgCheckBudgetHead.DepartmentCode = Convert.ToInt16(deptcode);
            if (objEgCheckBudgetHead.VarifyOfficeId() != 1)
            {
                return "031";//OfficeId not map with Treasury and Department Integration
            }
            return "200O";
        }
        /// <summary>
        /// Check Pd Account 
        /// </summary>dow
        /// <returns></returns>
        public string MapBudgetHead()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = budgethead.Substring(0, 13) };
            PM[1] = new SqlParameter("@schecode", SqlDbType.Int) { Value = budgethead.Substring(13, 5) };
            PM[2] = new SqlParameter("@Deptcode", SqlDbType.Char, 1) { Value = deptcode };
            return gf.ExecuteScaler(PM, "EGSNACheckBudgetHead");
        }

        /// <summary>
        /// Check Pd Account 
        /// </summary>dow
        /// <returns></returns>
        public string VerifyPdAcc()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = location };
            PM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = budgethead.Substring(0, 13) };
            PM[2] = new SqlParameter("@PDAcc", SqlDbType.Int) { Value = pd };
            PM[3] = new SqlParameter("@PayMode", SqlDbType.Char, 1) { Value = "N" };
            var val = gf.ExecuteScaler(PM, "EgCheckPDAccountMapping");
            return val == "True" ? "200P" : "033";
        }
    }

    class SnaServiceResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ReturnData { get; set; }
    }
    class SnaServiceErrorResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
