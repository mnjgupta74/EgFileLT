using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgAddBankPersonBL
    {
        GenralFunction gf;
        //EgInsertNodalOfficer
        #region Class Properties
        public int Bankid { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
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
        public int InsertBankData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.Char,7) { Value = BankCode };
            PARM[1] = new SqlParameter("@BankName", SqlDbType.VarChar, 50) { Value = BankName };
            PARM[2] = new SqlParameter("@Number", SqlDbType.Char, 25) { Value = Number };
            PARM[3] = new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = Address };
            PARM[4] = new SqlParameter("@EmailID", SqlDbType.VarChar, 50) { Value = EmailID };
            return gf.UpdateData(PARM, "EgInsertBankOfficer");
        }
        /// <summary>
        /// Show nodal officer detail Department wise
        /// </summary>
        /// <param name="grd"></param>
        public DataTable  BankGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.Char,7) { Value = BankCode };
            return gf.Filldatatablevalue(PARM, "EgGetBankDetails", dt,null);

           
        }
        /// <summary>
        /// Update Nodal person details 
        /// </summary>
        /// <returns></returns>
        public int UpdateBankData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[6];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.Char,7) { Value = BankCode };
            PARM[1] = new SqlParameter("@BankName", SqlDbType.VarChar, 50) { Value = BankName };
            PARM[2] = new SqlParameter("@Number", SqlDbType.Char, 25) { Value = Number };
            PARM[3] = new SqlParameter("@Address", SqlDbType.VarChar, 200) { Value = Address };
            PARM[4] = new SqlParameter("@Bankid", SqlDbType.Int) { Value = Bankid };
            PARM[5] = new SqlParameter("@EmailID", SqlDbType.VarChar, 50) { Value = EmailID };
            return gf.UpdateData(PARM, "EgUpdateBankOfficer");
        }
        /// <summary>
        /// Delete nodal officer data
        /// </summary>
        /// <returns></returns>
        public int DeleteBankData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Bankid", SqlDbType.Int) { Value = Bankid };
            return gf.UpdateData(PARM, "EgDeleteBankOfficer");
        }

        //public void GetBankOfficerList(DataList dList)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[2];
        //    PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
        //    PagedDataSource objpds = new PagedDataSource();
        //    DataTable dt = new DataTable();
        //    dt = gf.Filldatatablevalue(PARM, "EgGetDepartmetnNodalOfficer", dt, null);
        //    DataView objdv = new DataView(dt);
        //    objpds.DataSource = objdv;
        //    dList.DataSource = objpds;
        //    dList.DataBind();
        //}
        /// <summary>
        /// fills DepartmentList  name wise
        /// </summary>
        /// <param name="ddl"></param>
        public void BankListNamewise(DropDownList ddl) // fill department Droddownlist
        {
            gf = new GenralFunction();
            gf.FillListControl(ddl, "EgChallanBankList", "BankName", "BSRCode", null);
            ddl.Items.Insert(0, new ListItem("--Select Bank List--", "0"));
        }
        #endregion
    }
}
