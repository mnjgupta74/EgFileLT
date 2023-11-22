using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgDeptServiceBL
    {
        GenralFunction gf;
        #region Class Properties
        public string ScheCode { get; set; }
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public int UserId { get; set; }
        public string Flag { get; set; }
        public bool isActiveFlag { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        #endregion
        public int InsertDeptService()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            //PARM[2] = new SqlParameter("@ScheCode", SqlDbType.VarChar, 130) { Value = ScheCode };
            PARM[2] = new SqlParameter("@ServiceName", SqlDbType.VarChar, 30) { Value = ServiceName };
            PARM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char, 180) { Value = BudgetHead };
            result = gf.UpdateData(PARM, "EgInsertDeptService");
            return result;
        }
        public int EditDeptService()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            //PARM[2] = new SqlParameter("@ScheCode", SqlDbType.VarChar, 130) { Value = ScheCode };
            PARM[2] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = ServiceId };
            PARM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char, 180) { Value = BudgetHead };
            result = gf.UpdateData(PARM, "EgEditDeptService");
            return result;
        }
        public int Active_DeactiveDeptService()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@ServiceId", SqlDbType.VarChar, 30) { Value = ServiceId };
            PARM[2] = new SqlParameter("@Flag", SqlDbType.Bit) { Value = isActiveFlag };
            result = gf.UpdateData(PARM, "EgActvDactvDeptService");
            return result;
        }
        public string GetServiceNameList1()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserType"] };
            gf.Filldatatablevalue(PARM, "EgGetServiceNameList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public void GetServiceNameList(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserType"] };
            gf.Filldatatablevalue(PARM, "EgGetServiceNameList", dt, null);

            ddl.DataSource = dt;
            ddl.DataTextField = "ServiceName";
            ddl.DataValueField = "ServiceId";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select Service--", "0"));
        }
        public DataTable GetServiceNameDt()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserType"] };
            gf.Filldatatablevalue(PARM, "EgGetServiceNameList", dt, null);
            return dt;
        }
        public string GetServiceHeadsList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@ServiceId", SqlDbType.Int) { Value = ServiceId };
            PARM[2] = new SqlParameter("@Flag", SqlDbType.Char, 1) { Value = "Y" };
            gf.Filldatatablevalue(PARM, "EgGetServiceHeadsList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
