using System;
using System.Data;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.Services;
using System.IO;

public partial class WebPages_EgHome : System.Web.UI.Page
{
    EgHomeBL objEgHomeBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Page"] != null && (Request.QueryString["Page"] == "" || Request.QueryString["Page"] != ""))
        {
            GeneralClass.ShowMessageBox("You are not authorized to access the page");
        }

        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }


        if (!IsPostBack)
        {

            try
            {
                
                objEgHomeBL = new EgHomeBL();
                EgUserProfileBL objEgUserProfileBL = new EgUserProfileBL();
                objEgUserProfileBL.GetServiceDepartmentsList(ddlDeptPopup);
              //  Page.SetFocus(HyperLink1);
                objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objEgHomeBL.GetProfileListWithDepartment(ddlprofile);
                BindInfo();
                //BindRepeater();
                //Cache.Insert("Banks", objEgHomeBL.GetBank(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
                //Cache.Insert("District", objEgHomeBL.FillDistrictList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
                //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:openStyleBox('stylebox');", true);
            }
            catch (Exception ex)
            {
                //Browserinfo objbrowseringo = new Browserinfo();
                //string msg = ex.Message + objbrowseringo.Browserinformaion();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "pageload" + Session["UserId"].ToString());
            }
        }
    }

    public void CreateServiceChallan(int DeptCode, string ServiceId )
    {
       
              objEgHomeBL = new EgHomeBL();
              string[] Service = ServiceId.Split('|');
              objEgHomeBL.ServiceId = Convert.ToInt32(Service[0]);
              objEgHomeBL.DeptCode = DeptCode;
              objEgHomeBL.ProcUserId = Convert.ToInt32(Service[1]);
             if (objEgHomeBL.ServiceId == 0)
             {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert(' Please Select Service ')", true);
                return;
             }

            string msg = objEgHomeBL.RedirectToEChallan();
            if (msg != string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('" + msg + "')", true);
            }
            else
            {
                Response.Redirect(objEgHomeBL.UrlWithData);
            }
       
    }
    /// <summary>
    /// Show  Online or Manual Challan
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnShowRecords_Click(object sender, EventArgs e)
    {
       
            objEgHomeBL = new EgHomeBL();
            stylebox.Visible = true;
            trOptions.Visible = true;
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            // BindInfo();
            BindRepeater();
            btnShowRecords.Visible = false;
       
    }
    public void BindRepeater()
    {
        string[] userpro = ddlprofile.SelectedValue.Split('-');
        objEgHomeBL.UserPro = Convert.ToInt32(userpro[0]);
        objEgHomeBL.RblTransactionSelectedValue = rblTransaction.SelectedValue;
        rpt.DataSource = ddlprofile.SelectedValue == "0" ? objEgHomeBL.BinTransactionPayment() : objEgHomeBL.GetProfileWiseTransactionTable();
        rpt.DataBind();
    }
    /// <summary>
    /// used ot bind the schema according to profile
    /// </summary>
    public void BindGetSchemaName()
    {
     
            objEgHomeBL = new EgHomeBL();
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"]);
            string[] userpro = ddlprofile.SelectedValue.Split('-');
            objEgHomeBL.UserPro = Convert.ToInt32(userpro[0]);
            objEgHomeBL.FillUserSchemaRpt(RptProfile);
            SchemaDiv.Visible = true;
            BindRepeater();
        
    }

    /// <summary>
    /// redirect the page to EChallan
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnRedirect_Click(object sender, EventArgs e)
    {
       
            objEgHomeBL = new EgHomeBL();
            if (rblService.SelectedValue == "P")
            {

                objEgHomeBL.ddlProfileSelectedValue = ddlprofile.SelectedValue;
                string msg = objEgHomeBL.RedirectToEChallan();
                if (msg != string.Empty)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('" + msg + "')", true);
                }
                else
                {
                    Response.Redirect(objEgHomeBL.UrlWithData, true);
                }

            }
            else
            {   // Get ServiceId And USerId in  ddlservice Drop Down Value Fronm Procedure
                CreateServiceChallan(Convert.ToInt32(ddlDeptPopup.SelectedValue), ddlService.SelectedValue);
            }
     
    }

    /// <summary>
    /// used ot bind the logined user information
    /// </summary>
    public void BindInfo()
    {
      
            int result = objEgHomeBL.GetUserDetail();
            if (result == 1)
            {
                lblFirstNameBound.Text = objEgHomeBL.FirstName;
                lblLastsuccess.Text = objEgHomeBL.lastslogin.ToString();
                if (objEgHomeBL.lastflogin.ToString() == "")
                {
                    lbllastfail.Visible = false;
                    lblfail.Visible = false;
                }
                else
                {
                    lbllastfail.Text = objEgHomeBL.lastflogin.ToString();
                }
                if (objEgHomeBL.lastchangepass.ToString() == "")
                {
                    lblpass.Visible = false;
                    lblLastchange.Visible = false;
                }
                else
                {
                    lblLastchange.Text = objEgHomeBL.lastchangepass.ToString();
                }
            }
       
    }

    /// <summary>
    /// used ot bind the schema according to profile
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlprofile_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            BindGetSchemaName();
            if (RptProfile.Items.Count > 0)
            {
                SchemaDiv.Visible = true;
                RptProfile.Visible = true;
                btnRedirect.Visible = true;
                rpt.Visible = true;
                btnShowRecords.Visible = false;
                stylebox.Visible = true;
                trOptions.Visible = true;
            }
            else
            {
                SchemaDiv.Visible = false;
                RptProfile.Visible = false;
                btnRedirect.Visible = false;
            }
       
    }
    protected void ddlDeptPopup_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
            objEgDeptServiceBL.DeptCode = Convert.ToInt32(ddlDeptPopup.SelectedValue);
            objEgDeptServiceBL.GetServiceNameList(ddlService);
       
    }

    ///<summary>
    /// Display Message  Proc Challan ANd No Proc Challan
    ///</summary> 
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] Service = ddlService.SelectedValue.Split('|');
        if (Service[1] == "-1" || Service[1] == "-2")
        {
            string message = Service[1] == "-1" ? "For EProc Tender Value Should Be Greater Than 10 Lac " : "For Non-EProc Tender Value Should Be Less Than 10 Lac";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "myAlert('ध्यान दें:-','" + message + "');", true);
        }
    }
    /// <summary>
    /// redirect page to EChallan and EChallanView Respectivly 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGetSchemaName();
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "openStyleBox('stylebox');", true);
    }
    protected void rblService_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblService.SelectedValue == "P")
        {
            divProfile.Visible = true;
            divService.Visible = false;
        }
        else
        {
            divProfile.Visible = false;
            divService.Visible = true;
        }
    }

    protected void rpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        objEgHomeBL = new EgHomeBL();
       
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                string Ptype = drv.Row["PaymentType"].ToString();
                string Pstatus = drv.Row["Status"].ToString();
                //string ME= drv.Row["DDOCode"].ToString();
                ImageButton Imgpdf = e.Item.FindControl("LinkPDF") as ImageButton;
                LinkButton lnkRepeat = (LinkButton)e.Item.FindControl("lnkrepeat");
                LinkButton lnkverify = (LinkButton)e.Item.FindControl("LinkVerify");
                LinkButton lnkStatus = (LinkButton)e.Item.FindControl("LinkStatus");
                if (Ptype == "Online" && (Pstatus == "Pending" || Pstatus == "Fail"))
                {
                    LinkButton lnk = e.Item.FindControl("LinkStatus") as LinkButton;
                    lnk.Enabled = false;
                    lnk.ToolTip = "You Can't View this Transaction.!";
                    Imgpdf.Visible = false;
                    Imgpdf.ToolTip = "You Can't download this PDF.!";
                    lnkverify.Visible = Pstatus == "Fail" ? false : true;
                }
                //else 
                //if (ME == "102106")
                //{
                //    LinkButton lnkRepeat1 = (LinkButton)e.Item.FindControl("lnkrepeat");
                //    lnkRepeat1.ForeColor = System.Drawing.Color.Red;
                //}
                else if (Ptype == "Manual")
                {
                    if (lnkverify.ToolTip.Substring(7, 1).Trim() == "N")//ADDED BY sandeep for Verify link visible for manual SBI ANYWHERE branch
                        lnkverify.Visible = true;
                    Imgpdf.Visible = false;
                    Imgpdf.ToolTip = "You Can't download this PDF.!";
                }

                else
                {
                    Imgpdf.Visible = true;
                }
                if (lnkverify.ToolTip.Substring(0, 7).Trim() == "0000000") /// added by sandeep to disable all operations for grn whose bank is closed
                {
                    lnkverify.Visible = false;
                    Imgpdf.Visible = false;
                    lnkRepeat.Visible = false;
                    lnkStatus.Visible = false;
                }
            }
        
    }

    /// <summary>
    /// For Repeat and View for Challan
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        objEgHomeBL = new EgHomeBL();
        if (e.CommandName.Equals("Verify"))
        {
            //VerifiedClass objverify = new VerifiedClass();
            //LinkButton Imgverify = e.Item.FindControl("LinkVerify") as LinkButton;
            //objverify.BankCode = Imgverify.ToolTip.Substring(0, 7).Trim();
            //objverify.PaymentType = Imgverify.ToolTip.Substring(8, 1).Trim(); // Add By jp Gupta 27/4/2017 for Check Payment Mode
            //ImageButton Imgpdf = e.Item.FindControl("LinkPDF") as ImageButton;
            //objverify.GRN = Convert.ToInt64(Imgpdf.CommandArgument);
            //Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            //objverify.amt = Convert.ToDouble(lblAmount.Text.Trim());
            //objverify.flag = "R";
            //string msg = objverify.Verifieddetails();
            VerifiedBankClass objverify = new VerifiedBankClass();
            LinkButton Imgverify = e.Item.FindControl("LinkVerify") as LinkButton;
            objverify.BSRCode = Imgverify.ToolTip.Substring(0, 7).Trim();
            objverify.PaymentMode = Imgverify.ToolTip.Substring(8, 1).Trim(); // Add By jp Gupta 27/4/2017 for Check Payment Mode
            ImageButton Imgpdf = e.Item.FindControl("LinkPDF") as ImageButton;
            objverify.GRN = Convert.ToInt64(Imgpdf.CommandArgument);
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            objverify.TotalAmount = Convert.ToDouble(lblAmount.Text.Trim());
            //objverify.flag = "R";
            string msg = objverify.Verify();
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());

            BindRepeater();
            if (msg != null || msg != "")
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + msg + "');", true);
        }
        else
        {
            objEgHomeBL.RptCommandName = e.CommandName;
            objEgHomeBL.RptCommandArgument = e.CommandArgument.ToString();
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"]);
            objEgHomeBL.UserType = Session["UserType"].ToString();
            if (e.CommandName.Equals("Status"))
            {
                LinkButton linkVerify = (LinkButton)e.Item.FindControl("LinkVerify");
                objEgHomeBL.LinkVerifyVisible = linkVerify.Visible;
            }
            string msg = objEgHomeBL.ChallanView();
            Session["GrnNumber"] = objEgHomeBL.Grn;

            if (msg != "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('" + msg + "')", true);
                return;
            }
            else
            {
                Response.Redirect(objEgHomeBL.UrlWithData, false);
            }
        }

    }

    protected void lnkbtnHelp_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/Document/ProcurementTendor.pdf");
        FileInfo file = new FileInfo(filePath);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.Flush();
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["_ViewState"];
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        Session["_ViewState"] = viewState;
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        //Echallan Service
        CreateServiceChallan(104, "11|0");
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        //WaterBill Service
        CreateServiceChallan(7, "6|374");
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ////Electricity Service
        CreateServiceChallan(4, "42|372");
    }
}