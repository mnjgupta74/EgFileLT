using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgChangePasswordBL
    {

        GenralFunction gf = new GenralFunction();
        #region Class Properties
        /// <summary>
        /// EgChangePasswordBL Class Properties
        /// </summary>
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string Rnd { get; set; }
        public string rslt { get; set; }
        public string LoginID { get; set; }
        public string TreasuryCode { get; set; }
        public Int64 MobileNumber { get; set; }
        #endregion
        #region Function

        ///// <summary>
        ///// update New  password 
        ///// </summary>
        ///// <returns></returns>
        //public string ChangeUserPassword()
        //{
        //    SqlParameter[] PM = new SqlParameter[2];
        //    //PM[0] = new SqlParameter("@Rnd", SqlDbType.Char, 10) { Value = Rnd };
        //    PM[0] = new SqlParameter("@Password", SqlDbType.Char, 255) { Value = Password };
        //    PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
        //    int i = gf.UpdateData(PM, "EgUpdateNewUserPassword");
        //    return i.ToString();
        //}

        public int UpdateLoginAttempt()                                                    // Function to Update Login Attempts(  Only for Admin )
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20) { Value = LoginID };
            int i = gf.UpdateData(PM, "ResetLoginAttempt");
            return i;
        }

        public string UpdateNewPassword()
        {
            SqlParameter[] PM = new SqlParameter[4];

            PM[0] = new SqlParameter("@Password", SqlDbType.Char, 255);
            PM[0].Value = Password;
            PM[1] = new SqlParameter("@OldPassword", SqlDbType.Char, 255);
            PM[1].Value = OldPassword;
            PM[2] = new SqlParameter("@UserId", SqlDbType.Int);
            PM[2].Value = UserId;
            PM[3] = new SqlParameter("@rslt", SqlDbType.Char, 1);
            PM[3].Value = rslt;
            PM[3].Direction = ParameterDirection.Output;
            gf.UpdateData(PM, "ChangeUserPassword");
            rslt = PM[3].Value.ToString();

            return rslt;
        }
        public void EgUpdateDeptFlag()
        {
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.UpdateData(PM, "EgUpdateDeptFlag");
        }
        /// <summary>
        /// get user login ID
        /// </summary>
        /// <param name="ddl"></param>
        public void FillLoginId(DropDownList ddl)
        {

            gf.FillListControl(ddl, "EgLoginIdList", "LoginID", "UserId", null);

            ddl.Items.Insert(0, new ListItem("--Select LoginID--", "0"));
        }
        /// <summary>
        /// Change User Admin User Password
        /// </summary>
        public string AdminChangeUserPassword()
        {
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
            PM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 250) { Value = Password };
            PM[2] = new SqlParameter("@rslt", SqlDbType.Char, 1) { Value = rslt };
            PM[2].Direction = ParameterDirection.Output;
            PM[3] = new SqlParameter("@LoginId", SqlDbType.VarChar, 20) { Value = LoginID };
            gf.UpdateData(PM, "EgAdminChangeUserPassword");
            rslt = PM[2].Value.ToString().Trim();
            return rslt;
        }
        /// <summary>
        /// Check User Already LoginID and Get PersonalDetails 
        /// </summary>
        /// <returns></returns>
        public DataTable GetChangeUserPasswordDetails()
        {
            DataTable dt = new DataTable();

            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20);
            PM[0].Value = LoginID;
            dt = gf.Filldatatablevalue(PM, "EgGetChangeUserPasswordDetails", dt, null);
            return dt;
        }
        /// <summary>
        /// Check User Already LoginID and Get PersonalDetails 
        /// </summary>
        /// <returns></returns>
        public DataTable GetSearchLoginId()
        {
            DataTable dt = new DataTable();

            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20);
            PM[0].Value = LoginID;
            dt = gf.Filldatatablevalue(PM, "EgSearchLoginId", dt, null);
            return dt;
        }
        /// <summary>
        /// Create For Office Change PAssword . Get Treasury Wise Office List (use in Account/EgOfficeChangePassword)
        /// </summary>
        /// <param name="ddl">Office DropDownList</param>
        public void BindOfficeList(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 10) { Value = TreasuryCode };
            gf.FillListControl(ddl, "EgGetDepartmentwiseOffice", "officename", "officeid", PARM);
            ddl.Items.Insert(0, new ListItem("--Select Office--", "0"));
        }
        #endregion


        // Add  Method to check  Mobile No Exist Or Not In  System User_type Wise   21 may 2019
        public string  CheckMobileNoExistence()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@LoginiD", SqlDbType.VarChar, 30) { Value = LoginID };
            PM[1] = new SqlParameter("@MobileNo", SqlDbType.BigInt) { Value = MobileNumber };
            string res = gf.ExecuteScaler(PM, "[EgCheckMobileNoExistence]");
            return res;
        }

        // Add Mothod  for Update  Mobile Number  21 May 2019
        public int UpdateMobileNumber()                                                    // Function to Update Login Attempts(  Only for Admin )
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@LoginiD", SqlDbType.VarChar, 30) { Value = LoginID };
            PM[1] = new SqlParameter("@MobileNo", SqlDbType.BigInt) { Value = MobileNumber };
            int i = Convert.ToInt16( gf.ExecuteScaler(PM, "[EgCheckMobileNoForUpdation]"));
            return i;
        }
    }
}
