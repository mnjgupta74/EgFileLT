using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgFailTransactionsBL
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
            PARM[0] = new SqlParameter("@BankName", SqlDbType.Char) { Value = BankName };
            PARM[1] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            //gf.FillRepeaterControl(rt,PARM,"EgFailTransactions");
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgFailTransactions", dt, null);
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
        public String getIdentity()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@Userid", SqlDbType.Int) { Value = Userid };
            String bsrcode = gf.ExecuteScaler(PARM, "EgGetBSRCode");
            return bsrcode;
        }

        /// <summary>
        /// Bind Fali Transaction on Bank Selection for TO or Bank Login with JSON
        /// </summary>
        /// <param name="rpt"></param>
        public string GetFailTransaction()
        {

            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BankName", SqlDbType.Char) { Value = BankName };
            PARM[1] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgFailTransactions", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
