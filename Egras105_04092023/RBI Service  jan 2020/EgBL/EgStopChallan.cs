using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EgBL
{
    public class EgStopChallan
    {
        GenralFunction gf;
        private DataTable dt;
        public Int64 Grn { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string BankCode { get; set; }
        public Int64 UserID { get; set; }
        public string type { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public DateTime BankDate { get; set; }

        public void GetGrnData(GridView grd)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[2];
            PM[0] = new SqlParameter("@Grn", SqlDbType.BigInt) { Value = Grn };
            PM[1] = new SqlParameter("@paymentType", SqlDbType.Char,1) { Value = PaymentType };
            gf.FillGridViewControl(grd, PM, "EgGrnDataForStopChallan");
        } public void GetGRNlistDeActive(GridView grd)
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BankCode };
            PM[1] = new SqlParameter("@fromdate", SqlDbType.DateTime) { Value = fromdate };
            PM[2] = new SqlParameter("@todate", SqlDbType.DateTime) { Value = todate };
            gf.FillGridViewControl(grd, PM, "EgGetDeActivatedGrnList");
        }
        public int InsertSelectedGrnData()
        {
            gf = new GenralFunction();
            SqlParameter[] PM = new SqlParameter[6];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = Grn };
            PM[1] = new SqlParameter("@Amount", SqlDbType.Decimal) { Value = Amount };
            PM[2] = new SqlParameter("@BankCode", SqlDbType.Char,7) { Value = BankCode };
            PM[3] = new SqlParameter("@userid", SqlDbType.BigInt) { Value = UserID };
            PM[4] = new SqlParameter("@type", SqlDbType.Char,1) { Value = type };
            PM[5] = new SqlParameter("@BankChallanDate", SqlDbType.Date) { Value = BankDate };

            int ret =gf.UpdateData(PM, "EgInsertGrnToStopChallan");
            return ret;
        }
        
    }
}
