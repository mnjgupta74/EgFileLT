using System;

using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgDefaceAndRefundDetailBL
    {
        GenralFunction gf = new GenralFunction();
     
        /// Eg Deface And Refund Detail  Properties
        /// </summary>
       
        public Int64 Grn { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public  Int16  Type { get; set; }


        // Get Deface And Refund  Detail Date WiSE or gRN wISE
        public string GetDefaceAndReundDetail()
        {
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Type", SqlDbType.Int) { Value = Type };
            PARM[1] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[2] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PARM[3] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
          
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgDefaceAndReleaseDetail", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
