using System;

using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;
using Newtonsoft.Json;
namespace EgBL
{
   public class EgChallanWithORWithOutDivisionBL
    {
        GenralFunction gf = new GenralFunction();

        public Int16 Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BudgetHead { get; set; }

        // Get Challan List with Or With Out Division
        public string GetChallanWithORWithOutDivision()
        {
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@Type", SqlDbType.Int) { Value = Type };
            PARM[1] = new SqlParameter("@BudgetHead", SqlDbType.Char,17) { Value = BudgetHead };
            PARM[2] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = FromDate };
            PARM[3] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = ToDate };

            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgDivisionWiseChallanRpt", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
    }
}
