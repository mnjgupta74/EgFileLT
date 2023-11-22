using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Admin_EgAddDivisionMasterNew : System.Web.UI.Page
{
    egAddDivisionBL ObjDivision;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tdnewoffice.Visible = false;
            trdrpdiv.Visible = false;
            btnAdd.Visible = false;
            btnSubmit.Visible = false;
            ObjDivision = new egAddDivisionBL();
            ObjDivision.FillTreasury(drpTreasury);
            ObjDivision.GetDepartmentListForOfficeMap(drpDepartment);
            EgBankSoftCopyBL objEgBankSoftCopyBL = new EgBankSoftCopyBL();
            objEgBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserID"]);
            drpTreasury.SelectedValue = objEgBankSoftCopyBL.GetBSRCode().Trim();
            drpTreasury.Enabled = false;
        }
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjDivision = new egAddDivisionBL();
        ObjDivision.TreasuryCode = drpTreasury.SelectedValue;
        ObjDivision.DeptCode = Convert.ToInt32(drpDepartment.SelectedValue);

        if (rbldivtype.SelectedValue == "1")
        {
            DataTable dt1 = ObjDivision.GetOfficeListForDivision();
            if (dt1.Rows.Count > 0)
            {
                chksel.DataSource = dt1;
                chksel.DataTextField = "OfficeName";
                chksel.DataValueField = "OfficeId";
                chksel.DataBind();
                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    chksel.Items[i].Selected = true;
                //}
                chksel.Visible = true;
                PanelSelected.Visible = true;
                lblofc.Visible = true;

            }
            else {
                chksel.DataSource = null;
                chksel.Visible = false;
                PanelSelected.Visible = false;
                lblofc.Visible = false;
            }
            btnAdd.Visible = true;
            dt1.Clear();

        }
        else
        {

            ObjDivision.GetDiviSionListMaster(drpdivision);
            btnAdd.Visible = false;
        }

        
    }
    protected void drpdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpdivision.SelectedValue != "0")
        {
            chksel.Items.Clear();
            ObjDivision = new egAddDivisionBL();
            ObjDivision.TreasuryCode = drpTreasury.SelectedValue;
            ObjDivision.DeptCode = Convert.ToInt32(drpDepartment.SelectedValue);
            string[] divcode = drpdivision.SelectedItem.Text.Split('|');
            ObjDivision.DivisionCode = divcode[2].ToString();
            //ObjDivision.DivisionCode = (drpdivision.SelectedValue);
            if (rbldivtype.SelectedValue == "2")
            {
                DataTable dt1 = ObjDivision.GetOfficeListForSubDivision();
                if (dt1.Rows.Count > 0)
                {
                    chksel.DataSource = dt1;
                    chksel.DataTextField = "OfficeName";
                    chksel.DataValueField = "OfficeId";
                    chksel.DataBind();
                    //for (int i = 0; i < dt1.Rows.Count; i++)
                    //{
                    //    chksel.Items[i].Selected = true;
                    //}
                    chksel.Visible = true;
                    PanelSelected.Visible = true;
                    lblofc.Visible = true;

                }
                else
                {
                    chksel.DataSource = null;
                    chksel.Visible = false;
                    PanelSelected.Visible = false;
                    lblofc.Visible = false;
                }
                dt1.Clear();
            }
        }
        else {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Subdivision')", true);
            return;
        }
        //btnSubmit.Visible = true;
    }
    protected void chksel_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        lstSelected.Items.Clear();
        foreach (ListItem li in chksel.Items)
        {
            if (li.Selected)
            {
                lstSelected.Items.Add(li.Text);
            }
        }
        if (lstSelected.Items.Count > 0)
        {
            btnSubmit.Visible = true;
            lstSelected.Visible = true;
            lblsel.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
            lstSelected.Visible = false;
            lblsel.Visible = false;
        }
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        DataTable SelectedOfficeList = new DataTable();
        SelectedOfficeList.Columns.Add("OfficeId");
        DataTable RemoveOfficeList = new DataTable();
        RemoveOfficeList.Columns.Add("OfficeId");
        ObjDivision = new egAddDivisionBL();
        ObjDivision.TreasuryCode = drpTreasury.SelectedValue.ToString();
        ObjDivision.DeptCode = Convert.ToInt32(drpDepartment.SelectedValue);
        ObjDivision.type = Convert.ToInt16(rbldivtype.SelectedValue);
        foreach (ListItem li in chksel.Items)
        {
            if (li.Selected)
            {
                DataRow drremoveofc = RemoveOfficeList.NewRow();
                string[] A = li.Value.Split('-');
                drremoveofc[0] = A[0].ToString();
                RemoveOfficeList.Rows.Add(drremoveofc);
            }
            else
            {
                DataRow drSelectofc = SelectedOfficeList.NewRow();
                string[] B = li.Value.Split('-');
                drSelectofc[0] = B[0].ToString();
                SelectedOfficeList.Rows.Add(drSelectofc);
            }
        }
        ObjDivision.SelectOfficeList = SelectedOfficeList; // unchecked items in office list Remaining as it is.(Remaining items to be added later)
        ObjDivision.RemoveOfficeList = RemoveOfficeList;   // Selected office to add in division master

        if (rbldivtype.SelectedValue == "2")
        {
            if (Convert.ToInt16(drpdivision.SelectedValue) > 0)
            {

                ObjDivision.DivisionCode = drpdivision.SelectedValue.ToString();

                string[] drp = drpdivision.SelectedItem.Text.ToString().Split('|');
                ObjDivision.DivisionName = drp[1].ToString();
                ObjDivision.AgOfficeId = drp[2].ToString();
                // remove Item
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select division code')", true);
                return;
            }
        }
            int output = ObjDivision.InsertNewDivisionMaster();
            if (output > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Update Successfully')", true);
                drpdivision.Items.Clear();
                lstSelected.Items.Clear();
                lstSelected.Visible = false;
                lblsel.Visible = false;
                PanelSelected.Visible = false;
                lblofc.Visible = false;
                //drpTreasury.SelectedValue = "0";
                drpDepartment.SelectedValue = "0";
            btnSubmit.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Not Saved')", true);
            }
        
    }

    protected void rbldivtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblofc.Visible = false;
        lblsel.Visible = false;

        PanelSelected.Visible = false;
        chksel.Items.Clear();
        chksel.Visible = false;
        lstSelected.Items.Clear();
        lstSelected.Visible = false;
        btnSubmit.Visible = false;
        btnAdd.Visible = false;
        drpDepartment.SelectedValue = "0";
        tdnewoffice.Visible = false;
        btnAdd.Text = "Add New Office if Not Present";
        if (rbldivtype.SelectedValue == "2")
        {
            trdrpdiv.Visible = true;
            trNewOffice.Visible = false;
        }
        else
        {
            trdrpdiv.Visible = false;
            trNewOffice.Visible = true;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "Add Office")
        {
            if (txtAddOffice.Text.Trim() != "")
            {
                tdnewoffice.Visible = false;

                egAddDivisionBL objaddbl = new egAddDivisionBL();
                objaddbl.OfficeId = txtAddOffice.Text;
                objaddbl.DeptCode = Convert.ToInt32(drpDepartment.SelectedValue);
                objaddbl.TreasuryCode = (drpTreasury.SelectedValue);

                string res = objaddbl.GetOfficeId();
                if (res == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Office Added Successfully')", true);
                }
                else if (res == "2")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Office Id does not exist')", true);
                }
                else if (res == "3")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Office Id does not mapped with egras')", true);
                }
                else if (res == "4")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Office Id Already Exists')", true);
                }

                drpDepartment.SelectedValue = "0";
                btnAdd.Text = "Add New Office if Not Present";
            }
            else { 
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Enter Office Id')", true);
            }
        }
        else
        {
            tdnewoffice.Visible = true;
            btnAdd.Text = "Add Office";
            btnSubmit.Visible = false;
            lblofc.Visible = false;
            lblsel.Visible = false;

            PanelSelected.Visible = false;
            chksel.Items.Clear();
            chksel.Visible = false;
            lstSelected.Items.Clear();
            lstSelected.Visible = false;
        }
    }
}