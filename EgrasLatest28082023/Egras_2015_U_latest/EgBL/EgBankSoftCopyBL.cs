using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgBankSoftCopyBL
    {

        GenralFunction gf = new GenralFunction();
        #region Properties
        public int GRN { get; set; }
        public string BSRCode { get; set; }//Priyanka Sharma
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string ScrollType { get; set; }
        #endregion

        #region Function
        // for checking duplicate scroll
        public string CheckExistScroll()
        {
            int result = 0;
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRcode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@BankDate", SqlDbType.DateTime);
            PM[1].Value = Date;
            string Status = gf.ExecuteScaler(PM, "EgCheckExistScroll");

            return Status;
        }
        /// <summary>
        ///  After Upload Scroll Get MismatchData 
        /// </summary>
        /// <returns>datatable</returns>
        public DataTable GetBankScroll()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
            PM[1].Value = Date;

            dt = gf.Filldatatablevalue(PM, "EgBankScrollUpdate", dt, null);
            return dt;
        }
        /// <summary>
        /// Get BSRcode 
        /// </summary>
        /// <returns></returns>
        public string GetBSRCode()
        {
            DataTable dt = new DataTable();
            string BSRCode = "";
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@UserId", SqlDbType.Int);
            PM[0].Value = UserId;
            dt = gf.Filldatatablevalue(PM, "EgGetBSRcode", dt, null);
            if (dt.Rows.Count > 0)
            {
                BSRCode = dt.Rows[0][0].ToString();
            }
            else
            {
                BSRCode = "";
            }
            return BSRCode;
        }
        /// <summary>
        /// Fetch all mismatched records regarding bank scroll
        /// </summary>
        /// <returns></returns>
        public DataTable GetScrollMismatchRecords()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            return gf.Filldatatablevalue(PM, "EgBankScrollInfo", dt, null);
        }
        public void FillBank(DropDownList ddl)
        {
            SqlParameter[] PM = new SqlParameter[0];
            gf.FillListControl(ddl, "EgFillBank", "BankName", "BSRCode", PM);
        }

        public DataTable GetSoftCopyDetail()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
            PM[1].Value = Date;
            return gf.Filldatatablevalue(PM, "EgBankScroll", dt, null);
        }

        public DataTable FillXml()
        {
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(null, "EgFillXml", dt, null);
        }

        public DataTable GetBankManualData()
        {
            SqlParameter[] PM = new SqlParameter[1];

            PM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = GRN };
            DataTable dt = new DataTable();
            return gf.Filldatatablevalue(PM, "EgGetBankManualData", dt, null);
        }

        #endregion

        #region Validate Function



        public System.Boolean IsNumeric(System.Object Expression, int size, char type)  // function for numeric validation
        {

            if (Expression == null || Expression is DateTime)
                return false;


            try
            {

                if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
                    return true;


                if (type.Equals('O') && Expression.ToString() == "")  // check weather it is optional or mandatory , if optional then return true;
                    return true;

                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());

                if (Expression.ToString().Length > size)
                    return false;


                return true;
            }
            catch { } // just dismiss errors but return false

            return false;
        }

        public System.Boolean IsChar(System.Object Expression, int size, char type)  // function for Char validation
        {

            if (Expression == null || Expression is DateTime)
                return false;


            try
            {
                if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
                    return true;

                if (Expression.ToString().Length > size)
                    return false;

                if (Expression is string)
                    return true;
                else
                    return false;
            }
            catch { } // just dismiss errors but return false

            return false;
        }

        public System.Boolean IsDate(System.Object Expression, char type)
        {

            if (Expression == null)
                return false;


            try
            {
                if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
                    return false;
                DateTime CDate;
                CDate = Convert.ToDateTime(Expression);
                return true;

            }
            catch { } // just dismiss errors but return false

            return false;

        }
        #endregion
        //added on 26 feb 2015 for BankScroll Insert

        public void UpdateAndInsertBankStatus(DataTable dt , SqlTransaction trans)
        {
            try
            {
                SqlParameter[] PM = new SqlParameter[2];
                PM[0] = new SqlParameter("@mytable", SqlDbType.Structured);
                PM[0].Value = dt;
                PM[1] = new SqlParameter("@scrolltype", SqlDbType.Char, 1);
                PM[1].Value = ScrollType;
                gf.UpdateData(PM, "EgUpdateAndInsertBankStatus");
            }
            catch (Exception ex)
            {
               // Trans.Rollback();
            }
        }

    }
}
