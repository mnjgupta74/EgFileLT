using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
namespace EgBL
{
   public class EgClosingAbstractBL : EgCommonFunctionBL
    {
        GenralFunction gf;
        DataTable dt;
        //public DataTable ClosingAbstract()
        //{
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime);
        //    PM[0].Value = Fromdate;
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime);
        //    PM[1].Value = Todate;
        //    gf = new GenralFunction();
        //    dt = new DataTable();
        //    SqlDataReader dr = gf.FillDataReader(PM, "EgClosingAbstract");
        //    if (dr.HasRows != false)
        //    {
        //        dt.Load(dr);
        //        //dr.Dispose();
        //    }
        //    dr.Close();
        //    dr.Dispose();
        //    return dt;
        //}
    }
}
