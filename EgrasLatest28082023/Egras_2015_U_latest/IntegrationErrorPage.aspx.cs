using System;

public partial class IntegrationErrorPage : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {        lblError.Text = IntegrationClass.errorName;
    }
}
