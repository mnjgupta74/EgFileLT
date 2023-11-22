using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using EgBL;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

public partial class WebPages_EgManualChallan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "" || Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
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
                    var validUpToDate = challandate.AddMonths(1).AddDays(-1);
                    lblChallanDateValidUpto.Text = Convert.ToDateTime(validUpToDate).ToString("dd/MM/yyyy");
                    lblChallanDate.Text = Convert.ToDateTime(objEgEChallan.ChallanDate).ToString("dd/MM/yyyy hh:mm:ss tt");
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
                    //if (objEgEChallan.BankName.Trim() == "sbi aNY WHERE".Trim())
                    if (objEgEChallan.BankName.ToUpper().Trim() == "RBI".ToUpper().Trim())
                    {
                        btnshow.Visible = true;
                    }
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "addScript", " checkamounttotal('" + lblTotalAmount.ID + "')", true);
            }
            catch (Exception ex)
            {
                if (Session["GrnNumber"] == null || Session["GrnNumber"].ToString() == "")
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError(ex.StackTrace.ToString());
                }
                else
                {
                    EgErrorHandller obj = new EgErrorHandller();
                    obj.InsertError("GRN=" + Session["GrnNumber"].ToString() + "|" + ex.StackTrace.ToString());
                }
            }
        }
    }

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
        EgErrorHandller obj = new EgErrorHandller();
        obj.InsertError("Grn : " + grn);
        try
        {
            var objEgManualBankServiceBl = new EgManualBankServiceBL { GRNNumber = grn };
            objEgManualBankServiceBl.BankCode = objEgManualBankServiceBl.GetGRNBsrCode();
            BankCode = objEgManualBankServiceBl.BankCode;
            var challanDetails = objEgManualBankServiceBl.GetGrnManualDetails();
            obj.InsertError("Get challanDetails : " + grn);
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
                //SBIManualServ.RAJASTHANWS objsbiPush = new SBIManualServ.RAJASTHANWS();
                var cipherText = objEncry.Encrypt(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "RAJASTHAN_EGRASS.key");

                Ojpost.ProcessRajasthanData(cipherText);// PUSH Service SBI Manual Challan anywhere
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
                var cipherText = objEncry.EncryptSBIWithKey256(challanDetails, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "BOB1.key");

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
                        var response = webClient1.UploadString(url, "POST", data1);

                        EgManualBankServiceBL objEgManual = new EgManualBankServiceBL();
                        objEgManual.BankURL = url;
                        objEgManual.CipherText = response + "_Manual";
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


                // Ojpost.ProcessRajasthanData(cipherText);// PUSH Service SBI Manual Challan anywhere
            }
           
            if (objEgManualBankServiceBl.GetGRNBsrCode() == "1000199")
            {
                obj.InsertError("neftRTGS enter  ");
                egNeftRtgsBL objegNeftRtgsBL = new egNeftRtgsBL();
                objegNeftRtgsBL.CPIN = grn.ToString();
                objegNeftRtgsBL.certificatePath = Server.MapPath(@"~\Certificate\" + ConfigurationManager.AppSettings["Certificate"].ToString());


                obj.InsertError("neftRTGS CPIN : " + grn);


                string JSONString = objegNeftRtgsBL.CPINPUSHREQ(grn);

                obj.InsertError("neftRTGS JSONString : " + JSONString);

                string jsonResponse = string.Empty;
                using (WebClient webClient1 = new WebClient())
                {

                    obj.InsertError("neftRTGS call URL1 : " + System.Web.Configuration.WebConfigurationManager.AppSettings["NEFTRTGS"]);
                    var url = System.Web.Configuration.WebConfigurationManager.AppSettings["NEFTRTGS"];
                    webClient1.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient1.Headers[HttpRequestHeader.ContentType] = "application/json";
                    // string data1 = JsonConvert.SerializeObject(objbobParent);
                    obj.InsertError("neftRTGS call URL : " + url);
                    jsonResponse = webClient1.UploadString(url, "POST", JSONString);

                    obj.InsertError("neftRTGS jsonResponse : " + jsonResponse);
                    // return result.eChallanPaymentInqResp.value;
                    //return "SKaKNJKaODnbgqLNzXlY2Xn0tKmzCkYpa7sx4uvj8Z6jrI9CaNw4yTdY/w9mbUfVeJWH2MmW9jUUl2BbnMsZRiiqdRFfI4/sDZX/Py+qosI/xM2QYolQ8H50cxkcWv4NSP1UfdUVdep47eZ2J8Maw+JDb75rbEOHrLGLIjF7L1s0tl8aDBbcYjbZasOxeKlylQMLQqHveqTYOAusp++IdZu8eu7KpMfz8WdJZYgJvXePkY5MdH4tf3ikK23dB5QmDuZ5GQ9u0+6AYEzCYrR7cqAeRb/MkawX";
                }

                string insertFlag = objegNeftRtgsBL.CPINPUSHRES(jsonResponse);


            }
        }
        catch (Exception ex)
        {
            //EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError("neftRTGS Exception : " + ex.Message);
            var objEgManualBankServiceBl = new EgManualBankServiceBL { Errorname = ex.Message }; //sandeep 

            objEgManualBankServiceBl.BankCode = BankCode;
            objEgManualBankServiceBl.ServiceType = 0;
            objEgManualBankServiceBl.BankURL = BankCode.Substring(0, 3) == "030" ? ConfigurationManager.AppSettings["PNBManualWebServ.ReceiveData"].Split('/').GetValue(2).ToString().Replace("www.", "") : ConfigurationManager.AppSettings["SBIManualServ.RAJASTHANWS"].Split('/').GetValue(2).ToString().Replace("www.", "");
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
