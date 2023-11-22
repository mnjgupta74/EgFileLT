using System;
using System.Data;
using System.Web.UI;
using EgBL;
using System.Web.UI.WebControls;

public partial class WebPages_Admin_EgBankResponseAudit : System.Web.UI.Page
{
    EGBankDecrypt objEGBankDecrypt;
    string Bank_Code;
    string result;
    DataTable Responsedt;// = new DataTable();
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

    protected void btnclick_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        objEGBankDecrypt = new EGBankDecrypt();
        string[] fromdate = txtFromdate.Text.Trim().Replace("-", "/").Split('/');
        objEGBankDecrypt.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtTodate.Text.Trim().Replace("-", "/").Split('/');
        objEGBankDecrypt.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEGBankDecrypt.bankCode = ddlbankname.SelectedValue.ToString();

        if (rdtype.SelectedValue == "1") // for E service
        {
            dt = objEGBankDecrypt.FillEncryptedStringEservice();
        }
        else // for Bank Response
        {
            dt = objEGBankDecrypt.FillEncryptedStringBankResponse();
        }
      
       
        Bank_Code = ddlbankname.SelectedValue.ToString();

        DataTable Responsedt = CreateDataSource();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            AddRow(GetDecryptString(dt.Rows[i]["EncryptedData"].ToString()), Responsedt);
        }

        ViewState["Responsedt"] = null;
        ViewState["Responsedt"] = Responsedt;
        rowGrid.Visible = true;
        grdDecryptedString.DataSource = Responsedt;
        grdDecryptedString.DataBind();
    }

    public DataTable CreateDataSource()
    {
        Responsedt = new DataTable();
        Responsedt.Columns.Add("Response", typeof(string));
        return Responsedt;
    }

    public void AddRow(string response, DataTable dt)
    {
        dt.Rows.Add(new object[] { response });
        dt.AcceptChanges();
    }

    protected string GetDecryptString(string text)
    {
        SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
        switch (Bank_Code)
        {
            case "0200113":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"BOB.key");
                break;
            case "0292861":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"Union_Bank.key");
                break;
            case "0006326":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                break;
            case "0171051":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"RAJASTHAN_EGRASS.key");
                break;
            case "0304017":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"PNB.key");
                break;
            case "6910213":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"IDBI.key");
                break;
            case "0280429":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"CBI.key");
                break;
            case "0361193":
                result = objEncry.Decrypt(text, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"OBC.key");
                break;
            case "1000132":
                ePayEncryptionDecryptionBL objEpay = new ePayEncryptionDecryptionBL();
                result = objEpay.Decrypt(text, "BwmHPemeQsQhpwEGWmyQtQ==", 128);
                break;
            default:
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('BSR not matched');", true);
                break;
        }
        return result;

    }


    protected void grdDecryptedString_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDecryptedString.PageIndex = e.NewPageIndex;

        if (ViewState["Responsedt"] != null && ViewState["Responsedt"].ToString() != "")
        {
            grdDecryptedString.DataSource = ViewState["Responsedt"];
            grdDecryptedString.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Submit Decrypt Details !');", true);
        }
    }
}

