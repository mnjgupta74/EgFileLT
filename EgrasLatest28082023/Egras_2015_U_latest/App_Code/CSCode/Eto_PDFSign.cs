using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Web;

/// <summary>
/// Summary description for PDFSign
/// </summary>
public class Eto_PDFSign
{
    public Eto_PDFSign()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public byte[] SignDocument(byte[] pdfData, X509Certificate2 cert,string path)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            var reader = new PdfReader(pdfData);
            PdfReader.unethicalreading = true;
            var stp = PdfStamper.CreateSignature(reader, stream, '\0');
            var sap = stp.SignatureAppearance;
            int TotalPage = reader.NumberOfPages;

            //Protect certain features of the document 
            stp.SetEncryption(null,
                Guid.NewGuid().ToByteArray(), //random password 
                PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_COPY | PdfWriter.ALLOW_SCREENREADERS,
                PdfWriter.ENCRYPTION_AES_256);

            //Get certificate chain
            var cp = new Org.BouncyCastle.X509.X509CertificateParser();
            var certChain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };

            //Set signature appearance
            BaseFont helvetica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font font = new Font(helvetica, 10, iTextSharp.text.Font.NORMAL);
            sap.Layer2Font = font;
            sap.Certificate = certChain[0];
            sap.SetVisibleSignature(new iTextSharp.text.Rectangle(10, 100, 485, 40), TotalPage, "CertificationSign");

            var dic = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);
            //Set some stuff in the signature dictionary.
            dic.Date = new PdfDate(sap.SignDate);

            dic.Name = cert.Subject;    //Certificate name

            var image = iTextSharp.text.Image.GetInstance(path);
            sap.Acro6Layers = true;
            sap.SignatureGraphic = image;
            sap.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
            sap.SignatureCreator = "Digitally Signed by e-Treasury";
            sap.Reason = "Signature";
            sap.Location = "Jaipur";

            if (sap.Reason != null)
            {
                dic.Reason = sap.Reason;
            }
            if (sap.Location != null)
            {
                dic.Location = sap.Location;
            }
            dic.SignatureCreator = sap.SignatureCreator;
            //Set the crypto dictionary 

            //Set the crypto dictionary 
            sap.CryptoDictionary = dic;

            //Set the size of the certificates and signature. 
            int csize = 8192; //Size of the signature - 4K


            //Reserve some space for certs and signatures
            var reservedSpace = new Dictionary<PdfName, int>();
            reservedSpace[PdfName.CONTENTS] = csize * 2 + 2; //*2 because binary data is stored as hex strings. +2 for end of field
            sap.PreClose(reservedSpace);    //Actually reserve it 

            //Build the signature 
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            var sapStream = sap.GetRangeStream();
            int read = 0;
            byte[] buff = new byte[8192];

            while ((read = sapStream.Read(buff, 0, 8192)) > 0)
            {
                sha.TransformBlock(buff, 0, read, buff, 0);
            }

            sha.TransformFinalBlock(buff, 0, 0);

            byte[] pk = SignMsg(sha.Hash, cert, false);

            //Put the certs and signature into the reserved buffer 
            byte[] outc = new byte[csize];
            Array.Copy(pk, 0, outc, 0, pk.Length);

            //Put the reserved buffer into the reserved space 
            PdfDictionary certificateDictionary = new PdfDictionary();
            certificateDictionary.Put(PdfName.CONTENTS, new PdfString(outc).SetHexWriting(true));

            //Write the signature 
            sap.Close(certificateDictionary);
            //Close the stamper and save it 
            stp.Close();

            reader.Close();

            //Return the saved pdf 
            return stream.GetBuffer();
        }
    }

    private byte[] SignMsg(Byte[] msg, X509Certificate2 cert, bool detached)
    {
        //Place message in a ContentInfo object. This is required to build a SignedCms object. 
        System.Security.Cryptography.Pkcs.ContentInfo contentInfo = new System.Security.Cryptography.Pkcs.ContentInfo(msg);

        //Instantiate SignedCms object with the ContentInfo above. 
        //Has default SubjectIdentifierType IssuerAndSerialNumber. 
        SignedCms signedCms = new SignedCms(contentInfo, detached);

        //Formulate a CmsSigner object for the signer. 
        CmsSigner cmsSigner = new CmsSigner(cert);  //First cert in the chain is the signer cert

        //Do the whole certificate chain. This way intermediate certificates get sent across as well.
        cmsSigner.IncludeOption = X509IncludeOption.ExcludeRoot;

        //Sign the CMS/PKCS #7 message. The second argument is needed to ask for the pin.
        signedCms.ComputeSignature(cmsSigner, true);

        //Encode the CMS/PKCS #7 message. 
        return signedCms.Encode();
    }
}