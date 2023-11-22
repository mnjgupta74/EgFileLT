using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgDeletePdAccountBL
    {
        public int PdAcc { get; set; }
        public string TreasuryCode { get; set; }
        public string Flag { get; set; }
        public int DeletePD()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@PdAcc", SqlDbType.Int) { Value = PdAcc };
            PM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[2] = new SqlParameter("@Flag", SqlDbType.Char, 1) { Value = Flag };
            //return gf.UpdateData(PM, "EgDeletePdAccount");
            int result;
            result = Convert.ToInt16(gf.ExecuteScaler(PM, "EgDeletePdAccount"));
            return result;
        }
    }
}
