using System;
using System.Linq;
using System.Web.UI;
using EgBL;

public partial class WebPages_Admin_EgAddNewOffice : System.Web.UI.Page
{
    EgAddNewOfficeBL objEgAddNewOfficeBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        objEgAddNewOfficeBL = new EgAddNewOfficeBL();
        objEgAddNewOfficeBL.OfficeId = Convert.ToInt32(txtofficeid.Text);
        if (objEgAddNewOfficeBL.CheckOfficeId() == 1)
        {
            lblmsg.Text = "OfficeId is Already Mapped ";
            btnVerify.Visible = true;
            btnSubmit.Text = "Update";
            btnSubmit.Visible = false;
            lblmsg.Visible = true;

            /*Getting detail according office id through Link Server........... */
            GetOfficeData_L_Server();
        }
        else
        {
            btnSubmit.Visible = true;
            lblmsg.Visible = false;
            btnVerify.Visible = false;
            GetOfficeData_L_Server();
            btnSubmit.Text = "Submit";
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        /*Calling Function for Verifying Existing detail according office id through Link Server........... */
        btnSubmit.Visible = true;
        lblmsg.Visible = false;
        GetOfficeData_L_Server();
        btnSubmit.Text = "Update";
    }

    protected void GetOfficeData_L_Server()
    {
        /*Verifying Existing detail according office id through Link Server........... */        
        objEgAddNewOfficeBL = new EgAddNewOfficeBL();
        string[] result = objEgAddNewOfficeBL.getOfficeIdDetails_L_Server(Convert.ToInt32(txtofficeid.Text)).Split('|');
        if (result.Length > 1)
        {
            Fieldset1.Visible = true;

            lblOffice.Text = result[1].ToString().Split('=').Last().ToString();
            lblTreas.Text = result[2].ToString().Split('=').Last().ToString();
            lblDDO.Text = result[3].ToString().Split('=').Last().ToString();
            lblFromdate.Text = result[4].ToString().Split('=').Last().ToString();
            lblpid.Text = result[5].ToString().Split('=').Last().ToString();
            lbldept.Text = result[6].ToString().Split('=').Last().ToString();
        }
        else
        {
            txtofficeid.Text = "";
            btnVerify.Visible = false;
            Fieldset1.Visible = false;
            lblmsg.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewOfficeBL.errMSG + "');", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        /*  working without using webservices and with LinkServer */
        objEgAddNewOfficeBL = new EgAddNewOfficeBL();
        objEgAddNewOfficeBL.OfficeId = Convert.ToInt32(txtofficeid.Text);
        objEgAddNewOfficeBL.InsertOfficeDetail();
        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewOfficeBL.errMSG + "');", true);
        txtofficeid.Text = "";
        btnVerify.Visible = false;
        Fieldset1.Visible = false;
    }
}