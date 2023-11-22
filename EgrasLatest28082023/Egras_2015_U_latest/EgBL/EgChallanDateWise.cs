using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    /// <summary>
    /// Class Use For Get Grn List Date - Wise 
    /// </summary>
    public class EgChallanDateWise
    {
        GenralFunction gf;
        #region properties
        public int UserId { get; set; }
        public double TotalAmount { get; set; }
        public int totalcount { get; set; }
       
        public Int32 GRN { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StartIdx { get; set; }
        public int EndIdx { get; set; }
        public int Type { get; set; }
        public int DeptCode { get; set; }
        #endregion

        #region Function
        /// <summary>
        /// use for get challan list datewise and total record and total amount
        /// </summary>
        /// <param name="dt"> return challanlist datatable and totalamount and Total Record</param>
        public DataTable ChallanList()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[8];
            DataTable dt = new DataTable();
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@StartIdx", SqlDbType.Int) { Value = StartIdx };
            PM[3] = new SqlParameter("@EndIdx", SqlDbType.Int) { Value = EndIdx };
            PM[4] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PM[5] = new SqlParameter("@RowCount", SqlDbType.Int) { Value = 0 };
            PM[5].Direction = ParameterDirection.Output;
            PM[6] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = 0 };
            PM[6].Direction = ParameterDirection.Output;
            PM[7] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            if (Type == 2)
            {
                dt = gf.Filldatatablevalue(PM, "EgChallanDateWise", dt, null);
            }
            else
            {
                dt = gf.Filldatatablevalue(PM, "EgChallanDateWise_CTD", dt, null);
            }
            totalcount = Int32.Parse(PM[5].Value.ToString());
            if (PM[6].Value.ToString() != "")
            { TotalAmount = Convert.ToDouble(PM[6].Value.ToString()); }
            return dt;
        }
        /// <summary>
        /// use for get budget head amount data on particular GRN number 
        /// </summary>
        /// <param name="dt"> budget head and amount Data Table</param>
        public void BudgeAmountList(DataTable dt)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            dt = gf.Filldatatablevalue(PM, "EgChallanBudgetHeadList", dt, null);
        }


        #endregion

    }
}
