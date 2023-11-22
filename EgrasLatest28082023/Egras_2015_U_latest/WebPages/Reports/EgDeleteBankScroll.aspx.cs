using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Data;

public partial class WebPages_Reports_EgDeleteBankScroll : System.Web.UI.Page
{
    EgDeleteBankScroll objEgDeleteBankScroll = new EgDeleteBankScroll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            //EgEChallanBL objEChallan = new EgEChallanBL();
            //objEChallan.GetBanks(ddlbankname);
            EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
            objEgGRNBankStatus.PopulateBankList(ddlbankname);
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {

        BindScrollData();
    }
    public void BindScrollData()
    {
        DataTable dt = new DataTable();
        setData();
        dt = objEgDeleteBankScroll.FillDeleteBankScrollGrid();
        grdScroll.DataSource = dt;
        grdScroll.DataBind();
        if (dt.Rows.Count > 0)
            btndelete.Visible = true;
        else
            btndelete.Visible = false;
        dt.Dispose();
    }
    public void setData()
    {
        string[] fromdate = txtDate.Text.Trim().Replace("-", "/").Split('/');
        objEgDeleteBankScroll.Date = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        objEgDeleteBankScroll.BSRcode = ddlbankname.SelectedValue;
        objEgDeleteBankScroll.PaymentType = Online_ManualRadioButton.SelectedValue;
    }

    protected void grdScroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindScrollData();

        grdScroll.PageIndex = e.NewPageIndex;
        grdScroll.DataBind();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        setData();
        //string value = "";
        //XDocument doc = XDocument.Load(Server.MapPath("~/SecurePassword.xml"));
        //foreach (XElement element in doc.XPathSelectElement("//Detail").Descendants())
        //{
        //    value = element.Value;
        //}

        if (txtSecurePass.Text.Trim() == System.Configuration.ConfigurationManager.AppSettings["SecurePassword"].ToString().Trim())
        {
            int i = objEgDeleteBankScroll.DeleteBankScrollData();
            if (i == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Scroll Deleted successfully.!');", true);
                BindScrollData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Scroll Not Deleted.!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
        }
    }

}
