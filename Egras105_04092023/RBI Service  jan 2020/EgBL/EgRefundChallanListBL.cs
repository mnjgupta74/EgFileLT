using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgRefundChallanListBL
    {
        GenralFunction gf;
        #region Properties
        public string TreasuryCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        #endregion
        #region Function
        public DataTable GetRefundChallanData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 2) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            DataTable dt = new DataTable();
            return dt = gf.Filldatatablevalue(PARM, "EgRefundChallanList", dt, null);
           
        }
        #endregion
    }
}
