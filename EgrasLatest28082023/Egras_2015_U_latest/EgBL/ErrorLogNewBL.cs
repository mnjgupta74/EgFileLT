using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class ErrorLogNewBL
    {
        GenralFunction gf = new GenralFunction();
        public DateTime Fromdate { get; set; }
        public DateTime Todate   { get; set; }


        public DataTable ErrorReport()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime)   { Value =  Todate  };
            return gf.Filldatatablevalue(PARM, "ErrorLog_App", dt, null);
        }
    }
}
