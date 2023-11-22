using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EgBL
{
   public class EgForwardBankBL
    {
       public void ForwardBank(string cipherText, string Merchantcode, string url)
       {
           RemoteClass myremotepost = new RemoteClass();
           System.Configuration.Configuration rootWebConfig1 =
           System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
           System.Configuration.KeyValueConfigurationElement customSetting =
           rootWebConfig1.AppSettings.Settings["URL"];
           //string Address = bankData.Rows[0][0].ToString() ;
           string Address = "https://Egras.rajasthan.gov.in/WebPages/BankForward.aspx";  //"http://localhost:52324/sbi/default.aspx"; //"http://localhost:55863/EgrasWebSite/WebPages/BankForward.aspx";  //"http://localhost:52324/sbi/default.aspx"; //"http://localhost:56933/EgrasWebSite/WebPages/BankForward.aspx"; //  //"http://59.163.35.6/corp/BANKAWAY?Action.UBIShoppingMall.Login.GBASE.Init=Y ";  

           myremotepost.Add("encdata", cipherText.ToString());
           myremotepost.Add("merchant_code", Merchantcode); //bankData.Rows[0][1].ToString());   
           myremotepost.Add("URL", url);

           myremotepost.Url = Address;
           myremotepost.Post();
       }
    }
}
