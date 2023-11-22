using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace EgBL
{
    public class EgGRNScrollRpt
    {
        GenralFunction gf;
        #region Properties
        public string BSRcode { get; set; }
        public DateTime Date { get; set; }
        public DateTime ToDate { get; set; }
        public int StatusType { get; set; }
        public string PaymentType { get; set; }
        #endregion
        #region Method
        /// <summary>
        /// Get Match Mismatch and Doubted Rsult 
        /// </summary>
        /// <param name="grd">S.No,GRN,CIN,Transdate,E-treasuryAmount,BankAmount,Status</param>
        public DataTable FillGRNScrollGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BSRcode };
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PM[2] = new SqlParameter("@StatusType", SqlDbType.Int) { Value = StatusType };
            PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
            return gf.Filldatatablevalue(PM, "EgGRNScrollRpt", dt, null);
          
        }
        #endregion
    }
}
