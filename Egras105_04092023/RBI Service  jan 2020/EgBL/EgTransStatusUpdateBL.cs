
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using Update;

namespace EgBL
{
   
    public class EgTransStatusUpdateBL
    {
        private string version { get; set; }
        private string SCode { get; set; }
        private string key { get; set; }
        public string enctype { get; set; }
        private string Merchantcode { get; set; }
        public string Auth { get; set; }
        private Int64 GRN { get; set; }
        private string BANK_CODE { get; set; }
        private string BankReferenceNo { get; set; }
        private string CIN { get; set; }
        private DateTime PAID_DATE { get; set; }
        private double PAID_AMT { get; set; }
        private string TRANS_STATUS { get; set; }
        private string DebitBankCode { get; set; }
        private string BankRefNo { get; set; }
        private string PayMode { get; set; }
        private string Reason { get; set; }
        private string flag { get; set; }
        private string exceptionMessage { get; set; }
        public string token { get; set; }

        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
       
        public string ProcessData()
        {
            return Validation(GetStatusCode());
        }
        private string GetStatusCode()
        {
            BanksEncryptionDecryptionBL objEcnDecBL = new BanksEncryptionDecryptionBL();
            try
            {
                if (string.IsNullOrEmpty(Auth))
                    return SCode = "001";//Header Blank

                if (string.IsNullOrEmpty(enctype))
                    return SCode = "002";//  Parameter Empty

                var AuthList = Auth.Split('|');
                if (AuthList.Length != 3)
                    return SCode = "021";// Get Auth token String

                token = Auth.Split('|')[0];
                if (string.IsNullOrEmpty(token))
                    return SCode = "005";// Get token 

                var mkey = Auth.Split('|')[1];
                Merchantcode = mkey;
                if (string.IsNullOrEmpty(mkey))
                    return SCode = "004";// Get Merchant Key

             
                version = Auth.Split('|')[2];
                if (string.IsNullOrEmpty(version))
                    return SCode = "003";// Get Version

                SNAMerchantCode obj = new SNAMerchantCode();
                key = obj.GetKey(mkey);

                if (string.IsNullOrEmpty(key))
                    return SCode = "006";// Get Encryption/Dectyption Key Name

                SCode = VerifyToken();
                if (SCode != "200T")
                    return SCode == "006" ? "020" : SCode;


                List<string> lstPlainText = new List<string>();
                string PlainText = objEcnDecBL.GetDecryptedString(enctype, key, version);


                lstPlainText = PlainText.Split('|').ToList();

                if (string.IsNullOrEmpty(PlainText))
                    return SCode = "007";// Get Plain String 

                //GRN
                if (lstPlainText[0].Split('=').GetValue(1).ToString() == "0")
                    return SCode = "008";
                else
                {
                    try
                    {
                        GRN = Convert.ToInt64(lstPlainText[0].Split('=').GetValue(1).ToString());
                    }
                    catch
                    {
                        return SCode = "008";
                    }
                }
                // BankCode
                if (lstPlainText[1].Split('=').GetValue(1).ToString().Trim().Length != 7)
                {
                    return SCode = "010";
                }
                else
                {
                    BANK_CODE = (lstPlainText[1].Split('=').GetValue(1).ToString());
                }
                // BankReference
                if (lstPlainText[2].Split('=').GetValue(1).ToString() == null || lstPlainText[2].Split('=').GetValue(1).ToString() == "")
                {
                    return SCode = "009";
                }
                else
                {
                    BankReferenceNo = (lstPlainText[2].Split('=').GetValue(1).ToString());
                }
                // CIN
                if (lstPlainText[3].Split('=').GetValue(1).ToString() == null || lstPlainText[3].Split('=').GetValue(1).ToString() == "" || lstPlainText[3].Split('=').GetValue(1).ToString().Length < 20)
                {
                    return SCode = "011";
                }
                else
                {
                    CIN = (lstPlainText[3].Split('=').GetValue(1).ToString());
                }
                // PAID_DATE
                if (lstPlainText[4].Split('=').GetValue(1).ToString() == null || lstPlainText[4].Split('=').GetValue(1).ToString() == "")
                    return SCode = "012";
                else
                {
                    try
                    {
                        PAID_DATE = Convert.ToDateTime(lstPlainText[4].Split('=').GetValue(1).ToString());
                    }
                    catch
                    {
                        return SCode = "012";
                    }
                }
                // PAID_Amt
                if (lstPlainText[5].Split('=').GetValue(1).ToString() == "0")
                    return SCode = "013";
                else
                {
                    try
                    {
                        PAID_AMT = Convert.ToDouble(lstPlainText[5].Split('=').GetValue(1));
                    }
                    catch
                    {
                        return SCode = "013";
                    }
                }
                // TRANS_STATUS
                if (lstPlainText[6].Split('=').GetValue(1).ToString() == null || lstPlainText[6].Split('=').GetValue(1).ToString() == "")
                {
                    return SCode = "014";
                }
                else
                {
                    TRANS_STATUS = lstPlainText[6].Split('=').GetValue(1).ToString();
                }

                flag = obj.GetParameterListCountFlag(BANK_CODE.Trim().ToString());

                if (flag == "1")
                {
                    // DebitBankCode
                    if (lstPlainText[7].Split('=').GetValue(1).ToString() == "0")
                    {
                        return SCode = "015";
                    }
                    else
                    {
                        DebitBankCode = (lstPlainText[7].Split('=').GetValue(1)).ToString();
                    }
                    // BankRefNo
                    if (lstPlainText[8].Split('=').GetValue(1).ToString() == "0")
                    {
                        return SCode = "016";
                    }
                    else
                    {
                        BankRefNo = (lstPlainText[8].Split('=').GetValue(1)).ToString();
                    }
                    // PayMode
                    if (lstPlainText[9].Split('=').GetValue(1).ToString() == "0")
                    {
                        return SCode = "017";
                    }
                    else
                    {
                        PayMode = (lstPlainText[9].Split('=').GetValue(1)).ToString();
                    }
                    // Reason
                    if (lstPlainText[10].Split('=').GetValue(1).ToString() == "0")
                    {
                        return SCode = "018";
                    }
                    else
                    {
                        Reason = (lstPlainText[10].Split('=').GetValue(1).ToString());
                    }
                }
                GenralFunction BLobj = new GenralFunction();
                SqlTransaction Trans = BLobj.Begintrans();
                try
                {
                   // InsertAuditTransaction(enctype, BANK_CODE, GRN);
                    EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
                    objEgEChallanBankBL.GRN = GRN;
                    objEgEChallanBankBL.BankCode = BANK_CODE;
                    objEgEChallanBankBL.CIN = CIN;
                    objEgEChallanBankBL.Ref = BankReferenceNo;
                    objEgEChallanBankBL.Amount = PAID_AMT;
                    objEgEChallanBankBL.Status = TRANS_STATUS;
                    objEgEChallanBankBL.timeStamp = PAID_DATE;
                    InsertAuditTransaction();
                    int result = objEgEChallanBankBL.UpdateSuccessStatus();
                    SCode = result == 1 && flag == "0" ? "200" : result == 1 && flag == "1" ? "200U" : "019";
                    if (SCode == "200U" && flag == "1")
                    {
                        objEgEChallanBankBL.epayBSRCode = DebitBankCode;//////DebitBankCode
                        objEgEChallanBankBL.bankRefNo = BankRefNo;///////BankRefNo
                        objEgEChallanBankBL.payMode = PayMode;///////Transtype
                        objEgEChallanBankBL.Reason = Reason;
                        int res = objEgEChallanBankBL.UpdatePAYUStatus();
                        SCode = res == 1 ? "200" : "019";
                    }
                }
                catch (Exception exe)
                {
                    exceptionMessage = exe.Message.ToString();
                    EgErrorHandller objError = new EgErrorHandller();
                    objError.InsertError(exe.Message.ToString());
                    BLobj.Rollaback();
                    return SCode = "035";
                }
                finally
                {
                    BLobj.Endtrans();
                }
                return SCode;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message.ToString();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                return SCode = "035";
            }
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
                    Status = "HF"; Message = "Header Empty";
                    break;
                case "002":
                    Status = "HSNP"; Message = "Parameter Empty";
                    break;

                case "005":
                    Status = "HSNT"; Message = "Token Not Found";
                    break;
                case "004":
                    Status = "HSNM"; Message = "Merchant Code Not Found";
                    break;
                case "003":
                    Status = "HSNV"; Message = "Version Not Found";
                    break;
                case "006":
                    Status = "HSNK"; Message = "Key Not Found";
                    break;
                case "007":
                    Status = "HSNP"; Message = "String Not Found In Pararmeter After Decryption";
                    break;
                case "008":
                    Status = "GRN"; Message = "GRN Could  not be Zero And Alphanumeric";
                    break;
                case "009":
                    Status = "BankReferenceNo"; Message = "BankReferenceNo Could  not be Blank";
                    break;

                case "010":
                    Status = "BankCode"; Message = "BankCode Length Should be 7";
                    break;
                case "011":
                    Status = "CIN"; Message = "CIN Could not be null";
                    break;
                case "012":
                    Status = "Paid_Date"; Message = "Paid date could not be null";
                    break;
                case "013":
                    Status = "Paid_Amt"; Message = "Paid Amt  could not be null ";
                    break;
                case "014":
                    Status = "TRANS_STATUS"; Message = "TRANS_STATUS could not be null ";
                    break;
                case "015":
                    Status = "DebitBankCode"; Message = "DebitBankCode could not be null";
                    break;
                case "016":
                    Status = "BankRefNo"; Message = "BankRefNo could not be null";
                    break;
                case "017":
                    Status = "PayMode"; Message = "PayMode could not be null";
                    break;
                case "018":
                    Status = "Reason"; Message = "Reason  could not be null";
                    break;
                case "019":
                    Status = "Update"; Message = "Could Not Updated Successfully";
                    break;
                case "020":
                    Status = "HSTEX"; Message = "Token Created Time Exceeded";
                    break;
                case "021":
                    Status = "HFNA"; Message = "Parameter Missing In Token";
                    break;
                case "035":
                    Status = "Exception"; Message = "Due to some Technical Error";
                    break;
                case "200U":
                    Status = "Update"; Message = "Could Not Updated Successfully";
                    break;
                case "200":
                    Status = "200"; Message = "Updated Successfully";
                    break;
                default:
                    Status = "000"; Message = "Could Not Updated Successfully";
                    break;
            }

