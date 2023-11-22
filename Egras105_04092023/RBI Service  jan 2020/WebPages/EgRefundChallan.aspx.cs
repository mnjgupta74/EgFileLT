using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;

public partial class WebPages_EgRefundChallan : System.Web.UI.Page
{

    EgDeptAmountRptBL objEgDeptAmountRptBL;
    EgEncryptDecrypt ObjEncryptDecrypt;

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            SetFocus(txtgrn);

        }
    }

    /// <summary>
    /// Bind all records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Bind();
    }

    /// <summary>
    /// Bind Gridview with respective data
    /// </summary>
    public void Bind()
    {
        EgRefundChallanBL objRefund = new EgRefundChallanBL();

        GenralFunction BLobj = new GenralFunction();
        DataTable dt = new DataTable();
        objRefund.UserId = Convert.ToInt32(Session["UserId"].ToString());

        if (txtgrn.Text != "")
        {
            objRefund.Grn = Convert.ToInt64(txtgrn.Text);

            dt= objRefund.BindGrid();
            gvRefund.DataSource= dt;
            gvRefund.DataBind();


        }
        else if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
            objRefund.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
            objRefund.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
           
             dt=    objRefund.BindGrid();
             gvRefund.DataSource = dt;
             gvRefund.DataBind();

        }
        dt.Dispose();
    }

    protected void imgbtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        EgRefundChallanBL objRefund = new EgRefundChallanBL();
        
        txtAmount.Text = "";
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        objRefund.Grn = Convert.ToInt64(gvRefund.DataKeys[gvrow.RowIndex].Value);
        lblgrn.Text = objRefund.Grn.ToString();
     
        objRefund.GetPartialAmount();
        double amt = objRefund.amount;
       
            lbllastDeface.Text = Convert.ToString(amt);
       
        lblTotal.Text = Convert.ToString(Convert.ToDouble(gvrow.Cells[2].Text));
       
        this.ModalPopupExtender1.Show();
        AjaxControlToolkit.Utility.SetFocusOnLoad(txtAmount);
    }

    /// <summary>
    /// used to refund the amount partially in popup window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
        EgRefundChallanBL objRefund = new EgRefundChallanBL();
       
        if (txtAmount.Text.ToString() != "" && txtBillNo.Text != "" && txtBillDate.Text != "")
        {

          
            if (Math.Round(Convert.ToDouble(txtAmount.Text), 2) > Math.Round(Convert.ToDouble(lbllastDeface.Text), 2))
            {
                //ajaxloader.Style.Add("display", "none");
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Amount can not be greater than Refund Amount.')", true);
                txtAmount.Text = "";
               
            
            }
            else if (Math.Round(Convert.ToDouble(txtAmount.Text), 2) == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Refund Amount can not be zero.')", true);
                txtAmount.Text = "";
                
            }

            else
            {


                objRefund.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objRefund.Grn = Convert.ToInt32(lblgrn.Text);
                objRefund.deface = "R";
                objRefund.amount = Convert.ToDouble(txtAmount.Text);
                objRefund.BillNo = Convert.ToInt32(txtBillNo.Text);
                objRefund.BillDate = Convert.ToDateTime(txtBillDate.Text);
                int J = objRefund.CheckBillNoExistence();
                if (J == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Bill No Already Exist With This Grn .')", true);
                    return;

                }

                int i = objRefund.InsertRefundChallan();
                if (i == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Challan Refund Successfully.')", true);
                    Bind();
                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Challan not refund.')", true);
                    
                }

                txtAmount.Text = "";
                txtBillNo.Text = "";
                txtBillDate.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Plz Fill All Details.)", true);
            
        }

        
    }

    protected void gvRefund_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
          
            string script = "window.open('EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
           
        }
    }

}
