using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgUnBlockOfficeForDivBL
    {
        public string TreasuryCode { get; set; }
        public string OfficeCodeList { get; set; }
        GenralFunction gf;
        public DataTable GetOfficenameList()
        {
            gf = new EgBL.GenralFunction();
            DataTable table = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@TreacuryCode", System.Data.SqlDbType.Char, 4) { Value = TreasuryCode };
            table = gf.Filldatatablevalue(PM, "EgGetUnBlockOfficeListtreasuryWise", table, null);
            return table;
        }
        public void UnBlockOffice()
        {
            gf = new EgBL.GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@OfficeList", System.Data.SqlDbType.VarChar, 100) { Value = OfficeCodeList };
            gf.UpdateData(PM, "EgUnBlockOffice");
        }
    }
}
