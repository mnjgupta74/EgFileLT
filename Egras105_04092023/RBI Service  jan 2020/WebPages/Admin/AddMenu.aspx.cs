using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_Admin_AddMenu : System.Web.UI.Page
{
    EgAddMenu objEgAddMenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            ddlChildMenu.Visible = false;
            objEgAddMenu = new EgAddMenu();
            objEgAddMenu.GetRootMenuItems(ddlMenuParentID);
            BindGrid();
        }
        else
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string filePath, fileName, filename1;

        if (Page.IsValid)
        {
            objEgAddMenu = new EgAddMenu();
            objEgAddMenu.MenuDesc = txtMenuDesc.Text.Trim();
         
                if (ddlChildMenu.SelectedValue!="0" && ddlChildMenu.SelectedValue !="")
                {
                    objEgAddMenu.MenuParentId = Convert.ToInt32(ddlChildMenu.SelectedValue);
                }
                else
                {
                    objEgAddMenu.MenuParentId = Convert.ToInt32(ddlMenuParentID.SelectedValue);
                }
            
            //if (ddlMenuSecure.SelectedItem.Text.Trim().ToUpper() == "Y")
            //{
            //    objEgAddMenu.NavigateUrl = "../WebPages/Account/EgDoubleVerification.aspx";
                objEgAddMenu.ActualNavigateUrl = Convert.ToString(txtNavigateURL.Text.Trim());
            //}
            //else
            //{
            //    objEgAddMenu.NavigateUrl = Convert.ToString(txtNavigateURL.Text.Trim());
            //}

            #region FileUpload Control for Navigate URL
            //if (FileUploadUrl.HasFile)
            //{
            //    fileName = FileUploadUrl.FileName;
            //    String path = HttpContext.Current.Request.PhysicalApplicationPath + FileUploadUrl.FileName;
            //    filename1 = Path.GetDirectoryName(FileUploadUrl.PostedFile.FileName); // file name with path.
            //    filePath = Server.MapPath(FileUploadUrl.FileName);
            //    if (filePath.Contains("WebPages"))
            //    {
            //        if (filePath.Contains("Accounts"))
            //        {
            //            filePath = "../WebPages/Accounts/" + fileName;
            //        }
            //        else if (filePath.Contains("Admin"))
            //        {
            //            filePath = "../WebPages/Admin/" + fileName;
            //        }
            //        else if (filePath.Contains("AG"))
            //        {
            //            filePath = "../WebPages/AG/" + fileName;
            //        }
            //        else if (filePath.Contains("charts"))
            //        {
            //            filePath = "../WebPages/charts/" + fileName;
            //        }
            //        else if (filePath.Contains("Department"))
            //        {
            //            filePath = "../WebPages/Department/" + fileName;
            //        }
            //        else if (filePath.Contains("TO"))
            //        {
            //            filePath = "../WebPages/TO/" + fileName;
            //        }
            //        else
            //        {
            //            filePath = "../WebPages/" + fileName;
            //        }
            //    }
            //    else
            //    {
            //        filePath = "../" + fileName;
            //    }

            #endregion
            string selectedItems = String.Join(",", chkUserType.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));
            objEgAddMenu.UserType = selectedItems;
            objEgAddMenu.UserId = Convert.ToInt32(Session["UserID"]);
            objEgAddMenu.ModuleId = Convert.ToInt32(txtModeuleID.Text.Trim());
            objEgAddMenu.MenuVisible = Convert.ToChar(ddlMenuVisible.SelectedItem.Text);
            objEgAddMenu.MenuSecured = Convert.ToChar(ddlMenuSecure.SelectedItem.Text);

            objEgAddMenu.OrderId = Convert.ToInt32(txtOrderID.Text.Trim());
            if (btnSubmit.Text == "Submit")
            {
                int result = objEgAddMenu.InsertMenuDetail();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Saved Successfully.! ')", true);
                    BindGrid();
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not saved.! ')", true);
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                objEgAddMenu.menuid = Convert.ToInt32(ViewState["MenuId"]);
                int result = objEgAddMenu.UpdateMenuDetail();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Saved Successfully.! ')", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not saved.! ')", true);
                }
            }
            //BindGrid();
            ResetControl();
        }
    }

    public void ResetControl()
    {
        txtMenuDesc.Text = "";
        txtNavigateURL.Text = "";
        txtModeuleID.Text = "";
        ddlMenuParentID.SelectedIndex = -1;
        ddlMenuVisible.SelectedIndex = -1;
        txtOrderID.Text = "";
        btnSubmit.Text = "Submit";
      
        ddlChildMenu.Visible = false;
        ddlMenuSecure.SelectedValue = "0";
        chkUserType.ClearSelection();
    }

    protected void ddlMenuParentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        EgAddMenu objEgAddMenu = new EgAddMenu();
        objEgAddMenu.MenuParentId = Convert.ToInt32(ddlMenuParentID.SelectedItem.Value);
        DataTable dt = objEgAddMenu.GetChildMenuItems();
        if (dt.Rows.Count > 0)
        {
            ddlChildMenu.DataSource = dt;
            ddlChildMenu.DataTextField = "MenuDesc";
            ddlChildMenu.DataValueField = "MenuId";
            ddlChildMenu.DataBind();
            ListItem l = new ListItem("--Select menu--", "0", true); l.Selected = true;
            ddlChildMenu.Items.Add(l);
            //ddlChildMenu.Items.Insert(0, new ListItem("--Select menu--", "0"));
            
            ddlChildMenu.Visible = true;
            //btnGetmoreitems.Visible = true;
        }
    }
    

    public void BindGrid()
    {
        objEgAddMenu = new EgAddMenu();

        objEgAddMenu.gridMenuItems(gridMenuDetail);
        ResetControl();
    }


    protected void btnGvEdit_Click(object sender, EventArgs e)
    {
        objEgAddMenu = new EgAddMenu();

        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        int MenuId = Convert.ToInt32(gridMenuDetail.DataKeys[row.RowIndex]["MenuId"].ToString());
        txtMenuDesc.Text = row.Cells[1].Text;
        txtNavigateURL.Text = row.Cells[2].Text;
        txtModeuleID.Text = row.Cells[4].Text;
        txtOrderID.Text = row.Cells[5].Text;
        if((row.Cells[6].Text).ToString().ToUpper()== "Y")
            ddlMenuVisible.SelectedValue = "1";
        else
            ddlMenuVisible.SelectedValue = "2";
        var result = (ddlMenuParentID.Items.FindByValue((row.Cells[3].Text).ToString()));
        if (result != null)
        {
            if (ddlMenuParentID.Items.FindByText(new ListItem(result.ToString()).ToString()) != null)
            {
                ddlMenuParentID.SelectedValue = ((row.Cells[3].Text).ToString());
                ddlMenuParentID.AutoPostBack = true;
            }
            objEgAddMenu.MenuParentId = Convert.ToInt32(ddlMenuParentID.SelectedValue);
            DataTable dt = objEgAddMenu.GetChildMenuItems();
            if (dt.Rows.Count > 0)
            {
                ddlChildMenu.DataSource = dt;
                ddlChildMenu.DataTextField = "MenuDesc";
                ddlChildMenu.DataValueField = "MenuId";
                ddlChildMenu.DataBind();
                ddlChildMenu.Items.Insert(0, new ListItem("--Select menu--", "0"));

                ddlChildMenu.Visible = true;
                //btnGetmoreitems.Visible = true;
            }
            ddlChildMenu.SelectedValue = "0";
            //ddlChildMenu.Visible = false;
        }
        else
        {
            
            objEgAddMenu.menuid = Convert.ToInt32(row.Cells[8].Text);
            string res= objEgAddMenu.GetMenuVal();

            ddlMenuParentID.SelectedValue = res.Split('|').GetValue(0).ToString();
            //CreateDropdown("ddltest1");
            objEgAddMenu.MenuParentId = Convert.ToInt32(ddlMenuParentID.SelectedValue);
            DataTable dt = objEgAddMenu.GetChildMenuItems();
            if (dt.Rows.Count > 0)
            {
                ddlChildMenu.DataSource = dt;
                ddlChildMenu.DataTextField = "MenuDesc";
                ddlChildMenu.DataValueField = "MenuId";
                ddlChildMenu.DataBind();
                ddlChildMenu.Items.Insert(0, new ListItem("--Select menu--", "0"));
               
                ddlChildMenu.Visible = true;
                //btnGetmoreitems.Visible = true;
            }

            ddlChildMenu.SelectedValue = res.Split('|').GetValue(1).ToString();
        }
        string[] chkitems = (row.Cells[7].Text).Split(',');
        String str = string.Empty;
       
        foreach (ListItem item in chkUserType.Items)
        {
            if (chkitems.Contains(item.Value))
            {
                item.Selected = true;
            }
        }

       
        btnSubmit.Text = "Update";
        ViewState["MenuId"] = Convert.ToString(MenuId);
    }


}
