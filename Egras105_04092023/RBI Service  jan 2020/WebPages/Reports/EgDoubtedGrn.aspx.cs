using EgBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgDoubtedGrn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetDoubtedGrn(string FromDate, string ToDate)
    {
        EgDoubtedGrnBL objEgGetDoubtedGrn = new EgDoubtedGrnBL();
        string[] fromdate = FromDate.Split('/');
        objEgGetDoubtedGrn.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = ToDate.Split('/');
        objEgGetDoubtedGrn.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        string JSONGRNString = objEgGetDoubtedGrn.getDoubtedGrnList();
        return JSONGRNString;
    }
}