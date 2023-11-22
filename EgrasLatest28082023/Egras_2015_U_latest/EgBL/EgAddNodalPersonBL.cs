using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgAddNodalPersonBL
    {
        GenralFunction gf;
        //EgInsertNodalOfficer
        #region Class Properties
        public int Nid { get; set; }
        public int DeptCode { get; set; }
        public string NodalName { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string EmailID { get; set; }
        #endregion

        #region Class Functions
        /// <summary>
        /// Insert Nodal Officer Details 
        /// </summary>
        /// <returns></returns>
        public int InsertNodalData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@NodalName", SqlDbType.VarChar, 50) { Value = NodalName };
            PARM[2] = new SqlParameter("@Number", SqlDbType.Char, 25) { Value = Number };
            PARM[3] = new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = Address };
            PARM[4] = new SqlParameter("@EmailID", SqlDbType.VarChar, 50) { Value = EmailID };
            return gf.UpdateData(PARM, "EgInsertNodalOfficer");
        }
        /// <summary>
        /// Show nodal officer detail Department wise
        /// </summary>
        /// <param name="grd"></param>
        public DataTable  NodalGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            return gf.Filldatatablevalue(PARM, "EgGetNodalDetails",dt,null);

           
        }
        /// <summary>
        /// Update Nodal person details 
        /// </summary>
        /// <returns></returns>
        public int UpdateNodalData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[6];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@NodalName", SqlDbType.VarChar, 50) { Value = NodalName };
            PARM[2] = new SqlParameter("@Number", SqlDbType.Char, 25) { Value = Number };
            PARM[3] = new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = Address };
            PARM[4] = new SqlParameter("@Nid", SqlDbType.Int) { Value = Nid };
            PARM[5] = new SqlParameter("@EmailID", SqlDbType.VarChar, 50) { Value = EmailID };
            return gf.UpdateData(PARM, "EgUpdateNodalOfficer");
        }
        /// <summary>
        /// Delete nodal officer data
        /// </summary>
        /// <returns></returns>
        public int DeleteNodalData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Nid", SqlDbType.Int) { Value = Nid };
            return gf.UpdateData(PARM, "EgDeleteNodalOfficer");
        }

        public void GetNodalOfficerList(DataList dList)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PagedDataSource objpds = new PagedDataSource();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDepartmetnNodalOfficer", dt, null);
            DataView objdv = new DataView(dt);
            objpds.DataSource = objdv;
            dList.DataSource = objpds;
            dList.DataBind();
        }
        /// <summary>
        /// fills DepartmentList  name wise
        /// </summary>
        /// <param name="ddl"></param>
        public void DepartmentListNamewise(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            gf.FillListControl(ddl, "EgDepartmentListNamewise", "deptnameEnglish", "deptcode", null);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }
        #endregion
    }
}
