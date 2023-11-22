using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace EgBL
{
  public  class EncryptDecryptionBL
    {
        private const string KEY = "@1B2c3D4e5F6g7H8";
        #region Encrypt Function
        public string Encrypt(string stringToEncrypt)
        {
            try
            {
                MACTripleDES des = new MACTripleDES();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
                string encrypted = Convert.ToBase64String(des.ComputeHash(Encoding.UTF8.GetBytes(stringToEncrypt))) + '-' + Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncrypt));
                return HttpUtility.UrlEncode(encrypted);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion
        #region
        public string Decrypt(string stringToDecrypt)
        {
            // byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                MACTripleDES des = new MACTripleDES();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(KEY));
                string decoded = HttpUtility.UrlDecode(stringToDecrypt);
                decoded = decoded.Replace(" ", "+");
                string value = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[1].Replace(" ", "+")));
                string savedHash = Encoding.UTF8.GetString(Convert.FromBase64String(decoded.Split('-')[0].Replace(" ", "+")));
                string calculatedHash = Encoding.UTF8.GetString(des.ComputeHash(Encoding.UTF8.GetBytes(value)));
                if (savedHash != calculatedHash)
                    return null;
                return value;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        public string GetMD5Hash(string name)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] ba = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Add SHA 256 Method 21/12/2021  For SBI Integration
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetSHA256(string name)
        {

            SHA256 SHA256 = new SHA256CryptoServiceProvider();
            byte[] ba = SHA256.ComputeHash(Encoding.Default.GetBytes(name));
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
