using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_TO_EgReleaseDefacedEntry : System.Web.UI.Page
{
    EgReleaseDefacedEntryBL objEgReleaseDefacedEntryBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    protected void btnSearch_onClick(object sender, EventArgs e)
    {
        if (txtgrn.Text != null && txtgrn.Text != "")
        {
            if (true)
            {
                objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
                objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(txtgrn.Text);
                objEgReleaseDefacedEntryBL.ReleaseType = rblReleaseServiceType.SelectedValue == "0" ? "D" : "R";
                if (objEgReleaseDefacedEntryBL.CheckGrnStatus() == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('This Grn Request Accepted  by Release Amount Service !')", true);
                    txtgrn.Text = string.Empty;
                    return;
                }
                Bind();
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Enter GRN!')", true);
    }

    /// <summary>
    /// used to bind Gridview with respect to GRN value in Text box
    /// </summary>
    private void Bind()
    {
        DataTable dt = new DataTable();
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        objEgReleaseDefacedEntryBL.Grn = rblReleaseServiceType.SelectedValue == "0" ? Convert.ToInt64(txtgrn.Text) : 0;
        objEgReleaseDefacedEntryBL.type = Convert.ToInt32(rblReleaseType.SelectedValue);
        dt = rblReleaseServiceType.SelectedValue == "0" ? objEgReleaseDefacedEntryBL.GetDefacedGRN() : objEgReleaseDefacedEntryBL.GetReleaseGRNByService();
        if (dt.Rows.Count > 0)
        {
            grdprofile.DataSource = null;
            grdOnline.DataSource = null;

            if (rblReleaseServiceType.SelectedValue == "0")
            {
                grdOnline.Visible = false;
                grdprofile.Visible = true;
                grdprofile.DataSource = dt;
                grdprofile.DataBind();
            }
            else
            {
                grdOnline.Visible = true;
                grdprofile.Visible = false;
                grdOnline.DataSource = dt;
                grdOnline.DataBind();
            }
            rblReleaseType.Enabled = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('GRN not found!')", true);
        }
    }

    ///// <summary>
    ///// used to Release the partial amount
    ///// </summary>
    protected void imgbtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        txtAmount.Text = "";
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(rblReleaseServiceType.SelectedValue == "0" ? grdprofile.DataKeys[gvrow.RowIndex].Value : grdOnline.DataKeys[gvrow.RowIndex].Value);
        lblGRN_pop.Text = objEgReleaseDefacedEntryBL.Grn.ToString();
        objEgReleaseDefacedEntryBL.GetPartialAmount();
        double amt = objEgReleaseDefacedEntryBL.DefacedAmount;
        if (amt == 0.0)
        {
            lbllastDeface.Text = "No Amount Defaced";
        }
        else
        {
            lbllastDeface.Text = Convert.ToString(amt);//500
        }
        objEgReleaseDefacedEntryBL.GetRefundAmount();
        double refundAmt = objEgReleaseDefacedEntryBL.RefundAmount;
        if (refundAmt == 0.0)
        {
            lblRefundedAmt.Text = "No Amount Refunded";
        }
        else
        {
            lblRefundedAmt.Text = Convert.ToString(refundAmt);//100
        }
        //var amount = rblReleaseServiceType.SelectedValue == "0" ? gvrow.Cells[3].Text : gvrow.Cells[4].Text;
        var amount = gvrow.Cells[3].Text;
        lblTotal.Text = Convert.ToString(Convert.ToDouble(amount));

        if (rblReleaseType.SelectedValue == "0")//deface
        {
            double releasedAmount = objEgReleaseDefacedEntryBL.GetReleaseAmount(); //100
            lblRemaining.Text = (Convert.ToDouble(amt.ToString()) - releasedAmount).ToString();//500-100=400 Amuont can be refunded
        }
        else
            lblRemaining.Text = lblRefundedAmt.Text;//refund

        if (rblReleaseServiceType.SelectedValue == "1")
        {
            txtAmount.Enabled = false;
            objEgReleaseDefacedEntryBL.ReleaseType = rblReleaseType.SelectedValue == "0" ? "D" : "R";
            hdnRefrenceno.Value = gvrow.Cells[6].Text;
            objEgReleaseDefacedEntryBL.RefrenceNo = Convert.ToInt64(gvrow.Cells[6].Text);
            txtAmount.Text = objEgReleaseDefacedEntryBL.GetReleasableServiceAmount().ToString(); ;
        }
        else
        {
            txtAmount.Enabled = true;
        }
        this.ModalPopupExtender1.Show();
        AjaxControlToolkit.Utility.SetFocusOnLoad(txtAmount);
    }

    ///// <summary>
    ///// Used to update the partial released amount in database, this button is in panel .
    ///// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        if (txtAmount.Text.ToString() != "")
        {
            objEgReleaseDefacedEntryBL.releaseAmt = Convert.ToDouble(txtAmount.Text);
            objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(lblGRN_pop.Text);
            if (Convert.ToDouble(txtAmount.Text) <= Convert.ToDouble(lblRemaining.Text))
            {
                objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(lblGRN_pop.Text);
                objEgReleaseDefacedEntryBL.UserId = Convert.ToInt32(Session["UserId"]);
                objEgReleaseDefacedEntryBL.releaseAmt = Convert.ToDouble(txtAmount.Text);
                objEgReleaseDefacedEntryBL.type = Convert.ToInt32(rblReleaseType.SelectedValue);
                objEgReleaseDefacedEntryBL.RefrenceNo = Convert.ToInt64(hdnRefrenceno.Value);
                int i = objEgReleaseDefacedEntryBL.InsertReleaseAmount();
                if (i == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Released Successfully.')", true);
                    Bind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Not Released.')", true);
                }
                txtAmount.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Invalid Release Amount.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Plz Fill Amount)", true);
        }
    }
    protected void grdprofile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            string script = "window.open('EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }
    }

    protected void grdprofile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (rblReleaseType.SelectedValue == "1")
                e.Row.Cells[2].Text = "Refunded Amount";
            else
                e.Row.Cells[2].Text = "Defaced Amount";
        }
    }
    protected void rblReleaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rblReleaseServiceType.SelectedValue = "0";
        txtgrn.Visible = true;
        btnSearch.Visible = true;
        tdManual.Visible = rblReleaseServiceType.SelectedValue == "0" ? true : false;
        rblReleaseServiceType.Visible = rblReleaseType.SelectedValue == "0" ? true : false;
    }
    protected void rblReleaseServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdprofile.Visible = false;
        grdOnline.Visible = false;

        if (rblReleaseServiceType.SelectedValue == "1")
        {
            lblGrn.Visible = false;
            txtgrn.Visible = false;
            btnSearch.Visible = false;
            Bind();
        }
        else
        {
            lblGrn.Visible = false;
            txtgrn.Visible = true;
            btnSearch.Visible = true;
        }
    }
}
