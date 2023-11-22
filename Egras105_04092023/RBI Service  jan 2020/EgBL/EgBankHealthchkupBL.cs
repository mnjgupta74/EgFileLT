using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgBankHealthchkupBL
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BsrCode { get; set; }






        public DataTable GetEgErrorData()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char) { Value = BsrCode };
            PM[1] = new SqlParameter("@Fromdate", SqlDbType.Date) { Value = FromDate };
            PM[2] = new SqlParameter("@Todate", SqlDbType.Date) { Value = ToDate };


            dt = gf.Filldatatablevalue(PM, "EgGetBankErrorLogs", dt, null);
            return dt;
        }

        public DataTable GetEgBankData()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[0];
            dt = gf.Filldatatablevalue(PM, "EgGetBanks", dt, null);
            return dt;
        }

    }
}
