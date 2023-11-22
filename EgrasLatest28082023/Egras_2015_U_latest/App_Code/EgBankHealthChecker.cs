using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
[AspNetCompatibilityRequirements
    (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EgBankHealthChecker" in code, svc and config file together.
public class EgBankHealthChecker : IEgBankHealthChecker
{
    bool returnVal;
    public int Check()
    {
        //string JSON = "{\"0304017\":true,\"0006326\":true,\"0200113\":true,\"0292861\":true,\"6910213\":true,\"0280429\":false,\"0361193\":false,\"0240539\":false,\"1000132\":true}";
        //JObject json = JObject.Parse(JSON);
        //var ss = json.First;
        //for (int i = 0; i < 4; i++)
        //{
        //    string d = ss.ToString();
        //    ss = ss.Next;
        //}
        string GRNList = "54750171|54750119|54750296|54745957|54750394|54738272|54724040|45095266|54731522|54750874|54672955|54750491|54751050|54751004";
        string AmtList = "100.00|350.00|350.00|200.00|100.00|50.00|10.00|400.00|30.00|300.00|100.00|100.00|100.00|100.00";
       
        string[] grnList = GRNList.Split('|').ToArray();
        string[] amtList = AmtList.Split('|').ToArray();
        Dictionary<string, bool> BankStatus = new Dictionary<string, bool>();


        // Code comment  on 20 Oct 2021 For purpose Only disable message  which Bank service is Down 

        //BankServiceCheck objBankServiceCheck = new global::BankServiceCheck();  
        EgBankServiceStatus objEgBankService = new EgBankServiceStatus();
        for (int i = 0; i < Banks.bankCodeList.Keys.Count; i++)
        {
            try
            {
                VerifiedBankClass objVerifyBank = new VerifiedBankClass();
                objVerifyBank.BSRCode = Banks.bankCodeList.ElementAt(i).Key;
                objVerifyBank.GRN = Convert.ToInt64(grnList[i]);
                objVerifyBank.TotalAmount = Convert.ToDouble(amtList[i]);
                objVerifyBank.PaymentMode = "N";
                //string msg = objVerifyBank.Verify();
                //  bool returnVal = objBankServiceCheck.Verify();
                string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", objVerifyBank.GRN.ToString(), objVerifyBank.TotalAmount.ToString());
                EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
                string checkSum = objEncryption.GetMD5Hash(plainText);
                Banks objBank = Banks.SelectBanks(objVerifyBank.BSRCode);

                objBank.checkSum = checkSum;
                string cipherText = objBank.GetRequestString(plainText);
               
                    string returnData = objBank.CallVerifyService(cipherText);
                if (returnData == null)
                {
                    returnVal = false;
                    BankStatus.Add(Banks.bankCodeList.ElementAt(i).Key, false);
                }
                else
                    returnVal = true;
               
                
                objEgBankService.BSRCode = Banks.bankCodeList.ElementAt(i).Key;
                objEgBankService.status = returnVal;

                int returnValue = objEgBankService.InsertBankServiceStatus();
                BlockBanks(BankStatus);

            }
        
            catch (Exception ex)
            {
                BankStatus.Add(Banks.bankCodeList.ElementAt(i).Key, false);
                objEgBankService.BSRCode = Banks.bankCodeList.ElementAt(i).Key;
                objEgBankService.status = false;

                int returnValue = objEgBankService.InsertBankServiceStatus();

            }
           
        }
        return 1;
    }
     
    


    private bool BlockBanks(Dictionary<string, bool> BankStatus)
    {
        string JSON = JsonConvert.SerializeObject(BankStatus);
        using (StreamWriter file = File.CreateText(System.Web.HttpContext.Current.Server.MapPath("~/InActiveBanks.json")))
        {
            file.Write(JSON);
            
        }
        return true;
    }
}
