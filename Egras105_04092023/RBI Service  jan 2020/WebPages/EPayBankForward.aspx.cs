using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;

public partial class WebPages_EPayBankForward : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
            objEgManualBankServiceBL.BankCode = "1000132";
            dt= objEgManualBankServiceBL.GetEPayGRN();
            grdVerifyChallan.DataSource = dt;
            grdVerifyChallan.DataBind();
            dt.Dispose();
        }
    }

    protected void grdVerifyChallan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idx = Convert.ToInt32(e.CommandArgument);
        int grn = int.Parse(grdVerifyChallan.Rows[idx - 1].Cells[1].Text);
        string GEN = "";//000000000" + Convert.ToString(grn); // Commented by sandeep on 02/03/2017 for removing leading zeros
        //GEN = GEN.Substring(GEN.Length - 10, 10);//Commented by sandeep on 02/03/2017 for removing leading zeros
        GEN = grn.ToString();

        string Bank_Code = grdVerifyChallan.Rows[idx - 1].Cells[2].Text;
        string amount = grdVerifyChallan.Rows[idx - 1].Cells[3].Text;
       
        EgManualBankServiceBL objEgManualBankServiceBL = new EgManualBankServiceBL();
        objEgManualBankServiceBL.GRNNumber = grn;
        objEgManualBankServiceBL.BankCode = Bank_Code;
        string plainText = objEgManualBankServiceBL.GetEPayChallanDetail();
      
        //string plainText = string.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}^{22}^{23}^{24}", Gennolbl.Text, Head_Name[0], Head_Amount[0], Head_Name[1], Head_Amount[1], Head_Name[2], Head_Amount[2], Head_Name[3], Head_Amount[3], Head_Name[4], Head_Amount[4], Head_Name[5], Head_Amount[5], Head_Name[6], Head_Amount[6], Head_Name[7], Head_Amount[7], Head_Name[8], Head_Amount[8], fullnamelbl.Text, NetAmountlbl.Text, payMode, Tinlbl.Text, ViewState["LocationCode"], "A");

        plainText = "1000132|DOM|IN|INR|" + amount.ToString() + "|" + plainText + "|" + ConfigurationManager.AppSettings["SuccessURL"].ToString() + "|" + ConfigurationManager.AppSettings["FailURL"].ToString() + "|SBIEPAY|" + GEN.ToString() + "|1000132|IMPS|ONLINE|ONLINE";

        RemoteClass myremotepost = new RemoteClass();

        ePayEncryptionDecryptionBL objEncry = new ePayEncryptionDecryptionBL();

        string cipherText = objEncry.Encrypt(plainText, "BwmHPemeQsQhpwEGWmyQtQ==", 128);

        EgEChallanBL objEgEChallan = new EgEChallanBL();
        objEgEChallan.BankName = "1000132";
        DataTable bankData = objEgEChallan.GetBankDetail();

        string Address = ConfigurationManager.AppSettings["URL"].ToString();
        myremotepost.Add("encdata", cipherText.ToString());
        myremotepost.Add("merchant_code", bankData.Rows[0][1].ToString());
        myremotepost.Add("URL", bankData.Rows[0][0].ToString());

        myremotepost.Url = Address;
        myremotepost.Post();



    }
}
