using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgMobileChallanBL
    {
        GenralFunction gf = new GenralFunction();

        /// Eg Deface And Refund Detail  Properties
        /// </summary>

        //public Int64 Grn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //public Int16 Type { get; set; }


        // Get Deface And Refund  Detail Date WiSE or gRN wISE
        public string GetMobileChallanData()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            //PARM[0] = new SqlParameter("@Type", SqlDbType.Int) { Value = Type };
            //PARM[1] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };

            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgMobileChallanRecords", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
