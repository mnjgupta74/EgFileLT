using EgBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for BankForward
/// </summary>
public class BankForward
{


    //public void GRNBankForward(string PaymentBank)
    public void GRNBankForward(string PaymentBank)
    {
       

            EgEChallanBL objEgEChallan = new EgEChallanBL();
            DataTable bankForwardData = new DataTable();

                objEgEChallan.GRNNumber = Int64.Parse(HttpContext.Current.Session["GrnNumber"].ToString());
                objEgEChallan.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());

                bankForwardData = objEgEChallan.GetBankForwardDetail();
          
           
            if (bankForwardData.Rows.Count > 0)
            {
                Banks objForward = Banks.SelectBanks(PaymentBank);
                objForward.GRN = Int64.Parse(bankForwardData.Rows[0]["GRN"].ToString());
                objForward.RemitterName = bankForwardData.Rows[0]["RemitterName"].ToString();
                objForward.TotalAmount = Convert.ToDouble(bankForwardData.Rows[0]["Head_Amount"].ToString());
                objForward.PaymentMode = bankForwardData.Rows[0]["payMode"].ToString();
                objForward.TIN = bankForwardData.Rows[0]["tin"].ToString() == "" ? (bankForwardData.Rows[0]["UserId"].ToString() == "73") ? "G" + bankForwardData.Rows[0]["GRN"].ToString() : bankForwardData.Rows[0]["UserId"].ToString() : bankForwardData.Rows[0]["tin"].ToString();
                string[] Head_Name1 = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] Head_Amount1 = new string[] { "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00" };

                Head_Name1[0] = bankForwardData.Rows[0]["BSRCode"].ToString() != "9910001" ? bankForwardData.Rows[0]["Head_Name"].ToString() : bankForwardData.Rows[0]["Head_Name"].ToString().Substring(0, 4);
                Head_Amount1[0] = System.Decimal.Round(Convert.ToDecimal(bankForwardData.Rows[0]["Head_Amount"]), 2).ToString();
                objForward.Head_Name = Head_Name1;
                objForward.Head_Amount = Head_Amount1;
                objForward.LocationCode = bankForwardData.Rows[0]["LocationCode"].ToString();
                objForward.filler = "A";
                objForward.merchantCode = bankForwardData.Rows[0]["merchantCode"].ToString();
                objForward.URL = bankForwardData.Rows[0]["URL"].ToString();
                objForward.BsrCode = bankForwardData.Rows[0]["BSRCode"].ToString();
                Dictionary<string, string> GetforwardDictionary = objForward.GetBankForwardString();

                //Bank Forward Log 13 July 2021

                EgBankForwarBL objBankforward = new EgBankForwarBL();
                objBankforward.Grn = objForward.GRN;
                string encdata = objForward.BsrCode == "9910001" ? "hash" : "encdata";
                objBankforward.encdata = GetforwardDictionary[encdata];
                objBankforward.BankCode = objForward.BsrCode;
                objBankforward.flag = "F";
                int ret = objBankforward.InsertAudit();// flag=F (forward)

                if (objForward.GRN != Convert.ToInt64(HttpContext.Current.Session["GrnNumber"]) || PaymentBank != objForward.BsrCode)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("ErrorBankForward" + '-' + Convert.ToString(objForward.GRN) + '-' + Convert.ToString(HttpContext.Current.Session["GrnNumber"]) + '-' + Convert.ToString(objForward.BsrCode));
                    HttpContext.Current.Response.Redirect("~/ErrorPage.aspx");
                }

                RemoteClass myremotepost = new RemoteClass();
                myremotepost.AddDictionary(GetforwardDictionary);
                string Address = ConfigurationManager.AppSettings["URL"].ToString();
                myremotepost.Url = Address;
                myremotepost.Post();

            }

            else
            {

                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError("BanForwardSpReturnZERO" + '-' + Convert.ToString(HttpContext.Current.Session["GrnNumber"].ToString()));
                HttpContext.Current.Response.Redirect("~/ErrorPage.aspx");
            }
        
       
    }
}


