using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using EgBL;
using System.Configuration;


/// <summary>
/// Summary description for VerifiedClass:Common Class For Verification By TO and User On july 2014
/// </summary>
public class VerifiedClass
{
    public int userid { get; set; }
    //public int Status { get; set; }
    public string BankCode { get; set; }
    public double amt { get; set; }
    public string GEN { get; set; }
    // public string Bank_Code { get; set; }
    public Int64 GRN { get; set; }
    public string flag { get; set; }
    public string PlainText { get; set; }
    public string BankUrl { get; set; }
    public string PaymentType { get; set; }
    EgFrmTOVerified ObjFrm;

    public VerifiedClass()
    {

    }
    public string Verifieddetails()
    {
        string message = "";
        try
        {
            string cipherText = "";
            string returnData = "";
            string IPAddress = "";
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
            //GEN = "21399999";
            //amt = 24.00;
            string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", GEN.ToString(), amt.ToString());
            //plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", "0000187204", "3255.00");
            if(PaymentType == null || PaymentType == "" )
                PaymentType = GetPaymentType(GRN);
            EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
            string checkSum = objEncryption.GetMD5Hash(plainText);
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();

            if (PaymentType == "M")
            {
                switch (BankCode.Substring(0, 3))
                {
                    case "030":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["PNBManualWebServVerify.VerifyOfflineTransaction"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        PNBManualWebServVerify.VerifyOfflineTransaction objPnbManual = new PNBManualWebServVerify.VerifyOfflineTransaction();
                        //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                        returnData = objPnbManual.VerifyTranStatus(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["PNBManualWebServVerify.VerifyOfflineTransaction"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress,GRN,PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        message = UpdateStatus(plainText, GRN.ToString(), amt.ToString());
                        break;
                    case "000":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                        BankUrl = "gbss.sbi.co.in"; //ConfigurationManager.AppSettings["SBIManualServ.RAJASTHANWS"].Split('/').GetValue(2).ToString().Replace("www.", "");
                       // SBIManualServ.RAJASTHANWS obj = new SBIManualServ.RAJASTHANWS();
                        returnData = Ojpost.OnlineEnquiry_Rajasthan(cipherText);
                        IPAddress = "gbss.sbi.co.in"; //Dns.GetHostByName(ConfigurationManager.AppSettings["SBIManualServ.RAJASTHANWS"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress,GRN,PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        message = UpdateStatus(plainText, GRN.ToString(), amt.ToString());
                        break;

                    case "028":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["CBIManualServVerify.graswebservice"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        CBIManualServVerify.graswebservice CBIManualService = new CBIManualServVerify.graswebservice();
                        returnData = CBIManualService.verifyGRASData(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["CBIManualServVerify.graswebservice"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        message = UpdateStatus(plainText, GRN.ToString(), amt.ToString());
                        break;


                    default:
                        message = "BSR not matched";
                        break;
                }
            }
            else
            {
                switch (BankCode.Substring(0,3))
                {
                    case "020":
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["BOBWebServ.GetRajGrasPmtStatusSoap"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        BOBWebServ.Service bobService  = new BOBWebServ.Service();
                        ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                        returnData = bobService.BOBRajGrasVerifyPayment(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["BOBWebServ.GetRajGrasPmtStatusSoap"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "029":
                        plainText = "RAJCOMMTAX|ubi@321|" + GEN.ToString() + "," + amt.ToString();
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["UBIWebServ.StateTaxService"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        UBIWebServ.Service UbiService = new UBIWebServ.Service();
                        ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                        returnData = UbiService.Verify_State_Tax_Payment(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["UBIWebServ.StateTaxService"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "000":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        SBIWebServ.DoubleVerifyMerchantService SbiService = new SBIWebServ.DoubleVerifyMerchantService();
                        cipherText = cipherText + "^" + "RAJASTHAN_EGRASS";
                        returnData = SbiService.DoubleVerification(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "017":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        SBIWebServ.DoubleVerifyMerchantService SbiServ = new SBIWebServ.DoubleVerifyMerchantService();
                        cipherText = cipherText + "^" + "RAJASTHAN_EGRASS";
                        returnData = SbiServ.DoubleVerification(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["SBIWebServ.DoubleVerifyMerchantService"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "030":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["PNBWebServ.verify"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        PNBWebServ.Service pnbService = new PNBWebServ.Service();
                        returnData = pnbService.verifyTransaction(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["PNBWebServ.verify"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "691":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["IDBIWebServ.inquiryservice"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        IDBIWebServ.Service idbiService = new IDBIWebServ.Service();// Change in Live
                        returnData = idbiService.UpdateRajEgrasInquiry(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["IDBIWebServ.inquiryservice"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "028":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["CBIWebServ.graswebservice"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        CBIWebServ.graswebservice CBIService = new CBIWebServ.graswebservice();
                        returnData = CBIService.verifyGRASData(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["CBIWebServ.graswebservice"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "036":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["OBCWebServ.RajasthanVerifyWebServiceImplService"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        OBCWebServ.RajasthanVerifyWebServiceImplService OBCService = new OBCWebServ.RajasthanVerifyWebServiceImplService();
                        returnData = OBCService.rajverificationrequest(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["OBCWebServ.RajasthanVerifyWebServiceImplService"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;
                    case "024":
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CANARA.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["CanaraWebServ.VerifyTxn"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        CanaraWebServ.VerifyTxn objCanara = new CanaraWebServ.VerifyTxn();

                        CanaraWebServ.AuthHeader objAuthHeader = new CanaraWebServ.AuthHeader();
                        objAuthHeader.UserName = "CanRajeGRAS";
                        objAuthHeader.Password = "Can@GBM#2018";
                        objCanara.AuthHeaderValue = objAuthHeader;
                        CanaraWebServ.verifyResponse objResponse = new CanaraWebServ.verifyResponse();
                        objResponse = objCanara.getverifyStatus(cipherText);
                        returnData = objResponse.encdata;
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["CanaraWebServ.VerifyTxn"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CANARA.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break;

                       case "022":                  // Boi 
                        plainText = plainText + "|checkSum=" + checkSum;
                        cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOI.key");
                        InsertAudit(plainText, BankCode, cipherText, flag);
                        BankUrl = ConfigurationManager.AppSettings["BOIWebServ.RJSTVerify"].Split('/').GetValue(2).ToString().Replace("www.", "");
                        BOIWebServ.RJSTVerify objBOI = new BOIWebServ.RJSTVerify();
                        returnData = objBOI.UpdateGRN_RJST(cipherText);
                        IPAddress = Dns.GetHostByName(ConfigurationManager.AppSettings["BOIWebServ.RJSTVerify"].Split('/').GetValue(2).ToString().Replace("www.", "")).AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOI.key");
                        message = UpdateStatus(plainText, GEN, amt.ToString());
                        break; 

                    case "100":
                        plainText = "" + "|" + BankCode + "|" + GEN.ToString();
                        string postData = "queryRequest=" + plainText + "&aggregatorId=SBIEPAY&merchantId=" + "1000132";
                        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                        BankUrl = "https://www.sbiepay.sbi/payagg/orderStatusQuery/getOrderStatusQuery";
                        WebRequest request = WebRequest.Create(BankUrl);
                        request.Method = "POST";
                        request.ContentLength = byteArray.Length;
                        request.ContentType = "application/x-www-form-urlencoded";
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        WebResponse response = request.GetResponse();
                        //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                        Stream data = response.GetResponseStream();
                        //queryParameters.Value = ((HttpWebResponse)response).StatusDescription;
                        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader readStream = new StreamReader(data, encode);
                        Char[] read = new Char[256];
                        int count = readStream.Read(read, 0, 256);
                        plainText = "";
                        while (count > 0)
                        {
                            String str = new String(read, 0, count);
                            returnData = returnData + "" + str;
                            count = readStream.Read(read, 0, 256);
                        }
                        IPAddress = Dns.GetHostByName("sbiepay.sbi").AddressList[0].ToString();
                        UploadData(BankCode, returnData, IPAddress, GRN, PaymentType);
                        ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
                        plainText = objEpay.Decrypt(returnData, "BwmHPemeQsQhpwEGWmyQtQ==", 128);
                        message = UpdateEpayStatus(plainText, GEN, amt.ToString());
                        response.Close();
                        break;
                    case "991":
                        //////add code for payu
                        VerifiedBankClass obj = new VerifiedBankClass();
                        obj.BSRCode = "9910001";
                        obj.GRN = GRN;
                        obj.PaymentMode = "N";
                        obj.TotalAmount = amt;
                        string msg = obj.Verify();
                        switch (msg.Trim())
                        {
                            case "Status updated as successfull":
                                plainText = "GRN=0|BANK_CODE=9910001|BankReferenceNo=0|CIN=0|PAID_DATE=2018/10/15 10:56:53|PAID_AMT=0|TRANS_STATUS=S";
                                break;
                            case "Status updated as Pending":
                                plainText = "GRN=0|BANK_CODE=9910001|BankReferenceNo=0|CIN=0|PAID_DATE=2018/10/15 10:56:53|PAID_AMT=0|TRANS_STATUS=P";
                                break;
                            case "Status updated as Unsuccessfull":
                                plainText = "GRN=0|BANK_CODE=9910001|BankReferenceNo=0|CIN=0|PAID_DATE=2018/10/15 10:56:53|PAID_AMT=0|TRANS_STATUS=F";
                                break;
                            default:
                                plainText = "GRN=0|BANK_CODE=9910001|BankReferenceNo=0|CIN=0|PAID_DATE=2018/10/15 10:56:53|PAID_AMT=0|TRANS_STATUS=P";
                                break;
                        }
                        break;
                    default:
                        //  ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('BSR not matched');", true);
                        //ScriptManager.RegisterStartupScript( ,GetType(), "Message", "BSR not matched", true);
                        message = "BSR not matched";
                        break;

                }
                PlainText = plainText;
            }
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //int result = obj.InsertError(ex.Message.ToString());

            var objEgManualBankServiceBl = new EgManualBankServiceBL { Errorname = ex.Message }; //sandeep 

            //var url = "EgManualChallan.aspx";
            //objEgManualBankServiceBl.PageName = url + grn;
            //objEgManualBankServiceBl.insertErrorLog();
            objEgManualBankServiceBl.BankCode = BankCode;
            objEgManualBankServiceBl.ServiceType = 1;
            objEgManualBankServiceBl.BankURL = BankUrl;
            objEgManualBankServiceBl.InsertBankServiceErrorLog();

            //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, Unable to process.');", true);
            message = "Response is still awaited from bank";
        }
        return message;
    }

    public void InsertAudit(string plainText, string BankCode, string cipherText, string flag)
    {
        ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        ObjFrm.encData = cipherText;
        ObjFrm.plainText = plainText;
        ObjFrm.flag = flag;
        ObjFrm.BankServiceAuditData();
    }

    private string GetPaymentType(Int64 GRN)
    {
        ObjFrm = new EgFrmTOVerified();
        ObjFrm.GRN = GRN;
        return ObjFrm.GetPaymentType();
    }
    private void UploadData(string BankCode, string encData,string IpAddress, Int64 GRn, string Paymenttype)
    {
        ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        ObjFrm.encData = encData;
        ObjFrm.Ipaddress = IpAddress;
        ObjFrm.GRN = GRn;
        ObjFrm.paymentType = Paymenttype;
        ObjFrm.BankResponseAudit();
    }
    public DataTable BindGrid()
    {
        ObjFrm = new EgFrmTOVerified();
        ObjFrm.BankCode = BankCode;
        return ObjFrm.FillVerifyChallan() as DataTable;

    }
    private string UpdateStatus(string palinText, string Grn, string amt)
    {
        string strmsg = "";
        try
        {

            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = palinText.Split('|');
            string[] arrIndMsg;
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');

                lstPlainText.Add(arrIndMsg[0]);
                lstPlainText.Add(arrIndMsg[1]);
            }

            if (lstPlainText[1].ToString().Trim() == Grn.ToString().Trim() && Convert.ToDouble(lstPlainText[11].ToString()) == Convert.ToDouble(amt.ToString()))
            {

                EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
                objEgEChallanBankBL.GRN = Convert.ToInt32(lstPlainText[1]);
                objEgEChallanBankBL.BankCode = lstPlainText[3].ToString().Trim();
                objEgEChallanBankBL.CIN = lstPlainText[7].ToString();
                objEgEChallanBankBL.Ref = lstPlainText[5].ToString();
                objEgEChallanBankBL.Amount = Convert.ToDouble(lstPlainText[11]);
                objEgEChallanBankBL.Status = lstPlainText[13].ToString();
                if (Convert.ToString(lstPlainText[9]) != null && Convert.ToString(lstPlainText[9]) != "")
                    objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
                else
                {
                    objEgEChallanBankBL.timeStamp = DateTime.Now;
                    //strmsg = "Response is Still Awaited From BankSide.";
                    //return strmsg;
                }
                int result = objEgEChallanBankBL.UpdateSuccessStatus();

                if (result != 1)
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Unable to process.');", true);
                    strmsg = "Due to some technical issue Unable to process";
                }
                else if (objEgEChallanBankBL.Status == "F")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Status Updated.');", true);
                    strmsg = "Status updated as Unsuccessfull ";
                }
                else if (objEgEChallanBankBL.Status == "S")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Status Updated.');", true);
                    strmsg = "Status updated as successfull ";
                }
                else if (objEgEChallanBankBL.Status == "P")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Status Updated.');", true);
                    strmsg = "Status updated as Pending ";
                }
            }
            else
            {
                EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
                objEgEChallanBankBL.GRN = Convert.ToInt32(Grn);
                objEgEChallanBankBL.BankCode = lstPlainText[3].ToString();
                objEgEChallanBankBL.CIN = lstPlainText[7].ToString();
                objEgEChallanBankBL.Ref = lstPlainText[5].ToString();
                objEgEChallanBankBL.Amount = Convert.ToDouble(amt);
                objEgEChallanBankBL.Status = "F";
                if (Convert.ToString(lstPlainText[9]) != null && Convert.ToString(lstPlainText[9]) != "")
                    objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
                else
                {
                    objEgEChallanBankBL.timeStamp = DateTime.Now;
                    //strmsg = "Response is Still Awaited From BankSide.";
                    //return strmsg;
                }
                objEgEChallanBankBL.UpdateSuccessStatus();
                // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Verified Unsuccessful.');", true);
                strmsg = "Verified Unsuccessful.";
            }

        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            int result = obj.InsertError(ex.Message.ToString());
            // ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, Unable to process.');", true);
            strmsg = "Response is Still Awaited From BankSide";
        }
        return strmsg;
    }

    private string UpdateEpayStatus(string palinText, string Grn, string amt)
    {
        string strmsg = "";
        try
        {
            EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
            //List<string> lstPlainText = new List<string>();
            string[] lstPlainText = palinText.Split('|');

            if (lstPlainText[0].ToString().Trim() == Grn.ToString().Trim() && Convert.ToDouble(lstPlainText[3].ToString()) == Convert.ToDouble(amt.ToString()))
            {

                objEgEChallanBankBL.GRN = Convert.ToInt32(Grn);
                objEgEChallanBankBL.BankCode = lstPlainText[13].ToString();
                if (lstPlainText[12].ToString() == "NA")
                {
                    objEgEChallanBankBL.CIN = "0";
                }
                else
                {
                    objEgEChallanBankBL.CIN = lstPlainText[12].ToString();
                }
                objEgEChallanBankBL.Ref = lstPlainText[1].ToString();
                objEgEChallanBankBL.Amount = Convert.ToDouble(amt);
                objEgEChallanBankBL.Status = lstPlainText[2].ToString().Substring(0, 1);
                if (lstPlainText[10].ToString() != "NA")
                {
                    objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[10].ToString());
                }
                else
                {
                    objEgEChallanBankBL.timeStamp = DateTime.Now;
                    //strmsg = "Due to some error, Unable to process.";
                    //return strmsg;
                }
                
                int result = objEgEChallanBankBL.UpdateSuccessStatus();

                if (result != 1)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Unable to process.');", true);
                    strmsg = "Unable to process.";
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Status Updated.');", true);
                    strmsg = "Status Updated.";
                }
            }
            else
            {
                objEgEChallanBankBL.BankCode = lstPlainText[13].ToString();
                objEgEChallanBankBL.Ref = lstPlainText[1].ToString();
                if (lstPlainText[12].ToString() == "NA")
                {
                    objEgEChallanBankBL.CIN = "0";
                }
                else
                {
                    objEgEChallanBankBL.CIN = lstPlainText[12].ToString();
                }
                objEgEChallanBankBL.Amount = Convert.ToDouble(amt);
                objEgEChallanBankBL.Status = "F";
                objEgEChallanBankBL.GRN = Convert.ToInt32(Grn);
                if (lstPlainText[10].ToString() != "NA")
                {
                    objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[10].ToString());
                }
                else
                {
                    objEgEChallanBankBL.timeStamp = DateTime.Now;
                    //strmsg = "Due to some error, Unable to process.";
                    //return strmsg;
                }
                objEgEChallanBankBL.UpdateSuccessStatus();

                //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Verified Unsuccessful.');", true);
                strmsg = "Verified Unsuccessful.";
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            int result = obj.InsertError(ex.Message.ToString());
            //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, Unable to process.');", true);
            strmsg = "Due to some error, Unable to process.";
        }
        return strmsg;
    }

    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy() { }
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert,
            WebRequest req,
            int problem)
        {
            return true;
        }
    }


    public void ShoeMessage(string strmessage)
    {
        Page currentPage = (Page)HttpContext.Current.CurrentHandler;
        //currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(),
        //                                  "AlertWindow", String.Format("alert('{0}')", strmessage), true);
        ScriptManager.RegisterStartupScript(currentPage, typeof(Page), "Message", "alert( ' " + strmessage + "  ');", true);

    }
}
