using System;
using System.Data;
using System.Linq;
using EgBL;
using System.Web.UI;

public partial class WebPages_Admin_EgAddDDONo : System.Web.UI.Page
{
    EgAddNewDDOCodeBL objEgAddNewDDOCode;//= new EgAddNewDDOCodeBL();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            txtddocode.Text = "";
            txtptreascode.Text = "";
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            showDetail();
        }
    }

    public void showDetail()
    {
        objEgAddNewDDOCode = new EgAddNewDDOCodeBL();
        btnSubmit.Visible = true;
        objEgAddNewDDOCode.ddo_code = Convert.ToInt32(txtddocode.Text);
        objEgAddNewDDOCode.treas_code = txtptreascode.Text.ToString().Trim();
        objEgAddNewDDOCode.EgGetDDOCodeDetails();

        if (objEgAddNewDDOCode.chkflag == "3")
        {
            Fieldset1.Visible = true;
            btnSubmit.Text = "Submit";
            InsertData();
        }

        else
        {
            Fieldset1.Visible = true;
            btnSubmit.Enabled = true;
            btnSubmit.Text = "ReVerify";
        }



        lblddoName.Text = objEgAddNewDDOCode.ddo_code + "-" + objEgAddNewDDOCode.officename;
        lblTreas.Text = objEgAddNewDDOCode.treas_code;
        lblFlag.Text = objEgAddNewDDOCode.Status;
        lblmsg.Text = objEgAddNewDDOCode.errMSG;
    }

    protected void InsertData()
    {
        objEgAddNewDDOCode = new EgAddNewDDOCodeBL();
        string str = objEgAddNewDDOCode.getddoCodeDetails_L_Server(Convert.ToInt32(txtddocode.Text), txtptreascode.Text.ToString().Trim());
        objEgAddNewDDOCode.StrString = str;
        objEgAddNewDDOCode.InsertProperty();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /*  working without using webservices and with LinkServer */
            //-- calling Insert Detail--> sp - "EgInsertDDOCodeDetail"
            objEgAddNewDDOCode = new EgAddNewDDOCodeBL();
            objEgAddNewDDOCode.treas_code = txtptreascode.Text.ToString().Trim();
            objEgAddNewDDOCode.ddo_code = Convert.ToInt32(txtddocode.Text);
            objEgAddNewDDOCode.InsertDDODetail();
            btnSubmit.Visible =objEgAddNewDDOCode.flag==3?false:true;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewDDOCode.errMSG + "');", true);
            
        }
        catch (Exception ex)
        {
            lblmsg.Text = "<B>Data Not Saved </br>" + ex.Message.ToString() + "</B>";
            lblmsg.Visible = true;
        }
    }

}
