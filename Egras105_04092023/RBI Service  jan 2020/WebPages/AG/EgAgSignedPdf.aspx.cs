using System;
using Microsoft.Reporting.WebForms;
using EgBL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;


public partial class WebPages_Reports_EgAgSignedPdf : System.Web.UI.Page
{
    EgAGSignedReportBL objAGSReport_BL;
    
    string eKTrsyList;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        //{
        //    Response.Write("<Script>alert('Session Expired')</Script>");
        //    Server.Transfer("~\\webpages\\logout.aspx");
        //}

        if (!IsPostBack)
        {
            eKTrsyList = System.Configuration.ConfigurationManager.AppSettings["eKTrsyList"];
            BindYear();
            FillReportName();
            ReportViewer1.Visible = true;
        }
    }
    private void BindYear()
    {
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
    private void FillReportName()
    {
        ddlReportName.Items.Insert(0, new ListItem("Select Report", "-1"));
        ddlReportName.Items.Insert(1, new ListItem("LOR", "LOR"));
        ddlReportName.Items.Insert(2, new ListItem("LOR Details", "LORDetail"));
        ddlReportName.Items.Insert(3, new ListItem("LOP", "PaymentReport"));
        //ddlReportName.Items.Insert(4, new ListItem("TY.33", "EgTy11"));
        ddlReportName.Items.Insert(4, new ListItem("TY.33 Summary", "EgTy11Summary"));
        //ddlReportName.Items.Insert(5, new ListItem("TY.33 Division", "EgTy11DivisionWise"));
        //ddlReportName.Items.Insert(7, new ListItem("TY.34", "TY12"));
        ddlReportName.Items.Insert(5, new ListItem("TY.34 Summary", "EgTY12Summary"));
        ddlReportName.Items.Insert(6, new ListItem("Closing Abstract", "ClosingAbstract"));
        ddlReportName.Items.Insert(7, new ListItem("Covering Letter", "CoveringLetter"));

    }

    //private void FillDivision()
    //{
    //    ObjDivCode.FillAllDivisionList(ddlDivCode);
    //    ddlDivCode.Items.Clear();
    //    ddlDivCode.Items.Insert(0, new ListItem("ALL DIVISION", "0"));

    //}
    protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtFMajorHead.Text = "";

        btnSavePDF.Visible = false;
        trrpt.Visible = false;
        Div2.Visible = false;

        if (ddlReportName.SelectedValue == "EgTy11Summary")
        {
            txtFMajorHead.Enabled = true;
            tdMajorHead.Visible = true;

            trCoveringLetter.Visible = false;
            trReport.Visible = true;


        }
        else if(ddlReportName.SelectedValue == "CoveringLetter")
        {
            trCoveringLetter.Visible = true;
            trReport.Visible = false;
            tdMajorHead.Visible = false;
        }
        else
        {
            txtFMajorHead.Enabled = false;
            tdMajorHead.Visible = false;
            trCoveringLetter.Visible = false;
            trReport.Visible = true;
        }

    }
    public void loadSSRS()
    {
        ReportViewer1.Visible = true;
        ReportParameter[] param = null;
        string RptName = string.Empty;
        RptName = ddlReportName.SelectedValue;

        string[] Rdate = null;
        Rdate = txtfromdate.Text.Split("/".ToCharArray());
        string[] Tdate = null;
        Tdate = txttodate.Text.Split("/".ToCharArray());
        if (ddlReportName.SelectedValue == "EgTy11Summary")
        {
            RptName = "EgTy11Summary";
            param = new ReportParameter[4];
            param[0] = new ReportParameter("Fromdate", (Rdate[2] + "/" + Rdate[1] + "/" + Rdate[0]));
            param[1] = new ReportParameter("Todate", (Tdate[2] + "/" + Tdate[1] + "/" + Tdate[0]));
            param[2] = new ReportParameter("majorHead", (txtFMajorHead.Text.Trim() == "" ? null : txtFMajorHead.Text.Trim()));
            param[3] = new ReportParameter("divisioncode", (""));
            ReportViewer1.Visible = true;
        }

        if (ddlReportName.SelectedValue == "EgTY12Summary")
        {
            RptName = "EgTY12Summary";
            param = new ReportParameter[2];
            param[0] = new ReportParameter("fromdate", (Rdate[2] + "/" + Rdate[1] + "/" + Rdate[0]));
            param[1] = new ReportParameter("todate", (Tdate[2] + "/" + Tdate[1] + "/" + Tdate[0]));

        }

        else if (ddlReportName.SelectedValue == "LOR" || ddlReportName.SelectedValue == "LORDetail")
        {
            param = new ReportParameter[2];

            param[0] = new ReportParameter("fromdate", (Rdate[2] + "/" + Rdate[1] + "/" + Rdate[0]));
            param[1] = new ReportParameter("todate", (Tdate[2] + "/" + Tdate[1] + "/" + Tdate[0]));
            ReportViewer1.Visible = true;
        }
        else if (ddlReportName.SelectedValue == "PaymentReport")
        {
            param = new ReportParameter[2];
            param[0] = new ReportParameter("fromdate", (Rdate[2] + "/" + Rdate[1] + "/" + Rdate[0]));
            param[1] = new ReportParameter("todate", (Tdate[2] + "/" + Tdate[1] + "/" + Tdate[0]));
            ReportViewer1.Visible = true;
        }

        else if (ddlReportName.SelectedValue == "ClosingAbstract")
        {
            param = new ReportParameter[2];
            param[0] = new ReportParameter("Fromdate", (Rdate[2] + "/" + Rdate[1] + "/" + Rdate[0]));
            param[1] = new ReportParameter("Todate", (Tdate[2] + "/" + Tdate[1] + "/" + Tdate[0]));
            ReportViewer1.Visible = true;
        }
        //else if (ddlReportName.SelectedValue == "PaymentReport")
        //{
        //    param = new ReportParameter[3];
        //    param[0] = new ReportParameter("ListNo", (rbllisttype.SelectedItem.Text));
        //    param[1] = new ReportParameter("Month", (ddlstartmonth.SelectedItem.Text));
        //    param[2] = new ReportParameter("Year", (ddlyear.SelectedItem.Text));
        //}
        SSRS objssrs = new SSRS();
        objssrs.LoadSSRS(ReportViewer1, RptName, param);
        trrpt.Visible = true;

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if(ddlReportName.SelectedValue == "CoveringLetter")
        {
            if (ddlReportName.SelectedValue == "-1")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please Select Reprot Name');", true);
                ddlReportName.Focus();
                return;
            }

            if (ddlstartmonth.SelectedValue == "0" )
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please Select Month');", true);
                txtfromdate.Focus();
                return;
            }
            else
            {
                ddlstartmonth.Enabled = false;
                ddlyear.Enabled = false;
                rbllisttype.Enabled = false;
                txtfromdate.Enabled = false;
                txttodate.Enabled = false;
                txtFMajorHead.Enabled = false;
                btnshow.Enabled = false;
                ddlReportName.Enabled = false;
                hidFName.Value = ddlReportName.SelectedValue;
                hidYYYY.Value = ddlyear.SelectedValue;
                hidMM.Value = ddlstartmonth.SelectedValue;
                hidReqSign.Value = "N";
                hidPhase.Value = rbllisttype.SelectedValue;
                btnSavePDF.Visible = true;
                trrpt.Visible = false;
                Div2.Visible = true;
                loadreport();
            }
            
        }
        else
        {
            btnSavePDF.Visible = false;
            //hidReqSign.Value = "Y";
            if (ddlReportName.SelectedValue == "-1")
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please Select Reprot Name');", true);
                ddlReportName.Focus();
                return;
            }

            if (txtfromdate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please Enter From Date Here');", true);
                txtfromdate.Focus();
                return;
            }

            if (txttodate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Please Enter To Date Here');", true);
                txttodate.Focus();
                return;
            }

            DateTime date1 = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", null).Date;
            DateTime date2 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", null).Date;
            TimeSpan ts = date2.Subtract(date1);
            int daydiff = Math.Abs(ts.Days);

            if (Convert.ToInt16(daydiff) >= 31)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Maximum One MONTH Data Can be Fetched !');", true);

                txtfromdate.Focus();
                return;
            }
            hidSummary.Value = "NO";
            loadSSRS();
            txtfromdate.Enabled = false;
            txttodate.Enabled = false;
            txtFMajorHead.Enabled = false;
            ddlReportName.Enabled = false;
            btnshow.Enabled = false;
            // eSign part


            hidPhase.Value = "";
            hidFName.Value = "";
            hidYYYY.Value = "";
            hidMM.Value = "";


            DateTime lastDate = new DateTime(date1.Year, date1.Month, 1).AddMonths(1).AddDays(-1);
            int LastDay = lastDate.Day;
            if (date1 < DateTime.ParseExact("01/07/2018", "dd/MM/yyyy", null).Date)   //FTP On date  Jp Gupta
            {
                return;
            }


            hidReqSign.Value = "N";

            if ((date1.Day == 1 && date2.Day == 10) || (date1.Day == 1 && date2.Day == LastDay))
            {
                if (date1.Day == 1 && date2.Day == 10) { hidPhase.Value = "1"; }
                if (date1.Day == 1 && date2.Day == LastDay) { hidPhase.Value = "2"; }

                //string MM = "";
                if (date1.Month.ToString().Length == 1)
                { hidMM.Value = "0" + date1.Month.ToString(); }
                else
                { hidMM.Value = date1.Month.ToString(); }

                hidYYYY.Value = date1.Year.ToString();

                hidFName.Value = ddlReportName.SelectedValue;
                btnSavePDF.Visible = true;
            }

            // End of eSign part
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        trrpt.Visible = false;
        btnSavePDF.Visible = false;
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        txtFMajorHead.Enabled = true;
        btnshow.Enabled = true;
        ddlReportName.Enabled = true;
        Div2.Visible = false;
        ddlstartmonth.Enabled = true;
        ddlyear.Enabled = true;
        rbllisttype.Enabled = true;
    }

    protected void btnSavePDF_Click(object sender, EventArgs e)
    {
        SavePDF();
    }
    public void loadssrsreport()
    {
        
        ReportParameter[] param = null;
        string RptName = string.Empty;
        RptName = ddlReportName.SelectedValue;
        if (ddlstartmonth.SelectedValue != "" && ddlyear.SelectedValue != "")
        {
            param = new ReportParameter[3];
            param[0] = new ReportParameter("ListNo", (rbllisttype.SelectedItem.Text));
            param[1] = new ReportParameter("Month", (ddlstartmonth.SelectedItem.Text));
            param[2] = new ReportParameter("Year", (ddlyear.SelectedItem.Text));
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(ReportViewer1, RptName, param);
            ReportViewer1.Visible = false;
        }
    }
    private void SavePDF()
    {
        if(ddlReportName.SelectedValue == "CoveringLetter")
        {
            loadssrsreport();
        }
        // validations pending
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string filenameExtension = "pdf";

        byte[] bytes = this.ReportViewer1.ServerReport.Render("pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
        objAGSReport_BL = new EgAGSignedReportBL();
        if (hidFName.Value == "LOR")
        {
            objAGSReport_BL.DocID = 1;
        }
        if (hidFName.Value == "LORDetail")
        {
            objAGSReport_BL.DocID = 2;
        }
        if (hidFName.Value == "PaymentReport")
        {
            objAGSReport_BL.DocID = 3;
        }
        if (hidFName.Value == "EgTy11Summary")
        {
            objAGSReport_BL.DocID = 4;
        }
        if (hidFName.Value == "EgTY12Summary")
        {
            objAGSReport_BL.DocID = 5;
        }
        if (hidFName.Value == "ClosingAbstract")
        {
            objAGSReport_BL.DocID = 6;
        }
        if (hidFName.Value == "CoveringLetter")
        {
            objAGSReport_BL.DocID = 7;
        }

        objAGSReport_BL.Mode = "SAV";
        objAGSReport_BL.FName = hidFName.Value;
        objAGSReport_BL.YYYY = Convert.ToInt32(hidYYYY.Value);
        objAGSReport_BL.MM = Convert.ToInt16(hidMM.Value);
        objAGSReport_BL.Phase = Convert.ToInt16(hidPhase.Value);
        objAGSReport_BL.ReqSign = hidReqSign.Value;
        objAGSReport_BL.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
        objAGSReport_BL.UserId = Convert.ToInt32(Session["UserId"].ToString());
        objAGSReport_BL.UnSignData = bytes;
        int result = objAGSReport_BL.SavePDF();

        if (result == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Something is Wrong !!! ');", true);
            return;
        }

        if (result == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File Saved Successfully !!! ');", true);
            return;
        }
        if (result == -1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File has been already signed for this period/Phase !!! ');", true);
            return;
        }

        if (result == 3)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Could Not be saved for eSign,Please clear the pendancy of uploading documnets and then save for eSign');", true);
            return;
        }

        if (result == 2)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File has already saved and updated successfully !!!');", true);
            return;
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
}