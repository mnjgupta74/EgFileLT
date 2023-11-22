using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgBankServiceStatus
    {

        GenralFunction gf = new GenralFunction();
        public string BSRCode { get; set; }
        public bool status { get; set; }
        #region Function

        public int InsertBankServiceStatus()
        {
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@BSRCode", SqlDbType.NVarChar, 7) { Value = BSRCode };
            PARM[1] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = status == true ? "A" : "D" };
            int result = gf.UpdateData(PARM, "InsertBankServiceUpDownStatus");
            return result;
        }
       
        #endregion
    }
}
