using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgTokenServiceBL
    {
        public string username { get; set; }

        public string password { get; set; }
        public string merchantcode { get; set; }
        public string encdata { get; set; }
        public string IPAddress { get; set; }
        public string token { get; set; }

        public string VeryfyTokenData()
        {
            try
            {
                if (string.IsNullOrEmpty(merchantcode))
                    return JsonConvert.SerializeObject(new TokenMessage { msg = "Merchant code cannot be found !" });//Merchant Code Blank

                if (string.IsNullOrEmpty(encdata))
                    return JsonConvert.SerializeObject(new TokenMessage { msg = "Parameter cannot be blank !" });//  Parameter Empty

                SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
                SNAMerchantCode ObjSNAMerchantCode = new SNAMerchantCode();

                string PlainString = objDecrypt.DecryptAES256(encdata, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(merchantcode) + ".key", null);

                username = PlainString.Split('|')[0].ToString();
                password = PlainString.Split('|')[1].ToString();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return JsonConvert.SerializeObject(new TokenMessage { msg = "Username or Password cannot be found !" });// Get token 
                else
                {
                    return verifyUser();
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("EgAuthTokenAPI IPAddress= " + IPAddress + " and MerchantCode=" + merchantcode + ":- " + ex.Message.ToString());
                return JsonConvert.SerializeObject(new TokenMessage { msg = "Some Technical Error !" });
            }

        }
        public string verifyUser()
        {
            string str = string.Empty;
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[3];

                PM[0] = new SqlParameter("@username", SqlDbType.VarChar, 20) { Value = username };
                PM[1] = new SqlParameter("@password", SqlDbType.VarChar, 20) { Value = password };
                PM[2] = new SqlParameter("@merchantcode", SqlDbType.Char, 7) { Value = merchantcode };
                str = gf.ExecuteScaler(PM, "EgSNAUserVerify");
                return str.Length > 0 && str.Trim() != "0" ? JsonConvert.SerializeObject(new TokenMessage { msg = "success", token = str }) : JsonConvert.SerializeObject(new TokenMessage { msg = "Token Not Generated !" });
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("EgAuthTokenAPI IPAddress= " + IPAddress + " and MerchantCode=" + merchantcode + ":- " + ex.Message.ToString());
                return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Some Technical Error !" });
            }
        }
    }

    public class TokenMessage
    {
        public string msg { set; get; }
        public string token { set; get; }
    }
}
