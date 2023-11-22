using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgDoubtedGrnBL
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserId { get; set; }


        public string getDoubtedGrnList()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            dt = gf.Filldatatablevalue(PM, "EgDoubtedGrnList", dt, null);
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(dt);
            return jsonString;
        }

    }
}
