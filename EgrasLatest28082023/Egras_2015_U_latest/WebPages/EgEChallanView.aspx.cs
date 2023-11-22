using System;
using System.Data;
using System.Web.UI;
using EgBL;
using System.Web.Services;
using System.Web;
using System.Collections.Generic;

public partial class WebPages_EgEChallanView : System.Web.UI.Page
{
    public static UserControl Umenu;
    DataTable SchemaDt = new DataTable();
    string[] words;
    EgEncryptDecrypt ObjEncrytDecrypt;
    Int64 RequestGRN;
    Int32 ChallanUserid;
    Int64 RandomNo;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "" || Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEgEChallan = new EgEChallanBL();
            objEgEChallan.GetChallanBanks(ddlBank);
            objEgEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEgEChallan.GRNNumber = Convert.ToInt64(Session["GrnNumber"].ToString());
            //objEgEChallan.UserId = ChallanUserid;//Convert.ToInt32(Request.Form["GRN"].ToString()); 
            //objEgEChallan.GRNNumber = RequestGRN;//Convert.ToInt32(Request.Form["UserId"].ToString());
            MpeOTP.Hide();
            int Result = 0;
            Result = objEgEChallan.EChallanView();

            if (Result == 1)
            {
                lblDepartment.Text = "Department";
                Departmentlbl.Text = objEgEChallan.Location;
                if (objEgEChallan.UserId == 73)
                {
                    HyperLink1.Visible = true;
                }

                //string GEN = "000000000" + Convert.ToString(objEgEChallan.GRNNumber);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                //Gennolbl.Text = GEN.Substring(GEN.Length - 10, 10);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                Gennolbl.Text = Convert.ToString(objEgEChallan.GRNNumber);
                //formdatelbl.Text = Convert.ToString(objEgEChallan.ChallanDate);
                TypeOfPaymentlbl.Text = "Online";
                Tinlbl.Text = objEgEChallan.Identity;
                OfficeNamelbl.Text = objEgEChallan.Office;
                PanNolbl.Text = objEgEChallan.PanNumber;
                Locationlbl.Text = objEgEChallan.TreasuryName;
                ViewState["LocationCode"] = objEgEChallan.TreasuryCode;
                fullnamelbl.Text = objEgEChallan.FullName;
                frommonthlbl.Text = Convert.ToString(objEgEChallan.ChallanFromMonth.ToString("dd/MM/yyyy"));
                tomonthlbl.Text = Convert.ToString(objEgEChallan.ChallanToMonth.ToString("dd/MM/yyyy"));
                addresslbl.Text = objEgEChallan.Address;
                Townlbl.Text = objEgEChallan.CityName.ToString();
                Pinlbl.Text = objEgEChallan.PINCode;
                Remarklbl.Text = objEgEChallan.Remark;            //":-Challan Working Good";
                deductcommisiionlbl.Text = objEgEChallan.DeductCommission.ToString("0.00");
                NetAmountlbl.Text = objEgEChallan.TotalAmount;
                Session["NetAmount"] = objEgEChallan.TotalAmount;
                ddlBank.SelectedValue = objEgEChallan.BankCode;
                ddlBank.Enabled = false;
                if (objEgEChallan.BankCode == "0006326")
                {
                    lblmsg.Text = "SBI Has Resumed Netbanking Service 24*7 !";
                }
                if (objEgEChallan.OfficeName == 0) { lnkMultipleOfcs.Visible = true; }// Show For MultipleOffice Challan
                EgEChallanBankBL objEgEChallanBankBL = new EgEChallanBankBL();
                objEgEChallanBankBL.GRN = Convert.ToInt32(Session["GrnNumber"].ToString());
                if (objEgEChallanBankBL.CheckGrnMerchantCode() != 0)
                {
                    //lnkGoBack.Visible = false;
                    HyperLink1.Visible = false;
                    Umenu = Page.Master.FindControl("vmenu1") as UserControl;
                    Umenu.Visible = false;
                }

                // Get ProcUserId Form Grn And Display Message To User That Vhallan related tO Proc or Non Proc
                //int ProcUserid=   objEgEChallan.CheckProcUserId();
                //if (ProcUserid<0)
                // {
                // Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript1", "ProcPopup()", true);
                // }


                if (objEgEChallan.Details != "") { lnkExtraDetails.Visible = true; } // Show For Add Extra Details On Challan
                Amountwordslbl.Text = objEgEChallan.AmountInWords;
                SchemaDt = objEgEChallan.fillSchemaChallan();
                if (SchemaDt.Rows.Count > 0)
                {
                    // ViewState["SchemaTable"] = SchemaDt;
                    GridView1.DataSource = SchemaDt;
                    GridView1.DataBind();

                }

                bool stampCase = objEgEChallanBankBL.CheckStamp10PercentCase();
                if (stampCase)
                {
                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " StampPopup()", true);
                }

                //Bitmap barcode = CreateBarCode(GEN.Substring(GEN.Length - 10, 10));// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                //Bitmap barcode = CreateBarCode(Convert.ToString(objEgEChallan.GRNNumber));
                //barcode.Save(Server.MapPath("../Image/barcode.Gif"), ImageFormat.Gif);
                ////Image2.ImageUrl = "~/Image/barcode.Gif";
                //barcode.Dispose();
            }
            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " checkamounttotal('" + NetAmountlbl.ID + "')", true);

        }
    }


    //protected Bitmap CreateBarCode(string data)
    //{
    //    string Code = data;

    //    // Multiply the lenght of the code by 25 (just to have enough width)
    //    int w = Code.Length * 25;

    //    // Create a bitmap object of the width that we calculated and height of 30
    //    Bitmap oBitmap = new Bitmap(w, 30);
    //    Graphics oGraphics = Graphics.FromImage(oBitmap);

    //    PrivateFontCollection fnts = new PrivateFontCollection();
    //    fnts.AddFontFile(Server.MapPath("../WebPages/font/IDAutomationHC39M.ttf"));
    //    FontFamily fntfam = new FontFamily("IDAutomationHC39M", fnts);
    //    Font oFont = new Font(fntfam, 25);

    //    PointF oPoint = new PointF(2f, 2f);
    //    SolidBrush oBrushWrite = new SolidBrush(Color.Black);
    //    SolidBrush oBrush = new SolidBrush(Color.White);

    //    oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

    //    oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);
    //    oGraphics.Dispose();
    //    return oBitmap;
    //}


    /// <summary>
    /// Use for transfer data to bank application 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 


    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlBank.SelectedValue == "0")
        {
            MpeOTP.Hide();            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Select Bank Name.');", true);
        }
        //else if (ddlBank.SelectedValue == "9950001")
        //{
        //    MpeOTP.Show();
        //}
        else
        {
            MpeOTP.Hide();
            BankForward objfwrd = new BankForward();
            objfwrd.GRNBankForward(ddlBank.SelectedValue);
        }
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