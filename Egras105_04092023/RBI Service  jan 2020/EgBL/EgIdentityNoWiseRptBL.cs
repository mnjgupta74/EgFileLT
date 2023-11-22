using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgIdentityNoWiseRptBL
    {
        GenralFunction gf;
        #region Class Properties

        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public int deptCode { get; set; }
        public string Identity { get; set; }
        #endregion

        #region Class Method
        public DataTable  BindIdentityGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[6];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[3] = new SqlParameter("@Identity", SqlDbType.VarChar, 50) { Value = Identity };
            PM[4] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = deptCode };
            PM[5] = new SqlParameter("@UserType", SqlDbType.TinyInt) { Value = UserType };
            return gf.Filldatatablevalue(PM, "EgIdentityNoWiseRecored", dt,null);
           
        }
       
        #endregion
    }
}
