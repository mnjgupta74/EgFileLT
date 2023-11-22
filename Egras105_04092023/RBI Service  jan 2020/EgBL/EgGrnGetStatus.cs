using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
    public class EgGrnGetStatusBL
    {
        public Int64 GRN { get; set; }
        //public string GetGRNStatus()
        //{
        //    GenralFunction gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[1];
        //    PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
        //    string Amount = gf.ExecuteScaler(PARM, "EgGetGrnStatus");
        //    return Amount;
        //}

        public DataTable GetGRNStatus()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            DataTable dt = new DataTable();
            //DataSet DS = GF.Filldatasetvalue(PM, "EgGetDailySingedPdf", ds, null);
            return gf.Filldatatablevalue(PARM, "EgGetGrnStatus", dt, null);
        }
    }

}
