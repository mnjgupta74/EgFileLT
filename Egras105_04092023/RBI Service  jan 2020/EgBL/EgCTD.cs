using System;
using System.Data;
using System.Data.SqlClient;
using DL; 

namespace EgBL
{
    public class EgCTD
    {
        #region "Properties"
        
        public int _Userid;
        public DateTime _FromDate,_Todate;
        public string _TreasuryCode;
        public long _GRN;

        public int Userid
        {
            get { return _Userid; }
            set { _Userid = value; }
        }

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime Todate
        {
            get { return _Todate; }
            set { _Todate = value; }
        }

        public string TreasuryCode
        {
            get { return _TreasuryCode; }
            set { _TreasuryCode = value; }
        }

        public long GRN
        {
            get { return _GRN; }
            set { _GRN = value; }
        }
        public int DeptCode { get; set; }
        #endregion
        #region "Function"
        
        public string GetCTDData()
        {
            DataSet ds = new DataSet();

            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@To", SqlDbType.SmallDateTime) { Value = Todate };
            PM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = Userid  };
            PM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            string result = SqlHelper.ExecuteScalar(SqlHelper.conString, "GetCTDData", PM).ToString();
            return result;

        }

        public string GetGrnVoucherData()
        {
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = GRN };
            PM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = TreasuryCode };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgGrnVoucherService", PM));
            return result;
        }
     
        #endregion
    }
}

          
