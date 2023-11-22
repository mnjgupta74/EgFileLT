using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_Reports_EgDefaceRefundAndReleaseRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    [WebMethod]
    public static string GetDefaceAndRefundDetail(string Grn, Int16 Type, string FromDate, string ToDate)
    {
        EgDefaceAndRefundDetailBL objEgDefaceAndRefundDetailBL = new EgDefaceAndRefundDetailBL();

        string[] fromdate = FromDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        string[] todate = ToDate.Trim().Substring(0, 10).Replace("-", "/").Split('/');
        if (Grn == "")
        {
            objEgDefaceAndRefundDetailBL.Grn = 0;
        }
        else {
            objEgDefaceAndRefundDetailBL.Grn = Convert.ToInt64(Grn);
        }
        objEgDefaceAndRefundDetailBL.Type = Type;
        objEgDefaceAndRefundDetailBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgDefaceAndRefundDetailBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        return objEgDefaceAndRefundDetailBL.GetDefaceAndReundDetail();
    }
}