using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EgDAL;
using System.Data;
using System.Data.SqlClient;
namespace EgBL
{
    public class EgYearWiseChart
    {
        private string _type, _year;

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public void FillRepeter(DataTable dt)
        {
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@year", SqlDbType.Char, 4);
            PM[0].Value = Year;
            PM[1] = new SqlParameter("@type", SqlDbType.Char, 1);
            PM[1].Value = Type;
            GenralFunction gf = new GenralFunction();
            gf.Filldatatablevalue(PM, "EgYearorFinYearwisechart", dt, null);
        }

        public void FillRepeterYearly(DataTable dt)
        {
            SqlParameter[] PM = new SqlParameter[0];
            //PM[0] = new SqlParameter("@year", SqlDbType.Char, 4);
            //PM[0].Value = Year;
            //PM[1] = new SqlParameter("@type", SqlDbType.Char, 1);
            //PM[1].Value = Type;
            GenralFunction gf = new GenralFunction();
            gf.Filldatatablevalue(PM, "EgTotalChallanFinYearWise", dt, null);
        }
    }
}
