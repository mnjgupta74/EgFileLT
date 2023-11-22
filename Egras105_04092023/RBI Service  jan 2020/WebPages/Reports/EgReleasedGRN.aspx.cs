using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

public partial class WebPages_Reports_EgReleasedGRN : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    public void LoadReport()
    {
        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtFromDate.Text.Trim().Split('/');
        param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txtToDate.Text.Trim().Split('/');
        param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        SSRS obj = new SSRS();
        obj.LoadSSRS(rptRG, "ReleasedGrn", param);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        rptSchemaDi.Visible = true;
        EgReleaseDefacedEntryBL objEgReleaseDefacedEntryBL = new EgReleaseDefacedEntryBL();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgReleaseDefacedEntryBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgReleaseDefacedEntryBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        
        objEgReleaseDefacedEntryBL.fillrepeater(rptrReleasedGRN);
        lblAmount.Text= "Total Amount:" + ' ' +objEgReleaseDefacedEntryBL.sumofobject.ToString();
        if (objEgReleaseDefacedEntryBL.flag == 1)
        {
            lblAmount.Visible = true;
            trrpt.Visible = true;
            }
            else
            {
                lblAmount.Visible = false;
                trrpt.Visible = false;
                rptSchemaDi.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);

                return;
            }
 }

    protected void btnpdf_Click(object sender, EventArgs e)
    { 
                LoadReport();
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
                returnValue = rptRG.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=ReleasedGrn.pdf");
                Response.BinaryWrite(returnValue);
                Response.Flush();
                Response.End();
            

        }
}
