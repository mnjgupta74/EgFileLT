using System;
using System.Data;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EgHomeForOffice : System.Web.UI.Page
{
    EgHomeBL objEgHomeBL = new EgHomeBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Page"] != null && (Request.QueryString["Page"] == "" || Request.QueryString["Page"] != ""))
        {
            GeneralClass.ShowMessageBox("You are not authorized to access the page");
        }
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.rbltype);

        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            Page.SetFocus(HyperLink1);
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            if (rbltype.SelectedValue == "1")
            {
                EgDeptAmountRptBL objDeptAmount = new EgDeptAmountRptBL();
                objDeptAmount.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objDeptAmount.PopulateDepartmentList(ddlDeptPopup);
                objEgHomeBL.GetProfileListWithDepartment(ddlprofile);
                    BindRepeater();
                
            }
            else
            {
                EgUserProfileBL_ME objme = new EgUserProfileBL_ME();
                objme.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objme.GetProfileListME(ddlprofile);
                BindRepeaterME();
            }
            //BindInfo();

            Cache.Insert("Banks", objEgHomeBL.GetBank(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
            Cache.Insert("District", objEgHomeBL.FillDistrictList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
            //ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:openStyleBox('stylebox');", true);
        }
    }
    public void BindRepeaterME()
    {
        EgUserProfileBL_ME objme = new EgUserProfileBL_ME();
        objme.UserId = Convert.ToInt32(Session["UserId"]);
        objme.RblTransactionSelectedValue = rblTransaction.SelectedValue;
        string[] userpro = ddlprofile.SelectedValue.Split('-');
        objme.UserPro = Convert.ToInt32(userpro[0]);
        rpt.DataSource = ddlprofile.SelectedValue == "0" ? objme.BinTransactionPaymentME() : objme.GetProfileWiseTransactionME();
        rpt.DataBind();
    }
    public void BindRepeater()
    {
        objEgHomeBL.RblTransactionSelectedValue = rblTransaction.SelectedValue;
        rpt.DataSource = ddlprofile.SelectedValue == "0" ? objEgHomeBL.BinTransactionPayment() : objEgHomeBL.GetProfileWiseTransactionTable();
        rpt.DataBind();
    }
    /// <summary>
    /// used ot bind the schema according to profile
    /// </summary>

    /// <summary>
    /// redirect the page to EChallan
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnRedirect_Click(object sender, EventArgs e)
    {
        if (rblService.SelectedValue == "S")
         {
            CreateServiceChallan(Convert.ToInt32(ddlDeptPopup.SelectedValue), ddlService.SelectedValue);
         }
        else
        {
            objEgHomeBL.ddlProfileSelectedValue = ddlprofile.SelectedValue;
            objEgHomeBL.rbltype = rbltype.SelectedValue;
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
    }
    /// <summary>
    /// used ot bind the logined user information
    /// </summary>

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
          //  btnShowRecords.Visible = false;
            //stylebox.Visible = true;
          //  trOptions.Visible = true;
        }
        else
        {
            SchemaDiv.Visible = false;
            RptProfile.Visible = false;
            btnRedirect.Visible = false;
        }

    }

    public void BindGetSchemaName()
    {
        objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"]);
        string[] userpro = ddlprofile.SelectedValue.Split('-');
        objEgHomeBL.UserPro = Convert.ToInt32(userpro[0]);
        if (rbltype.SelectedValue == "1")
        {
            objEgHomeBL.FillUserSchemaRpt(RptProfile);
        }
        else
        {
            objEgHomeBL.FillUserSchemaRptME(RptProfile);
        }
        SchemaDiv.Visible = true;
        BindRepeater();
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
    protected void rpt_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            string Ptype = drv.Row["PaymentType"].ToString();
            string Pstatus = drv.Row["Status"].ToString();
            //string ME = drv.Row["DDOCode"].ToString();
            ImageButton Imgpdf = e.Item.FindControl("LinkPDF") as ImageButton;
            LinkButton lnkRepeat = (LinkButton)e.Item.FindControl("lnkrepeat");
            LinkButton lnkverify = (LinkButton)e.Item.FindControl("LinkVerify");
            if (Ptype == "Online" && (Pstatus == "Pending" || Pstatus == "Fail"))
            {
                LinkButton lnk = e.Item.FindControl("LinkStatus") as LinkButton;
                lnk.Enabled = false;
                lnk.ToolTip = "You Can't View this Transaction.!";
                Imgpdf.Visible = false;
                Imgpdf.ToolTip = "You Can't download this PDF.!";
                lnkverify.Visible = Pstatus == "Fail" ? false : true;
            }
            //else if (ME == "102106")
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
        }
    }
    /// <summary>
    /// For Repeat and View for Challan
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName.Equals("Verify"))
        {
            VerifiedClass objverify = new VerifiedClass();
            LinkButton Imgverify = e.Item.FindControl("LinkVerify") as LinkButton;
            objverify.BankCode = Imgverify.ToolTip.Substring(0, 7).Trim();
            objverify.PaymentType = Imgverify.ToolTip.Substring(8, 1).Trim(); // Add By jp Gupta 27/4/2017 for Check Payment Mode
            ImageButton Imgpdf = e.Item.FindControl("LinkPDF") as ImageButton;
            objverify.GRN = Convert.ToInt64(Imgpdf.CommandArgument);
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            objverify.amt = Convert.ToDouble(lblAmount.Text.Trim());
            objverify.flag = "R";
            string msg = objverify.Verifieddetails();
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            BindRepeater();
            if (msg != null || msg != "")
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + msg + "');", true);
        }
        else
        {
            if (rbltype.SelectedValue == "2")
            {
                objEgHomeBL.RblTransactionSelectedValue = "E";
            }
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
                Response.Redirect(objEgHomeBL.UrlWithData);
            }
        }
    }
    protected void ddlDeptPopup_SelectedIndexChanged(object sender, EventArgs e)
    {

        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.DeptCode = Convert.ToInt32(ddlDeptPopup.SelectedValue);
        objEgDeptServiceBL.GetServiceNameList(ddlService);

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
    // Add Service
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] Service = ddlService.SelectedValue.Split('|');
        if (Service[1] == "-1" || Service[1] == "-2")
        {
            string message = Service[1] == "-1" ? "For EProc Tender Value Should Be Greater Than 10 Lac " : "For Non-EProc Tender Value Should Be Less Than 10 Lac";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "myAlert('ध्यान दें:-','" + message + "');", true);
        }
    }
    protected void rbltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlprofile.Items.Clear();
        SchemaDiv.Visible = false;
        if (rbltype.SelectedValue == "2")
        {
            HyperLink1.NavigateUrl = "~/WebPages/NewEgUserProfileME.aspx";
            rblTransaction.SelectedValue = "M";
            rblTransaction.Enabled = false;
            EgUserProfileBL_ME objme = new EgUserProfileBL_ME();
            objme.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objme.GetProfileListME(ddlprofile);
            BindRepeaterME();

        }
        else
        {
            HyperLink1.NavigateUrl = "~/WebPages/NewEgUserProfile.aspx";
            rblTransaction.Enabled = true;
            objEgHomeBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEgHomeBL.GetProfileListWithDepartment(ddlprofile);
            BindRepeater();
        }
    }

    //  Add service From Ddo Login  12 May 2020  Proc  And EProc Service
    public void CreateServiceChallan(int DeptCode, string ServiceId)
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
}