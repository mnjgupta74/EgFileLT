using DL;
using EgBL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;

/// <summary>
/// Summary description for EgGrnPdfService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EgGrnPdfService : System.Web.Services.WebService
{
    string cipherText = string.Empty;
    public EgGrnPdfService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Byte[] GetGRNPDF(string encGRN, int MerchantCode)
    {
        ErrorHandlePdf objPdf = new ErrorHandlePdf();
        byte[] returnValue = null;
        try
        {
            SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
            EncryptDecryptionBL objEncryptDecryptionBL = new EncryptDecryptionBL();
            cipherText = objEncry.Decrypt(encGRN, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            string[] Ivalues = cipherText.Split('|');
            Int64 VID = Convert.ToInt64(Ivalues[0].Split('=').GetValue(1).ToString());
            int MerCode = Convert.ToInt32(Ivalues[1].Split('=').GetValue(1));
             double Amount = Convert.ToDouble(Ivalues[2].Split('=').GetValue(1));

            SqlParameter[] PM = new SqlParameter[3];
            PM[0] = new SqlParameter("@GRN", SqlDbType.BigInt) { Value = VID };
            PM[1] = new SqlParameter("@Amount", SqlDbType.Money) { Value = Amount };
            PM[2] = new SqlParameter("@Mcode", SqlDbType.Int) { Value = MerchantCode };
            // Result = gf.ExecuteScaler(PM, "GetGrnMerchantDetails_ws");
            int Result = Convert.ToInt16(SqlHelper.ExecuteScalar(SqlHelper.conString, "EgCheckGrnExistancewithMerchaneCode", PM));
            if (Result == 1)
            {
                ReportViewer objReport = new ReportViewer();
                ReportParameter[] param = new ReportParameter[4];
                param[0] = new ReportParameter("UserId", "46");
                param[1] = new ReportParameter("Usertype", "2");
                param[2] = new ReportParameter("GRN", Convert.ToString(VID));
                param[3] = new ReportParameter("ChallanNo", Convert.ToString(0));
                SSRS objssrs = new SSRS();
                objssrs.LoadSSRS(objReport, "EgDefaceDetailNew", param);
                ////create PDF
                //byte[] returnValue = null;
                string format = "PDF";
                string deviceinfo = "";
                string mimeType = "";
                string encoding = "";
                string extension = "pdf";
                string[] streams = null;
                Microsoft.Reporting.WebForms.Warning[] warnings = null;
                returnValue = objReport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            }

            else
            {
                string Msg = "Record Not Found With Relevant Information";
                returnValue = objPdf.Error(Msg);


            }
        }
        catch (Exception ex)
        {
            string Msg = "Request Unable To Process !";
            returnValue = objPdf.Error(Msg);
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString() + "-Grn PDF Service-");
        }
        return returnValue;
    }

  
}
