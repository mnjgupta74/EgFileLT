using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;

namespace EgBL
{
    public class AndroidAppBL
    {
        GenralFunction gf;
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string PasswordSHA256 { get; set; }
        public int UserId { get; set; }
        public int Usertype { get; set; }
        public string error { get; set; }
        public string machineID { get; set; }
        public string fullName { get; set; }
        public string headList { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
        public string ErrorCode { get; set; }
        public string IPAddress { get; set; }
        public string Userflag { get; set; }
        public string AddressUrl { get; set; }
        public sbyte RetrunVal { get; set; }
        public sbyte integration { get; set; }
        public bool isMpinCreated { get; set; }
        public string VerificationCode { get; set; }
        public string MobileNo { get; set; }
        public string GRN { get; set; }
        public string UserID { get; set; }
        public string UserType { get; set; }
        public string Page_Name { get; set; }
        public string ErrorName { get; set; }
        public string UserProfile { get; set; }
        public string ServiceId { get; set; }
        public string DeviceId { get; set; }
        public string IpAddress { get; set; }
        public string Mpin { get; set; }
        public string RND { get; set; }
        public string DeptCode { get; set; }
        public string VCode { get; set; }
        public int OfficeID { get; set; }
        public string TreasuryCode { get; set; }
        public DateTime LastModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfbirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string pinCode { get; set; }


        /// <summary>
        /// get user login info , attempt number,userid,usertype and check username and password valid and not 
        /// </summary>
        /// <returns> errorcode (after check username,password and attempt number), userid and usertype</returns>
        public string GetAppLogin()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[11];

            PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 20);
            PM[0].Value = LoginID;

            PM[1] = new SqlParameter("@Rnd", SqlDbType.Char, 10);
            PM[1].Value = RND;

            PM[2] = new SqlParameter("@Password", SqlDbType.VarChar, 255);
            PM[2].Value = Password;
                                        
            PM[3] = new SqlParameter("@UserID", SqlDbType.Int);
            PM[3].Value = UserId;
            PM[3].Direction = ParameterDirection.Output;

            PM[4] = new SqlParameter("@UserType", SqlDbType.TinyInt);
            PM[4].Value = Usertype;
            PM[4].Direction = ParameterDirection.Output;

            PM[5] = new SqlParameter("@ErrorCode", SqlDbType.Char, 2);
            PM[5].Value = ErrorCode;
            PM[5].Direction = ParameterDirection.Output;

            PM[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 16);
            PM[6].Value = IPAddress;

            PM[7] = new SqlParameter("@Userflag", SqlDbType.Char, 1);
            PM[7].Value = Userflag;
            PM[7].Direction = ParameterDirection.Output;
            PM[8] = new SqlParameter("@isMpinCreated", SqlDbType.Bit);
            PM[8].Value = 0;
            PM[8].Direction = ParameterDirection.Output;
            PM[9] = new SqlParameter("@DeviceId", SqlDbType.VarChar, 50);
            PM[9].Value = DeviceId;

            PM[10] = new SqlParameter("@SHAPassword", SqlDbType.VarChar, 500);
            PM[10].Value = PasswordSHA256;

            gf.UpdateData(PM, "UserLoginInfo_App");

            ErrorCode = PM[5].Value.ToString();
            if (PM[4].Value.ToString() != "") { Usertype = Convert.ToInt32(PM[4].Value); }
            else { Usertype = 0; }
            if (PM[3].Value.ToString() != "") { UserId = Convert.ToInt32(PM[3].Value.ToString()); }
            else { UserId = 0; }
            if (PM[7].Value.ToString() != "") { Userflag = PM[7].Value.ToString(); }
            else { Userflag = "N"; }
            if (PM[8].Value.ToString() != "") { isMpinCreated = Convert.ToBoolean(PM[8].Value.ToString()); }
            else { isMpinCreated = false; }

            return ErrorCode;
        }


