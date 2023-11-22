using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEgrasAppLoginService" in both code and config file together.
[ServiceContract]
public interface IEgrasAppLoginService
{
    [OperationContract]
    [WebInvoke(Method = "POST",
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "LatestVersion")]
    string GetLatestVersion();

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "Login")]
    string Login(string param);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "CheckMPIN")]
    string MpinLogin(string param);

    [OperationContract]
    [WebInvoke(Method = "POST",

        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "InsertMPIN")]
    string InsertMpinCredential(string param);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SinUp")]
    string SinUp(string registration);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "MobileVerify_GenerateOTP")]
    int MobileNoVerify_GenerateCode(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "MobileVerify_VerifyOTP")]
    string MobileNoVerify_OTPVerify(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "CheckUserExist")]
    string ForgotPasswordbyUser(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SendMessage")]
    int SendOTP(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ResetMpin")]
    int ResetMpinEntry(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "CheckExistingUserMobile")]
    int CheckExistingUser(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "OTPVerify")]
    string ForgotPasswordOTPVerify(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ChangePwd")]
    string ForgotPasswordChangePassword(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "UpdateProfile")]
    string EditUserProfileData(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "EditProfileDetail")]
    string GetUserProfileDetail(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare,
    UriTemplate = "CheckEditMobileNo")]
    string CheckEditMobileNoExist(string Parameter);

    [OperationContract]
    [WebInvoke(Method = "POST",
    ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare,
    UriTemplate = "Test")]
    string TestMethod(string Parameter);

}
