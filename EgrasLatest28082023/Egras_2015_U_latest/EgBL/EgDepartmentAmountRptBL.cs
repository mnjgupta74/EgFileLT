using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgDepartmentAmountRptBL
    {

        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Ordertype { get; set; }

        #endregion
        #region Function

        public DataTable EgGetDepartmentRevenue()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@OrderBy", SqlDbType.Char, 5) { Value = Ordertype };
            dt = gf.Filldatatablevalue(PARM, "EgGetDepartmentTotalAmount", dt, null);
            return dt;
        }

        #endregion
    }
}
