using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class OfficeActiveDeactiveBL
    {
        GenralFunction gf;
        public string Tcode { get; set; }
        public int DeptCode { get; set; }
        public int UserId { get; set; }
        public int officeId { get; set; }
        public string SelectedUserProfileList { get; set; }
        public string RemovedUserProfileList { get; set; }
        DataTable dt;
        public DataTable FillOfficeList()
        {
            dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = Tcode };
            return gf.Filldatatablevalue(PARM, "EgFillOfficeList", dt,null);
        }
        public void FillOfficeListRepeater(Repeater rpt)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgFillOfficeListForActiveDeactive", dt, null);
            rpt.DataSource = dt;
            rpt.DataBind();
            dt.Dispose();
        }
        public int UpdateOfficeActiveDeactive()
        {
            //dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@TresCode", SqlDbType.VarChar,50) { Value = Tcode };
            PARM[2] = new SqlParameter("@RemovedofficeList", SqlDbType.VarChar) { Value = RemovedUserProfileList };
            PARM[3] = new SqlParameter("@SelectedofficeList", SqlDbType.VarChar) { Value = SelectedUserProfileList };
            return gf.UpdateData(PARM, "EgUpdateOfficeActiveDeactive");
        }
        public int DivisionActiveDeactive(string DivCode, string Activate)            //// Sandeep Profile Actibe 
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]) };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = Tcode };
            PARM[2] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = officeId };
            PARM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[4] = new SqlParameter("@Type", SqlDbType.Char, 1) { Value = Activate };
            return gf.UpdateData(PARM, "EgUpdateOfficeActiveDeactive");
        }


    }
}
