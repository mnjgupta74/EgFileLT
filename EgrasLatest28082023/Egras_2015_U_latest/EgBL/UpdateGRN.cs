using System;
using System.Data;
using System.Data.SqlClient;
using EgBL;
using System.Web;

/// <summary>
/// Summary description for UpdateGRN
/// </summary>
namespace Update
{

    public class UpdateGRN
    {
        public string Status { get; set; }
        public Int64 GRN { get; set; }
        public string CIN { get; set; }
        public string Ref { get; set; }        
        public double Amount { get; set; }
        public DateTime timeStamp { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string BSRCode { get; set; }
        public string Result { get; set; }
        public string encdata { get; set; }
        public DateTime TransDate { get; set; }
        public string URL { get; set; }
        public string IPAddress { get; set; }
        public string epayBSRCode { get; set; }
        public string bankRefNo { get; set; }
        public string payMode { get; set; }
        public string Reason { get; set; }
        public UpdateGRN()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string CheckData()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserID };
            PARM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            PARM[2] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
            string result = Convert.ToString(gf.ExecuteScaler(PARM, "GetWeblogin"));
            return result;
        }

        public string CheckLogin()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserID };
            PARM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            string result = Convert.ToString(gf.ExecuteScaler(PARM, "GetGrnAuthentication"));
            return result;
        }

        public void InsertAudit()
        {
            //IPAddress = HttpContext.Current.Request.UserHostAddress;
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[8];
            PARM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserID };
            PARM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            PARM[2] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = BSRCode };
            PARM[3] = new SqlParameter("@EncData", SqlDbType.NVarChar, 500) { Value = encdata };
            PARM[4] = new SqlParameter("@TransDate", SqlDbType.SmallDateTime, 7) { Value = TransDate };
            PARM[5] = new SqlParameter("@URL", SqlDbType.NVarChar, 200) { Value = URL };
            PARM[6] = new SqlParameter("@IPAddress", SqlDbType.Char, 15) { Value = IPAddress };
            PARM[7] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };

            gf.UpdateData(PARM, "InsertAuditTransaction");
        }

       public int update()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[7];
            PARM[0] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = Status };
            PARM[1] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[2] = new SqlParameter("@CIN", SqlDbType.Char,21) { Value = CIN };
            PARM[3] = new SqlParameter("@Ref", SqlDbType.Char,30 ) { Value = Ref };
            PARM[4] = new SqlParameter("@amount", SqlDbType.Money) { Value = Amount };
            PARM[5] = new SqlParameter("@timeStamp", SqlDbType.DateTime) { Value = timeStamp };
            PARM[6] = new SqlParameter("@bankCode", SqlDbType.Char,7 ) { Value = BSRCode };

            return gf.UpdateData(PARM, "EgUserBankStatus");    
        }
        // Update  Grn  through  Pine Lab   with corporate service 26 nov 2019

        public int updatePineLabStatus()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[4];
            PARM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            PARM[1] = new SqlParameter("@epayBankCode", SqlDbType.VarChar, 10) { Value = epayBSRCode };
            PARM[2] = new SqlParameter("@bankRef", SqlDbType.VarChar, 40) { Value = bankRefNo };
            PARM[3] = new SqlParameter("@PayMode", SqlDbType.VarChar, 10) { Value = payMode };
            return gf.UpdateData(PARM, "EgUpdateEpayStatus");
        }

        public int updateManual()
       {
           GenralFunction gf = new GenralFunction();
           SqlParameter[] PARM = new SqlParameter[7];
           PARM[0] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = Status };
           PARM[1] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
           PARM[2] = new SqlParameter("@CIN", SqlDbType.Char, 21) { Value = CIN };
           PARM[3] = new SqlParameter("@Ref", SqlDbType.Char, 30) { Value = Ref };
           PARM[4] = new SqlParameter("@amount", SqlDbType.Money) { Value = Amount };
           PARM[5] = new SqlParameter("@timeStamp", SqlDbType.DateTime) { Value = timeStamp };
           PARM[6] = new SqlParameter("@bankCode", SqlDbType.Char, 7) { Value = BSRCode };

           return gf.UpdateData(PARM, "EguserBankStatusManual");
       }

       public int CheckUserBankStatus()
       {
           GenralFunction gf = new GenralFunction();
           SqlParameter[] PARM = new SqlParameter[4];
           PARM[0] = new SqlParameter("@Status", SqlDbType.Char, 1) { Value = Status };
           PARM[1] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
           PARM[2] = new SqlParameter("@amount", SqlDbType.Money) { Value = Amount };
           PARM[3] = new SqlParameter("@bankCode", SqlDbType.Char, 7) { Value = BSRCode };

           return Convert.ToInt32(gf.ExecuteScaler(PARM, "EgCheckUserBankStatusManual"));
       }

        /// <summary>
        /// test bank data
        /// </summary>
        /// <returns></returns>
        public int TestBankStatus()
        {
            GenralFunction gf = new GenralFunction();
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@bankCode", SqlDbType.Char, 8) { Value = BSRCode };
            PARM[1] = new SqlParameter("@encData", SqlDbType.NVarChar, 4000) { Value = encdata };
            PARM[2] = new SqlParameter("@timeStamp", SqlDbType.DateTime) { Value = DateTime.Now };           
            return gf.UpdateData(PARM, "EgTestBankStatus");
        }
    }
}
