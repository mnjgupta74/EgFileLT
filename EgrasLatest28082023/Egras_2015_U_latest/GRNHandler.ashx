<%@ WebHandler Language="C#" Class="GRNHandler" %>

using System;
using System.Web;

public class GRNHandler : IHttpHandler,System.Web.SessionState.IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        string gg = context.Request.Url.ToString();
        if (context.Request.RequestType == "POST")
        {
                HttpContext.Current.Session["HandlerVal"] = context.Request.QueryString["HandlerVal"].ToString();
        }
        else
        {

        }
        //HttpContext.Current.Session["HandlerVal"] = context.Request.RequestType == "POST" //QueryString["HandlerVal"].ToString(); //gg.Split('?').GetValue(1).ToString().Split('=').GetValue(1).ToString();

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}