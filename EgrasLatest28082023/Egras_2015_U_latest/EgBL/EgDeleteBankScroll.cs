using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
{
    public class EgDeleteBankScroll
    {
        GenralFunction gf = new GenralFunction();
        #region Properties
        public string BSRcode { get; set; }
        public DateTime Date { get; set; }
        public string PaymentType { get; set; }
        #endregion
        #region Method
        /// <summary>
        /// Get Scroll Data 
        /// </summary>
        /// <param name="grd"> Gridview</param>
        public DataTable FillDeleteBankScrollGrid()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BSRcode };
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PM[2] = new SqlParameter("@PaymentType", SqlDbType.Char,1) { Value = PaymentType };
            return gf.Filldatatablevalue(PM, "EgDeleteBankScroll", dt, null);
           
        }
        /// <summary>
        /// Delete Scrolldata
        /// </summary>
        /// <param name="grd"> int </param>
        public int DeleteBankScrollData()
        {
            int Result = 0;
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BsrCode", SqlDbType.Char, 7) { Value = BSRcode };
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            PM[2] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
            Result = gf.UpdateData(PM, "EgDeleteBankScrollData");
            return Result;
        }
        #endregion
    }
}
