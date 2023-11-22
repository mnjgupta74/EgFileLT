using EgBL;
using System;
using System.ServiceModel.Activation;
using System.Web;

[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
//[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgUpdateStatusOnlineBankingGrn" in code, svc and config file together.
public class EgTransStatusUpdate : IEgTransStatusUpdate
{
    string result;
    //string plainText = string.Format("GRN={0}|BANK_CODE={1}|BankReferenceNo={2}|CIN={3}|PAID_DATE={4}|PAID_AMT={5}|TRANS_STATUS={6}|DebitBankCode={7}|BankRefNo={8}|PayMode={9}|Reason={10}",
    //                                   GRN.ToString(), "9930001", "PayTm12345", "993000112345605012022", DateTime.Now, Amount.ToString(), "S", "HDFC", "201908971735173", "DC", "SBIIB");

    //private string version = "2.0";
    //private string SCode = string.Empty;
    //private string key = string.Empty;
    //private string enctype;
    //private string Merchantcode;
    //private string Auth = string.Empty;

    //private string Parameter = string.Empty;
    //private Int64 GRN;
    //private string BANK_CODE;
    //private string BankReferenceNo;
    //private string CIN;
    //private DateTime PAID_DATE;
    //private double PAID_AMT;
    //private string TRANS_STATUS;
    //private string DebitBankCode;
    //private string BankRefNo;
    //private string PayMode;
    //private string Reason;
    //private bool flag = false;
    //private string exceptionMessage;
    //private string Result;
    //SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
    // b EgSnaServiceBL objEgSnaService = new EgSnaServiceBL();
    public string UpdateGRNStatus(string Parameter)
    {
        //parameter => Ife1L/s8/EK9Uj6CblMsr17OdW7OS6/W2ws/osEn3bcv+f9V7d2I05tcHxF4SGuSd13+cXB9A0pbj+ym9N8jjhHaG+zT
        //Auth => bXyyfqvgpnOrACNmHE+gaoUoFUNIfoxuIdTFWXnx+n4bR0+H1hAJxVv5sUgVRTihFN+MIIbgwnCyQxZxFxZL0CGJWdHikq6EAz0KWc6ShJSjy/Ferv8LCveSjqpWFEV5zps3zP2CsZuFNmIj3EcJEJSVZt6C43A33Srt|5019
        try
        {
            EgTransStatusUpdateBL objEgTransService = new EgTransStatusUpdateBL();
            objEgTransService.enctype = Parameter;
            var AuthToken = HttpContext.Current.Request.Headers["Token"];
            var BankCode = AuthToken.Split('|')[1];
            try
            {
                 Banks objResponse;
                objResponse = Banks.SelectBanks(BankCode);

                if (objResponse.Version != "2.0") return result = "Service is used by version 2.0 only !";

                objEgTransService.Auth = AuthToken + "|" + objResponse.Version;
                var version = objResponse.Version;
                result = objEgTransService.ProcessData();
            }
            catch { result = "Invalid Data"; }
        }
        catch (Exception ex)
        {
            result = "Request Unable To Process !";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return result;
    }

}

/// <summary>
/// P-> Parameter
/// H-> Header
/// F-> Fail
/// T-> Time
/// N-> NO
/// S-> Success
/// E-> Encryption
/// TE-> Time Exceeded
/// TB-> Time Between
/// IP-> IP Address
/// M-> Merchant
/// R-> Refrence Number
/// A-> Amount
/// AC-> Acount
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
///     003       |          HFM       |    Merchant key Missing                                           |     Merchant key Missing
///     004       |          HFU       |    User ID Header Missing                                           |     User ID  In Header is Wrong
///     005       |          HFP       |    Password In Header Missing                                     |     Password In Header is Missing
///     006       |          HFT       |    Time In Header Missing                                         |     Time In Header is Missing
///     007       |          HFTE      |    Created Time Span Can Not Be Greater Then Given Time Span      |     created time must be within a given time span 
///     008       |          HFTB      |    Created Time Can Not Be Greater Then Current Time              |     Created Time must be greater then current time    
///     009       |          HFWU      |    Wrong UserId in Header                                         |     Wrong User Id In Header 
///     010       |          HFWP      |    Wrong Password in Header                                       |     Wrong Password in Header
///     011       |          HSBH      |    No Budget Found                                                |     Duplicate Refrence Number Found
///     012       |          HSA       |    No Amount Found                                                |     Refrence No Not Matched    
///     013       |          HSRF      |    No Refrence no Found                                           |     Amount Requested Can Be Defaced
///     014       |          HSAC      |    No Account No Found                                            |     Deface Amount must be lower then total amount  
///     015       |          HSLC      |    No Location Found                                              |     No Grn Exists For Deface  
///     016       |          HSMC      |    No Merchant Key Found                                          |     Exception generated during Service check amount  for deface   
///     017       |          HSBF      |    Budget Head Formate is not correct                             |     Exception generated throughout the deface process  
///     018       |          HSAF      |    Amount Formate is not correct                                  |     Exception generated throughout the deface process  
///     019       |          HSRF      |    Refrence No Formate is not correct                             |     Exception generated throughout the deface process  
///     020       |          HSLF      |    Location Formate is not correct                                |     Exception generated throughout the deface process  
///     021       |          HSMF      |    Merchant Code Formate is not correct                           |     Exception generated throughout the deface process  
///     022       |          HSACF     |    Account No Formate is not correct                              |     Exception generated throughout the deface process  
///     023       |          HSIPM     |    Invalid IP Addres Or Merchant Code                             |     Exception generated throughout the deface process  
///     024       |          HSDR      |    Budget Head Formate is not correct                             |     Exception generated throughout the deface process  
///     025       |          HSNGR     |    No GRN Generated                                               |     Exception generated throughout the deface process  
///     026       |          HSGR      |    GRN created successfully                                       |     Exception generated throughout the deface process  
///     035       |          GREX      |    Exception Suring GRN Generation                                |   
///===============|====================|===================================================================|========================================================
///               |                    |                          VERIFY STATUS CODE                       |
///===============|====================|===================================================================|========================================================
///     101       |          VHEX     |    Exception + Message                                            |     Exception generated throughout the Verify process
///     102       |          VNG       |    No Grn Found                                                   |     No Grn Found in verify   
///     200       |           OK        |    Data                                                           |    Return data from database
///===============|====================|===================================================================|======================================================= 

class BankServiceErrorResponse
{
    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}

