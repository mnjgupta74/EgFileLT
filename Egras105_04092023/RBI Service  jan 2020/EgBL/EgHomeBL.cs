using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgHomeBL
    {
        GenralFunction gf;
        #region Class Properties
        /// <summary>
        /// EgHome Class Properties 
        /// </summary>

        public int UserPro { get; set; }
        public string lastflogin { get; set; }
        public DateTime lastslogin { get; set; }
        public string lastchangepass { get; set; }
        public string Ptype { get; set; }
        public string ddlProfileSelectedValue { get; set; }
        public string RblTransactionSelectedValue { get; set; }
        public bool RptVisible { get; set; }
        public bool ErrorMessageVisible { get; set; }
        public string UrlWithData { get; set; }
        public string RptCommandName { get; set; }
        public string RptCommandArgument { get; set; }
        public bool LinkVerifyVisible { get; set; }
        public Int32 UserId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Identity { get; set; }
        public string output { get; set; }
        public string Grn { get; set; }

        public string DDO { get; set; }
        public string rbltype { get; set; } // added by priya on 30th march 2018 to identify url redirect for challan page
        public int ServiceId { get; set; }
        public int DeptCode { get; set; }
        public int ProcUserId {get; set;}
        #endregion
        #region Function


        /// <summary>
        /// gets all the made profile of logined user
        /// </summary>
        /// <param name="ddl"></param>
        public virtual void GetProfileListWithDepartment(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@UserType", SqlDbType.Int) { Value = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserType"].ToString()) };
            PM[2] = new SqlParameter("@Type", SqlDbType.Bit) { Value = 1 };// added to to get profile names that are activated
            gf.FillListControl(ddl, "EgGetProfileListwithdeptdelete", "UserProfile", "UserPro", PM);
            ddl.Items.Insert(0, new ListItem("--Select Profile--", "0"));
        }

        /// <summary>
        /// get last 20 Transaction
        /// </summary>
        /// <returns> datatable</returns>
        public DataTable BinTransactionPayment()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = RblTransactionSelectedValue == "" ? null : RblTransactionSelectedValue };
            dt = gf.Filldatatablevalue(PM, "EgGetTransactionList", dt, null);
            return dt;
        }

        /// <summary>
        /// get last 20 Transaction on profile
        /// </summary>
        /// <returns> datatable</returns>
        public DataTable GetProfileWiseTransactionTable()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@Profile", SqlDbType.Int) { Value = UserPro };
            PM[2] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = RblTransactionSelectedValue == "" ? null : RblTransactionSelectedValue };

            dt = gf.Filldatatablevalue(PM, "EgProfileWiseTransactionList", dt, null);
            return dt;
        }

        /// <summary>
        /// gets all the schema of selected profile by user
        /// </summary>
        /// <param name="rpt"></param>
        public void FillUserSchemaRpt(Repeater rpt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };

            PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetProfileSchema", dt, null);
            //DataView objdv = new DataView(dt);
            //objpds.DataSource = objdv;
            rpt.DataSource = dt;
            rpt.DataBind();
        }

        ///<Summary>
        ///Fill user Schema for Minus Expensditure
        ///
        public void FillUserSchemaRptME(Repeater rpt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };

            PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetProfileSchemaME", dt, null);
            DataView objdv = new DataView(dt);
            objpds.DataSource = objdv;
            rpt.DataSource = objpds;
            rpt.DataBind();
        }


        /// <summary>
        /// gets all the logined user persional details
        /// </summary>
        /// <returns></returns>
        public int GetUserDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };


            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgGetUserDetail", dt, null);

            int output = 0;
            if (dt.Rows.Count != 0)
            {
                FirstName = dt.Rows[0]["Name"].ToString();
                lastslogin = Convert.ToDateTime(dt.Rows[0]["Last_slogin_date"].ToString());
                if (dt.Rows[0]["Last_flogin_date"].ToString() == "")
                {
                    lastflogin = dt.Rows[0]["Last_flogin_date"].ToString();
                }
                else
                {
                    lastflogin = dt.Rows[0]["Last_flogin_date"].ToString();
                }
                if (dt.Rows[0]["PasswordChange_Date"].ToString() == "")
                {
                    lastchangepass = dt.Rows[0]["PasswordChange_Date"].ToString();
                }
                else
                {
                    lastchangepass = dt.Rows[0]["PasswordChange_Date"].ToString();
                }
                Address = dt.Rows[0]["Address"].ToString();
                City = dt.Rows[0]["City"].ToString().Trim();
                PinCode = dt.Rows[0]["Pincode"].ToString();
                Identity = dt.Rows[0]["Identity"].ToString();
                output = 1;
            }
            else
            {
                output = 0;
            }
            return output;
        }

        /// <summary>
        /// Fill DistrictList
        /// </summary>
        /// <returns></returns>
        public DataTable FillDistrictList()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgFillDistrictList", dt, null);
            return dt;
        }
        /// <summary>
        /// fill Banks
        /// </summary>
        /// <returns></returns>
        public DataTable GetBank()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgChallanBankList", dt, null);
            return dt;

        }

       

        /// <summary>
        /// Redirect to Echallan on Continue
        /// </summary>
        /// <returns></returns>
        public string RedirectToEChallan()
        {
            string msg = string.Empty;
            UrlWithData = string.Empty;
            if (ServiceId > 0)
            {   // Add ProUserId For Identity Challan Is Related to Proc Tender Fee Or Non Proc Tender fee  22 April 2020
                string service = "service|" + ServiceId + "|" + DeptCode + "|" + ProcUserId;
                var ObjEncryptDecrypt = new EgEncryptDecrypt();
                string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Service={0}", service));
                UrlWithData = "EgEChallan.aspx?" + strURLWithData;
            }
            else
            {
                if (ddlProfileSelectedValue == "0" )
                {
                    msg = "Please Select Profile.!";
                }
                else
                {
                    var ObjEncryptDecrypt = new EgEncryptDecrypt();
                    string[] userpro = ddlProfileSelectedValue.Split('-');
                    if (userpro[1] != "2")
                    {
                        if (rbltype == "2") // to identify ME or general challan
                        {
                            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Profile={0}", userpro[0]));
                            UrlWithData = "EgEChallanForOffice.aspx?" + strURLWithData;
                        }
                        else
                        {
                            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Profile={0}", userpro[0]));
                            UrlWithData = "EgEChallan.aspx?" + strURLWithData;
                        }
                    }
                    else
                    {
                        msg = "Department/Profile has been De-Active.";
                    }
                }

            }
            return msg;
        }

        /// <summary>
        /// Manual or Online Challan View
        /// </summary>
        /// <returns></returns>
        public string ChallanView()
        {
            var ObjEncryptDecrypt = new EgEncryptDecrypt();
            string msg = string.Empty;
            UrlWithData = string.Empty;
            if (RptCommandName.Equals("Status"))
            {
                string[] grn = Convert.ToString(RptCommandArgument).Split('&');
                Grn = grn[0].ToString() ;
                if (grn[1].ToString() == "Manual")
                {
                    //if (LinkVerifyVisible == true)
                        UrlWithData = "~/webpages/reports/EgManualChallan.aspx?" + ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}", grn[0].ToString().Trim()));
                }
                else
                {
                    UrlWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}", grn[0].ToString(), UserId, UserType));
                    UrlWithData = "EgDefaceDetailNew.aspx?" + UrlWithData;
                }

            }
            if (RptCommandName.Equals("Repeat"))
            {
                EgVisibility objVisible = new EgVisibility();


                string[] grn = Convert.ToString(RptCommandArgument).Split('&');
                Grn = grn[0].ToString();
                //DDO = grn[1].ToString();
                bool eligible = objVisible.IsEligible(Convert.ToInt64(Grn));
                if (eligible == true)
                {

                    if (RblTransactionSelectedValue == "E")
                    {
                        UrlWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&Type={1}", Grn, "Repeat"));
                        UrlWithData = "EgEchallanForOffice.aspx?" + UrlWithData.ToString();
                    }

                    else

                    {
                        UrlWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&Type={1}", Grn, "Repeat"));
                        UrlWithData = "EgEchallan.aspx?" + UrlWithData.ToString();
                    }
                }
                else
                {
                    msg = "This TRANSACTION would not process!";
                }
            }
            if (RptCommandName.Equals("PDF"))
            {
                Grn=RptCommandArgument;
                UrlWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&Type={1}", RptCommandArgument, "PDF"));
                UrlWithData = "Reports/EgEchallanViewPDF.aspx?" + UrlWithData.ToString();
            }
            return msg;
        }
        #endregion
    }
}
