using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgAddBudgetHead
    {
        public string BudgetHead { get; set; }
        public int InsertBudgetHead()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            int val = gf.UpdateData(PARM, "link_server_budget");
            return val;
        }
    }
}
