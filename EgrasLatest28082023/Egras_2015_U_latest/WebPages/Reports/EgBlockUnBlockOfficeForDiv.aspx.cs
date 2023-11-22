using EgBL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgBlockUnBlockOfficeForDiv : System.Web.UI.Page
{
    EgUnBlockOfficeForDivBL objEgUnBlockOfficeForDivBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\Default.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
    public void BindRepeater()
    {
        objEgUnBlockOfficeForDivBL = new EgUnBlockOfficeForDivBL();
        objEgUnBlockOfficeForDivBL.TreasuryCode = ddlTreasury.SelectedValue;
        DataTable dt = objEgUnBlockOfficeForDivBL.GetOfficenameList();
        if (dt.Rows.Count > 0)
        {
            trrpt.Visible = true;
            rptrUnblockOfficelist.DataSource = dt;
            rptrUnblockOfficelist.DataBind();
        }
        else
        {
            trrpt.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Office to UnBlock.')", true);
        }
    }

    protected void rptrUnblockOfficelist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void rptrUnblockOfficelist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string OfficeCode_Seperated = "";
        foreach (RepeaterItem item in rptrUnblockOfficelist.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chkBox = (CheckBox)item.FindControl("chkBox");
                if (chkBox.Checked == true)
                {
                    OfficeCode_Seperated += chkBox.ToolTip.Split('_').GetValue(0) + "|";
                }
            }
        }
        objEgUnBlockOfficeForDivBL = new EgUnBlockOfficeForDivBL();
        objEgUnBlockOfficeForDivBL.OfficeCodeList = OfficeCode_Seperated;
        objEgUnBlockOfficeForDivBL.UnBlockOffice();
        BindRepeater();
    }
}