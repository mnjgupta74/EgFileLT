using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EgDAL;
using DL;

namespace EgBL
{
    public class EgRefundChallanBL
    {

        EgDepartmentBL obj = new EgDepartmentBL();

        #region Class Properties
        /// <summary>
        ///
        /// </summary>

        public Int64 Grn { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int DeptCode { get; set; }    
        public double amount { get; set; }
        public string deface { get; set; }
        public int ScheCode { get; set; }
        public string LoginId { get; set; }
        public int Result { get; set; }
        public int BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public int Sno { get; set; }
        public double defaceAmount { get; set; }
        #endregion

        #region Function

        /// <summary>
        /// Return Record which GRN STATUS Successful and bank flag is y 
        /// </summary>
        /// <param name="grd"> dataset for fill gridview</param>
        public DataTable  BindGrid()
        {
            DataTable dt = new DataTable();
            GenralFunction gf = new GenralFunction();
            if (Grn != 0)
            {
                SqlParameter[] PARM = new SqlParameter[2];
                PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
                PARM[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
                return gf.Filldatatablevalue(PARM, "EgRefundGrn", dt, null);
                
            }
            else
            {
                SqlParameter[] PARM = new SqlParameter[3];
                PARM[0] = new SqlParameter("@Fromdate", SqlDbType.DateTime) { Value = FromDate };
                PARM[1] = new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = ToDate };
                PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
                return gf.Filldatatablevalue(PARM, "EgRefundGrnDateWise", dt, null);
               
            }
        
        }

        /// <summary>
        /// Get Total deface Amount  on GRN
        /// </summary>
        public void GetPartialAmount()
        {
            GenralFunction gf = new GenralFunction();

            SqlParameter[] PARM = new SqlParameter[1];

            PARM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PARM, "EgGetRefundAmount", dt, null);
            if (dt.Rows[0]["Amt"].ToString() != "")
            {
                amount = Convert.ToDouble(dt.Rows[0]["Amt"]);
            }
            else
                amount = 0.0;
        }

        /// <summary>
        /// Insert Refund detail
        /// </summary>
        /// <returns> 1 and 0</returns>
        public int InsertRefundChallan()
        {
            GenralFunction gf = new GenralFunction();

            SqlParameter[] PARM = new SqlParameter[6];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@Deface", SqlDbType.Char, 1) { Value = deface };
            PARM[2] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserId };
            PARM[3] = new SqlParameter("@amount", SqlDbType.Money) { Value = amount };
            PARM[4] = new SqlParameter("@BillNo", SqlDbType.Int) { Value = BillNo };
            PARM[5] = new SqlParameter("@BillDate", SqlDbType.SmallDateTime) { Value = BillDate };
            return gf.UpdateData(PARM, "EgInsertRefundChallan");
        }
        // Add Method for Check Bill No existence In Bill refund  22 May 2019
        public int CheckBillNoExistence()
        {
            GenralFunction gf = new GenralFunction();

            SqlParameter[] PARM = new SqlParameter[2];
            PARM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = Grn };
            PARM[1] = new SqlParameter("@BillNo", SqlDbType.Int) { Value = BillNo };
            return Convert.ToInt16( gf.ExecuteScaler(PARM, "EgBillNoExistenceInRefund"));

        }

        /// <summary>
        /// Create BY manoj Get Grn Detail for Refund Bill Update 5 Feb 20120
        /// </summary>
        /// <returns></returns>

        public DataTable GetDetailsbyGrn()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] pm = new SqlParameter[1];
            DataTable dt = new DataTable();
            pm[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            return gf.Filldatatablevalue(pm, "EgGetRefundGrnBillNo", dt, new string[] { "table1" });
        }


        /// <summary>
        /// update refund bill no Update 5 Feb 2020
        /// </summary>
        /// <returns></returns>
        public int UpdateRefundBillNo()
        {
            //string result = "";
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@grn", SqlDbType.BigInt) { Value = Grn };
            PARM[1] = new SqlParameter("@billno", SqlDbType.Int) { Value = BillNo };
            PARM[2] = new SqlParameter("@BillDate", SqlDbType.DateTime) { Value = BillDate };
            PARM[3] = new SqlParameter("@Sno", SqlDbType.Int) { Value = Sno };
            PARM[4] = new SqlParameter("@Amount", SqlDbType.Money) { Value = defaceAmount };
            //result = gf.ExecuteScaler(PARM, "EgUpdateRefundBillNo");
            //return Convert.ToInt32(result);
            return gf.UpdateData(PARM, "EgUpdateRefundBillNo");

        }

        #endregion
    }
}
