using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgUserProfileBL_ME
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
        public string ObjectHead { get; set; }
        public string PlanNonPlan { get; set; }
        public string VotedCharged { get; set; }
        public string RblTransactionSelectedValue { get; set; }
        public string MajorHeadCode { get; set; }
        #endregion
        public int InsertUserProfileForME()
        {
                int result = 0;
                gf = new GenralFunction();
                SqlParameter[] PARM = new SqlParameter[9];
                PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
                PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value =DeptCode };
                PARM[2] = new SqlParameter("@ScheCode", SqlDbType.Int) { Value = ScheCode };
                PARM[3] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
                PARM[4] = new SqlParameter("@ProfileName", SqlDbType.VarChar, 30) { Value = ProfileName };
                PARM[5] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value =BudgetHead };
                PARM[6] = new SqlParameter("@ObjectHead", SqlDbType.Char,2) { Value = ObjectHead };
                PARM[7] = new SqlParameter("@PlanNonPlan", SqlDbType.Char, 1) { Value = PlanNonPlan };
                PARM[8] = new SqlParameter("@VotedCharged", SqlDbType.Char, 1) { Value = VotedCharged };
                result = gf.UpdateData(PARM, "EgInsertUserProfileME");
                return result;
        }
        public int GetMaxUserProME()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[0];
            return int.Parse(gf.ExecuteScaler(PM, "EgGetMaxUserProME"));
        }
        public virtual void GetProfileListME(DropDownList ddl)
        {

            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.FillListControl(ddl, "EgGetProfileList_ME", "UserProfile", "UserPro", PM);
            ddl.Items.Insert(0, new ListItem("--Select Profile--", "0"));
        }

        public DataTable BinTransactionPaymentME()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = RblTransactionSelectedValue == "" ? null : RblTransactionSelectedValue };
            dt = gf.Filldatatablevalue(PM, "EgGetTransactionListME", dt, null);
            return dt;
        }
        public DataTable GetProfileWiseTransactionME()
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

        public void FillUserProfileRptME(Repeater rpt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            //PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetUserProfileME", dt, null);
            //DataView objdv = new DataView(dt);
            //objpds.DataSource = objdv;
            rpt.DataSource = dt;
            rpt.DataBind();
            dt.Dispose();
        }
        public DataTable GetDeptSchemaNewME()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDeptSchemaNewME", dt, null);
            return dt;
        }
        public DataTable fillDeptWiseMajorHeadListME()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PM, "EgDeptwiseMajorHeadListME", dt, null);
        }
        public int deleteUserProfileME()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            result = gf.UpdateData(PARM, "EgdeleteUserProfileME");
            return result;
        }
        public int ProfileActiveDeactive_ME(string UserPro, string Activate)            //// Sandeep Profile Actibe 
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"].ToString()) };
            PM[1] = new SqlParameter("@UserPro", SqlDbType.Int) { Value = UserPro };
            PM[2] = new SqlParameter("@Type", SqlDbType.Char, 1) { Value = Activate };
            return gf.UpdateData(PM, "EgProfileActiveDeActive_ME");
        }
        
    }
}