            //retMsg = SCode == "200" ?
            //    objDecrypt.EncryptSBIWithKey256(new JavaScriptSerializer().Serialize(new BankServiceResponse()
            //    {
            //        StatusCode = SCode,
            //        Status = Status,
            //        Message = Message,
            //        GRN = GRN.ToString(),
            //        Amount = PAID_AMT.ToString()
            //    }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key") :
            //    new JavaScriptSerializer().Serialize(new BankServiceErrorResponse() { StatusCode = SCode, Status = Status, Message = Message });
            retMsg = SCode == "200" ?
                new JavaScriptSerializer().Serialize(new BankServiceErrorResponse()
                {
                    StatusCode = SCode,
                    Status = Message,
                    Message = objDecrypt.EncryptSBIWithKey256(new JavaScriptSerializer().Serialize(new BankService()
                    {
                        GRN = GRN.ToString(),
                        Amount = PAID_AMT.ToString()
                    }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key")
                })
                  :
                new JavaScriptSerializer().Serialize(new BankServiceResponse()
                {
                    StatusCode = SCode,
                    Status = Status,
                    Message = Message
                });
            return retMsg;


        }
        public string VerifyToken()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
            PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.NVarChar, 7) { Value = Merchantcode };
            string result = gf.ExecuteScaler(PARM, "EgSnaTokenVerify");
            return result;
        }

