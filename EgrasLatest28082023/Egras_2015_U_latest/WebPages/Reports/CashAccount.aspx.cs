using System;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Collections.Generic;
using Org.BouncyCastle.Pkcs;
using iTextSharp.text;
using System.Security.Cryptography;
using System.IO;
using EgBL;
using System.Configuration;

public partial class CashAccount : System.Web.UI.Page
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
            calendartodate.EndDate = DateTime.Now;
        }
    }
    protected void LoadReport()
    {

        ReportParameter[] param = new ReportParameter[2];
        string[] revdateFrom, revdateTo;
        revdateFrom = txtfromdate.Text.Trim().Split('/');
        param[0] = new ReportParameter("fromdate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
        revdateTo = txttodate.Text.Trim().Split('/');
        param[1] = new ReportParameter("todate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
        if ((Convert.ToDateTime(revdateTo[2].ToString() + "/" + revdateTo[1].ToString() + "/" + revdateTo[0].ToString()) - Convert.ToDateTime(revdateFrom[2].ToString() + "/" + revdateFrom[1].ToString() + "/" + revdateFrom[0].ToString())).TotalDays > 180)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 180 days');", true);
            return;
        }
        string rptname = string.Empty;
        if (rbtnList.SelectedValue.Trim() == "LOR".Trim())
        {
            rptname = "LOR";
        }
        else
        {
            rptname = "LORDetail";

        }
        SSRS objssrs = new SSRS();
        //objssrs.LoadSSRS(rptLORSSRS, "LOR", param);
        objssrs.LoadSSRS(rptLORSSRS, rptname, param);
        trrpt.Visible = true;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        LoadReport();
        btnDisable();
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        

        if (trrpt.Visible == false)
        {
            LoadReport();
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

        returnValue = rptLORSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);

       
        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
       
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=LOR.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();
    }

    protected void btnSignPdf_Click(object sender, EventArgs e)
    {
        EgDigitalSignPdf Objdigitalsign = new EgDigitalSignPdf();

        if (trrpt.Visible == false)
        {
            LoadReport();
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

        returnValue = rptLORSSRS.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);


        //string path = (System.Configuration.ConfigurationManager.AppSettings["ServerCertficate"]);
        // X509Certificate2 cert = new X509Certificate2(Server.MapPath(@"~\Certificate\kamal preet kaur.pfx"), "123");
        X509Certificate2 cert= new X509Certificate2(System.Web.Configuration.WebConfigurationManager.AppSettings["SecureCertificate"] + ConfigurationManager.AppSettings["Certificate"].ToString(), ConfigurationManager.AppSettings["CertificatePassword"].ToString());
        PDFSign objpdfsign = new PDFSign();
        byte[] signedData =objpdfsign.SignDocument(returnValue, cert, Server.MapPath("../../Image/right.jpg"));
        Objdigitalsign.PageName = "LOR";
        Objdigitalsign.SignData = signedData;
        Objdigitalsign.Duration = txtfromdate.Text + '-' + txttodate.Text;
       // Objdigitalsign.InsertSignData();
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=LOR.pdf");

        Response.BinaryWrite(signedData);
        Response.Flush();
        Response.End();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        trrpt.Visible = false;
        btnEnable();
    }
    public void btnEnable()
    {
        txtfromdate.Enabled = true;
        txttodate.Enabled = true;
        rbtnList.Enabled = true;
        btnSubmit.Enabled = true;
    }
    public void btnDisable()
    {
        txtfromdate.Enabled = false;
        txttodate.Enabled = false;
        rbtnList.Enabled = false;
        btnSubmit.Enabled = false;
    }

    //private byte[] SignDocument(byte[] pdfData, X509Certificate2 cert)
    //{
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        var reader = new PdfReader(pdfData);
    //        var stp = PdfStamper.CreateSignature(reader, stream, '\0');
    //        var sap = stp.SignatureAppearance;
    //        int TotalPage = reader.NumberOfPages;

    //        //Protect certain features of the document 
    //        stp.SetEncryption(null,
    //            Guid.NewGuid().ToByteArray(), //random password 
    //            PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_COPY | PdfWriter.ALLOW_SCREENREADERS,
    //            PdfWriter.ENCRYPTION_AES_256);

    //        //Get certificate chain
    //        var cp = new Org.BouncyCastle.X509.X509CertificateParser();
    //        var certChain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };

    //        //Set signature appearance
    //        BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
    //        Font font = new Font(helvetica, 10, iTextSharp.text.Font.NORMAL);
    //        sap.Layer2Font = font;
    //        sap.Certificate = certChain[0];
    //        sap.SetVisibleSignature(new iTextSharp.text.Rectangle(415, 100, 585, 40), TotalPage, "CertificationSignature");

    //        var dic = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);
    //        //Set some stuff in the signature dictionary.
    //        dic.Date = new PdfDate(sap.SignDate);

    //        dic.Name = cert.Subject;    //Certificate name

    //        var image = iTextSharp.text.Image.GetInstance(Server.MapPath("../../Image/right.jpg"));
    //        sap.Acro6Layers = true;
    //        sap.SignatureGraphic = image;
    //        sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
    //        sap.SignatureCreator = "Digitally Signed by e-Treasury";
    //        sap.Reason = "Signature";
    //        sap.Location = "Jaipur";

    //        if (sap.Reason != null)
    //        {
    //            dic.Reason = sap.Reason;
    //        }
    //        if (sap.Location != null)
    //        {
    //            dic.Location = sap.Location;
    //        }
    //        dic.SignatureCreator = sap.SignatureCreator;
    //        //Set the crypto dictionary 

    //        //Set the crypto dictionary 
    //        sap.CryptoDictionary = dic;

    //        //Set the size of the certificates and signature. 
    //        int csize = 8192; //Size of the signature - 4K


    //        //Reserve some space for certs and signatures
    //        var reservedSpace = new Dictionary<PdfName, int>();
    //        reservedSpace[PdfName.CONTENTS] = csize * 2 + 2; //*2 because binary data is stored as hex strings. +2 for end of field
    //        sap.PreClose(reservedSpace);    //Actually reserve it 

    //        //Build the signature 
    //        HashAlgorithm sha = new SHA1CryptoServiceProvider();

    //        var sapStream = sap.GetRangeStream();
    //        int read = 0;
    //        byte[] buff = new byte[8192];

    //        while ((read = sapStream.Read(buff, 0, 8192)) > 0)
    //        {
    //            sha.TransformBlock(buff, 0, read, buff, 0);
    //        }

    //        sha.TransformFinalBlock(buff, 0, 0);

    //        byte[] pk = SignMsg(sha.Hash, cert, false);

    //        //Put the certs and signature into the reserved buffer 
    //        byte[] outc = new byte[csize];
    //        Array.Copy(pk, 0, outc, 0, pk.Length);

    //        //Put the reserved buffer into the reserved space 
    //        PdfDictionary certificateDictionary = new PdfDictionary();
    //        certificateDictionary.Put(PdfName.CONTENTS, new PdfString(outc).SetHexWriting(true));

    //        //Write the signature 
    //        sap.Close(certificateDictionary);
    //        //Close the stamper and save it 
    //        stp.Close();

    //        reader.Close();

    //        //Return the saved pdf 
    //        return stream.GetBuffer();
    //    }
    //}

    //private byte[] SignMsg(Byte[] msg, X509Certificate2 cert, bool detached)
    //{
    //    //Place message in a ContentInfo object. This is required to build a SignedCms object. 
    //    ContentInfo contentInfo = new ContentInfo(msg);

    //    //Instantiate SignedCms object with the ContentInfo above. 
    //    //Has default SubjectIdentifierType IssuerAndSerialNumber. 
    //    SignedCms signedCms = new SignedCms(contentInfo, detached);

    //    //Formulate a CmsSigner object for the signer. 
    //    CmsSigner cmsSigner = new CmsSigner(cert);  //First cert in the chain is the signer cert

    //    //Do the whole certificate chain. This way intermediate certificates get sent across as well.
    //    cmsSigner.IncludeOption = X509IncludeOption.ExcludeRoot;

    //    //Sign the CMS/PKCS #7 message. The second argument is needed to ask for the pin.
    //    signedCms.ComputeSignature(cmsSigner,true);

    //    //Encode the CMS/PKCS #7 message. 
    //    return signedCms.Encode();
    //}

}
