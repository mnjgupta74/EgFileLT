using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgGrnAmountDetailRptBL
    {
        GenralFunction gf;
        #region Class Properties

        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public double Amount1 { get; set; }
        public double Amount2 { get; set; }
        public string RemitterName { get; set; }
        #endregion

        #region Class Method

        public string BindGrnAmountDetails()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@Amount1", SqlDbType.Money) { Value = Amount1 };
            PM[3] = new SqlParameter("@Amount2", SqlDbType.Money) { Value = Amount2 };

            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgGrnAmountDetails", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        //public void BindGrnAmountDetails(GridView grd)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
        //    PM[2] = new SqlParameter("@Amount1", SqlDbType.Money) { Value = Amount1 };
        //    PM[3] = new SqlParameter("@Amount2", SqlDbType.Money) { Value = Amount2 };
        //    gf.FillGridViewControl(grd, PM, "EgGrnAmountDetails");
        //}

        //public DataTable  BindRemitterDetails()
        //{
        //    DataTable dt = new DataTable();
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[3];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
        //    PM[2] = new SqlParameter("@RemitterName", SqlDbType.Char,30) { Value = RemitterName };
        //    return gf.Filldatatablevalue(PM, "EgGrnRemitterDetails", dt, null);

        //}

        public string AmountDetailsRemmitterWise()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@RemitterName", SqlDbType.Char, 30) { Value = RemitterName };
            dt = gf.Filldatatablevalue(PM, "EgGrnRemitterDetails", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        #endregion
    }
}
