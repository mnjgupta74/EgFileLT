using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgPDAccountDmftBL
    {
        #region Properties
        public string LoginId { get; set; }
        public int UserId { get; set; }
        public string TresCode { get; set; }
        public string BudgetHead { get; set; }
        public string RemovedPdAccList { get; set; }
        public string SelectedPdAccList { get; set; }
        #endregion
        GenralFunction gf = new GenralFunction();
        DataTable dt;
        public DataTable GetPdAccountForDMFT()
        {
            gf = new GenralFunction();
            dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@TresCode", SqlDbType.VarChar) { Value = TresCode };
            PM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char,13) { Value = BudgetHead };
            dt = gf.Filldatatablevalue(PM, "EgPdAccForDMFTOnOFF", dt, null);
            return dt;
        }
        public int UpdatePdAccountForDMFT()
        {

            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@TresCode", SqlDbType.VarChar) { Value = TresCode };

            PM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PM[2] = new SqlParameter("@RemovePdAccountList", SqlDbType.VarChar) { Value = RemovedPdAccList };
            PM[3] = new SqlParameter("@SelectedPdAccountList", SqlDbType.VarChar) { Value = SelectedPdAccList };
            PM[4] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            return gf.UpdateData(PM, "EgUpdatePdAccForDMFTOnOFF");
        }
    }
}
