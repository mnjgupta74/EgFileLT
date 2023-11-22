using System;
using System.Web.UI;
using Microsoft.Reporting.WebForms;

namespace WebPages.Reports
{
    public partial class WebPages_EgSNAReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
            {
                Response.Redirect("~\\LoginAgain.aspx");
            }
        }
        protected void LoadReport()
        {
            if (txtfromdate.Text != "")
            {
                //if (RadioButtonList1.SelectedValue == "1" || trrpt.Visible == false)
                //{
                    var param = new ReportParameter[1];
                    string[] revdateFrom = txtfromdate.Text.Trim().Split('/');
                    param[0] = new ReportParameter("date", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
                    var objssrs = new SSRS();
                    var ssrsname = rbtnList.SelectedValue == "0" ? "EgSNAData" : rbtnList.SelectedValue == "1" ? "EgSNABankData" : "EgSNAGeneratedGRN";
                    objssrs.LoadSSRS(rbtnList.SelectedValue == "0" ? rptEgSNADataSSRS : rbtnList.SelectedValue == "1" ? rptEgSNABankDataSSRS : rptEgSNAGeneratedGRNSSRS, ssrsname, param);
                    //trrpt.Visible = true;
                //}
                //if (RadioButtonList1.SelectedValue == "2")
                //{
                    //create PDF
                    byte[] returnValue = null;
                    string format = "PDF";
                    string deviceinfo = "";
                    string mimeType = "";
                    string encoding = "";
                    string extension = "pdf";
                    string[] streams = null;
                    Microsoft.Reporting.WebForms.Warning[] warnings = null;
                    returnValue = (rbtnList.SelectedValue == "0" ? rptEgSNADataSSRS : rbtnList.SelectedValue == "1" ? rptEgSNABankDataSSRS : rptEgSNAGeneratedGRNSSRS).ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
                    Response.Buffer = true;
                    Response.Clear();

                    Response.ContentType = mimeType;
                    string FileName = rbtnList.SelectedValue == "0" ? "EgSNAData" : rbtnList.SelectedValue == "1" ? "EgSNABankData" : "EgSNAGeneratedGRN"; ;
                    Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");

                    Response.BinaryWrite(returnValue);
                    Response.Flush();
                    Response.End();
                //}
            }
            else
            {
                
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Fill FromDate');", true);
                return;
            }
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            trrpt.Visible = false;
        }
    }
}
