using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgDeptAmountRptBL
    {
        GenralFunction gf;
        #region properties

        public int DeptCode { get; set; }
        public string majorHead { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BudgetHead { get; set; }
        public int ScheCode { get; set; }
        public int type { get; set; }
        public string TreasuryCode { get; set; }
        public int TransactionType { get; set; }
        public string OfficeCode { get; set; }
        #endregion
        #region Function
        /// <summary>
        /// fill department Droddownlist
        /// </summary>
        /// <param name="ddl"></param>
        public void PopulateDepartmentList(DropDownList ddl)
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            //gf.FillListControl(ddl, "EgGetSelectedDepartmentList", "deptnameEnglish", "deptcode", PARM);
            gf.Filldatatablevalue(PARM, "EgGetSelectedDepartmentList", dt, null);
            ddl.DataTextField = "deptnameEnglish";
            ddl.DataValueField = "deptcode";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
            dt.Dispose();
        }
        /// <summary>
        /// Get Department Revenue REport
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable  ShowRptDeptWise()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            //if(TransactionType==2)
            //gf.FillRepeaterControl(rpt, PM, "EgDeptAmount");
            //else
            
            return gf.Filldatatablevalue(PM, "EgDeptAmount_CTD",dt,null);

        }

        public DataTable  showSchemaRpt()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@ScheCode ", SqlDbType.Int) { Value = ScheCode };
            PM[4] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            //if(TransactionType==2)
          
            return  gf.Filldatatablevalue(PM, "EgPurposeAmountDateWise", dt,null);
            //else
            // gf.FillRepeaterControl(rpt, PM, "EgPurposeAmountDateWise_CTD");
        }

        //public void FillAmountGrid(GridView grd)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[3];
        //    PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
        //    PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
        //    PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
        //    gf.FillGridViewControl(grd, PARM, "EgDeptAmount");
        //}
        public void PopulateSchemaList(DataTable dt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@DepartmentCode", SqlDbType.Int) { Value = DeptCode };
            gf.Filldatatablevalue(PARM, "EgBudgetHeadListRpt", dt, null);
        }
        public void FillSchemaMajorHeadWise(DataTable dt)
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            PARM[1] = new SqlParameter("@majorHead", SqlDbType.Char, 4) { Value = majorHead };
            gf.Filldatatablevalue(PARM, "EgSchemaBudgetHeadWise", dt, null);
        }
        public string FillSchemaMajorHeadWise_Dept()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            //PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            //PARM[1] = new SqlParameter("@majorHead", SqlDbType.Char, 4) { Value = majorHead };
            PARM[0] = new SqlParameter("@DeptCodeSelect", SqlDbType.Int) { Value = DeptCode };
            //gf.Filldatatablevalue(PARM, "EgSchemaBudgetHeadWise_Dept", dt, null);

            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgSchemaBudgetHeadWise_Dept", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;

        }

        /// <summary>
        /// Get  Total amount  Treasurywise  on BudgetHead  for department 
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable  ShowRptHeadWise()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[4] = new SqlParameter("@BudgetHead", SqlDbType.VarChar, 13) { Value = BudgetHead };
           
            return gf.Filldatatablevalue(PM, "EgRevenueHeadRpt", dt,null);

        }
        /// <summary>
        /// Get Department Revenue for all  Treasury
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable TreasuryTotalAmount()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@Deptcode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PM[4] = new SqlParameter("@type", SqlDbType.TinyInt) { Value = type };
            return gf.Filldatatablevalue(PM, "EgTreasuryTotalDeptWise", dt, null);
        }
        /// <summary>
        /// Get Total amount Treasurywise on BudgetHead Purpose 
        /// </summary>
        /// <param name="rpt"></param>
        public DataTable shqpSchemaRpt()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PM[3] = new SqlParameter("@BudgetHead ", SqlDbType.Char, 13) { Value = BudgetHead };
            PM[4] = new SqlParameter("@TreasuryCode ", SqlDbType.Char, 4) { Value = TreasuryCode };
            // gf.FillRepeaterControl(rpt, PM, "EgBudgetheadwiseSchemaAmount");
            return gf.Filldatatablevalue(PM, "EgBudgetheadwiseSchemaAmount",dt,null);
        }

        public int MapBudgetHead()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            return gf.UpdateData(PARM, "EgMapBudgethead");
        }

        public string CheckBudgetHeadMapping()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            return gf.ExecuteScaler(PARM, "EgCheckBudgetHeadMapping");
        }
        /// mainTain  Budget head Transfer Log 5 june 2018
        /// 
        public void InsertBudgetHeadMappingLog()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            gf.UpdateData(PARM, "EgBudgetHeadTransferLog");
        }

        public string BindDepartment()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserId };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetSelectedDepartmentList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        #endregion
    }
}
