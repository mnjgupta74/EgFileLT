
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebPages_admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ProFiles.ProrataDetail obj = new ProFiles.ProrataDetail();
        // Proratatest.ProrataDetail obj = new Proratatest.ProrataDetail();

        GRNPDF.EgGrnPdfService obj = new GRNPDF.EgGrnPdfService();

        // GRNPDF.eggrnpdfservice obj = new GRNPDF.eggrnpdfservice();


        //string filepath = Path.GetFileName(fileDTA.PostedFile.FileName);
        // string path = Server.MapPath("~/Document/" + "eGras.pdf");
        byte[] keyBytes = obj.GetGRNPDF("uqHeNDr2Ot08HrrAqAvEwu2lt+RjbKeufH2ue8j2OIVpwrjvpYm/boxX4NwELnmJ", 5006);
        //System.IO.File.ReadAllBytes(path);
        // SignDetachedResource(keyBytes);
        // int[] PosArray = ReadY(keyBytes);

        //string SignPosition = "";

        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = "";
        Response.AddHeader("content-disposition", "attachment; filename=ProRata" + DateTime.Now.ToShortDateString() + ".pdf");
        Response.BinaryWrite(keyBytes);
        Response.Flush();
        Response.End();
        Response.Close();

        //using (MemoryStream myMemoryStream = new MemoryStream())
        //{
        //    Document myDocument = new Document();
        //    PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

        //    myDocument.Open();

        //    // Add to content to your PDF here...
        //    PdfPTable table = new PdfPTable(2);
        //    PdfPCell header = new PdfPCell(new Phrase("Your Heading"));
        //    header.Colspan = 2;
        //    header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
        //    table.AddCell(header);
        //    table.AddCell("ID:" + "abc");
        //    myDocument.Add(table);
        //    myDocument.Close();

        //    byte[] content = myMemoryStream.ToArray();

        //    // Write out PDF from memory stream.
        //    using (FileStream fs = File.Create("C:\\Test.pdf"))
        //    {
        //        fs.Write(content, 0, (int)content.Length);
        //    }
        //}

    }
}
