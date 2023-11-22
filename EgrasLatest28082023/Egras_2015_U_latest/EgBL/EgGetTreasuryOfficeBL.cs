using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
    public class EgGetTreasuryOfficeBL
    {
        public Int64 OfficeID { get; set; }
        public DataTable GetTreasuryOfficeMapList()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@OfficeID", SqlDbType.BigInt) { Value = OfficeID };
            DataTable dt = new DataTable();
            //DataSet DS = GF.Filldatasetvalue(PM, "EgGetDailySingedPdf", ds, null);
            return gf.Filldatatablevalue(PARM, "EgCheckOfficeMapWithTreasury", dt, null);
        }
    }
}
