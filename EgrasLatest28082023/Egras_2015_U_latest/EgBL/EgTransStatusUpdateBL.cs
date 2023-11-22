
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using Update;

namespace EgBL
{

    public class EgTransStatusUpdateBL
    {
        public string version { get; set; }
        private string SCode { get; set; }
        public string key { get; set; }
        public string enctype { get; set; }
        public string Merchantcode { get; set; }
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
        public string checksum { get; set; }
        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        EgErrorHandller objError = new EgErrorHandller();
        SNAMerchantCode objSNA = new SNAMerchantCode();
        BanksEncryptionDecryptionBL objEcnDecBL = new BanksEncryptionDecryptionBL();
        public string ProcessData()
        {
            return Validation(GetStatusCode());
        }
        public string ProcessURL()
        {
            return Validation(GetStatusCodeForURL());
        }


        //Pine Lab Start


        //Pine Lab END
        private string GetStatusCode()
        {

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


                key = objSNA.GetKey(mkey);

                if (string.IsNullOrEmpty(key))
                    return SCode = "006";// Get Encryption/Dectyption Key Name

                SCode = VerifyToken();
                if (SCode != "200T")
                    return SCode == "006" ? "020" : SCode;
                SCode = InsertData(mkey);

                return SCode;


            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message.ToString();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("GRN= " + GRN + " and Exceptoin : " + ex.Message.ToString());
                return SCode = "035";
            }
            //=======================================
        }
        private string GetStatusCodeForURL()
        {
            if (string.IsNullOrEmpty(enctype))
                return SCode = "002";//  Parameter Empty

            //version = Auth.Split('|')[2];
            //if (string.IsNullOrEmpty(version))
            //    return SCode = "003";// Get Version

            if (string.IsNullOrEmpty(key))
                return SCode = "006";// Get Encryption/Dectyption Key Name

            if (string.IsNullOrEmpty(Merchantcode))
                return SCode = "004";// Get Merchant Key
            if (Merchantcode == "1000132")
            {
                SCode = ePayInsertData(Merchantcode);
            }
            else
            {
                SCode = InsertData(Merchantcode);
            }


            return SCode;
        }
        private string InsertData(string mkey)
        {
            try
            {
                List<string> lstPlainText = new List<string>();
                string PlainText = objEcnDecBL.GetDecryptedString(enctype, key, version);


                lstPlainText = PlainText.Split('|').ToList();

                if (string.IsNullOrEmpty(PlainText))
                    return SCode = "007";// Get Plain String 
                try
                {
                    EgDeptIntegrationPropBL objdeptintegratin = new EgDeptIntegrationPropBL();
                    //GRN
                    if (lstPlainText[0].Split('=').GetValue(1).ToString() == "0")
                        return SCode = "008";
                    else
                    {
                        try
                        {
                            GRN = objdeptintegratin.GRN(lstPlainText[0].Split('=').GetValue(1).ToString());
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
                        BANK_CODE = objdeptintegratin.BankName(lstPlainText[1].Split('=').GetValue(1).ToString());
                    }
                    // BankReference


                    if (lstPlainText[6].Split('=').GetValue(1).ToString().ToUpper() == "S" && (lstPlainText[2].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[2].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[2].Split('=').GetValue(1).ToString())))
                    {
                        return SCode = "009";
                    }
                    else
                    {
                        BankReferenceNo = objdeptintegratin.refNo(lstPlainText[2].Split('=').GetValue(1).ToString());
                    }
                    // CIN
                    if (lstPlainText[6].Split('=').GetValue(1).ToString().ToUpper() == "S" && (lstPlainText[3].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[3].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[3].Split('=').GetValue(1).ToString().ToUpper())))
                    {
                        return SCode = "011";
                    }
                    else
                    {
                        CIN = objdeptintegratin.CINNumber(lstPlainText[3].Split('=').GetValue(1).ToString());
                    }
                    // PAID_DATE
                    //
                    if (!IsDateTime(lstPlainText[4].Split('=').GetValue(1).ToString()) || lstPlainText[4].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[4].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[4].Split('=').GetValue(1).ToString()))
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
                    if (lstPlainText[5].Split('=').GetValue(1).ToString() == "0" || lstPlainText[5].Split('=').GetValue(1).ToString().Length > 14)
                        return SCode = "013";
                    else
                    {
                        try
                        {
                            PAID_AMT = objdeptintegratin.amount(lstPlainText[5].Split('=').GetValue(1).ToString());
                            //if (Convert.ToDouble(lstPlainText[5].Split('=').GetValue(1)) > 0)
                            //    PAID_AMT = Convert.ToDouble(lstPlainText[5].Split('=').GetValue(1));
                            //else
                            //    return SCode = "013";
                        }
                        catch
                        {
                            return SCode = "013";
                        }
                    }
                    // TRANS_STATUS
                    if (lstPlainText[6].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[6].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[6].Split('=').GetValue(1).ToString()))
                    {
                        return SCode = "014";
                    }
                    else
                    {
                        TRANS_STATUS = objdeptintegratin.status(lstPlainText[6].Split('=').GetValue(1).ToString().ToUpper());
                    }

