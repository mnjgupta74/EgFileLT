<%@ WebHandler Language="C#" Class="ChallanView" %>

using System;
using System.Web;

public class ChallanView : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}