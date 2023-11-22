using System;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

namespace WebPages.Reports
{
    public partial class WebPages_EgGetSNABankDetailData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
            {
                Response.Redirect("~\\LoginAgain.aspx");
            }
            if (!Page.IsPostBack)
            {
                calendarfromdate.EndDate = DateTime.Now;
            }
        }
        protected void LoadReport()
        {
            if (txtfromdate.Text != "")
            {
                if (RadioButtonList1.SelectedValue == "1" || trrpt.Visible == false)
                {
                    var param = new ReportParameter[2];
                    string[] revdateFrom = txtfromdate.Text.Trim().Split('/');
                    param[0] = new ReportParameter("date", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
                    param[1] = new ReportParameter("type", rbtnList.SelectedValue);
                    var objssrs = new SSRS();
                    objssrs.LoadSSRS(rptEgGetSNABankDetailDataSSRS, "EgSNABankDetailDataRpt", param);
                    trrpt.Visible = true;
                }
                if (RadioButtonList1.SelectedValue == "2")
                {
                    //create PDF
                    byte[] returnValue = null;
                    string format = "PDF";
                    string deviceinfo = "";
                    string mimeType = "";
                    string encoding = "";
                    string extension = "pdf";
                    string[] streams = null;
                    Microsoft.Reporting.WebForms.Warning[] warnings = null;
                    returnValue = rptEgGetSNABankDetailDataSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
                    Response.Buffer = true;
                    Response.Clear();

                    Response.ContentType = mimeType;
                    string FileName = "EgGetSNABankDetailData";
                    Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");

                    Response.BinaryWrite(returnValue);
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                btnEnable();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Fill FromDate');", true);
                return;
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            btnDisable();
            LoadReport();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            trrpt.Visible = false;
            txtfromdate.Text = "";
            //txttodate.Text = "";
            btnEnable();
        }
        public void btnEnable()
        {
            txtfromdate.Enabled = true;
            //txttodate.Enabled = true;
            rbtnList.Enabled = true;
        }
        public void btnDisable()
        {
            txtfromdate.Enabled = false;
            //txttodate.Enabled = false;
            rbtnList.Enabled = false;
        }
    }
}
