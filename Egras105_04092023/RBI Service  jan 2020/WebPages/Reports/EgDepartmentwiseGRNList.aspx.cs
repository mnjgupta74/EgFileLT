using System;
using EgBL;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

public partial class WebPages_Reports_EgDepartmentwiseGRNList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if(!Page.IsPostBack)
        {            
            FillDepartment();
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
            LoadReport();
        else
            ShowPDF();
    }
    public void FillDepartment()
    {
        EgDeptAmountRptBL objdept = new EgDeptAmountRptBL();
       
        objdept.UserId = (Convert.ToInt32(Session["UserID"].ToString()));
        objdept.PopulateDepartmentList(ddldepartment);
        if (Session["UserType"].ToString().Trim() == "5")
        {
            ddldepartment.SelectedIndex = 1;
            ddldepartment.Enabled = false;
        }
        //ddldepartment.DataSource = dt;
        //ddldepartment.DataTextField = "deptnameEnglish";
        //ddldepartment.DataValueField = "DeptCode";
        //ddldepartment.DataBind();
    }

    private void LoadReport()
    {
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[3];
            string[] revdateFrom, revdateTo;
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            DateTime fromDate = Convert.ToDateTime(revdateFrom[1].ToString() + '/' + revdateFrom[0].ToString() + '/' + revdateFrom[2].ToString());
            param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txtToDate.Text.Trim().Split('/');
            DateTime toDate = Convert.ToDateTime(revdateTo[1].ToString() + '/' + revdateTo[0].ToString() + '/' + revdateTo[2].ToString());
            if ((toDate - fromDate).TotalDays > 30)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 30 days');", true);
                return;
            }
            param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("DeptCode", ddldepartment.SelectedValue.ToString().Trim());
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptDepartmentWiseGRNList, "DepartmentWiseGRNList", param);
            trrpt.Visible = true;
        }
    }
    protected void ShowPDF()
    {
        if (trrpt.Visible == false)
        {
            LoadReport();
        }
        //create PDF
        //if (Response.IsClientConnected) { Response.Flush(); }
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptDepartmentWiseGRNList.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=DepartmentWiseGRNList.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
}
