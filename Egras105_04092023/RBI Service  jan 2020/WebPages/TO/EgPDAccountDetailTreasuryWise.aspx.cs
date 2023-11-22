using EgBL;
using System;
using System.Web.Services;

public partial class WebPages_TO_EgPDAccountDetailTreasuryWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

    }
    [WebMethod]
    public static string GetPDAccountDetail(string FromDate, string ToDate)
    {
        EgPDAccountDetail objEgPDAccountDetail = new EgPDAccountDetail();
        //objEgPDAccountDetail.FromDate = Convert.ToDateTime(FromDate);
        //objEgPDAccountDetail.ToDate = Convert.ToDateTime(ToDate);
        string[] fromdate = FromDate.Split('/');
        objEgPDAccountDetail.FromDate = Convert.ToDateTime(fromdate[1].ToString() + '/' + fromdate[0].ToString() + '/' + fromdate[2].ToString());
        string[] Todate = ToDate.Split('/');
        objEgPDAccountDetail.ToDate = Convert.ToDateTime(Todate[1].ToString() + '/' + Todate[0].ToString() + '/' + Todate[2].ToString());
        string JSONGRNString = objEgPDAccountDetail.GetPdAccountDetail();
        if (JSONGRNString.Length > 2)
        {
            return JSONGRNString;
        }
        else
        {
            return "0";
        }
    }
}