using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgOfficeHeadAmountRptBL
    {
        GenralFunction gf;
        #region properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserID { get; set; }
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public string Treasurycode { get; set; }
         
        #endregion


        #region functions
        public DataTable  OfficeTotalAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
            //gf.FillGridViewControl(grdBudgetHead, PM, "egOfficeWiseRevenueRpt");
            return gf.Filldatatablevalue(PM, "egOfficeWiseRevenueRpt", dt,null);
        }

        public DataTable  shqpSchemaRpt()
        {
            // Changes to be made in this method only 
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
           
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@Userid", SqlDbType.Int) { Value = UserID};
            PM[3] = new SqlParameter("@BudgetHead ", SqlDbType.Char, 20) { Value = BudgetHead };
            //PM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            //PM[4] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = Treasurycode };
           
            return gf.Filldatatablevalue(PM, "EgBudgetheadwiseSchemaAmountoff",dt,null);
        }


        #endregion

    }
}
