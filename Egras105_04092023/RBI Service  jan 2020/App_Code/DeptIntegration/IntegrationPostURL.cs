using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for IntegrationFlashURL
/// </summary>
public class IntegrationPostURL
{
    public IntegrationPostURL()
    {
    }

    public void PushData(string merchantCode , string encData)
    {

        if (merchantCode == "5009")
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //string cipherText = "{\"encdata\":\" mz8AuEobq2rahJj6kiCQFlYH1+g8ASDp1q9xYwAclz59ur8DIa4fsZr/EUjCB3LjT3fZvBUsYSVBrAMIhavvMajVCKABRYr7tbvLlrivmQFJJJ8kc3stUGh4vzNd6k/1odEUkBn9+gjaGSREVChdMcCoj9Ot+yk9pe+34iRiyMD8pWH4xFtd5qZihmjAeTLPb8k3m75taYWId9rU5F7+HX/c6MPaaUr4EEwYDus+aM0= \", \"merchnat_code\":\"5006\"}";
                string cipherText = "{\"encdata\":\" " + encData + " \", \"merchnat_code\":\"" + merchantCode + "\"}";
                byte[] byteArray = Encoding.UTF8.GetBytes(cipherText);


                WebRequest request = WebRequest.Create(ConfigurationManager.AppSettings["IntegrationURL"].ToString());
                request.Method = "POST";
                request.ContentLength = byteArray.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                //IpAddress = Dns.GetHostByName("sbiepay.sbi").AddressList[0].ToString();
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(data, encode);
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                string returnData = "";
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    returnData = returnData + "" + str;
                    count = readStream.Read(read, 0, 256);
                }
                response.Close();
                EgIntegrationPostURLBL objLog = new EgIntegrationPostURLBL();
                objLog.MerchantCode = merchantCode;
                objLog.StatusLog = ((System.Net.HttpWebResponse)response).StatusDescription;
                objLog.EncData = encData;
                objLog.InsertPushLog();

            }
            catch { }
        }
    }

}