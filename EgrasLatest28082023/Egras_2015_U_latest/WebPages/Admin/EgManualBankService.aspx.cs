using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Admin_EgManualBankService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.GetChallanBanks(ddlbankname);

        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
        objEgManualBankServiceBL.BankCode = ddlbankname.SelectedValue.Trim();
        dt=objEgManualBankServiceBL.ManualGRNlist();
        grdVerifyChallan.DataSource = dt;
        grdVerifyChallan.DataBind();
        dt.Dispose();
    }

   
    protected void grdVerifyChallan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string cipherText = "";
            string returnData = "";
            if (e.CommandName.Equals("Verify"))
            {
                int idx = Convert.ToInt32(e.CommandArgument);
                int grn = int.Parse(grdVerifyChallan.Rows[idx - 1].Cells[1].Text);
                string Bank_Code = grdVerifyChallan.Rows[idx - 1].Cells[2].Text;
                double Amount =    Convert.ToDouble(grdVerifyChallan.Rows[idx - 1].Cells[3].Text);
                // EgEChallanBL objEgEChallan = new EgEChallanBL();
                EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
                objEgManualBankServiceBL.GRNNumber = grn;
                objEgManualBankServiceBL.BankCode = Bank_Code;
                
                string ChallanDetails = objEgManualBankServiceBL.GetGrnManualDetails();
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                switch (Bank_Code.Substring(0,3))
                {
                    case  "020":
                        
                        break;
                    case "029":
                        
                        break;
                    case "000":
                        string plainText = string.Format("GRN={0}|TOTALAMOUNT={1}", objEgManualBankServiceBL.GRNNumber.ToString(), Amount.ToString());
                        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
                        string checkSum = objEncryption.GetMD5Hash(plainText);
                        Banks objBank = Banks.SelectBanks((objEgManualBankServiceBL.BankCode));
                        objBank.TotalAmount = Amount;
                        objBank.checkSum = checkSum;
                        cipherText = objBank.GetRequestString(plainText);
                        SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                        returnData= Ojpost.OnlineEnquiry_Rajasthan(cipherText);

                        //returnData = objBank.CallVerifyManualService(cipherText);
                        //cipherText = objEncry.Encrypt(ChallanDetails, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));
                        //SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                        //Ojpost.ProcessRajasthanData(cipherText);
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('"+returnData+"');", true);


                        break;
                    case "017":

                        //cipherText = objEncry.Encrypt(ChallanDetails, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));
                        //SBIManualServ.RAJASTHANWS objSBBJ = new SBIManualServ.RAJASTHANWS();
                        //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                        //returnData = objSBBJ.challanposting(cipherText);
                        //returnData = objEncry.Decrypt(returnData, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + returnData + "');", true);
                        break;
                    case "030":
                       
                        PNBManualWebServ.ReceiveData objpnbPush = new PNBManualWebServ.ReceiveData();
                        cipherText = objEncry.Encrypt(ChallanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                        objpnbPush.UploadData(cipherText);// PUSH Service PNB Manual Challan anywhere
                        break;
                    case "691":
                       
                        break;
                    case "028":
                       
                        CBIPushServ.EGRASS obj = new CBIPushServ.EGRASS();
                        cipherText = objEncry.Encrypt(ChallanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                        obj.EGRASSFETCHDETAILS(cipherText);
                        break;
                    case "036":
                      
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('BSR not matched');", true);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            int result = obj.InsertError(ex.Message.ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, Unable to process.');", true);

        }
    }
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy() { }
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert,
            WebRequest req,
            int problem)
        {
            return true;
        }
    }
}
