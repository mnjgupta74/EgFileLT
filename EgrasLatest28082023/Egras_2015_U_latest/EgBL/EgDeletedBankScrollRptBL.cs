using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgDeletedBankScrollRptBL
    {
        GenralFunction gf = new GenralFunction();

        #region Class Properties
        public Int64 Grn { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime BankDate { get; set; }
        public DateTime TransDate { get; set; }
        public int BSRCode { get; set; }
        

        public int flag { get; set; }
        #endregion

        #region Class Functions
        public void fillRptrDate(Repeater rpt)
        {
            DataTable tblReport = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@BSRCode", SqlDbType.Int) { Value = BSRCode };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgDeletedScrollRpt_Summary", dt, null);
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

        public void fillRptrData(GridView rpt)
        {
            DataTable tblReport = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@BankDate", SqlDbType.DateTime) { Value = BankDate };
            PARM[1] = new SqlParameter("@TransDate", SqlDbType.DateTime) { Value = TransDate };
            PARM[2] = new SqlParameter("@BSRCode", SqlDbType.Int) { Value = BSRCode };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgDeletedScrollRpt_Detail", dt, null);
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
        #endregion
    }
}
