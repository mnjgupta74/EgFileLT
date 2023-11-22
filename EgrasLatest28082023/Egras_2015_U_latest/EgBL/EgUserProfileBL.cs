using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EgBL
{
    public class EgUserProfileBL
    {
        GenralFunction gf;
        #region Class Properties
        public int ScheCode { get; set; }
        public string rbltype { get; set; } 
        public int ServiceId { get; set; }
        public int ProfileId { get; set; }
        public string UrlWithData { get; set; }
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public int UserId { get; set; }
        public int UserPro { get; set; }
        public string ProfileName { get; set; }
        public List<EgUserProfileBL> lst { get; set; }
        public string majorheadcode { get; set; }
        public int UserType { get; set; }

        #endregion
        #region Function

        /// <summary>
        /// show Major Head according to department Code
        /// </summary>
        /// <param name="ddl"> </param>
        public DataTable fillDeptWiseMajorHeadList()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PM, "EgDeptwiseMajorHeadList", dt, null);
        }
        /// <summary>
        /// fills DepartmentList 
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddl, "EgGetDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }
        public void GetServiceDepartmentsList(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddl, "EgGetDepartmentListForService", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }

        /// <summary>
        /// show Budget Head according to department select
        /// </summary>
        /// <param name="ddl"> </param>
        public void filldropdownbudgetheadlist(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            gf.FillListControl(ddl, "EgBudgetHeadList", "BudgetHead", "BudgetHead", PM);
            ddl.Items.Insert(0, new ListItem("--Select Budget Head--", "0"));
        }
        public DataTable GetSchemaBudgetName()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = majorheadcode };
            PARM[2] = new SqlParameter("@UserType", SqlDbType.Int) { Value = UserType };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PARM, "EgGetSchemaBudgetHeadName", dt, null);

        }

        /// <summary>
        /// insert the profile created by user
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int InsertUserProfile(List<EgUserProfileBL> lst)
        {
            int result = 0;
            gf = new GenralFunction();
            foreach (var item in lst)
            {
                SqlParameter[] PARM = new SqlParameter[6];
                PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = item.UserId };
                PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = item.DeptCode };
                PARM[2] = new SqlParameter("@ScheCode", SqlDbType.Int) { Value = item.ScheCode };
                PARM[3] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = item.UserPro };
                PARM[4] = new SqlParameter("@ProfileName", SqlDbType.VarChar, 30) { Value = item.ProfileName };
                PARM[5] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = item.BudgetHead };
                result = gf.UpdateData(PARM, "EgInsertUserProfile");
            }
            return result;
        }

        /// <summary>
        /// delete the specific profile
        /// </summary>
        /// <returns></returns>
        public int deleteUserProfile()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            result = gf.UpdateData(PARM, "EgdeleteUserProfile");
            return result;
        }
        /// <summary>
        /// Show User Profile According to UserId
        /// </summary>
        /// <param name="rpt"></param>
        public void FillUserProfileRpt(Repeater rpt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetUserProfile", dt, null);
            rpt.DataSource = dt;
            rpt.DataBind();
            dt.Dispose();
        }
        
        public DataTable GetDeptSchemaNew()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDeptSchemaNew", dt, null);
            return dt;
        }

        public int GetMaxUserPro()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[0];
            return int.Parse(gf.ExecuteScaler(PM, "EgGetMaxUserPro"));
        }

        /// <summary>
        /// Search Department manually
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptList()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGetDepartmentList", dt, null);
            return dt;

        }
       
        
        public int ProfileActiveDeactive(string UserPro,string Activate)            //// Sandeep Profile Actibe 
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"].ToString()) };
            PM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            PM[2] = new SqlParameter("@Type", SqlDbType.Char, 1) { Value = Activate };
            return gf.UpdateData(PM, "EgProfileActiveDeActive");
        }
        
        #endregion
    }
}
