using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for EgReleaseAmount
/// </summary>
[WebService(Namespace = "Egras")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EgReleaseAmount : System.Web.Services.WebService
{
    public AuthSoapHd spAuthenticationHeader;

    private string SCode = string.Empty;
    private string key = string.Empty;
    private string verifyData = string.Empty;
    private string exceptionMessage = string.Empty;
    private string Parameter = string.Empty;
    private Int64 Grn;
    private Int64 ReferenceNo;
    private Double Amount;
    private string enctype = string.Empty;
    private bool flag = false;

    SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
    EgReleaseAmountBL objEgReleaseGrn = new EgReleaseAmountBL();

    public EgReleaseAmount()
    {
    }
    public class AuthSoapHd : SoapHeader
    {
        public string enctype;
    }

    [WebMethod, SoapHeader("spAuthenticationHeader")]
    public string ReleaseAmount(string ParameterStr)//ReleaseAmount
    {
        Parameter = ParameterStr;
        enctype = spAuthenticationHeader.enctype;//HttpContext.Current.Request.Headers["enctype"];//header  type="D" or "V"
        SCode = GetStatusCode();
        if (flag)
        {
            try
            {
                objEgReleaseGrn.EncData = Parameter;
                objEgReleaseGrn.Grn = Grn;
                objEgReleaseGrn.Amount = Amount;
                objEgReleaseGrn.MapCode = key;// Merchant Code
                objEgReleaseGrn.Flag = flag;
                
                objEgReleaseGrn.ReleaseAuditLogs();
                objEgReleaseGrn.ReleaseAuditStatus();
                SCode = objEgReleaseGrn.statuscode;
            }
            catch (Exception ex)
            {
                SCode = "015";
                exceptionMessage = ex.Message.ToString();
                //return SCode;
                return new JavaScriptSerializer().Serialize(new ReleaseErrorResponse() { StatusCode = "015", Message = exceptionMessage });

            }
            return Validation(1 , SCode);
        }

        else
        {

            return Validation(0, SCode);
        }
       
    }

    private string VerifyGrn()
    {
        try
        {
            string Message = string.Empty;
            DataTable dt = new DataTable();
            EgReleaseAmountBL objEgReleaseServiceBL = new EgReleaseAmountBL();
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            objEgReleaseServiceBL.EncData = "Verify_" + Parameter;
           
           
            List<string> ParametersReceived = new List<string>();
            ParametersReceived = Parameter.Split('|').ToList();//Encrypted(Plainstring) | MerchantCode
            Parameter = objDecrypt.Decrypt(ParametersReceived[0], System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
            List<string> Data = new List<string>();
            Data = Parameter.Split('|').ToList();
            objEgReleaseServiceBL.isVerify = Convert.ToInt64(Data[1]) > 0 ? true : false;
            objEgReleaseServiceBL.ReferenceNo = Convert.ToInt64(Data[1]);//RefrenceNo
            objEgReleaseServiceBL.MerchantCode = Convert.ToInt32(ParametersReceived[1]);//RefrenceNo
            objEgReleaseServiceBL.Grn = Convert.ToInt64(Data[0]);//GRN
            objEgReleaseServiceBL.ReleaseAuditLogs();
            dt = objEgReleaseServiceBL.GetReleaseStatus();

            
            //string JSONString = string.Empty;
            verifyData = JsonConvert.SerializeObject(dt);
            //verifyData = objDecrypt.Encrypt(JSONString, System.Web.HttpContext.Current.Server.MapPath("~/WebPages/key/" + ParametersReceived[1] + ".key"));
            return dt.Rows.Count>0 ?"200" :"102";
        }
        catch (Exception ex)
        {
            
            exceptionMessage = ex.Message.ToString();
            return "105";
            //return new JavaScriptSerializer().Serialize(new (ReleaseErrorResponse) { StatusCode = 0, Message = "Error Accesing Service" });
        }
    }

    private string GetStatusCode()
    {
        //string Parameter = string.Empty;
        //string enctype = string.Empty;
        try
        {

            //=======================================
            if (string.IsNullOrEmpty(enctype))
                return SCode = "001";
            if (string.IsNullOrEmpty(Parameter))
                return SCode = "002";//  Parameter Emplty
            
            key = Parameter.Split('|')[1];
            if (string.IsNullOrEmpty(key))
                return SCode = "003";

            string HeaderText = objDecrypt.Decrypt(enctype, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + Parameter.Split('|')[1] + ".key");
            var type = HeaderText.Split('|')[0];//header  type="D" or "V"
            var CreatedDate = HeaderText.Split('|')[1];//header  CreatedDate 2021/05/31 11:53:35.460

            if (string.IsNullOrEmpty(type))
                return SCode = "004";
            if (type != "D" && type != "V")
                return SCode ="005" ;
            if (string.IsNullOrEmpty(CreatedDate))
                return SCode = "008";

            string receiveDate = CreatedDate.Split(' ')[0];
            string receiveTime = CreatedDate.Split(' ')[1];
            int day = Convert.ToInt16(receiveDate.Split('/')[2]);
            int month = Convert.ToInt16(receiveDate.Split('/')[1]);
            int year = Convert.ToInt16(receiveDate.Split('/')[0]);
            int hour = Convert.ToInt16(receiveTime.Split(':')[0]);
            int minute = Convert.ToInt16(receiveTime.Split(':')[1]);
            int second = Convert.ToInt16(receiveTime.Split(':')[2].Split('.')[0]);

            DateTime moment = new DateTime(year, month, day, hour, minute, second);
            double min = DateTime.Now.Subtract(moment).TotalMinutes;
            if (min < 0)
            {
                ReferenceNo = 0;
                return SCode = "007";
            }
            if (min > Convert.ToInt16(ConfigurationManager.AppSettings["Timespan"]))
            {
                ReferenceNo = 0;
                return SCode = "006";
            }
            if (type == "D")
            {
                List<string> ParametersReceived = new List<string>();
                ParametersReceived = Parameter.Split('|').ToList();
                ReferenceNo = Convert.ToInt64(ParametersReceived[2]);
                int MerchantCode = Convert.ToInt32(ParametersReceived[1]);

                objEgReleaseGrn.ReferenceNo = ReferenceNo;
                objEgReleaseGrn.MerchantCode = MerchantCode;

                int refCheck = objEgReleaseGrn.CheckReferenceNo();
                if (refCheck == -1)
                    return SCode = "009";
                if (refCheck != -1 && refCheck != 0)
                {
                    ReferenceNo = 0;
                    return SCode = "010";
                }

                string PlainText = objDecrypt.Decrypt(ParametersReceived[0], System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + ParametersReceived[1] + ".key");
                List<string> Data = new List<string>();
                Data = PlainText.Split('|').ToList();
                if (refCheck == 0 && Convert.ToInt64(Data[3]) == objEgReleaseGrn.ReferenceNo)
                {
                    Grn = Convert.ToInt64(Data[0]);
                    Amount = Convert.ToDouble(Data[1]);
                    MerchantCode = Convert.ToInt16(Data[2]);
                    ReferenceNo = Convert.ToInt64(Data[3]);
                    flag = true;
                }
                else
                    return SCode = "011";
            }
            else
            {
                return VerifyGrn();
            }
        }
        catch (Exception ex)
        {
            //objEgReleaseGrn.EncData = Parameter + "Error: " + ex.Message;
            //objEgReleaseGrn.ReleaseAuditLogs();
            exceptionMessage = ex.Message.ToString();
            return SCode = "016";
        }

        return SCode;
        //=======================================
    }
    private string Validation(int val , string SCode)
    {
        string Status = string.Empty;
        string Message = string.Empty;
        string retMsg = string.Empty;

        switch (SCode)
        {
            case "001":
                Status = "HF"; Message = "Header Empty";
                break;
            case "002":
                Status = "PF"; Message = "Parameter Empty";
                break;
            case "003":
                Status = "HEF"; Message = "Key In Parameter Missing";
                break;
            case "004":
                Status = "HTNF"; Message = "Type In Header Missing(D OR V)";
                break;
            case "005":
                Status = "HTNDV"; Message = "Type In Header Not Allowed Other Than D OR V";
                break;
            case "006":
                Status = "HSTE"; Message = "Created Time Span Can Not Be Greater Then Given Time Span ";
                break;
            case "007":
                Status = "HSTB"; Message = "Created Time Can Not Be Greater Than Current Time   ";
                break;
            case "008":
                Status = "HDE"; Message = "Created Date Empty";
                break;
            case "009":
                Status = "DHSMF"; Message = "Invalid IP Address OR Merchant Code";
                break;
            case "010":
                Status = "DHSDR"; Message = "Duplicate ReferenceNo Found";
                break;
            case "011":
                Status = "DHSRF"; Message = "ReferenceNo  Mismatch";
                break;
            case "012":
                Status = "DRSAA"; Message = "Deface Release Amount Accepted";
                break;
            case "013":
                Status = "DRSAF"; Message = "Deface Release Amount Can Not Be Greater Than Deface Total Amount";
                break;
            case "014":
                Status = "DRSGN"; Message = " GRN Not Available for  Release Refund ";
                break;
            case "015":
                Status = "DRSEX"; Message = "Exception :" + exceptionMessage;
                break;
            case "016":
                Status = "DHEX"; Message = "Exception  : " + exceptionMessage;
                break;
            case "101":
                Status = "VHEX"; Message = "Exception :" + exceptionMessage;
                break;
            case "102":
                Status = "VGN"; Message = "No Grn Found";
                break;
            case "200":
                Status = "OK"; Message = verifyData;
                break;
            default:
                Status = "000"; Message = "No Status Code Matched";
                break;
        }

        objEgReleaseGrn.EncData = Parameter + "_" + Status + "_" + Message;

        if (val == 0 && (SCode!= "101" && SCode!= "102" && SCode!= "200"))
        {
            objEgReleaseGrn.EncData = Parameter;
            objEgReleaseGrn.ReleaseAuditLogs();
            
            objEgReleaseGrn.msg = Message;
            objEgReleaseGrn.ReleaseAuditStatusLog();
        }
        retMsg = SCode == "001" || SCode == "002" || SCode == "003" ||
                SCode == "015" || SCode == "016" || SCode == "105" ?
            new JavaScriptSerializer().Serialize(new ReleaseErrorResponse() { StatusCode = SCode, Status = Status, Message = Message }) :
            SCode == "200" ? objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new VerifyGrnORRef()
            {
                StatusCode = SCode,
                Status = Status,
                Data = Message
            }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key") :
            objDecrypt.Encrypt(new JavaScriptSerializer().Serialize(new ReleaseResponse()
            {
                StatusCode = SCode,
                Status = Status,
                Message = Message,
                ReferenceNo = objEgReleaseGrn.ReferenceNo,
                GRN = objEgReleaseGrn.Grn,
                Amount = objEgReleaseGrn.Amount
            }), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + key + ".key");
        return retMsg;
    }
}

/// <summary>
/// P-> Parameter
/// D-> Deface
/// V-> Verify
/// H-> Header
/// F-> Fail
/// T-> Type
/// N-> NO
/// S-> Success
/// E-> Encryption
/// TE-> Time Exceeded
/// TB-> Time Between
/// IP-> IP Address
/// M-> Merchant
/// R-> Refrence Number
/// AA-> Amount Accepted
/// AF-> Amount Failed
/// GN-> NO GRN
/// EX-> Exception
/// DR-> Duplicate Refrence No
/// 
/// ==============|====================|===================================================================|======================================================
/// StatusCode    |         Status     |                            Message                                |         Description
/// ==============|====================|===================================================================|=======================================================
///               |                    |                          DEFACE STATUS CODE                       |
/// ==============|====================|===================================================================|=======================================================
///     001       |          HF        |    Header Empty                                                   |     Header Fail Due to Blank String in Header
///     002       |          PF        |    Parameter Empty                                                |     Header Fail Due to Blank String in parameter
///     003       |          HEF       |    Key In Parameter Missing                                       |     Merchant Key In Header Missing
///     004       |          HTN       |    Type In Header Missing(D OR V)                                 |     Type Missing In Header
///     005       |          HTNDV     |    Type In Header Not Allowed Other Then D OR V                   |     Only D=Deface and V=Verify type allowed
///     006       |          HSTE      |    Created Time Span Can Not Be Greater Then Given Time Span      |     created time must be within a given time span 
///     007       |          HSTB      |    Created Time Can Not Be Greater Then Current Time              |     Created Time must be greater then current time    
///     008       |          HDE        |    Create Date Empty                                             |      Create Date Empty  
///     009       |          DHSMF     |    Invalid IP Address OR Merchant Code                            |     Invalid Merchant Code
///     010       |          DHSDR     |    Duplicate RefrenceNo Found                                     |      Duplicate Refrence Number Found
///     011       |          DHSRF     |    Refrence No Mismatch                                           |     Refrence No Not Matched    
///     012       |          DRSAA     |    Deface Release Amount Accepted                                 |     Amount Requested Can Be Defaced
///     013       |          DRSAF     |    Deface Release Amount Can Not Be Greater Then Total Amount     |     Deface Amount must be lower then total amount  
///     014       |          DRSGN      |   GRN Not Available for  Release Refund                                                  |     No Grn Exists For Deface  
///     015       |          DRSEX     |    Exception + Message                                            |     Exception generated during Service check amount  for deface   
///     016       |          DHEX     |    Exception + Message                                            |     Exception generated throughout the deface process  
///    
///===============|====================|===================================================================|========================================================
///               |                    |                          VERIFY STATUS CODE                       |
///===============|====================|===================================================================|========================================================
///     101       |          VHEX     |    Exception + Message                                            |     Exception generated throughout the Verify process
///     102       |          VNG       |    No Grn Found                                                   |     No Grn Found in verify   
///     200       |           OK        |    Data                                                           |    Return data from database
///===============|====================|===================================================================|======================================================= 
/// </summary>
class ReleaseResponse
{


    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public Int64 ReferenceNo { get; set; }
    public Int64 GRN { get; set; }
    public double Amount { get; set; }

}
class ReleaseErrorResponse
{
    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
class VerifyGrnORRef
{
    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Data { get; set; }
}
