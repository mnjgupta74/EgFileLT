using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.Threading.Tasks;

namespace EgBL
{
    public class AppEncryptionDecryption
    {
        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
       // public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
       // {
       //     byte[] encryptedBytes = null;

       //     // Set your salt here, change it to meet your flavor:
       //     // The salt bytes must be at least 8 bytes.
       //     byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

       //     using (MemoryStream ms = new MemoryStream())
       //     {
       //         using (RijndaelManaged AES = new RijndaelManaged())
       //         {
       //             AES.KeySize = 256;
       //             AES.BlockSize = 128;

       //             var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
       //             AES.Key = key.GetBytes(AES.KeySize / 8);
       //             AES.IV = key.GetBytes(AES.BlockSize / 8);

       //             AES.Mode = CipherMode.CBC;

       //             using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
       //             {
       //                 cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
       //                 cs.Close();
       //             }
       //             encryptedBytes = ms.ToArray();
       //         }
       //     }

       //     return encryptedBytes;
       // }
       // public byte[] AES_Decrypt1(byte[] bytesToBeDecrypted, byte[] passwordBytes)
       // {
       //     byte[] decryptedBytes = null;

       //     // Set your salt here, change it to meet your flavor:
       //     // The salt bytes must be at least 8 bytes.
       //     byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

       //     using (MemoryStream ms = new MemoryStream())
       //     {
       //         using (RijndaelManaged AES = new RijndaelManaged())
       //         {
       //             AES.KeySize = 256;
       //             AES.BlockSize = 128;

       //             var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
       //             AES.Key = key.GetBytes(AES.KeySize / 8);
       //             AES.IV = key.GetBytes(AES.BlockSize / 8);

       //             AES.Mode = CipherMode.CBC;

       //             using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
       //             {
       //                 cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
       //                 cs.Close();
       //             }
       //             decryptedBytes = ms.ToArray();
       //         }
       //     }

       //     return decryptedBytes;
        
       //}
       // public string EncryptText(string planTextToEncrypt, string appkey) // Working Method
       // {
       //     string msg = planTextToEncrypt;

       //     RijndaelManaged aes = new RijndaelManaged();

       //     aes.BlockSize = 128;
       //     aes.KeySize = 256;

       //     aes.Mode = CipherMode.ECB;

       //     byte[] keyArr = Convert.FromBase64String(appkey);

       //     byte[] KeyArrBytes32Value = new byte[32];

       //     for (int i = 0; i < 31; i++)
       //     {
       //         KeyArrBytes32Value[i] = 0x20;
       //     }

       //     Array.Copy(keyArr, KeyArrBytes32Value, 32);

       //     aes.Key = KeyArrBytes32Value;

       //     ICryptoTransform encrypto = aes.CreateEncryptor();

       //     byte[] plainTextByte = ASCIIEncoding.UTF8.GetBytes(msg);

       //     byte[] CipherText = encrypto.TransformFinalBlock(plainTextByte, 0, plainTextByte.Length);
       //     return Convert.ToBase64String(CipherText);

       // }
       // public string DecryptText(string stringtoDecrypt, string appkey) // Working Method
       // {
       //     string key = appkey;
       //     byte[] dataToDecrypt = Convert.FromBase64String(stringtoDecrypt);
       //     var keyBytes = Convert.FromBase64String(key);
       //     AesManaged tdes = new AesManaged();
       //     tdes.KeySize = 256;
       //     tdes.BlockSize = 128;
       //     byte[] KeyArrBytes32Value = new byte[32];

       //     for (int i = 0; i < 31; i++)
       //     {
       //         KeyArrBytes32Value[i] = 0x20;
       //     }

       //     Array.Copy(keyBytes, KeyArrBytes32Value, 32);

       //     tdes.Key = KeyArrBytes32Value;
       //     //tdes.Key = keyBytes;
       //     tdes.Mode = CipherMode.ECB;
       //     //tdes.Padding = PaddingMode.PKCS7;
       //     ICryptoTransform decrypt__1 = tdes.CreateDecryptor();
       //     byte[] deCipher = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
       //     tdes.Clear();
       //     return Convert.ToBase64String(deCipher);
       // }
    }
}
