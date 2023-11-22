using System;
using EgBL;
using System.Web.UI;
using System.Web.Services;

public partial class WebPages_EgEChallanView : System.Web.UI.Page
{
    public static UserControl Umenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        EgEChallanBL objEgEChallan = new EgEChallanBL();
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "" || Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {


            objEgEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEgEChallan.GRNNumber = Convert.ToInt64(Session["GrnNumber"].ToString());

            int Result = 0;
            Result = objEgEChallan.EGRNAmount();

            //    if (Result == 1)

            //    {
            //        //Gennolbl.Text = Convert.ToString(objEgEChallan.GRNNumber);
            //        //Session["NetAmount"] = objEgEChallan.TotalAmount;
            //        //amountLbl.Text = objEgEChallan.TotalAmount;
            //        //hdnBank.Value = objEgEChallan.BankCode;

            //        if (objEgEChallan.DeptCode == 86)
            //        {
            //            EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
            //            objEgEChallanBankBL.GRN = objEgEChallan.GRNNumber;
            //            bool stampCase = objEgEChallanBankBL.CheckStamp10PercentCase();

            //            if (stampCase)
            //            {
            //                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " StampPopup()", true);
            //            }
            //        }
            //        if (objEgEChallan.OfficeName == 0)
            //        {
            //            //lnkMultipleOfcs.Visible = true;

            //        }// Show For MultipleOffice Challan

            //        if (objEgEChallan.MerchantCode != 0)
            //        {
            //            //lnkGoBack.Visible = false;
            //            //HyperLink1.Visible = false;
            //            Umenu = Page.Master.FindControl("vmenu1") as UserControl;
            //            Umenu.Visible = false;
            //        }
            //    }
            //}

            BankForward objfwrd = new BankForward();
            objfwrd.GRNBankForward(objEgEChallan.BankCode);

        }

        /// <summary>
        /// Use for transfer data to bank application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        //protected void btnGo_Click(object sender, EventArgs e)
        //{
        //    BankForward objfwrd = new BankForward();
        //    objfwrd.GRNBankForward(hdnBank.Value);
        //}

    }
    [WebMethod]
    public static string EncryptData(string id)
    {
        var a = "View";
        EncryptDecryptionBL objenc = new EncryptDecryptionBL();
        var comm = id + '-' + a;
        comm = objenc.Encrypt(comm);
        return comm;


    }




}