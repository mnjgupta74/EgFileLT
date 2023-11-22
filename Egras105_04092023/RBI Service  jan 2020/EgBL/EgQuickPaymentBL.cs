using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
{
    public class EgQuickPaymentBL 
    {
        GenralFunction gf ;//= new GenralFunction();

        #region Class Properties
        public  string  type { get; set; }
        public string SearchText { get; set; }
        public int DeptCode { get; set; }
        #endregion
       
        
     

        #region Function

        public DataTable GetSearchData()
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = type  };
            PARM[1] = new SqlParameter("@SearchText", SqlDbType.VarChar,100) { Value = SearchText };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgQuickPaymentSearchRecords", dt, null);
            return dt;
        }

        public DataTable GetDeptList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            dt = gf.Filldatatablevalue(null, "EgGetDepartmentList", dt, null);
            return dt;

        }

        public DataTable QuickPayDeptWiseMajorHeadList()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@DeptCode", SqlDbType.BigInt) { Value = DeptCode };           
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PM, "EgquickpaymentDeptwiseMajorHeadList", dt, null);
        }


        #endregion

    }
}
