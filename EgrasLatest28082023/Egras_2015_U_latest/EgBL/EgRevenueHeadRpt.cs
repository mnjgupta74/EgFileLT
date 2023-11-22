using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
//using DL;
namespace EgBL
{

    public class EgRevenueHeadRpt
    {
        GenralFunction gf = new GenralFunction();
        #region Properties


        public string Date { get; set; }
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public int totalcount { get; set; }
        public double TotalAmount { get; set; }
        #endregion

        #region Function
        //public void ShowRptHeadWise(Repeater rpt)
        //{

        //    SqlParameter[] PM = new SqlParameter[3];
        //    PM[0] = new SqlParameter("@fromDate", SqlDbType.VarChar, 10) { Value = FromDate };
        //    PM[1] = new SqlParameter("@TODate", SqlDbType.VarChar, 10) { Value = ToDate };
        //    PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };

        //    gf.FillRepeaterControl(rpt, PM, "EgRevenueHeadRpt");

        //}
        //public void shqpSchemaRpt(Repeater rpt)
        //{
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@fromDate", SqlDbType.VarChar, 10) { Value = FromDate };
        //    PM[1] = new SqlParameter("@TODate", SqlDbType.VarChar, 10) { Value = ToDate };
        //    PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
        //    PM[3] = new SqlParameter("@BudgetHead ", SqlDbType.Char, 13) { Value = BudgetHead };

        //    gf.FillRepeaterControl(rpt, PM, "EgBudgetheadwiseSchemaAmount");
        //}
       
        public void BankChallanList(DataTable dt)
        {
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@Date", SqlDbType.Char, 10) { Value = Date };
            PARM[1] = new SqlParameter("@RowCount", SqlDbType.Int) { Value = 0 };
            PARM[1].Direction = ParameterDirection.Output;
            PARM[2] = new SqlParameter("@TotalAmount", SqlDbType.Money) { Value = 0 };
            PARM[2].Direction = ParameterDirection.Output;
            gf.Filldatatablevalue(PARM, "EgTotalChallan", dt, null);
            totalcount = Int32.Parse(PARM[1].Value.ToString());
            if (PARM[2].Value.ToString() != "")
            { TotalAmount = Convert.ToDouble(PARM[2].Value.ToString()); }
        }
        #endregion
    }
}
