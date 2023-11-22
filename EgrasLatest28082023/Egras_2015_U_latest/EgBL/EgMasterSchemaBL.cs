using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DL;
using EgBL;

namespace EgBL
{
    public class EgMasterSchemaBL
    {
        GenralFunction gf;

        public int DeptCode { get; set; }
        public string BudgetHead { get; set; }
        public string SchemaName { get; set; }
        //public string SchemaCode{get; set; }
        public int ScheCode { get; set; }

        #region Function
        /// <summary>
        /// fills the department dropdown list
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];

            gf.FillListControl(ddl, "EgGetDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        /// <summary>
        /// insert the records of schema in master table
        /// </summary>
        /// <returns></returns>
        public int insertbudgetschema()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@SchemaName", SqlDbType.NVarChar, 300) { Value = SchemaName };
            PARM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            int result = gf.UpdateData(PARM, "EgInsertBudgetSchema");
            return result;
        }


        public int Updatebudgetschema()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("SchemaName", SqlDbType.NVarChar, 300) { Value = SchemaName };
            PARM[2] = new SqlParameter("ScheCode", SqlDbType.Int) { Value = ScheCode };
            int result = gf.UpdateData(PARM, "EgUpdateBudgetSchema");
            return result;
        }

        /// <summary>
        /// fills the schema grid according to budgethead
        /// </summary>
        /// <param name="grd"></param>
        public DataTable  fillbudgetschemagrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            return gf.Filldatatablevalue(PARM, "EgBubgetHeadSchema",dt,null);
         
        }

        #endregion
    }
}
