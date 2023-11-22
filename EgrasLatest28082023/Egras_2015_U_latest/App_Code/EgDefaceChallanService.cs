using System;
using System.Collections.Generic;
using System.Linq;
using EgBL;
using System.Web.Script.Serialization;
using System.ServiceModel.Activation;
using System.Configuration;

// NOTE: You can use the "Rename" command on the "Refactor"menu to change the class name "EgDefaceChallanService" in code, svc and config file together.
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class EgDefaceChallanService : IEgDefaceChallanService
{
    public string DefaceChallan(string Parameter)
    {
        try
        {
           
            string Message = "";
            EgDefaceDetailServiceBL objEgDefaceDetailServiceBL = new EgDefaceDetailServiceBL();
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            List<string> ParametersReceived = new List<string>();
            ParametersReceived = Parameter.Split('|').ToList();
            objEgDefaceDetailServiceBL.ReferenceNo = Convert.ToInt64(ParametersReceived[2]);
            objEgDefaceDetailServiceBL.MerchantCode = Convert.ToInt32(ParametersReceived[1]);
            objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();

            int refCheck = objEgDefaceDetailServiceBL.CheckReferenceNo();
            if (refCheck == 0)
            {
                //SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
                string PlainText = objDecrypt.Decrypt(ParametersReceived[0], System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                List<string> Data = new List<string>();
                Data = PlainText.Split('|').ToList();
                if (Convert.ToInt64(Data[3]) == objEgDefaceDetailServiceBL.ReferenceNo)
                {
                    objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + Parameter;
                    objEgDefaceDetailServiceBL.Grn = Convert.ToInt64(Data[0]);
                    objEgDefaceDetailServiceBL.MessageStatus = "Request is Pending";
                    objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
                    objEgDefaceDetailServiceBL.DefaceAuditLogs();
                    objEgDefaceDetailServiceBL.DefaceAmount = Convert.ToDouble(Data[1]);
                    objEgDefaceDetailServiceBL.OfficeCode = Data[2];
                    Message = objEgDefaceDetailServiceBL.UpdateDefaceGrn();
                    return objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new DefaceResponse() { StatusCode = 1, Message = Message, ReferenceNo = objEgDefaceDetailServiceBL.ReferenceNo, GRN = objEgDefaceDetailServiceBL.Grn, Amount = objEgDefaceDetailServiceBL.DefaceAmount }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                }
                else
                {
                    objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + Parameter + "_Reference Number Mismatch";
                    objEgDefaceDetailServiceBL.MessageStatus = "Reference Number Mismatch";
                    objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
                    objEgDefaceDetailServiceBL.DefaceAuditLogs();
                    return objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new DefaceResponse() { StatusCode = 1, Message = "Reference Number Mismatch", ReferenceNo = objEgDefaceDetailServiceBL.ReferenceNo, GRN = objEgDefaceDetailServiceBL.Grn, Amount = objEgDefaceDetailServiceBL.DefaceAmount }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                }
            }
            else if (refCheck == -1)
            {
                objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + Parameter + "_Invalid IPAddress";
                objEgDefaceDetailServiceBL.MessageStatus = "Invalid IPAddress";
                objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
                objEgDefaceDetailServiceBL.DefaceAuditLogs();
                return objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new DefaceResponse() { StatusCode = 1, Message = "Invalid IPAddress", ReferenceNo = objEgDefaceDetailServiceBL.ReferenceNo, GRN = objEgDefaceDetailServiceBL.Grn, Amount = objEgDefaceDetailServiceBL.DefaceAmount }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
            }
            else
            {
                objEgDefaceDetailServiceBL.ReferenceNo = 0;
                objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + Parameter + "_Duplicate Reference Number";
                objEgDefaceDetailServiceBL.MessageStatus = "Duplicate Reference Number";
                objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
                objEgDefaceDetailServiceBL.DefaceAuditLogs();
                return objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new DefaceResponse() { StatusCode = 1, Message = "Duplicate Reference Number", ReferenceNo = objEgDefaceDetailServiceBL.ReferenceNo, GRN = objEgDefaceDetailServiceBL.Grn, Amount = objEgDefaceDetailServiceBL.DefaceAmount }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
            }
        }
        catch (Exception ex)
        {
            EgDefaceDetailServiceBL objEgDefaceDetailServiceBL = new EgDefaceDetailServiceBL();
            objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + Parameter + "Error: " + ex.Message;
            objEgDefaceDetailServiceBL.MessageStatus = "Error Accesing Service";
            objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
            objEgDefaceDetailServiceBL.DefaceAuditLogs();
            return new JavaScriptSerializer().Serialize(new DefaceResponse() { StatusCode = 0, Message = "Error Accesing Service" });
        }
    }

    public string VerifyReferenceNo(string Parameter)
    {
        try
        {
            string Message = string.Empty;
            EgDefaceDetailServiceBL objEgDefaceDetailServiceBL = new EgDefaceDetailServiceBL();
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + "Verify_" + Parameter;
            objEgDefaceDetailServiceBL.MessageStatus = "Verify";
            objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
            objEgDefaceDetailServiceBL.DefaceAuditLogs();
            List<string> ParametersReceived = new List<string>();
            ParametersReceived = Parameter.Split('|').ToList();
            Parameter = objDecrypt.Decrypt(ParametersReceived[0], System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
            objEgDefaceDetailServiceBL.ReferenceNo = Convert.ToInt64(Parameter);
            string DStatus = objEgDefaceDetailServiceBL.GetDefaceStatus();
            //switch (DStatus.ToUpper())
            //{
            //    case "S":
            //        Message = "GRN Defaced Successfully";
            //        break;
            //    case "F":
            //        Message = "GRN Deface Failed";
            //        break;
            //    default:
            //        Message = "Invalid Reference Number";
            //        break;
            //}
            if(DStatus == null || DStatus == "")
                DStatus = "Invalid Reference Number";
            return objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new DefaceErrorResponse() { StatusCode = 1, Message = DStatus }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
        }
        catch(Exception ex)
        {
            EgDefaceDetailServiceBL objEgDefaceDetailServiceBL = new EgDefaceDetailServiceBL();
            objEgDefaceDetailServiceBL.EncData = ConfigurationManager.AppSettings["ServerName"].ToString() + " => " + "Verify_" + Parameter + "Error: " + ex.Message; ;
            objEgDefaceDetailServiceBL.MessageStatus = "Error Accesing Service";
            objEgDefaceDetailServiceBL.IpAddress = System.Web.HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString();
            objEgDefaceDetailServiceBL.DefaceAuditLogs();
            return new JavaScriptSerializer().Serialize(new DefaceErrorResponse() { StatusCode = 0, Message = "Error Accesing Service" });
        }
    }
}
class DefaceResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public Int64 ReferenceNo { get; set; }
    public Int64 GRN { get; set; }
    public double Amount { get; set; }
}
class DefaceErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}