        public string VerifyMobileNoGenerateOTP()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@MobileNo", SqlDbType.Char, 10) { Value = MobileNo };
            string Vcode = gf.ExecuteScaler(PM, "EgMobileNoVerify_App");
            return Vcode;
        }

        public string VerifyMobileNo_OTPCheck()
        {
            string Vcode = "";
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@VerifyCode", SqlDbType.Char, 6) { Value = VerificationCode };
            PM[1] = new SqlParameter("@UserID", SqlDbType.Char, 10) { Value = UserID };
            return gf.ExecuteScaler(PM, "EgVerifySMSCode_App");
            //SqlDataReader dr = gf.FillDataReader(PM, "EgVerifySMSCode");
            //if (dr.HasRows != false)
            //{
            //    if (dr.Read())
            //    {
            //        Vcode = dr[0].ToString();
            //    }
            //}
            //dr.Close();
            //dr.Dispose();
            //return Vcode.ToString();

        }

        public string send(string uid, string password, string message, string no, string SENDERID, string dlt_entity, string dlt_tempate_id)
        {
            EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            RemoteClass myremotepost = new RemoteClass();
            string plainText = string.Format("Vcode={0}|Mobile={1}", message, no);
            string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
            plainText = plainText + "|checkSum=" + CheckSum;

            string cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");

            return cipherText;
        }

        public string GetLatestVersionApp()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[0];
            return gf.ExecuteScaler(PM, "EgLatestVersion_App");
        }

        public string GetGRNSearchData()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt);
            PM[0].Value = Convert.ToInt64(GRN);
            PM[1] = new SqlParameter("@Userid", SqlDbType.BigInt);
            PM[1].Value = Convert.ToInt64(UserID);
            gf.Filldatatablevalue(PM, "EgGetGRNSearch_App", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetGRNDetail()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt);
            PM[0].Value = Convert.ToInt64(GRN);
            PM[1] = new SqlParameter("@Userid", SqlDbType.Int);
            PM[1].Value = Convert.ToInt64(UserID);
            PM[2] = new SqlParameter("@Usertype", SqlDbType.Int);
            PM[2].Value = Convert.ToInt64(10);
            gf.Filldatatablevalue(PM, "EgEchallanView", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetTopTransactions()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@Userid", SqlDbType.Int);
            PM[0].Value = Convert.ToInt64(UserID);
            gf.Filldatatablevalue(PM, "EgGetTransactionList_APP", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetGRNSchemas()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt);
            PM[0].Value = Convert.ToInt64(GRN);
            gf.Filldatatablevalue(PM, "EgGRNSchemas", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }


        // Add New Method  for  get Challan schemas for Repeat challan
        public string GetChallanSchemas()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt);
            PM[0].Value = Convert.ToInt64(GRN);
            PM[1] = new SqlParameter("@Userid", SqlDbType.Int);
            PM[1].Value = Convert.ToInt64(UserID);
            gf.Filldatatablevalue(PM, "EgChallanSchema_App", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string CheckExistingLogin()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20) { Value = LoginID };
            PM[1] = new SqlParameter("@MobileNo", SqlDbType.Char, 10) { Value = MobileNo };
            return gf.ExecuteScaler(PM, "EgCheckExistingLogin_App");
        }


        //Add New method for Create Challan schema
        public string CreateChallanSchemas()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserPro", SqlDbType.BigInt);
            PM[0].Value = Convert.ToInt64(UserProfile);
            PM[1] = new SqlParameter("@Userid", SqlDbType.Int);
            PM[1].Value = Convert.ToInt32(UserID);
            gf.Filldatatablevalue(PM, "EgGetUserProSchema_App", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }


        /// <summary>
        /// updation in mobile app
        /// </summary>
        /// <returns></returns>
        public string CreateServiceSchemas()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[1] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = ServiceId };
            PM[2] = new SqlParameter("@UserType", SqlDbType.Int) { Value = 10 };
            gf.Filldatatablevalue(PM, "EgGetServiceSchema_App", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string FillUserSchema()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
            // PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            return DataTableToJSONWithJavaScriptSerializer(gf.Filldatatablevalue(PARM, "EgGetProfileSchema_App", dt, null));
        }

        public int CheckExistingUser()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@MobileNo", SqlDbType.Int) { Value = MobileNo };
            PARM[1] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50) { Value = LoginID };
            string result = gf.ExecuteScaler(PARM, "EgCheckExistingMobileNo_App");
            if (result != "" && result != null)
                return Convert.ToInt32(result);
            else
                return 0;
        }

        public string GetOfficeList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = TreasuryCode };
            return DataTableToJSONWithJavaScriptSerializer(gf.Filldatatablevalue(PARM, "EgFillOfficeList", dt, null));
        }

        public string GetDivisionList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeID };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = TreasuryCode };
            return DataTableToJSONWithJavaScriptSerializer(gf.Filldatatablevalue(PARM, "EgGetDivision_App", dt, null));
        }


        // Add method  for Get Online  Banklist
        public string GetBanksList()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            gf.Filldatatablevalue(null, "EgChallanBankList", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetDepartmentsList()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDepartmentList", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetServiceDepartmentsList()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDepartmentListForService", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string GetMajorHeadList()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgDeptwiseMajorHeadList", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public void GetErrorLog()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ErrorName", SqlDbType.VarChar, 200) { Value = ErrorName };
            PM[1] = new SqlParameter("@PageName", SqlDbType.VarChar, 100) { Value = Page_Name };
            gf.UpdateData(PM, "GetErrorLog");
        }

        public string GetChallanTypeGRN()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Convert.ToInt64(GRN) };
            PM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            return gf.ExecuteScaler(PM, "EgGetChallanTypeByGrn_App");
        }

        public string GetChallanTypeProfile()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ProfileId", SqlDbType.Int) { Value = Convert.ToInt32(UserProfile) };
            PM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            return gf.ExecuteScaler(PM, "EgGetChallanTypeByProfile_App");
        }

        public string GetChallanTypeService()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = Convert.ToInt32(ServiceId) };
            return gf.ExecuteScaler(PM, "EgGetChallanTypeByService_App");
        }

        public int ResetMpinEntry()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            return gf.UpdateData(PM, "ResetMpin_App");
        }

        public void GetCatchErrorLog()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Convert.ToInt64(UserID) };
            PM[1] = new SqlParameter("@ErrorName", SqlDbType.VarChar, 200) { Value = ErrorName };
            PM[2] = new SqlParameter("@BLMethodName", SqlDbType.VarChar, 100) { Value = Page_Name };
            gf.UpdateData(PM, "GetCatchErrorLog_App");
        }

        public DataTable GetUserFullName()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable(); ;
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@Userid", SqlDbType.Int);
            PM[0].Value = Convert.ToInt64(UserID);
            gf.Filldatatablevalue(PM, "EgGetUserFullName_App", dt, null);
            return dt;
        }


        // method for get Status AFETR tRANSACTION
        /// <summary>
        /// updation in mobile app
        /// </summary>
        /// <returns></returns>
        public string GetGRNStatus()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@Userid", SqlDbType.BigInt) { Value = UserID };
           // return gf.ExecuteScaler(PM, "EgGRNStatus_App");
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgGRNStatus_App", dt, null);
            return dt.Rows[0][0].ToString();
        }


        // get Deaprtment Name and  Office Name
        public string GetDeaprtment(Int64 Grn)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            return gf.ExecuteScaler(PM, "EgGetDepartmentGRN_App");
        }


        // get Department Name By deptcode
        public string GetDeaprtmentList()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = Convert.ToInt32(UserProfile) };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            return gf.ExecuteScaler(PM, "EgGetDepartmentProfile_App");
        }

        public string GetDeaprtmentList_Service()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = Convert.ToInt32(ServiceId) };
            PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = Convert.ToInt32(DeptCode) };
            return gf.ExecuteScaler(PM, "EgGetDepartmentService_App");
        }


        // get Mpin Detail 
        public string GetMpinLogin()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            PM[1] = new SqlParameter("@DeviceId", SqlDbType.VarChar, 50) { Value = DeviceId };
            PM[2] = new SqlParameter("@Mpin", SqlDbType.VarChar, 50) { Value = Mpin };
            PM[3] = new SqlParameter("@Rnd", SqlDbType.Char, 10) { Value = RND };
            return gf.ExecuteScaler(PM, "EgGetMpinDetail_App");
        }

        public string GetPayeeDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            PM[1] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Convert.ToInt64(GRN) };
            return gf.ExecuteScaler(PM, "EgPayeeDetail_App");
        }

        public string GetTreasuryOfficeDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Convert.ToInt64(GRN) };
            PM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            return gf.ExecuteScaler(PM, "EgGetOfficeTreasurybyGRN_App");
        }

        public string CheckAuthorizationHeader()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            PM[1] = new SqlParameter("@Mpin", SqlDbType.VarChar, 50) { Value = Mpin };
            return gf.ExecuteScaler(PM, "EgAuthorizeHeader_App");
        }

        public int NewRegistration(string objData)
        {
            Registration objRegistration;
            objRegistration = JsonConvert.DeserializeObject<Registration>(objData);
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[23];
            PM[0] = new SqlParameter("@LoginID", SqlDbType.VarChar, 20) { Value = objRegistration.LoginID };
            PM[1] = new SqlParameter("@Dept", SqlDbType.VarChar, 1000) { Value = 0 };
            PM[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = objRegistration.FirstName };
            PM[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = objRegistration.LastName };
            PM[4] = new SqlParameter("@DOB", SqlDbType.DateTime) { Value = objRegistration.DOB };
            PM[5] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objRegistration.Address };
            PM[6] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objRegistration.City };
            PM[7] = new SqlParameter("@State", SqlDbType.Int) { Value = 19 };
            PM[8] = new SqlParameter("@Country", SqlDbType.Int) { Value = 1 };
            PM[9] = new SqlParameter("@MobilePhone", SqlDbType.VarChar, 10) { Value = objRegistration.MobilePhone };
            PM[10] = new SqlParameter("@PinCode", SqlDbType.Char, 6) { Value = 302020 };
            PM[11] = new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = objRegistration.Email };
            PM[12] = new SqlParameter("@Password", SqlDbType.VarChar, 250) { Value = objRegistration.Password };
            PM[13] = new SqlParameter("@VerificationCode", SqlDbType.VarChar, 50) { Value = 0 };
            PM[14] = new SqlParameter("@AttemptNumber", SqlDbType.VarChar, 150) { Value = 0 };
            PM[15] = new SqlParameter("@Identity", SqlDbType.VarChar, 50) { Value = 0 };
            PM[16] = new SqlParameter("@UserType", SqlDbType.TinyInt) { Value = 10 };
            PM[17] = new SqlParameter("@VCode", SqlDbType.Char, 6) { Value = 10 };
            PM[17].Direction = ParameterDirection.Output;
            PM[18] = new SqlParameter("@QuestionId", SqlDbType.Int) { Value = 0 };
            PM[19] = new SqlParameter("@Question", SqlDbType.NVarChar, 100) { Value = 1 };
            PM[20] = new SqlParameter("@Gender", SqlDbType.Char, 1) { Value = "M" };
            PM[21] = new SqlParameter("@MaritalStatus", SqlDbType.Char, 1) { Value = "M" };
            PM[22] = new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 };
            PM[22].Direction = ParameterDirection.Output;
            int x = gf.UpdateData(PM, "App_EgUserRegistration");
            //VCode = gf.ExecuteScaler(PM, "App_EgUserRegistration");
            if (x > 0)
            {
                MobileNo = objRegistration.MobilePhone;
                VCode = PM[17].Value.ToString();
                UserId = Convert.ToInt32(PM[22].Value.ToString());
                return 1;
                //if (VCode != "" && VCode != null)
                //    return 1;
                //else
                //    return 0;
            }
            else
                return 0;
        }

        private DataTable CreateSchemaAmtTable()
        {
            DataTable schemaAmtTable = new DataTable();
            schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
            schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
            schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));
            return schemaAmtTable;
        }

        public string InsertChallan(string objData, int UserID)
        {
            InsertChallan challan;
            challan = JsonConvert.DeserializeObject<InsertChallan>(objData);
            DataTable FinalSchema = CreateSchemaAmtTable();
            DataTable dt = new DataTable();
            dt = GetDataTableFromJsonString(challan.GRNSchema);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = FinalSchema.NewRow();
                dr["DeptCode"] = challan.DeptCode;
                dr["ScheCode"] = Convert.ToInt32(dt.Rows[i]["ScheCode"].ToString());
                dr["amount"] = Convert.ToDouble(dt.Rows[i]["amount"].ToString());
                dr["UserId"] = UserID;
                dr["BudgetHead"] = dt.Rows[i]["BudgetHead"].ToString();
                FinalSchema.Rows.Add(dr);
                FinalSchema.AcceptChanges();
            }
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[25];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
            PARM[1] = new SqlParameter("@Id", SqlDbType.Int) { Value = 0 };
            PARM[1].Direction = ParameterDirection.Output;
            PARM[2] = new SqlParameter("@FullName", SqlDbType.VarChar, 50) { Value = challan.FullName };
            PARM[3] = new SqlParameter("@DeductCommission", SqlDbType.Money) { Value = challan.DeductCommission };
            PARM[4] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = challan.TotalAmount };
            PARM[5] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = challan.City };
            PARM[6] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = challan.Address };
            PARM[7] = new SqlParameter("@PINCode", SqlDbType.Char, 6) { Value = challan.PinCode };
            PARM[8] = new SqlParameter("@Tin", SqlDbType.VarChar, 50) { Value = challan.TIN };
            PARM[9] = new SqlParameter("@BankName", SqlDbType.Char, 7) { Value = challan.BankCode };
            PARM[10] = new SqlParameter("@Remarks", SqlDbType.VarChar, 200) { Value = challan.Remarks };
            PARM[11] = new SqlParameter("@mytable", SqlDbType.Structured) { Value = FinalSchema };// added 9 sep 2014
            PARM[12] = new SqlParameter("@DivCode", SqlDbType.Int) { Value = challan.DivCode };
            PARM[13] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = challan.FromDate };
            PARM[14] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = challan.ToDate };
            PARM[15] = new SqlParameter("@profileId", SqlDbType.Int) { Value = challan.ProfileId };
            PARM[16] = new SqlParameter("@OfficeCode", SqlDbType.Int) { Value = challan.OfficeCode };
            PARM[17] = new SqlParameter("@Treasury", SqlDbType.Char, 4) { Value = challan.TreasuryCode };
            PARM[18] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = challan.GRN };
            PARM[19] = new SqlParameter("@MobileNo", SqlDbType.VarChar, 10) { Value = challan.MobileNumber };
            PARM[20] = new SqlParameter("@PanNumber", SqlDbType.VarChar, 10) { Value = challan.PanNumber };
            PARM[21] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 50) { Value = challan.IpAddress };
            PARM[22] = new SqlParameter("@DeviceId", SqlDbType.VarChar, 50) { Value = challan.DeviceId };
            PARM[23] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = challan.ProfileId };
            PARM[24] = new SqlParameter("@ServiceType", SqlDbType.Int) { Value = challan.ServiceType };
            int Rv = gf.UpdateData(PARM, "EgEChallan_App");
            //AndroidAppBL objdgjdgh = new AndroidAppBL();
            //objdgjdgh.ErrorName = Rv.ToString();
            //objdgjdgh.Page_Name = "InsertChallanNew" + Rv;
            //objdgjdgh.GetErrorLog();
            if (Rv == 0)
            {
                Rv = -1;
            }
            else
            {

                Rv = int.Parse(PARM[1].Value.ToString());
            }
            return Rv.ToString();
        }

        public void InsertMpinCredential()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(UserID) };
            PM[1] = new SqlParameter("@DeviceId", SqlDbType.VarChar, 50) { Value = DeviceId };
            PM[2] = new SqlParameter("@IpAddress", SqlDbType.VarChar, 50) { Value = IpAddress };
            PM[3] = new SqlParameter("@Mpin", SqlDbType.VarChar, 50) { Value = Mpin };
            gf.UpdateData(PM, "EgInsertMpinCredential_App");

        }


        //public byte[] GetGRNPDF()
        //{
        //    ReportViewer objReport = new ReportViewer();
        //    ReportParameter[] param = new ReportParameter[4];
        //    param[0] = new ReportParameter("UserId", Convert.ToString(UserID));
        //    param[1] = new ReportParameter("Usertype", Convert.ToString(10));
        //    param[2] = new ReportParameter("GRN", Convert.ToString(GRN));
        //    param[3] = new ReportParameter("ChallanNo", Convert.ToString(0));
        //    SSRS objssrs = new SSRS();
        //    objssrs.LoadSSRS(objReport, "EgDefaceDetailNew", param);
        //    ////create PDF
        //    byte[] returnValue = null;
        //    string format = "PDF";
        //    string deviceinfo = "";
        //    string mimeType = "";
        //    string encoding = "";
        //    string extension = "pdf";
        //    string[] streams = null;
        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;
        //    returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        //    return returnValue;
        //}


        public string GetDistrictsList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[0];
            return DataTableToJSONWithJavaScriptSerializer(gf.Filldatatablevalue(PARM, "EgFillLocation_App", dt, null));
        }

        public string GetTreasuryByOffice()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@officeId", SqlDbType.Int) { Value = OfficeID };
            return gf.ExecuteScaler(PARM, "EgFillOfficeWiseTreasury_App");
        }

        public string GetTreasuryList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[0];
            return DataTableToJSONWithJavaScriptSerializer(gf.Filldatatablevalue(PARM, "DropDownTreasury", dt, null));
        }

        public string GetUserProfileList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
            PM[1] = new SqlParameter("@CreationDate", SqlDbType.DateTime) { Value = LastModified };
            dt = gf.Filldatatablevalue(PM, "EgGetProfileListWithDeptDelete_App", dt, null);
            if (dt.Rows[0][0].ToString().Trim() == "0")
                return "0";
            else
                return DataTableToJSONWithJavaScriptSerializer(dt);
        }

        public string CheckEditMobileNoExist()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@MobileNo", SqlDbType.VarChar) { Value = MobileNo };
            return gf.ExecuteScaler(PM, "EgMobileNoExist_App");
        }

        public string EditData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };

            SqlDataReader dr = gf.FillDataReader(PARM, "EgGetUserEditDetail_App");
            if (dr.HasRows)
            {
                dr.Read();
                FirstName = dr["FirstName"].ToString();
                LastName = dr["LastName"].ToString();
                DateOfbirth = dr["DOB"].ToString();
                Email = dr["Email"].ToString();
                Address = dr["Address"].ToString();
                if (dr["City"].ToString() == "") { City = ""; }
                else { City = dr["City"].ToString().Trim(); }
                MobileNo = dr["MobilePhone"].ToString();
                pinCode = dr["PinCode"].ToString();
                dr.Close();
                dr.Dispose();
                return FirstName + "|" + LastName + "|"  + DateOfbirth + "|" + Email + "|" + Address + "|" + City + "|" + MobileNo + "|" + pinCode;
            }
            else
                dr.Close();
                dr.Dispose();
            return "";
        }

        public int UpdateUserData(string objData)
        {
            gf = new GenralFunction();
            Registration objRegistration;
            objRegistration = JsonConvert.DeserializeObject<Registration>(objData);
            SqlParameter[] PM = new SqlParameter[9];
            PM[0] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = objRegistration.FirstName };
            PM[1] = new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = objRegistration.LastName };
            PM[2] = new SqlParameter("@DOB", SqlDbType.DateTime) { Value = objRegistration.DOB };
            PM[3] = new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = objRegistration.Address };
            PM[4] = new SqlParameter("@City", SqlDbType.VarChar, 40) { Value = objRegistration.City };
            PM[5] = new SqlParameter("@MobilePhone", SqlDbType.VarChar, 10) { Value = objRegistration.MobilePhone };
            PM[6] = new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = objRegistration.Email };
            PM[7] = new SqlParameter("@userId", SqlDbType.Int) { Value = UserId };
            PM[8] = new SqlParameter("@VCode", SqlDbType.Char, 6) { Value = VCode };
            PM[8].Direction = ParameterDirection.Output;
            int x = gf.UpdateData(PM, "EgEditProfile_App");
            if (x > 0)
            {
                VCode = PM[8].Value.ToString();
                MobileNo = objRegistration.MobilePhone;
            }
            return x;
        }

        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

        public DataTable GetDataTableFromJsonString(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {

                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public string GetServiceNameList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@UserType", SqlDbType.Int) { Value = Usertype };
            gf.Filldatatablevalue(PARM, "EgGetServiceNameList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        /// <summary>
        /// service challan - 31/12/19
        /// </summary>
        /// <returns></returns>
        public string CreateServiceChallan()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[1] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = ServiceId };
            PM[2] = new SqlParameter("@UserType", SqlDbType.Int) { Value = 10 };
            gf.Filldatatablevalue(PM, "EgGetServiceSchema_App", dt, null);
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }

    }


    class InsertChallan
    {

        public Int32 ProfileId { get; set; }

        public Int64 GRN { get; set; }

        public Int32 OfficeCode { get; set; }

        public Int32 DeptCode { get; set; }

        public string TreasuryCode { get; set; }

        public string FullName { get; set; }

        public double DeductCommission { get; set; }

        public double TotalAmount { get; set; }

        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string PanNumber { get; set; }
        public string IpAddress { get; set; }
        public string DeviceId { get; set; }
        public string Address { get; set; }

        public string PinCode { get; set; }

        public string Remarks { get; set; }

        public string TIN { get; set; }

        public string BankCode { get; set; }

        public string GRNSchema { get; set; }

        public Int32 DivCode { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CTDZone { get; set; }

        public string CTDCircle { get; set; }

        public string CTDWard { get; set; }

        public Int32 ServiceId { get; set; }
        public Int32 ServiceType { get; set; }
    }





    class Registration
    {
        public string LoginID { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public int UserId { get; set; }

        public DateTime DOB { get; set; }
    
    }
}
