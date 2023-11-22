using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;


[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TempServiceForBankUploadIngo" in code, svc and config file together.
public class TempServiceForBankUploadInfo : ITempServiceForBankUploadInfo
{
    public Dictionary<string, string> GRNData;
    public string CipherText { get; set; }
    public Int64 GRN { get; set; }
    public double TotalAmount { get; set; }
    public string KeyName { get; set; }
    public string Message { get; set; }

    public string ProcessGRN(string bsrcode)
    {
       // KeyName = "CANARA";
        TempServiceForBankUploadInfoBL objServiceBL = new TempServiceForBankUploadInfoBL();
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
        objServiceBL.BSRCode = bsrcode;


        DataTable dt = new DataTable();
        dt = objServiceBL.GetGrnForBankUploadInfo();
        KeyName = dt.Rows[0]["keyname"].ToString();
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            objServiceBL.Grn = Convert.ToInt64(dt.Rows[i]["GRN"].ToString());
            objServiceBL.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
            objServiceBL.BSRCode = dt.Rows[i]["BankName"].ToString();
            objServiceBL.PaymentMode = dt.Rows[i]["PaymentType"].ToString();


            string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", dt.Rows[i]["GRN"].ToString().ToString(), dt.Rows[i]["Amount"].ToString().ToString());
            string checkSum = objEncryption.GetMD5Hash(plainText);
            var cipherTextReq = BanksEncryptionDecryption.GetEncryptedString(plainText + "|checkSum=" + checkSum, KeyName);

            try
            {
                CanaraWebServ.VerifyTxn objCanara = new CanaraWebServ.VerifyTxn();
                CanaraWebServ.AuthHeader objAuthHeader = new CanaraWebServ.AuthHeader();
                objAuthHeader.UserName = "CanRajeGRAS";
                objAuthHeader.Password = "Can@GBM#2018";
                objCanara.AuthHeaderValue = objAuthHeader;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                CanaraWebServ.verifyResponse objResponse = new CanaraWebServ.verifyResponse();
                var IpAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["CanaraWebServ.VerifyTxn"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                objResponse = objCanara.getverifyStatus(cipherTextReq);
                CipherText = objResponse.encdata;
            }
            catch { }

            string PlainText = DecryptString();
            List<string> lstPlainText = new List<string>();
            GRNData = new Dictionary<string, string>();
            lstPlainText = PlainText.Split('|').ToList();

            //if (isEpay && lstPlainText.Count > 13)
            //{
            //    GRNData.Add("GRN", lstPlainText[0]);
            //    GRNData.Add("BANK_CODE", lstPlainText[13]);
            //    GRNData.Add("BankReferenceNo", lstPlainText[1]);
            //    GRNData.Add("CIN", lstPlainText[12]);
            //    GRNData.Add("PAID_DATE", lstPlainText[10]);
            //    GRNData.Add("PAID_AMT", lstPlainText[3]);
            //    GRNData.Add("TRANS_STATUS", lstPlainText[2].Substring(0, 1));
            //    GRNData.Add("epayBSRCode", lstPlainText[8]);
            //    GRNData.Add("bankRefNo", lstPlainText[9]);
            //    GRNData.Add("payMode", lstPlainText[5]);
            //}
            //else if (!isEpay)
            //{
            GRNData.Add("GRN", lstPlainText[0].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("BANK_CODE", lstPlainText[1].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("BankReferenceNo", lstPlainText[2].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("CIN", lstPlainText[3].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("PAID_DATE", lstPlainText[4].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("PAID_AMT", lstPlainText[5].Split('=').GetValue(1).ToString().Trim());
            GRNData.Add("TRANS_STATUS", lstPlainText[6].Split('=').GetValue(1).ToString().Trim().Substring(0, 1));
            //foreach (string data in lstPlainText)
            //{
            //    GRNData.Add(data.Split('=').GetValue(0).ToString().Trim(), data.Split('=').GetValue(1).ToString().Trim());
            //}
            //}
            //else
            //{
            //    EgErrorHandller obj = new EgErrorHandller();
            //    obj.InsertError(PlainText + "|" + "ELSE_CASE_UpdateStatus");
            //}

            //if (ValidateData(false, false))
            //{
            UpdateData();
            //}



        }
        //objServiceBL.ProcessGRN();
        return "done";
    }
    private string DecryptString()
    {
        return BanksEncryptionDecryption.GetDecryptedString(CipherText, KeyName);
    }
    private void UpdateData()
    {

        GenralFunction gf = new GenralFunction();
        SqlParameter[] PARM = new SqlParameter[7];

        PARM[0] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = GRNData["TRANS_STATUS"] };
        PARM[1] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Convert.ToInt64(GRNData["GRN"]) };
        PARM[2] = new SqlParameter("@CIN", SqlDbType.Char, 21) { Value = GRNData["CIN"] };
        PARM[3] = new SqlParameter("@Ref", SqlDbType.Char, 30) { Value = GRNData["BankReferenceNo"] };
        PARM[4] = new SqlParameter("@amount", SqlDbType.Money) { Value = Convert.ToDouble(GRNData["PAID_AMT"]) };
        PARM[5] = new SqlParameter("@timeStamp", SqlDbType.DateTime) { Value = Convert.ToDateTime(GRNData["PAID_DATE"]) };
        PARM[6] = new SqlParameter("@bankCode", SqlDbType.Char, 7) { Value = GRNData["BANK_CODE"] };
        int result = gf.UpdateData(PARM, "InsertGRNIntoBankUploadInfo");

        //int result = objEgEChallanBankBL.UpdateSuccessStatus();
        //if (result < 1)
        //    Message = "Invalid GRN Data";
        //if (isEpay && result >= 1)
        //{
        //    objEgEChallanBankBL.epayBSRCode = GRNData["epayBSRCode"];
        //    objEgEChallanBankBL.bankRefNo = GRNData["bankRefNo"];
        //    objEgEChallanBankBL.payMode = GRNData["payMode"];
        //    objEgEChallanBankBL.UpdateEpayStatus();
        //}
        //else if (isPayU && result >= 1)
        //{
        //    objEgEChallanBankBL.epayBSRCode = GRNData["PayUBSRCode"];//////DebitBankCode
        //    objEgEChallanBankBL.bankRefNo = GRNData["bankRefNo"];///////BankRefNo
        //    objEgEChallanBankBL.payMode = GRNData["payMode"];///////Transtype
        //    objEgEChallanBankBL.Reason = GRNData["reason"];
        //    objEgEChallanBankBL.UpdatePAYUStatus();
        //}

        //return result >= 1 ? true : false;
    }
    //private bool ValidateData(bool isEpay, bool PayU)
    //{
    //    if (GRN == 0 && TotalAmount == 0.00)
    //    {
    //        try
    //        {
    //            GRN = Convert.ToInt64(GRNData["GRN"]);
    //        }
    //        catch
    //        {
    //            Message = "Invalid GRN";
    //            return false;
    //        }
    //        try
    //        {
    //            TotalAmount = Convert.ToDouble(GRNData["PAID_AMT"]);
    //        }
    //        catch
    //        {
    //            Message = "Invalid Amount";
    //            return false;
    //        }
    //    }
    //    if (GRNData["PAID_AMT"] == "NA" && isEpay)
    //    {
    //        Message = "Status updated as Unsuccessfull ";
    //        GRNData["GRN"] = GRN.ToString();
    //        GRNData["PAID_AMT"] = TotalAmount.ToString();
    //        GRNData["TRANS_STATUS"] = "F";
    //        GRNData["PAID_DATE"] = DateTime.Now.ToString();
    //        return true;
    //    }
    //    if (Convert.ToInt64(GRNData["GRN"]) != GRN || Convert.ToDouble(GRNData["PAID_AMT"]) != TotalAmount)
    //    {
    //        Message = "Verified Unsuccessful.";
    //        GRNData["GRN"] = GRN.ToString();
    //        GRNData["PAID_AMT"] = TotalAmount.ToString();
    //        GRNData["TRANS_STATUS"] = "F";
    //        return true;
    //    }
    //    try
    //    {
    //        Convert.ToDateTime(GRNData["PAID_DATE"]);
    //    }
    //    catch
    //    {
    //        Message = "Invalid Date";
    //        return false;
    //    }
    //    try
    //    {
    //        if (Convert.ToDouble(GRNData["PAID_AMT"]) <= 0.00)
    //        {
    //            Message = "Amount Less Than 0.00";
    //            return false;
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //    if (GRNData["BANK_CODE"].Length != 7)
    //    {
    //        Message = "Invalid BankCode";
    //        return false;
    //    }
    //    switch (GRNData["TRANS_STATUS"].Substring(0, 1).ToUpper())
    //    {
    //        case "S":
    //            if ((GRNData["CIN"].Length != 21 && !isEpay) || (GRNData["CIN"].Length != 20 && isEpay))
    //            {
    //                Message = "Invalid CIN";
    //                return false;
    //            }
    //            else if (GRNData["BankReferenceNo"] == null || GRNData["BankReferenceNo"] == "")
    //            {
    //                Message = "Invalid Reference No";
    //                return false;
    //            }
    //            else
    //            {
    //                Message = "Status updated as successfull ";
    //                return true;
    //            }
    //        case "P":
    //            Message = "Status updated as Pending ";
    //            return true;
    //        case "F":
    //            Message = "Status updated as Unsuccessfull ";
    //            return true;
    //        default:
    //            Message = "Invalid Status";
    //            return false;
    //    }
    //    return true;
    //}
}
