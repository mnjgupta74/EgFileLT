using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgTreasuryMaster
    {
        GenralFunction gf;
        public string Tcode { get; set; }
        public int DeptCode { get; set; }
        public void FillTreasury(DropDownList ddl)
        {
            gf = new GenralFunction();
            gf.FillListControl(ddl, "EgFillMainTreasury", "TreasuryName", "TreasuryCode", null);
            ddl.Items.Insert(0, new ListItem("--Select Treasury--", "0"));
        }
        public void FillOfficeList(DropDownList ddl)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 50) { Value = Tcode };
            gf.FillListControl(ddl, "EgFillOfficeList", "officename", "officeid", PARM);
            ddl.Items.Insert(0, new ListItem("--Select Office--", "0"));
        }
        public void FillLocation(DropDownList ddl)
        {
            gf = new GenralFunction();
            gf.FillListControl(ddl, "EgFillLocation", "DistrictName", "TreasuryCode", null);
            ddl.Items.Insert(0, new ListItem("--Select Location--", "0"));

        }
    }
}
