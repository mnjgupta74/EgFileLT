using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace EgBL
{
    public class EgTreasuryAndDepartmetRptBL
    {
        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Tcode { get; set; }
        public string Mcode { get; set; }
        public int DeptCode { get; set; }
        public double TotalAmount { get; set; }
        #endregion
        #region Function
        /// <summary>
        /// Get Treasury wise Total Amount List
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable  TreasuryTotalAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            if (Tcode != null && Tcode.Trim().Substring(2, 2) != "00")
            {
                return gf.Filldatatablevalue(PM, "EgGetSubTreasuryTotalAmount", dt, null);
                // gf.FillRepeaterControl(rpt, PM,);
            }
            else
            {
                return gf.Filldatatablevalue(PM, "EgGetTreasuryTotalAmount", dt, null);
                //gf.FillRepeaterControl(rpt, PM, "EgGetTreasuryTotalAmount");
            }

        }
        /// <summary>
        /// Fill Treasury and MajorHead Wise Total Amount List 
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable TreasuryMajorHeadAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            PM[3] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = Mcode };
            return gf.Filldatatablevalue(PM, "EgGetTreasuryMajorHeadAmount", dt, null);
            
        }
        //public void TreasuryBudgetHeadAmount(Repeater rpt)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[3];
        //    PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
        //    PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
        //    PM[2] = new SqlParameter("@Mcode", SqlDbType.Char, 4) { Value = Mcode };
        //    gf.FillRepeaterControl(rpt, PM, "EgGetTreasuryBudgetHeadAmount");





        public void TreasuryBudgetHeadDetail(GridView gv)
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            PARM[3] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = Mcode };
            DataTable dt1 = gf.Filldatatablevalue(PARM, "EgGetTreasuryMajorHeadAmountDetail", dt, null);
            gv.DataSource = dt1;
            gv.DataBind();
            TotalAmount = Convert.ToInt64(dt1.Compute("Sum(Amount)", ""));
        }






        //}
        /// <summary>
        /// Fill TreasuryWise Department List
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable TreasuryDepartmentAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            return  gf.Filldatatablevalue (PM, "EgGetTreasuryDepartmentAmount",dt,null);

        }
        /// <summary>
        /// Fill Treasury and  Department wise Major head List
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable  FillMajorHeadListAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            PM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[4] = new SqlParameter("@MajorHead", SqlDbType.Char,4) { Value =Mcode };
         
             return gf.Filldatatablevalue(PM, "EgDepartmentWiseMajorHeadAmount", dt, null);

        }
        /// <summary>
        /// show  Majorhead List wise BudgetHead List Amount
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable  FillBudgetHeadListAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            PM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[4] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = Mcode };
            return gf.Filldatatablevalue(PM, "EgMajorHeadWiseBudgetHeadAmount",dt,null);
        }
        #endregion
    }
}
