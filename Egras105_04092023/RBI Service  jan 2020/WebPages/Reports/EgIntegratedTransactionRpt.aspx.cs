using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System.Collections;
using System.Web.Services;

public partial class WebPages_Reports_EgIntegratedTransactionRpt : System.Web.UI.Page
{
    int runningTotalSuccess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");

        }

        //if (!Page.IsPostBack)
        //{
        //    Departmentlist();
        //    trMsg.Visible = false;
        //    ddlDepartment.SelectedValue = Convert.ToString(Session["UserType"]) == "5" ? Convert.ToString(Session["UserName"]) : "0";
        //    FillMerchant();
        //}

    }
    [WebMethod]
    public static string GetMerchant(string deptcode)
    {
        EgIntegratedTransactionBL objIntegratedTransactionBl = new EgIntegratedTransactionBL();
        objIntegratedTransactionBl.deptcode = deptcode;
        string JSONGRNString = objIntegratedTransactionBl.FillMerchantName();
        if (JSONGRNString.Length > 0)
        {
            return JSONGRNString;
        }
        else
        {
            return "0";
        }
    }
    [WebMethod]
    public static string GetDepartment(string userid)
    {
        EgIntegratedTransactionBL objEgIntegratedTransactionBL = new EgIntegratedTransactionBL();
        objEgIntegratedTransactionBL.userid = Convert.ToInt32(userid);
        string JSONGRNString = objEgIntegratedTransactionBL.GetDepartment();
        if (JSONGRNString.Length > 0)
        {
            return JSONGRNString;
        }
        else
        {
            return "0";
        }
    }
    [WebMethod]
    public static string GetSummaryReport(string FromDate, string ToDate, string deptcode)
    {
        EgIntegratedTransactionBL objEgIntegratedTransactionBL = new EgIntegratedTransactionBL();
        string[] fromdate = FromDate.Split('/');
        objEgIntegratedTransactionBL.fromdate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
        string[] Todate = ToDate.Split('/');
        objEgIntegratedTransactionBL.todate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
        objEgIntegratedTransactionBL.deptcode = deptcode;
        objEgIntegratedTransactionBL.merchantcode = "0";

        string JSONGRNString = objEgIntegratedTransactionBL.FillIntegratedChallanRpt();
        if (JSONGRNString.Length > 0)
        {
            return JSONGRNString;
        }
        else
        {
            return "0";
        }
    }
    [WebMethod]
    public static string GetDetailReport(string FromDate, string ToDate, string deptcode, string merchantcode, string auin)
    {
        EgIntegratedTransactionBL objEgIntegratedTransactionBL = new EgIntegratedTransactionBL();
        string[] fromdate = FromDate.Split('/');
        objEgIntegratedTransactionBL.fromdate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
        string[] Todate = ToDate.Split('/');
        objEgIntegratedTransactionBL.todate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
        objEgIntegratedTransactionBL.deptcode = deptcode;
        objEgIntegratedTransactionBL.merchantcode = merchantcode;
        objEgIntegratedTransactionBL.auin = (auin == string.Empty || auin == null) ? "0" : auin;

        string JSONGRNString = objEgIntegratedTransactionBL.FillIntegratedChallanDetail();
        if (JSONGRNString.Length > 0)
        {
            return JSONGRNString;
        }
        else
        {
            return "0";
        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    trMsg.Visible = false;
    //    txtfromdate.Enabled = false;
    //    txttodate.Enabled = false;
    //    btnSubmit.Enabled = false;
    //    BindRepeater();
    //}
    //public void BindRepeater()
    //{
    //    EgIntegratedTransactionBL objEgIntegratedTransactionBL = new EgIntegratedTransactionBL();
    //    string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
    //    objEgIntegratedTransactionBL.fromdate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
    //    string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
    //    objEgIntegratedTransactionBL.todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
    //    objEgIntegratedTransactionBL.deptcode = Convert.ToString(Session["UserType"]) == "5" ? Convert.ToString(Session["UserName"]) : (ddlDepartment.SelectedValue);
    //    objEgIntegratedTransactionBL.merchantcode = ddlMerchant.SelectedValue;
    //    if (rbl.SelectedValue == "S")
    //    {
    //        objEgIntegratedTransactionBL.FillIntegratedChallanRpt(rptdeptIntegrated);
    //    }
    //    else
    //    {
    //        //objEgIntegratedTransactionBL.FillIntegratedChallanSummary(rptDetail);
    //        FillIntegratedChallanSummary(objEgIntegratedTransactionBL);
    //    }
    //    if (objEgIntegratedTransactionBL.flag)
    //    {
    //        trMsg.Visible = false;
    //        rptdeptIntegrated.Visible = rbl.SelectedValue == "S" ? true : false;
    //        trDetail.Visible = rbl.SelectedValue == "S" ? false : true;
    //    }
    //    else
    //    {
    //        trMsg.Visible = true;
    //        trDetail.Visible = false;
    //        trrpt.Visible = false;
    //    }

    //}
    //private void FillIntegratedChallanSummary(EgIntegratedTransactionBL objEgIntegratedTransactionBL)
    //{
    //    DataTable dt = new DataTable();
    //    dt = objEgIntegratedTransactionBL.FillIntegratedChallanSummary();
    //    PagedDataSource pageds = new PagedDataSource();
    //    DataView dv = new DataView(dt);
    //    pageds.DataSource = dv;
    //    pageds.AllowPaging = true;
    //    pageds.PageSize = 50;
    //    if (ViewState["PageNumber"] != null)
    //        pageds.CurrentPageIndex = Convert.ToInt32(ViewState["PageNumber"]);
    //    else
    //        pageds.CurrentPageIndex = 0;
    //    if (pageds.PageCount > 1)
    //    {
    //        rptPaging.Visible = true;
    //        ArrayList pages = new ArrayList();
    //        for (int i = 0; i < pageds.PageCount; i++)
    //            pages.Add((i + 1).ToString());
    //        rptPaging.DataSource = pages;
    //        rptPaging.DataBind();
    //    }
    //    else
    //    {
    //        rptPaging.Visible = false;
    //    }
    //    rptDetail.DataSource = pageds;
    //    rptDetail.DataBind();
    //}
    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillMerchant();
    //}
    //protected void ddlMerchant_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnSubmit.Enabled = true;
    //}
    //private void Departmentlist()
    //{
    //    EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
    //    objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session["UserId"]);
    //    objEgDeptAmountRptBl.PopulateDepartmentList(ddlDepartment);
    //}
    //private void FillMerchant()
    //{
    //    EgIntegratedTransactionBL objIntegratedTransactionBl = new EgIntegratedTransactionBL();
    //    objIntegratedTransactionBl.deptcode = Convert.ToString(Session["UserType"]) == "5" ? Convert.ToString(Session["UserName"]) : ddlDepartment.SelectedValue; ;
    //    objIntegratedTransactionBl.FillMerchantName(ddlMerchant);
    //}
    //protected void rptdeptIntegrated_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        runningTotalSuccess += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "TotalOnlineChallan"));
    //    }
    //    else if (e.Item.ItemType == ListItemType.Footer)
    //    {
    //        ((Label)e.Item.FindControl("lblOnlineChallan")).Text = Convert.ToString(runningTotalSuccess);
    //    }
    //}
    //protected void rptDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        runningTotalSuccess += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Amount"));
    //    }
    //    else if (e.Item.ItemType == ListItemType.Footer)
    //    {
    //        ((Label)e.Item.FindControl("lblTotalAmount")).Text = Convert.ToString(runningTotalSuccess);
    //    }
    //}
    //protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument) - 1;
    //    BindRepeater();
    //}
    //protected void rbl_Changed(object sender, EventArgs e)
    //{
    //    btnSubmit.Enabled = true;
    //    rptdeptIntegrated.Visible = false;
    //    trMsg.Visible = false;

    //    ddlMerchant.Items.Clear();
    //    ddlDepartment.Items.Clear();

    //    if (rbl.SelectedValue == "D")
    //    {
    //        tdDept.Visible = true;
    //        tdMerchant.Visible = true;
    //        rfcfromdate.ValidationGroup = "b";
    //        rfctodate.ValidationGroup = "b";
    //        btnSubmit.ValidationGroup = "b";
    //        Departmentlist();

    //    }
    //    else
    //    {
    //        tdDept.Visible = false;
    //        tdMerchant.Visible = false;
    //        rfcfromdate.ValidationGroup = "a";
    //        rfctodate.ValidationGroup = "a";
    //        btnSubmit.ValidationGroup = "a";
    //    }
    //}
    //protected void btnReset_Click(object sender, EventArgs e)
    //{
    //    txtfromdate.Enabled = true;
    //    txttodate.Enabled = true;
    //    txtfromdate.Text = "";
    //    txttodate.Text = "";
    //    btnSubmit.Enabled = true;
    //    rptdeptIntegrated.Visible = false;
    //    trMsg.Visible = false;
    //    if (Convert.ToString(Session["UserType"]) == "5")
    //    {
    //        ddlDepartment.SelectedValue = Convert.ToString(Session["UserName"]);
    //        FillMerchant();
    //    }
    //    else
    //    {
    //        ddlMerchant.Items.Clear();
    //        ddlDepartment.Items.Clear();
    //        if (rbl.SelectedValue == "D")
    //            Departmentlist();
    //    }

    //    if (rbl.SelectedValue == "D")
    //    {
    //        tdDept.Visible = true;
    //        tdMerchant.Visible = true;
    //    }
    //    else
    //    {
    //        tdDept.Visible = false;
    //        tdMerchant.Visible = false;
    //    }
    //}
}
