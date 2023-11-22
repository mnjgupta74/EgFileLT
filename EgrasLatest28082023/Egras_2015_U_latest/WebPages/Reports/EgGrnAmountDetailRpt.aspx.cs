using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.Services;

public partial class WebPages_Reports_EgGrnAmountDetailRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    //protected void btnshow_Click(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}

    [WebMethod]
    public static string GrnAmountDetails(double Amount1, double Amount2, string FromDate, string ToDate)
    {
        


        EgGrnAmountDetailRptBL objEgGrnAmountDetailRptBL = new EgGrnAmountDetailRptBL();
        string[] revdateFrom, revdateTo;
        revdateFrom = FromDate.Trim().Split('/');
        objEgGrnAmountDetailRptBL.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
        revdateTo = ToDate.Trim().Split('/');
        objEgGrnAmountDetailRptBL.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
        objEgGrnAmountDetailRptBL.Amount1 = Amount1;
        objEgGrnAmountDetailRptBL.Amount2 = Amount2;
        //if (Amount2 >= Amount1)
        return objEgGrnAmountDetailRptBL.BindGrnAmountDetails();

    }
    //public void BindGrid()
    //{
    //    EgGrnAmountDetailRptBL objEgGrnAmountDetailRptBL = new EgGrnAmountDetailRptBL();
    //    string[] revdateFrom, revdateTo;
    //    revdateFrom = txtFromDate.Text.Trim().Split('/');
    //    objEgGrnAmountDetailRptBL.Fromdate = Convert.ToDateTime(revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]);
    //    revdateTo = txtToDate.Text.Trim().Split('/');
    //    objEgGrnAmountDetailRptBL.Todate = Convert.ToDateTime(revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]);
    //    objEgGrnAmountDetailRptBL.Amount1 = Convert.ToDouble(txtAmt1.Text);
    //    objEgGrnAmountDetailRptBL.Amount2 = Convert.ToDouble(txtAmt2.Text);
    //    if (Convert.ToDouble(txtAmt2.Text) >= Convert.ToDouble(txtAmt1.Text))
    //        objEgGrnAmountDetailRptBL.BindGrnAmountDetails(grdGrn);
    //    else
    //        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Amount1 Should be less than Amount2.!')", true);
    //}
    //protected void grdGrn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    BindGrid();
    //    grdGrn.PageIndex = e.NewPageIndex;
    //    grdGrn.DataBind();
    //}
    //protected void grdGrn_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
    //    if (e.CommandName == "grnbind")
    //    {
    //        LinkButton lb = (LinkButton)e.CommandSource;
    //        int grn = Convert.ToInt32(lb.Text);

    //        string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));

    //        string script = "window.open('../EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";



    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);

    //    }
    //}
}
