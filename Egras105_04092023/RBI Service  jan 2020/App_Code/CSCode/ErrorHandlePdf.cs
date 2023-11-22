using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ErrorHandlePdf
/// </summary>
public class ErrorHandlePdf
{
    public ErrorHandlePdf()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public byte[] Error(string msg)
    {

        using (System.IO.MemoryStream myMemoryStream = new System.IO.MemoryStream())
        {
            Document myDocument = new Document();
            PdfWriter myPDFWriter = PdfWriter.GetInstance(myDocument, myMemoryStream);

            myDocument.Open();

            // Add to content to your PDF here...
            PdfPTable table = new PdfPTable(2);
            PdfPCell header = new PdfPCell(new Phrase("Pdf Could not be Generated"));
            header.Colspan = 2;
            header.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(header);
            table.AddCell("ID:" + msg);
            myDocument.Add(table);
            myDocument.Close();
            byte[] content = myMemoryStream.ToArray();
            return content;
        }
    }
}