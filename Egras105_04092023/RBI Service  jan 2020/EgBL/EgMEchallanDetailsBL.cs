using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace EgBL
{


    public class EgMEchallanDetailsBL
    {
        GenralFunction gf;
        #region properties
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        #endregion

        #region function
        public DataTable MEChallanDetail()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = fromdate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = todate };
            return gf.Filldatatablevalue(PM, "EggetMEchallanDetails", dt, null);
            
        }
        #endregion

    }
}
