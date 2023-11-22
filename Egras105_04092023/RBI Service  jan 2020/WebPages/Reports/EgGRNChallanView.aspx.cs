using System;
using EgBL;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class WebPages_Reports_EgGRNChallanView : System.Web.UI.Page
{
    EgEncryptDecrypt ObjEncrytDecrypt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        lblMsg.Visible = false;
    }
    protected void txtbtn_Click(object sender, EventArgs e)
    {
        if (txtGRN.Text.TrimStart('0') == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "alert('Enter Valid GRN or Challan no.')", true);
            txtGRN.Text = "";
            return;
        }
        EgEChallanBL objEgEChallan = new EgEChallanBL();
        objEgEChallan.GRNNumber = rblSearchType.SelectedValue == "1" ? Convert.ToInt32(txtGRN.Text.TrimStart('0')) : 0;
        objEgEChallan.UserId = Convert.ToInt32(Session["Userid"].ToString());
        objEgEChallan.UserType = Session["UserType"].ToString();
        objEgEChallan.ChallanNo = rblSearchType.SelectedValue == "1" ? 0 : Convert.ToInt32(txtGRN.Text.TrimStart('0'));

        DataTable dt = new DataTable();
        dt = objEgEChallan.GetGrnOrChallanDetail();
        if (dt.Rows.Count > 0)
        {
            lblMsg.Visible = false;
            rptChallanFill.DataSource = dt;
            rptChallanFill.DataBind();
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "No Record Found !";
        }
    }
    protected void rptChallanFill_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            Label Grn = (Label)e.Item.FindControl("LabelGRN");
            string grn = Grn.Text;
            EgEChallanBL objEgEChallan = new EgEChallanBL();
            objEgEChallan.GRNNumber = Convert.ToInt64(grn);
            objEgEChallan.UserId = Convert.ToInt32(Session["Userid"].ToString());
            objEgEChallan.UserType = Session["UserType"].ToString();

            ObjEncrytDecrypt = new EgEncryptDecrypt();
            string strURLWithData = "";
            //if (rblSearchType.SelectedValue == "1")
            //{
                //strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}", txtGRN.Text.TrimStart('0'), objEgEChallan.UserId, objEgEChallan.UserType));
                strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}", grn, objEgEChallan.UserId, objEgEChallan.UserType));
            //}
            //else
            //{
            //    strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("Challan={0}&userId={1}&usertype={2}", txtGRN.Text.TrimStart('0'), objEgEChallan.UserId, objEgEChallan.UserType));
            //}
            strURLWithData = "../EgDefaceDetailNew.aspx?" + strURLWithData;
            Response.Redirect(strURLWithData);
        }
    }
    protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSearchType.SelectedValue == "2")
        {
            lblGRN.Text = "Challan No :";
        }
        else
        {
            lblGRN.Text = "GRN :";
        }
    }

}
