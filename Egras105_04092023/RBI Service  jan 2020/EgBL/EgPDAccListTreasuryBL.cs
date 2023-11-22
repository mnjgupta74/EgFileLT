using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace EgBL
{
    public class EgPDAccListTreasuryBL
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
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
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
        //public void BindHeadWiseRepeater(Repeater rpt)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[4];
        //    PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
        //    PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
        //    PARM[2] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
        //    PARM[3] = new SqlParameter("@MajorHead", SqlDbType.Int) { Value = MajorHead };
        //    gf.FillRepeaterControl(rpt, PARM, "EgHeadWiseListTreasuryWise");
        //}

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
            SqlParameter[] PARM = new SqlParameter[3];
            DataTable dt = new DataTable();
            PARM[0] = new SqlParameter("@FromDate", SqlDbType.SmallDateTime) { Value = FromDate };
            PARM[1] = new SqlParameter("@ToDate", SqlDbType.SmallDateTime) { Value = ToDate };
            PARM[2] = new SqlParameter("@BudgetHead", SqlDbType.Char,13) { Value = BudgetHead };
            dt = gf.Filldatatablevalue(PARM, "EgHeadWiseExtraDetail", dt, null);
            return dt;
            //gf.FillRepeaterControl(rpt, PARM, "EgPDAccListTreasuryWise");
        }
        #endregion
    }
}
