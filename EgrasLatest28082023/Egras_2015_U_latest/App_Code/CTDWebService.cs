using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using EgBL;


[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class CTDWebService : System.Web.Services.WebService 
{
    EgTimeSlab objEgTimeSlab = new EgTimeSlab();

    public CTDWebService()
    {
    }
    [WebMethod]
    public string UpdateSlab(string encData)
    {
        string result = "";
        try
        {
            //encData = "CtQrIXnncwhtldpOBuPv1TjDOdxv8Lfc/BXAEr95Pu7QkpVznXXgqB34+Vytc5yLr4ua6VcZYlBEZpKYPSkJ4A==";
            string plainText = Decrypt(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = plainText.Split('|');
            string[] arrIndMsg = { };
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');
                lstPlainText.Add(arrIndMsg[0]);
                lstPlainText.Add(arrIndMsg[1]);
            }

            //check user-----------------------------------------
            objEgTimeSlab.UserName = lstPlainText[1].ToString();
            objEgTimeSlab.Password = lstPlainText[3].ToString();
            result = objEgTimeSlab.CheckData();
            //---------------------------------------------------

            if (result.ToString() == "1")//Sucessful Login
            {
                string ReturnSlabValue = "";
                objEgTimeSlab.UserName = lstPlainText[1].ToString();
                objEgTimeSlab.Password = lstPlainText[3].ToString();
                objEgTimeSlab.Slab = int.Parse(lstPlainText[5].ToString());

                string[] dat = lstPlainText[7].ToString().Split('/');
                objEgTimeSlab.RequestedTime = DateTime.Parse(dat[1].ToString().Trim() + "/" + dat[0].ToString().Trim() + "/" + dat[2].ToString().Trim());
                //objEgTimeSlab.RequestedTime = DateTime.Parse(lstPlainText[7].ToString()); //change on 8feb
                ReturnSlabValue = objEgTimeSlab.UpdateSlab();//return CTD requested Result
                result = objEgTimeSlab.Encrypt(ReturnSlabValue, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
            }
        }
        catch (Exception ex)
        {
            result = "0";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
            
        }
        return result;
    }

    [WebMethod]
    public string GetManualChallan(string encData)
    {
        string result = "";
        try
        {
            //encData = "CtQrIXnncwhtldpOBuPv1TjDOdxv8Lfc/BXAEr95Pu7QkpVznXXgqB34+Vytc5yLr4ua6VcZYlBEZpKYPSkJ4A==";
            string plainText = Decrypt(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key"); //" UserName=CTD|Password =Ctd#18|RequestedTime =21/06/2013"; // Decrypt(encData, Server.MapPath("~/WebPages/Key/aes.key"));
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = plainText.Split('|');
            string[] arrIndMsg = { };
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');
                lstPlainText.Add(arrIndMsg[0]);
                lstPlainText.Add(arrIndMsg[1]);
            }

            //check user-----------------------------------------
            objEgTimeSlab.UserName = lstPlainText[1].ToString();
            objEgTimeSlab.Password = lstPlainText[3].ToString();
            result = objEgTimeSlab.CheckData();
            //---------------------------------------------------

            if (result.ToString() == "1")//Sucessful Login
            {
                string ReturnSlabValue = "";
                objEgTimeSlab.UserName = lstPlainText[1].ToString();
                objEgTimeSlab.Password = lstPlainText[3].ToString();
                //objEgTimeSlab.Slab = int.Parse(lstPlainText[5].ToString());

                string[] dat = lstPlainText[5].ToString().Split('/');
                objEgTimeSlab.RequestedTime = DateTime.Parse(dat[1].ToString().Trim() + "/" + dat[0].ToString().Trim() + "/" + dat[2].ToString().Trim());
                //objEgTimeSlab.RequestedTime = DateTime.Parse(lstPlainText[7].ToString()); //change on 8feb
                ReturnSlabValue = objEgTimeSlab.GetManualChallan();//return CTD requested Result
                result = objEgTimeSlab.Encrypt(ReturnSlabValue, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
            }
        }
        catch (Exception ex)
        {
            result = "0";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return result;
    }

    
    //fOR SINGLE grn DETAILS FOR CTD
    [WebMethod]
    public string GetSingleGrnCtd(string encData)
    {
        string result = "";
        try
        {
            //CtQrIXnncwhtldpOBuPv1UyFZMzeVKf7PJWKKWob5cp9hzGxaCfYsNnTP2/btw3U";

            string plainText = Decrypt(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
            List<string> lstPlainText = new List<string>();
            string[] arrMsgs = plainText.Split('|');
            string[] arrIndMsg = { };
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');
                lstPlainText.Add(arrIndMsg[0]);
                lstPlainText.Add(arrIndMsg[1]);
            }

            //check user-----------------------------------------
            objEgTimeSlab.UserName = lstPlainText[1].ToString();
            objEgTimeSlab.Password = lstPlainText[3].ToString();
            result = objEgTimeSlab.CheckData();
            //---------------------------------------------------


            if (result.ToString() == "1")//Sucessful Login
            {
                string ReturnSlabValue = "";
                objEgTimeSlab.GRN =  Int64.Parse(lstPlainText[5].ToString());
                ReturnSlabValue = objEgTimeSlab.GetSingleGRN();//return CTD requested Result
                result = objEgTimeSlab.Encrypt(ReturnSlabValue, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
            }
        }
        catch (Exception ex)
        {
            result = "0";
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }
        return result;
    }

    private string Decrypt(string strQueryString, string filePath)
    {
        return objEgTimeSlab.Decrypt(strQueryString, filePath);
    }
}