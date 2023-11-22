﻿using System;
using System.Collections.Generic;
using EgBL;
using System.Configuration;
using Update;

public partial class PaymentVerifyURL : System.Web.UI.Page
{
    UpdateGRN objUpdate = new UpdateGRN();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            SNAMerchantCode ObjSNAMerchantCode = new SNAMerchantCode();
            string PlainString = string.Empty;
            string cipherText = Request.Form["encData"];
            string BankCode = Request.Form["BankCode"];
            string Version = ObjSNAMerchantCode.Getversion(BankCode);
            if(Version == "1")
                PlainString = objDecrypt.DecryptAES256(cipherText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(BankCode) + ".key", null);
            else
                PlainString= objDecrypt.Decrypt(cipherText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ObjSNAMerchantCode.GetBankName(BankCode) + ".key");


            UpdateStatus(PlainString, cipherText, BankCode);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string UpdateStatus(string palinText, string encdata, string BsrCode)
    {
        string rv;

        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = palinText.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }
        InsertAuditTransaction(ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + encdata, BsrCode, Convert.ToInt64(lstPlainText[1].ToString()));
        objUpdate.GRN = Convert.ToInt64(lstPlainText[1].ToString());
        objUpdate.BSRCode = lstPlainText[3].ToString();
        objUpdate.Ref = lstPlainText[5].ToString();
        objUpdate.CIN = lstPlainText[7].ToString();
        objUpdate.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
        objUpdate.Amount = Convert.ToDouble(lstPlainText[11].ToString());
        objUpdate.Status = lstPlainText[13].ToString();

        rv = Convert.ToString(objUpdate.update());


        return rv;
    }

    private void InsertAuditTransaction(String encdata, string bsrcode, Int64 GRN)
    {

        string url = System.Web.HttpContext.Current.Request.UserHostName;
        // string url = Path.GetDirectoryName(Application.ExecutablePath);
        //string url = Request.UrlReferrer.AbsoluteUri.ToString(); //HttpContext.Current.Request.ApplicationPath;
        // string address = url[0] + url[1] + url[2];
        //   IPHostEntry ipHostInfo = Dns.GetHostByName(url[2].ToString());
        // IPAddress ipAddress = ipHostInfo.AddressList[0];

        objUpdate.UserID = "URL";
        objUpdate.Password = "URL";
        objUpdate.BSRCode = bsrcode.ToString();
        objUpdate.encdata = encdata.ToString();
        objUpdate.TransDate = DateTime.Now.Date;
        objUpdate.URL = url.ToString();
        objUpdate.GRN = GRN;
        //objUpdate.IPAddress = Request.UserHostAddress;//ipAddress.ToString();
        objUpdate.IPAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
        objUpdate.InsertAudit();
    }
}
