using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using System.Web;


namespace EgBL
{
    public class EgLoginBL
    {
        GenralFunction gf;

        #region properties
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string SHAPassword { get; set; }
        public string RND { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public string ErrorCode { get; set; }
        public string IPAddress { get; set; }
        public string pdfname { get; set; }
        public int pdfId { get; set; }
        // public string OldPassword { get; set; }//changes by shubhang

        //Add properties
        public string Userflag { get; set; }
        public string AddressUrl { get; set; }
        public sbyte RetrunVal { get; set; }
        public sbyte integration { get; set; }

        public string BsrCode { get; set; }
        //----------------------------------
        #endregion

        #region method


        /// <summary>
        /// get user login info , attempt number,userid,usertype and check username and password valid and not 
        /// </summary>
        /// <returns> errorcode (after check username,password and attempt number), userid and usertype</returns>
        //public string GetLogin()
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[7];

        //    PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar , 20);
        //    PM[0].Value = LoginID;

        //    PM[1] = new SqlParameter("@Rnd", SqlDbType.Char, 10);
        //    PM[1].Value = RND;

        //    PM[2] = new SqlParameter("@Password", SqlDbType.VarChar, 255);
        //    PM[2].Value = Password;

        //    PM[3] = new SqlParameter("@UserID", SqlDbType.Int);
        //    PM[3].Value = UserId;
        //    PM[3].Direction = ParameterDirection.Output;

        //    PM[4] = new SqlParameter("@UserType", SqlDbType.TinyInt);
        //    PM[4].Value = UserType;
        //    PM[4].Direction = ParameterDirection.Output;

        //    PM[5] = new SqlParameter("@ErrorCode", SqlDbType.Char, 2);
        //    PM[5].Value = ErrorCode;
        //    PM[5].Direction = ParameterDirection.Output;

        //    PM[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 16);
        //    PM[6].Value = IPAddress;

        //    gf.UpdateData(PM, "UserLoginInfo");

        //    ErrorCode = PM[5].Value.ToString();
        //    if (PM[4].Value.ToString() != "") { UserType = Convert.ToInt32(PM[4].Value); }
        //    else { UserType = 0; }
        //    if (PM[3].Value.ToString() != "") { UserId = Convert.ToInt32(PM[3].Value.ToString()); }
        //    else { UserId = 0; }


        //    return ErrorCode;

        //}
        // Add new 30/3/2015
        public string GetLogin(int integrate)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[8];

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
            PM[4].Value = UserType;
            PM[4].Direction = ParameterDirection.Output;

            PM[5] = new SqlParameter("@ErrorCode", SqlDbType.Char, 2);
            PM[5].Value = ErrorCode;
            PM[5].Direction = ParameterDirection.Output;

            PM[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 16);
            PM[6].Value = IPAddress;

            PM[7] = new SqlParameter("@Userflag", SqlDbType.Char, 1);
            PM[7].Value = Userflag;
            PM[7].Direction = ParameterDirection.Output;
            gf.UpdateData(PM, "UserLoginInfo");

            ErrorCode = PM[5].Value.ToString();
            if (PM[4].Value.ToString() != "") { UserType = Convert.ToInt32(PM[4].Value); }
            else { UserType = 0; }
            if (PM[3].Value.ToString() != "") { UserId = Convert.ToInt32(PM[3].Value.ToString()); }
            else { UserId = 0; }
            if (PM[7].Value.ToString() != "") { Userflag = PM[7].Value.ToString(); }
            else { Userflag = "N"; }


            return ErrorCode;
        }
        // Add on 30/3/2015
        public sbyte GetLogin()
        {
            try
            {
                gf = new GenralFunction();
                SqlParameter[] PM = new SqlParameter[9];

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
                PM[4].Value = UserType;
                PM[4].Direction = ParameterDirection.Output;

                PM[5] = new SqlParameter("@ErrorCode", SqlDbType.Char, 2);
                PM[5].Value = ErrorCode;
                PM[5].Direction = ParameterDirection.Output;

                PM[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 16);
                PM[6].Value = IPAddress;

                PM[7] = new SqlParameter("@Userflag", SqlDbType.Char, 1);
                PM[7].Value = Userflag;
                PM[7].Direction = ParameterDirection.Output;

                PM[8] = new SqlParameter("@SHAPassword", SqlDbType.VarChar, 255);
                PM[8].Value = SHAPassword;
                gf.UpdateData(PM, "UserLoginInfo");

                ErrorCode = PM[5].Value.ToString();
                if (PM[4].Value.ToString() != "") { UserType = Convert.ToInt32(PM[4].Value); }
                else { UserType = 0; }
                if (PM[3].Value.ToString() != "") { UserId = Convert.ToInt32(PM[3].Value.ToString()); }
                else { UserId = 0; }
                if (PM[7].Value.ToString() != "") { Userflag = PM[7].Value.ToString(); }
                else { Userflag = "N"; }

                if (Convert.ToInt16(ErrorCode) == -2 || Convert.ToInt16(ErrorCode) == 0)
                {
                    if (Userflag == "D")
                    {
                        HttpContext.Current.Session["MobileUpdate"] = "1";
                        AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                        SessionDeActive();
                    }
                    else
                    {
                        switch (UserType)
                        {
                            case 10:
                                if (Userflag == "N")
                                {
                                    AddressUrl = "~/WebPages/EgHome.aspx";
                                    SessionActive(1);
                                }
                                else if (Userflag == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "2";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }
                                else
                                {
                                    AddressUrl = "~/WebPages/EgHome.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 9:
                                AddressUrl = "~/WebPages/reports/eggrnsearch.aspx";
                                SessionActive(1);
                                break;

                            case 8:     // Treasury Report  User
                                if (Userflag == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "1";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }
                                else
                                {
                                    AddressUrl = "~/WebPages/Reports/EgReportTy11.aspx";
                                    SessionActive(1);
                                }
                                break;

                            case 7:
                                if (Userflag == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "1";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }
                                else
                                {
                                    AddressUrl = "~/WebPages/AG/EgAgDepartment.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 6:
                                if (Userflag == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "1";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }
                                else
                                {
                                    AddressUrl = "~/WebPages/banksoftcopy.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 5:


                                if (Userflag.ToUpper() == "N")
                                {
                                    AddressUrl = "~/WebPages/EgDeptChangePassword.aspx";
                                    SessionDeActive();
                                }
                                else if (Userflag.ToUpper() == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "1";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }
                                else
                                {
                                    AddressUrl = "~/WebPages/egdepartment.aspx";
                                    SessionActive(1);
                                }

                                break;
                            case 4:
                                if (Userflag.ToUpper() == "N" || Userflag.ToUpper() == "P")
                                {
                                    HttpContext.Current.Session["MobileUpdate"] = "1";
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }

                                else if (Userflag.ToUpper() == "U")
                                {
                                    AddressUrl = "~/WebPages/EgChangeUserLoginID.aspx";
                                    SessionActive(1);

                                }

                                else
                                {
                                    AddressUrl = "~/WebPages/egdepartment.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 3:
                                if (Userflag.ToUpper() == "N" || Userflag.ToUpper() == "P")
                                {
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }

                                else
                                {
                                    AddressUrl = "~/WebPages/egdepartment.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 2:
                                if (Userflag.ToUpper() == "P")
                                {
                                    AddressUrl = "~/WebPages/EgEditUserProfile.aspx";
                                    SessionDeActive();
                                }

                                else
                                {
                                    AddressUrl = "~/WebPages/Admin/EgChallanHistory.aspx";
                                    SessionActive(1);
                                }
                                break;
                            case 1:
                                {
                                    // CodeGenerate();
                                    SessionDeActive();
                                    AddressUrl = "~/WebPages/EgHome.aspx";
                                    SessionActive(1);

                                }

                                break;
                        }

                        RetrunVal = 0;
                    }
                }
                else if (Convert.ToInt16(ErrorCode) == -1)
                {
                    SessionActive(1);
                    AddressUrl = "~/WebPages/Account/EgChangePassword.aspx";
                    RetrunVal = -1;
                }

                else if (Convert.ToInt16(ErrorCode) == 2)
                {
                    AddressUrl = "Your account is blocked.\n Please try after 30 minutes.";
                    RetrunVal = 2;
                }
                else
                {
                    AddressUrl = "Invalid UserName/Password.";
                    RetrunVal = 3;
                }




            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "3stIF");
            }
            return RetrunVal;
        }
        // Add method 30/3/2015

        public void SessionActive(int mode)
        {
            try
            {

                if (mode == 2)
                {
                    gf = new GenralFunction();
                    SqlParameter[] PARM = new SqlParameter[1];
                    PARM[0] = new SqlParameter("@UserId", SqlDbType.BigInt) { Value = UserId };
                    SqlDataReader dr = gf.FillDataReader(PARM, "EgSessionActive");
                    if (dr.HasRows)
                    {
                        dr.Read();

                        HttpContext.Current.Session["UserId"] = UserId;
                        HttpContext.Current.Session["UserType"] = dr["user_type"].ToString();
                        HttpContext.Current.Session["userName"] = dr["loginid"].ToString();
                        HttpContext.Current.Session["MenuDataSet"] = "";
                        HttpContext.Current.Session["GrnNumber"] = "";
                        dr.Close();
                        dr.Dispose();
                    }
                    else
                    {
                        dr.Close();
                        dr.Dispose();

                    }
                }
                else if (mode == 1)
                {
                    HttpContext.Current.Session["UserId"] = UserId;
                    HttpContext.Current.Session["UserType"] = UserType;
                    HttpContext.Current.Session["userName"] = LoginID;
                    HttpContext.Current.Session["MenuDataSet"] = "";
                    HttpContext.Current.Session["GrnNumber"] = "";

                }
                else
                {
                    HttpContext.Current.Session["UserId"] = "";
                    HttpContext.Current.Session["UserType"] = "";
                    HttpContext.Current.Session["userName"] = "";
                    HttpContext.Current.Session["MenuDataSet"] = "";
                    HttpContext.Current.Session["GrnNumber"] = "";

                }


            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "4stIF");
            }
        }

        public void SessionDeActive()
        {
            HttpContext.Current.Session["DeActive"] = UserId;
            HttpContext.Current.Session["DeActive1"] = ErrorCode;
            HttpContext.Current.Session["UserType"] = UserType;
            HttpContext.Current.Session["UserId"] = "";
        }
        public string GetUserFlag()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int);
            PM[0].Value = UserId;

            dt = gf.Filldatatablevalue(PM, "EgGetUserFlag", dt, null);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0][0]);

            }
            else
            {
                return "0";
            }

        }
        #endregion
        public string UpdateUserLoginID()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50) { Value = LoginID };
            PM[1].Direction = ParameterDirection.Output;
            gf.UpdateData(PM, "EgUpdateOfficeLoginID");
            if (PM[1].Value.ToString() != null)
            { return LoginID = PM[1].Value.ToString(); }
            else
            {
                return LoginID = "";
            }
        }

        //ADD FOR nODAL officer detail
        public DataTable GetNodalOfficerDetails()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGetDepartmetnNodalOfficer", dt, null);
            return dt;
        }
        public DataTable PopulateDepartmentList() // fill department Droddownlist
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGetDepartmentList", dt, null);
            return dt;
        }
        public void CircularPdf(Repeater rpt)
        {
            gf = new GenralFunction();
            PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGetPdfFile", dt, null);
            //DataView objdv = new DataView(dt);
            //objpds.DataSource = objdv;
            if (dt.Rows.Count > 0)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
                dt.Dispose();
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
            }

        }

        public void EditCircularPdf()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@pdfname", SqlDbType.VarChar, 100) { Value = pdfname };
            PM[1] = new SqlParameter("@pdfid", SqlDbType.Int) { Value = pdfId };
            gf.UpdateData(PM, "EgUpdateGetPdfFile");
        }

        public void DeleteCircularPdf()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@pdfid", SqlDbType.Int) { Value = pdfId };
            gf.UpdateData(PM, "EgDeleteGetPdfFile");
        }


        public DataTable GetBankOfficerDetails()
        {



            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char,7);
            PM[0].Value = BsrCode;

            dt = gf.Filldatatablevalue(PM, "EgGetBankOfficersDetail", dt, null);
            if (dt.Rows.Count > 0)
            {

                return dt;
            }
            else
            {
                return dt;
            }


           
        }
    }
}
