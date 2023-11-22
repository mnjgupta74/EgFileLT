using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgDMFTGrnList
    {
        GenralFunction gf;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DataTable  FillDMFTRepeter()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
          
            return gf.Filldatatablevalue(PARM, "Report_EgDMFTGrnList",dt,null);
        }
        public DataTable  FillDMFTSummaryRepeter()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "Report_EgGetDMFTGRNCount", dt, null);
           
        }
    }

}
