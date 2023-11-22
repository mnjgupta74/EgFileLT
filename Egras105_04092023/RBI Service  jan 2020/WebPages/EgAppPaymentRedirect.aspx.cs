
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using EgBL;
using System.Web;

public partial class WebPages_EgAppPaymentRedirect : System.Web.UI.Page
{
    DataTable SchemaDt = new DataTable();
    string GRN;
    string amount;
    string UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GRN = Request.Form["GRN"].ToString();
            // amount = Request.Form["TotalAmount"].ToString();
           
            UserID = Request.Form["UserID"].ToString().Replace(" ", "+");
            SbiEncryptionDecryption objDecrypt = new SbiEncryptionDecryption();
            Session["UserId"] = objDecrypt.DecryptString(UserID, ConfigurationManager.AppSettings["AppKey"].ToString());
            Session["GrnNumber"] = GRN;
            Session["userName"] = "";
            lblGRN.Text = GRN;
            lblTotalAmount.Text = amount;

            btnProceedToPay_Click();
        }
    }
    protected void btnProceedToPay_Click()
    {




        EgEChallanBL objEgEChallan = new EgEChallanBL();
        BankForward objfwrd = new BankForward();
        DataTable bankForwardData = new DataTable();
        objEgEChallan.GRNNumber = Int64.Parse(Session["GrnNumber"].ToString());
        objEgEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
        bankForwardData = objEgEChallan.GetBankForwardDetail();
        Session["NetAmount"] = Convert.ToDouble(bankForwardData.Rows[0]["Head_Amount"].ToString());
         objfwrd.GRNBankForward(bankForwardData.Rows[0]["BSRCode"].ToString()); 
       


        //    BankForward objfwrd = new BankForward();
        //    EgEChallanBL objEgEChallan = new EgEChallanBL();
        //    objEgEChallan.GRNNumber = Convert.ToInt32(Session["GRN"].ToString().Trim());
        //    objEgEChallan.UserId = Convert.ToInt32(Session["UserID"].ToString().Trim());
        //    string[] Head_Name = new string[9];
        //    string[] Head_Amount = new string[9];
        //    string[] words;
        //    decimal NetAmount = 0;
        //    string payMode = "N";
        //    int Result = 0;
        //    Result = objEgEChallan.EChallanView();
        //    objEgEChallan.BankName = objEgEChallan.BankCode;
        //    DataTable bankData = objEgEChallan.GetBankDetail();
        //    if (Result == 1)
        //    {
        //        string GEN = Convert.ToString(objEgEChallan.GRNNumber);
        //        DataTable dt = (DataTable)objEgEChallan.GetSchemas();
        //        if (objEgEChallan.BankCode != "9910001")
        //        {
        //            if (dt.Rows.Count > 9)
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Budget Head List Exceed the Limit.');", true);
        //                return;
        //            }



        //            for (int i = 0; i < 9; i++)
        //            {

        //                //if (i < dt.Rows.Count)
        //                if (i == 0)
        //                {
        //                    words = dt.Rows[i][1].ToString().Split('-');
        //                    Head_Name[i] = words[0].Substring(0, 4) + "-" + words[0].Substring(4, 2) + "-" + words[0].Substring(6, 3) + "-" + words[0].Substring(9, 2) + "-" + words[0].Substring(11, 2);   //+ " _" + dt.Rows[i][0].ToString();
        //                                                                                                                                                                                                    //Head_Amount[i] = System.Decimal.Round(Convert.ToDecimal(dt.Rows[i][2]), 2).ToString();
        //                    NetAmount = NetAmount + Convert.ToDecimal(System.Decimal.Round(Convert.ToDecimal(dt.Rows[i][2]), 2).ToString());
        //                }
        //                else
        //                {
        //                    Head_Name[i] = "0";
        //                    Head_Amount[i] = "0.00";
        //                    decimal amount = i < dt.Rows.Count ? System.Decimal.Round(Convert.ToDecimal(dt.Rows[i][2]), 2) : Convert.ToDecimal(0.00);
        //                    NetAmount = NetAmount + amount;
        //                }


        //            }

        //            Head_Amount[0] = NetAmount.ToString();        // Net Amount Assign With Single Head
        //            if (Head_Name[0].ToString().Substring(0, 4) == "0030" && Convert.ToDouble(objEgEChallan.DeductCommission) > 0)
        //            {
        //                Head_Amount[0] = (Convert.ToDouble(Head_Amount[0]) - Convert.ToDouble(objEgEChallan.DeductCommission)).ToString();
        //                //NetAmount =  Convert.ToDecimal(Head_Amount[0]);

        //                NetAmount = (NetAmount - Convert.ToDecimal(objEgEChallan.DeductCommission));

        //            }





        //            if (objEgEChallan.Identity == "")
        //            {
        //                if (Session["UserId"].ToString() == "73")
        //                {
        //                    objEgEChallan.Identity = "G" + GEN;
        //                }
        //                else
        //                {
        //                    objEgEChallan.Identity = Session["UserId"].ToString();
        //                }
        //            }
        //            objfwrd.filler = "A";
        //            objfwrd.Head_Name = Head_Name;
        //            objfwrd.Head_Amount = Head_Amount;
        //            objfwrd.LocationCode = Convert.ToString(ViewState["LocationCode"]);
        //            objfwrd.TIN = objEgEChallan.Identity;

        //            objfwrd.merchantCode = bankData.Rows[0][1].ToString();
        //            objfwrd.PaymentMode = payMode;
        //        }



        //    else

        //    {
        //            SchemaDt = objEgEChallan.fillSchemaChallan();
        //            if (SchemaDt.Rows.Count > 0)
        //            {                    
        //                words = SchemaDt.Rows[0][0].ToString().Split('-');
        //                Head_Name[0] = words[0].Substring(0, 4);
        //                objfwrd.Head_Name = Head_Name;
        //                NetAmount = Convert.ToDecimal(objEgEChallan.TotalAmount);
        //            }


        //        }

        //        objfwrd.URL = bankData.Rows[0][0].ToString();
        //        objfwrd.BSRCode = objEgEChallan.BankCode;
        //        objfwrd.GRN = objEgEChallan.GRNNumber;
        //        objfwrd.TotalAmount = Convert.ToDouble(objEgEChallan.TotalAmount);
        //        objfwrd.RemitterName = objEgEChallan.FullName;
        //    if (Convert.ToDecimal(objEgEChallan.TotalAmount) == Convert.ToDecimal(NetAmount))
        //    {
        //        objfwrd.GRNBankForward();
        //    }
        //    else
        //    {

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Security Reason Please ReCreate Grn. Go To Home Link');", true);
        //        return;
        //    }
        //}


        //}
    }
}