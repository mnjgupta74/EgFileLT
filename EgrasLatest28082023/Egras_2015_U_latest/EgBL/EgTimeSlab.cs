using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DL; 

namespace EgBL
{
    public class EgTimeSlab
    {
        #region "Properties"
        public string _UserName, _Password;
        public int _Slab;
        public DateTime _RequestedTime;
        public string flag { get; set; }
        public Int64 GRN { get; set; }
        
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public int Slab
        {
            get { return _Slab; }
            set { _Slab = value; }
        }
        public DateTime RequestedTime
        {
            get { return _RequestedTime; }
            set { _RequestedTime = value; }
        }
        #endregion
        #region "Function"
        public string CheckData()
        {
            SqlParameter[] PARM = new SqlParameter[3];
            PARM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserName };
            PARM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            PARM[2] = new SqlParameter("@BSRCode", SqlDbType.Char, 7) { Value = 0 };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetWeblogin", PARM));
            return result;
        }
        public string UpdateSlab()
        {
            SqlParameter[] PM = new SqlParameter[4];
            PM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserName };
            PM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            PM[2] = new SqlParameter("@Slab", SqlDbType.Int) { Value = Slab };
            PM[3] = new SqlParameter("@RequestedTime", SqlDbType.DateTime) { Value = RequestedTime };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetAuditData", PM));
            return result;
        }
        public string Encrypt(string textToEncrypt, string FilePath)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = GetFileBytes(FilePath);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
        public string Decrypt(string textToDecrypt, string FilePath)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = GetFileBytes(FilePath);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
        
        public string GetManualChallan()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@UserID", SqlDbType.VarChar, 20) { Value = UserName };
            PM[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = Password };
            PM[2] = new SqlParameter("@RequestedTime", SqlDbType.DateTime) { Value = RequestedTime };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "GetCtdDetailManual", PM));
            return result;
        }
        public byte[] GetFileBytes(String filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                int count;
                int sum = 0;
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public string GetSingleGRN()
        {
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = GRN };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgGetSingleGrn", PM));
            return result;
        }

        public string CTDOnlineDataForReconcile()
        {
            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@Slab", SqlDbType.Int) { Value = Slab };
            PM[1] = new SqlParameter("@RequestedTime", SqlDbType.DateTime) { Value = RequestedTime };
            PM[2] = new SqlParameter("@flag", SqlDbType.Char, 1) { Value = flag };
            string result = Convert.ToString(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgCTDOnlineDataForReconcile", PM));
            return result;
        }
        public DataTable CTDRecordForReconcile()
        {
            GenralFunction gf = new GenralFunction();
            DataTable dt = new DataTable();
            SqlParameter[] PM = new SqlParameter[1];
            PM[0] = new SqlParameter("@RequestedTime", SqlDbType.DateTime) { Value = RequestedTime };
            return gf.Filldatatablevalue(PM, "EgCTDRecordForReconcile", dt, null);
        }


        #endregion
    }
}

          
