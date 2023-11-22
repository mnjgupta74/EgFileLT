namespace EgBL
{
    public class BanksEncryptionDecryptionBL
    {
        public  string GetEncryptedString(string PlainText, string KeyName, string Version = null)
        {
            if (KeyName == "BwmHPemeQsQhpwEGWmyQtQ==")
            {
                ePayEncryptionDecryptionBL objEncry = new ePayEncryptionDecryptionBL();
                return objEncry.Encrypt(PlainText, KeyName, 128);
            }
            else
            {
                return SelectEncryption(KeyName, PlainText, Version);
            }
        }
        public  string GetDecryptedString(string EncData, string KeyName, string Version = null)
        {
            if (KeyName == "BwmHPemeQsQhpwEGWmyQtQ==")
            {
                ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
                return objEpay.Decrypt(EncData, KeyName, 128);
            }
            else
            {
                return SelectDecryption(KeyName, EncData, Version);
            }
        }

        private  string SelectEncryption(string KeyName, string PlainText, string Version)
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            switch (Version)
            {
                case "2.0":
                    return objEncry.EncryptSBIWithKey256(PlainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + KeyName + ".key");
                default:
                    return objEncry.Encrypt(PlainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + KeyName + ".key");
            }
        }
        private  string SelectDecryption(string KeyName, string EncData, string Version)
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            switch (Version)
            {
                case "2.0":
                    return objEncry.DecryptSBIWithKey256(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + KeyName + ".key", null);
                default:
                    return objEncry.Decrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + KeyName + ".key");
            }
        }
    }
}
