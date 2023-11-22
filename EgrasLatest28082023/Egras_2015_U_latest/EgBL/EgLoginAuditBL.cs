using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
{
    public class EgLoginAuditBL
    {
        GenralFunction gf = new GenralFunction();

        #region Variables

        private Int32 _StartIdx, _EndIdx, _refpageindex;
        #endregion

        #region Class Properties
        public string Username { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StartIdx
        {
            get { return _StartIdx; }
            set { _StartIdx = value; }
        }
        public int EndIdx
        {
            get { return _EndIdx; }
            set { _EndIdx = value; }
        }
        #endregion

      

        public string TotalRecord()
        {
            string Result = "";
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            SqlDataReader dr = gf.FillDataReader(PARM, "EgLoginauditTotal");
            if (dr.HasRows)
            {
                dr.Read();
                Result = dr[0].ToString();
                
            }
            dr.Close();
            dr.Dispose();
            return Result;
        }

        public DataTable tablegrid()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@StartIdx", SqlDbType.Int) { Value = StartIdx };
            PARM[3] = new SqlParameter("@EndIdx", SqlDbType.Int) { Value = EndIdx };
            return gf.Filldatatablevalue(PARM, "EgLoginAudit", dt, null);
        }



        #region Method use in  LoginHistory page
        public DataTable FillrptLoginDetail()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[0];
            return gf.Filldatatablevalue(PARM, "EgLoginHistory", dt, null);

        }
        #endregion
        #region  Method Use for Error Report
        public DataTable ErrorReport()
        {

            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "EgErrorInfo", dt, null);
        }

        public DataTable BankServiceAudit()
        {

            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "EgServiceAudit", dt, null);
        }

        public DataTable BankResponseAudit()
        {

            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "EgResponseAudit", dt, null);
        }

        public DataTable EgrasServiceAudit()
        {

            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = ToDate };
            return gf.Filldatatablevalue(PARM, "EgEgrasServiceAudit", dt, null);
        }
        #endregion
    }
   
}
