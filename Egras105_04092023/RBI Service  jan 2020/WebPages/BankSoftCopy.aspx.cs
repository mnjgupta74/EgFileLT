using System;
using System.Data;
using System.Web.UI;
using System.IO;
using EgBL;
using System.Xml;
using System.Xml.Linq;
using System.Web.Services;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class WebPages_BankSoftCopyUpload : System.Web.UI.Page
{
    string filename;
    string tempfile = "";
    int SubNodesCount;
    string Mode;
    EgBankSoftCopyUploadBL objBankSoftCopyBL;
    string fileType_byName;
    string ScrollType;
    DataColumn PaymentType;
    DataColumn BranchCode;
    DataColumn debitbank;
    DataColumn debitbankref;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FileUpload1.Attributes.Add("onchange", "return EnableDisableUploadButton();");
            btnProcessToNext.Visible = false;
            ViewState["BankBSRcode"] = null;
            ViewState["BankScrollType"] = null;
            ViewState["BankScrollDate"] = null;
            lblFileName.Visible = false;
            ajaxloader.Style.Add("display", "none");
        }
        iFrameDMSPDF.Attributes["src"] = "Admin/EgDMSScrollPDF.aspx";
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "try{ EnableDisableUploadButton(); }catch(err){}", true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
       
        //loadFile();
        lblFileName.Visible = false;
        ViewState["BankBSRcode"] = null;
        ViewState["BankScrollType"] = null;
        ViewState["BankScrollDate"] = null;
        if (FileUpload1.HasFile && FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        {
            if (FileUpload1.PostedFile.ContentLength > 0 && FileUpload1.PostedFile.ContentType.Equals("text/xml") && Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower().Equals(".xml"))
            {


                // checked  Signature   13  May 2020	
                //0->Run  without signature check
                //1->Run with  signature check
               

                objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
                objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
                string BankBSRcode = objBankSoftCopyBL.GetBSRCode();
                objBankSoftCopyBL.BSRCode = BankBSRcode.ToString().Trim();
                string signFlag = objBankSoftCopyBL.CheckSignFlag();

                // Check File Has Signature Tag or Not  11/08/2020
               
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

                catch(Exception ex)
                {
                    Message("Issue in Uploaded File");
                    return;
                }

                if (signFlag == "1" )
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

                        System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(Server.MapPath(@"~\Certificate\" + BankBSRcode.ToString() + ".cer.txt"));
                         bool result = objVerifySignature.VerifyXmlFile(xmlDoc, cert);
                        if (!result)
                        {
                            XmlDocument xmlDocument1 = new XmlDocument();
                            xmlDocument1.PreserveWhitespace = true;
                            xmlDocument1.Load(file_path);
                            result = objVerifySignature.VerifyXmlFile(xmlDocument1, cert);
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
                // End Check Singature 
                string[] Cdate = FileUpload1.FileName.Split('_');
                if (Cdate.Count() == 3)
                {
                    fileType_byName = "Manual";
                }
                else
                {
                    if (Cdate[0].ToString().ToLower().Trim() == "epay")
                    {
                        fileType_byName = "EPAY";
                    }
                    else if (Cdate[0].ToString().ToLower().Trim() == "pnbg")
                    {
                        fileType_byName = "EPAYU";
                    }
                    else if (Cdate[0].ToString().ToLower().Trim() == "rtsb")
                    {
                        fileType_byName = "ERTSB";
                    }
                    else if (Cdate[0].ToString().ToLower().Trim() == "bldk")
                    {
                        fileType_byName = "EBLDK";
                    }
                    else
                    {
                        fileType_byName = "Online";
                    }
                }

                //string[] Cdate = hdnFileName.Value.Split('_');
                // Check Scroll Existence sandeep singh
                filename = Path.GetFileName(FileUpload1.FileName);  //Get FileName
                filename = Server.MapPath(filename);
                string Challandate = (Cdate[1].ToString().Substring(4, 4) + "/" + Cdate[1].ToString().Substring(2, 2) + "/" + Cdate[1].ToString().Substring(0, 2));
                #region Check If Scroll Already Exist in BankUploadInfo Table in Database
                //objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
                objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
                //string BankBSRcode = objBankSoftCopyBL.GetBSRCode();
                //objBankSoftCopyBL.BSRCode = BankBSRcode.ToString().Trim();
                objBankSoftCopyBL.Date = Convert.ToDateTime(Challandate);
                objBankSoftCopyBL.PaymentType = fileType_byName.Substring(0, 1).Trim();
                string result1 = objBankSoftCopyBL.CheckExistScroll();

                if (result1 == "2") // 2 -> 0 changed by sandeep 04-03-2018
                {
                    Message("Scroll Already Exists.!");
                    return;
                }
                else if (Convert.ToInt32(result1) > 1)
                {
                    Message("Previous Date Scroll Missing.!");
                    return;
                }
                #endregion
                else
                {
                    FsetMismatch.Visible = false;
                    grdVerify.DataSource = null;
                    grdVerify.DataBind();

                    lblFileName.Text = "Uploaded File: " + FileUpload1.PostedFile.FileName;
                    lblFileName.Visible = true;
                    FileInfo file = new FileInfo(filename);
                    FileUpload1.SaveAs(filename);  // save the file in folder 

                    #region xml and validate items

                    //For Loading XML Document and Replacing special Characters
                    XmlDocument xmlDocument = new XmlDocument();
                    string filepath = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    xmlDocument = LoadXMLDocNew(Server.MapPath(filepath));
                    if (tempfile != "")
                    {
                        string path = tempfile;
                        FileInfo myfileinf = new FileInfo(path);
                        myfileinf.Delete();
                    }
                    //end


                    //Setting BSRcode and XML nodes
                    XElement xdoc = XElement.Load(Server.MapPath(filepath));
                    IEnumerable<XElement> childList = from el in xdoc.Elements("Detail")
                                                      select el;


                    // Read and Validate XML Items using class
                    BankScrollValidations objBankScrollValidations = new BankScrollValidations();
                    SubNodesCount = childList.ElementAt(0).Descendants().Count();


                    //for Identifying Manual or E-Pay in case of 10 Nodes
                    if (SubNodesCount == 10 || SubNodesCount == 11)
                    {
                        if (childList.ElementAt(0).Descendants().ElementAt(8).Name == "PaymentType")
                        {
                            Mode = "M";
                        }
                        else
                        {
                            Mode = "E";
                        }
                    }
                    else // for 8 nodes in online
                    {
                        Mode = "O";
                    }
                    if (fileType_byName.Substring(0, 1) != Mode)// ADDED By SANDEEP on 18/11/2016
                    {
                        Message("Invalid Scroll");
                        return;
                    }


                    int ValidStatus = objBankScrollValidations.CheckScrollValidation(childList, BankBSRcode, SubNodesCount, Mode); // x=0 for all Valid entries and x=1 for Wrong entries
                    //int ValidStatus = 0;                                                                                                           //end

                    // return in case of Wrong entries
                    if (ValidStatus == 1)
                    {
                        return;
                    }
                    //end

                    #endregion

                    #region Assign XML into DataTable
                    DataSet ds = new DataSet();
                    ds.ReadXml(filename);
                    DataTable dtxml = new DataTable();
                    dtxml = ds.Tables[0];

                    if (SubNodesCount == 9)
                    {
                        dtxml.Columns.Remove("Head");
                        dtxml.AcceptChanges();
                        SubNodesCount = 8;
                    }
                    else if (SubNodesCount == 11)
                    {
                        dtxml.Columns.Remove("Head");
                        dtxml.AcceptChanges();
                        SubNodesCount = 10;

                    }

                    switch (SubNodesCount)
                    {
                        case 8:
                            ScrollType = "O";
                            PaymentType = new DataColumn("PaymentType", Type.GetType("System.String"));
                            PaymentType.AllowDBNull = true;
                            dtxml.Columns.Add(PaymentType);
                            BranchCode = new DataColumn("BranchCode", Type.GetType("System.String"));
                            BranchCode.AllowDBNull = true;
                            dtxml.Columns.Add(BranchCode);
                            debitbank = new DataColumn("debitbank", Type.GetType("System.String"));
                            debitbank.AllowDBNull = true;
                            dtxml.Columns.Add(debitbank);
                            debitbankref = new DataColumn("debitbankref", Type.GetType("System.String"));
                            debitbankref.AllowDBNull = true;
                            dtxml.Columns.Add(debitbankref);
                            if (dtxml.Columns.Count != 12)
                            {
                                Message("XmlIs Not Proper describe Format");
                                return;
                            }

                            break;
                        case 10:
                            if (Mode == "M")
                            {
                                ScrollType = "M";
                                debitbank = new DataColumn("debitbank", Type.GetType("System.String"));
                                debitbank.AllowDBNull = true;
                                dtxml.Columns.Add(debitbank);
                                debitbankref = new DataColumn("debitbankref", Type.GetType("System.String"));
                                debitbankref.AllowDBNull = true;
                                dtxml.Columns.Add(debitbankref);
                            }

                            if (Mode == "E")
                            {
                                ScrollType = "E";
                                PaymentType = new DataColumn("PaymentType", Type.GetType("System.String"));
                                PaymentType.AllowDBNull = true;
                                dtxml.Columns.Add(PaymentType);
                                PaymentType.SetOrdinal(8); // for setting Payment Type at specific position to match TVP order of columns
                                BranchCode = new DataColumn("BranchCode", Type.GetType("System.String"));
                                BranchCode.AllowDBNull = true;
                                dtxml.Columns.Add(BranchCode);
                                BranchCode.SetOrdinal(9);// for setting BranchCode at specific position to match TVP order of columns
                            }
                            break;
                        default:
                            break;
                    }

                    DataColumn BankChallanDate = new DataColumn("BankChallanDate", Type.GetType("System.DateTime"));
                    BankChallanDate.DefaultValue = Challandate;
                    dtxml.Columns.Add(BankChallanDate);

                    dtxml.AcceptChanges();

                    #endregion

                    #region Checking Duplicacy and Sending TVP into database

                   // GenralFunction BLobj = new GenralFunction();
                   // System.Data.SqlClient.SqlTransaction Trans = BLobj.Begintrans();

                    try
                    {

                        #region insert Scroll

                        var dtXmlAmount = (from row in dtxml.AsEnumerable()
                                           select row).Sum(row => Convert.ToDouble(row.Field<string>("Amount")));

                        double sumObject;
                        sumObject = Convert.ToDouble(dtXmlAmount);
                        objBankSoftCopyBL.ScrollType = ScrollType;
                        objBankSoftCopyBL.PaymentType = Mode;
                        int iReturn = objBankSoftCopyBL.PreUpdateAndInsertBankStatus(dtxml);
                        if (iReturn > 0)
                        {
                            lblTotalRecord.Text = "Total Number Of Record :- " + " " + dtxml.Rows.Count.ToString();
                            lblsum.Text = "Total Amount:- " + " " + sumObject.ToString("0.00");

                            #endregion

                            #region Save File in Bank Folder


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
                            }

                            #endregion

                            #region Check for Mismatch
                            DataTable dtMismatch;
                            objBankSoftCopyBL.BSRCode = BankBSRcode.ToString();
                            objBankSoftCopyBL.Date = Convert.ToDateTime(Challandate);
                            if (Mode == "M") // added by sandeep on 18/11/2016
                                dtMismatch = objBankSoftCopyBL.GetBankScrollFromPreBankUploadInfo_Manual();
                            else
                                dtMismatch = objBankSoftCopyBL.GetBankScrollFromPreBankUploadInfo();
                            if (dtMismatch != null)
                            {
                                grdVerify.DataSource = dtMismatch;
                                grdVerify.DataBind();

                                if (dtMismatch.Rows.Count > 0)
                                {
                                    FsetMismatch.Visible = true;
                                    btnProcessToNext.Visible = false;
                                    trSave.Visible = true;
                                }
                                else
                                {
                                    ViewState["BankScrollType"] = ScrollType;
                                    ViewState["BankBSRcode"] = BankBSRcode;
                                    ViewState["BankScrollDate"] = Convert.ToDateTime(Challandate);
                                    ViewState["PaymentType"] = Mode;
                                    btnProcessToNext.Visible = true;
                                    trSave.Visible = false;
                                }
                                FsetMismatch.Visible = true;
                            }
                            #endregion
                           // BLobj.Endtrans();
                        }
                        else
                        {
                            Message("Something went wrong when a file uploading, please come again on this page letter or try again.");
                           // BLobj.Endtrans();
                        }
                    }
                    catch (Exception ex)
                    {
                        Message(ex.Message);
                        //BLobj.Endtrans();

                    }

                    finally
                    {
                    //    BLobj.Endtrans();


                    }

                    //}

                    #endregion

                    if (file.Exists)
                    {
                        file.Delete();
                    }

                }
            }
            else
            {
                Message("File is Invalid or not in .xml Formate");
            }

        }
        else
        {
            Message("Plz select any .xml file");
        }

        FileUpload1.Enabled = true;
        ajaxloader.Style.Add("display", "none");
    }
    protected void btnProcessToNext_Click(object sender, EventArgs e)
    {
        if (objBankSoftCopyBL == null)
        {
            objBankSoftCopyBL = new EgBankSoftCopyUploadBL();
        }
        objBankSoftCopyBL.UserId = Convert.ToInt32(Session["UserId"]);
        objBankSoftCopyBL.BSRCode = Convert.ToString(ViewState["BankBSRcode"]);
        objBankSoftCopyBL.ScrollType = Convert.ToString(ViewState["BankScrollType"]);
        objBankSoftCopyBL.Date = Convert.ToDateTime(ViewState["BankScrollDate"]);
        objBankSoftCopyBL.PaymentType = Convert.ToString(ViewState["PaymentType"]);
       // GenralFunction BLobj = new GenralFunction();
        //System.Data.SqlClient.SqlTransaction Trans = BLobj.Begintrans();
        try
        {


            string res = objBankSoftCopyBL.FinalUpdateAndInsertBankStatus();    // Change By priyanka  4 April 2019
          //  BLobj.Endtrans();
            if (res == "1")
            {
                trSave.Visible = true;
                FsetMismatch.Visible = false;
                btnProcessToNext.Visible = false;
                Message("File Upload SuccessFully.");
            }
            else
            {
                Message("File Already Exist !");
            }

            //objBankSoftCopyBL.FinalUpdateAndInsertBankStatus(Trans);
            //BLobj.Endtrans();
            //trSave.Visible = true;
            //FsetMismatch.Visible = false;
            //btnProcessToNext.Visible = false;
            //Message("File Upload SuccessFully.");
        }
        catch (Exception ex)
        {
            Message(ex.Message);
           // Trans.Rollback();
        }
    }
    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
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
    protected void grdVerify_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTreasuryDate = (Label)e.Row.FindControl("lblTreasuryDate");
            Label lblBankDate = (Label)e.Row.FindControl("lblBankDate");

            DateTime BankScrollDate, BankScrollFromDate, BankScrollToDate;
            BankScrollDate = Convert.ToDateTime(objBankSoftCopyBL.Date.ToString("MM/dd/yyyy"));
            BankScrollFromDate = Convert.ToDateTime(BankScrollDate.AddDays(-1).ToString("MM/dd/yyyy") + " 20:00:00");
            BankScrollToDate = Convert.ToDateTime(BankScrollDate.ToString("MM/dd/yyyy") + " 20:00:00");

            Decimal decTreasuryAmount = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TreasuryAmount")).Trim()) ? 0 : Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TreasuryAmount").ToString());
            Decimal decBankAmount = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankAmount")).Trim()) ? 0 : Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BankAmount").ToString());

            DateTime? BankChallanDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankDate")).Trim()) ? (DateTime?)null : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "BankDate"));
            DateTime? ETreasuryDate = string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TreasuryDate")).Trim()) ? (DateTime?)null : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "TreasuryDate"));

            lblTreasuryDate.Text = ETreasuryDate == (DateTime?)null ? "" : Convert.ToDateTime(ETreasuryDate).ToString("dd/MM/yyyy hh:mm:ss");
            lblBankDate.Text = BankChallanDate == (DateTime?)null ? "" : Convert.ToDateTime(BankChallanDate).ToString("dd/MM/yyyy hh:mm:ss");

            if (decTreasuryAmount != decBankAmount)
            {
                e.Row.Cells[1].ForeColor = Color.Green;
                e.Row.Cells[2].ForeColor = Color.Maroon;
            }

            if ((BankChallanDate < BankScrollFromDate || BankChallanDate > BankScrollToDate) || (ETreasuryDate < BankScrollFromDate || ETreasuryDate > BankScrollToDate))
            {
                e.Row.Cells[3].ForeColor = Color.Green;
                e.Row.Cells[4].ForeColor = Color.Maroon;
            }
        }
    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["_ViewState"];
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        Session["_ViewState"] = viewState;
    }
    /// <summary>
    /// get file name 
    /// </summary>
    /// <param name="Cdate"></param>
    private void GetFileName(string[] Cdate)
    {
        if (Cdate.Count() == 3)
        {
            fileType_byName = "Manual";
        }
        else
        {
            if (Cdate[0].ToString().ToLower().Trim() == "epay")
            {
                fileType_byName = "EPAY";
            }
            else if (Cdate[0].ToString().ToLower().Trim() == "pnbg")
            {
                fileType_byName = "EPAYU";
            }
            else if (Cdate[0].ToString().ToLower().Trim() == "rtsb")
            {
                fileType_byName = "ERTSB";
            }
            else
            {
                fileType_byName = "Online";
            }
        }
    }
    // Dms Pdf File Upload 21Jan 2021
    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblType.SelectedValue == "1")
        {
            trXML.Visible = true;
            divDMSPDF.Visible = false;
        }
        else
        {
            trXML.Visible = false;
            divDMSPDF.Visible = true;
        }
    }

}
