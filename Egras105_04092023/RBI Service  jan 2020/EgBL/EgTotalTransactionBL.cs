using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgTotalTransactionBL : EgCommonPropertyBL
    {
        public void GetTotalTransaction(DataList datalist)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgGetTotalTransactions", dt, null);
            datalist.DataSource = dt;
            datalist.DataBind();
            dt.Dispose();
            //gf.FillRepeaterControl(rpt, PM, "EgGetTotalTransactions");
        }

    }
}
