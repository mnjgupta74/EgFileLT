using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
namespace EgBL
{
    public class EgToChallanReportBL
    {
        #region Properties
        public int UserId { get; set; }
        public int totalcount { get; set; }
        public int StartIdx { get; set; }
        public int EndIdx { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double TotalAmount { get; set; }
        public string TreasuryCode { get; set; }
        public int DeptCode { get; set; }
        public string BudgetHead { get; set; }
        public long GRN { get; set; }
        #endregion
        #region Method

        public void FillBudgetHeads(DropDownList ddl)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            gf.FillListControl(ddl, "EgGetBudgetHeads", "budgetheadname", "budgethead", PARM);
            ddl.Items.Insert(0, new ListItem("--Select BudgetHead--", "0"));
        }

        //public void BindData(GridView grd)
        //{
        //    GenralFunction gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[9];
        //    PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char,2) { Value = TreasuryCode };
        //    PARM[1] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
        //    PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
        //    PARM[3] = new SqlParameter("@startIdx", SqlDbType.Int) { Value = StartIdx };
        //    PARM[4] = new SqlParameter("@endIdx", SqlDbType.Int) { Value = EndIdx };
        //    PARM[5] = new SqlParameter("@RowCount", SqlDbType.Int) { Value = 0 };
        //    PARM[5].Direction = ParameterDirection.Output;
        //    PARM[6] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = 0 };
        //    PARM[6].Direction = ParameterDirection.Output;
        //    PARM[7] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
        //    PARM[8] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
        //    gf.FillGridViewControl(grd, PARM, "EgToChallanReport");
        //    totalcount = Int32.Parse(PARM[5].Value.ToString());
        //    if (PARM[6].Value.ToString() != "")
        //    { TotalAmount = Convert.ToDouble(PARM[6].Value.ToString()); }

        //}

        //public void BindDeptRevenue(GridView grd)
        //{
        //    SqlParameter[] PARM = new SqlParameter[3];
        //    PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 2) { Value = TreasuryCode };
        //    PARM[1] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
        //    PARM[2] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
        //    GenralFunction gf = new GenralFunction();
        //    gf.FillGridViewControl(grd, PARM, "EgGetDeptRevenue");          

        //}

        public string GetTreasCode()
        {
            string TreasCode = "0";
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            SqlDataReader dr = gf.FillDataReader(PARM, "EgGetTreasCode");
            if (dr.HasRows)
            {
                dr.Read();
                TreasCode = dr[0].ToString().Trim();
               
            }
            dr.Close();
            dr.Dispose();
            return TreasCode;
        }

        public DataTable FillDetailDiv()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.Int) { Value = GRN };
            return gf.Filldatatablevalue(PM, "EgGrnSchemaAmountDetail", dt, null);
        }

        #endregion
    }
}
