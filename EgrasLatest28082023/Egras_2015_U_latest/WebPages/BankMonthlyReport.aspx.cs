using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using System.IO;
using EgBL;
using System.Xml;

public partial class WebPages_BankMonthlyReport : System.Web.UI.Page
{
    EgBankSoftCopyUploadBL objBankSoftCopyBL;
    DataTable dt = null;
    string tempfile = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FileUpload1.Attributes.Add("onchange", "return checkFileExtension(this);");

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Write("<Script>alert('Session Expired')</Script>");
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (!FileUpload1.HasFile)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Upload File.')", true);
            }
            else
            {
                if (FileUpload1.HasFile && FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
                {
                    if (FileUpload1.PostedFile.ContentLength > 0 && FileUpload1.PostedFile.ContentType.Equals("text/xml") && Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Equals(".xml"))
                    {

                        objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
                        objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
                        string BankBSRcode = objBankSoftCopyBL.GetBSRCode();
                        objBankSoftCopyBL.BSRCode = BankBSRcode.ToString().Trim();
                        string signFlag = objBankSoftCopyBL.CheckSignFlag();


                        string filename;
                        filename = Path.GetFileName(FileUpload1.FileName);  //Get FileName
                        filename = Server.MapPath(filename);
                        string Challandate = string.Empty;

                        try
                        {
                            string[] Cdate = FileUpload1.FileName.Split('_');
                            Challandate = (Cdate[1].ToString().Substring(2, 4) + "/" + Cdate[1].ToString().Substring(0, 2) + "/" + "01");
                            if (Cdate.Length < 2)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Format is Not Valid')", true);
                                return;
                            }

                            if (Cdate[0] != BankIFSCName.GetBankName(BankBSRcode.Trim()))
                            {

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File  is not Proper FileName_MMYYYY Format')", true);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('File Format is Not Valid')", true);
                            return;
                        }
                        try
                        {
                            string filepathforSign = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            filepathforSign = Server.MapPath(filepathforSign);
                            FileInfo file2 = new FileInfo(filepathforSign);
                            FileUpload1.SaveAs(filepathforSign);

                            XElement xdocsign = XElement.Load(filepathforSign);
                            XNamespace ns = "http://www.w3.org/2000/09/xmldsig#";

                            IEnumerable<XElement> childListsign = from el in xdocsign.Elements(ns + "Signature")
                                                                  select el;

                            if (file2.Exists)
                            {
                                file2.Delete();
                            }
                            if (signFlag == "0" && childListsign.Count() > 0)
                            {
                                Message(" Please Get Signature File Upload Permission From Egras");
                                return;
                            }

                        }

                        catch (Exception ex)
                        {
                            Message("Issue in Uploaded File");
                            return;
                        }

                        if (signFlag == "1")
                        {

                            try
                            {
                                VerifySignature objVerifySignature = new VerifySignature();
                                XmlDocument xmlDoc = new XmlDocument();
                                string file_path = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                file_path = Server.MapPath(file_path);
                                FileInfo file1 = new FileInfo(file_path);
                                FileUpload1.SaveAs(file_path);
                                xmlDoc = LoadXMLDocNew(file_path);

                                //System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(System.Web.Configuration.WebConfigurationManager.AppSettings["SecureCertificate"] + BankBSRcode.ToString() + ".cer.txt");
                                bool result = true;//objVerifySignature.VerifyXmlFile(xmlDoc, cert);
                                if (!result)
                                {
                                    XmlDocument xmlDocument1 = new XmlDocument();
                                    xmlDocument1.PreserveWhitespace = true;
                                    xmlDocument1.Load(file_path);
                                    result = true;//objVerifySignature.VerifyXmlFile(xmlDocument1, cert);
                                    if (!result)
                                    {
                                        Message("Uploaded File is not Trusted!");
                                        return;
                                    }
                                }
                                if (file1.Exists)
                                {
                                    file1.Delete();
                                }
                            }
                            catch (Exception ex)
                            {
                                Message("File is not with  trusted Signature or Proper Format ");
                                return;
                            }
                        }

                        // Signature Check 4 Auguest 2021


                        FileInfo file = new FileInfo(filename);
                        FileUpload1.SaveAs(filename);  // save the file in folder 
                        string Msg = ReadFromFile(filename, Challandate);        // read the saved file from folder 
                        Message(Msg);


                        if (file.Exists)
                            file.Delete();
                        return;

                    }
                    else
                        Message("File is Invalid or not in .xml Formate");
                }
                else
                    Message("Plz select any .xml file");
            }
        }
    }

    private string ReadFromFile(string filename, string CDateTime)
    {
        createTable();
        string filepath = Path.GetFileName(FileUpload1.PostedFile.FileName);
        EgBankSoftCopyBL objEgBankSoftCopyBL = new EgBankSoftCopyBL();
        objEgBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
        string BankBSRcode = objEgBankSoftCopyBL.GetBSRCode();
        int resultXml = CheckRecord(filepath, BankBSRcode);

        if (resultXml == 0 || resultXml == -1)
        {
            // Message("XML File Not Uploaded");
            return "XML File Not Uploaded";


        }
        else
        {
            EgBankMonthlyReportBL objEgBankMonthlyReportBL = new EgBankMonthlyReportBL();
            int result = objEgBankMonthlyReportBL.CheckDMSexistScroll(dt);
            if (result == 2)
            {
                //Message("Scroll has been uploaded");
                //return "Scroll has been uploaded";
                return "Scroll already uploaded";
            }
            else
            {
                double sumObject;
                sumObject = Convert.ToDouble(dt.Compute("Sum(Amount)", ""));
                lblTotalRecord.Text = "Total Number Of Uploaded Record :- " + " " + dt.Rows.Count.ToString();
                lblsum.Text = "Total Amount:- " + " " + sumObject.ToString("0.00");
                objEgBankMonthlyReportBL.InsertDMSdata(dt);


                switch (BankBSRcode)
                {
                    case "0006326":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/SBI/" + filepath));
                        break;
                    case "0171051":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/SBBJ/" + filepath));
                        break;
                    case "0200113":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/BOB/" + filepath));
                        break;
                    case "0304017":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/PNB/" + filepath));
                        break;
                    case "0292861":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/UBI/" + filepath));
                        break;
                    case "0001234":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/HDFC/" + filepath));
                        break;
                    case "6910213":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/IDBI/" + filepath));
                        break;
                    case "0281065":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/CBI/" + filepath));
                        break;
                    case "0361193":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/OBC/" + filepath));
                        break;
                    case "1000132":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/Epay/" + filepath));
                        break;
                    case "0240539":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/CANARA/" + filepath));
                        break;
                    case "9910001":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/PayU/" + filepath));
                        break;
                    case "9920001":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/RTSB/" + filepath));
                        break;
                    case "0220123":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/BOI/" + filepath));
                        break;
                    case "6390013":
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/ICICI/" + filepath));
                        break;
                    case "6360010":  // 09/11/2022
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/AXIS/" + filepath));
                        break;
                    case "9940001": // 09/11/2022
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/HDFC/" + filepath));
                        break;
                    case "9970001": // 09/11/2022
                        FileUpload1.SaveAs(Server.MapPath("~/BankUploadInfo/EMTR/" + filepath));
                        break;
                }


                FsetMismatch.Visible = true;
                return "File Upload SuccessFully.";
            }
        }
    }

    public int CheckRecord(string filepath, string BSRcode)
    {
        EgBankSoftCopyBL objBankSoftCopyBL = new EgBankSoftCopyBL();
        XElement xdoc = XElement.Load(Server.MapPath(filepath));
        //IEnumerable<XElement> childList = from el in xdoc.Elements()
        //                                  select el;
        IEnumerable<XElement> childList = from el in xdoc.Elements("Detail")
                                          select el;
        int x = 0;
        bool chk;
        foreach (XElement e in childList)
        {
            int SubNodesCount = childList.ElementAt(x).Descendants().Count();
            if (SubNodesCount == 3)
            {

                try
                {
                    DataRow dr = dt.NewRow();

                    chk = objBankSoftCopyBL.IsNumeric(e.Element("Amount").Value.Trim(), 15, 'M');  // Amount Check
                    if (chk == false)
                    { Message("Invalid Amount in Scroll"); goto loc; }
                    else
                    {
                        dr["Amount"] = Convert.ToDouble(e.Element("Amount").Value);
                    }

                    chk = objBankSoftCopyBL.IsNumeric(e.Element("paiddate").Value.Trim(), 8, 'M');  // Amount Check
                    if (chk == false)
                    { Message("Invalid date in Scroll.DateFormat(yyyymmdd)"); goto loc; }
                    else
                    {
                        dr["PaidDate"] = Convert.ToDateTime(e.Element("paiddate").Value.Substring(0, 4) + "/" + e.Element("paiddate").Value.Substring(4, 2) + "/" + e.Element("paiddate").Value.Substring(6, 2));
                    }
                    if (BSRcode == e.Element("Bankcode").Value.Trim())  // BankBSRcode Check
                    {
                        dr["BankCode"] = Convert.ToString(e.Element("Bankcode").Value);
                    }
                    else
                    {
                        Message("Invalid BankCode in Scroll"); goto loc;
                    }
                    dt.Rows.Add(dr);  // add row in temp table    
                    x = 1;
                }
                catch (Exception ex)
                {
                    Message(ex.Message);
                    return 0;
                }
            }

            else
            {
                Message("Invalid XML"); goto loc;
            }

         }
           
          loc:
        return x;
    }
    private void createTable() // this is used for bulk upload
    {

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("BankCode", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Amount", Type.GetType("System.Double")));
        dt.Columns.Add(new DataColumn("PaidDate", Type.GetType("System.DateTime")));
    }

    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
        return;
    }

    public XmlDocument LoadXMLDocNew(string filepath)
    {
        XmlDocument xdoc;
        long lnum;
        try
        {
            xdoc = new XmlDocument();
            xdoc.Load(filepath);
        }
        catch (XmlException ex)
        {
            // Message(ex.Message);
            lnum = ex.LineNumber;
            ReplaceSpecialChars(lnum, filepath);
            xdoc = LoadXMLDocNew(filepath);
        }
        return (xdoc);
    }
    private void ReplaceSpecialChars(long linenumber, string filepath)
    {
        try
        {
            System.IO.StreamReader strm;
            //string strline;
            //string strreplace = " ";

            tempfile = Server.MapPath("Temp_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".xml"); //"C:\\Temp.xml";  filepath

            XmlDocument XD = new XmlDocument();
            XmlNode Root = XD.AppendChild(XD.CreateElement("Root"));
            XmlNode Child = Root.AppendChild(XD.CreateElement("Child"));
            XmlAttribute ChildAtt = Child.Attributes.Append(XD.CreateAttribute("Attribute"));
            ChildAtt.InnerText = "My innertext";


            Child.InnerText = "Node Innertext";
            XD.Save(tempfile);
            try
            {
                System.IO.File.Copy(filepath, tempfile, true);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

            StreamWriter strmwriter = new StreamWriter(filepath);
            strmwriter.AutoFlush = true;
            strm = new StreamReader(tempfile);
            strmwriter.WriteLine(strm.ReadToEnd().Replace("&", " and "));
            strm.Close();
            strm = null;

            strmwriter.Flush();
            strmwriter.Close();
            strmwriter = null;
        }
        catch (Exception ex)
        {
            Message(ex.Message);
        }
    }
}
