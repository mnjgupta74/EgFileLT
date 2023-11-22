using System;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgEChallanEditDivCodeBL
    {
        public int DivCode { get; set; }
        public int UserID { get; set; }
        public string Location { get; set; }
        public Int64 GRN { get; set; }
        public DataTable DivCodeList { get; set; }
        public string FullName { get; set; }
        public string TreasuryName { get; set; }
        public double Amount { get; set; }
        public string MajorHead { get; set; }
        public Int16 Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int officeCode { get; set; }
        GenralFunction gf = new GenralFunction();

        public DataTable  GetChallanList()
        {
            //string[] tableName = { "abc" };
            DataTable table = new DataTable();
            SqlParameter[] PM = new SqlParameter[6];
            PM[0] = new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime) { Value = FromDate };
            PM[1] = new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime) { Value = ToDate };
            PM[2] = new SqlParameter("@MajorHead", System.Data.SqlDbType.Char) { Value = MajorHead };
            PM[3] = new SqlParameter("@OfficeCode", System.Data.SqlDbType.Int) { Value =officeCode };
            PM[4] = new SqlParameter("@Type", System.Data.SqlDbType.TinyInt) { Value = Type };
            PM[5] = new SqlParameter("@Location", System.Data.SqlDbType.Char,4) { Value = Location };
            return gf.Filldatatablevalue(PM, "EgGetGrnListForUpdateDivision", table, null);
          
        }
        public  DataTable GetGRNChallanList()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
             return dt = gf.Filldatatablevalue(PARM, "EgGetGrnForUpdateDivision", dt, null);
           
        }
        public int UpdateDivCodeDetail()
        {
            
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt);
            PM[0].Value = GRN;
            PM[1] = new SqlParameter("@DivCode", SqlDbType.Int);
            PM[1].Value = DivCode;
            PM[2] = new SqlParameter("@OfficeCode", SqlDbType.Int);
            PM[2].Value = officeCode;
            return gf.UpdateData(PM, "EgUpdateDivCodeDetail");
        }

        public void FillDivisionList(DropDownList ddl)
        {
            DivCodeList = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = UserID };
            DivCodeList = gf.Filldatatablevalue(PARM, "EgGetAgDivisionCode", DivCodeList, null);
            ddl.DataTextField = "Divname";
            ddl.DataValueField = "SubDivisionofficecode";
            ddl.DataSource = DivCodeList;
            ddl.DataBind();
            //gf.FillListControl(ddl, "EgGetAgDivisionCode", "Divname", "SubDivisionofficecode", PARM);
            //ddl.Items.Insert(0, new ListItem("WithOutDivision", "0"));
        }
        public void FillDivisionListGRNWise(DropDownList ddl)
        {

            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            gf.FillListControl(ddl, "EgGetDivCodeOfficeWise", "DivisionName", "DivCode", PARM);
            //ddl.Items.Insert(0, new ListItem("--Select Div--", "-1"));
        }
        /// fill division List For All Treasury 
        /// 
        public void FillAllDivisionList(DropDownList ddl)
        {
            DivCodeList = new DataTable();
           
            DivCodeList = gf.Filldatatablevalue(null, "EgGetAgDivisionCodeforAllTreasury", DivCodeList, null);
            ddl.DataTextField = "Divname";
            ddl.DataValueField = "SubDivisionofficecode";
            ddl.DataSource = DivCodeList;
            ddl.DataBind();
            
            //ddl.Items.Insert(0, new ListItem("--All Division--", "0"));
            //gf.FillListControl(ddl, "EgGetAgDivisionCode", "Divname", "SubDivisionofficecode", PARM);
            //ddl.Items.Insert(0, new ListItem("WithOutDivision", "0"));
        }
        public void FillTreasury(DropDownList ddl)
        {
            gf.FillListControl(ddl, "EgFillTreasury", "TreasuryName", "TreasuryCode", null);
            ddl.Items.Insert(0, new ListItem("--Select Treasury--", "0"));

        }
    }
}
