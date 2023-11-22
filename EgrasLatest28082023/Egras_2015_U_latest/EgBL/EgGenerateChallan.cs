using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace EgBL
{
    public class EgGenerateChallan
    {
        GenralFunction gf;
        DataTable dt;
        #region Properties

        public  Int64 GRN { get; set; }
        public DateTime ChallanDate { get; set; }
        public string BankCode { get; set; }
        public int UserID { get; set; }
        public double Amount { get; set; }
        public int ChallanNo { get; set; }
        public string ChallanList { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public string PaymentType { get; set; }
        public string type { get; set; }
        #endregion

        #region Function
        /// <summary>
        /// Get GRN List Which status is successful and BankScroll 'Y'
        /// </summary>
        /// <param name="grd"></param>
        //public DataTable  GetGRNlistforChallan()
        //{
        //    DataTable dt = new DataTable();
        //    gf = new GenralFunction();
        //    SqlParameter[] PM = new SqlParameter[4];
        //    PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BankCode };
        //    PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
        //    PM[2] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
        //    PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
        //    return gf.Filldatatablevalue(PM, "EgGetGRNList", dt, null);
        

        //}


        /// <summary>
        /// Get GRN List Which status is successful and BankScroll 'Y'
        /// </summary>
        /// <param name="grd"></param>
        public string GetGRNlistforChallan()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[5];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BankCode };
            PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
            PM[2] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
            PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };
            PM[4] = new SqlParameter("@type", SqlDbType.Char, 1) { Value = type };
            DataTable dt = new DataTable();
            dt = gf.Filldatatablevalue(PM, "EgGetGRNList", dt, null);
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dt);
            return JSONString;
        }
        /// <summary>
        /// Generate Single GRN ChallaN No
        /// </summary>
        public void InsertSelectedGrnData()
        {
            gf = new GenralFunction();
            dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PM[1] = new SqlParameter("@ChallanNo", SqlDbType.Int) { Value = ChallanNo };
            PM[1].Direction = ParameterDirection.Output;
            gf.UpdateData(PM, "EgGenerateGrnChallanNo");
            ChallanNo = Convert.ToInt32(PM[1].Value);
        }
        /// <summary>
        /// Generate top 25 GRN Challan NO
        /// </summary>
        public string InsertAllSelectedGrnData()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BankCode };
            PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
            PM[2] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
            PM[3] = new SqlParameter("@paymentType", SqlDbType.Char, 1) { Value = PaymentType };
            int val = gf.UpdateData(PM, "EgGenerateAllGrnChallanNo");
            return val.ToString();

        }
        /// <summary>
        /// Get Generate GRN Challan No for set in Grid Challan Number Column
        /// </summary>
        /// <returns></returns>
        public DataTable GetChallanList()
        {
            DataTable dt = new DataTable();
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BankCode };
            PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
            PM[2] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
            PM[3] = new SqlParameter("@PaymentType", SqlDbType.Char, 1) { Value = PaymentType };         
            return gf.Filldatatablevalue(PM, "EgGetChallanList", dt,null);
          
           
        }


        #endregion
    }
}
