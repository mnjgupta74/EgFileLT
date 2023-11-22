using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
    

{
    public class EgAddDeptBudgetHead
    {
        GenralFunction gf = new GenralFunction();
        #region Class Properties
        public int DeptCode { get; set; }

        public string MajorHead { get; set; }
        public string SubMajorHead { get; set; }
        public string MinorHead { get; set; }
        public string SubMinorHead { get; set; }
        public string GroupSubHead { get; set; }
        public string BudgetHeadName { get; set; }
        #endregion
        #region Class Method

        public DataTable FillMapBudgetHeadRpt()
        {
            gf = new GenralFunction();
            PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgMapBudgetHeadList", dt, null);
           return dt;
        }
        public int InsertMapBudgetHead()
        {
            int result = 0;
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = MajorHead };
            //PARM[1] = new SqlParameter("@SubMajorHead", SqlDbType.Char, 2) { Value = SubMajorHead };
            //PARM[2] = new SqlParameter("@MinorHead", SqlDbType.Char, 3) { Value = MinorHead };
            //PARM[3] = new SqlParameter("@SubMinorHead", SqlDbType.Char, 2) { Value = SubMinorHead };
            //PARM[4] = new SqlParameter("@GroupSubHead", SqlDbType.Char, 2) { Value = GroupSubHead };
            PARM[1] = new SqlParameter("@BudgetHeadName", SqlDbType.NVarChar, 250) { Value = BudgetHeadName };
            result = gf.UpdateData(PARM, "EgInsertMapBudgetHead");
            return result;
        }
      

        #endregion
    }
}
