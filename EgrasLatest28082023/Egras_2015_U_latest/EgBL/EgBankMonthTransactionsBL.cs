using System.Data;
using System.Data.SqlClient;
using System;

namespace EgBL
{
    public class EgBankMonthTransactionsBL
    {
        #region Properties
        public string Year { get; set; }
        public string Type { get; set; }
        public string Month { get; set; }
        public DateTime Fdate { get; set; }
        public DateTime Tdate { get; set; }
        #endregion

        #region Methods

        public DataTable GetBankMonthTransaction()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@year", SqlDbType.Char, 4) { Value = Year };
            PM[1] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = Type };
            PM[2] = new SqlParameter("@month", SqlDbType.Char, 25) { Value = Month };
            //PM[3] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fdate };
            //PM[4] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Tdate };
            return gf.Filldatatablevalue(PM, "EgBankReport1", dt, null);
        }
       public DataTable GetBankWiseTransaction()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@year", SqlDbType.Char, 4) { Value = Year };
            PM[1] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = Type };
            PM[2] = new SqlParameter("@month", SqlDbType.Char, 25) { Value = Month };
            PM[3] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fdate };
            PM[4] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Tdate };
            return gf.Filldatatablevalue(PM, "EgBankReport1", dt, null);
        }
        #endregion
    }
}
