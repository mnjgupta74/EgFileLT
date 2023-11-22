using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgWAMDataServBL
    {
        #region Properties
        public Int64 GRNNumber { get; set; }
        #endregion

        public string GetWAMData()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRNNumber };
            return gf.ExecuteScaler(PARM, "EgGetWAMData");
        }
    }
}
