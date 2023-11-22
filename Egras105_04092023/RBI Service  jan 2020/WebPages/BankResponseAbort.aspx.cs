using System;
using System.Web;
using System.Web.UI;
using EgBL;
using System.Threading;

public partial class WebPages_BankResponseAbort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Form.Keys.Count < 2)
            {
                string RequestString = "";
                for (int i = 0; i < Request.Form.Keys.Count; i++)
                {
                    RequestString += Request.Form[i].ToString();
                }
                BankResponseReceived objResponse = new BankResponseReceived();
                objResponse.BankCode = "";
                objResponse.CipherText = RequestString;
                objResponse.URL = "InsufficientData_" + Request.UrlReferrer.AbsoluteUri.ToString();
                objResponse.IPAddress = Request.UserHostAddress.ToString();
                if (!objResponse.UpdateResponse())
                {
                    ShowInsufficientData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                }
            }
            else if (Request.Form[1] == null || Request.Form[0] == null || Request.Form[1] == "" || Request.Form[0] == "")
            {
                BankResponseReceived objResponse = new BankResponseReceived();
                objResponse.BankCode = Convert.ToString(Request.Form[1]);
                objResponse.CipherText = Convert.ToString(Request.Form[0]);
                objResponse.URL = "InsufficientData_" + Request.UrlReferrer.AbsoluteUri.ToString();
                objResponse.IPAddress = Request.UserHostAddress.ToString();
                if (!objResponse.UpdateResponse())
                {
                    ShowInsufficientData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                }
            }
            else
            {
                BankResponseReceived objResponse = new BankResponseReceived();
                objResponse.BankCode = Request.Form[1].ToString();
                objResponse.CipherText = Request.Form[0].ToString();
                objResponse.URL = Request.UrlReferrer.AbsoluteUri.ToString();
                objResponse.IPAddress = Request.UserHostAddress.ToString();
                if (objResponse.UpdateResponse())
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');", true);
                    ShowData(objResponse);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objResponse.Message + "');window.location='../Logout.aspx';", true);
                }
            }
        }
    }
    private void ShowInsufficientData()
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
        appcookie.Domain = HttpContext.Current.Request.Url.Host;
        appcookie.Value = Session["RND"].ToString();
        appcookie.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(appcookie);
        lblUser.Text = Convert.ToString(Session["userName"]);
        lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lnkGuest.Visible = true;
        //lnkprint.Visible = false;
        lblstatus.Visible = true;
        //unsuccesspay.Visible = true;
        lblstatus.Text = "Data Not Received from Bank";
        lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
        //DisplayTimer(0);
    }
    private void ShowData(BankResponseReceived objResp)
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
        appcookie.Domain = HttpContext.Current.Request.Url.Host;
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
                //lnkprint.Visible = true;
                lblstatus.Visible = true;
                //successpay.Visible = true;
                lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#27ae60");
                lblstatus.Text = "Challan Successfull";
                break;
            case "P":
                Label4.Text = "Pending";
                Label4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
                lblstatus.Text = "Challan Pending";
                lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");
                lblstatus.Visible = true;
                //pendingpay.Visible = true;
                //lnkprint.Visible = false;
                break;
            case "F":
                Label4.Text = "Fail";
                Label4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                //lnkprint.Visible = false;
                lblstatus.Visible = true;
                //unsuccesspay.Visible = true;
                lblstatus.Text = "Challan UnSuccessfull";
                lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                break;
            default:
                //lnkprint.Visible = false;
                lblstatus.Visible = true;
                //unsuccesspay.Visible = true;
                lblstatus.Text = "Challan UnSuccessfull";
                lblstatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#c73424");
                break;
        }
        //DisplayTimer(objResp.MerchantCode);
    }

    //private void SetBank(string encData, string Bank_Code)
    //{
    //    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
    //    string plainText = "";
    //    if (Bank_Code.ToString().Length == 7)
    //    {
    //        switch (Bank_Code)
    //        {
    //            case "0292861":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/Union_Bank.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0200113":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/BOB.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0171051":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/RAJASTHAN_EGRASS.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0006326":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/RAJASTHAN_EGRASS.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0304017":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/PNB.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0280429":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/CBI.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "6910213":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/IDBI.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "0361193":
    //                plainText = objEncry.Decrypt(encData, Server.MapPath("~/WebPages/Key/OBC.key"));
    //                UpdateStatus(plainText);
    //                break;
    //            case "1000132":
    //                ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
    //                plainText = objEpay.Decrypt(encData, "BwmHPemeQsQhpwEGWmyQtQ==", 128);
    //                UpdateEpayStatus(plainText);
    //                break;
    //            default:
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('BSRCode not matched.');", true);
    //                TABLE1.Visible = false;
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid BSRCode.');", true);
    //        TABLE1.Visible = false;
    //    }
    //}

    //private int UpdateStatus(string palinText)
    //{
    //    int result = 0;
    //    try
    //    {
    //        List<string> lstPlainText = new List<string>();
    //        string[] arrMsgs = palinText.Split('|');
    //        string[] arrIndMsg;
    //        for (int i = 0; i < arrMsgs.Length; i++)
    //        {
    //            arrIndMsg = arrMsgs[i].Split('=');

    //            lstPlainText.Add(arrIndMsg[0]);
    //            lstPlainText.Add(arrIndMsg[1]);
    //        }

    //        grnlbl.Text = lstPlainText[1].ToString();
    //        netamountlbl.Text = lstPlainText[11].ToString();
    //        Label1.Text = lstPlainText[5].ToString();
    //        Label2.Text = lstPlainText[7].ToString();
    //        Label3.Text = lstPlainText[9].ToString();
    //        Label5.Text = lstPlainText[3].ToString();
    //        Label4.Text = lstPlainText[13].ToString();

    //        if (lstPlainText[13].ToString().ToUpper() != "F")
    //        {
    //            if ((lstPlainText[7].ToString().Length == 21 && (lstPlainText[5].ToString().Length != 0 || lstPlainText[5].ToString() != null) && lstPlainText[13].ToString().ToUpper() == "S") ||
    //             (lstPlainText[7].ToString().Length != 0 && (lstPlainText[5].ToString().Length != 0 || lstPlainText[5].ToString() != null) && lstPlainText[13].ToString().ToUpper() == "P"))
    //            {
    //                EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.LoadUserEntries();

    //                Session["UserID"] = objEgEChallanBankBL.UserId;
    //                Session["UserType"] = objEgEChallanBankBL.UserType;
    //                Session["UserName"] = objEgEChallanBankBL.Name;
    //                Session["MenuDataSet"] = "";

    //                Random randomclass = new Random();
    //                Session.Add("Rnd", randomclass.Next().ToString());

    //                HttpCookie appcookie = new HttpCookie("Appcookie");
    //                appcookie.Value = Session["RND"].ToString();
    //                appcookie.Expires = DateTime.Now.AddDays(1);
    //                Response.Cookies.Add(appcookie);

    //                lblUser.Text = Session["userName"].ToString();
    //                lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");



    //                if (Session["UserID"] != "73")
    //                {
    //                    lnkHome.Visible = true;
    //                }
    //                else
    //                {
    //                    lnkGuest.Visible = true;
    //                }
    //                objEgEChallanBankBL.BankCode = lstPlainText[3].ToString();
    //                objEgEChallanBankBL.CIN = lstPlainText[7].ToString();
    //                objEgEChallanBankBL.Ref = lstPlainText[5].ToString();
    //                objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text);
    //                objEgEChallanBankBL.Status = lstPlainText[13].ToString();
    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
    //                result = objEgEChallanBankBL.UpdateSuccessStatus();

    //                if (lstPlainText[13].ToString().ToUpper() == "S")
    //                {
    //                    Label4.Text = "Success";
    //                    //lnkprint.Visible = true;
    //                    lblstatus.Visible = true;
    //                    lblstatus.Text = "Challan successfull submitted";
    //                }
    //                if (lstPlainText[13].ToString().ToUpper() == "P")
    //                {
    //                    Label4.Text = "Pending";
    //                    lblstatus.Text = "Challan Pending.";
    //                    lblstatus.Visible = true;
    //                    //lnkprint.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid Cin or Status is wrong.');", true);
    //                lblstatus.Text = "Challan Unsuccessfull.";
    //            }
    //        }
    //        else if (lstPlainText[13].ToString().ToUpper() == "F")
    //        {
    //            if (lstPlainText[3].ToString().Length == 7)
    //            {
    //                EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
    //                objEgEChallanBankBL.BankCode = lstPlainText[3].ToString();
    //                objEgEChallanBankBL.Ref = lstPlainText[5].ToString();
    //                objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text);
    //                objEgEChallanBankBL.Status = lstPlainText[13].ToString();
    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[9].ToString());
    //                result = objEgEChallanBankBL.UpdateStatus();

    //                Label4.Text = "Fail";
    //                //lnkprint.Visible = false;
    //                lblstatus.Visible = true;
    //                lblstatus.Text = "Challan Unsuccessfull.";
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid BSRCode.');", true);
    //                //lnkprint.Visible = false;
    //                lblstatus.Visible = true;
    //                lblstatus.Text = "Challan Unsuccessfull.";

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        EgErrorHandller obj = new EgErrorHandller();
    //        obj.InsertError(ex.Message.ToString());
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, We are not able to process.');", true);
    //    }
    //    return result;
    //}

    //private int UpdateEpayStatus(string palinText)
    //{
    //    int result = 0;
    //    try
    //    {
    //        EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
    //        string[] lstPlainText = palinText.Split('|');

    //        grnlbl.Text = lstPlainText[0].ToString();// grn no.
    //        netamountlbl.Text = lstPlainText[3].ToString(); // amount
    //        Label1.Text = lstPlainText[1].ToString();  // bankref
    //        Label2.Text = lstPlainText[12].ToString();  // cin
    //        Label3.Text = lstPlainText[10].ToString();   // date
    //        Label5.Text = lstPlainText[13].ToString();   // bankcode
    //        Label4.Text = lstPlainText[2].ToString().Substring(0, 1);  // status

    //        if (lstPlainText[2].ToString().Substring(0, 1).ToUpper() != "F")
    //        {
    //            if (((lstPlainText[1].ToString().Length != 0 || lstPlainText[1].ToString() != null) && lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "S") ||
    //             (lstPlainText[12].ToString().Length != 0 && (lstPlainText[1].ToString().Length != 0 || lstPlainText[1].ToString() != null) && lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "P"))
    //            {

    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.BankCode = lstPlainText[13].ToString();
    //                if (lstPlainText[12].ToString() == "NA")
    //                {
    //                    objEgEChallanBankBL.CIN = "0";
    //                }
    //                else
    //                {
    //                    objEgEChallanBankBL.CIN = lstPlainText[12].ToString();
    //                }
    //                objEgEChallanBankBL.Ref = lstPlainText[1].ToString();
    //                objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text);
    //                objEgEChallanBankBL.Status = lstPlainText[2].ToString().Substring(0, 1); ;
    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[10].ToString());
    //                result = objEgEChallanBankBL.UpdateSuccessStatus();

    //                //epay entry
    //                objEgEChallanBankBL.epayBSRCode = lstPlainText[8].ToString();
    //                objEgEChallanBankBL.bankRefNo = lstPlainText[9].ToString();
    //                objEgEChallanBankBL.payMode = lstPlainText[5].ToString();
    //                objEgEChallanBankBL.UpdateEpayStatus();

    //                objEgEChallanBankBL.LoadUserEntries();

    //                Session["UserID"] = objEgEChallanBankBL.UserId;
    //                Session["UserType"] = objEgEChallanBankBL.UserType;
    //                Session["UserName"] = objEgEChallanBankBL.Name;
    //                Session["MenuDataSet"] = "";

    //                Random randomclass = new Random();
    //                Session.Add("Rnd", randomclass.Next().ToString());

    //                HttpCookie appcookie = new HttpCookie("Appcookie");
    //                appcookie.Value = Session["RND"].ToString();
    //                appcookie.Expires = DateTime.Now.AddDays(1);
    //                Response.Cookies.Add(appcookie);

    //                lblUser.Text = Session["userName"].ToString();
    //                lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

    //                if (Session["UserID"] != "73")
    //                {
    //                    lnkHome.Visible = true;
    //                }
    //                else
    //                {
    //                    lnkGuest.Visible = true;
    //                }

    //                if (lstPlainText[2].ToString().ToUpper() == "S")
    //                {
    //                    Label4.Text = "Success";
    //                    //                        lnkprint.Visible = true;
    //                    lblstatus.Visible = true;
    //                    lblstatus.Text = "Challan successfull submitted";
    //                }
    //                if (lstPlainText[2].ToString().ToUpper() == "P")
    //                {
    //                    Label4.Text = "Pending";
    //                    lblstatus.Text = "Challan Pending.";
    //                    lblstatus.Visible = true;
    //                    //                      lnkprint.Visible = false;
    //                }

    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid Cin or Status is wrong.');", true);
    //                lblstatus.Text = "Challan Unsuccessfull.";
    //            }
    //        }
    //        else if (lstPlainText[2].ToString().Substring(0, 1).ToUpper() == "F")
    //        {
    //            if (lstPlainText[13].ToString().Length == 7)
    //            {

    //                objEgEChallanBankBL.BankCode = lstPlainText[13].ToString();
    //                objEgEChallanBankBL.Ref = lstPlainText[1].ToString();
    //                if (lstPlainText[12].ToString() == "NA")
    //                {
    //                    objEgEChallanBankBL.CIN = "0";
    //                }
    //                else
    //                {
    //                    objEgEChallanBankBL.CIN = lstPlainText[12].ToString();
    //                }
    //                objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text);
    //                objEgEChallanBankBL.Status = lstPlainText[2].ToString().Substring(0, 1);
    //                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
    //                objEgEChallanBankBL.timeStamp = Convert.ToDateTime(lstPlainText[10].ToString());
    //                result = objEgEChallanBankBL.UpdateStatus();

    //                // epay entry
    //                objEgEChallanBankBL.epayBSRCode = lstPlainText[8].ToString();
    //                objEgEChallanBankBL.bankRefNo = lstPlainText[9].ToString();
    //                objEgEChallanBankBL.payMode = lstPlainText[5].ToString();
    //                objEgEChallanBankBL.UpdateEpayStatus();

    //                Label4.Text = "Fail";
    //                //                  lnkprint.Visible = false;
    //                lblstatus.Visible = true;
    //                lblstatus.Text = "Challan Unsuccessfull.";
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid BSRCode.');", true);
    //                //                    lnkprint.Visible = false;
    //                lblstatus.Visible = true;
    //                lblstatus.Text = "Challan Unsuccessfull.";
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        EgErrorHandller obj = new EgErrorHandller();
    //        obj.InsertError(ex.Message.ToString());
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, We are not able to process.');", true);
    //    }
    //    return result;
    //}

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("EgHome.aspx");
    }

    protected void lnkGuest_Click(object sender, EventArgs e)
    {
        Response.Redirect("EgGuestProfile.aspx");
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
            obj.InsertError(ex.Message.ToString() + "|" + grnlbl.Text + "|" + "AbortLogout");
        }
    }
}
