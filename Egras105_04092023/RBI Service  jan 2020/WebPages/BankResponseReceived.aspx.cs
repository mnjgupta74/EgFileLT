﻿using System;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Configuration;
using System.Threading;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

public partial class WebPages_BankResponseReceived : System.Web.UI.Page
{
    int version = 1;
    BankResponseReceived objResponse = new BankResponseReceived();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Form.Keys.Count < 2)
            {
                try
                {
                    string RequestString = "";
                    for (int i = 0; i < Request.Form.Keys.Count; i++)
                    {
                        RequestString += Request.Form[i].ToString();
                    }

                    objResponse.BankCode = "";
                    objResponse.CipherText = RequestString;
                    if (Request.UrlReferrer == null)
                        objResponse.URL = "InsufficientData_";
                    else
                        objResponse.URL = "InsufficientData_" + Convert.ToString(Request.UrlReferrer.AbsoluteUri);
                    //objResponse.IPAddress = Request.UserHostAddress.ToString();
                    if (!objResponse.UpdateResponse())
                    {
                        ShowInsufficientData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                    }
                }
                catch (Exception ec)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ec.StackTrace);
                }
            }
            ////////for payu
            else if ( Request.Form["udf1"] == "9910001" )
            {
                try
                {
                    Dictionary<string, string> PayURespond = new Dictionary<string, string>()
                    {
                        { "BankReferenceNo", Request.Form["mihpayid"] },
                        { "payMode", Request.Form["mode"] },
                        { "TRANS_STATUS", Request.Form["status"] },
                        { "key", Request.Form["key"] },
                        { "GRN", Request.Form["txnid"] },
                        { "PAID_AMT", Request.Form["amount"] },
                        { "PAID_DATE", Request.Form["addedon"] },
                        { "BANK_CODE", Request.Form["udf1"] },
                        { "hash", Request.Form["hash"] },
                        { "bankRefNo", Request.Form["bank_ref_num"] },
                        { "PayUBSRCode", Request.Form["bankcode"] },
                        { "reason", Request.Form["error"] },
                        { "CIN", Request.Form["cin"] },
                        { "email", Request.Form["email"] },
                        { "firstname", Request.Form["firstname"] },
                        { "udf1", Request.Form["udf1"] },
                        { "udf2", Request.Form["udf2"] },
                        { "productinfo", Request.Form["productinfo"] }


                    };


                    objResponse.GRNData = PayURespond;
                    if (Request.UrlReferrer == null)
                        objResponse.URL = "No_URL";
                    else
                        objResponse.URL = Convert.ToString(Request.UrlReferrer.AbsoluteUri);
                    if (objResponse.UpdatePayuResponse())
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');", true);
                        ShowData(objResponse);
                    }
                    else
                    {
                        //divReport.Visible = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                    }
                }
                catch
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("last else|" + "PageLoad");
                }


            }
            else if (Request.Form[1] == null || Request.Form[0] == null || Request.Form[1] == "" || Request.Form[0] == "")
            {
                try
                {
                    BankResponseReceived objResponse = new BankResponseReceived();
                    objResponse.BankCode = Convert.ToString(Request.Form[1]);
                    objResponse.CipherText = Convert.ToString(Request.Form[0]);
                    objResponse.URL = "InsufficientData_" + Request.UrlReferrer.AbsoluteUri.ToString();
                    //objResponse.IPAddress = Request.UserHostAddress.ToString();
                    if (!objResponse.UpdateResponse())
                    {
                        ShowInsufficientData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                    }
                }
                catch
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("2nd else IF|" + "PageLoad");
                }
            }
            else
            {
                try
                {
                    bool response = false;
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("BankCode:" + Request.Form[1].ToString());
                    BankResponseReceived objResponse = new BankResponseReceived();
                    objResponse.BankCode = Request.Form[1].ToString();
                    obj.InsertError("CipherText : " + Request.Form[0].ToString());
                    objResponse.CipherText = Request.Form[0].ToString();
                    //obj.InsertError("URL : " + Request.UrlReferrer.AbsoluteUri.ToString());
                    //objResponse.URL = Request.UrlReferrer.AbsoluteUri.ToString();
                    objResponse.URL = "Egras.Raj";
                    //Created Bank.xml file with decrepted value for banks with key=Egras and value=0006326
                    //XElement xdocsign = XElement.Load(Server.MapPath("~/Bank.xml"));
                    //IEnumerable<XElement> node = from el in xdocsign.Elements("code")
                    //                             select el;
                    //bool flag = false;
                    //foreach (var xNode in node)
                    //{
                    //    string key = System.Web.Configuration.WebConfigurationManager.AppSettings["BankEncDecKey"];
                    //    string bankname = objResponse.DecryptString(xNode.Value, key);
                    //    if (bankname.Trim() == Request.Form[1].ToString().Trim())
                    //    {
                    //        flag = true;
                    //        break;
                    //    }
                    //}
                    //bool response = false;
                    //if (flag)
                    //    response = objResponse.UpdateResponseNew();
                    //else
                    obj.InsertError("Start Update ");
                    response = objResponse.UpdateResponse();

                    //objResponse.IPAddress = Request.UserHostAddress.ToString();
                    if (response)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');", true);
                        ShowData(objResponse);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                    }
                }
                catch(Exception ex)
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("last else|" + "PageLoad : "+ex.Message);
                }
            }
        }
    }
    private void ShowInsufficientData()
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('InSufficient Data');", true);
            grnlbl.Text = "";
            netamountlbl.Text = " \u20B9 " + Convert.ToDouble(0).ToString("F");
            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " AmountInWords('" + Convert.ToDouble(0).ToString("F") + "');", true);
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";
            Label5.Text = "";
            Label4.Text = "";
            Random randomclass = new Random();
            Session.Add("Rnd", randomclass.Next().ToString());
            HttpCookie appcookie = new HttpCookie("Appcookie");
            appcookie.HttpOnly = true;
            //  appcookie.Domain = "Egras.raj.nic.in";
            appcookie.Value = Session["RND"].ToString();
            appcookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(appcookie);
            lblUser.Text = Convert.ToString(Session["userName"]);
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lnkGuest.Visible = true;
            lnkprint.Visible = false;
            lblstatus.Visible = true;
            unsuccesspay.Visible = true;
            lblstatus.Text = "Data Not Received from Bank";
            lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
            DisplayTimer(0);
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "ShowInsufficientData");
        }
    }
    private void ShowData(BankResponseReceived objResp)
    {
        try
        {
            grnlbl.Text = objResp.GRNData["GRN"];    // grn no.
            netamountlbl.Text = " \u20B9 " + Convert.ToDouble(objResp.GRNData["PAID_AMT"]).ToString("F");
            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " AmountInWords('" + Convert.ToDouble(objResp.GRNData["PAID_AMT"]).ToString("F") + "');", true);
            Label1.Text = objResp.GRNData["BankReferenceNo"];  // bankref
            Label2.Text = objResp.GRNData["CIN"];  // cin
            Label3.Text = Convert.ToDateTime(objResp.GRNData["PAID_DATE"]).ToString("dd-MMM-yyyy");   // date
            Label5.Text = objResp.GRNData["BANK_CODE"];   // bankcode
            Label4.Text = objResp.GRNData["TRANS_STATUS"];  // status
            Random randomclass = new Random();
            Session.Add("Rnd", randomclass.Next().ToString());
            HttpCookie appcookie = new HttpCookie("Appcookie");
            appcookie.HttpOnly = true;
            //  appcookie.Domain = "Egras.raj.nic.in";
            appcookie.Value = Session["RND"].ToString();
            appcookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(appcookie);
            lblUser.Text = Convert.ToString(Session["userName"]);
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            if (Session["UserID"].ToString() != "73")
            {
                lnkHome.Visible = true;
            }
            else
            {
                lnkGuest.Visible = true;
            }
            switch (objResp.GRNData["TRANS_STATUS"].ToUpper())
            {
                case "S":
                    Label4.Text = "Success";
                    Label4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#27ae60");
                    lnkprint.Visible = true;
                    lblstatus.Visible = true;
                    successpay.Visible = true;
                    lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#27ae60");
                    lblstatus.Text = "Challan Successfull";
                    break;
                case "P":
                    Label4.Text = "Pending";
                    Label4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
                    lblstatus.Text = "Challan Pending";
                    lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
                    lblstatus.Visible = true;
                    pendingpay.Visible = true;
                    lnkprint.Visible = false;
                    break;
                case "F":
                    Label4.Text = "Fail";
                    Label4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                    lnkprint.Visible = false;
                    lblstatus.Visible = true;
                    unsuccesspay.Visible = true;
                    lblstatus.Text = "Challan UnSuccessfull";
                    lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                    break;
                default:
                    lnkprint.Visible = false;
                    lblstatus.Visible = true;
                    unsuccesspay.Visible = true;
                    lblstatus.Text = "Challan UnSuccessfull";
                    lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                    break;
            }
            DisplayTimer(objResp.MerchantCode);
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "ShowData");
        }
    }
    private void DisplayTimer(int mercode)
    {
        if (mercode == 0)
        {
            TABLE2.Visible = false;

            AjaxTimerControl.Enabled = false;
        }
        else
        {
            lnkHome.Visible = false;
            lnkGuest.Visible = false;
        }
    }
    protected void lnkprint_Click(object sender, EventArgs e)
    {
        try

        {
            if ((Session["UserId"] == null || Session["UserId"].ToString() == "") || (Session["UserType"] == null || Session["UserType"].ToString() == ""))
            {
                Response.Redirect("~\\logout.aspx", false);
                return;
            }
            EgEncryptDecrypt objEncrypt = new EgEncryptDecrypt();
            string strURLWithData = objEncrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}", grnlbl.Text, Session["UserId"].ToString(), Session["UserType"].ToString()));
            strURLWithData = "EgDefaceDetailNew.aspx?" + strURLWithData.ToString();
            Response.Redirect(strURLWithData, false);
        }
        catch (ThreadAbortException ed) { }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "print");
        }
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("EgHome.aspx", false);
    }

    protected void lnkGuest_Click(object sender, EventArgs e)
    {
        Response.Redirect("EgGuestProfile.aspx", false);
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["RND"] == null || Session["RND"].ToString() == "")
            {
                Response.Redirect("~\\logout.aspx", false);
                return;
            }
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Convert.ToString(Session["RND"])));
            Response.Redirect("~\\logout.aspx?" + strURLWithData, false);
        }
        catch (ThreadAbortException ed) { }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "Logout");
        }
    }

    protected void LinkIntegration_Click(object sender, EventArgs e)
    {
        Redirect();
    }

    protected void AjaxTimerControl_Tick(object sender, EventArgs e)
    {
        Redirect();
    }
    public void Redirect()
    {
        try
        {
            string plainText = "";
            string CheckSum = "";
            string cipherText = "";
            //string address = "https://egras.raj.nic.in/WebPages/EgIntergrationRedirect.aspx";
            string address = ConfigurationManager.AppSettings["IntegrationRedirectURL"].ToString();
            SbiEncryptionDecryption objSbiEncryptionDecryption = new SbiEncryptionDecryption();
            EgEncryptDecrypt objEnc = new EgEncryptDecrypt();
            EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
            if (grnlbl.Text != "GRN" && grnlbl.Text != "")
            {
                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
                objEgEChallanBankBL.GetGrnMerchantDetails();
                if (objEgEChallanBankBL.url.Trim() != "" && objEgEChallanBankBL.url != null)
                {
                    version = objEgEChallanBankBL.GetVersioninfo();

                    CheckSum = version == 1 ? objEnc.GetMD5Hash(objEgEChallanBankBL.encData) : objEnc.GetSHA256(objEgEChallanBankBL.encData);
                    plainText = objEgEChallanBankBL.encData + "|checkSum=" + CheckSum;
                    cipherText = CipherText(plainText, objEgEChallanBankBL.Mcode);
                    RemoteClass myremotepost = new RemoteClass();
                    myremotepost.Add("encdata", cipherText);
                    myremotepost.Add("merchant_code", Convert.ToString(objEgEChallanBankBL.Mcode));
                    myremotepost.Add("URL", objEgEChallanBankBL.url);
                    myremotepost.Url = address;
                    myremotepost.Post();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No Data');", true);
                Response.Redirect("~\\logout.aspx", false);
            }
        }
        catch (ThreadAbortException ed) { }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "Redirect");
        }
    }
    public string CipherText(string plainText, int MCode)
    {
        string returnstring = "";
        SbiEncryptionDecryption objSbiEncryptionDecryption = new SbiEncryptionDecryption();
        returnstring = version == 1 ? objSbiEncryptionDecryption.Encrypt(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key") :
        objSbiEncryptionDecryption.EncryptSBIWithKey256(plainText, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MCode + ".key");
        return returnstring;
    }
}


