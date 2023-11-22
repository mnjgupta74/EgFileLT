using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgEditTreasuryRpt
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int UserId { get; set; }
        public string GetEditTreasuryData()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            PM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            dt = gf.Filldatatablevalue(PM, "[EgGetEditTreasuryLogs]", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
