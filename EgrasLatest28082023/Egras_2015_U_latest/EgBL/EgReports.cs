using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgReports
    {
        GenralFunction gf;
        #region Class Properties

        public int userid { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public string BsrCode { get; set; }
        public string Budgethead { get; set; }
        public int officeid { get; set; }
       // public int Type { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        #endregion

        #region Class Method

        public DataTable EBankScroll45ARpt()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BsrCode };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[2] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = Todate };
            dt = gf.Filldatatablevalue(PM, "EgBankScroll45A", dt, null);
            return dt;
        }

        public DataTable E45AChallan()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BsrCode };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[2] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = Todate };
            dt = gf.Filldatatablevalue(PM, "Eg45AChallan", dt, null);
            return dt;
        }


        public void Fillddl(DropDownList ddlbankbranch) // fill department Droddownlist
        {
            gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[1];

            gf.FillListControl(ddlbankbranch, "EgGetBanks", "BankName", "BSRCode", null);
            ddlbankbranch.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        }

        public DataTable GetData()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@bankbranchcode", SqlDbType.Char, 7) { Value = BsrCode };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[2] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = Todate };
            dt = gf.Filldatatablevalue(PM, "EgChallanChecklistReport", dt, null);
            return dt;
        }
        public DataTable E45AChallanHeadwise()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@toDate", SqlDbType.DateTime) { Value = Todate };
            PM[2] = new SqlParameter("@Budgethead", SqlDbType.Char, 13) { Value = Budgethead };

            dt = gf.Filldatatablevalue(PM, "Eg45AChallanheadwise", dt, null);
            return dt;
        }

        //public DataTable EgTy12report()
        //{
        //    gf = new GenralFunction();
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime);
        //    PM[0].Value = Fromdate;
        //    PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime);
        //    PM[1].Value = Todate;
        //    SqlDataReader dr = gf.FillDataReader(PM, "EGty12");
        //    if (dr.HasRows != false)
        //    {
        //        dt.Load(dr);
        //        //dr.Dispose();
        //    }
        //    dr.Dispose();
        //    dr.Close();
        //    return dt;
        //}

        public DataTable  BindTy12DetailGrid()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@Todate", SqlDbType.DateTime) { Value = Todate };
            return gf.Filldatatablevalue(PM, "EgTy12Detail", dt, null);
           
        }

        //public void GetUnDefacedGRNs(GridView grd)
        //{
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[3];
        //    PM[0] = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
        //    PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
        //    PM[2] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = Todate };
        //    gf.FillGridViewControl(grd, PM, "EgGetUnDefacedGRNs");
        //}

        /// <summary>
        /// Changed For JSON 3 
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        public string GetUnDefacedGRNs()
        {
            gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            PM[1] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[2] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = Todate };
           
            gf.Filldatatablevalue(PM, "EgGetUnDefacedGRNs", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }

        public DataTable  GetCover()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = Todate };
            return  gf.Filldatatablevalue(PM, "EgCoveringLetter", dt, null);
           
        }

        public DataTable GetPendingDefaceGRNs()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@officeid", SqlDbType.Int) { Value = officeid };
            PM[1] = new SqlParameter("@userid", SqlDbType.Int) { Value = userid };
            PM[2] = new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = Fromdate };
            PM[3] = new SqlParameter("@TODate", SqlDbType.DateTime) { Value = Todate };
          
            return gf.Filldatatablevalue(PM, "EgGetPendingDefaceGRNs",dt,null);
        }
        /// <summary>
        /// To shift the LOR and LOP to Rajkosh ,for the AG to download all the data from one place.
        /// start on sep 2015
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// To shift the LOR and LOP to Rajkosh ,for the AG to download all the data from one place.
        /// start on sep 2015
        /// </summary>
        /// <returns></returns>
        public int LORTransfer()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@month", SqlDbType.Int) { Value = month };
            PM[1] = new SqlParameter("@year", SqlDbType.Int) { Value = year };
            //PM[2] = new SqlParameter("@Type", SqlDbType.TinyInt) { Value = Type };
            result = gf.UpdateData(PM, "EgrasLORtransfer");
            return result;
        }






        public int LOPTransfer()
        {
            int result = 0;
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@fromdate", SqlDbType.SmallDateTime) { Value = Fromdate };
            PM[1] = new SqlParameter("@todate", SqlDbType.SmallDateTime) { Value = Todate };
            result = gf.UpdateData(PM, "EgrasLOPtransfer");
            return result;
        }

        #endregion
    }
}
