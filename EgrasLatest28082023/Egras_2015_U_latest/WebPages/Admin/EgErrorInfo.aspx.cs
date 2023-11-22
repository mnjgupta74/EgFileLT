using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EgErrorInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");

        }
        if (!IsPostBack)
        {
            Table2.Visible = false;
            txtDecrypt.Visible = false;
        }        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    private void BindRepeater()
    {
        DataTable dt = new DataTable();
        EgLoginAuditBL objEgErrorInfo = new EgLoginAuditBL();
        string[] fromDate = txtFromDate.Text.Split('/');
        objEgErrorInfo.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = txtToDate.Text.Split('/');
        objEgErrorInfo.ToDate  = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
        if (rbltype.SelectedValue == "1")
        {
            dt = objEgErrorInfo.ErrorReport();
            txtDecrypt.Visible = false;
        }
        if (rbltype.SelectedValue == "2")
        {
            dt = objEgErrorInfo.BankServiceAudit();
            txtDecrypt.Visible = true;
        }
        if (rbltype.SelectedValue == "3")
        {
            dt = objEgErrorInfo.BankResponseAudit();
            txtDecrypt.Visible = true;
        }
        if (rbltype.SelectedValue == "4")
        {
            dt = objEgErrorInfo.EgrasServiceAudit();
            txtDecrypt.Visible = true;
        }
        if (dt.Rows.Count == 0)
        {
            lblEmptyData.Visible = true;
            lblEmptyData.Text = "No Record Found.";
            Table2.Visible = false;
        }
        else 
        {
            lblEmptyData.Visible = false;
            Table2.Visible = true;
            rptErrorInfo.DataSource = dt;
            rptErrorInfo.DataBind();

        }       
    }


    /// <summary>
    /// For Repeat and View for Challan
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptErrorInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Decrypt"))
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            string Bank_Code = Convert.ToString(e.CommandArgument);
            string result = "";
            Label lblValue = e.Item.FindControl("lblError") as Label;
            string textToDecrypt = lblValue.Text.ToString();
            switch (Bank_Code)
            {
                case "0200113":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                    break;
                case "0292861":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                    break;
                case "0006326":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    break;
                case "0171051":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                    break;
                case "0304017":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                    break;
                case "6910213":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                    break;
                case "0280429":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                    break;
                case "0361193":
                    result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                    break;
                case "1000132":
                    ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
                    result = objEpay.Decrypt(textToDecrypt, "BwmHPemeQsQhpwEGWmyQtQ==", 128);
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('BSR not matched');", true);
                    break;
            }
            txtDecrypt.Text = result;

        }
    }
}
