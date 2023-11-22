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
        else
        {
            if (!Page.IsPostBack)
            {
                rblReleaseType.Visible = false;
                //rblReleaseServiceType.SelectedValueK
                EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
                objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objEgDeptAmountRptBL.PopulateDepartmentList(ddldepartment);
                string[] DeptCode = Session["UserName"].ToString().Split('.');
                if (Session["UserType"].ToString() == "11")
                {
                    ddldepartment.Items.FindByValue(DeptCode[1].ToString()).Selected = true;
                    ddldepartment.Enabled = false;
                }
                if (Session["UserType"].ToString() == "5")
                {
                    ddldepartment.Items.FindByValue(DeptCode[0].ToString()).Selected = true;
                    ddldepartment.Enabled = false;
                }
                else
                {

                    ddldepartment.Enabled = true;

                }

                Bind();
            }
        }
    }

    /// <summary>
    /// Search Button Click Event
    /// </summary>
    //protected void btnSearch_onClick(object sender, EventArgs e)
    //{
    //    if (txtgrn.Text != null && txtgrn.Text != "")
    //    {
    //        if (true)
    //        {
    //            objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
    //            objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(txtgrn.Text);
    //            objEgReleaseDefacedEntryBL.ReleaseType = rblReleaseServiceType.SelectedValue == "0" ? "D" : "R";
    //            //if (objEgReleaseDefacedEntryBL.CheckGrnStatus() == 1)
    //            //{
    //            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('This Grn Request Accepted  by Release Amount Service !')", true);
    //            //    txtgrn.Text = string.Empty;
    //            //    return;
    //            //}
    //            Bind();
    //        }
    //        else
    //            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
    //    }
    //    else
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Enter GRN!')", true);
    //}

    /// <summary>
    /// used to bind Gridview with respect to GRN value in Text box
    /// </summary>
    private void Bind()
    {
        trgrdOnline.Visible = true;
        grdOnline.Visible = true;
        grddefacerelease.Visible = false;
        trgrddefacerelease.Visible = false;
        DataTable dt = new DataTable();
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        objEgReleaseDefacedEntryBL.Grn = rblReleaseServiceType.SelectedValue == "0" ? Convert.ToInt64(txtgrn.Text) : 0;
        objEgReleaseDefacedEntryBL.type = Convert.ToInt32(rblReleaseType.SelectedValue);
        //string[] DeptCode = Session["UserName"].ToString().Split('.');
        //objEgReleaseDefacedEntryBL.DeptCode = Convert.ToInt32(DeptCode[1]);
        if (ddldepartment.SelectedValue != "0")
        {
            objEgReleaseDefacedEntryBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue.ToString());
            objEgReleaseDefacedEntryBL.Valuetype = Convert.ToInt32(rblReleaseServiceType.SelectedValue);
            dt = objEgReleaseDefacedEntryBL.GetReleaseGRNByServiceForOffice();
            //}
            //dt = rblReleaseServiceType.SelectedValue == "0" ?  objEgReleaseDefacedEntryBL.GetDefacedGRN() : objEgReleaseDefacedEntryBL.GetReleaseGRNByService();
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
                grdprofile.DataSource = null;
                grdOnline.DataSource = null;
                grdprofile.DataBind();
                grdOnline.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('GRN not found!')", true);
                //rblReleaseServiceType.Items.FindByValue("1").Selected = true;
                //rblReleaseServiceType.Items.FindByValue("2").Selected = false;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Department')", true);
            rblReleaseServiceType.Items.FindByValue("0").Selected = true;
            rblReleaseServiceType.Items.FindByValue("1").Selected = false;
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

        LinkButton lblReference = (LinkButton)gvrow.FindControl("btnReferenceNo");


        objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(rblReleaseServiceType.SelectedValue == "0" ? grdprofile.DataKeys[gvrow.RowIndex].Value : grdOnline.DataKeys[gvrow.RowIndex].Value);
        lblGRN_pop.Text = objEgReleaseDefacedEntryBL.Grn.ToString();
        objEgReleaseDefacedEntryBL.GetPartialAmount();
        hdnRefrenceno.Value = lblReference.Text;//gvrow.Cells[6].Text;
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

        if (rblReleaseServiceType.SelectedValue == "1" || rblReleaseServiceType.SelectedValue == "2")
        {
            txtAmount.Enabled = false;
            objEgReleaseDefacedEntryBL.ReleaseType = rblReleaseType.SelectedValue == "0" ? "D" : "R";
            objEgReleaseDefacedEntryBL.Valuetype = Convert.ToInt32(rblReleaseServiceType.SelectedValue);
            objEgReleaseDefacedEntryBL.RefrenceNo = Convert.ToInt64(lblReference.Text);
            txtAmount.Text = objEgReleaseDefacedEntryBL.GetReleasableServiceAmount().ToString();
        }
        else
        {
            txtAmount.Enabled = true;
        }
        this.ModalPopupExtender1.Show();
        AjaxControlToolkit.Utility.SetFocusOnLoad(txtAmount);
    }

    /// <summary>
    /// /Objection window 5 April 2022
    /// put Objection By e -to AT rELEASE aMOUNT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void imgobjection_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        btnObjSubmit.Enabled = true;
        btnReset.Visible = false;
        txtobjection.Text = "";

        DataTable objectionDt = new DataTable();
        DataTable dt = new DataTable();
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        LinkButton lblReference = (LinkButton)gvrow.FindControl("btnReferenceNo");
        objEgReleaseDefacedEntryBL.RefrenceNo = Convert.ToInt64(lblReference.Text);
        lblObjectionGRN.Text = rblReleaseServiceType.SelectedValue == "0" ? grdprofile.DataKeys[gvrow.RowIndex].Value.ToString() : grdOnline.DataKeys[gvrow.RowIndex].Value.ToString();
        objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(lblObjectionGRN.Text);
        hdnRefrenceno.Value = lblReference.Text;
        objectionDt = objEgReleaseDefacedEntryBL.GetObjectionList();
        ObjectionList.DataTextField = "ObjectionName";
        ObjectionList.DataValueField = "ObjectionName";
        ObjectionList.DataSource = objectionDt;//Set Datasource to CheckBox List  
        ObjectionList.DataBind(); // Bind the checkboxList with String List.
       
            dt = objEgReleaseDefacedEntryBL.GetObjectionListForOffice();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                string CustomList = "";
                string[] lstobj = dt.Rows[0]["Objection"].ToString().Split(',');
                txtobjection.Text = dt.Rows[0]["Comment"].ToString();
                foreach (string list in lstobj)
                {
                    for (i = 0; i < ObjectionList.Items.Count; i++)
                    {
                        string item = ObjectionList.Items[i].ToString();
                        if (item == list)
                        {
                            ObjectionList.Items.FindByText(item).Selected = true;
                        }                 
                    }


                }
            }
        
        


        this.ModalPopupExtender3.Show();

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
                objEgReleaseDefacedEntryBL.Valuetype = Convert.ToInt32(rblReleaseServiceType.SelectedValue);
                int i = objEgReleaseDefacedEntryBL.InsertReleaseAmount();
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
    protected void btnResetObj_Click(object sender, EventArgs e)
    {
        txtobjection.Text = "";
        btnObjSubmit.Enabled = true;
        btnReset.Visible = false;
    }
    protected void btnObjSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string objectionList = "";
            objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
            if (txtobjection.Text.ToString() != "")
            {
                objectionList = txtobjection.Text.ToString() + ",";

            }
            for (int i = 0; i < ObjectionList.Items.Count; i++)
            {
                if (ObjectionList.Items[i].Selected)
                {

                    objectionList = objectionList + ObjectionList.Items[i].Text + ",";
                }

            }
            objEgReleaseDefacedEntryBL.Comment = txtobjection.Text;
            objEgReleaseDefacedEntryBL.Grn = Convert.ToInt64(lblObjectionGRN.Text);
            objEgReleaseDefacedEntryBL.RefrenceNo = Convert.ToInt64(hdnRefrenceno.Value);
            objEgReleaseDefacedEntryBL.ObjectionList = objectionList;
            if (objectionList == "" || objectionList == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Select At least One Objection')", true);
                return;
            }
            int rtn;
            if (rblReleaseServiceType.SelectedValue == "2")
            {

                rtn = objEgReleaseDefacedEntryBL.InsertObjectionForOffice();
            }
            else
            {

                rtn = objEgReleaseDefacedEntryBL.InsertObjectionForOffice();
            }


            if (rtn == 1)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Insert with Objection.')", true);

            }
            Bind();

        }
        else
        {
            btnObjSubmit.Enabled = false;
            btnReset.Visible = true;
            txtobjection.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Special charectors not allowed !')", true);
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
            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }

        if (e.CommandName == "ReferenceNo")  // Add SectionLetter Url fro IGRS 28 April 2022
        {
            int GRN = Convert.ToInt32(e.CommandArgument);
            LinkButton lb = (LinkButton)e.CommandSource;
            int RefNo = Convert.ToInt32(lb.Text);

            int OnlineOrManualStatus = 0;
            //0 for Manula and 1 for online  reference
            //LinkButton lb = (LinkButton)e.CommandSource;
            //Int64 reference = 0;
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();

            if (rblReleaseServiceType.SelectedValue == "1")
            {
                OnlineOrManualStatus = 0;
            }
            if (rblReleaseServiceType.SelectedValue == "2")
            {
                OnlineOrManualStatus = 1;
            }


            //Fetch value of Name.
            /// string name = (row.FindControl("txtName") as TextBox).Text;

            if (OnlineOrManualStatus == 0)
            {


                string strURLWithData = objEncrypt.Encrypt(string.Format("{0}", "eGrasSanctionLetter|eGras@2022|" + RefNo), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "86.key");

                string script = "window.open('" + System.Web.Configuration.WebConfigurationManager.AppSettings["IGRSUrl"] + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
            }

            else
            {
                //LinkButton lb1 = (LinkButton)e.CommandSource;
                //int grn = Convert.ToInt32(lb1.Text);

                //Create & Download PDF File

                byte[] returnValue = getPDfBytes(RefNo, GRN);



                Response.Buffer = true;
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + RefNo + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(returnValue);
                Response.Flush();
                Response.End();

            }


        }
    }

    public byte[] getPDfBytes(Int64 refNo, Int64 Grn)
    {
        byte[] bytes;
        objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        objEgReleaseDefacedEntryBL.RefrenceNo = refNo;
        objEgReleaseDefacedEntryBL.Grn = Grn;
        DataTable dt = objEgReleaseDefacedEntryBL.GetDefaceReleasePDfBytes();
        bytes = (byte[])dt.Rows[0]["PDFFile"];
        return bytes;
        //GetDefaceReleasePDfBytes
    }

    protected void grdprofile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (rblReleaseType.SelectedValue == "1")
                e.Row.Cells[2].Text = "Refunded Amount";
            if (rblReleaseServiceType.SelectedValue == "2")
                e.Row.Cells[6].Text = "Sanction Download";
            //else
            //    e.Row.Cells[2].Text = "Request Date";
        }

    }
    //protected void rblReleaseType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //rblReleaseServiceType.SelectedValue = "1";
    //    //txtgrn.Visible = true;
    //    //btnSearch.Visible = true;
    //    //tdManual.Visible = rblReleaseServiceType.SelectedValue == "0" ? true : false;
    //    //rblReleaseServiceType.Visible = rblReleaseType.SelectedValue == "0" ? true : false;
    //}
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
        else if (rblReleaseServiceType.SelectedValue == "2")
        {
            lblGrn.Visible = false;
            txtgrn.Visible = false;
            btnSearch.Visible = false;
            Bind();
        }
        else
        {
            lblGrn.Visible = false;
            //txtgrn.Visible = true;
            //btnSearch.Visible = true;
        }
    }
    protected void btnReset_onClick(object sender, EventArgs e)
    {
        grdprofile.Visible = false;
        grdOnline.Visible = false;
        rblReleaseServiceType.SelectedValue = "0";
        rblReleaseServiceType.Enabled = true;
        rblReleaseType.SelectedValue = "0";
        rblReleaseType.Enabled = true;

        lblGrn.Visible = false;
        //txtgrn.Visible = true;
        //btnSearch.Visible = true;
        //txtgrn.Text = "";
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        trgrdOnline.Visible = false;
        grdOnline.Visible = false;
        grddefacerelease.Visible = true;
        EgReleaseAmountBL egReleaseBL = new EgReleaseAmountBL();
        DataTable dt = new DataTable();
        if (txtDefaceGrn.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Enter GRN.')", true);
            return;
        }

        egReleaseBL.Grn = Convert.ToInt64(txtDefaceGrn.Text.ToString());
        dt = egReleaseBL.EgGetDefacerequestStatus();
        //if(dt.Rows.Count > 0)
        //{
        trgrddefacerelease.Visible = true;
        //}
        grddefacerelease.DataSource = dt;
        grddefacerelease.DataBind();
        //rblReleaseServiceType.Items.FindByValue("1").Selected = false;
        //rblReleaseServiceType.Items.FindByValue("2").Selected = false;
    }
    protected void grddefacerelease_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }

        if (e.CommandName == "ReferenceNo")  // Add SectionLetter Url fro IGRS 28 April 2022
        {
            int GRN = Convert.ToInt32(e.CommandArgument);
            LinkButton lb = (LinkButton)e.CommandSource;
            int RefNo = Convert.ToInt32(lb.Text);
            SbiEncryptionDecryption objEncrypt = new SbiEncryptionDecryption();
            int OnlineOrManualStatus = 0;

            if (rblReleaseServiceType.SelectedValue == "1")
            {
                OnlineOrManualStatus = 0;
            }
            if (rblReleaseServiceType.SelectedValue == "2")
            {
                OnlineOrManualStatus = 1;
            }
            if (OnlineOrManualStatus == 0)
            {


                string strURLWithData = objEncrypt.Encrypt(string.Format("{0}", "eGrasSanctionLetter|eGras@2022|" + RefNo), System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "86.key");

                string script = "window.open('" + System.Web.Configuration.WebConfigurationManager.AppSettings["IGRSUrl"] + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
            }

            else
            {
                byte[] returnValue = getPDfBytes(RefNo, GRN);
                Response.Buffer = true;
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + RefNo + ".pdf");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(returnValue);
                Response.Flush();
                Response.End();
            }
        }
    }

}
