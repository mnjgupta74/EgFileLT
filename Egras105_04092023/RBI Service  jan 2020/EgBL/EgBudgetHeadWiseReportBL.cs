using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DL;
using EgDAL;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System.Web;

namespace EgBL
{
 public   class EgBudgetHeadWiseReportBL
    {
        GenralFunction gf;
        public string BudgetHead { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set;}
        public Int16 type { get; set; }
        public Int32 OfficeCode { get; set; }
        public Int64 TotalAmount { get; set; }

        public string Treasury{ get; set; }
       // bool disposed = false;
        //public void FillBudgetHeadWiseGrd(GridView grd)
        //{

         
        //        DataTable dt = new DataTable();
        //        gf = new GenralFunction();
        //        SqlParameter[] PARM = new SqlParameter[3];
        //        PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
        //        PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
        //        PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
        //        DataTable dt1 = gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReport", dt, null);
        //        grd.DataSource = dt1;
        //        grd.DataBind();
        //        if (dt1.Rows.Count > 0)
        //            TotalAmount = Convert.ToInt64(dt1.Compute("Sum(Amount)", ""));
        //        else
        //            TotalAmount = 0;
          
        //}

        //public void  FillBudgetHeadAllDept(GridView grd )
        //{
           
        //        gf = new GenralFunction();
        //        SqlParameter[] PARM = new SqlParameter[4];
        //        PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
        //        PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
        //        PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
        //        PARM[3] = new SqlParameter("@Type", SqlDbType.SmallInt) { Value = type };

        //        gf.FillGridViewControl(grd, PARM, "EgBudgetHeadWiseReportAllDepartment");
          
           
        //}
        public DataTable BindBudgetheadDetails()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReport", dt, null);
            return dt;
        }


        //public void FillBudgetHeadWiseGrdForOffice(GridView grd)
        //{
        //    DataTable dt = new DataTable();
        //    gf = new GenralFunction();
        //    SqlParameter[] PARM = new SqlParameter[5];
        //    PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
        //    PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
        //    PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
        //    PARM[3] = new SqlParameter("@OfficeCode", SqlDbType.Int) { Value = OfficeCode };
        //    PARM[4] = new SqlParameter("@Treasury", SqlDbType.Char,4) { Value = Treasury };


        //    DataTable dt1 = gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReportForOffice", dt, null);
        //    grd.DataSource = dt1;
        //    grd.DataBind();
        //    if (dt1.Rows.Count > 0)
        //        TotalAmount = Convert.ToInt64(dt1.Compute("Sum(Amount)", ""));
        //    else
        //        TotalAmount = 0;
        //}


        public string FillBudgetHeadDataForOffice()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
            PARM[3] = new SqlParameter("@OfficeCode", SqlDbType.Int) { Value = Convert.ToInt32(HttpContext.Current.Session["UserId"]) };
            PARM[4] = new SqlParameter("@Treasury", SqlDbType.Char, 4) { Value = Treasury };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReportForOfficeRpt", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        /// JSON Table Modify  31 May 2019
        /// </summary>
        public string FillBudgetHeadWiseGrd()
        {


            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
            DataTable dt1 = gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReport", dt, null);
            //grd.DataSource = dt1;
            //grd.DataBind();
            //if (dt1.Rows.Count > 0)
            //    TotalAmount = Convert.ToInt64(dt1.Compute("Sum(Amount)", ""));
            //else
            //    TotalAmount = 0;
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;

        }
        /// JSON Table Modify  31 May 2019
        public string FillBudgetHeadAllDept()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@BudgetHead", SqlDbType.Char, 13) { Value = BudgetHead };
            PARM[1] = new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = FromDate };
            PARM[2] = new SqlParameter("@ToDate", SqlDbType.DateTime, 10) { Value = ToDate };
            PARM[3] = new SqlParameter("@Type", SqlDbType.SmallInt) { Value = type };          
            gf.Filldatatablevalue(PARM, "EgBudgetHeadWiseReportAllDepartment", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }


    }
}
