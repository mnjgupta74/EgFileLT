using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class EgFrmBankVerified
    {
        GenralFunction gf = new GenralFunction();

        #region Properties
        public string BankCode { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public int UserId { get; set; }
        public Int64 GRN { get; set; }
        public bool Isvalid { get; set; }
        #endregion

        #region Function
        /// <summary>
        /// test bank data
        /// </summary>
        /// <returns></returns>
        private string GetBSRCode()
        {
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserId"] };
            BankCode = gf.ExecuteScaler(PARM, "EgGetBSRCodebyUserid");
            return BankCode;
        }
        private string GetGRNStatus()
        {
            SqlParameter[] PARM = new SqlParameter[5];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserId"] };
            PARM[2] = new SqlParameter("@UserType", SqlDbType.Int) { Value = System.Web.HttpContext.Current.Session["UserType"] };
            PARM[3] = new SqlParameter("@PaymentMode", SqlDbType.Char, 1) { Value = PaymentType };
            PARM[4] = new SqlParameter("@BankCode", SqlDbType.Char, 7) { Value = BankCode };

            Status = gf.ExecuteScaler(PARM, "EgGetGRNStatus_App");
            return Status;
        }
        public bool GetBankGRNDetails()
        {
            GetBSRCode();
            GetGRNStatus();
            switch (Status.ToUpper())
            {
                case "S":
                    Isvalid = true;
                    return true;
                case "P":
                    Isvalid = true;
                    return false;
                case "F":
                    Isvalid = true;
                    return true;
                default:
                    Isvalid = false;
                    return false;
            }
        }
        #endregion

    }
}
