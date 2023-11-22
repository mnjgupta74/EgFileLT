using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AutoBankStatusService" in code, svc and config file together.
public class AutoBankStatusService : IAutoBankStatusService
{
    public Dictionary<string, string> GRNData;
    EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
    public string PrepareData()
    {
        try
        {
            EgAutoBankStatusServiceBL objServiceBL = new EgAutoBankStatusServiceBL();
            EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
            //objServiceBL.BSRCode = bsrcode;

            DataTable dt = new DataTable();
            dt = objServiceBL.GetPrepareData();
            Banks objBank = Banks.SelectBanks("6360010");
            string bankcode = "6360010";
            //string KeyName = objBank.KeyName;//dt.Rows[0]["keyname"].ToString();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                try
                {
                    objServiceBL.GRN = Convert.ToInt64(dt.Rows[i]["GRN"].ToString());
                    objServiceBL.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                    objServiceBL.BSRCode = dt.Rows[i]["BankName"].ToString();
                    objServiceBL.PaymentMode = dt.Rows[i]["PaymentType"].ToString();

                    if (bankcode != dt.Rows[i]["BankName"].ToString())
                    {
                        objBank = Banks.SelectBanks(dt.Rows[i]["BankName"].ToString());
                        bankcode = dt.Rows[i]["BankName"].ToString();
                    }

                    string KeyName = objBank.KeyName;
                    objServiceBL.BSRCode = dt.Rows[i]["BankName"].ToString();


                    string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", dt.Rows[i]["GRN"].ToString().ToString(), dt.Rows[i]["Amount"].ToString().ToString());
                    string checkSum = string.Empty;
                    string cipherTextReq = string.Empty;
                    try
                    {
                        checkSum = objBank.Version == "2.0" ? objEncryption.GetSHA256(plainText) : objEncryption.GetMD5Hash(plainText);
                    }
                    catch { }
                    try
                    {
                        objBank.checkSum = checkSum;
                        cipherTextReq = objBank.GetRequestString(plainText);
                        //cipherTextReq = BanksEncryptionDecryption.GetEncryptedString(plainText + "|checkSum=" + checkSum, KeyName);
                    }
                    catch { }
                    //objServiceBL
                    objServiceBL.cipherText = cipherTextReq;
                    objServiceBL.PlainText = plainText;
                    objServiceBL.CheckSum = checkSum;
                    int res = objServiceBL.UpdatePrepareCipherTextData();

                }
                catch (Exception ex)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.Message.ToString());
                    return "Request Unable To Process !";
                }
            }

            ProcessGRN(objServiceBL.BSRCode);


            return "done";
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Request Unable To Process !";
        }
    }

    private string ProcessGRN(string bsrcode)
    {
        try
        {
            // KeyName = "CANARA";
            EgAutoBankStatusServiceBL objServiceBL = new EgAutoBankStatusServiceBL();
            objServiceBL.BSRCode = bsrcode;


            DataTable dt = new DataTable();
            dt = objServiceBL.GetCipherTextData();
            Banks objBank = Banks.SelectBanks(bsrcode);
            string encdata = string.Empty;

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string CipherText = dt.Rows[i]["CipherTextRequest"].ToString();
                try
                {
                    encdata = objBank.CallVerifyService(CipherText);

                }
                catch { }
                objServiceBL.GRN = Convert.ToInt64(dt.Rows[i]["GRN"].ToString());
                objServiceBL.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                objServiceBL.BSRCode = dt.Rows[i]["BankName"].ToString();
                objServiceBL.cipherText = encdata;





                int res = objServiceBL.InsertBankResponseCipherText();
            }

            UpdateBankResponse(objServiceBL.BSRCode);
            return "done";
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Request Unable To Process !";
        }
    }
    private string UpdateBankResponse(string bsrcode)
    {
        try
        {
            EgAutoBankStatusServiceBL objServiceBL = new EgAutoBankStatusServiceBL();
            objServiceBL.BSRCode = bsrcode;


            DataTable dt = new DataTable();
            dt = objServiceBL.GetBankResponseData();
            Banks objBank = Banks.SelectBanks(bsrcode);
            string KeyName = objBank.KeyName;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                try
                {
                    //string PlainText = dt.Rows[i]["CipherTextResponse"].ToString().Trim();
                    //string PlainText = DecryptString(dt.Rows[i]["CipherTextResponse"].ToString().Trim(), KeyName.Trim());
                    //List<string> lstPlainText = new List<string>();
                    //lstPlainText = PlainText.Split('&').ToList();

                    //objServiceBL.TRANS_STATUS = lstPlainText[1].Split('=').GetValue(1).ToString().Substring(0, 1);///status;
                    //objServiceBL.GRN = Convert.ToInt64(lstPlainText[2].Split('=').GetValue(1));///txnid
                    //objServiceBL.hash = lstPlainText[3].Split('=').GetValue(1).ToString();////hash
                    //objServiceBL.payMode = lstPlainText[4].Split('=').GetValue(1).ToString();////mode
                    //objServiceBL.PAID_AMT = lstPlainText[5].Split('=').GetValue(1).ToString(); ///amount
                    //objServiceBL.BankReferenceNo = lstPlainText[6].Split('=').GetValue(1).ToString();/////mihpayid
                    //objServiceBL.CIN = lstPlainText[7].Split('=').GetValue(1).ToString(); ////////cin
                    //objServiceBL.bankRefNo = lstPlainText[8].Split('=').GetValue(1).ToString();//////bank_ref_num
                    //objServiceBL.reason = lstPlainText[9].Split('=').GetValue(1).ToString();////error
                    //objServiceBL.PayUBSRCode = lstPlainText[10].Split('=').GetValue(1).ToString();///////bankcode
                    //objServiceBL.PAID_DATE = lstPlainText[11].Split('=').GetValue(1).ToString();////addedon
                    //objServiceBL.BANK_CODE = lstPlainText[12].Split('=').GetValue(1).ToString();//////udf1
                    //objServiceBL.Head = lstPlainText[13].Split('=').GetValue(1).ToString();//////udf2


                    List<string> lstPlainText = new List<string>();
                    GRNData = new Dictionary<string, string>();
                    string PlainText = DecryptString(dt.Rows[i]["CipherTextResponse"].ToString().Trim(), KeyName.Trim(), objBank.Version);
                    lstPlainText = PlainText.Split('|').ToList();

                    //GRNData.Add("GRN", lstPlainText[0].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("BANK_CODE", lstPlainText[1].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("BankReferenceNo", lstPlainText[2].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("CIN", lstPlainText[3].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("PAID_DATE", lstPlainText[4].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("PAID_AMT", lstPlainText[5].Split('=').GetValue(1).ToString().Trim());
                    //GRNData.Add("TRANS_STATUS", lstPlainText[6].Split('=').GetValue(1).ToString().Trim().Substring(0, 1));

                    objServiceBL.GRN = Convert.ToInt64(lstPlainText[0].Split('=').GetValue(1).ToString().Trim());
                    objServiceBL.BANK_CODE = lstPlainText[1].Split('=').GetValue(1).ToString().Trim();
                    objServiceBL.BankReferenceNo = lstPlainText[2].Split('=').GetValue(1).ToString().Trim();
                    objServiceBL.CIN = lstPlainText[3].Split('=').GetValue(1).ToString().Trim();
                    objServiceBL.PAID_DATE = lstPlainText[4].Split('=').GetValue(1).ToString().Trim();
                    objServiceBL.PAID_AMT = lstPlainText[5].Split('=').GetValue(1).ToString().Trim();
                    objServiceBL.TRANS_STATUS = lstPlainText[6].Split('=').GetValue(1).ToString().Trim().Substring(0, 1);

                    int res = objServiceBL.InsertBankResponseData();


                }
                catch { }
            }
            return "done";
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            return "Request Unable To Process !";
        }
    }
    private string DecryptString(string CipherText, string KeyName, string version)
    {
        return BanksEncryptionDecryption.GetDecryptedString(CipherText, KeyName, version);
    }
}
