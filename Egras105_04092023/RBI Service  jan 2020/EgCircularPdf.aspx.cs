using System;
using EgBL;
public partial class EgCircularPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EgLoginBL obj = new EgLoginBL();
            obj.CircularPdf(RptCircular);
        }

    }
}