        private void InsertAuditTransaction()
        {
            try
            {

                UpdateGRN objUpdate = new UpdateGRN();
                string url = System.Web.HttpContext.Current.Request.UserHostName;

                //objUpdate.UserID = UserId;
                //objUpdate.Password = Password;
                objUpdate.BSRCode = Merchantcode.ToString();
                objUpdate.encdata = enctype.ToString();
                objUpdate.TransDate = DateTime.Now.Date;
                objUpdate.URL = url.ToString();
                objUpdate.GRN = GRN;
                objUpdate.InsertAudit();
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message.ToString();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
                SCode = "035";
            }
        }

        public void abc(string plaintext)
        {
            var valueDictionary = new List<string>();
            foreach (string a in plaintext.Split('|')) 
            {
                string[] b = a.Split('=');
                valueDictionary[0] = b[1]; //Your example was 1 base but I think it's should be a 0 base
            }
        }
    }

    class BankServiceResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ReferenceNo { get; set; }
        public string GRN { get; set; }
        public string Amount { get; set; }

    }
    class BankServiceErrorResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
    class BankService
    {
        //public string StatusCode { get; set; }
        //public string Status { get; set; }
        //public string Message { get; set; }
        //public string ReferenceNo { get; set; }
        public string GRN { get; set; }
        public string Amount { get; set; }

    }
    class VerifyGrnORRef
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Data { get; set; }
    }


   
}

