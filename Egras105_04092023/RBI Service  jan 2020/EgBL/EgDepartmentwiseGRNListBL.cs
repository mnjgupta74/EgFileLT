using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
   public class EgDepartmentwiseGRNListBL
    {
        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Dept { get; set; }
        #endregion
        #region Function
        public DataTable GetDepartmentGRNRecord()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = Dept };
            DataTable dt = new DataTable();
            return dt = gf.Filldatatablevalue(PARM, "EgDepartementWiseGRNList", dt, null);
            //gf.FillGridViewControl(grd, PARM, "Eg8782HeadDetails");
        }
        #endregion
    }
}
