using System;
using System.Web.UI;
using EgBL;
using System.Data;

public partial class WebPages_Admin_EgChangeUserMobileNo : System.Web.UI.Page
{
    EgChangePasswordBL objEgChangePasswordBL = new EgChangePasswordBL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void lnkChangeMobileNo_Click(object sender, EventArgs e)

    {

        if (txtMobileNumber.Text == txtConfirmMobileNumber.Text && txtMobileNumber.Text.Length==10)
        {
            objEgChangePasswordBL.LoginID = ViewState["LoginID"].ToString();
            objEgChangePasswordBL.MobileNumber = Convert.ToInt64(txtMobileNumber.Text);

            
            int returnvalue = objEgChangePasswordBL.UpdateMobileNumber();
            if (returnvalue == 1){
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Mobile Number Changed Successfully!  Mobile No is " + txtMobileNumber.Text + " ')", true);
                Reset();

            }
            else if (returnvalue == 3){
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Mobile Number Already Associated  !')", true);
                Reset();
            }
            else{
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Mobile Number Could not be Change !')", true);
                Reset();
                }
        }
        else{
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Mobile Number is Not Same Or Moblie No Should be 10 Digit !')", true);
            Reset();
        }

    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
          txtlogin.Text = "";
          txtlogin.Enabled = true;
          Fieldset1.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (txtlogin.Text != "") {

            objEgChangePasswordBL.LoginID = txtlogin.Text;
            ViewState["LoginID"] = txtlogin.Text;
            dt = objEgChangePasswordBL.GetChangeUserPasswordDetails();
                if (dt.Rows.Count > 0) { 
                   
                    Fieldset1.Visible = true;
                    lblDOB.Text = dt.Rows[0][0].ToString();
                    lblEmail.Text = dt.Rows[0][1].ToString();
                    lblMobile.Text = dt.Rows[0][2].ToString();
                    lblAddress.Text = dt.Rows[0][3].ToString();
                    LabelQuestion.Text = dt.Rows[0][5].ToString();
                    LabelAnswer.Text = dt.Rows[0][6].ToString();
                    lbluserid.Text = dt.Rows[0][4].ToString();
                    btnreset.Visible = true;
                    txtlogin.Enabled = false;
                }
                else{
                    Fieldset1.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Login Id Not Found')", true);
                 }

            }
            else {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Enter LoginID')", true);
                 }
        
        dt.Dispose();
    }

    private void Reset()
    {

        txtlogin.Text = "";
        txtMobileNumber.Text = "";
        txtConfirmMobileNumber.Text = "";
        Fieldset1.Visible = false;
        btnreset.Visible = false;
        txtlogin.Enabled = true;
    }
}