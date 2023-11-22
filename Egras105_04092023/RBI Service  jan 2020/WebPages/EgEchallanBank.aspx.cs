using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Security.Cryptography;
using System.Data;
using EgBL;
public partial class WebPages_EgEchallanBank : System.Web.UI.Page
{
    EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
    EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
    string PayStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            //string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("RND={0}", Session["RND"].ToString()));
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            string url = Request.UrlReferrer.ToString();           
          
            DataTable dt = (DataTable)objEgEChallanBankBL.GetIpAddress();

            var checkIP = dt.AsEnumerable().SingleOrDefault(r => r.Field<string>("IpAddress") == ipAddress.ToString() && r.Field<string>("bankurl") == url);

            if (checkIP != null)
            {

                #region 2nd
                if (Request.Form["String1"] != null)
                {
                    SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                    string plainText = Request.Form["String1"].ToString();

                    List<string> decryText = DecryptString(plainText);
                   
                    grnlbl.Text = decryText[1].ToString();

                    //Priyanka Sharma *****************
                    objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
                    objEgEChallanBankBL.LoadUserEntries();

                    Session["UserID"] = objEgEChallanBankBL.UserId;
                    if (objEgEChallanBankBL.UserId == 0)
                    {
                        Session["UserType"] = 20;
                    }
                    else
                    {
                        Session["UserType"] = objEgEChallanBankBL.UserType;
                    }                  

                    netamountlbl.Text = decryText[3].ToString();


                #endregion

                    if (decryText[5].ToString() != null) 
                    { 
                        PayStatus = decryText[5].ToString(); 
                    }
                    if (PayStatus == "2")//0 for Pending,  1 For SuccessFull 2 For Failed
                    {
                        lnkprint.Visible = false;
                        lblstatus.Visible = true;
                        lblstatus.Text = "Challan Unsuccessfull Try Again";

                        objEgEChallanBankBL.Status = "F";
                        objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
                        if (decryText[3].ToString() != null)
                        { 
                            objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text);
                        }
                        objEgEChallanBankBL.timeStamp = Convert.ToDateTime(decryText[7].ToString());
                        objEgEChallanBankBL.UpdateStatus();

                    }

                    else
                    {
                        if (PayStatus == "1")
                        {
                                if (decryText[7].ToString() != null) { objEgEChallanBankBL.BankCode = decryText[7].ToString(); }
                                if (decryText[9].ToString() != null) { objEgEChallanBankBL.CIN = decryText[9].ToString(); }
                                if (decryText[11].ToString() != null) { objEgEChallanBankBL.Ref = decryText[11].ToString(); }
                                objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text); 
                                objEgEChallanBankBL.Status = "S";
                                objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
                                objEgEChallanBankBL.timeStamp = Convert.ToDateTime(decryText[13].ToString());
                                objEgEChallanBankBL.UpdateSuccessStatus();

                                //BService.Service objBservice = new BService.Service();
                                //string returnBankData = objBservice.ToVerfied(int.Parse(grnlbl.Text), Convert.ToDouble(decryText[3].ToString()));

                                //List<string> words = SplitString(returnBankData);

                                //objEgEChallanBankBL.GRN = Convert.ToInt32(words[1]);
                                //objEgEChallanBankBL.CIN = words[3].ToString();
                                //objEgEChallanBankBL.Ref = Convert.ToInt32(words[5]);
                                //objEgEChallanBankBL.Amount = Convert.ToDouble(words[7]);
                                //objEgEChallanBankBL.Status = words[9].ToString();
                                //objEgEChallanBankBL.timeStamp = Convert.ToDateTime(words[11].ToString());

                                //objEgEChallanBankBL.UpdateSuccessStatusReconcile();

                            lnkprint.Visible = true;
                            lblstatus.Visible = true;
                            lblstatus.Text = "Challan successfull Submitted";

                        }
                        else if (PayStatus == "0")
                        {
                            lblstatus.Text = "Challan Pending";
                            lblstatus.Visible = true;
                            objEgEChallanBankBL.Status = "P";
                            objEgEChallanBankBL.GRN = Convert.ToInt32(grnlbl.Text);
                            if (decryText[3].ToString() != null) { objEgEChallanBankBL.Amount = Convert.ToDouble(netamountlbl.Text); }
                            objEgEChallanBankBL.timeStamp = Convert.ToDateTime(decryText[7].ToString());
                            objEgEChallanBankBL.UpdatePendingStatus();
                        }
                        else
                        {
                            lnkprint.Visible = false;
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('IP or URL is not valid.');", true);
                TABLE1.Visible = false;

            }

        }
    }

    private List<string> DecryptString(string strQueryString)
    {
        List<string> tempList = new List<string>();
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        string ss = objEncry.Decrypt(strQueryString);
        if (ss != null)
        {
            string[] arrMsgs = ss.Split('|');
            string[] arrIndMsg;
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');

                tempList.Add(arrIndMsg[0]);
                tempList.Add(arrIndMsg[1]);
            }
        }

        return tempList;
    }

    private List<string> SplitString(string strQueryString)
    {
        List<string> tempList = new List<string>();
       
            string[] arrMsgs = strQueryString.Split('&');
            string[] arrIndMsg;
            for (int i = 0; i < arrMsgs.Length; i++)
            {
                arrIndMsg = arrMsgs[i].Split('=');

                tempList.Add(arrIndMsg[0]);
                tempList.Add(arrIndMsg[1]);
            }      

        return tempList;
    }

    /// <summary>
    /// redirect the page where printout is taken
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkprint_Click(object sender, EventArgs e)
    {
        string strURLWithData = EncryptQueryString(string.Format(grnlbl.Text, "S"));
        strURLWithData = "EgEchallanView.aspx?" + strURLWithData.ToString();
        Response.Redirect(strURLWithData);
    }
    
   
    #region Encrypt and Decrypt
    public string EncryptQueryString(string strQueryString)
    {
        strQueryString = objEncryptDecryptionBL.Encrypt(strQueryString);
        return strQueryString;
    }
    private string DecryptQueryString(string strQueryString)
    {
        int random = ((Session["Rnd"].ToString().Length) + 6);
        strQueryString = strQueryString.Substring(0, 6) + strQueryString.Substring(random);
        return objEncryptDecryptionBL.Decrypt(strQueryString);
    }

    #endregion
}