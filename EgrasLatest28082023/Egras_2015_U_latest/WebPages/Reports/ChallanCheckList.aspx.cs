using System;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_ChallanCheckList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EgReports objEgReports = new EgReports();

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }


        if (!IsPostBack)
        {
            EgEchallanHistoryBL objEChallan = new EgEchallanHistoryBL();
            objEChallan.PopulateBankList(ddlbankbranch);
            //objEgReports.Fillddl(ddlbankbranch);
        }

    }

    private void LoadReport()
    {
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[3];
            string[] revdateFrom, revdateTo;
            revdateFrom = txtfromdate.Text.Trim().Split('/');
            param[0] = new ReportParameter("fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txttodate.Text.Trim().Split('/');
            param[1] = new ReportParameter("todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("bankbranchcode", ddlbankbranch.SelectedValue);
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptChallanCheckList, "ChallanCheckList", param);
            rptChallanCheckList.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        LoadReport();
        btnDisable();

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        rptChallanCheckList.Visible = false;
        txtfromdate.Text = "";
        txttodate.Text = "";
        ddlbankbranch.SelectedIndex = 0;
        btnEnable();
    }
    public void btnEnable()
    {
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        ddlbankbranch.Enabled = true;
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        ddlbankbranch.Enabled = false;
    }

}


