using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgBL
{
    public class EgOpenYearForBank_BL
    {
        GenralFunction gf = new GenralFunction();
        public Int32 userid { get; set; }
        public string year { get; set; }
        public string bsrcode { get; set; }
        public int duration { get; set; }

        public string UpdateBankStatus()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            PM[1] = new SqlParameter("@year", SqlDbType.Char, 4) { Value = year };
            PM[2] = new SqlParameter("@bsrcode", SqlDbType.Char, 7) { Value = bsrcode };
            PM[3] = new SqlParameter("@duration", SqlDbType.Int) { Value = duration };
            return Convert.ToString(gf.UpdateData(PM, "EgInsertOpenYearForBank"));
            //return gf.ExecuteScaler(PM, "EgOpenYearForBank_sp");
        }
    }
}
