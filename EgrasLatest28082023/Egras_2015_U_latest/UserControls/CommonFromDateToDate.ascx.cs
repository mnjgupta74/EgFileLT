using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_CommonFromDateToDate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        calDate.EndDate = DateTime.Now.Date;
        CalendarExtender1.EndDate = DateTime.Now.Date;
    }
}