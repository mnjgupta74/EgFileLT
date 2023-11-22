using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgManualSuccessChallanBL
    {
        public string BankName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Userid { get; set; }
        public int flag { get; set; }

        GenralFunction gf = new GenralFunction();
        public void fillrepeater(Repeater rpt)
        {

            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BankCode", SqlDbType.Char) { Value = BankName };
            PARM[1] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            //gf.FillRepeaterControl(rt,PARM,"EgFailTransactions");
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetManualSuccessChallan", dt, null);
            if (dt.Rows.Count > 0)
            {
                flag = 1;
                rpt.DataSource = dt;
                rpt.DataBind();
                dt.Dispose();
            }
            else
            {
                flag = 0;
            }
        }
    }
}
