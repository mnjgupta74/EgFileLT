using System;
using Microsoft.Reporting.WebForms;
using EgBL;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Configuration;

public partial class WebPages_Reports_SignedPDF : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string signCheck;
    byte[] bytes;
    EgAGSignedReportBL objAGSReport_BL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Write("<Script>alert('Session Expired')</Script>");
            Server.Transfer("~\\webpages\\logout.aspx");
        }

        if (!IsPostBack)
        {

            FillMonth();
            ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString();
            FillYear();
            ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();

            FillPhase();

        }

    }

    private void FillMonth()
    {
        ddlMonth.Items.Insert(0, new ListItem("Select Month", "-1"));
        DateTimeFormatInfo monthinfo = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            ddlMonth.Items.Add(new ListItem(monthinfo.GetMonthName(i), i.ToString()));
        }
        monthinfo = null;
    }
    private void FillYear()
    {
        ddlYear.Items.Insert(0, new ListItem("Select Year", "-1"));
        int currentYear = DateTime.Today.Year;
        for (int i = currentYear; i > 2010; i--)
        {
            ddlYear.Items.Add(new ListItem((i).ToString(), i.ToString()));
        }

    }


    private void FillPhase()
    {
        ddlPhase.Items.Insert(0, new ListItem("Select Account", "-1"));
        ddlPhase.Items.Insert(1, new ListItem("First", "1"));
        ddlPhase.Items.Insert(2, new ListItem("Second", "2"));
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {




        try
        {
            Session["hidID"] = null;
            objAGSReport_BL = null;
            hidPDfBinaryData.Value = "";
            hidSignaturePosition.Value = "";
            hidDSCUserName.Value = "";
            hidDSCSerialKey.Value = "";
            hidThumbprint.Value = "";
            hidDSCValiddate.Value = "";
            hideSignDate.Value = "";
            hidID.Value = "";
            hidDSCId.Value = "";
            hidTreasuryCode.Value = "";


            if (ddlYear.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Please Select Financial Year');", true);
                ddlYear.Focus();
                return;
            }

            if (ddlMonth.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Please Select  Month');", true);
                ddlMonth.Focus();
                return;
            }
            if (rblFileType.SelectedValue == "1")
            {
                if (ddlPhase.SelectedValue == "-1")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Please Select  Phase');", true);
                    ddlPhase.Focus();
                    return;
                }
            }
            pnldgekSign.Visible = false;
            dgekSignDtls.SelectedIndex = -1;
            dgekSignDtls.DataSource = null;
            dgekSignDtls.DataBind();
            BindGrid();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found...');", true);
        }

    }

    private void BindGrid()
    {
        try
        {

            objAGSReport_BL = new EgAGSignedReportBL();
            
            objAGSReport_BL.Mode = "DG1";

            //objAGSReport_BL.TreasuryCode = hidTreasuryCode.Value.ToString();
            objAGSReport_BL.YYYY = Convert.ToInt16(ddlYear.SelectedValue);
            objAGSReport_BL.MM = Convert.ToInt16(ddlMonth.SelectedValue);
            if (rblFileType.SelectedValue == "1")
            {
                objAGSReport_BL.Phase = Convert.ToInt16(ddlPhase.SelectedValue);

                ds = objAGSReport_BL.GetFilesGrid();
                dgekSignDtls.DataSource = null;
                dgekSignDtls.DataBind();
                dgekSignDtls_BankUpload.Visible = false;
                pnldgekSign_BankUpload.Visible = false;
                dgekSignDtls.Visible = false;
                pnldgekSign.Visible = false;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgekSignDtls_BankUpload.Visible = false;
                    pnldgekSign_BankUpload.Visible = false;
                    pnldgekSign.Visible = true;
                    dgekSignDtls.Visible = true;
                    dgekSignDtls.DataSource = ds;
                    dgekSignDtls.DataBind();
                    
                    ds.Dispose();
                    objAGSReport_BL = null;
                    btnFTP.Visible = true;
                }
                else
                {
                    btnFTP.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found..');", true);
                }
            }
            else
            {
                dgekSignDtls_BankUpload.Visible = false;
                pnldgekSign_BankUpload.Visible = false;
                dgekSignDtls.Visible = false;
                pnldgekSign.Visible = false;
                ds = objAGSReport_BL.GetFilesGrid_BankUpload();
                dgekSignDtls_BankUpload.DataSource = null;
                dgekSignDtls_BankUpload.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgekSignDtls_BankUpload.Visible = true;
                    pnldgekSign_BankUpload.Visible = true;
                    dgekSignDtls.Visible = false;
                    pnldgekSign.Visible = false;
                    dgekSignDtls_BankUpload.DataSource = ds;
                    dgekSignDtls_BankUpload.DataBind();
                    
                    ds.Dispose();
                    objAGSReport_BL = null;
                    btnFTP.Visible = true;
                }
                else
                {
                    btnFTP.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found..');", true);
                }
            }
            
        }
        catch (Exception ex)
        {
            btnFTP.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found...');", true);
        }

    }


    [System.Web.Services.WebMethod]
    public static string GetPdfAndDSCFromDB(string hidID, string hidDSCSerialKey, string hidDSCValiddate, string hidDSCUserName, string hidThumbprint, string hidTreasuryCode)
    {
        DSCResponse res = new DSCResponse();
        string JSONString = "";

        try
        {

            Int64 ID = Convert.ToInt64(hidID);
            byte[] bytes;
            EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();

            DataSet ds = new DataSet();
            objAGSReport_BL.Mode = "VER";

            objAGSReport_BL.ID = ID;
            objAGSReport_BL.TreasuryCode = hidTreasuryCode;
            objAGSReport_BL.Thumbprint = hidThumbprint;
            objAGSReport_BL.DSCSerialKey = hidDSCSerialKey;
            // ds = //objAGSReport_BL.VerifyDSC(objAGSReport_BL);
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // = ds.Tables[0].Rows[0]["UnSignData"];
                    bytes = (byte[])ds.Tables[0].Rows[0]["UnSignData"];
                    objAGSReport_BL = null;
                    res.Pdfinbytes = Convert.ToBase64String(bytes);
                    res.Date = DateTime.Now.ToString("ddd MMM d HH:mm:ss \"UTC\"zzz yyyy");
                    ////  res.File = "";
                    ////set Signature Position
                    string SignPosition = "";

                    //// int[] PosArray = ReadY(res.File);  //ReadY method
                    int[] PosArray = ReadY(bytes);

                    for (int i = 0; i <= PosArray.Length - 1; i++)
                    {
                        SignPosition += PosArray[i].ToString() + ",";
                    }
                    res.SignaturePosition = SignPosition.Substring(0, SignPosition.Length - 1);
                    JSONString = JsonConvert.SerializeObject(res);
                    ds.Dispose();
                    objAGSReport_BL = null;
                }
                else
                {
                    JSONString = "VER2";
                }
            }
            else
            {
                JSONString = "VER1";
            }

            return JSONString;
        }
        catch (Exception ex)
        {
            return "VER0";


        }
        //}
    }
    public static int[] ReadY(byte[] bytes)
    {

        PdfReader pdfReader = new PdfReader(bytes);
        int[] position = new int[pdfReader.NumberOfPages];

        for (int page = 1; page <= pdfReader.NumberOfPages; page++)
        {
            PdfReaderContentParser parser = new PdfReaderContentParser(pdfReader);
            TextMarginFinder finder;
            finder = parser.ProcessContent(page, new TextMarginFinder());
            //position[page - 1] = 841 - Convert.ToInt16(finder.GetHeight());
            position[page - 1] = 560 - Convert.ToInt16(finder.GetHeight());
        }
        pdfReader.Close();
        return (position);
    }

    public class DSCResponse
    {
        public string Date { get; set; }
        public string File { get; set; }
        public string SignaturePosition { get; set; }
        public string Pdfinbytes { get; set; }
    }




    protected void btnFTP_Click(object sender, EventArgs e)
    {
        try
        {
            EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();
            int i = objAGSReport_BL.FTPTransfer();
            if (i == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File Successsfully Transfer!!');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Something Wrong!!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('" + ex + "');", true);
        }


    }



    protected void dgekSignDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btneSign1 = (e.Row.FindControl("btneSign") as Button);
            HtmlTableRow tr = (HtmlTableRow)e.Row.FindControl("trID");
            DataRowView drv = e.Row.DataItem as DataRowView;
            string Flag = Convert.ToString(drv["eSignDate"]);
            if (Flag == "")
            {
                btneSign1.BackColor = Color.FromArgb(0, 153, 0);
            }
        }
    }

    protected void dgekSignDtls_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < dgekSignDtls.Rows.Count; i++)
        {
            Button btneSign1 = dgekSignDtls.Rows[i].FindControl("btneSign") as Button;
            btneSign1.Enabled = false;
            btneSign1.BackColor = System.Drawing.Color.Green;
            string eSignDate = dgekSignDtls.SelectedRow.Cells[7].Text.ToString().Trim().Replace("&nbsp;", "");

            Button btneSign = dgekSignDtls.Rows[dgekSignDtls.SelectedIndex].FindControl("btneSign") as Button;
            btneSign.Enabled = false;
            btneSign.BackColor = System.Drawing.Color.Green;

            //Int64 ID = Convert.ToInt64(dgekSignDtls.SelectedItem.Cells[1].Text.ToString().Trim());
            hidID.Value = dgekSignDtls.SelectedRow.Cells[1].Text.ToString().Trim();
            Session["hidID"] = hidID.Value;

            if (eSignDate == "")
            {
                btneSign.Enabled = true;
                //// btneSign.ForeColor = System.Drawing.Color.Red;
                btneSign.BackColor = System.Drawing.Color.Red;
            }

            Response.Write(string.Format("<script>window.open('{0}','new window', 'top=300, left=450, width=1024, height=660, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes');</script>", "SignedPDFView.aspx"));
        }
    }

    protected void dgekSignDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "eSign")
        {
            try
            {
                EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();

                DataSet ds = new DataSet();
                objAGSReport_BL.Mode = "DWN";
                int ID = Convert.ToInt32(e.CommandArgument);

                Button lnk = (e.CommandSource) as Button;
                GridViewRow clickedRow = lnk.NamingContainer as GridViewRow;
                Label lblReportName = (clickedRow.FindControl("lblFName") as Label);
                hidID.Value = ID.ToString();
                objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);


                ds = objAGSReport_BL.DownloadFile();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bytes = (byte[])ds.Tables[0].Rows[0]["BINDATA"];
                        signCheck = ds.Tables[0].Rows[0]["eSign"].ToString();
                    }
                }
                if (signCheck == "YES")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File Alredy Signed.');", true);
                }
                else
                {
                    //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
                    //X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
                    X509Certificate2 cert = new X509Certificate2((Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString())), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
                    PDFSign objpdfsign = new PDFSign();
                    byte[] signedData = objpdfsign.SignDocument(bytes, cert, Server.MapPath("../../Image/right.jpg"));
                    objAGSReport_BL.Mode = "FRZ";
                    objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);
                    objAGSReport_BL.eSignData = signedData;
                    objAGSReport_BL.FName = lblReportName.Text;
                    objAGSReport_BL.DSCUserName = Session["userName"].ToString();
                    objAGSReport_BL.Thumbprint = cert.Thumbprint;
                    objAGSReport_BL.DSCSerialKey = cert.SerialNumber;
                    ////string a = hidDSCValiddate.Value;
                    objAGSReport_BL.DSCValidDate = cert.NotAfter.ToString();////Convert.ToDateTime("14/01/2020");// Convert.ToDateTime(DateTime.ParseExact(hidDSCValiddate.Value, "dd-MM-yyyy", CultureInfo.InvariantCulture)); ////Convert.ToDateTime("01/01/2020");////Convert.ToDateTime(hidDSCValiddate.Value);// Convert.ToDateTime(DateTime.ParseExact(hidDSCValiddate.Value, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    objAGSReport_BL.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    objAGSReport_BL.UserId = Convert.ToInt32(Session["UserId"]);
                    objAGSReport_BL.Type = Convert.ToInt32(rblFileType.SelectedValue);
                    string rslt = objAGSReport_BL.eSignFRZ();
                    BindGrid();
                    if (rslt == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Signed successfully.');", true);
                        BindGrid();
                        Session["hidID"] = null;
                        objAGSReport_BL = null;
                        hidPDfBinaryData.Value = "";
                        hidSignaturePosition.Value = "";
                        hidDSCUserName.Value = "";
                        hidDSCSerialKey.Value = "";
                        hidThumbprint.Value = "";
                        hidDSCValiddate.Value = "";
                        hideSignDate.Value = "";
                        hidID.Value = "";
                        hidDSCId.Value = "";

                    }
                    else
                    {
                        Session["hidID"] = null;
                        objAGSReport_BL = null;
                        hidPDfBinaryData.Value = "";
                        hidSignaturePosition.Value = "";
                        hidDSCUserName.Value = "";
                        hidDSCSerialKey.Value = "";
                        hidThumbprint.Value = "";
                        hidDSCValiddate.Value = "";
                        hideSignDate.Value = "";
                        hidID.Value = "";
                        hidDSCId.Value = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Failed to Sign..');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "  eSign");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Failed to Sign..');", true);
            }
        }
        if (e.CommandName == "Select")
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            hidID.Value = ID.ToString();



            byte[] bytes;
            string fileName = "", contentType;


            EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();

            DataSet ds = new DataSet();
            objAGSReport_BL.Mode = "DWN";

            objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);

            ds = objAGSReport_BL.DownloadFile();



            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    bytes = (byte[])ds.Tables[0].Rows[0]["BINDATA"];
                    // Session["a"] = Convert.ToBase64String(bytes);
                    contentType = "application/pdf";
                    if (ds.Tables[0].Rows[0]["eSign"].ToString() == "NO")
                    {
                        fileName = ds.Tables[0].Rows[0]["FName"].ToString() + ".pdf";
                    }
                    else if (ds.Tables[0].Rows[0]["eSign"].ToString() == "YES")
                    {
                        fileName = "E" + ds.Tables[0].Rows[0]["FName"].ToString() + ".pdf";

                    }

                    //hidFileName.Value = fileName;
                    objAGSReport_BL = null;
                    SSRS objssrs = new SSRS();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found !!! ');", true);
                }
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found !!! ');", true);
            }
        }
    }
    protected void rblFileType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblFileType.SelectedValue == "1")
        {
            ddlMonth.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ds.Dispose();
            ds.Clear();
            tdAccount.Visible = true;
            dgekSignDtls_BankUpload.Visible = false;
            pnldgekSign_BankUpload.Visible = false;
            dgekSignDtls.Visible = false;
            pnldgekSign.Visible = false;
        }
        else
        {
            ddlMonth.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            ds.Dispose();
            ds.Clear();
            tdAccount.Visible = false;
            dgekSignDtls.Visible = false;
            pnldgekSign.Visible = false;
            dgekSignDtls_BankUpload.Visible = false;
            pnldgekSign_BankUpload.Visible = false;
        }
    }    
    protected void dgekSignDtls_BankUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "eSign_BankUpload")
        {
            try
            {
                EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();

                DataSet ds = new DataSet();
                objAGSReport_BL.Mode = "DWN";
                int ID = Convert.ToInt32(e.CommandArgument);

                Button lnk = (e.CommandSource) as Button;
                GridViewRow clickedRow = lnk.NamingContainer as GridViewRow;
                Label lblReportName = (clickedRow.FindControl("lblFName_BankUpload") as Label);
                hidID.Value = ID.ToString();
                objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);


                ds = objAGSReport_BL.DownloadFile_BankUpload();
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bytes = (byte[])ds.Tables[0].Rows[0]["BINDATA"];
                        signCheck = ds.Tables[0].Rows[0]["eSign"].ToString();
                    }
                }
                if (signCheck == "YES")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('File Alredy Signed.');", true);
                }
                else
                {
                    X509Certificate2 cert;
                    byte[] signedData;
                    if (rblFileType.SelectedValue == "1")
                    {
                       
                        cert = new X509Certificate2((Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString())), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
                        PDFSign objpdfsign = new PDFSign();
                        signedData = objpdfsign.SignDocument(bytes, cert, Server.MapPath("../../Image/right.jpg"));
                    }
                    else
                    {
                        cert = new X509Certificate2((Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString())), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
                        Eto_PDFSign TOobjpdfsign = new Eto_PDFSign();
                        signedData = TOobjpdfsign.SignDocument(bytes, cert, Server.MapPath("../../Image/right.jpg"));
                    }
                    //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
                    objAGSReport_BL.Mode = "FRZ";
                    objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);
                    objAGSReport_BL.eSignData = signedData;
                    objAGSReport_BL.FName = lblReportName.Text;
                    objAGSReport_BL.DSCUserName = Session["userName"].ToString();
                    objAGSReport_BL.Thumbprint = cert.Thumbprint;
                    objAGSReport_BL.DSCSerialKey = cert.SerialNumber;
                    ////string a = hidDSCValiddate.Value;
                    objAGSReport_BL.DSCValidDate = cert.NotAfter.ToString();////Convert.ToDateTime("14/01/2020");// Convert.ToDateTime(DateTime.ParseExact(hidDSCValiddate.Value, "dd-MM-yyyy", CultureInfo.InvariantCulture)); ////Convert.ToDateTime("01/01/2020");////Convert.ToDateTime(hidDSCValiddate.Value);// Convert.ToDateTime(DateTime.ParseExact(hidDSCValiddate.Value, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    objAGSReport_BL.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    objAGSReport_BL.UserId = Convert.ToInt32(Session["UserId"]);
                    objAGSReport_BL.Type = Convert.ToInt32(rblFileType.SelectedValue);
                    string rslt = objAGSReport_BL.eSignFRZ();
                    BindGrid();
                    if (rslt == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Signed successfully.');", true);
                        BindGrid();
                        Session["hidID"] = null;
                        objAGSReport_BL = null;
                        hidPDfBinaryData.Value = "";
                        hidSignaturePosition.Value = "";
                        hidDSCUserName.Value = "";
                        hidDSCSerialKey.Value = "";
                        hidThumbprint.Value = "";
                        hidDSCValiddate.Value = "";
                        hideSignDate.Value = "";
                        hidID.Value = "";
                        hidDSCId.Value = "";

                    }
                    else
                    {
                        Session["hidID"] = null;
                        objAGSReport_BL = null;
                        hidPDfBinaryData.Value = "";
                        hidSignaturePosition.Value = "";
                        hidDSCUserName.Value = "";
                        hidDSCSerialKey.Value = "";
                        hidThumbprint.Value = "";
                        hidDSCValiddate.Value = "";
                        hideSignDate.Value = "";
                        hidID.Value = "";
                        hidDSCId.Value = "";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Failed to Sign..');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "  eSign_BankUpload");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('Failed to Sign..');", true);
            }
        }
        if (e.CommandName == "Select_BankUpload")
        {
            int ID = Convert.ToInt32(e.CommandArgument);
            hidID.Value = ID.ToString();



            byte[] bytes;
            string fileName = "", contentType;


            EgAGSignedReportBL objAGSReport_BL = new EgAGSignedReportBL();

            DataSet ds = new DataSet();
            objAGSReport_BL.Mode = "DWN";

            objAGSReport_BL.ID = Convert.ToInt64(hidID.Value);

            ds = objAGSReport_BL.DownloadFile_BankUpload();
            

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    bytes = (byte[])ds.Tables[0].Rows[0]["BINDATA"];
                    // Session["a"] = Convert.ToBase64String(bytes);
                    contentType = "application/pdf";
                    if (ds.Tables[0].Rows[0]["eSign"].ToString() == "NO")
                    {
                        fileName = ds.Tables[0].Rows[0]["FName"].ToString() + ".pdf";
                    }
                    else if (ds.Tables[0].Rows[0]["eSign"].ToString() == "YES")
                    {
                        fileName = "E" + ds.Tables[0].Rows[0]["FName"].ToString() + ".pdf";

                    }

                    //hidFileName.Value = fileName;
                    objAGSReport_BL = null;
                    SSRS objssrs = new SSRS();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found !!! ');", true);
                }
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Msg", "alert('No Record Found !!! ');", true);
            }
        }
    }

    protected void dgekSignDtls_BankUpload_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgekSignDtls_BankUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btneSign_BankUpload = (e.Row.FindControl("btneSign_BankUpload") as Button);
            HtmlTableRow tr = (HtmlTableRow)e.Row.FindControl("trID_BankUpload");
            DataRowView drv = e.Row.DataItem as DataRowView;
            string Flag = Convert.ToString(drv["eSignDate"]);
            if (Flag == "")
            {
                btneSign_BankUpload.BackColor = Color.FromArgb(0, 153, 0);
            }
        }
    }
}