using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using EgBL;
using System.Web.Script.Serialization;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgrasAppLoginService" in code, svc and config file together.
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class EgrasAppLoginService : IEgrasAppLoginService
{
    private string Key = "fd876698-89a7-46f5-bf5a-70a64f65d323";
    AndroidAppBL androidAppBL;

    public string GetLatestVersion()
    {
        androidAppBL = new AndroidAppBL();
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        return androidAppBL.GetLatestVersionApp();
    }

    #region Registration with Mobile Verify
    public string SinUp(string objRegistration)
    {
        try
        {
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            //objRegistration = "ANJEymU/2hi0jAv3GZPzhnZPRht6rTp3fXHF2EqHaM7doPra7IoWELCANUqvcVrnTxA5p4afa6YH+d/I5ugbelzXjGdwpeescrgMFW6psm5kAyH3SVsP/VWaOXY+GmNBtP07Xr742QIMRtc5T8Ko5mv6XdOSokwJRsluTG2ImSNaKnrs9IKlDcfzfbRRL0abKrKKkoqCH3EhMSIjqUw/nvSo5sBocLXSLg5xBnK/r0nMSMT8EcSijtGSNdBYyrWvGxfQ5sXnIHobo4h45AFw6xkJGpSlrAQRwu0bBYWGrEc=";
            AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
            objRegistration = objEncryptDecrypt.DecryptText(objRegistration, Key);
            androidAppBL = new AndroidAppBL();
            
            int result = androidAppBL.NewRegistration(objRegistration);
            bool smsSend = false;
            if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null) && (androidAppBL.VCode != "" && androidAppBL.VCode != null))
            {
                string cipherText = androidAppBL.send("egrasraj.otp", "T3%23uB7%24xD0", androidAppBL.VCode.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ","1001524671154484790", "1007056277014110427");
                SMSservice.EgTrgService SMSservic = new SMSservice.EgTrgService();
                string responseString = SMSservic.GetSMSDetails(cipherText);
                if (responseString != "" && responseString != null)
                    smsSend = true;
            }
            else
                smsSend = false;
            if (smsSend)
                return objEncrypt.EncryptString(androidAppBL.UserId.ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString()));
            else
            {
                if (result == 1)
                    return objEncrypt.EncryptString(androidAppBL.UserId.ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString()));
                return "";
            }
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    public int CheckExistingUser(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        androidAppBL = new AndroidAppBL();
        string PlainText = objEncryptDecrypt.DecryptText(Parameter, Key);
        androidAppBL.LoginID = PlainText.Split('|').GetValue(0).ToString().Trim();
        androidAppBL.MobileNo = PlainText.Split('|').GetValue(1).ToString().Trim();
        int ResultCode = androidAppBL.CheckExistingUser();
        //if (ResultCode == 0)
        //{
        //    string Vcode = androidAppBL.VerifyMobileNoGenerateOTP().ToString().Trim();
        //    bool smsSend = false;
        //    if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null) && (Vcode != "" && Vcode != null))
        //    {
        //        string cipherText = androidAppBL.send("egras.auth", "Jh*$23et", Vcode.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ");
        //        SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
        //        string responseString = SMSservice.GetSMSDetails(cipherText);
        //        if (responseString != "" && responseString != null)
        //            smsSend = true;
        //    }
        //    else
        //        smsSend = false;
        //    if (smsSend)
        //        return 0;
        //    else
        //        return 3;
        //}
        return ResultCode;
    }

    public int MobileNoVerify_GenerateCode(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        androidAppBL = new AndroidAppBL();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        androidAppBL = new AndroidAppBL();
        androidAppBL.MobileNo = returnValues[0];
        string Vcode = androidAppBL.VerifyMobileNoGenerateOTP().ToString().Trim();
        bool smsSend = false;
        if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null) && (Vcode != "" && Vcode != null))
        {
            string cipherText = androidAppBL.send("egrasraj.otp", "T3%23uB7%24xD0", Vcode.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ", "1001524671154484790", "1007056277014110427");
            SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
            string responseString = SMSservice.GetSMSDetails(cipherText);
            if (responseString != "" && responseString != null)
                smsSend = true;
        }
        else
            smsSend = false;
        if (smsSend)
            return 1;
        else
            return 0;
    }

    public string MobileNoVerify_OTPVerify(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        androidAppBL = new AndroidAppBL();
        androidAppBL.UserID = objEncrypt.DecryptString(returnValues[0], (ConfigurationManager.AppSettings["AppKey"].ToString()));
        androidAppBL.VerificationCode = returnValues[1].Trim();
        string result = androidAppBL.VerifyMobileNo_OTPCheck();
        return objEncryptDecrypt.EncryptText(result + "|" + returnValues[2].Trim(), Key);
    }

    public string GetUserProfileDetail(string Parameter)
    {
        try
        {
            androidAppBL = new AndroidAppBL();
            //Parameter = "ISU/OAB+hjNxcjpcDkrvUm5lkwPaKXm1wHOoJDQlkOM=";
            AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            androidAppBL.UserId = Convert.ToInt32(objEncrypt.DecryptString(objEncDec.DecryptText(Parameter, Key), (ConfigurationManager.AppSettings["AppKey"].ToString())));
            return objEncDec.EncryptText(androidAppBL.EditData(), Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string CheckEditMobileNoExist(string Parameter)
    {
        try
        {
            //Parameter = "mU4TYdr5S6ryDs3GBCQsBwA7X9/+BJGPvaHdBF12g112aZsf5UcGhlq8g/PDvY4a";
            androidAppBL = new AndroidAppBL();
            AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            string Plaintext = objEncDec.DecryptText(Parameter, Key).Trim();
            androidAppBL.UserId = Convert.ToInt32(objEncrypt.DecryptString(Plaintext.Split('|').GetValue(0).ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString())));
            androidAppBL.MobileNo = Plaintext.Split('|').GetValue(1).ToString();
            return androidAppBL.CheckEditMobileNoExist();
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string EditUserProfileData(string Parameter)
    {
        try
        {
            //Parameter = "ISU/OAB+hjNxcjpcDkrvUq2pL8SsuJxO9om6kZju8yFf/ACaXJMQp+uvY0LEqs1ZKUlL5hQOKVjnsZ2/ApMsf5dL1R579KJW/X0KPBm5Pz9wXeSzOtpgLS2iVHT2NAn0d69Xk9P8fnmMQauCIgz3mEXNZDULkocCqFfffLmj0nWQjb2a6Jq2HTEbs1Lu0U9JDS0SRVuVUvWi9SLSixwjuA326T191O2woYrQe4yvyNx4mE/Nb938IyykQkUGHZ+GBsaOJcp8UmE1sregddSSVg==";
            androidAppBL = new AndroidAppBL();
            AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            Parameter = objEncDec.DecryptText(Parameter, Key);
            androidAppBL.UserId = Convert.ToInt32(objEncrypt.DecryptString(Parameter.Split('|').GetValue(0).ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString())));
            //List<string> returnValues = new List<string>();
            //returnValues = Parameter.Split('|').ToList();
            //androidAppBL.FirstName = returnValues[0].Trim();
            //androidAppBL.LastName = returnValues[1].Trim();
            //androidAppBL.DateOfbirth = returnValues[2].Trim();
            //androidAppBL.Email = returnValues[3].Trim();
            //androidAppBL.Address = returnValues[4].Trim();
            //androidAppBL.City = returnValues[5].Trim();
            //androidAppBL.MobileNo = returnValues[6].Trim();
            int result = androidAppBL.UpdateUserData(Parameter.Split('|').GetValue(1).ToString());
            
            bool smsSend = false;
            if ((androidAppBL.MobileNo != "" && androidAppBL.MobileNo != null) && (androidAppBL.VCode != "" && androidAppBL.VCode != null) && result == 1)
            {
                string cipherText = androidAppBL.send("egrasraj.otp", "T3%23uB7%24xD0", androidAppBL.VCode.Trim(), "91" + androidAppBL.MobileNo.Trim(), "EGRASJ", "1001524671154484790", "1007056277014110427");
                SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
                string responseString = SMSservice.GetSMSDetails(cipherText);
                if (responseString != "" && responseString != null)
                    smsSend = true;
            }
            else
                smsSend = false;
            if (smsSend)
                return objEncDec.EncryptText(1.ToString(), Key);
            else
            {
                if (result == 1)
                    return objEncDec.EncryptText(2.ToString(), Key);
                return "";
            }
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }
    #endregion

    public string Login(string Parameter)
    {
        try
        {
            AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
            Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            androidAppBL = new AndroidAppBL();
            string ReturnVal;
            androidAppBL.LoginID = returnValues[0];
            androidAppBL.Password = returnValues[1];
            androidAppBL.RND = returnValues[3];
            androidAppBL.IPAddress = returnValues[4];
            androidAppBL.DeviceId = returnValues[5];
            androidAppBL.PasswordSHA256 = returnValues[2];
            ReturnVal = Convert.ToString(androidAppBL.GetAppLogin()).Trim();
            return objEncryptDecrypt.EncryptText(ReturnVal + "|" + objEncrypt.EncryptString(androidAppBL.UserId.ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString())) + "|" + androidAppBL.RND + "|" + androidAppBL.isMpinCreated + "|" + androidAppBL.Userflag, Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

   

    #region Forgot Password
    public string ForgotPasswordbyUser(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        AndroidAppBL objForgotPassword = new AndroidAppBL();
        objForgotPassword.LoginID = returnValues[0].ToString().Trim();
        objForgotPassword.MobileNo = returnValues[1].ToString().Trim();
        string UserID = objForgotPassword.CheckExistingLogin();
        if (UserID == "")
            return objEncryptDecrypt.EncryptText("-1|NA|NA", Key);

        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        objUserReg.UserId = Convert.ToInt32(UserID);
        objUserReg.GetUserVerifyDetails();
        return objEncryptDecrypt.EncryptText("0|" + objUserReg.FirstName + "|" + "+**********" + objUserReg.MobilePhone.Substring(8, 2) + "|" + objEncrypt.EncryptString(objUserReg.UserId.ToString(), (ConfigurationManager.AppSettings["AppKey"].ToString())), Key);
    }
    public int SendOTP(string Parameter)
    {
        androidAppBL = new AndroidAppBL();
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        objUserReg.UserId = Convert.ToInt32(objEncrypt.DecryptString(returnValues[0], (ConfigurationManager.AppSettings["AppKey"].ToString())));
        objUserReg.GetUserVerifyDetails();
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = objUserReg.UserId;
        string Vcode = objEgForgotPasswordBL.VerifyCode().ToString().Trim();
        bool smsSend = false;
        if ((objUserReg.MobilePhone != "" && objUserReg.MobilePhone != null) && (Vcode != "" && Vcode != null))
        {
            string cipherText = androidAppBL.send("egrasraj.otp", "T3%23uB7%24xD0", Vcode.Trim(), "91" + objUserReg.MobilePhone.Trim(), "EGRASJ", "1001524671154484790", "1007056277014110427");
            SMSservice.EgTrgService SMSservice = new SMSservice.EgTrgService();
            string responseString = SMSservice.GetSMSDetails(cipherText);
            if (responseString != "" && responseString != null)
                smsSend = true;
        }
        else
            smsSend = false;
        if (smsSend)
            return 1;
        else
            return 0;
    }
    
    public string ForgotPasswordOTPVerify(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        EgForgotPasswordBL objEgForgotPasswordBL = new EgForgotPasswordBL();
        objEgForgotPasswordBL.UserId = Convert.ToInt32(objEncrypt.DecryptString(returnValues[0], (ConfigurationManager.AppSettings["AppKey"].ToString())));
        objEgForgotPasswordBL.VerificationCode = returnValues[1].Trim();
        string result = objEgForgotPasswordBL.VerifyCodeSMS();
        return objEncryptDecrypt.EncryptText(result + "|" + returnValues[2].Trim(), Key);
    }
    public string ForgotPasswordChangePassword(string Parameter)
    {
        AppEncryptionDecryption objEncryptDecrypt = new AppEncryptionDecryption();
        SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
        Parameter = objEncryptDecrypt.DecryptText(Parameter, Key);
        List<string> returnValues = new List<string>();
        returnValues = Parameter.Split('|').ToList();
        EgChangePasswordBL objChangePassword = new EgChangePasswordBL();
        //EgForgotPasswordBL objForgotPassword = new EgForgotPasswordBL();
        objChangePassword.Password = returnValues[1];//GeneralClass.Md5AddSecret(txtNewPassword.Text.Trim());
        objChangePassword.UserId = Convert.ToInt32(objEncrypt.DecryptString(returnValues[0], (ConfigurationManager.AppSettings["AppKey"].ToString())));
        string output = objChangePassword.AdminChangeUserPassword();

        if (output.Trim() == "2")
        {
            return objEncryptDecrypt.EncryptText("0|" + returnValues[2], Key); ;
        }
        if (output.Trim() == "3")
        {
            return objEncryptDecrypt.EncryptText("1|" + returnValues[2], Key); ;
        }
        return objEncryptDecrypt.EncryptText("-1|" + returnValues[2], Key);
    }
    #endregion

    #region MPIN Check and Create
    public string MpinLogin(string Parameter)
    {
        try
        {
            string UserId;
            string DeviceId;
            string Mpin;
            string Rnd;
            AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
            Parameter = objEncDec.DecryptText(Parameter, Key);
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            UserId = returnValues[0];
            DeviceId = returnValues[1];
            Mpin = returnValues[2];
            Rnd = returnValues[3];
            androidAppBL = new AndroidAppBL();
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            androidAppBL.UserID = objDecrypt.DecryptString(UserId, ConfigurationManager.AppSettings["AppKey"].ToString());
            androidAppBL.DeviceId = DeviceId;
            androidAppBL.Mpin = Mpin;
            androidAppBL.RND = Rnd;
            string ReturnVal = objEncDec.EncryptText(Convert.ToString(androidAppBL.GetMpinLogin()) + "|" + Rnd, Key);
            return ReturnVal;
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }


    public int ResetMpinEntry(string Parameter)
    {
        AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
        SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
        string UserId = objDecrypt.DecryptString(objEncDec.DecryptText(Parameter, Key), ConfigurationManager.AppSettings["AppKey"].ToString());
        androidAppBL = new AndroidAppBL();
        androidAppBL.UserID = UserId;
        return androidAppBL.ResetMpinEntry();
    }
    public string InsertMpinCredential(string Parameter)
    {
        try
        {
            string UserId;
            string DeviceId;
            string IPAddress;
            string Mpin;
            string Rnd;
            AppEncryptionDecryption objEncDec = new AppEncryptionDecryption();
            Parameter = objEncDec.DecryptText(Parameter, Key);
            List<string> returnValues = new List<string>();
            returnValues = Parameter.Split('|').ToList();
            UserId = returnValues[0];
            DeviceId = returnValues[1];
            IPAddress = returnValues[2];
            Mpin = returnValues[3];
            Rnd = returnValues[4];
            androidAppBL = new AndroidAppBL();
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            androidAppBL.UserID = objDecrypt.DecryptString(UserId, ConfigurationManager.AppSettings["AppKey"].ToString());
            androidAppBL.DeviceId = DeviceId;
            androidAppBL.IpAddress = IPAddress;
            androidAppBL.Mpin = Mpin;
            androidAppBL.InsertMpinCredential();
            return objEncDec.EncryptText("Success|" + Rnd, Key);
        }
        catch (Exception ex)
        {
            throw new FaultException(ex.Message);
        }
    }

    public string TestMethod(string Parameter)
    {
        return Parameter;
    }

    #endregion
}
