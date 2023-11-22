using System;
using System.Data;
using System.Data.SqlClient;

namespace EgBL
{
    public class TempServiceForBankUploadInfoBL
    {
        public string BSRCode { get; set; }
        public Int64 Grn { get; set; }
        public double Amount { get; set; }
        public string PaymentMode { get; set; }
        public string cipherText { get; set; }

        GenralFunction gf = new GenralFunction();
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
       
        public DataTable GetGrnForBankUploadInfo()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@bsrcode", SqlDbType.Char,7) { Value = BSRCode };
            dt = gf.Filldatatablevalue(PARM, "EgTempBankUploadInfo", dt, null);
            return dt;
        }
        public string InsertdataIntoBankUploadInfo()
        {
            DataTable dt = new DataTable();
            SqlParameter[] PARM = new SqlParameter[1];
            PARM[0] = new SqlParameter("@bsrcode", SqlDbType.Char, 7) { Value = BSRCode };
            int Rv = gf.UpdateData(PARM, "EgTempBankUploadInfo");
            return Rv.ToString();
        }
    }
}
