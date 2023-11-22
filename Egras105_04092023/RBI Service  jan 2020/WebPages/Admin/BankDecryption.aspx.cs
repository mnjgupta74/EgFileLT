using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EgBL;
using System.IO;
using System.Collections.Generic;

public partial class WebPages_Admin_BankDecryption : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Write("<Script>alert('Session Expired')</Script>");
            Response.Redirect("~\\Default.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.GetBanks(ddlbankname);

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            string Key_Code = ddlbankname.SelectedValue;
            string result = "";
            string textToDecrypt = txtEncrypt.Text.ToString().Trim();
            if (Key_Code != "" && rblBankMerchant.SelectedValue == "B")
            {
                Dictionary<string, string> bankCodeList = new Dictionary<string, string>();
                XDocument ooo = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Code/BankNames_BSRCode.xml"));
                foreach (XElement rootElement in ooo.Descendants().Where(p => p.HasElements == false))
                {
                    string value = rootElement.Value.ToString().Trim();
                    String KName = value.Split('|').GetValue(0).ToString();
                    string Kvalue = value.Split('|').GetValue(1).ToString();
                    bankCodeList.Add(KName, Kvalue);
                }

                result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + bankCodeList[Key_Code] + ".key");
            }
            else if (Key_Code != "" && rblBankMerchant.SelectedValue == "V")
            {
                result = objEncry.Decrypt(textToDecrypt, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + Key_Code + ".key");
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Text not matched');", true);

            txtDecrypt.Text = result;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error in Decrypting String');", true);
        }
    }
    protected void rblBankMerchant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblBankMerchant.SelectedValue == "B")
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.GetBanks(ddlbankname);
        }
        else
        {
             EgEChallanBL objEChallan = new EgEChallanBL();
             objEChallan.GetMerchant(ddlbankname);
        }
    }
}
