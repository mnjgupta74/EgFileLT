using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using EgBL;
public partial class WebPages_TO_EgAddNodalPerson : System.Web.UI.Page
{
    EgAddNodalPersonBL objEgAddNodalPersonBL;
    static DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {

            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEgAddNodalPersonBL = new EgAddNodalPersonBL();
            objEgAddNodalPersonBL.DepartmentListNamewise(ddlDepartment);
            EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            DtSearch = objUserProfileBL.GetDeptList();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
             objEgAddNodalPersonBL = new EgAddNodalPersonBL();
            objEgAddNodalPersonBL.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
            objEgAddNodalPersonBL.NodalName = txtNodelName.Text.Trim();
            if (txtNumber2.Text != "")
                objEgAddNodalPersonBL.Number = txtNumber.Text.Trim() + ',' + txtNumber2.Text.Trim();
            else
                objEgAddNodalPersonBL.Number = txtNumber.Text.Trim();
            objEgAddNodalPersonBL.Address = txtAddress.Text.Trim();
            objEgAddNodalPersonBL.EmailID = txtEmail.Text.Trim();
            if (btnSubmit.Text == "Submit")
            {
                int result = objEgAddNodalPersonBL.InsertNodalData();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Saved Successfully.! ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not saved.! ')", true);
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                objEgAddNodalPersonBL.Nid = Convert.ToInt32(ViewState["Code"]);
                int result = objEgAddNodalPersonBL.UpdateNodalData();
                if (result == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record Updated Successfully.! ')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not Updated.! ')", true);
                }

            }
            ResetControl();
            BindGrid();
        }
    }
    public void ResetControl()
    {
        //ddlDepartment.SelectedValue = "0";
        txtNodelName.Text = "";
        txtNumber.Text = "";
        txtNumber2.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        btnSubmit.Text = "Submit";
        txtDepartment.Text = "";

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlDepartment.SelectedValue = "0";
        ResetControl();
        BindGrid();

    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
         objEgAddNodalPersonBL = new EgAddNodalPersonBL();
         objEgAddNodalPersonBL.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
         dt= objEgAddNodalPersonBL.NodalGrid();
         gridNodal.DataSource = dt;
         gridNodal.DataBind();
         dt.Dispose();
         ResetControl();
    }
    public void BindGridWithNoReset()
    {
        DataTable dt = new DataTable();
        objEgAddNodalPersonBL = new EgAddNodalPersonBL();
        objEgAddNodalPersonBL.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
        dt= objEgAddNodalPersonBL.NodalGrid();
        gridNodal.DataSource = dt;
        gridNodal.DataBind();
        dt.Dispose();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        txtDepartment.Text = ddlDepartment.SelectedItem.Text;
    }
    protected void btnGvEdit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        int Nid = Convert.ToInt32(gridNodal.DataKeys[row.RowIndex]["Nodalid"].ToString());//row.Cells[1].Text;
        txtNodelName.Text = row.Cells[1].Text;
        if (row.Cells[2].Text.Contains(',') == true)
        {
            txtNumber.Text = row.Cells[2].Text.Split(',').First();
            txtNumber2.Text = row.Cells[2].Text.Split(',').Last();
        }
        else
        {
            txtNumber.Text = row.Cells[2].Text;
            txtNumber2.Text = "";
        }
        txtEmail.Text = row.Cells[3].Text;
        txtAddress.Text = row.Cells[4].Text;
        btnSubmit.Text = "Update";
        ViewState["Code"] = Convert.ToString(Nid);
        txtDepartment.Text = ddlDepartment.SelectedItem.Text;
    }
    protected void btnGvDelete_Click(object sender, EventArgs e)
    {
         objEgAddNodalPersonBL = new EgAddNodalPersonBL();
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        objEgAddNodalPersonBL.Nid = Convert.ToInt32(gridNodal.DataKeys[row.RowIndex]["Nodalid"].ToString());//row.Cells[1].Text;
        int result = objEgAddNodalPersonBL.DeleteNodalData();
        if (result == 1)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record deleted successfully.! ')", true);

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Record not deleted.! ')", true);
        }
        BindGrid();
    }
    public void txtDepartment_TextChanged(object obj, EventArgs e)
    {
        if (txtDepartment.Text == "No record Found!")                                  // if no record found and user tries to select the "No Record Found!" 
        {
            txtDepartment.Text = "Please type correct department!";
            ddlDepartment.SelectedValue = "0";
        }
        else
        {
            var getdeptcode = txtDepartment.Text;
            if (getdeptcode.IndexOf('-') > 0)
            {

                int Getcode = Convert.ToInt32(getdeptcode.Split('-')[0]);
                ddlDepartment.SelectedValue = Getcode.ToString();
                BindGridWithNoReset();

            }
            else
            {
                txtDepartment.Text = "Please Type full department name";
            }
        }
        //ddlDepartment.SelectedValue = Getcode.ToString();
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDepartmentList(string prefixText, int count)                //  Method for txtDepartment AjaxAutoCompleteExtender By Rachit on 20 Nov 13
    {


        string Text = prefixText.ToUpper().ToString();


        List<string> LST = new List<string>();


        var query = from t in DtSearch.AsEnumerable()
                    orderby t.Field<string>("DeptNameEnglish"), t.Field<Int32>("DeptCode")// getting all the value from static datatable filled on pageload(DtSearch)
                    where (t.Field<string>("DeptNameEnglish").ToUpper().Contains(Text))
                    select new
                    {
                        DeptNameEnglish = t.Field<string>("DeptNameEnglish"),
                    };
        foreach (var i in query.Take(10))



        //taking top 5 matching records from the LINQ variable
        {
            LST.Add(i.DeptNameEnglish.ToString().Trim());

        };

        if (LST.Count > 0)
            return LST.OrderBy(s => s).ToList();

        else
            LST = new List<string> { "No record Found!" };                      // if list count is 0 than add message to the list of no record found
        return LST;
        //  }
    }
}
