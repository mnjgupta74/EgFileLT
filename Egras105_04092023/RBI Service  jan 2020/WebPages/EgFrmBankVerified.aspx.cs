using System;
using EgBL;
using System.Web.UI;

public partial class WebPages_EgFrmBankVerified : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        EgFrmBankVerified objEgFrmBankVerified = new EgFrmBankVerified();
        objEgFrmBankVerified.GRN = Convert.ToInt64(txtGRN.Text);
        objEgFrmBankVerified.PaymentType = Online_ManualRadioButton.SelectedValue;
        bool result = objEgFrmBankVerified.GetBankGRNDetails();
        if (!objEgFrmBankVerified.Isvalid)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid GRN or GRN does not belong to your bank.');", true);
            return;
        }
        if (!result)
        {
            VerifiedBankClass objVerifiedBankClass = new VerifiedBankClass();
            objVerifiedBankClass.BSRCode = objEgFrmBankVerified.BankCode;
            objVerifiedBankClass.GRN = objEgFrmBankVerified.GRN;
            objVerifiedBankClass.PaymentMode = Online_ManualRadioButton.SelectedValue;
            string Message = objVerifiedBankClass.Verify();
            switch (Message)
            {
                case "Status updated as successfull ":
                    lblStatus.Text = "Success";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                case "Status updated as Pending ":
                    lblStatus.Text = "Pending";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                case "Status updated as Unsuccessfull ":
                    lblStatus.Text = "Failed";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, GetType(), "MSG", "myAlert('Alert','" + Message + "');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + Message + "');", true);
                    break;
            }
        }
        else
        {
            switch (objEgFrmBankVerified.Status)
            {
                case "S":
                    lblStatus.Text = "Success";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                case "P":
                    lblStatus.Text = "Pending";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                case "F":
                    lblStatus.Text = "Failed";
                    txtGRN.Enabled = false;
                    Online_ManualRadioButton.Enabled = false;
                    trStatus.Visible = true;
                    break;
                default:
                    ScriptManager.RegisterStartupScript(this, GetType(), "MSG", "myAlert('Alert','" + "Invalid Request" + "');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid Request');", true);
                    break;
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtGRN.Text = "";
        lblStatus.Text = "";
        txtGRN.Enabled = true;
        Online_ManualRadioButton.Enabled = true;
        trStatus.Visible = false;
    }
}