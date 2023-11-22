using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgSearchByIdentity
    {
        GenralFunction gf;

        public string Identity { get; set; }
        public string DeptCode { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public int UserType { get; set; }
        /// <summary>
        /// fill department Droddownlist
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.FillListControl(ddl, "EgGetSelectedDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }

        /// <summary>
        /// Get Department Revenue for all  Treasury
        /// </summary>
        /// <param name="rpt"></param>
        public void SearchByIdentity(DataTable dt)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[6];
            PM[0] = new SqlParameter("@identity", SqlDbType.NVarChar, 50) { Value = Identity };
            PM[1] = new SqlParameter("@deptCode", SqlDbType.NVarChar, 10) { Value = DeptCode };
            PM[2] = new SqlParameter("@userId", SqlDbType.BigInt) { Value = UserId };
            PM[3] = new SqlParameter("@userType", SqlDbType.SmallInt) { Value = UserType };
            PM[4] = new SqlParameter("@fromdate", SqlDbType.Date) { Value = FromDate };
            PM[5] = new SqlParameter("@todate", SqlDbType.Date) { Value = Todate };

            gf.Filldatatablevalue(PM, "EgSearchByIdentity", dt, null);

            //gf.FillRepeaterControl(rpt, PM, "EgSearchByIdentity");
        }
    }
}
