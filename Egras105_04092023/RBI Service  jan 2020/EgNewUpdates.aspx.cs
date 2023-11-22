using System;
using EgBL;

public partial class EgNewUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EgNewUpdates objnewupdates=new EgNewUpdates();
        objnewupdates.Flag = 1;
        objnewupdates.NewUpdatesPdf(rptNewUpdate);
    }
}
