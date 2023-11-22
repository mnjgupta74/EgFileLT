using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EgDAL;

namespace EgBL
{
   public class EgLOPBL :EgCommonFunctionBL
    {
       //public DataTable GetReportLOP()
       //{
       //    SqlParameter[] PM = new SqlParameter[3];

       //    PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime);
       //    PM[1].Value = Fromdate;
       //    PM[2] = new SqlParameter("@todate", SqlDbType.DateTime);
       //    PM[2].Value = Todate;
       // //   DataSet ds = new DataSet();
       //    GenralFunction gf = new GenralFunction();
       //    SqlDataReader dr = gf.FillDataReader(PM, "EgrasLOP");
       //    DataTable dt = new DataTable();
       //    if (dr.HasRows != false)
       //    {
       //        dt.Load(dr);
       //        dr.Dispose();
       //    }
       //     dr.Close();
       //     dr.Dispose();
       //     return dt;

       //}
    }
}
