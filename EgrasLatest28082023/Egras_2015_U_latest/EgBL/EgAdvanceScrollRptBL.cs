using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgAdvanceScrollRptBL
    {
        GenralFunction gf;
        #region Properties
        public string BSRcode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StatusType { get; set; }
        #endregion
        #region Method
        /// <summary>
        /// Get Match Mismatch and Doubted Rsult 
        /// </summary>
        /// <param name="grd">S.No,GRN,CIN,Transdate,E-treasuryAmount,BankAmount,Status</param>
        public DataTable  FillGRNScrollGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BSRcode };
            PM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[3] = new SqlParameter("@StatusType", SqlDbType.Int) { Value = StatusType };
            return  gf.Filldatatablevalue(PM, "EgAdvanceScrollRpt", dt, null);
           
        }
        #endregion
    }
}
