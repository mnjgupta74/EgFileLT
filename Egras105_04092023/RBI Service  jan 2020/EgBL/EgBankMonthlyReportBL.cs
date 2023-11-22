using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DL;
using EgDAL;
using System.IO;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgBankMonthlyReportBL
    {
        GenralFunction gf = new GenralFunction();
        #region Properties
        public Int64 GRN { get; set; }
        public string BSRCode { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string PaymentType { get; set; }
        #endregion
        #region Function
        /// <summary>
        /// Check DMS File Exist and not 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>2,0</returns>
        public int CheckDMSexistScroll(DataTable dt)
        {
            int result = 0;
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@mytable", SqlDbType.Structured);
            PM[0].Value = dt;
            SqlDataReader dr = gf.FillDataReader(PM, "EgCheckDMSexistScroll");
            if (dr.HasRows)
            {
                dr.Read();
                result = int.Parse(dr[0].ToString().Trim());
               
            }
            dr.Close();
            dr.Dispose();
            return result;
        }
        /// <summary>
        /// Insert DMS Data after Check DMS File Exist and not
        /// </summary>
        /// <param name="dt"></param>
        public void InsertDMSdata(DataTable dt)
        {
            SqlParameter[] PM = new SqlParameter[1];

            PM[0] = new SqlParameter("@mytable", SqlDbType.Structured);
            PM[0].Value = dt;
            gf.UpdateData(PM, "EgInsertDMSdata");
        }
        /// <summary>
        /// MissMatch DMS Data
        /// </summary>
        /// <returns></returns>
        //public DataTable GetMissMatchDMSdata()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
        //    PM[0].Value = BSRCode;
        //    PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
        //    PM[1].Value = Date;
        //    dt = gf.Filldatatablevalue(PM, "EgDMSmissmatchRecord", dt, null);
        //    return dt;
        //}
        //public DataTable BindScollRepeater()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@Month", SqlDbType.Int) { Value = Month };
        //    PM[1] = new SqlParameter("@Year", SqlDbType.Int) { Value = Year };
        //    PM[2] = new SqlParameter("@BSRcode", SqlDbType.Char, 7) { Value = BSRCode };
        //    PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
        //    return gf.Filldatatablevalue(PM, "EgDMSScroll", dt, null);

        //}
        public string BindScollRepeaterJson()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@Month", SqlDbType.Int) { Value = Month };
            PM[1] = new SqlParameter("@Year", SqlDbType.Int) { Value = Year };
            PM[2] = new SqlParameter("@BSRcode", SqlDbType.Char, 7) { Value = BSRCode };
            PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
            dt = gf.Filldatatablevalue(PM, "EgDMSScroll", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        #endregion

    }
}
