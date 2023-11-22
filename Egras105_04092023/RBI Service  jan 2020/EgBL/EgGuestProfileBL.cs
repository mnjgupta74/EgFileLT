using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgGuestProfileBL
    {
        GenralFunction gf;
       

        public int DeptCode { get; set; }

        /// <summary>
        /// fills dropdown list
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];

            gf.FillListControl(ddl, "EgGetDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }

        /// <summary>
        /// gets all the schema according to deptwise
        /// </summary>
        /// <returns></returns>
        public DataTable GetGuestSchema()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];

            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@type", SqlDbType.Int) { Value = 1 };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PARM, "EgGuestDeptSchema", dt, null);
        }
    }
}
