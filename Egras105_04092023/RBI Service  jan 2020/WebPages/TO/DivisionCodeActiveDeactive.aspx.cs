using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_TO_DivisionCodeActiveDeactive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.FillTreasury(ddlTreasury);
        }
    }

    protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.TreasuryCode = ddlTreasury.SelectedValue;
        objHeadActiveDeactiveBL.FillOfficeList(ddlOffice);
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindDivision();
        btnActionSubmit();
    }
    public void BindDivision()
    {
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.TreasuryCode = ddlTreasury.SelectedValue;
        objHeadActiveDeactiveBL.OfficeId = Convert.ToInt32(ddlOffice.SelectedValue);
        objHeadActiveDeactiveBL.FillDivisionCodeRpt(rptDivActive);
    }
    //34414866  office 
    protected void rptDivActive_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.TreasuryCode = ddlTreasury.SelectedValue;
        objHeadActiveDeactiveBL.OfficeId = Convert.ToInt32(ddlOffice.SelectedValue);
        if (e.CommandName == "Ac")
        {
            Label Label1 = (Label)e.Item.FindControl("Label1");
            if (Label1.Text == "A")
            {
                objHeadActiveDeactiveBL.DivisionActiveDeactive(e.CommandArgument.ToString(), "D");
                BindDivision();
            }
            else
            {
                objHeadActiveDeactiveBL.DivisionActiveDeactive(e.CommandArgument.ToString(), "A");
                BindDivision();
            }

        }
    }
    public void btnActionSubmit()
    {
        ddlTreasury.Enabled = false;
        ddlOffice.Enabled = false;
        btnShow.Enabled = false;
        btnreset.Enabled = true;
    }
    public void btnActionReset()
    {
        ddlTreasury.Enabled = true;
        ddlOffice.Enabled = true;
        btnShow.Enabled = true;
        btnreset.Enabled = false;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        btnActionReset();
        rptDivActive.DataSource = null;
        rptDivActive.DataBind();
    }
}