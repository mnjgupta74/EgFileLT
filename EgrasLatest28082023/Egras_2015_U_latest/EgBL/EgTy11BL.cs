using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgTy11BL : EgCommonFunctionBL
    {
        #region
        GenralFunction gf;
        DataTable dt;
        //public DataTable EgTy11report()
        //{
        //    SqlParameter[] PM = new SqlParameter[3];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
        //    PM[2] = new SqlParameter("@majorHead", SqlDbType.Char, 4) { Value = majorHead };

        //    gf = new GenralFunction();
        //    dt = new DataTable();
        //    SqlDataReader dr = gf.FillDataReader(PM, "Eg_Ty11");
        //    if (dr.HasRows != false)
        //    {
        //        dt.Load(dr);
        //       // dr.Dispose();
        //    }

        //    dr.Close();
        //    dr.Dispose();
        //    return dt;
        //}

        public DataTable BindTy11Grid()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = majorHead };
            PM[3] = new SqlParameter("@tcode", SqlDbType.Char, 4) { Value = tcode };
            return gf.Filldatatablevalue(PM, "Eg_Ty11_BudgetHeadWise", dt, null);

        }
        public DataTable  BindTreasuryWiseChallanGrid()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = majorHead };
            PM[3] = new SqlParameter("@tcode", SqlDbType.Char, 4) { Value = tcode };
            return gf.Filldatatablevalue(PM, "Eg_TreasuryWiseChallanRpt", dt,null);
         
           

        }
        public DataTable GetTreasuryWiseChallanDt()
        {
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = majorHead };
            PM[3] = new SqlParameter("@tcode", SqlDbType.Char, 4) { Value = tcode };
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            return gf.Filldatatablevalue(PM, "Eg_TreasuryWiseChallanRpt", dt, null);

        }

        // Add New function For List of Treasury And Sub Treasury 17 May 2019


        /// <summary>
        /// JSON WORK 29 may 2019
        /// </summary>
        /// <returns></returns>
        public string FillTreasury()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            gf.Filldatatablevalue(PM, "EgGetTreasurySubTreasury", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;

        }
        //public string BindTreasuryWiseChallanGrid()
        //{
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
        //    PM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = majorHead };
        //    PM[3] = new SqlParameter("@tcode", SqlDbType.Char, 4) { Value = tcode };

        //    DataTable dt = new DataTable();
        //    gf = new GenralFunction();
        //    gf.Filldatatablevalue(PM, "Eg_TreasuryWiseChallanRpt", dt, null);
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);
        //    return JSONString;
        //}
        //public string BindTy11Grid()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
        //    PM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = majorHead };
        //    PM[3] = new SqlParameter("@tcode", SqlDbType.Char, 4) { Value = tcode };

        //    gf = new GenralFunction();
        //    gf.Filldatatablevalue(PM, "Eg_Ty11_BudgetHeadWise", dt, null);
        //    string JSONString = string.Empty;
        //    JSONString = JsonConvert.SerializeObject(dt);
        //    return JSONString;

        //}

        #endregion
    }
}
