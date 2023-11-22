using System.Data.SqlClient;
using System.Data;

namespace EgBL
{

    public class EgIntegrationPostURLBL
    {
        public string MerchantCode { get; set; }
        public string StatusLog { get; set; }
        public string EncData { get; set; }

        public int InsertPushLog()
        {

            int result = 0;
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Merchnatcode", SqlDbType.Int) { Value = MerchantCode };
            PM[1] = new SqlParameter("@StatusLog", SqlDbType.VarChar,20) { Value = StatusLog };
            PM[2] = new SqlParameter("@EncData", SqlDbType.VarChar,500) { Value = EncData };
            result = gf.UpdateData(PM, "EgInsertIntegrationPostURLLog");
            return result;
        }
    }
}
