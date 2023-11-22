using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
    public class EgBankBranchMaster
    {

        public string MerchantCode { get; set; }
        public string Paymenttype { get; set; }
        public string Identity { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public string ObjectHead { get; set; }
        public string VNC { get; set; }
        public string PNP { get; set; }
        public string DistrictCode { get; set; }
        public string OfficeName { get; set; }
        public string ddo { get; set; }
        public string DeductCommission { get; set; }
        public string ChallanFromMonth { get; set; }
        public string ChallanToMonth { get; set; }
        public string Address { get; set; }
        public string PINCode { get; set; }
        public string City { get; set; }
        public string Remarks { get; set; }
        public string RefNumber { get; set; }
        public string TotalAmount { get; set; }
        public string ChallanYear { get; set; }
        public string Filler { get; set; }
        public string DeptCode { get; set; }
        public string Profile { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Auth { get; set; }
        public string type { get; set; }
        public string key { get; set; }
        public string token { get; set; }
        public string enctype { get; set; }
        public double HeadTotalAmount { get; set; }
        public DataTable dtCheckHeads { get; set; }
        public string TreasuryCode { get; set; }
        public int Id { get; set; }
        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();

        public string GetBankByTreasury()
        {
            string SCode = string.Empty;
            try
            {
                //=======================================
                if (string.IsNullOrEmpty(Auth))
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Header cannot be blank !" });//Header Blank

                if (string.IsNullOrEmpty(enctype))
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Parameter cannot be blank !" });//  Parameter Empty

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Token cannot be found !" });// Get token 

                key = Auth.Split('|')[1];
                if (string.IsNullOrEmpty(key))
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Merchant code cannot be found !" });// Get Merchant Key

                SCode = VerifyToken();
                if (SCode != "200T")
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Token not matched !" });

                var PlainString = objDecrypt.DecryptSBIWithKey256(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key", null);

                try
                {
                    TreasuryCode = PlainString;
                    return GetBank();
                }
                catch (Exception ex)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("EgBankBranchMasterService Level 1 IPAddress= " + IpAddress + " and MerchantCode=" + key + ":- " + ex.Message.ToString());
                    return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Some Technical Error !" });
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("EgBankBranchMasterService Level 2 IPAddress= " + IpAddress + " and MerchantCode=" + key + ":- " + ex.Message.ToString());
                return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Some Technical Error !" });
            }
        }
        public string VerifyToken()
        {
            string result = string.Empty;
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[2];
                gf = new GenralFunction();
                PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
                PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.VarChar, 8) { Value = key };
                result = gf.ExecuteScaler(PARM, "EgSNATokenVerify");
                //return result;
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("EgBankBranchMasterService  IPAddress= " + IpAddress + " and MerchantCode=" + key + ":- " + ex.Message.ToString());
                return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Some Technical Error !" });
            }
            return result;
        }
        private string GetBank()
        {
            try
            {
                SqlParameter[] PARM = new SqlParameter[1];
                GenralFunction gf = new GenralFunction();
                PARM[0] = new SqlParameter("@treasurycode", SqlDbType.Char, 4) { Value = TreasuryCode };
                DataTable dt = new DataTable();
                dt = gf.Filldatatablevalue(PARM, "EgBankBranchMaster", dt, null);
                BankByTreasuryMessage objmsg = new BankByTreasuryMessage();
                if (dt.Rows.Count > 0)
                {
                    IList<BankData> items = dt.AsEnumerable().Select(row =>
                            new BankData
                            {
                                BSRCode = row.Field<string>("BSRCode"),
                                BANKNAME = row.Field<string>("BANKNAME")
                            }).ToList();
                    objmsg.BankData = items;
                }


                return dt.Rows.Count > 0 ? JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Success", BankData = objmsg.BankData }) : JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Treasury dose not map with any Bank !" });
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("EgBankBranchMasterService IPAddress= " + IpAddress + " and MerchantCode=" + key + ":- " + ex.Message.ToString());
                return JsonConvert.SerializeObject(new BankByTreasuryMessage { msg = "Some Technical Error !" });
            }
        }
    }

    public class BankByTreasury
    {
        public string encdata { get; set; }
    }

    public class BankData
    {
        public string BSRCode { get; set; }
        public string BANKNAME { get; set; }
    }
    public class BankByTreasuryMessage
    {
        public string msg { get; set; }
        public IList<BankData> BankData { get; set; }
    }
}
