using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using EgBL;

public partial class WebPages_EgFrmTOVerified : System.Web.UI.Page
{
    EgFrmTOVerified objEgFrmTOVerified;
    Int16 type ;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.GetChallanBanks(ddlbankname);
            
            
        }
        if (RblFilter.SelectedValue == "1")
        {
            type = 2;
            RowSearch.Visible = true;
            RowFromDate.Visible = false;
            RowToDate.Visible = false;
        }
        else if (RblFilter.SelectedValue == "2")
        {
            type = 3;
            RowFromDate.Visible = true;
            RowToDate.Visible = true;
            RowSearch.Visible = false;
        }
        else
        {
            type = 1;
            RowSearch.Visible = false;
            RowFromDate.Visible = false;
            RowToDate.Visible = false;
        }
    }

    public DataTable BindGrid()
    {
        objEgFrmTOVerified = new EgFrmTOVerified();
        objEgFrmTOVerified.BankCode = ddlbankname.SelectedValue;
        objEgFrmTOVerified.paymentType = Online_ManualRadioButton.SelectedValue;
        if (type == 3)
        {
            string[] fromdate = txtFromDate.Text.Split('/');
            objEgFrmTOVerified.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            //objEgFailTransactionsBL.FromDate = Convert.ToDateTime(txtFromDate.Text);
            string[] todate = txtToDate.Text.Split('/');
            objEgFrmTOVerified.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        }
        objEgFrmTOVerified.Type = type;
       

        if (txtSearch.Text.Trim() != "")
        {
            objEgFrmTOVerified.GRN = Convert.ToInt64(txtSearch.Text.Trim());
        }
        Cache.Insert("Details", objEgFrmTOVerified.FillVerifyChallan(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
        return HttpContext.Current.Cache["Details"] as DataTable;
    }


    /// <summary>
    /// Calling bank Service for verifying GRN
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdVerifyChallan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("Verify"))
            {
                int idx = Convert.ToInt32(e.CommandArgument);
                int grn = int.Parse(grdVerifyChallan.Rows[idx - 1].Cells[1].Text);
                double amt = Convert.ToDouble(grdVerifyChallan.Rows[idx - 1].Cells[4].Text);
                string Bank_Code = grdVerifyChallan.Rows[idx - 1].Cells[2].Text;
                VerifiedBankClass objVerifyBank = new VerifiedBankClass();
                objVerifyBank.BSRCode = Bank_Code;
                objVerifyBank.GRN = grn;
                objVerifyBank.TotalAmount = amt;
                objVerifyBank.PaymentMode = Online_ManualRadioButton.SelectedValue;
                string msg = objVerifyBank.Verify();
                //VerifiedClass objverify = new VerifiedClass();
                //objverify.BankCode = Bank_Code;
                //objverify.GRN = grn;
                //objverify.amt = amt;
                //objverify.flag = "A";
                //objverify.PaymentType = Online_ManualRadioButton.SelectedValue;
                //string msg = objverify.Verifieddetails();
                if (msg != null || msg != "")
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            int result = obj.InsertError(ex.Message.ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Due to some error, Unable to process.');", true);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = BindGrid();
        grdVerifyChallan.DataSource = dt;
        grdVerifyChallan.DataBind();
        RowFilter.Visible = true;
        txtSearch.Text = "";
        lblMatch.Visible = false;
    }

    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    if (HttpContext.Current.Cache["Details"] == null)
    //    {
    //        dt = BindGrid();

    //    }
    //    else
    //    {
    //        dt = HttpContext.Current.Cache["Details"] as DataTable;
    //    }
    //    string searchstr = txtSearch.Text.ToUpper().ToString().Trim();
    //    var rows = dt.AsEnumerable().Where(s => s.Field<Int64>("GRN").ToString() == searchstr);
    //    dt = rows.Any() ? rows.CopyToDataTable() : dt.Clone();
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblMatch.Visible = false;
    //        grdVerifyChallan.DataSource = dt;
    //        grdVerifyChallan.DataBind();
    //    }
    //    else
    //    {
    //        dt = BindGrid();
    //        if (dt.Rows.Count > 0)
    //        {
    //            lblMatch.Visible = false;
    //            grdVerifyChallan.DataSource = dt;
    //            grdVerifyChallan.DataBind();
    //        }
    //        else
    //        {
    //            lblMatch.Visible = true;
    //            lblMatch.Text = "No Match Found";
    //            grdVerifyChallan.DataSource = null;
    //            grdVerifyChallan.DataBind();
    //        }
    //    }
    //}

    protected void rdBtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        reset();
    }

    protected void RblFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblFilter.SelectedValue == "1")
        {
            type = 2;
            RowSearch.Visible = true;
            RowFromDate.Visible = false;
            RowToDate.Visible = false;
        }
        else if (RblFilter.SelectedValue == "2")
        {
            type = 3;
            RowFromDate.Visible = true;
            RowToDate.Visible = true;
            RowSearch.Visible = false;
        }
        else
        {
            type = 1;
            RowSearch.Visible = false;
            RowFromDate.Visible = false;
            RowToDate.Visible = false;
        }
    }

    //protected void grdVerifyChallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdVerifyChallan.PageIndex = e.NewPageIndex;
    //    this.BindGrid();
    //}

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }



    private void reset()
    {
        RblFilter.ClearSelection();
        grdVerifyChallan.DataSource = null;
        grdVerifyChallan.DataBind();
        RowFromDate.Visible = false;
        RowToDate.Visible = false;
        RowSearch.Visible = false;
        lblMatch.Visible = false;
    }
}



public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy() { }
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert,
            WebRequest req,
            int problem)
        {
            return true;
        }
    }