                    // chech Bank code with BSR Code in Header
                    if (mkey.Trim() != BANK_CODE.Trim())
                    {
                        return "023";
                    }
                    SCode = VerifyGrnStatus();
                    if (SCode.Trim() != "200G")
                        return SCode;

                    //int indexCheckSum = 7;
                    //if (lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString()))
                    //{
                    //    return SCode = "022";
                    //}
                    //else
                    //{
                    //    checksum = (lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString());
                    //}
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message.ToString();
                    EgErrorHandller objinner = new EgErrorHandller();
                    objinner.InsertError("GRN= " + GRN + " and Exceptoin : " + ex.Message.ToString());
                    return SCode = "037";
                }
                #region ENABLE WHEN PG WOULD BE STARTED- THIS SECTION FOR PG ONLY

                flag = objSNA.GetParameterListCountFlag(BANK_CODE.Trim().ToString());

                //checksum
                //GRN=7485856|BANK_CODE=9950001|BankReferenceNo=12345678899|CIN=BLDK20310493150920236|
                //PAID_DATE =2023/09/15 11:04:57|PAID_AMT=11|transactionStatus=S|
                //DebitBankCode =CNRB|bankrefnumber=UHDF0001468828|PayMode=N|reason=NA|
                //checksum=a8535ca6cc3d978a9c55136363e8fa3ceb1532385d6563fe3d53bf9f5b7311a7
                int indexCheckSum = flag == "1" ? 11 : 7;
                if (lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString().Length == 0 || lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString()))
                {
                    return SCode = "022";
                }
                else
                {
                    checksum = (lstPlainText[indexCheckSum].Split('=').GetValue(1).ToString());
                }

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
                #endregion

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
                    //Check GRN is Online(IF Online or Pending Then 1) Or Other Then 0  
                    int CheckGRNISOnline = objEgEChallanBankBL.CheckGRNValidation();
                    int result = 0;
                    if (CheckGRNISOnline == 1)
                    {
                        result = objEgEChallanBankBL.UpdateSuccessStatus();
                    }
                    else
                    {
                        return SCode = "200U";
                    }


                    //objError.InsertError("result= " + result);

                    //SCode = result == 1 ? "200" : "019";

                    #region ENABLE WHEN PG WOULD BE STARTED
                    SCode = result == 1 && flag == "0" ? "200" : result == 1 && flag == "1" ? "200U" : "019";


                    if (SCode == "200U" && flag == "1" && (BANK_CODE == "9910001"|| BANK_CODE == "9950001"))
                    {
                        objEgEChallanBankBL.epayBSRCode = DebitBankCode;//////DebitBankCode
                        objEgEChallanBankBL.bankRefNo = BankRefNo;///////BankRefNo
                        objEgEChallanBankBL.payMode = PayMode;///////Transtype
                        objEgEChallanBankBL.Reason = Reason;
                        int res = objEgEChallanBankBL.UpdatePAYUStatus();
                        SCode = res == 1 ? "200" : "019";
                    }
                    if (SCode == "200U" && flag == "1" && BANK_CODE == "1000132")
                    {
                        objEgEChallanBankBL.epayBSRCode = DebitBankCode;//////DebitBankCode
                        objEgEChallanBankBL.bankRefNo = BankRefNo;///////BankRefNo
                        objEgEChallanBankBL.payMode = PayMode;///////Transtype
                        //objEgEChallanBankBL.Reason = Reason;
                        int res = objEgEChallanBankBL.UpdateEpayStatus();
                        SCode = res == 1 ? "200" : "019";
                    }
                    #endregion
                }
                catch (Exception exe)
                {
                    exceptionMessage = exe.Message.ToString();
                    EgErrorHandller objError1 = new EgErrorHandller();
                    objError1.InsertError(exe.Message.ToString());
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
                obj.InsertError("GRN= " + GRN + " and Exceptoin : " + ex.Message.ToString());
                return SCode = "035";
            }
        }
        private string ePayInsertData(string mkey)
        {
            try
            {
                List<string> lstPlainText = new List<string>();
                string PlainText = objEcnDecBL.GetDecryptedString(enctype, key, version);


                lstPlainText = PlainText.Split('|').ToList();
                //string PlainText = "8305348|0|FAIL|00.0|NA|NA|NA|No Records Found|0|0|NA|NA|0|1000132|0.00^0.00||||||||||";
                if (string.IsNullOrEmpty(PlainText))
                    return SCode = "007";// Get Plain String 
                try
                {
                    EgDeptIntegrationPropBL objdeptintegratin = new EgDeptIntegrationPropBL();
                    //GRN
                    if (lstPlainText[0].ToString() == "0")
                        return SCode = "008";
                    else
                    {
                        try
                        {
                            GRN = objdeptintegratin.GRN(lstPlainText[0].ToString());
                        }
                        catch
                        {
                            return SCode = "008";
                        }
                    }
                    // BankCode
                    if (lstPlainText[13].ToString().Trim().Length != 7)
                    {
                        return SCode = "010";
                    }
                    else
                    {
                        BANK_CODE = objdeptintegratin.BankName(lstPlainText[13].ToString());
                    }
                    // BankReference


                    if (lstPlainText[2].Substring(0, 1).ToUpper() == "S" && (lstPlainText[1].ToString().Length == 0 || lstPlainText[1].ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[1].ToString())))
                    {
                        return SCode = "009";
                    }
                    else
                    {
                        BankReferenceNo = objdeptintegratin.refNo(lstPlainText[1].ToString());
                    }
                    // CIN
                    if (lstPlainText[2].Substring(0, 1).ToUpper() == "S" && (lstPlainText[12].ToString().Length == 0 || lstPlainText[12].ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[12].ToString().ToUpper())))
                    {
                        return SCode = "011";
                    }
                    else
                    {
                        CIN = objdeptintegratin.CINNumber(lstPlainText[12].ToString());
                    }
                    // PAID_DATE
                    //
                    if (!IsDateTime(lstPlainText[10].ToString()) || lstPlainText[10].ToString().Length == 0 || lstPlainText[10].ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[10].ToString()))
                        return SCode = "012";
                    else
                    {
                        try
                        {
                            PAID_DATE = Convert.ToDateTime(lstPlainText[10].ToString());
                        }
                        catch
                        {
                            return SCode = "012";
                        }
                    }
                    // PAID_Amt
                    if (lstPlainText[3].ToString() == "0" || lstPlainText[3].ToString().Length > 14)
                        return SCode = "013";
                    else
                    {
                        try
                        {
                            PAID_AMT = objdeptintegratin.amount(lstPlainText[3].ToString());

                        }
                        catch
                        {
                            return SCode = "013";
                        }
                    }
                    // TRANS_STATUS
                    if (lstPlainText[2].Substring(0, 1).Length == 0 || lstPlainText[2].Substring(0, 1).ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[2].Substring(0, 1)))
                    {
                        return SCode = "014";
                    }
                    else
                    {
                        TRANS_STATUS = objdeptintegratin.status(lstPlainText[2].Substring(0, 1).ToUpper());
                    }

                    // chech Bank code with BSR Code in Header
                    if (mkey.Trim() != BANK_CODE.Trim())
                    {
                        return "023";
                    }
                    SCode = VerifyGrnStatus();
                    if (SCode.Trim() != "200G")
                        return SCode;
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message.ToString();
                    EgErrorHandller objinner = new EgErrorHandller();
                    objinner.InsertError("GRN= " + GRN + " and Exceptoin : " + ex.Message.ToString());
                    return SCode = "037";
                }
                #region ENABLE WHEN PG WOULD BE STARTED- THIS SECTION FOR PG ONLY
                flag = objSNA.GetParameterListCountFlag(BANK_CODE.Trim().ToString());

                //checksum

                int indexCheckSum = flag == "1" ? 11 : 7;
                if (lstPlainText[indexCheckSum].ToString().Length == 0 || lstPlainText[indexCheckSum].ToString().ToUpper() == "NULL" || string.IsNullOrWhiteSpace(lstPlainText[indexCheckSum].ToString()))
                {
                    return SCode = "022";
                }
                else
                {
                    checksum = (lstPlainText[indexCheckSum].ToString());
                }

                if (flag == "1")
                {
                    // DebitBankCode
                    if (lstPlainText[8].ToString() == "0")
                    {
                        return SCode = "015";
                    }
                    else
                    {
                        DebitBankCode = (lstPlainText[8].ToString());
                    }
                    // BankRefNo
                    if (lstPlainText[9].ToString() == "0")
                    {
                        return SCode = "016";
                    }
                    else
                    {
                        BankRefNo = (lstPlainText[9].ToString());
                    }
                    // PayMode
                    if (lstPlainText[5].ToString() == "0")
                    {
                        return SCode = "017";
                    }
                    else
                    {
                        PayMode = (lstPlainText[5].ToString());
                    }
                    //// Reason
                    //if (lstPlainText[10].ToString() == "0")
                    //{
                    //    return SCode = "018";
                    //}
                    //else
                    //{
                    //    Reason = (lstPlainText[10].ToString());
                    //}
                }
                #endregion

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

                    //objError.InsertError("result= " + result);

                    //SCode = result == 1 ? "200" : "019";

                    #region ENABLE WHEN PG WOULD BE STARTED
                    SCode = result == 1 && flag == "0" ? "200" : result == 1 && flag == "1" ? "200U" : "019";


                    //if (SCode == "200U" && flag == "1" && BANK_CODE == "9910001")
                    //{
                    //    objEgEChallanBankBL.epayBSRCode = DebitBankCode;//////DebitBankCode
                    //    objEgEChallanBankBL.bankRefNo = BankRefNo;///////BankRefNo
                    //    objEgEChallanBankBL.payMode = PayMode;///////Transtype
                    //    objEgEChallanBankBL.Reason = Reason;
                    //    int res = objEgEChallanBankBL.UpdatePAYUStatus();
                    //    SCode = res == 1 ? "200" : "019";
                    //}
                    if (SCode == "200U" && flag == "1" && BANK_CODE == "1000132")
                    {
                        objEgEChallanBankBL.epayBSRCode = DebitBankCode;//////DebitBankCode
                        objEgEChallanBankBL.bankRefNo = BankRefNo;///////BankRefNo
                        objEgEChallanBankBL.payMode = PayMode;///////Transtype
                                                              //objEgEChallanBankBL.Reason = Reason;
                        int res = objEgEChallanBankBL.UpdateEpayStatus();
                        SCode = res == 1 ? "200" : "019";
                    }
                    #endregion

                }
                catch (Exception exe)
                {
                    exceptionMessage = exe.Message.ToString();
                    EgErrorHandller objError1 = new EgErrorHandller();
                    objError1.InsertError(exe.Message.ToString());
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
                obj.InsertError("GRN= " + GRN + " and Exceptoin : " + ex.Message.ToString());
                return SCode = "035";
            }
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
                    Status = "BankCode"; Message = "BankCode Length Should be 7 and greater than 1";
                    break;
                case "011":
                    Status = "CIN"; Message = "CIN Could not be null or blank ";
                    break;
                case "012":
                    Status = "Paid_Date"; Message = "Paid date could not be null or Not in Proper format";
                    break;
                case "013":
                    Status = "Paid_Amt"; Message = "Paid Amt  could not be 0  or Not in Proper format";
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
                case "022":
                    Status = "HFCS"; Message = "CheckSum could not be null or not in proper format";
                    break;
                case "023":
                    Status = "HSBSRM"; Message = "Bank Code Mismatch";
                    break;
                case "035":
                    Status = "Exception"; Message = "Due to some Technical Error";
                    break;
                case "036":
                    Status = "Exception"; Message = "GRN already updated";
                    break;
                case "037":
                    Status = "message"; Message = exceptionMessage;
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
            retMsg = SCode == "200" ?
                new JavaScriptSerializer().Serialize(new BankServiceErrorResponse()
                {
                    StatusCode = SCode,
                    Status = Message,
                    Message = objEcnDecBL.GetEncryptedString(new JavaScriptSerializer().Serialize(new BankService()
                    {
                        GRN = GRN.ToString(),
                        Amount = PAID_AMT.ToString()
                    }),key,version)
                })
                  :
                new JavaScriptSerializer().Serialize(new BankServiceResponse()
                {
                    StatusCode = SCode,
                    Status = Status,
                    Message = Message
                });
            return retMsg;
            //SCode = result == 1 && flag == "0" ? "200" : result == 1 && flag == "1" ? "200U" : "019";
            //retMsg = SCode == "200" ?
            //    new JavaScriptSerializer().Serialize(new BankServiceResponse()
            //    {
            //        StatusCode = SCode,
            //        Status = Message,
            //        Message = objEcnDecBL.GetEncryptedString(new JavaScriptSerializer().Serialize(new BankService()
            //        {
            //            GRN = GRN.ToString(),
            //            Amount = PAID_AMT.ToString()
            //        }), key, version)
            //    })
            //      :
            //    new JavaScriptSerializer().Serialize(new BankServiceResponse()
            //    {
            //        StatusCode = SCode,
            //        Status = Status,
            //        Message = Message
            //    });
            //objDecrypt.EncryptSBIWithKey256(new JavaScriptSerializer().Serialize(new BankServiceResponse()
            //{
            //    StatusCode = SCode,
            //    Status = Status,
            //    Message = Message,
            //    GRN = GRN.ToString(),
            //    Amount = PAID_AMT.ToString()
            //}), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key") :
            //new JavaScriptSerializer().Serialize(new BankServiceErrorResponse()
            //{
            //    StatusCode = SCode,
            //    Status = Status,
            //    Message = Message
            //});
            //return retMsg;


        }
        public string VerifyToken()
        {
            string result;
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[2];
                gf = new GenralFunction();
                PARM[0] = new SqlParameter("@token", SqlDbType.NVarChar, 500) { Value = token };
                PARM[1] = new SqlParameter("@MerchantCode", SqlDbType.NVarChar, 7) { Value = Merchantcode };
                result = gf.ExecuteScaler(PARM, "EgSnaTokenVerify");

            }
            catch (Exception ex)
            {
                result = "Unable To Process Token Verification !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
            }
            return result;
        }
        public string VerifyGrnStatus()
        {
            string result;
            try
            {
                GenralFunction gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[2];
                gf = new GenralFunction();
                PARM[0] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = GRN };
                PARM[1] = new SqlParameter("@amount", SqlDbType.Money) { Value = PAID_AMT };
                result = gf.ExecuteScaler(PARM, "EgCheckGrnTransactionStatus");
            }
            catch (Exception ex)
            {
                result = "Unable To Process GRN Verify Status !";
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message.ToString());
            }
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
        public bool IsDateTime(string tempDate)
        {
            try
            {
                DateTime fromDateValue;
                //var formats = new[] { "yyyy-dd-MM h:mm tt" };
                string[] formats = { "yyyy/MM/dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss" };
                //var formats = "yyyy-MM-dd h:mm:ss";
                fromDateValue = DateTime.ParseExact(tempDate, formats,
                                         new CultureInfo("en-US"),
                                         DateTimeStyles.None);
                if (DateTime.TryParseExact(tempDate, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue) && Convert.ToDateTime(tempDate).Year > 1900)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    class BankServiceErrorResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
    class BankService
    {
        public string GRN { get; set; }
        public string Amount { get; set; }

    }
    class BankServiceResponse
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
    class VerifyGrnORRef
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Data { get; set; }
    }




}

