using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgDuplicateCINListBL
    {
        public string BankName { get; set; }
        public string PaymentType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    
        GenralFunction gf = new GenralFunction();
        public DataTable  fillrepeater()
        {
           
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BankName", SqlDbType.Char,7) { Value = BankName };
            PARM[1] = new SqlParameter("@Fromdate", SqlDbType.SmallDateTime ) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[3] = new SqlParameter("@PaymentType", SqlDbType.Char,1) { Value = PaymentType };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgDuplicateCINList", dt, null);
            return dt;
           
        }
    }
}
