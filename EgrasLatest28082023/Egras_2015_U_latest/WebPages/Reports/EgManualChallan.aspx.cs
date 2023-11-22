using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using EgBL;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Newtonsoft.Json;

public partial class WebPages_EgManualChallan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "" || Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                var objEgEChallan = new EgEChallanBL()
                {
                    GRNNumber = Convert.ToInt64(Session["GrnNumber"]),
                    FormId = Session["UserId"].ToString() == "73" || Session["UserType"].ToString() == "4" ? "2" : "1"
                };
                int valueGRN = objEgEChallan.CheckExistGrnExtraDetails();
                if (valueGRN == 1)
                {
                    lnkExtraDetails.Visible = true;
                }
                var result = objEgEChallan.ManualChallanView();
                var dt1 = objEgEChallan.EChallanViewSubRptPDF();
                lblDepartment.Text = dt1.Rows[0]["DeptNameEnglish"].ToString();

                if (result == 1)
                {
                    if (objEgEChallan.BankCode.Trim() == "9970001")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Commission Amount not allowed more than 20% of Total Amount!    3');", true);
                        //return;
                        double amount = Convert.ToDouble(objEgEChallan.TotalAmount);
                        Int64 charges = 0;
                        if (amount > 0 && amount <= 2000)
                        {
                            charges = 10;
                        }
                        if (amount > 2000)
                        {
                            double a = (amount-2000) / 1000;
                            double b = Math.Ceiling(a);
                            charges = 10 + Convert.ToInt64(b * 2);
                        }
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "addScript", " myAlert('Additional Charge For Emitra','  Rs " + charges + "')", true);

                    }


                    try
                    {
                        new Thread(new ThreadStart(delegate
                        {
                            CallManualBankService(objEgEChallan.GRNNumber);
                        })).Start();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    lblGrn.Text = Convert.ToString(objEgEChallan.GRNNumber);
                    if (objEgEChallan.PdaccountNumber.Trim() != "0".Trim())
                    {
                        lblPdacc.Visible = true;
                        trPdacc.Visible = true;
                        lblPdacc.Text = objEgEChallan.PdaccountNumber.Split('|')[0] == "0" ? "Div Code-" + objEgEChallan.PdaccountNumber.Split('|')[1] : "PDACC NO - " + objEgEChallan.PdaccountNumber.Split('|')[1];
                    }
                    else
                    {
                        lblPdacc.Visible = false;
                        trPdacc.Visible = false;
                    }
                    if (Convert.ToInt16(dt1.Rows[0]["SchemaName"].ToString().Substring(0, 4)) >= 2000 &&
                        Convert.ToInt16(dt1.Rows[0]["SchemaName"].ToString().Substring(0, 4)) < 7000 && Session["UserType"].ToString() == "4")
                    {
                        lblDdoname.Visible = true;
                        lblDdo.Visible = true;
                        trDdo.Visible = true;
                        lblDdoname.Text = objEgEChallan.DdoName;
                    }
                    else
                    {
                        trDdo.Visible = false;
                        lblDdoname.Visible = false;
                        lblDdo.Visible = false;
                    }
                    if (dt1.Rows.Count > 0)
                    {
                        var sbSchema = new StringBuilder();
                        var sbSchemaAmount = new StringBuilder();
                        for (var i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (Convert.ToInt16(dt1.Rows[i]["SchemaName"].ToString().Substring(0, 4)) >= 2000 &&
                                Convert.ToInt16(dt1.Rows[i]["SchemaName"].ToString().Substring(0, 4)) <= 6000)
                            {
                                sbSchema.Append(dt1.Rows[i]["SchemaName"] + "</br> " + dt1.Rows[i]["objvotedplan"]);
                                sbSchemaAmount.Append(Convert.ToInt64(dt1.Rows[i]["Amount"]).ToString("0.00") + "</br> ");
                            }
                            else
                            {
                                sbSchema.Append(dt1.Rows[i]["SchemaName"] + "</br>");
                                sbSchemaAmount.Append(Convert.ToInt64(dt1.Rows[i]["Amount"]).ToString("0.00") + "</br> ");
                            }
                        }

                        lblMajorHead.Text = dt1.Rows[0]["SchemaName"].ToString().Substring(0, 4);
                        lblSchema.Text = sbSchema.ToString();
                        lblSchemaAmount.Text = sbSchemaAmount.ToString();
                        if (Convert.ToInt16(dt1.Rows[0]["SchemaName"].ToString().Substring(0, 4)) == 0040 ||
                            Convert.ToInt16(dt1.Rows[0]["SchemaName"].ToString().Substring(0, 4)) == 0045)
                        {
                            lblF1.Visible = true;
                            lblV3.Visible = true;
                            trFv.Visible = true;
                            lblF1.Text = objEgEChallan.F1 + "/" + objEgEChallan.F2 + "/" + objEgEChallan.F3 + ":-";
                            lblV3.Text = objEgEChallan.V3;
                        }
                    }
                    var challandate = Convert.ToDateTime(objEgEChallan.ChallanDate);

                    int CurrentYear =Convert.ToDateTime(objEgEChallan.ChallanDate).Year;
                    int PreviousYear = Convert.ToDateTime(objEgEChallan.ChallanDate).Year - 1;
                    int NextYear = Convert.ToDateTime(objEgEChallan.ChallanDate).Year + 1;
                    string PreYear = PreviousYear.ToString();
                    string NexYear = NextYear.ToString();
                    string CurYear = CurrentYear.ToString();
                    string FinYear = null;

                    if (Convert.ToDateTime(objEgEChallan.ChallanDate).Month > 3)
                        FinYear = "31" +"/" +"3" + "/" + NexYear;
                    else
                        FinYear = "31" + "/" + "3" + "/" + CurYear;
                    //  var validUpToDate = FinYear;
                    lblChallanDateValidUpto.Text = FinYear;//Convert.ToDateTime(FinYear).ToString("dd/MM/yyyy");
                    lblChallanDate.Text = Convert.ToDateTime(objEgEChallan.ChallanDate).ToString("dd/MM/yyyy HH:MM:ss tt");
                    lblOfficeName.Text = objEgEChallan.Office;
                    if (objEgEChallan.Office == "Multiple Offices") { lnkMultipleOfcs.Visible = true; }// Show For MultipleOffice Challan
                    lbltin.Text = objEgEChallan.Identity;
                    lblPan.Text = objEgEChallan.PanNumber;
                    lblRemitter.Text = objEgEChallan.FullName;
                    lblFromYear.Text = Convert.ToString(objEgEChallan.ChallanFromMonth.ToString("dd/MM/yyyy"));
                    lblToYear.Text = Convert.ToString(objEgEChallan.ChallanToMonth.ToString("dd/MM/yyyy"));
                    lblAddress.Text = objEgEChallan.Address;
                    lblDiscount.Text = objEgEChallan.DeductCommission.ToString("0.00");
                    lblTotalAmount.Text = objEgEChallan.TotalAmount;
                    lblLocation.Text = objEgEChallan.TreasuryName;
                    lblOfficeName.Text = objEgEChallan.Office;
                    lblRemarks.Text = objEgEChallan.Remark;
                    lblBank.Text = objEgEChallan.BankName;
                    lblChequeNo.Text = objEgEChallan.ChequeDDNo;
                    // hdnBankCode.Value = objEgEChallan.BankCode;
                    //Bitmap barcode = CreateBarCode(Convert.ToString(objEgEChallan.GRNNumber));
                    //barcode.Save(Server.MapPath("~/Image/barcode.Gif"), ImageFormat.Gif);
                    ////Image2.ImageUrl = "~/Image/barcode.Gif";
                    //barcode.Dispose();
                    if (objEgEChallan.BankName.ToUpper().Trim() == "NEFT-RTGS".ToUpper().Trim())
                    {
                        btnshow.Visible = true;
                        imageButtonPdf.Visible = false;
                    }
                    else
                    {
                        imageButtonPdf.Visible = true;
                        btnshow.Visible = false;
                    }
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "addScript", " checkamounttotal('" + lblTotalAmount.ID + "')", true);

            }
            catch (Exception ex)
            {
                //Browserinfo objbrowseringo = new Browserinfo();
                //string msg = ex.StackTrace + objbrowseringo.Browserinformaion();
                if (Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.StackTrace);
                }
                else
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("GRN=" + Session["GrnNumber"].ToString() + "|" + ex.StackTrace);
                }
            }
        }
    }
    //protected Bitmap CreateBarCode(string data)
    //{
    //    string code = data;

    //    // Multiply the lenght of the code by 25 (just to have enough width)
    //    int w = code.Length * 25;

    //    // Create a bitmap object of the width that we calculated and height of 30
    //    Bitmap oBitmap = new Bitmap(w, 30);
    //    Graphics oGraphics = Graphics.FromImage(oBitmap);

    //    PrivateFontCollection fnt = new PrivateFontCollection();
    //    fnt.AddFontFile(Server.MapPath("../../WebPages/font/IDAutomationHC39M.ttf"));
    //    FontFamily fntfam = new FontFamily("IDAutomationHC39M", fnt);
    //    Font oFont = new Font(fntfam, 25);

    //    PointF oPoint = new PointF(2f, 2f);
    //    SolidBrush oBrushWrite = new SolidBrush(Color.Black);
    //    SolidBrush oBrush = new SolidBrush(Color.White);

    //    oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

    //    oGraphics.DrawString("*" + code + "*", oFont, oBrushWrite, oPoint);
    //    oGraphics.Dispose();
    //    return oBitmap;
    //}

    public void LoadPdf()
    {
        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("GRN", Session["GrnNumber"].ToString());

        param[1] = new ReportParameter("DeptName", lblDepartment.Text);
        param[2] = new ReportParameter("MajorHead", lblMajorHead.Text);

        if (Session["UserId"].ToString() == "73" || Session["UserType"].ToString() == "4")
        {
            param[3] = new ReportParameter("Mode", "2");
        }
        else
        {
            param[3] = new ReportParameter("Mode", "1");
        }

        var b = lblBank.Text.ToUpper().Contains("Any".ToUpper());

        var reportName = b ? "EgEChallanViewRptAnyWhere" : "EgEChallanViewRpt";
        var objssrs = new SSRS();
        objssrs.LoadSSRS(SSRSreport, reportName, param);
        //output as PDF
        var format = "PDF";
        var deviceinfo = "";
        string mimeType;
        string encoding;
        string extension;
        string[] streams;
        Warning[] warnings;

        byte[] returnValue = SSRSreport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension,
            out streams, out warnings);
        if (Response.IsClientConnected)//An error occurred while communicating with the remote host. The error code is 0x80070057.
        {
            Response.Buffer = true;
            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attachment; filename=EgEChallanViewRpt.pdf");

            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
    }


    protected void imageButtonPdf_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        LoadPdf();
    }
    private void CallManualBankService(Int64 grn)
    {
        string BankCode = "";
        try
        {
            var objEgManualBankServiceBl = new EgManualBankServiceBL { GRNNumber = grn };
            objEgManualBankServiceBl.BankCode = objEgManualBankServiceBl.GetGRNBsrCode();
            BankCode = objEgManualBankServiceBl.BankCode;
            var challanDetails = objEgManualBankServiceBl.GetGrnManualDetails();
            if (objEgManualBankServiceBl.GetGRNBsrCode().Substring(0, 3) == "030")
            {
                var objEncry = new SbiEncryptionDecryption();
                PNBManualWebServ.ReceiveData objpnbPush = new PNBManualWebServ.ReceiveData();
                var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "PNB.key");
                objpnbPush.UploadData(cipherText);// PUSH Service PNB Manual Challan anywhere
            }
            if (objEgManualBankServiceBl.GetGRNBsrCode().Substring(0, 3) == "000")
            {
                var objEncry = new SbiEncryptionDecryption();
                SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                // SBIManualServ.RAJASTHANWS objsbiPush = new SBIManualServ.RAJASTHANWS();

                var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS_Manual.key");
                //objsbiPush.challanposting(cipherText);// PUSH Service SBI Manual Challan anywhere
                //cipherText = objEncry.Encrypt(ChallanDetails, Server.MapPath("../Key/RAJASTHAN_EGRASS.key"));

                Ojpost.ProcessRajasthanData(cipherText);

            }
            if (objEgManualBankServiceBl.GetGRNBsrCode().Substring(0, 3) == "028")
            {
                var objEncry = new SbiEncryptionDecryption();
                CBIPushServ.EGRASS obj = new CBIPushServ.EGRASS();
                //var objEncryption = new EncryptDecryptionBL();
                //string checkSum = objEncryption.GetMD5Hash(ChallanDetails);

                var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "CBI.key");
                obj.EGRASSFETCHDETAILS(cipherText);
                // objsbiPush.challanposting(cipherText);// PUSH Service CBI Manual Challan anywhere
            }
            // Add BoB manual Challan Posting 12 Jan 2021 

            if (objEgManualBankServiceBl.GetGRNBsrCode().Substring(0, 3) == "020")
            {
                var objEncry = new SbiEncryptionDecryption();
                //  SBIManualChallanServ.IserSTGTV2Receipt_INBClient Ojpost = new SBIManualChallanServ.IserSTGTV2Receipt_INBClient();
                //SBIManualServ.RAJASTHANWS objsbiPush = new SBIManualServ.RAJASTHANWS();
                EncryptDecryptionBL objEncryption = new EncryptDecryptionBL();
                // string checkSum = objEncryption.GetMD5Hash(challanDetails);

                // challanDetails = challanDetails + "|checkSum=" + checkSum;
                var cipherText = objEncry.EncryptAES256(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOB1.key");

                eChallanPushReqChild objbobchild = new eChallanPushReqChild();
                objbobchild.value = cipherText;
                objbobchild.stateCode = "RJ";

                eChallanPushReqParent objbobParent = new eChallanPushReqParent();
                objbobParent.eChallanPushReq = objbobchild;

                try
                {
                    using (WebClient webClient1 = new WebClient())
                    {

                        //var url = "http://103.85.40.22:7773/cmsforwarderUAT";
                        var url = System.Web.Configuration.WebConfigurationManager.AppSettings["BOBM"];
                        webClient1.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        webClient1.Headers[HttpRequestHeader.ContentType] = "application/json";
                        string data1 = JsonConvert.SerializeObject(objbobParent);
                        //var response = webClient1.UploadString(url, "POST", data1);
                        var response = "liUbWQimEWl8cai41ENxz891ddsGbMq5EXcZ3DrUEVvnxZY0zUcRPsjTXUQ2Ax8+y/x73HPL0IqA9s5pAxiHRlgyRD+Xky7goMD3oeasqSneBj4P3o/Ysgxu6SnsBW5tk7AkdN6UaPkAIewVBOQSbaRJh4xiXyVsdvOtJS1HYfjHZzux80xVLdx4DIaOvNGw4FE6ZU0shiJ4k5joR2zIT4wgav+tGa1rU4B01xCJChlsGsDWi76DSp9pmq0wF6ug";
                        EgManualBankServiceBL objEgManual = new EgManualBankServiceBL();
                        objEgManual.BankURL = url;
                        objEgManual.CipherText = response + "_Manual";
                        objEgManual.GRNNumber = grn;
                        objEgManual.BankCode = "0200113";
                        bool a = objEgManual.InsertAuditLog();
                        //result = JsonConvert.DeserializeObject<ReturnMessageInfo>(response);
                        //return result;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (objEgManualBankServiceBl.GetGRNBsrCode() == "9960001")
            {
                NEFTRTGS ObjNeftRtgs = new NEFTRTGS();
                string rtn = ObjNeftRtgs.CallVerifyService(grn);


            }

        }
        catch (Exception ex)
        {
            //Browserinfo objbrowseringo = new Browserinfo();
            //string msg = ex.Message + objbrowseringo.Browserinformaion();

            var objEgManualBankServiceBl = new EgManualBankServiceBL { Errorname = ex.Message }; //sandeep 

            //var url = "EgManualChallan.aspx";
            //objEgManualBankServiceBl.PageName = url + grn;
            //objEgManualBankServiceBl.insertErrorLog();
            objEgManualBankServiceBl.BankCode = BankCode;
            objEgManualBankServiceBl.ServiceType = 0;
            objEgManualBankServiceBl.BankURL = BankCode.Substring(0, 3) == "030" ? ConfigurationManager.AppSettings["PNBManualWebServ.ReceiveData"].Split('/').GetValue(2).ToString().Replace("www.", "") : Dns.GetHostAddresses("ekuberonline.rbi.org.in")[0].ToString();
            objEgManualBankServiceBl.InsertBankServiceErrorLog();
        }
    }
    protected void LoadReport()
    {
        //await Task.Run(() =>
        // {
        var param = new ReportParameter[1];
        param[0] = new ReportParameter("grn", Session["GrnNumber"].ToString());
        var objssrs = new SSRS();
        objssrs.LoadSSRS(rptGoodsAndServices, "EgGoodsAndServiesRpt", param);
        //create PDF
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptGoodsAndServices.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;
        string FileName = "GoodsAndServicesReceipt";
        Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();

        // });
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        LoadReport();
    }
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy() { }
        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert,
            WebRequest req,
            int problem)
        {
            return true;
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
}
public class eChallanPushReqParent
{
    public eChallanPushReqChild eChallanPushReq { get; set; }
}
public class eChallanPushReqChild
{
    public string value { get; set; }
    public string stateCode { get; set; }
}
public class eChallanPushResParent
{
    public string System { get; set; }
    public string ServiceType { get; set; }
    public string Signature { get; set; }
    public string Statu_cd { get; set; }
    public Payload payload { get; set; }

}
public class Payload
{
    public string RESP_CODE { get; set; }
    public string RJCT_DESC { get; set; }
}