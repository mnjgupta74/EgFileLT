using System;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;

public partial class WebPages_EgIntergrationRedirect : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        using (HtmlTextWriter hWriter = new HtmlTextWriter(sw))
        {
            base.Render(hWriter);
            string html = sb.ToString();
            html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(__VIEWSTATE)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(__EVENTVALIDATION)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //if (Request.Form["merchant_code"].ToString() == Request.Form["merchant_code"].ToString())
            //{
            //html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(encdata)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(merchant_code)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //}
            //else
            //{
            //    html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(EncryptTrans)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //    html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(merchIdVal)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //}
            writer.Write(html);
            sb = null;
            html = null;
            hWriter.Dispose();
            sw.Dispose();
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        IntegrationPostURL objPush = new IntegrationPostURL();
        objPush.PushData(Request.Form["merchant_code"], Request.Form["encdata"]);
        #region GetPostMethod
        if (Request.Form["encdata"] != null && Request.Form["merchant_code"] != null && Request.Form["URL"] != null)
        {

            encdata.Value = Request.Form["encdata"].ToString();
            merchant_code.Value = Request.Form["merchant_code"].ToString();
            form1.Action = Request.Form["URL"].ToString(); // "http://localhost:29246/Server - Server/WebPages/BankResponseReceived.aspx"; //Request.Form["URL"].ToString();
            Session.Abandon();
        }
        else
        {
            Response.Redirect("~\\logout.aspx");
        }
        #endregion

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Submit1_onclick();</script>", false);
    }
  
}
