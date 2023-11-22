using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_TO_EgReleaseRefund : System.Web.UI.Page
{
    EgReleaseDefacedEntryBL objEgReleaseDefacedEntryBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        else
        {

            Random randomclass = new Random();
            hdnRnd.Value = randomclass.Next().ToString();
            HttpContext.Current.Session["PWdRND"] = hdnRnd.Value;

            //if (!Page.IsPostBack)
            //{
            //    EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
            //    objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            //    objEgDeptAmountRptBL.PopulateDepartmentList(ddldepartment);
            //    ddldepartment.Enabled = true;
            //    //Bind();
            //}
        }
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    protected void btnSearch_onClick(object sender, EventArgs e)
    {
        EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();

        if (txtgrn.Text != null && txtgrn.Text != "")
        {
            if (objEncryption.GetMD5Hash(objEncryption.GetMD5Hash(txtSecurePass.Text)+ HttpContext.Current.Session["PWdRND"].ToString()) == objEncryption.GetMD5Hash(System.Configuration.ConfigurationManager.AppSettings["SecurePassword"].ToString().Trim() + HttpContext.Current.Session["PWdRND"].ToString()))
            
            { 
                
          
            
                objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
                objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(txtgrn.Text);
                objEgReleaseDefacedEntryBL.ReleaseType = "R";

                Bind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
                return;
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Enter GRN!')", true);
    }

    private void Bind()
    {
        DataTable dt = new DataTable();
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(txtgrn.Text);
        objEgReleaseDefacedEntryBL.type = 1;
        
            dt = objEgReleaseDefacedEntryBL.GetDefacedGRN();
            //}
            //dt = rblReleaseServiceType.SelectedValue == "0" ?  objEgReleaseDefacedEntryBL.GetDefacedGRN() : objEgReleaseDefacedEntryBL.GetReleaseGRNByService();
            if (dt.Rows.Count > 0)
            {
                grdprofile.Visible = true;
                grdprofile.DataSource = dt;
                grdprofile.DataBind();
            }
            else
            {
                grdprofile.DataSource = null;
                grdprofile.DataBind();
            }
        
    }
   
    protected void imgbtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        txtAmount.Text = "";
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        LinkButton lblReference = (LinkButton)gvrow.FindControl("btnReferenceNo");
        objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(grdprofile.DataKeys[gvrow.RowIndex].Value);
        lblGRN_pop.Text = objEgReleaseDefacedEntryBL.Grn.ToString();
        objEgReleaseDefacedEntryBL.GetPartialAmount();
        //hdnRefrenceno.Value = lblReference.Text;//gvrow.Cells[6].Text;
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
        
        lblRemaining.Text = objEgReleaseDefacedEntryBL.GetRefundAvaiableAmount().ToString();
        txtAmount.Enabled = true;
        this.ModalPopupExtender1.Show();
        AjaxControlToolkit.Utility.SetFocusOnLoad(txtAmount);
    }
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
                
                int i = objEgReleaseDefacedEntryBL.InsertRefundReleaseAmount();
                if (i == 1)
                {
                    Bind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Released Successfully.')", true);

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
}