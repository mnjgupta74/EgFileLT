using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgDepartmentMaster
    {
        GenralFunction gf;
        public int UserId { get; set; }
        public void PopulateDepartmentList(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.FillListControl(ddl, "EgGetSelectedDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }
        // Add Method 17/12 2020 for Convert Report Html form
        public string GetDepartmentByUserId()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            //gf.FillListControl(ddl, "EgGetSelectedDepartmentList", "deptnameEnglish", "deptcode", PARM);
            //ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetSelectedDepartmentList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

    }
}
