<%@ Application Language="C#" %>
<%@ Import Namespace="EgBL" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        log4net.Config.XmlConfigurator.Configure();
    }
    void Application_End(object sender, EventArgs e)
    {

    }
    void Application_BeginRequest()
    {
        //NOTE: Stopping IE from being a caching whole
        //HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //HttpContext.Current.Response.Cache.SetNoStore();
        //Response.Cache.SetExpires(DateTime.Now);
        //Response.Cache.SetValidUntilExpires(true);

    }
    private void Page_Error(object sender, System.EventArgs e)
    {
        EgErrorHandller obj = new EgErrorHandller();
        Exception ex = Server.GetLastError();
        //Browserinfo objbrowseringo = new Browserinfo();
        //string msg = ex.Message + objbrowseringo.Browserinformaion();
        //string Response = obj.SetError(ex.Message.ToString());
        string Response = obj.SetError(ex.Message);
    }
    void Application_Error(object sender, EventArgs e)
    {
        EgErrorHandller obj = new EgErrorHandller();
        EgEncryptDecrypt ObjEnc = new EgEncryptDecrypt();

        Exception ex = Server.GetLastError();

        if (ex != null)
        {
            //Response=ObjEnc.Encrypt(string.Format("Response={0}",Response.ToString()));
            if (ex.GetType().Name == "HttpException" && ex.Message.Contains("does not exist"))
            {
                System.Web.HttpContext.Current.Response.Redirect("~\\404Error.aspx");
            }
            else
            {
                //Browserinfo objbrowseringo = new Browserinfo();
                string msg = ex.Message;
                //+ objbrowseringo.Browserinformaion();
                //string Response = obj.SetError(ex.Message.ToString());
                string Response = obj.SetError(msg);
                System.Web.HttpContext.Current.Response.Redirect("~\\ErrorPage.aspx");
            }
        }
        else
        {


        }


    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e)

    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }
</script>
