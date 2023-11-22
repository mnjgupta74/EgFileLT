//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.security;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EgBL
{
    public class EgDmsCheckBL
    {
        EgDTALetter objEgDTALetter = new EgDTALetter();
        EgBankSoftCopyUploadBL objBankSoftCopyBL;
        int res;
        /// <summary>
        /// check SignPdf 2 sept 2021
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FileName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public int SignAndUnSignPdf(string path, string FileName, int year, short month, string bankcode)
        {
            objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
            objBankSoftCopyBL.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            string BankBSRcode = objBankSoftCopyBL.GetBSRCode();
            objBankSoftCopyBL.BSRCode = BankBSRcode.ToString().Trim();
            string signFlag = objBankSoftCopyBL.CheckSignFlag();
            if (signFlag == "1")
            {
                PdfReader reader = new PdfReader(path);
                AcroFields af = reader.AcroFields;
                var names = af.GetSignatureNames();

                if (names.Count == 0)
                {
                    reader.Close();
                    throw new InvalidOperationException("No Signature present in pdf file.");

                }

                foreach (string name in names)
                {
                    if (!af.SignatureCoversWholeDocument(name))
                    {
                        reader.Close();
                        throw new InvalidOperationException(string.Format("The signature: {0} does not covers the whole document.", name));
                    }
                    System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(HttpContext.Current.Server.MapPath(@"~\Certificate\" + bankcode + ".cer.txt"));
                   
                    string[] clientCertificateSign = cert.Subject.ToString().Split(',');
                    PdfPKCS7 pk = af.VerifySignature(name);
                    var cal = pk.SignDate;
                    var pkc = pk.SigningCertificate;
                    string[] PdfSign = pkc.SubjectDN.ToString().Replace('{', ' ').Replace('}', ' ').Split(',');
                    cal = pk.SignDate;
                    if (!pk.Verify())
                    {
                        reader.Close();
                        throw new InvalidOperationException("The signature could not be verified.");
                    }
                    bool retrun = VerifySign(clientCertificateSign, PdfSign, pkc.NotAfter, cert.NotAfter);
                    if (!retrun)
                    {
                        reader.Close();
                        throw new InvalidOperationException("Pdf File Signature could not be verified by Trusted Certificate");
                    }

                }

                reader.Close();

            }
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            objEgDTALetter.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            objEgDTALetter.LetterName = FileName;
            objEgDTALetter.FileYear = year;
            objEgDTALetter.FileMonth = month;
            objEgDTALetter.BsrCode = bankcode;
            objEgDTALetter.eSignedBytes = bytes;

            return res = objEgDTALetter.InsertDMSPDF();
        }
        private bool VerifySign(string[] clientCertificate, string[] pdfSign, DateTime pdfCertValidity, DateTime clientCertValidity)
        {
            bool flag = false;
            if (pdfCertValidity.ToString("yyyy-MM-dd") != clientCertValidity.ToString("yyyy-MM-dd"))
                return false;
            // CertificateSign obj = new CertificateSign();
            //  obj.CN= clientCertificate["CN"]
            //var jsoncertificate = JsonConvert.SerializeObject(clientCertificate);
            //var organizations = clientCertificate.toli<CertificateSign>();
            //  List<CertificateSign> list = clientCertificate.ToList<CertificateSign>();
            //  var jsonPdf = JsonConvert.SerializeObject(pdfSign);
            //  List<string> clientCerList = new List<string>();
            //List<CertificateSign> clientCerList = clientCertificate.ToList<CertificateSign>();
            // CertificateSign objcert = JsonConvert.DeserializeObject<CertificateSign>(jsoncertificate);
            //  PdfSign  objPdf = JsonConvert.DeserializeObject<PdfSign>(jsonPdf);

            string[] arrclientCertificate;
           // List<string> PdfCerList = new List<string>();
            string[] arrPdfCerList;
            for (int i = 0; i < clientCertificate.Length; i++)
            {
                arrclientCertificate = clientCertificate[i].Split('=');

              //  clientCerList.Add(arrclientCertificate[0]);
               // clientCerList.Add(arrclientCertificate[1]);
                if ( (arrclientCertificate[0] == "CN" || arrclientCertificate[0].ToLower().Trim() == "postalcode" || arrclientCertificate[0].ToLower().Trim() == "serialnumber"))
                {
                    for (int j = 0; j < pdfSign.Length; j++)
                    {
                        arrPdfCerList = pdfSign[j].Split('=');
                        //if (arrPdfCerList[0] == "CN")
                        if (arrPdfCerList[0].ToLower().Trim() == arrclientCertificate[0].ToLower().Trim())
                        {
                            if (arrclientCertificate[1].ToLower().Trim() == arrPdfCerList[1].ToLower().Trim())
                            {
                                flag = true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        }
                      
                        //  PdfCerList.Add(arrPdfCerList[0]);
                        //  PdfCerList.Add(arrPdfCerList[1]);
                    }
                }
            }
            // Pdf Sign List 14 Auguest 2021



            //for (int i = 0; i < pdfSign.Length; i++)
            //{
            //    arrPdfCerList = pdfSign[i].Split('=');

            //    PdfCerList.Add(arrPdfCerList[0]);
            //    PdfCerList.Add(arrPdfCerList[1]);
            //}

            //Comparision  Both Certtificates

            //if (pdfCertValidity.ToString("yyyy-MM-dd") == clientCertValidity.ToString("yyyy-MM-dd") &&
            //    clientCerList[1] == PdfCerList[13] &&
            //    clientCerList[3] == PdfCerList[11] &&
            //    clientCerList[5] == PdfCerList[9] &&
            //    clientCerList[7] == PdfCerList[7] &&
            //    clientCerList[11] == PdfCerList[3] &&
            //    clientCerList[13] == PdfCerList[1]
            //    )
            //    return true;
            //else
            //    return false;
            return flag;
        }

        private void existFile(string filepath)
        {

            string UploadFile = HttpContext.Current.Server.MapPath("~/DMSScroll/" + filepath);
            FileInfo file1 = new FileInfo(UploadFile);
            if (file1.Exists)//check file exsit or not  
            {
                file1.Delete();

            }
        }

        //public class PdfSign
        //{
        //    public string CN { get; set; }
        //    public string SERIALNUMBER { get; set; }
        //    public string Phone { get; set; }
        //    public string S { get; set; }
        //    public string PostalCode { get; set; }
        //    public string OU { get; set; }
        //    public string O { get; set; }
        //    public string C { get; set; }

        //}

        //public class CertificateSign
        //{
        //    public string CN { get; set; }
        //    public string SERIALNUMBER { get; set; }
        //    public string Phone { get; set; }
        //    public string S { get; set; }
        //    public string PostalCode { get; set; }
        //    public string OU { get; set; }
        //    public string O { get; set; }
        //    public string C { get; set; }

        //}
    }
}
