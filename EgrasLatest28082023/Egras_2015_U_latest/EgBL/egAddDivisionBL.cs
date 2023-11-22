using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;
namespace EgBL
{
    public class egAddDivisionBL
    {
        GenralFunction gf = new GenralFunction();
        //DataFunctions df = new DataFunctions();
        public int DeptCode { get; set; }
        public Int16 type { get; set; }
        public string TreasuryCode { get; set; }
        public string OfficeId { get; set; }
        public string AgOfficeId { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public DataTable SelectOfficeList { get; set; }
        public DataTable RemoveOfficeList { get; set; }
        public void FillTreasury(DropDownList drpTreasury)
        {
            gf.FillListControl(drpTreasury, "[EgFillMainTreasury]", "TreasuryName", "TreasuryCode", null);
            drpTreasury.Items.Insert(0, new ListItem("--Select Treasury--", "0"));
        }
        public void GetDepartmentListForOfficeMap(DropDownList drpDepartment)
        {
            gf.FillListControl(drpDepartment, "EgDepartmentListForOfficeMap", "DeptNameEnglish", "DeptCode", null);
            drpDepartment.Items.Insert(0, new ListItem("--Select DepartmentList--", "0"));
        }
        public void GetDivisionList(DropDownList drpdivision)
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt1 = gf.Filldatatablevalue(PARM, "EgDiviSionListwithName", dt, null);
            drpdivision.DataSource = dt1;
            drpdivision.DataTextField = "DivisionName";
            drpdivision.DataValueField = "DivisionCode";
            drpdivision.DataBind();
            drpdivision.Items.Insert(0, new ListItem("--Select Division--", "0"));
            //gf.FillListControl(drpdivision, "EgDiviSionListwithName", "DivisionName", "DivisionCode", null);
        }
        public void GetDiviSionListMaster(DropDownList drpdivision)
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            DataTable dt1 = gf.Filldatatablevalue(PARM, "EgGetDiviSionListwithName", dt, null);
            drpdivision.DataSource = dt1;
            drpdivision.DataTextField = "DivisionName";
            drpdivision.DataValueField = "DivisionCode";
            drpdivision.DataBind();
            drpdivision.Items.Insert(0, new ListItem("--Select Division--", "0"));
            //gf.FillListControl(drpdivision, "EgDiviSionListwithName", "DivisionName", "DivisionCode", null);
        }
        public DataTable GetOfficeListForDivision()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            return gf.Filldatatablevalue(PARM, "EgOfficeListForDivision", dt, null);
        }
        public DataTable GetOfficeListForSubDivision()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            PARM[2] = new SqlParameter("@DivisionCode", SqlDbType.Int) { Value = DivisionCode };
            
            return gf.Filldatatablevalue(PARM, "EgOfficeListForSubDivision", dt, null);
            
        }

        public DataTable GetOfficeList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            return gf.Filldatatablevalue(PARM, "EgOfficeList", dt, null);
        }
        public DataTable GetSubDivisionofficeList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Char, 4) { Value = TreasuryCode };
            PARM[1] = new SqlParameter("@DeptCode", SqlDbType.Int) { Value = DeptCode };
            return gf.Filldatatablevalue(PARM, "EgSubdivisionOfficeList", dt, null);
            // drpSubofficeCode.Items.Insert(0, new ListItem("--Select SubDiviSion--", "0"));
        }
        public int InsertNewDivision()
        {
            SqlParameter[] PM = new SqlParameter[7];
            PM[0] = new SqlParameter("@Treasurycode", SqlDbType.Char, 4);
            PM[0].Value = TreasuryCode;
            PM[1] = new SqlParameter("@Divisioncode", SqlDbType.Int);
            PM[1].Value = DivisionCode;
            PM[2] = new SqlParameter("@DivisionName", SqlDbType.VarChar, 20);
            PM[2].Value = DivisionName;
            PM[3] = new SqlParameter("@SelectOfficeList", SqlDbType.Structured);
            PM[3].Value = SelectOfficeList;
            PM[4] = new SqlParameter("@RemoveOfficeList", SqlDbType.Structured);
            PM[4].Value = RemoveOfficeList;
            PM[5] = new SqlParameter("@AgOfficeId", SqlDbType.Int);
            PM[5].Value = AgOfficeId;
            PM[6] = new SqlParameter("@DeptCode", SqlDbType.Int);
            PM[6].Value = DeptCode;
            return gf.UpdateData(PM, "EgInsertNewDivision");
        }
        public int InsertNewDivisionMaster()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Treasurycode", SqlDbType.Char, 4);
            PM[0].Value = TreasuryCode;

            //PM[3] = new SqlParameter("@SelectOfficeList", SqlDbType.Structured);
            //PM[3].Value = SelectOfficeList;
            PM[1] = new SqlParameter("@RemoveOfficeList", SqlDbType.Structured);
            PM[1].Value = RemoveOfficeList;
            PM[2] = new SqlParameter("@AgOfficeId", SqlDbType.Int);
            PM[2].Value = AgOfficeId;
            //PM[6] = new SqlParameter("@DeptCode", SqlDbType.Int);
            //PM[6].Value = DeptCode;
            return gf.UpdateData(PM, "EgInsertNewDivisionMaster");
        }
        //public void GetNewDivisionList(GridView grd)
        //{
        //    SqlParameter[] PARM = new SqlParameter[3];
        //    PARM[0] = new SqlParameter("@TreasuryCode", SqlDbType.Int) { Value = TreasuryCode };
        //    PARM[1] = new SqlParameter("@DivisionCode", SqlDbType.Int) { Value = DivisionCode };
        //    PARM[2] = new SqlParameter("@OfficeId", SqlDbType.Int) { Value = OfficeId };
        //    gf.FillGridViewControl(grd, PARM, "EgGetNewDivisionList");
        //}
        public string GetOfficeId()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Treasurycode", SqlDbType.Char, 4);
            PM[0].Value = TreasuryCode;
            PM[1] = new SqlParameter("@DeptCode", SqlDbType.Int);
            PM[1].Value = DeptCode;
            PM[2] = new SqlParameter("@OfficeID", SqlDbType.Int);
            PM[2].Value = OfficeId;
            return gf.ExecuteScaler(PM, "EgCheckOfficeId");
        }
    }
}