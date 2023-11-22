using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProcessSNADeptORBankData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //divRequestexp.Visible = false;
        //divRequest.Visible = false;
        //divResponse.Visible = false;
        //string JSONString = "{\"ParameterStr\":\"ddLKhzOW7kYZ43ymEKUpc+FV6hWx/KQ4eR9ZBinCvCKywWaM6zOf5qtBFXJPz8N7fM3JamrQDQ==\"}";


        //using (WebClient webClient1 = new WebClient())
        //{


        //    var url = "http://localhost:56933/eservice/SnaService.svc/PullData";


        //    webClient1.Headers.Add("Token", "1a52af961e8e0861439dd60cf45803b1330dd9d96d0554ce5c367b49b6e4360d|6000");
        //    //webClient1.Headers.Add("pwd", "7a8fa3c67fcfd3646c9722efcc058c2cfdfe15fe8c045a0275547452f00b23a4");
        //    webClient1.Headers[HttpRequestHeader.ContentType] = "application/json";

        //    // string data1 = JsonConvert.SerializeObject(objbobParent);
        //    var response = webClient1.UploadString(url, "POST", JSONString);
        //    DataTable a1 = JsonConvert.DeserializeObject<DataTable>(response);
        //    //var result = JsonConvert.DeserializeObject<eChallanPushResParent>(response);
        //    // return result.eChallanPaymentInqResp.value;
        //    //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
        //}

    }

    protected void btnCreateToken_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            BanksEncryptionDecryptionBL objEcnDecBL = new BanksEncryptionDecryptionBL();
            SNAMerchantCode obj = new SNAMerchantCode();
            string key = obj.GetKey(txtmerchantcode.Text.Trim());
            string encText = objEcnDecBL.GetEncryptedString(txtusername.Text + "|" + txtpassword.Text, key, "2.0");

            //string JSONString = "{\"parameter\":\"" + encText + "\"}";
            string JSONString = "{\"PassCode\":\"" + encText + "\"}";


            using (WebClient webClient1 = new WebClient())
            {


                //var url = "http://164.100.153.105/egras105/eservice/EgAuthTokenAPI.svc/GetToken";
                //var url = "http://localhost:56933/eservice/EgTokenService.svc/GetToken";
                var url = ConfigurationManager.AppSettings["tokenURL"].ToString();


                //webClient1.Headers.Add("merchantKey", txtmerchantcode.Text.Trim());
                webClient1.Headers.Add("Merchant_code", txtmerchantcode.Text.Trim());
                //webClient1.Headers.Add("pwd", "7a8fa3c67fcfd3646c9722efcc058c2cfdfe15fe8c045a0275547452f00b23a4");
                webClient1.Headers[HttpRequestHeader.ContentType] = "application/json";

                // string data1 = JsonConvert.SerializeObject(objbobParent);
                var response = webClient1.UploadString(url, "POST", JSONString);
                spanResponse.InnerText = response;
                //DataTable a1 = JsonConvert.DeserializeObject<DataTable>(response);
                var result = JsonConvert.DeserializeObject<GetToken>(response);
                var a = JsonConvert.DeserializeObject<GetTokenResult>(result.GetTokenResult);
                var token = a.Token;
                txttoken.Text = token;

                //if (token.Trim().Length > 0)
                //{
                //    divRequestexp.Visible = true;
                //    divRequest.Visible = true;
                //    divResponse.Visible = true;
                //}

                ////string key1 = obj.GetKey(txtmerchantcode.Text.Trim());
                ////string encText1 = objEcnDecBL.GetEncryptedString("12327|10.00|844300106000000000|6400|Egras|1128|3800|123456|105|BARB", "6001", "2.0");
                //string encText1 = objEcnDecBL.GetEncryptedString(txtString.Text.Trim(), "6001", "2.0");

                //string JSONString1 = "{\"RequestParm\":\"" + encText1 + "\"}";


                //using (WebClient webClient11 = new WebClient())
                //{


                //    //var url = "http://164.100.153.105/egras105/eservice/EgAuthTokenAPI.svc/GetToken";
                //    var url1 = "http://localhost:56933/eservice/EgSNADeptData.svc/PushSnaData";


                //    webClient11.Headers.Add("Token", token + "|6001");
                //    //webClient1.Headers.Add("pwd", "7a8fa3c67fcfd3646c9722efcc058c2cfdfe15fe8c045a0275547452f00b23a4");
                //    webClient11.Headers[HttpRequestHeader.ContentType] = "application/json";

                //    // string data1 = JsonConvert.SerializeObject(objbobParent);
                //    var response1 = webClient11.UploadString(url1, "POST", JSONString1);
                //    spanResponse.InnerText = response1;
                //    //DataTable a1 = JsonConvert.DeserializeObject<DataTable>(response);
                //    //var result1 = JsonConvert.DeserializeObject<GetToken>(response);
                //    //var token1 = result.Token;
                //    //txttoken.Text = token;
                //    // return result.eChallanPaymentInqResp.value;
                //    //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
                //}
                //// return result.eChallanPaymentInqResp.value;
                //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
            }
        }
    }

    protected void btnGetResponse_Click(object sender, EventArgs e)
    {
        BanksEncryptionDecryptionBL objEcnDecBL = new BanksEncryptionDecryptionBL();
        SNAMerchantCode obj = new SNAMerchantCode();
        //string key1 = obj.GetKey(txtmerchantcode.Text.Trim());
        //string encText1 = objEcnDecBL.GetEncryptedString("12327|10.00|844300106000000000|6400|Egras|1128|3800|123456|105|BARB", "6001", "2.0");
        SNAMerchantCode objsna = new SNAMerchantCode();
        var key = objsna.GetKey(txtmerchantcode.Text.Trim());
        string encText1 = objEcnDecBL.GetEncryptedString(txtString.Text.Trim(), key, "2.0");

        string JSONString1 = "{\"RequestParm\":\"" + encText1 + "\"}";


        using (WebClient webClient11 = new WebClient())
        {


            //var url = "http://164.100.153.105/egras105/eservice/EgAuthTokenAPI.svc/GetToken";
            //var url1 = rbl.SelectedValue == "1" ? "http://localhost:56933/eservice/EgSNADeptData.svc/PushSnaData" : "http://localhost:56933/eservice/EgSNABankDataPush.svc/PushSnaData";
            var url1 = rbl.SelectedValue == "1" ? ConfigurationManager.AppSettings["SNADeptURL"].ToString() : ConfigurationManager.AppSettings["SNABankURL"].ToString();


            //webClient11.Headers.Add("Token", txttoken.Text.Trim() + "|6001");
            webClient11.Headers.Add("Token", txttoken.Text.Trim() + "|" + txtmerchantcode.Text.Trim());
            //webClient1.Headers.Add("pwd", "7a8fa3c67fcfd3646c9722efcc058c2cfdfe15fe8c045a0275547452f00b23a4");
            webClient11.Headers[HttpRequestHeader.ContentType] = "application/json";

            // string data1 = JsonConvert.SerializeObject(objbobParent);
            var response1 = webClient11.UploadString(url1, "POST", JSONString1);
            spanResponse.InnerText = response1;
            //DataTable a1 = JsonConvert.DeserializeObject<DataTable>(response);
            //var result1 = JsonConvert.DeserializeObject<GetToken>(response);
            //var token1 = result.Token;
            //txttoken.Text = token;
            // return result.eChallanPaymentInqResp.value;
            //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
        }
        // return result.eChallanPaymentInqResp.value;
    }
    protected void rblS_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtString.Text = string.Empty;
    }
}

class GetToken
{
    public string GetTokenResult { get; set; }
    //public string StatusCode { get; set; }
    //public string Status { get; set; }
    //public string Message { get; set; }
    //public string Token { get; set; }
}
class GetTokenResult
{
    public string StatusCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public string Token { get; set; }
}