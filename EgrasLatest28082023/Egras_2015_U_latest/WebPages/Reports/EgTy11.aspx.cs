using System;
using Microsoft.Reporting.WebForms;
using EgBL;
using System.Web.UI;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

public partial class WebPages_Reports_EgTy11 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!Page.IsPostBack)
        {
            calendarfromdate.EndDate = DateTime.Now;
            calendartodate.EndDate = DateTime.Now;
        }

    }
    public void loadreport()
    {

        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[4];
            string[] revdateFrom, revdateTo;
            string divisioncode = null;
            divisioncode = (rbtnList.SelectedValue.ToString().Trim() == "EgTy11DivisionWise".ToString().Trim()) ? divcode.SelectedValue.Split('|').GetValue(0).ToString() : null;
            revdateFrom = txtfromdate.Text.Trim().Split('/');
            param[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txttodate.Text.Trim().Split('/');

            if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
            {
                btnEnable();
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
                return;
            }
            param[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("majorHead", (txtMajorHead.Text.Trim() == "" ? null : txtMajorHead.Text.Trim()));
            param[3] = new ReportParameter("divisioncode", (divcode.SelectedValue.Split('|').GetValue(0).ToString()));
            string rptname = rbtnList.SelectedItem.Value.ToString();
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptTy11, rptname, param);
            trrpt.Visible = true;
        }
    }
    protected void ShowPDF()
    {
        if (trrpt.Visible == false)
        {
            loadreport();
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

        returnValue =rptTy11.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=" + rbtnList.SelectedItem.Value.ToString() + ".pdf");
        
        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {


        if (RadioButtonList1.SelectedValue == "1")
        {
            btnDisable();
            loadreport();
            
        }            
        else
            ShowPDF();
    }

    protected void rbtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        trrpt.Visible = false;
        tddivcode.Visible = false;
        if (rbtnList.SelectedItem.Value.ToString().Trim() == "EgTy11DivisionWise".ToString().Trim())
        {
            btnSignPdf.Enabled = false;
            EgEChallanEditDivCodeBL ObjEditDiv = new EgEChallanEditDivCodeBL();
            ObjEditDiv.UserID = Convert.ToInt32(Session["UserId"].ToString());
            ObjEditDiv.FillAllDivisionList(divcode);
            //divcode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All DivisionCode", "0"));
            //divcode.DataBind();
            Session["DivList"] = ObjEditDiv.DivCodeList;
            tddivcode.Visible = true;
        }
        else if(rbtnList.SelectedItem.Value.ToString().Trim() == "EgTy11Summary".ToString().Trim())
            {
               btnSignPdf.Enabled = true;

            }
        else
        {
            btnSignPdf.Enabled = false;
            tddivcode.Visible = false;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtMajorHead.Text = "";
        if(rbtnList.SelectedItem.Value == "EgTy11DivisionWise")
        {
            divcode.SelectedIndex = 0;
        }        
        btnEnable();
        trrpt.Visible = false;

    }
    public void btnEnable()
    {
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        txtMajorHead.Enabled = true;
        divcode.Enabled = true;
        rbtnList.Enabled = true;
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        txtMajorHead.Enabled = false;
        divcode.Enabled = false;
        rbtnList.Enabled = false;
    }
    protected void btnSignPdf_Click(object sender, EventArgs e)
    {
        EgDigitalSignPdf Objdigitalsign = new EgDigitalSignPdf();

        if (trrpt.Visible == false)
        {
            loadreport();
        }
        // create PDF
        // if (Response.IsClientConnected) { Response.Flush(); }
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptTy11.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
        // X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
        X509Certificate2 cert = new X509Certificate2(System.Web.Configuration.WebConfigurationManager.AppSettings["SecureCertificate"] + ConfigurationManager.AppSettings["Certificate"].ToString(), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
        PDFSign objpdfsign = new PDFSign();
        byte[] signedData = objpdfsign.SignDocument(returnValue, cert, Server.MapPath("../../Image/right.jpg"));
        Objdigitalsign.PageName = rbtnList.SelectedItem.Value.ToString();
        Objdigitalsign.SignData = signedData;
        Objdigitalsign.Duration = txtfromdate.Text + '-' + txttodate.Text;
        // Objdigitalsign.InsertSignData();
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename="+ rbtnList.SelectedItem.Value.ToString() + ".pdf");

        Response.BinaryWrite(signedData);
        Response.Flush();
        Response.End();
    }
}
