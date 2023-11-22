using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using EgBL;
public partial class WebPages_TO_EgAddBankPerson : System.Web.UI.Page
{
    EgAddBankPersonBL objEgAddBankPersonBL;
    static DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {

            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            objEgAddBankPersonBL = new EgAddBankPersonBL();
            objEgAddBankPersonBL.BankListNamewise(ddlBank);
            EgEChallanBL objChallanBL = new EgEChallanBL();
            DtSearch = objChallanBL.GetChallanBank();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            objEgAddBankPersonBL = new EgAddBankPersonBL();
            objEgAddBankPersonBL.BankCode = ddlBank.SelectedValue;
            objEgAddBankPersonBL.BankName = txtBankName.Text.Trim();
            if (txtNumber2.Text != "")
                objEgAddBankPersonBL.Number = txtNumber.Text.Trim() + ',' + txtNumber2.Text.Trim();
            else
                objEgAddBankPersonBL.Number = txtNumber.Text.Trim();
            objEgAddBankPersonBL.Address = txtAddress.Text.Trim();
            objEgAddBankPersonBL.EmailID = txtEmail.Text.Trim();
            if (btnSubmit.Text == "Submit")
            {
                int result = objEgAddBankPersonBL.InsertBankData();
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
                objEgAddBankPersonBL.Bankid = Convert.ToInt32(ViewState["Code"]);
                int result = objEgAddBankPersonBL.UpdateBankData();
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
        //ddlBank.SelectedValue = "0";
        txtBankName.Text = "";
        txtNumber.Text = "";
        txtNumber2.Text = "";
        txtAddress.Text = "";
        txtEmail.Text = "";
        btnSubmit.Text = "Submit";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlBank.SelectedValue = "0";
        ResetControl();
        BindGrid();

    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        objEgAddBankPersonBL = new EgAddBankPersonBL();
        objEgAddBankPersonBL.BankCode = ddlBank.SelectedValue;
        dt = objEgAddBankPersonBL.BankGrid();
        gridNodal.DataSource = dt;
        gridNodal.DataBind();
        dt.Dispose();
        ResetControl();
    }
    public void BindGridWithNoReset()
    {
        DataTable dt = new DataTable();
        objEgAddBankPersonBL = new EgAddBankPersonBL();
        objEgAddBankPersonBL.BankCode =ddlBank.SelectedValue;
        dt = objEgAddBankPersonBL.BankGrid();
        gridNodal.DataSource = dt;
        gridNodal.DataBind();
        dt.Dispose();
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnGvEdit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        int Nid = Convert.ToInt32(gridNodal.DataKeys[row.RowIndex]["Id"].ToString());//row.Cells[1].Text;
        txtBankName.Text = row.Cells[1].Text;
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
    }
    protected void btnGvDelete_Click(object sender, EventArgs e)
    {
        objEgAddBankPersonBL = new EgAddBankPersonBL();
        GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
        objEgAddBankPersonBL.Bankid =Convert.ToInt16(gridNodal.DataKeys[row.RowIndex]["Id"].ToString());//row.Cells[1].Text;
        int result = objEgAddBankPersonBL.DeleteBankData();
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
}