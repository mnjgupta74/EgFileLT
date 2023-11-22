using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgGetDataBL
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Int64 MerchantCode { get; set; }
        public Int32 OfficeId { get; set; }

        GenralFunction gf = new GenralFunction();

        public DataTable GetData()
        {
            SqlParameter[] PARM = new SqlParameter[4];
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            PARM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@officeId", SqlDbType.Int) { Value = OfficeId };
            PARM[3] = new SqlParameter("@merchantCode", SqlDbType.Int) { Value = MerchantCode };
            gf.Filldatatablevalue(PARM, "EgGetData", dt, null);
            return dt;
        }
    }
}
