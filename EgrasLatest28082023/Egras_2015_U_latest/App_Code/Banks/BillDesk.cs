﻿using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for BillDesk
/// </summary>
public class BillDesk : Banks
{
    private string BankCode { get; set; }
    public BillDesk()
    {
        //
        KeyName = "Billdesk";
        BankCode = "9950001";
        Version = "2.0";
        isPG = true;
        //
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
        var response = string.Empty;
        string URI = "https://uat1.billdesk.com/easytax/api/v1/egras/odv/RJ/CNRB";
        string myParameters = cipherText;
        using (WebClient wc = new WebClient())
        {
            response = wc.UploadString(URI, "POST", myParameters);
        }
        return response;
    }
    public override Dictionary<string, string> GetBankForwardString()
    {
        //string plainText = string.Format("GRN={0}|HEAD_OF_ACC={1}|AMT={2}|REMITTER_NAME={3}|UDF1={4}|Filler={5}", GRN.ToString(), Head_Name[0], Head_Amount[0], RemitterName, BsrCode.ToString(), filler);
        string plainText = string.Format("GRN={0}|HEAD_OF_ACC={1}|AMT={2}|REMITTER_NAME={3}|TOTALAMOUNT={4}|PayMode={5}|REG-TIN-NO={6}|LocationCode={7}|Filler={8}", GRN.ToString(), Head_Name[0].Substring(0, 4), Head_Amount[0], RemitterName, TotalAmount.ToString(), PaymentMode, TIN, LocationCode, filler);
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        checkSum = objEncryption.GetSHA256(plainText);
        plainText = plainText + "|checksum=" + checkSum;
        string EncData = BanksEncryptionDecryption.GetEncryptedString(plainText, KeyName, Version);
        return GetforwardDictionary(EncData);
    }

    public override string GetRequestString(string plainText)
    {
        return BanksEncryptionDecryption.GetEncryptedString(plainText + "|checkSum=" + checkSum, KeyName, Version);
    }
}