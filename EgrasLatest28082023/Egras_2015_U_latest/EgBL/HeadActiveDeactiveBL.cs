using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class HeadActiveDeactiveBL
    {
        GenralFunction gf;
        #region Class Properties
        public string BudgetHead { get; set; }
        public int DeptCode { get; set; }
        public string TreasuryCode { get; set; }
        public string type { get; set; }
        public string Flag { get; set; }
        public string Majorhead { get; set; }
        public string PDAccount { get; set; }
        public int OfficeId { get; set; }
        #endregion
        public string GetBudegtHeadsList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@MajorHead", SqlDbType.Char, 4) { Value = Majorhead };
            PARM[2] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["userType"]) };
            gf.Filldatatablevalue(PARM, "HeadClose_GetBudgetHeadsList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public int UpdateHeadStatus()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 130) { Value = BudgetHead };
            PARM[2] = new SqlParameter("@Mode", SqlDbType.Char, 1) { Value = type };
            PARM[3] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["userType"]) };
            PARM[4] = new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]) };
            result = gf.UpdateData(PARM, "EgBudgetHeadActiveDeactive");
            return result;
        }
        public string GetDepartmentsList()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDepartmentList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public string GetBudegtHeadsListRpt()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[1] = new SqlParameter("@Type", SqlDbType.TinyInt) { Value = type };
            PARM[2] = new SqlParameter("@Flag", SqlDbType.Char,1) { Value = Flag };
            gf.Filldatatablevalue(PARM, "GetActiveDeactiveHeadList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        //-------------PDActiveDeactive
        /// <summary>
        /// Pd Account Active Deactive 10 feb 2020
        /// </summary>
        /// <returns></returns>
        public string GetPDList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[2] = new SqlParameter("@type", SqlDbType.Int) { Value = 0 };
            gf.Filldatatablevalue(PARM, "EgGetPdAccountListForActiveDeactive", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        public int UpdatePDStatus()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.VarChar, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[2] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = type };
            PARM[3] = new SqlParameter("@PDAccNo", SqlDbType.VarChar, 300) { Value = PDAccount };
            result = gf.UpdateData(PARM, "EgUpdatePDActiveDeactive");
            return result;
        }


        // Division Code Active/Deactive

        public void FillOfficeList(DropDownList ddl)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            gf.FillListControl(ddl, "EgGetOfficeForDivision", "OfficeName", "SubDivisionofficecode", PARM);
            ddl.Items.Insert(0, new ListItem("--Select Office--", "0"));
        }
        public void FillDivisionCodeRpt(Repeater rpt)
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId};
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetDivisionOfficeWise", dt, null);
            rpt.DataSource = dt;
            rpt.DataBind();
            dt.Dispose();
        }
        public int DivisionActiveDeactive(string DivCode, string Activate)            //// Sandeep Profile Actibe 
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PM[1] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
            PM[2] = new SqlParameter("@DivCode", SqlDbType.Int) { Value = DivCode };
            PM[3] = new SqlParameter("@Type", SqlDbType.Char, 1) { Value = Activate };
            PM[4] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Convert.ToInt32(System.Web.HttpContext.Current.Session["UserID"]) };
            return gf.UpdateData(PM, "EgDivisionActiveDeActive");
        }
    }
}
