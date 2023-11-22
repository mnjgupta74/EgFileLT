using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
   public class EgGrnSearch:EgCommonPropertyBL
    {
        public string Tcode { get; set; }
        #region function
        public DataTable FillGrid()
        {
            DataTable dt = new DataTable();

            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@BankCode", SqlDbType.Char, 8) { Value = Bankcode };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@Amount", SqlDbType.Decimal) { Value = Amount };
            PM[4] = new SqlParameter("@officeid", SqlDbType.Int);
            PM[4].Value = OfficeId;
            return gf.Filldatatablevalue(PM, "EgGrnSearch", dt, null);
        }
        /// <summary>
        /// fill Treasury Droddownlist
        /// </summary>
        /// <param name="ddl"></param>
        public void FillTreasury(DropDownList ddl)
        {
            GenralFunction gf = new GenralFunction();
            gf.FillListControl(ddl, "EgFillTreasury", "TreasuryName", "TreasuryCode", null);
            ddl.Items.Insert(0, new ListItem("--Select Treasury--", "0"));

        }
        /// <summary>
        /// // fill office Droddownlist
        /// </summary>
        /// <param name="ddl"></param>
        public void FillOfficeList(DropDownList ddl)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = Tcode };
            gf.FillListControl(ddl, "EgFillOfficeList", "officename", "officeid", PARM);
            ddl.Items.Insert(0, new ListItem("--Select Office--", "0"));
        }
        /// <summary>
        /// Binding of DropDown ddlbank
        /// </summary>
        /// <param name="grd"></param>
        public void PopulateBankList(DropDownList ddlbank) // fill department Droddownlist
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddlbank, "EgGetBanks_Reports", "BankName", "BSRCode", null);
            ddlbank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        }
        /// <summary>
        /// fills dropdown list
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl) // fill department Droddownlist
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            gf.FillListControl(ddl, "EgGetDepartmentList", "deptnameEnglish", "deptcode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }
        /// <summary>
        /// Search Department manually
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptList()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(null, "EgGetDepartmentList", dt, null);
            return dt;

        }
        #endregion
    }
}
