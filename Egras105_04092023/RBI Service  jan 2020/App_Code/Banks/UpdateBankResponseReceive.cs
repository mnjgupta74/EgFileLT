using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateBankResponseReceive
/// </summary>
public class UpdateBankResponseReceive
{
    public string BankCode { get; set; }
    public Int64 GRN { get; set; }
    public decimal Amount { get; set; }
    public string KeyName { get; set; }
    public string CipherText { get; set; }
    public string Message { get; set; }
    private bool isEpay
    {
        get
        {
            return KeyName == "BwmHPemeQsQhpwEGWmyQtQ==" ? true : false;
        }
    }
    private bool isPayU
    {
        get
        {
            return KeyName == "PayU" ? true : false;
        }
    }

    private bool isPayTM
    {
        get
        {
            return KeyName == "PayTMKey" ? true : false;
        }
    }
    //public Dictionary<string, string> GRNData;
    public UpdateBankResponseReceive()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool VerifyBank()
    {
        bool flag = false;
        if (!isPayU && isEpay)
            flag = GetEPayString();
        else if (isPayU && !isEpay)
            flag = GetPayUString();
        else
            flag = GetGeneralString();

        if (flag)
        {
            Message = UpdateBankResponse();
        }
        return flag;
    }
    private bool GetEPayString()
    {
        try
        {
            string PlainText = DecryptString();
            List<string> lstPlainText = new List<string>();
            lstPlainText = PlainText.Split('|').ToList();
            if (!isPayU)
            {
                lstPlainText = PlainText.Split('|').ToList();
                if (isEpay && lstPlainText.Count > 13)
                {
                    GRN = Convert.ToInt64(lstPlainText[0]);
                    BankCode = lstPlainText[13].ToString();
                    Amount = Convert.ToDecimal(lstPlainText[3]);
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    private bool GetPayUString()
    {
        try
        {
            List<string> lstPlainText = new List<string>();
            lstPlainText = CipherText.Split('&').ToList();
            if (lstPlainText[0] == "CorrectHash")
            {
                GRN = Convert.ToInt64(lstPlainText[2].Split('=').GetValue(1).ToString());
                BankCode = lstPlainText[12].Split('=').GetValue(1).ToString();
                Amount = Convert.ToDecimal(lstPlainText[5].Split('=').GetValue(1).ToString());
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    private bool GetGeneralString()
    {
        try
        {
            string PlainText = DecryptString();
            List<string> lstPlainText = new List<string>();
            lstPlainText = PlainText.Split('|').ToList();
            if (!isPayU)
            {

                GRN = Convert.ToInt64(lstPlainText[0].Split('=').GetValue(1).ToString().Trim());
                BankCode = lstPlainText[1].Split('=').GetValue(1).ToString().Trim();
                Amount = Convert.ToDecimal(lstPlainText[5].Split('=').GetValue(1).ToString().Trim());
                //GRN = Convert.ToInt64(34374641);
                //BankCode = "0006326";
                //Amount = 335;
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    public string UpdateBankResponse()
    {
        string msg = string.Empty;
        try
        {
            VerifiedBankClass objVerifyBank = new VerifiedBankClass();
            objVerifyBank.BSRCode = BankCode;// "1000132";//lstPlainText[1].Split('=')[1];// GRNData["BANK_CODE"];
            objVerifyBank.GRN = Convert.ToInt64(GRN);//34187100lstPlainText[0].Split('=')[1]); //GRNData["GRN"]);
            objVerifyBank.TotalAmount = Convert.ToDouble(Amount);//400 lstPlainText[5].Split('=')[1]);//GRNData["PAID_AMT"]);
            objVerifyBank.PaymentMode = "N";
            msg = objVerifyBank.Verify();
        }
        catch
        {
            msg = "Error in updating GRN";
        }
        return msg;
    }
    private string DecryptString()
    {
        return BanksEncryptionDecryption.GetDecryptedString(CipherText, KeyName);
    }
}