
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgProfileBL
    {
        #region Properties
        public string LoginId { get; set; }
        public string SelectedUserProfileList { get; set; }
        public string RemovedUserProfileList { get; set; }
        public bool IsActive { get; set; }
        public int LoggedinUserId { get; set; }
        
        #endregion
        GenralFunction gf = new GenralFunction();
        DataTable dt;
        public DataTable GetProfileList()
        {

            gf = new GenralFunction();
            dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar,50) { Value = LoginId };
            dt = gf.Filldatatablevalue(PM, "EgGetProfileListOnOff", dt, null);
            return dt;
        }

        public int UpdateProfileList()
        {

            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50) { Value = LoginId };
            PM[1] = new SqlParameter("@RemovedUserProfileList", SqlDbType.VarChar) { Value = RemovedUserProfileList };
            PM[2] = new SqlParameter("@SelectedUserProfileList", SqlDbType.VarChar) { Value = SelectedUserProfileList };
            

            return gf.UpdateData(PM, "EgUpdateProfileOnOff");
        }
        public DataTable GetUserActiveDeactiveStatus()
        {
            gf = new GenralFunction();
            dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50) { Value = LoginId };
            dt = gf.Filldatatablevalue(PM, "EgGetUserActiveDeActive", dt, null);
            return dt;
        }
        public int UpdateUserActiveDeactiveStatus()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@LoginId", SqlDbType.VarChar, 50) { Value = LoginId };
            PM[1] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive };
            PM[2] = new SqlParameter("@LoggedinUserId", SqlDbType.VarChar) { Value = LoggedinUserId };
            return gf.UpdateData(PM, "EgUpdateUserActiveDeactive");
        }
    }
}
