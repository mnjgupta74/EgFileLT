using System;
using System.Data;
using System.Data.SqlClient;


namespace EgBL
{
  public class EGBankDecrypt
    {
      GenralFunction gf = new GenralFunction();

      #region Class Properties
      public DateTime FromDate { get; set; }
      public DateTime ToDate { get; set; }
      public string bankCode { get; set; }
      #endregion


        #region Function

      public DataTable FillEncryptedStringEservice()
      {
          DataTable dt = new DataTable();
          SqlParameter[] PARM = new SqlParameter[3];
          PARM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
          PARM[1] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = ToDate };
          PARM[2] = new SqlParameter("@bankcode", SqlDbType.Char,10) { Value = bankCode };
          return gf.Filldatatablevalue(PARM, "EgBankRespAudit_StringDecryptionEService", dt, null);
      }

      public DataTable FillEncryptedStringBankResponse()
      {
          DataTable dt = new DataTable();
          SqlParameter[] PARM = new SqlParameter[3];
          PARM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
          PARM[1] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = ToDate };
          PARM[2] = new SqlParameter("@bankcode", SqlDbType.Char, 10) { Value = bankCode };
          return gf.Filldatatablevalue(PARM, "EgBankStringDecryptionBankResponse", dt, null);
      }


        #endregion
    }
}
