using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class PDAccListTreasuryBL
    {
        GenralFunction gf;
        #region Properties
        // public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int UserId { get; set; }
        public string TreasuryCode { get; set; }
        public int PDAcc { get; set; }
        public int MajorHead { get; set; }
        public DataTable dttreasurylist { get; set; }
        public bool isAlreadyProcessed { get; set; }
        public string BudgetHead { get; set; }
        #endregion
        #region Function

        /// <summary>
        /// Binding of DropDown ddlpdacc
        /// </summary>
        /// <param name="grd"></param>
        public void PopulateMajorHeadList(DropDownList ddlmajorhead) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = TreasuryCode };
            gf.FillListControl(ddlmajorhead, "EgMajorHeadListTreasuryWise", "MajorHead", "MajorHead", PARM);
            ddlmajorhead.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
        }
        //public void BindPdListRepeater(Repeater rpt)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[4];
        //    PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
        //    PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
        //    PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
        //    PARM[3] = new SqlParameter("@Majorhead", SqlDbType.Int) { Value = MajorHead };
        //    gf.FillRepeaterControl(rpt, PARM, "EgPDAccListTreasuryWise");
        //}

        public DataTable BindPdListRepeater()
        {
            DataTable dt1 = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[3] = new SqlParameter("@Majorhead", SqlDbType.Int) { Value = MajorHead };
            dt1 = gf.Filldatatablevalue(PARM, "EgPDAccListTreasuryWise", dt1, null);
            return dt1;
        }
        public DataTable BindHeadWiseRepeater()
        {
            DataTable dt1 = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[3] = new SqlParameter("@MajorHead", SqlDbType.Int) { Value = MajorHead };
            dt1 = gf.Filldatatablevalue(PARM, "EgHeadWiseListTreasuryWise", dt1, null);
            if (dt1.Rows.Count > 0)
            {
                isAlreadyProcessed = Convert.ToInt32(dt1.Rows[0][3]) == 1 ? true : false;
            }
            return dt1;
        }
        /// <summary>
        /// Get Treasury List Transactions Wise
        /// </summary>
        public string GetTreasuryListTransWise()
        {
            gf = new GenralFunction();
            dttreasurylist = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            dttreasurylist = gf.Filldatatablevalue(PM, "EgGetTreasuryListTransWise", dttreasurylist, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dttreasurylist);
            return JSONString;
        }
        public int InsertRajkosh_PDDataTransfer()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };
            PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[3] = new SqlParameter("@MajorHead", SqlDbType.VarChar,4) { Value = MajorHead };
            return Convert.ToInt32(gf.ExecuteScaler(PARM, "EgInsertRajkosh_PDDataTransfer"));
        }
        public DataTable BindPdExtraDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@PDAcc", SqlDbType.Int) { Value = PDAcc };
            PARM[3] = new SqlParameter("@TreasuryCode", SqlDbType.Char,4) { Value = TreasuryCode };
            dt = gf.Filldatatablevalue(PARM, "EgPDAccListTreasuryWiseDetail", dt, null);
            return dt;
            //gf.FillRepeaterControl(rpt, PARM, "EgPDAccListTreasuryWise");
        }
        public DataTable BindHeadWiseExtraDetail()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char,13) { Value = BudgetHead };
            PARM[3] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            dt = gf.Filldatatablevalue(PARM, "EgHeadWiseExtraDetail", dt, null);
            return dt;
            //gf.FillRepeaterControl(rpt, PARM, "EgPDAccListTreasuryWise");
        }
        #endregion
    }
}
