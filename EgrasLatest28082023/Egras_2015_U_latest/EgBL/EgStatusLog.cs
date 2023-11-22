using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgGetStatusLog
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int UserId { get; set; }
        public string GetStatusLog()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            dt = gf.Filldatatablevalue(PM, "[EgChangeStatusLog]", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public string GetServicesBL()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            dt = gf.Filldatatablevalue(PM, "[EgGetMultipleHeadChallan]", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
