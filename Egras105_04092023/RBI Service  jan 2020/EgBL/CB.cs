using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EgBL
{
    public class CB : iBankForward
    {
        private string url;
        private string Merchantcode;
        public  CB(string url ,string Merchantcode)
        {
            this.url = url;
            this.Merchantcode = Merchantcode;
        }

        void iBankForward.BankForward(string plainText)
        {                EgEncryptDecrypt ObjEncrcryptDecrypt = new EgEncryptDecrypt();
                string CheckSum = ObjEncrcryptDecrypt.GetMD5Hash(plainText);
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                plainText = plainText + "|checkSum=" + CheckSum;
                string cipherText = objEncry.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CB.key");
                EgForwardBankBL ObjForward = new EgForwardBankBL();
                ObjForward.ForwardBank(cipherText, Merchantcode,url);
        }
    }
}
