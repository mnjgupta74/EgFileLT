using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgForgotPasswordBL
    {
        GenralFunction gf;
        #region Class Properties
        public string LoginID { get; set; }
        public int UserId { get; set; }
        public string VerificationCode { get; set; }
        public string Password { get; set; }
        #endregion
        #region Function

        /// <summary>
        /// Check User Valid and Invalid
        /// </summary>
        /// <returns> userid</returns>
        //public string CheckExistingLogin()
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[1];
        //    PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20) { Value = LoginID };
        //    return gf.ExecuteScaler(PM, "EgCheckExistingLogin");
        //}
        public string CheckExistingLogin()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20) { Value = LoginID };
            return gf.ExecuteScaler(PM, "EgCheckLogin");
        }

        //public string CheckForgetPassword(SqlTransaction Trans)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@Password", SqlDbType.Char, 255) { Value = Password };
        //    PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
        //    string i = gf.ExecuteScaler(PM, "EgCheckForgetPassword", Trans);
        //    return i;
        //}
        /// <summary>
        /// Keep Record of SMS
        /// </summary>
        public void SMSCount()
        {
            gf = new GenralFunction();
            gf.UpdateData(null, "SMSCountFinYearWise");

        }

        /// <summary>
        /// Create Function for get verification code if request time Hour is less than 24hr to last request time 
        /// </summary>
        /// <returns>if time(Hour) less than 24hr then  return Verificationcode else return 0 </returns>
        public string VerifyCode()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            string Vcode = gf.ExecuteScaler(PM, "EgVerifyMobileCode");
            return Vcode;
        }

        /// <summary>
        /// Get User Verification code is valid and Not
        /// </summary>
        /// <returns>1:- Invalid Varificationcode,0,2:-Verifiaction code is expires</returns>
        public string VerifyCodeSMS()
        {
            string Vcode = "";
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            PM[1] = new SqlParameter("@VerifyCode", SqlDbType.Char, 6) { Value = VerificationCode };
            //return gf.ExecuteScaler(PM, "EgVerifySMSCode");
            SqlDataReader dr = gf.FillDataReader(PM, "EgVerifySMSCode");
            if (dr.HasRows != false)
            {
                if (dr.Read())
                {
                    Vcode = dr[0].ToString();
                }

            }
            dr.Close();
            dr.Dispose();
            return Vcode.ToString();

        }

        public string GetLoginId()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            string Vcode = gf.ExecuteScaler(PM, "EgSendLoginIdOnMobile");
            return Vcode;
        }

        /// <summary>
        /// Get Login Id Base On UserId 
        /// </summary>
        /// <returns></returns>
        public string GetLoginByUserId()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            SqlDataReader dr = gf.FillDataReader(PM, "EgGetLoginID");
            if (dr.HasRows != false)
            {
                if (dr.Read())
                {
                    LoginID = dr[0].ToString();
                }

            }
            dr.Close();
            dr.Dispose();
            return LoginID.ToString();
        }
        #endregion
    }
}
