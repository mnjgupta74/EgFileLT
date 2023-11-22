using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class TreasuryBudgetHeadRevenueBL
    {
        GenralFunction gf;
        #region Properties
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Tcode { get; set; }
        public string Mcode { get; set; }
        public int DeptCode { get; set; }
        public double TotalAmount { get; set; }
        public string MajorHead { get; set; }
        public string ScheCode { get; set; }
        public string BudgetHead { get; set; }

        #endregion
        public string FillBudgetHeadListAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = Tcode };
            PM[3] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[4] = new SqlParameter("@MajorHead", SqlDbType.Char, 17) { Value = Mcode };
            dt = gf.Filldatatablevalue(PM, "[EgTreasuryBudgetHeadAmount]", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        /// <summary>
        /// Fill Schema Wise Total Amount Report Created By manoj 
        /// Get MajorHead List based on Dept And BudgetHead List based On MajorHead 
        /// Report Paramater From Date , Todate , dept And Schecode
        /// Return Parameter SchemaName , Amount
        /// Use Method FillMAjorhead , FillBudgetHeadWithSchema,FillTblSchemaWiseAmount()
        /// </summary>
        /// <returns></returns>
        public DataTable FillMajorHead()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PARM, "EgDeptwiseMajorHeadList", dt, null);
        }
        public DataTable FillBudgetHeadwithMultiMajorHeads()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@MajorHead", SqlDbType.VarChar, 200) { Value = MajorHead };
            return gf.Filldatatablevalue(PARM, "EgGetBudgetHeadWithSchema", dt, null);
        }
        public DataTable FillTblSchemaAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@ScheCode", SqlDbType.VarChar, 300) { Value = ScheCode };
            return gf.Filldatatablevalue(PM, "EgGetSelectedSchemaAmountRpt", dt, null);

        }
        /// <summary>
        /// Purpose wise report bifurcation treasury wise 25/02/2021
        /// </summary>
        /// <returns></returns>
        public DataTable FillTblSchemaAmount_TreasuryWise()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@ScheCode", SqlDbType.VarChar, 300) { Value = ScheCode };
            return gf.Filldatatablevalue(PM, "EgGetSelectedSchemaAmountRpt_TreasuryWise", dt, null);
        }
    }


}
