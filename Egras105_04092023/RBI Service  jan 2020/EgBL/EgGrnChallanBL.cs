using System;
using System.Data;
using System.Data.SqlClient;
using DL;

namespace EgBL
{
   public class EgGrnChallanBL
    {
       GenralFunction gf;

       #region Properties
       public string TreasuryCode { get; set; }
       public long GRN { get; set; }
       #endregion

       #region Function

       public string GetGrnChallanData()
       {
           gf = new GenralFunction();
           SqlParameter[] PM = new SqlParameter[2];
           PM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = GRN };
           PM[1] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
           string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgGrnChallanService", PM));
           return result;
       }
     
       #endregion
    }
}
