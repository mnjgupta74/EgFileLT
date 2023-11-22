using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using EgBL;
using System.Net;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class EgTrgService : System.Web.Services.WebService
{
    EgChallanDetail objEgChallanDetail;
    EncryptDecryptionBL objEncryptDecrypt;
    EgGrnChallanBL objEgGrnChallanBL;


    public EgTrgService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string GetServiceString(int grn, string Tcode)
    {
        string result;
        try
        {
            objEgChallanDetail = new EgChallanDetail();
            objEncryptDecrypt = new EncryptDecryptionBL();

            objEgChallanDetail.GRN = grn;
            objEgChallanDetail.TreasuryCode = Tcode.ToString();
            result = objEgChallanDetail.EchallanString();
        }
        catch (Exception ex)
        {
            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        //result = objEncryptDecrypt.Encrypt(result);
        return result;
    }





    /// <summary>
    /// Create For Testing SMS service with Test Server
    /// </summary>
    /// <param name="Vcode"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetSMSDetails(string Vcode)
    {
        string responseString = string.Empty;
        try
        {
            string dlt_entity = "1001524671154484790";
            string dlt_tempate_id = "1007056277014110427";
            EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            //string responseString = SMSservice.GetSMSDetails(cipherText);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; // added 16/08/2018 -- For Certification expired problem
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string cipherText = objEncry.Decrypt(Vcode, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key");
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(cipherText);
            string[] Ivalues = cipherText.Split('|');
            string Verfcode = Ivalues[0].ToString().Split('=').GetValue(1).ToString();
            string Mobile = Ivalues[1].ToString().Split('=').GetValue(1).ToString();
            HttpWebRequest myReq =
                (HttpWebRequest)WebRequest.Create("http://smsgw.sms.gov.in/failsafe/HttpLink?username=" + "egrasraj.otp" + "&pin=" + "T3%23uB7%24xD0" + "&message=" + "Egras Password Reset Code:loginID:" + "***" + " and OTP:" + Verfcode.Trim() + "&mnumber=" + Mobile.ToString().Trim() + "&signature=" + "EGRASJ" + "&dlt_entity_id=" + dlt_entity + "&dlt_template_id=" + dlt_tempate_id);

            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
           
            using (System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream()))
            {
                responseString = respStreamReader.ReadToEnd();//"Message Accepted for Request ID=12313946898506681731361~code=API000 & info=Platform accepted & Time =2014/03/13/11/20";// 
                respStreamReader.Close();
            }
            myResp.Close();
        }
        catch (Exception ex)
        {
            responseString = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return responseString;
        //xFiCXF1D5JX7AxCPwzWPvXfWEG2zq3UyRUQ+oXCL92XjViuY6/xN1QpjFF4IGMlUCP7fNV9/5lpzYe67l5CeLYcVIgIB4AFECFtPXDeqiAM=
    }













    /// <summary>
    /// Created by : Robins Saxena
    /// Description : Method to get GRNChallan Service data based on Treasury Code
    /// </summary>

    [WebMethod]
    public string GetGrnChallanServiceString(int grn, string Tcode)
    {
        string result;
        try
        {
            objEgGrnChallanBL = new EgGrnChallanBL();

            objEgGrnChallanBL.GRN = grn;
            objEgGrnChallanBL.TreasuryCode = Tcode.ToString().Trim();
            result = objEgGrnChallanBL.GetGrnChallanData();
        }
        catch (Exception ex)
        {
            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        return result;
    }


    /// <summary>
    /// Created by : rakesh
    /// Description : delete PdAccount
    /// </summary>

    [WebMethod]
    public int DeletePDAccount(int pdaccount, string TreasuryCode)
    {
        int output = 0;
        try
        {
            EgDeletePdAccountBL objEgDeletePdAccountBL = new EgDeletePdAccountBL();
            objEgDeletePdAccountBL.PdAcc = pdaccount;
            objEgDeletePdAccountBL.TreasuryCode = TreasuryCode;
            objEgDeletePdAccountBL.Flag = "D";
            output = objEgDeletePdAccountBL.DeletePD();
        }
        catch (Exception ex)
        {
            output = 0;
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        return output;
    }

    /// <summary>
    /// Data share to  Paymanager releated to refund And realease Amount
    /// </summary>
    /// <param name="encData"></param>
    /// <returns></returns>
    [WebMethod]
    public string EgrasData(string encData)
    {
        string result = "";
        string PlainText = "";
        string cipherText = "";
        ///////////////////////////////////////////////////////
        //string plainText = string.Format("GRN={0}", "35756");

        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            //encData = objEncry.Encrypt(plainText, Server.MapPath("~/WebPages/Key/RAJASTHAN_EGRASS.key"));

            PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Paymanager.key");
            result = GetGRNDetails(PlainText);
            cipherText = objEncry.Encrypt(result, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Paymanager.key");
        }
        catch (Exception ex)
        {
            cipherText = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());

        }
        return cipherText;
    }

    private string GetGRNDetails(string plaintext)
    {
        string result;

        List<string> lstPlainText = new List<string>();
        string[] arrMsgs = plaintext.Split('|');
        string[] arrIndMsg;
        for (int i = 0; i < arrMsgs.Length; i++)
        {
            arrIndMsg = arrMsgs[i].Split('=');

            lstPlainText.Add(arrIndMsg[0]);
            lstPlainText.Add(arrIndMsg[1]);
        }

        EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
        objEgManualBankServiceBL.GRNNumber = Convert.ToInt64(lstPlainText[1].ToString());

        result = objEgManualBankServiceBL.GetGrnDetailToPaymanager();
        return result;
    }

    private string DecryptString(string strQueryString, string filePath)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        return objEncry.Decrypt(strQueryString, filePath);
    }


    // Minus Expenditure Share  7july 2020
    [WebMethod]
    public Byte[] GetMinusExpenditureChallan(string encData)
    {
        ErrorHandlePdf objPdf = new ErrorHandlePdf();
        string PlainText = "";
        byte[] returnValue = null;
        try
        {
           
            ReportViewer objReport = new ReportViewer();
            PlainText = DecryptString(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Paymanager.key");           
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = PlainText.Split('=');
                lstPlainText.Add(arrMsgs[0]);
                lstPlainText.Add(arrMsgs[1]);
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("GRN", (lstPlainText[1]).ToString());
            param[1] = new ReportParameter("DeptName", "Egras");
            param[2] = new ReportParameter("MajorHead","0040");
            param[3] = new ReportParameter("Mode", "2");
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(objReport, "EgEChallanViewRpt", param);
            //output as PDF
            var format = "PDF";
            var deviceinfo = "";
            string mimeType;
            string encoding;
            string extension;
            string[] streams;
            Warning[] warnings;

            returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);

           
        }
        catch (Exception ex)
        {
            string Msg = "Request Unable To Process !";
            returnValue = objPdf.Error(Msg);
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "-EgTRG Service-");
        }
        return returnValue;
    }

    // Minus Expenditure Share  7july 2020
    [WebMethod]
    public Byte[] GetMinusExpenditureChallan1(string encData)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        ErrorHandlePdf objPdf = new ErrorHandlePdf();
        string PlainText = "";
        byte[] returnValue = null;
        try
        {

            ReportViewer objReport = new ReportViewer();
            PlainText = objEncry.DecryptSBIWithKey256(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "5018.key",null);
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = PlainText.Split('=');
            lstPlainText.Add(arrMsgs[0]);
            lstPlainText.Add(arrMsgs[1]);
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("GRN", (lstPlainText[1]).ToString());
            param[1] = new ReportParameter("DeptName", "Egras");
            param[2] = new ReportParameter("MajorHead", "0040");
            param[3] = new ReportParameter("Mode", "2");
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(objReport, "EgEChallanViewRpt", param);
            //output as PDF
            var format = "PDF";
            var deviceinfo = "";
            string mimeType;
            string encoding;
            string extension;
            string[] streams;
            Warning[] warnings;

            returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        }
        catch (Exception ex)
        {
            string Msg = "Request Unable To Process !";
            returnValue = objPdf.Error(Msg);
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "-EgTRG Service-");
        }
        return returnValue;
    }
}


