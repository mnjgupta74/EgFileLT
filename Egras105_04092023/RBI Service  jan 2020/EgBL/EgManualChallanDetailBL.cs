using System;
using System.Data;
using System.Data.SqlClient;
using DL;

namespace EgBL
{
    public class EgManualChallanDetailBL
    {
        #region Properties
        public string BSRCode { get; set; }
        public DateTime Date { get; set; }
        public string fromtime { get; set; }
        public string toTime { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        #endregion


        #region Function

        public string GetBankManualData()
        {
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@date", SqlDbType.DateTime) { Value = Date };
            PM[1] = new SqlParameter("@bsrCode", SqlDbType.Char, 7) { Value = BSRCode };
            PM[2] = new SqlParameter("@FromTime", SqlDbType.VarChar, 10) { Value = fromtime };
            PM[3] = new SqlParameter("@ToTime", SqlDbType.VarChar, 10) { Value = toTime };
            return Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgGetManualChallanDetail", PM));
        }
        public string GetOnlineChallanData()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@userId", SqlDbType.Int) { Value = UserId };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[2] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = ToDate };
            return Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgGetOnlineChallanDetail", PM));
        }
        #endregion
    }
}
