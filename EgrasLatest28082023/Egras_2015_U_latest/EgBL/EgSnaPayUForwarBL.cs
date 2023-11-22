using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace EgBL
{
    public class EgSnaPayUForwarBL
    {
        public string SnaBankjson { get; set; }
        public string SnaDate { get; set; }
        public string returndata { get; set; }
        public string token { get; set; }
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
              
                
                SnaDate = objDecrypt.DecryptAES256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(key)+ ".key",null);
                try
                {
                    if (string.IsNullOrEmpty(SnaDate))
                        return SCode = "041";
                    if (SCode == "200T")// Token verified
                        SCode = Getdata();
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

                case "035":
                    Status = "HSRNF"; Message = "No record found";
                    break;
                case "041":
                    Status = "HSDTF"; Message = "Date can not blank";
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

            retMsg = SCode == "200" ?
                 objDecrypt.EncryptAES256( returndata,
                 System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(key) + ".key",null) :
                 new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message });
            //retMsg = new JavaScriptSerializer().Serialize(new SnaServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message + " " + exceptionMessage });
            return retMsg;
        }
        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.Char,7) { Value = key };
           
            string result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
            return result;
        }

        // PayU Forward
        public string Getdata()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@SnaDate", SqlDbType.Date) { Value = SnaDate };
            PARM[1] = new SqlParameter("@BsrCode", SqlDbType.Char,7) { Value = key };

            dt = gf.Filldatatablevalue(PARM, "EgSnaBankPull", dt, null);
            SCode = dt.Rows.Count > 0 ? "200" : "035";
            returndata = JsonConvert.SerializeObject(dt);
            return SCode;
        }


    }
}
