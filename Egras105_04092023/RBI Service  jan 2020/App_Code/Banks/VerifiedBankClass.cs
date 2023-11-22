using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

/// <summary>
/// Summary description for VerifiedBankClass
/// </summary>
public class VerifiedBankClass
{
    EncryptDecryptionBL objEncryption;
    private string flag;

    public Int64 GRN { get; set; }
    public double TotalAmount { get; set; }
    public string BSRCode { get; set; }
    public string PaymentMode { get; set; }
    public string Version { get; set; }
    bool result;

    public string Verify()
    {
        try
        {
          
            return BSRCode == "9920001" ? VerifyPineLab() : VerifyAllBank();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();

            obj.InsertError(ex.Message.ToString());
            return "Unable to Process GRN !!";
        }
    }


    public string VerifyAllBank()
    {
        string cipherText = "";
        string returnData = "";
        string GEN = "";
        string checkSum = string.Empty;
        //GRN = 29283819;
        if (GRN > Convert.ToInt64(ConfigurationManager.AppSettings["OldLastGRN"]))
        {
            GEN = GRN.ToString();
        }
        else
        {
            GEN = "000000000" + Convert.ToString(GRN);
            GEN = GEN.Substring(GEN.Length - 10, 10);
        }
        
        //TotalAmount = 319;
        string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", GEN.ToString(), TotalAmount.ToString());
        Banks objBank = Banks.SelectBanks(BSRCode, PaymentMode);
        objEncryption = new EncryptDecryptionBL();
         if(objBank.Version=="2.0")
          checkSum = objEncryption.GetSHA256(plainText);
         else
          checkSum = objEncryption.GetMD5Hash(plainText);


        objBank.TotalAmount = TotalAmount;
        objBank.checkSum = checkSum;
       

        cipherText = objBank.GetRequestString(plainText);
        InsertAudit(plainText, BSRCode, cipherText, flag);
        if (PaymentMode == "M")
            returnData = objBank.CallVerifyManualService(cipherText);
        else
            returnData = objBank.CallVerifyService(cipherText);
        if (returnData == null)
            return "Response is still awaited from bank side";
        
        
        UpdateStatus objUpdate = new global::UpdateStatus();
        objUpdate.isPG = objBank.isPG; 

        switch (BSRCode)
        {
            //status = success & txnid = 7119573 
            //& hash = 27df99aac0e71d3e042e26163f8ca9a0fe7b96cb76be76eb3996545d6d463e34d5f0055b4fde59bfc0112817e7a91365ba79bcbaa8cfd50b82ca6e692c9e013f 
            //& mode = CC & amount = 111.00 & mihpayid = 403993715518502687 
            //& cin = 100031210001625102018 & bank_ref_num = 908694 & error = E000 & bankcode = CC 
            //& addedon = 2018 - 10 - 25 12:55:19 & udf1 = 1000312 & udf2 = 0041
            case "9910001":
                Dictionary<string, string> PayuData = new Dictionary<string, string>();
                List<string> lstPlainText = new List<string>();
                lstPlainText = returnData.Split('&').ToList();
                if (lstPlainText[0] == "CorrectHash")
                {
                    PayuData.Add("TRANS_STATUS", lstPlainText[1].Split('=').GetValue(1).ToString().Substring(0, 1));///status
                    PayuData.Add("GRN", lstPlainText[2].Split('=').GetValue(1).ToString());///txnid
                    PayuData.Add("hash", lstPlainText[3].Split('=').GetValue(1).ToString());////hash
                    PayuData.Add("payMode", lstPlainText[4].Split('=').GetValue(1).ToString());////mode
                    PayuData.Add("PAID_AMT", lstPlainText[5].Split('=').GetValue(1).ToString()); ///amount
                    PayuData.Add("BankReferenceNo", lstPlainText[6].Split('=').GetValue(1).ToString());/////mihpayid
                    PayuData.Add("CIN", lstPlainText[7].Split('=').GetValue(1).ToString()); ////////cin
                    PayuData.Add("bankRefNo", lstPlainText[8].Split('=').GetValue(1).ToString());//////bank_ref_num
                    PayuData.Add("reason", lstPlainText[9].Split('=').GetValue(1).ToString());////error
                    PayuData.Add("PayUBSRCode", lstPlainText[10].Split('=').GetValue(1).ToString());///////bankcode
                    PayuData.Add("PAID_DATE", lstPlainText[11].Split('=').GetValue(1).ToString());////addedon
                    PayuData.Add("BANK_CODE", lstPlainText[12].Split('=').GetValue(1).ToString());//////udf1
                    PayuData.Add("Head", lstPlainText[13].Split('=').GetValue(1).ToString());//////udf2


                    objUpdate.GRNData = PayuData;
                    result = objUpdate.UpdateGRNStatus_PayU();
                }
                else
                {
                    return "Incorrect hash value.";
                }

                break;

            case "9930001":
               
                List<string> lstPlainTextPayTM = new List<string>();
                string PlainText =  BanksEncryptionDecryption.GetDecryptedString(returnData, objBank.KeyName,objBank.Version);
                Dictionary<string, string> PayTMData = new Dictionary<string, string>();
                lstPlainText = PlainText.Split('|').ToList();

                //GRN ={ 0}| BANK_CODE ={ 1}| BankReferenceNo ={ 2}| CIN ={ 3}| PAID_DATE ={ 4}| PAID_AMT ={ 5}| TRANS_STATUS ={ 6},DebitBankCode ={ 7},BankRefNo ={ 8},PayMode ={ 9},Reason ={ 10}
                PayTMData.Add("GRN", lstPlainText[0].Split('=').GetValue(1).ToString());
                PayTMData.Add("BANK_CODE", lstPlainText[1].Split('=').GetValue(1).ToString());
                PayTMData.Add("BankReferenceNo", lstPlainText[2].Split('=').GetValue(1).ToString());
                PayTMData.Add("CIN", lstPlainText[3].Split('=').GetValue(1).ToString());
                PayTMData.Add("PAID_DATE", lstPlainText[4].Split('=').GetValue(1).ToString());
                PayTMData.Add("PAID_AMT", lstPlainText[5].Split('=').GetValue(1).ToString());
                PayTMData.Add("TRANS_STATUS", lstPlainText[6].Split('=').GetValue(1).ToString());
                PayTMData.Add("PayUBSRCode", lstPlainText[7].Split('=').GetValue(1).ToString());///////bankcode
                PayTMData.Add("bankRefNo", lstPlainText[8].Split('=').GetValue(1).ToString());////addedon
                PayTMData.Add("payMode", lstPlainText[9].Split('=').GetValue(1).ToString());//////udf1
                PayTMData.Add("reason", lstPlainText[10].Split('=').GetValue(1).ToString());////error
                objUpdate.GRNData = PayTMData;
                result = objUpdate.UpdateGRNStatus_PayU();
                

                break;
            default:
                UploadData(BSRCode, returnData, GRN, objBank.IpAddress, PaymentMode);
                objUpdate.CipherText = returnData;
                objUpdate.KeyName = objBank.KeyName;
                objUpdate.GRN = GRN;
                objUpdate.TotalAmount = TotalAmount;
                objUpdate.Version = objBank.Version;
                result = objUpdate.UpdateGRNStatus();
                break;
        }
        if (!result)
            objUpdate.Message = "Due to some technical issue Unable to process";
        //plainText = DecryptResponseString(returnData);
        //message = UpdateStatus(plainText, GEN, TotalAmount.ToString());
        return objUpdate.Message;
    }
    private string VerifyPineLab()
    {
        string cipherText = "";
        string returnData = "";
        string GEN = "";
     
        if (GRN > Convert.ToInt64(ConfigurationManager.AppSettings["OldLastGRN"]))
        {
            GEN = GRN.ToString();
        }
        else
        {
            GEN = "000000000" + Convert.ToString(GRN);
            GEN = GEN.Substring(GEN.Length - 10, 10);
        }

        //TotalAmount = 319;
        string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", GEN.ToString(), TotalAmount.ToString());
        Banks objBank = Banks.SelectBanks(BSRCode);
        SortedList<string, string> postRequestData = new SortedList<string, string>();
        postRequestData["GRN"] = GEN.ToString();
        postRequestData["Amount"] = String.Format("{0:0.00}", TotalAmount);
        PineLab objPineLab = new PineLab();
        cipherText = "{\"GRN\":\"" + GEN.ToString() + "\",\"Amount\":\"" + String.Format("{0:0.00}", TotalAmount) + "\",\"SecurityToken\":\"" + objPineLab.GetPineLabRequestString(postRequestData) + "\"}";
        InsertAudit(plainText, BSRCode, cipherText, flag);
        returnData = objBank.CallVerifyService(cipherText);
        if (returnData == null)
            return "Response is still awaited from bank side";

        //returnData = "XJmCX7rBnXrmqzb6LBXHWDCu0u7sNq0Xb0v7WP9dObuoZutdj9rOiyvRDZ/ZBj/WzliZKOW3FuLnZkx6eUk9OWTDW6cTrRJX1PZRJgHOb5HvkYJ2BERPB2pJjksUJKCpMVIXjX4xPc2tfdR8xZaH035JFJAMXdwEsZMLKMvGQiIwX1rE5K0GXDq21HMX/drkhJkpcBAgh5664KJ9EASAicHmB8ATrouktWogzU2PgbQWyxWKGskVk3n6DHkQ1Bq5ubaA4MeoXijYC+uZRb+IaQ==";
        UpdateStatus objUpdate = new global::UpdateStatus();
        var jsonResponse = JsonConvert.DeserializeObject<PineLabs>(returnData);
        Dictionary<string, string> PineLabData = new Dictionary<string, string>();
        SortedList<string, string> checkPostRequestData = new SortedList<string, string>();
        checkPostRequestData["GRN"] = jsonResponse.GRN.ToString();
        checkPostRequestData["Bank_Code"] = jsonResponse.Bank_Code;
        checkPostRequestData["BankReferenceNo"] = jsonResponse.BankReferenceNo;
        checkPostRequestData["CIN"] = jsonResponse.CIN;
        checkPostRequestData["Paid_Date"] = jsonResponse.Paid_Date.ToString();
        checkPostRequestData["Paid_Amt"] = String.Format("{0:0.00}", jsonResponse.Paid_Amt);
        checkPostRequestData["TRANS_STATUS"] = jsonResponse.TRANS_STATUS;
        checkPostRequestData["GT_BSRCode"] = jsonResponse.GT_BSRCode;
        checkPostRequestData["BankRefNo"] = jsonResponse.BankRefNo;
        checkPostRequestData["PayMode"] = jsonResponse.PayMode;
        var EncrypedString = objPineLab.GetPineLabRequestString(checkPostRequestData);
        if (jsonResponse.SecurityToken.Trim() == EncrypedString.Trim())
        {
            PineLabData.Add("TRANS_STATUS", jsonResponse.TRANS_STATUS);///status
            PineLabData.Add("GRN", jsonResponse.GRN.ToString());///txnid
            PineLabData.Add("payMode", jsonResponse.PayMode);////mode
            PineLabData.Add("PAID_AMT", jsonResponse.Paid_Amt.ToString()); ///amount
            PineLabData.Add("BankReferenceNo", jsonResponse.BankReferenceNo);/////mihpayid
            PineLabData.Add("CIN", jsonResponse.CIN.Trim()); ////////cin
            PineLabData.Add("bankRefNo", jsonResponse.BankRefNo);//////bank_ref_num
            PineLabData.Add("reason", jsonResponse.Reason);////error
            PineLabData.Add("PayUBSRCode", jsonResponse.GT_BSRCode);///////bankcode
            PineLabData.Add("PAID_DATE", jsonResponse.Paid_Date.ToString());////addedon
            PineLabData.Add("BANK_CODE", jsonResponse.Bank_Code);//////udf1

            objUpdate.GRNData = PineLabData;
            result = objUpdate.UpdateGRNStatus_PayU();
        }
        else
        {
            result = true;
            objUpdate.Message = "Security Token Does Not Match !";
        }
        if (!result)
            objUpdate.Message = "Due to some technical issue Unable to process";
        return objUpdate.Message;
    }
    private void InsertAudit(string plainText, string BankCode, string cipherText, string flag)
    {
        EgFrmTOVerified ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        ObjFrm.encData = cipherText;
        ObjFrm.plainText = plainText;
        ObjFrm.flag = flag;
        ObjFrm.BankServiceAuditData();
    }
    private void UploadData(string BankCode, string encData, Int64 GRn, string IpAddress, string Paymenttype)
    {
        EgFrmTOVerified ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        ObjFrm.encData = encData;
        ObjFrm.Ipaddress = IpAddress;
        ObjFrm.GRN = GRn;
        ObjFrm.paymentType = Paymenttype;
        ObjFrm.BankResponseAudit();
    }
}