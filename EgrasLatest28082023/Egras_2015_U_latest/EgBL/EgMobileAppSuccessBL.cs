using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgMobileAppSuccessBL
    {
        GenralFunction gf;
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public int totalcount { get; set; }
        public double TotalAmount { get; set; }

        public DataTable EgAppmobile()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.Date) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.Date) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "EgAppmobile", dt, null);
        }
    }
}