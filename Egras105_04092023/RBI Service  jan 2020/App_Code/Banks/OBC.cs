using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

/// <summary>
/// Summary description for OBC
/// </summary>
public class OBC : Banks
{
    public OBC()
    {
        KeyName = "OBC";
        //Version = "2.0";
    }

    public override string CallManualDataPushService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyManualService(string cipherText)
    {
        throw new NotImplementedException();
    }

    public override string CallVerifyService(string cipherText)
    {

        double Amount = 0.0;
        Int64 GRN = 0;

        SbiEncryptionDecryption ObjEncrytDecrypt = new SbiEncryptionDecryption();
        string plaintext = ObjEncrytDecrypt.DecryptSBIWithKey256(cipherText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "OBC.key",null);
        List<string> GRNvalue = plaintext.Split('|').ToList();
        if (GRNvalue != null && GRNvalue.Count > 0)
        {
            string[] aa = GRNvalue[0].ToString().Split('=');
            GRN = Convert.ToInt64(aa[1].ToString());
            string[] ab = GRNvalue[1].ToString().Split('=');
            Amount = Convert.ToDouble(ab[1].ToString());
        }
        string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}",
         GRN.ToString(), "0361193", "OBC12345", "036005714654654000010", DateTime.Now, Amount.ToString(), "S");


        EncryptDecryptionBL ObjEncrcryptDecrypt = new EncryptDecryptionBL();
        string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        RemoteClass myremotepost = new RemoteClass();
        plainText = plainText + "|checkSum=" + CheckSum;
        //string cipherText = objEncry.Encrypt(plainText, Server.MapPath("../WebPages/Key/RAJASTHAN_EGRASS.key"));
        string cipherText1 = objEncry.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "OBC.key");

        return cipherText1;
        //try
        //{
        //    using (SBIWebServ.DoubleVerifyMerchantService SbiService = new SBIWebServ.DoubleVerifyMerchantService())
        //    {
        //        IpAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
        //        cipherText = cipherText + "^" + "RAJASTHAN_EGRASS";
        //        return SbiService.DoubleVerification(cipherText);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    InsertErrorLog(ex.Message, "0006326", ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", ""), 1);
        //    return null;
        //}
        //===================================================
        //try
        //{
        //    using (OBCWebServ.Service1 OBCService = new OBCWebServ.Service1())
        //    {
        //        IpAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["OBCWebServ.Service1"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
        //        return OBCService.VerificationIncomingParamsRJS(cipherText);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    InsertErrorLog(ex.Message, "0361193", ConfigurationManager.AppSettings["OBCWebServ.Service1"].Split('/').GetValue(2).ToString().Replace("www.", ""), 1);
        //    return null;
        //}
    }

    public override Dictionary<string, string> GetBankForwardString()
    {
        string plainText = string.Format("HEAD_OF_ACC9={17}|HEAD_OF_ACC8={15}|HEAD_OF_ACC7={13}|HEAD_OF_ACC6={11}|HEAD_OF_ACC5={9}|HEAD_OF_ACC4={7}|HEAD_OF_ACC3={5}|HEAD_OF_ACC2={3}|HEAD_OF_ACC1={1}|TOTALAMOUNT={20}|Filler={24}|LocationCode={23}|GRN={0}|PayMode={21}|AMT9={18}|AMT8={16}|AMT7={14}|AMT6={12}|AMT5={10}|AMT4={8}|AMT3={6}|REMITTER_NAME={19}|AMT2={4}|AMT1={2}|REG-TIN-NO={22}", GRN.ToString(), Head_Name[0], Head_Amount[0], Head_Name[1], Head_Amount[1], Head_Name[2], Head_Amount[2], Head_Name[3], Head_Amount[3], Head_Name[4], Head_Amount[4], Head_Name[5], Head_Amount[5], Head_Name[6], Head_Amount[6], Head_Name[7], Head_Amount[7], Head_Name[8], Head_Amount[8], RemitterName, TotalAmount, PaymentMode, TIN, LocationCode, filler);
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        checkSum = objEncryption.GetMD5Hash(plainText);
        plainText = plainText + "|checkSum=" + checkSum;
        string EncData = BanksEncryptionDecryption.GetEncryptedString(plainText, KeyName,Version);
        return GetforwardDictionary(EncData);
    }
}