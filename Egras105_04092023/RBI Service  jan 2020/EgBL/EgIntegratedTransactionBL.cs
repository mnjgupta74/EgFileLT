using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgIntegratedTransactionBL
    {
        GenralFunction gf;
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string deptcode { get; set; }
        public string merchantcode { get; set; }
        public string auin { get; set; }
        public bool flag { get; set; }
        public int userid { get; set; }

        /// <summary>
        /// Get Departement total Successful and pending Transaction
        /// </summary>
        /// <param name="rpt"></param>
        public string FillIntegratedChallanRpt()
        {
            flag = false;
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@fromdate", SqlDbType.Date) { Value = fromdate };
            PARM[1] = new SqlParameter("@todate", SqlDbType.Date) { Value = todate };
            PARM[2] = new SqlParameter("@deptcode", SqlDbType.NVarChar, 10) { Value = deptcode };
            PARM[3] = new SqlParameter("@merchantcode", SqlDbType.NVarChar, 20) { Value = merchantcode };
            dt = gf.Filldatatablevalue(PARM, "EgDeptIntegratedTransaction", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        //public void FillIntegratedChallanRpt(Repeater rpt)
        //{
        //    flag = false;
        //    gf = new GenralFunction();
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PARM = new SqlParameter[4];
        //    PARM[0] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
        //    PARM[1] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
        //    PARM[2] = new SqlParameter("@deptcode", SqlDbType.NVarChar, 10) { Value = deptcode };
        //    PARM[3] = new SqlParameter("@merchantcode", SqlDbType.NVarChar, 20) { Value = merchantcode };
        //    dt = gf.Filldatatablevalue(PARM, "EgDeptIntegratedTransaction", dt, null);
        //    if (dt.Rows.Count > 0) flag = true;
        //    rpt.DataSource = dt;
        //    rpt.DataBind();
        //    dt.Dispose();
        //}
        ///// <summary>
        ///// Get Merchant Name by Department 
        ///// </summary>
        ///// <param name="rpt"></param>
        //public void FillMerchantName(DropDownList ddl)
        //{
        //    gf = new GenralFunction();
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PARM = new SqlParameter[1];
        //    PARM[0] = new SqlParameter("@deptcode", SqlDbType.BigInt) { Value = deptcode };
        //    dt = gf.Filldatatablevalue(PARM, "EgGetMerchantByDepartment", dt, null);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    ddl.DataTextField = "MerchantName";
        //    ddl.DataValueField = "MerchantCode";
        //    ddl.DataSource = dt;
        //    ddl.DataBind();
        //    ddl.Items.Insert(0, new ListItem("--All Merchant--", "0"));
        //    //}
        //    dt.Dispose();
        //}

        /// <summary>
        /// Get Merchant Name by Department 
        /// </summary>
        /// <param name="rpt"></param>
        public string FillMerchantName()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@deptcode", SqlDbType.BigInt) { Value = deptcode };
            dt = gf.Filldatatablevalue(PARM, "EgGetMerchantByDepartment", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }


        /// <summary>
        /// Fill Department Challan Detail
        /// </summary>
        /// <param name="rpt"></param>
        public string FillIntegratedChallanDetail()
        {
            flag = false;
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@fromdate", SqlDbType.Date) { Value = fromdate };
            PARM[1] = new SqlParameter("@todate", SqlDbType.Date) { Value = todate };
            PARM[2] = new SqlParameter("@deptcode", SqlDbType.NVarChar) { Value = deptcode };
            PARM[3] = new SqlParameter("@merchantcode", SqlDbType.NVarChar) { Value = merchantcode };
            PARM[4] = new SqlParameter("@auin", SqlDbType.NVarChar) { Value = auin };
            dt = gf.Filldatatablevalue(PARM, "EgDeptIntegratedTransaction_Detail", dt, null);
            if (dt.Rows.Count > 0) flag = true;
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        //public DataTable FillIntegratedChallanDetail()
        //{
        //    flag = false;
        //    gf = new GenralFunction();
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PARM = new SqlParameter[4];
        //    PARM[0] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
        //    PARM[1] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
        //    PARM[2] = new SqlParameter("@deptcode", SqlDbType.NVarChar) { Value = deptcode };
        //    PARM[3] = new SqlParameter("@merchantcode", SqlDbType.NVarChar) { Value = merchantcode };
        //    dt = gf.Filldatatablevalue(PARM, "EgDeptIntegratedTransaction_Detail", dt, null);
        //    if (dt.Rows.Count > 0) flag = true;

        //    return dt;
        //}
        /// <summary>
        /// fill department Droddownlist
        /// </summary>
        /// <param name="ddl"></param>
        public string GetDepartment()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userid };
            gf.Filldatatablevalue(PARM, "EgGetSelectedDepartmentList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
