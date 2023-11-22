using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System.Linq;
using Microsoft.Reporting.WebForms;

namespace WebPages.Reports
{
    public partial class WebPagesReportsEgReportTy11 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
            {
                Response.Redirect("~\\LoginAgain.aspx");
            }
            if (!IsPostBack)
            {
                var objEChallan = new EgEChallanBL();
                objEChallan.FillTreasury(ddlTreasury);

                if ((Session["UserType"].ToString().Trim() == "3".Trim()))
                {
                    var objEgBankSoftCopyBl = new EgBankSoftCopyBL {UserId = Convert.ToInt32(Session["UserID"])};
                    ddlTreasury.SelectedValue = objEgBankSoftCopyBl.GetBSRCode().Trim();
                    ddlTreasury.Enabled = false;
                }
            }
        }
        public void Loadreport()
        {
            DataTable  dt = new DataTable();
            var objTy11 = new EgTy11BL();
            string[] revdateFrom = txtFromDate.Text.Trim().Split('/');
            objTy11.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
            string[] revdateTo = txtToDate.Text.Trim().Split('/');
            objTy11.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
            objTy11.tcode = ddlTreasury.SelectedValue.Trim();
            if (txtMajorHead.Text != "")
            {
                objTy11.majorHead = txtMajorHead.Text.Trim();
            }
            dt = objTy11.BindTy11Grid();
            if (dt.Rows.Count > 0)
            {
                grdTy11Rpt.DataSource = dt;
                grdTy11Rpt.DataBind();
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("CashAmt"));
                lblTotalAmount.Text = "Total Amount: " + Convert.ToDouble(total.ToString("0.00"));
            }
            dt.Dispose();
        }
        protected void btnshow_Click(object sender, EventArgs e)
        {
            Loadreport();
        }
        protected void grdTy11Rpt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var objEncryptDecrypt = new EgEncryptDecrypt();
            if (e.CommandName == "grnbind")
            {
                var lb = (LinkButton)e.CommandSource;
                var strUrlWithData = objEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
                var script = "window.open('../EgDefaceDetailNew.aspx?" + strUrlWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
                ScriptManager.RegisterClientScriptBlock(Page, GetType(), "PopupScript", script, true);
            }
        }

        protected void grdTy11Rpt_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            Loadreport();
            grdTy11Rpt.PageIndex = e.NewPageIndex;
            grdTy11Rpt.DataBind();
        }

        protected void grdTy11Rpt_DataBound(object sender, EventArgs e)
        {
            lblTotalRow.Text = "Total Records: " + (grdTy11Rpt.DataSource as DataTable).Rows.Count;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport("pdf", "PDF");
        }
        private void PrintReport(string extention, string fileformate)
        {
            ReportParameter[] param = new ReportParameter[4];
            string[] revdateFrom, revdateTo;
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            param[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txtToDate.Text.Trim().Split('/');
            param[1] = new ReportParameter("Todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("BudgetHead", txtMajorHead.Text);
            param[3] = new ReportParameter("tcode", ddlTreasury.SelectedValue);

            if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
                return;
            }
            string rptname = string.Empty;

            rptname = "Ty33HeadWise";

            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptTy33HeadWise, rptname, param);
            // create PDF
            // if (Response.IsClientConnected) { Response.Flush(); }
            byte[] returnValue = null;
            string format = fileformate;
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string extension = extention;
            string[] streams = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            returnValue = rptTy33HeadWise.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=Ty33HeadWise." + extension);
            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
    }
}
