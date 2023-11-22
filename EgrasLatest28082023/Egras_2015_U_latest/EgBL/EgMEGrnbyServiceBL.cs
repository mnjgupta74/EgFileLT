using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
namespace EgBL
{
    public class EgMEGrnbyServiceBL
    {
        GenralFunction gf;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string BudgetHead { get; set; }
        public string RemitterName { get; set; }
        public DateTime TransDate { get; set; }
        public double Amount { get; set; }
        public object sumofobject { get; set; }
        public object rptSchemaDi { get; set; }
        public int flag { get; set; }
        public void fillrepeater(Repeater rpt)
        {
            DataTable tblReport = new DataTable();
            gf = new GenralFunction(); 
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgMEGRNByServiceRpt", dt, null);
            sumofobject = dt.Compute("Sum(Amount)", "");
            sumofobject = string.Format("{0:0.00}", sumofobject);
            if (dt.Rows.Count > 0)
            {

                rptSchemaDi = true;
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
