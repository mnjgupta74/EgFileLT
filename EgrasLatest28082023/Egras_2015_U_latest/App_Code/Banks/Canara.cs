﻿using EgBL;
using System;
using System.Configuration;
using System.Net;
/// <summary>
/// Summary description for Canara
/// </summary>
public class Canara : Banks
{
    public Canara()
    {
        KeyName = "CANARA";
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
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        try
        {
            CanaraWebServ.VerifyTxn objCanara = new CanaraWebServ.VerifyTxn();
            CanaraWebServ.AuthHeader objAuthHeader = new CanaraWebServ.AuthHeader();
            objAuthHeader.UserName = "CanRajeGRAS";
            objAuthHeader.Password = "Can@GBM#2018";
            objCanara.AuthHeaderValue = objAuthHeader;
            CanaraWebServ.verifyResponse objResponse = new CanaraWebServ.verifyResponse();
            IpAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["CanaraWebServ.VerifyTxn"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
            objResponse = objCanara.getverifyStatus(cipherText);
            return objResponse.encdata;
        }
        catch (Exception ex)
        {
            InsertErrorLog(ex.Message, "0240539", ConfigurationManager.AppSettings["CanaraWebServ.VerifyTxn"].Split('/').GetValue(2).ToString().Replace("www.", ""), 1);
            return null;
        }
    }

    //protected override string DecryptResponseString(string cipherText)
    //{
    //    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
    //    return objEncry.Decrypt(cipherText, System.Web.HttpContext.Current.Server.MapPath("../WebPages/Key/" + KeyName + ".key"));
    //}

    //protected override string GetBankForwardString()
    //{
    //    string plainText = string.Format("GRN={0}|HEAD_OF_ACC1={1}|AMT1={2}|HEAD_OF_ACC2={3}|AMT2={4}|HEAD_OF_ACC3={5}|AMT3={6}|HEAD_OF_ACC4={7}|AMT4={8}|HEAD_OF_ACC5={9}|AMT5={10}|HEAD_OF_ACC6={11}|AMT6={12}|HEAD_OF_ACC7={13}|AMT7={14}|HEAD_OF_ACC8={15}|AMT8={16}|HEAD_OF_ACC9={17}|AMT9={18}|REMITTER_NAME={19}|TOTALAMOUNT={20}|PayMode={21}|REG-TIN-NO={22}|LocationCode={23}|Filler={24}", GRN.ToString(), Head_Name[0], Head_Amount[0], Head_Name[1], Head_Amount[1], Head_Name[2], Head_Amount[2], Head_Name[3], Head_Amount[3], Head_Name[4], Head_Amount[4], Head_Name[5], Head_Amount[5], Head_Name[6], Head_Amount[6], Head_Name[7], Head_Amount[7], Head_Name[8], Head_Amount[8], RemitterName, TotalAmount, PaymentMode, TIN, LocationCode, flag);
    //    EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
    //    checkSum = objEncryption.GetMD5Hash(plainText);
    //    return plainText + "|checkSum=" + checkSum;
    //}

    //protected override string GetRequestString(string plainText)
    //{
    //    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
    //    return objEncry.Encrypt(plainText + "|checkSum=" + checkSum, System.Web.HttpContext.Current.Server.MapPath("../WebPages/Key/" + KeyName + ".key"));
    //}
}