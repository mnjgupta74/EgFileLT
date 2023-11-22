﻿using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgBankSoftCopyUploadBL
    {
        GenralFunction gf = new GenralFunction();

        #region Properties
        public Int64 GRN { get; set; }
        public string BSRCode { get; set; }//Priyanka Sharma
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public string ScrollType { get; set; }
        #endregion

        #region Function
        // for checking duplicate scroll
        public string CheckExistScroll()
        {
            int result = 0;
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BSRcode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@BankDate", SqlDbType.DateTime);
            PM[1].Value = Date;
            PM[2] = new SqlParameter("@PaymentType", SqlDbType.Char, 1);
            PM[2].Value = PaymentType;
            string Status = gf.ExecuteScaler(PM, "EgCheckExistScroll");

            return Status;
        }
        /// <summary>
        ///  After Upload Scroll Get MismatchData 
        /// </summary>
        /// <returns>datatable</returns>
        //public DataTable GetBankScroll()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
        //    PM[0].Value = BSRCode;
        //    PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
        //    PM[1].Value = Date;

        //    dt = gf.Filldatatablevalue(PM, "EgBankScrollUpdate", dt, null);
        //    return dt;
        //}




        /// <summary>
        /// Get Mismatch Records for Manual
        /// </summary>
        /// <returns></returns>
        public DataTable GetBankScrollFromPreBankUploadInfo_Manual()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
            PM[1].Value = Date;

            dt = gf.Filldatatablevalue(PM, "EgPreBankScrollUpdate_Manual", dt, null);
            return dt;
        }
        /// <summary>
        /// Get Mismatch Records
        /// </summary>
        /// <returns></returns>
        public DataTable GetBankScrollFromPreBankUploadInfo()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
            PM[1].Value = Date;

            dt = gf.Filldatatablevalue(PM, "EgPreBankScrollUpdate", dt, null);
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
        //public DataTable GetScrollMismatchRecords()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
        //    PM[1] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
        //    return gf.Filldatatablevalue(PM, "EgBankScrollInfo", dt, null);
        //}
        //public void FillBank(DropDownList ddl)
        //{
        //    SqlParameter[] PM = new SqlParameter[0];
        //    gf.FillListControl(ddl, "EgFillBank", "BankName", "BSRCode", PM);
        //}

        //public DataTable GetSoftCopyDetail()
        //{
        //    DataTable dt = new DataTable();
        //    SqlParameter[] PM = new SqlParameter[2];
        //    PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
        //    PM[0].Value = BSRCode;
        //    PM[1] = new SqlParameter("@Date", SqlDbType.DateTime);
        //    PM[1].Value = Date;
        //    return gf.Filldatatablevalue(PM, "EgBankScroll", dt, null);
        //}

        //public DataTable FillXml()
        //{
        //    DataTable dt = new DataTable();
        //    return gf.Filldatatablevalue(null, "EgFillXml", dt, null);
        //}

        //public DataTable GetBankManualData()
        //{
        //    SqlParameter[] PM = new SqlParameter[1];

        //    PM[0] = new SqlParameter("@Grn", SqlDbType.Int) { Value = GRN };
        //    DataTable dt = new DataTable();
        //    return gf.Filldatatablevalue(PM, "EgGetBankManualData", dt, null);
        //}

        #endregion

        #region Validate Function Commented
        //public System.Boolean IsNumeric(System.Object Expression, int size, char type)  // function for numeric validation
        //{

        //    if (Expression == null || Expression is DateTime)
        //        return false;


        //    try
        //    {

        //        if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
        //            return true;


        //        if (type.Equals('O') && Expression.ToString() == "")  // check weather it is optional or mandatory , if optional then return true;
        //            return true;

        //        if (Expression is string)
        //            Double.Parse(Expression as string);
        //        else
        //            Double.Parse(Expression.ToString());

        //        if (Expression.ToString().Length > size)
        //            return false;


        //        return true;
        //    }
        //    catch { } // just dismiss errors but return false

        //    return false;
        //}
        //public System.Boolean IsChar(System.Object Expression, int size, char type)  // function for Char validation
        //{

        //    if (Expression == null || Expression is DateTime)
        //        return false;


        //    try
        //    {
        //        if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
        //            return true;

        //        if (Expression.ToString().Length > size)
        //            return false;

        //        if (Expression is string)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch { } // just dismiss errors but return false

        //    return false;
        //}
        //public System.Boolean IsDate(System.Object Expression, char type)
        //{

        //    if (Expression == null)
        //        return false;


        //    try
        //    {
        //        if (type.Equals('M') && Expression.ToString() == "")  // check weather it is optional or mandatory , if mandatory then return false;
        //            return false;
        //        DateTime CDate;
        //        CDate = Convert.ToDateTime(Expression);
        //        return true;

        //    }
        //    catch { } // just dismiss errors but return false

        //    return false;

        //}
        #endregion

        //added on 26 feb 2015 for BankScroll Insert
        public int PreUpdateAndInsertBankStatus(DataTable dt)
        {
            try
            {
                SqlParameter[] PM = new SqlParameter[3];
                PM[0] = new SqlParameter("@mytable", SqlDbType.Structured);
                PM[0].Value = dt;
                PM[1] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
                PM[1].Value = GetBSRCode(); //BSRCode;
                PM[2] = new SqlParameter("@PaymentType", SqlDbType.Char, 1);
                PM[2].Value = PaymentType;
                //gf.UpdateData(PM, "EgUpdateAndInsertBankStatus");
                return gf.UpdateData(PM, "EgPreUpdateAndInsertBankStatus");
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        //public void FinalUpdateAndInsertBankStatus(SqlTransaction trans)
        //{
        //    try
        //    {
        //        SqlParameter[] PM = new SqlParameter[4];
        //        PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
        //        PM[0].Value = BSRCode;
        //        PM[1] = new SqlParameter("@scrolltype", SqlDbType.Char, 1);
        //        PM[1].Value = ScrollType;
        //        PM[2] = new SqlParameter("@Date", SqlDbType.DateTime);
        //        PM[2].Value = Date;
        //        PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char,7);
        //        PM[3].Value = PaymentType;
        //        gf.UpdateData(PM, "EgFinalUpdateAndInsertBankStatus");
        //    }
        //    catch (Exception ex).
        //    {
        //        // Trans.Rollback();
        //    }
        //}
        public string FinalUpdateAndInsertBankStatus()        //  Update By Priyanka  4 april 2019  change UpdateDta  to Execute Scalre method
        {
            try
            {

                SqlParameter[] PM = new SqlParameter[4];
                PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
                PM[0].Value = BSRCode;
                PM[1] = new SqlParameter("@scrolltype", SqlDbType.Char, 1);
                PM[1].Value = ScrollType;
                PM[2] = new SqlParameter("@Date", SqlDbType.DateTime);
                PM[2].Value = Date;
                PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 7);
                PM[3].Value = PaymentType;
                return gf.ExecuteScaler(PM, "EgFinalUpdateAndInsertBankStatus");
                //gf.UpdateData(PM, "");
            }
            catch (Exception ex)
            {
                return "";
                // Trans.Rollback();
            }
        }


        // Check Signature Flag On ? off For Particular Bank  9 july 2020
        public string CheckSignFlag()
        {
            string flag = string.Empty;
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7);
            PM[0].Value = BSRCode;
            return gf.ExecuteScaler(PM, "EgCheckSignFlag"); ;
        }

        // cHECK  xML  UPLOAD bEFORE pDF 6 APRL 2022

        // cHECK xML uPLOAD cONDITION 
        public string CheckDMSExistance(int year, short month, string bankcode)
        {

            SqlParameter[] PM = new SqlParameter[3];
            GenralFunction gf = new GenralFunction();
            PM[0] = new SqlParameter("@BSRcode", SqlDbType.Char, 7);
            PM[0].Value = bankcode;
            PM[1] = new SqlParameter("@Year", SqlDbType.Int);
            PM[1].Value = year;
            PM[2] = new SqlParameter("@Month", SqlDbType.Int);
            PM[2].Value = month;
            string Status = gf.ExecuteScaler(PM, "CheckDMSExistance");

            return Status;
        }
    }
}
