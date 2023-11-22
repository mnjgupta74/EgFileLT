using System;
using System.Web.UI;
using System.Configuration;
using System.Text;
using System.IO;

public partial class WebPages_BankForward  : System.Web.UI.Page
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
            if (Request.Form["key"] != null)
            {
            }
            else if (Request.Form["merchant_code"].ToString() == ConfigurationManager.AppSettings["Epay"].ToString())
            {
                html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(encdata)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(merchant_code)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            else
            {
                html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(EncryptTrans)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = System.Text.RegularExpressions.Regex.Replace(html, "<input[^>]*id=\"(merchIdVal)\"[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            writer.Write(html);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (Request.Form["key"] != null)
        {
            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in hash table for data post
            for (int i = 0; i < Request.Form.Keys.Count-1; i++)
            {
                data.Add(Request.Form.Keys[i], Request.Form[i]);
            }
            string strForm = PreparePOSTForm(Request.Form["URL"].ToString(), data);
            Page.Controls.Add(new LiteralControl(strForm));

        }
        else
        {

            if (Request.Form["merchant_code"] == null || Request.Form["encdata"] == null)
            {
                Response.Redirect("~\\logout.aspx");
            }
            #region GetPostMethod
            if (Request.Form["encdata"] != null)
            {
                if (Request.Form["merchant_code"].ToString() == ConfigurationManager.AppSettings["Epay"].ToString())
                {
                    EncryptTrans.Value = Request.Form["encdata"].ToString();
                    merchIdVal.Value = Request.Form["merchant_code"].ToString();

                }
                else
                {
                    encdata.Value = Request.Form["encdata"].ToString();
                    merchant_code.Value = Request.Form["merchant_code"].ToString();
                }
                form1.Action = Request.Form["URL"].ToString();
                Session.Abandon();

                // for tempary testing
                // Session["AbC"] = Request.Form["encdata"].ToString();
            }

            #endregion

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>Submit1_onclick();</script>", false);

        }


    }
    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

}
