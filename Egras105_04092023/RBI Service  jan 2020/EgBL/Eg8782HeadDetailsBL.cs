using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class Eg8782HeadDetailsBL
    {
        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Tcode { get; set; }
        #endregion
        #region Function

        public DataTable Get8782Record()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 2) { Value = Tcode };
            DataTable dt = new DataTable();
            return dt = gf.Filldatatablevalue(PARM, "Eg8782HeadDetails", dt, null);
           
        }
        #endregion
    }
}
