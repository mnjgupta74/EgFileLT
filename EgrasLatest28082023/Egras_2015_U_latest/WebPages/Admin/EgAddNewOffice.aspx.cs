using System;
using System.Linq;
using System.Web.UI;
using EgBL;
using System.Data;
using System.Web.UI.WebControls;
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
        string result = objEgAddNewOfficeBL.CheckOfficeId();
        if (result != "-1")
        {
            ViewState["TresuryCode"] = result;
            lblmsg.Text = "OfficeId is Already Mapped ";
            btnVerify.Visible = true;
            btnSubmit.Text = "Update";
            btnSubmit.Visible = false;
            lblmsg.Visible = true;
            tblQuestion.Visible = false;
            tblTreasury.Visible = false;

            /*Getting detail according office id through Link Server........... */
            GetOfficeData_L_Server();
        }
        else
        {
            ViewState["TresuryCode"] = "";
            btnSubmit.Visible = true;
            lblmsg.Visible = false;
            btnVerify.Visible = false;
            GetOfficeData_L_Server();
            btnSubmit.Text = "Submit";

            tblQuestion.Visible = true;
            tblTreasury.Visible = false;

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
            tblQuestion.Visible = false;
            tblTreasury.Visible = false;
            btnshow.Visible = true;
          
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
            tblQuestion.Visible = true;
            //txtofficeid.Text = "";
            btnVerify.Visible = false;
            Fieldset1.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = objEgAddNewOfficeBL.errMSG;
            //btnshow.Visible = false;
            //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objEgAddNewOfficeBL.errMSG + "');", true);
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

    /// <summary>
    /// Add new Method  for Add New Office  which is not in Rajkosh
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkClick_Click(object sender, EventArgs e)
    {
        objEgAddNewOfficeBL = new EgAddNewOfficeBL();
        tblTreasury.Visible = true;

        // DataTable dt = new DataTable();
        //dt = objEgAddNewOfficeBL.FillTreasury();


        //DrpTreasury.DataSource = dt;
        //DrpTreasury.DataTextField = "TreasuryName";
        //DrpTreasury.DataValueField = "TreasuryCode";
        //DrpTreasury.DataBind();
        //DrpTreasury.Items.Insert(0, new ListItem("--Select Treasury--", "0"));

        DropDownListX dp = new DropDownListX();
        DataTable treasuryData = new DataTable();
        treasuryData = dp.FillTreasury();

        for (int i = 1; i < 56; i++)
        {
            var rows = treasuryData.AsEnumerable().Where(t => t.Field<int>("TGroupCode") == i);

            string group = rows.ElementAtOrDefault(0).Field<string>("TreasuryName"); // ElementAtOrDefault(0).treasuryName.ToString().Trim();
            ddllocation.AddItemGroup(group.Trim());
            foreach (var item in rows)
            {
                ListItem items = new ListItem(item.Field<string>("TreasuryName"), item.Field<string>("TreasuryCode"));
                ddllocation.Items.Add(items);

            }

        }
        if (ViewState["TresuryCode"].ToString() != "")
        {
            ddllocation.SelectedValue = ViewState["TresuryCode"].ToString();
            btnAddTreasury.Text = "Update Treasury";
            tblQuestion.Visible = false;
            tblTreasury.Visible = true;

        }
        else
        {
            ddllocation.SelectedValue = "0";
            tblQuestion.Visible = false;
            tblTreasury.Visible = true;
            btnAddTreasury.Text = "Add Treasury";
        }
    }

    protected void btnAddTreasury_Click(object sender, EventArgs e)
    {
        objEgAddNewOfficeBL = new EgAddNewOfficeBL();
        objEgAddNewOfficeBL.OfficeId = Convert.ToInt32(txtofficeid.Text);
        objEgAddNewOfficeBL.TreasuryCode = ddllocation.SelectedValue;
        
        if(ViewState["TresuryCode"].ToString() != "")
        {
            objEgAddNewOfficeBL.flag = 2;
        }
        else
        {
            objEgAddNewOfficeBL.flag = 1;
        }
        if (ddllocation.SelectedValue != "0")
        {
            int a = objEgAddNewOfficeBL.InsertTreasuryDetail();
            if (a == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Treasury Map With Office " + txtofficeid.Text + " Successfully !!');", true);
                tblTreasury.Visible = false;
                tblQuestion.Visible = false;
                lblmsg.Visible = false;
                ViewState["TresuryCode"] = "";
                txtofficeid.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Treasury Updated With Office Successfully !!');", true);
                tblTreasury.Visible = false;
                tblQuestion.Visible = false;
                lblmsg.Visible = false;
                ViewState["TresuryCode"] = "";
                txtofficeid.Text = "";

            }
        }
        else
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select Treasury Code');", true);
        
    }
}