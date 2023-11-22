using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using EgBL;
/// <summary>
/// Summary description for EgDocumentDownloadService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EgDocumentDownloadService : System.Web.Services.WebService
{

    public EgDocumentDownloadService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

   
    public string GetData()
    {
       
        string path =(System.Web.Configuration.WebConfigurationManager.AppSettings["Document"] + "eGRAS.pdf");
        byte[] bytes = System.IO.File.ReadAllBytes(path);
        return Convert.ToBase64String(bytes);
    }

    [WebMethod]
    public void Download(string encData,int MerchantCode)
    {
        try
        {
            //SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();

            //string cipherText = objEncry.Decrypt(encData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + MerchantCode + ".key");
            //string[] Ivalues = cipherText.Split('|');
            //string documentName = Convert.ToString(Ivalues[0].Split('=').GetValue(1).ToString());
            //string  accessId = Convert.ToString(Ivalues[1].Split('=').GetValue(1));
            //string  accessPassword = Convert.ToString(Ivalues[2].Split('=').GetValue(1));


            EgDocumentDownloadService service = new EgDocumentDownloadService();
            string base64String = service.GetData();
            byte[] bytes = Convert.FromBase64String(base64String);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);

            Context.Response.Clear();
            Context.Response.Buffer = true;
            Context.Response.Charset = "";
            Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Context.Response.ContentType = "application/pdf";
            Context.Response.AddHeader("content-disposition", "attachment;filename=Document.pdf");
            Context.Response.BinaryWrite(bytes);
            Context.Response.Flush();
            Context.Response.End();
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Document.pdf");
            //ms.WriteTo(Response.OutputStream);
            //Response.End();
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message.ToString());
        }

    }

}
