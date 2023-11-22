using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
public partial class WebPages_FrmCoveringLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["dt"] = "";
            int year = DateTime.Now.Year;
            int lastyear = (year - 2);
            ArrayList g = new ArrayList();
            for (int i = year; i >= lastyear; i--)
            {
                g.Add(i);
            }
            ddlyear.DataSource = g;
            ddlyear.DataBind();
        }
        else
        {
            if (ViewState["dt"] != null)
            {
                loadreport();
            }
        }
    }
    public void loadreport()
    {
        if (rbllisttype.SelectedValue != "")
        {
            //ReportDocument objreport = new ReportDocument();
            string date = System.DateTime.Now.ToString("dd-MM-yyyy");
            string codee;
            codee = "E-Treasury";

            Bitmap image = CreateBarCode(codee);
            image.Save(Server.MapPath("~\\Image\\CoverletterBarcode.jpg"));
            image1.Visible = true;
            image1.ImageUrl = "~/Image/CoverletterBarcode.jpg";//displaying barcode
            Barcode.ImageUrl = "~/Image/CoverletterBarcode.jpg";//displaying barcode
            image.Dispose();
            month.InnerHtml = ddlstartmonth.SelectedItem.ToString() + "-" + ddlyear.Text.ToString().Substring(0, 4);    //objreport.Month + "-" + objreport.year.Substring(0, 4);
            month1.InnerHtml = ddlstartmonth.SelectedItem.ToString() + "-" + ddlyear.Text.ToString().Substring(0, 4);
            
            Treasuryname.InnerHtml = "E-Treasury Office, Jaipur";
            Treasury.InnerHtml = "Treasury Officer </br>   E-Treasury Office, Jaipur";
            Treasury1.InnerHtml = "Treasury Officer </br>  E-Treasury Office, Jaipur";
            
            datee.InnerHtml = date;
            datee1.InnerHtml = date;
            Finyear.InnerHtml = ddlyear.SelectedValue.ToString();
            Finyear1.InnerHtml = ddlyear.SelectedValue.ToString();
            ToName.InnerHtml = "";
            toname1.InnerHtml = ""; 
            //treasurycode.InnerHtml = "Treasury Officer E-Treasury Jaipur";
            list2.InnerHtml = rbllisttype.SelectedItem.Text;
            list3.InnerHtml = rbllisttype.SelectedItem.Text;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Select List Type')", true);
            return;
        }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        loadreport();
        if (rbllisttype.SelectedValue != "")
        {
            Div1.Visible = false;
            Div2.Visible = true;
            Div3.Visible = true;
        }

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Div3.Visible = false;
        Div2.Visible = false;
        Div1.Visible = true;
    }
    #region[For Barcode]
    protected Bitmap CreateBarCode(string data)
    {
        string Code = data;
        // Multiply the lenght of the code by 30 (just to have enough width)
        int w = Code.Length * 20;
        // Create a bitmap object of the width that we calculated and height of 100
        Bitmap oBitmap = new Bitmap(w, 25);
        // then create a Graphic object for the bitmap we just created.
        Graphics oGraphics = Graphics.FromImage(oBitmap);
        // Now create a Font object for the Barcode Font
        // (in this case the IDAutomationHC39M) of 18 point size
        PrivateFontCollection fnts = new PrivateFontCollection();
        fnts.AddFontFile(Server.MapPath("~/WebPages/font/IDAutomationHC39M.ttf"));
        FontFamily fntfam = new FontFamily("IDAutomationHC39M", fnts);
        Font oFont = new Font(fntfam, 18);
        // Let's create the Point and Brushes for the barcode
        PointF oPoint = new PointF(2f, 2f);
        SolidBrush oBrushWrite = new SolidBrush(Color.Black);
        SolidBrush oBrush = new SolidBrush(Color.White);
        // Now lets create the actual barcode image
        // with a rectangle filled with white color
        oGraphics.FillRectangle(oBrush, 0, 0, w, 100);
        // We have to put prefix and sufix of an asterisk (*),
        // in order to be a valid barcode
        oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);
        // Then we send the Graphics with the actual barcode
        //Response.ContentType = "image/jpeg";
        //oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
        oGraphics.Dispose();
        return oBitmap;
    }
    public void loadssrsreport()
    {

        if (ddlstartmonth.SelectedValue != "" && ddlyear.SelectedValue != "")
        {
            ReportParameter[] param = new ReportParameter[3];
            //string[] revdateFrom, revdateTo;
            //string divisioncode = null;
            //divisioncode = (rbtnList.SelectedValue.ToString().Trim() == "EgTy11DivisionWise".ToString().Trim()) ? divcode.SelectedValue.Split('|').GetValue(0).ToString() : null;
            //revdateFrom = txtfromdate.Text.Trim().Split('/');
            //param[0] = new ReportParameter("Fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            //revdateTo = txttodate.Text.Trim().Split('/');

            //if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
            //    return;
            //}

            param[0] = new ReportParameter("ListNo", (rbllisttype.SelectedItem.Text));
            param[1] = new ReportParameter("Month", (ddlstartmonth.SelectedItem.Text));
            param[2] = new ReportParameter("Year", (ddlyear.SelectedItem.Text));
            //string rptname = rbtnList.SelectedItem.Value.ToString();
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptCoverletter, "CoveringLetter", param);
        }
    }
    protected void btnSignPdf_Click(object sender, EventArgs e)
    {
        EgDigitalSignPdf Objdigitalsign = new EgDigitalSignPdf();

        loadssrsreport();

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

        returnValue = rptCoverletter.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
        // X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
        X509Certificate2 cert = new X509Certificate2((Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString())), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
        PDFSign objpdfsign = new PDFSign();
        byte[] signedData = objpdfsign.SignDocument(returnValue, cert, Server.MapPath("../../Image/right.jpg"));
        Objdigitalsign.PageName = "CoverLetter";
        Objdigitalsign.SignData = signedData;
        //Objdigitalsign.Duration = txtfromdate.Text + '-' + txttodate.Text;
        // Objdigitalsign.InsertSignData();
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=CoverLetter.pdf");

        Response.BinaryWrite(signedData);
        Response.Flush();
        Response.End();
    }
    #endregion
}    

