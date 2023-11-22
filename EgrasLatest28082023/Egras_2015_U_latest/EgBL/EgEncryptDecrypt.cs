using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Web;

namespace EgBL
{
   public class EgEncryptDecrypt
    {
        public string Encrypt(string textToEncrypt)
        {
            textToEncrypt = textToEncrypt + "&rnd=" + HttpContext.Current.Session["RND"].ToString().Trim();
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("Encry123");
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

        public List<string> Decrypt(string textToDecrypt)
        {
            try
            {
                //string FilePath = HttpContext.Current.Server.MapPath("../WebPages/key/encrykey.key");

                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = 0x80;
                rijndaelCipher.BlockSize = 0x80;
                byte[] encryptedData = Convert.FromBase64String(textToDecrypt.Replace(" ","+"));

                byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes("Encry123");

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
                string planText = Encoding.UTF8.GetString(plainText);
                //  value.Substring(index3));
                // string val = planText.Substring((planText.LastIndexOf("=") + 1));
                if (planText.Substring((planText.LastIndexOf("=") + 1)) == HttpContext.Current.Session["RND"].ToString().Trim())
                {
                    string ss = Encoding.UTF8.GetString(plainText);
                    List<string> tempList = new List<string>();

                    string[] arrMsgs = ss.Split('&');
                    string[] arrIndMsg;
                    for (int i = 0; i < arrMsgs.Length; i++)
                    {
                        arrIndMsg = arrMsgs[i].Split('=');

                        tempList.Add(arrIndMsg[0]);
                        tempList.Add(arrIndMsg[1]);
                    }
                    return tempList;

                }
                // return Encoding.UTF8.GetString(plainText);
                else
                    return null;

            }
            catch
            {
                return null;
            }
        }

        public string GetMD5Hash(string name)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] ba = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public string GetSHA256(string name)
        {

            SHA256 sha256 = new SHA256CryptoServiceProvider();// MD5CryptoServiceProvider();
            //byte[] ba = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            byte[] ba = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

}
