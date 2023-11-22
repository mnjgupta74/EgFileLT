using System;
using EgBL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class WebPages_Reports_EgManualFailGRN : System.Web.UI.Page
{
     EgManualFailBL objec = new EgManualFailBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            //div1.Visible = true;
            //div2.Visible = false;
            lblbank.Visible = true;
            ddlbank.Visible = true;
            //objec.PopulateBankList(ddlbank);
            EgGRNBankStatus objEgGRNBankStatus = new EgGRNBankStatus();
            objEgGRNBankStatus.PopulateBankList(ddlbank);
        }
       

    }
    public void setData()
    {
        TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
        TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
        string[] fromDate = txtfrmdate.Text.Split('/');
        objec.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
        string[] ToDate = txttodate.Text.Split('/');
        objec.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
        objec.BankCode = ddlbank.SelectedItem.Value;
    }
    public void stdata()
    {
        string txtbox = grntext.Text;
        objec.GRN = Convert.ToInt64(txtbox);
    }

    protected void btnfindresult_Click(object sender, EventArgs e)
    {
        setData();
        string value = "";
        XDocument doc = XDocument.Load(Server.MapPath("~/SecurePassword.xml"));
        foreach (XElement element in doc.XPathSelectElement("//Detail").Descendants())
        {
            value = element.Value;
        }
        if (rbllist.SelectedValue == "1")
        {
            EgManualFailBL objec = new EgManualFailBL();
            DataTable dt = new DataTable();
            TextBox txtfrmdate = (TextBox)this.frmdatetodate.FindControl("txtfromdatebank");
            TextBox txttodate = (TextBox)this.frmdatetodate.FindControl("txtTodatebnk");
            string[] fromDate = txtfrmdate.Text.Split('/');
            objec.FromDate = Convert.ToDateTime(fromDate[1].ToString() + '/' + fromDate[0].ToString() + '/' + fromDate[2].ToString());
            string[] ToDate = txttodate.Text.Split('/');
            objec.ToDate = Convert.ToDateTime(ToDate[1].ToString() + '/' + ToDate[0].ToString() + '/' + ToDate[2].ToString());
            objec.BankCode = ddlbank.SelectedItem.Value;
            if (textsecure.Text.Trim() == value.ToString().Trim())
            {
                dt = objec.manualfail();
                if (dt.Rows.Count > 0)
                {
                    divrpt.Visible = true;
                    //rptmanualfail.Visible = true;
                    rptmanualfail.DataSource = dt;
                    rptmanualfail.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                    divrpt.Visible = false;
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
            }
        }
        else if (rbllist.SelectedValue == "2")
        {
            divrpt.Visible = false;
            rptmanualfail.Visible = false;
        }
    }
    protected void grnsearch_Click(object sender, EventArgs e)
    {
        stdata();
        string value = "";
        XDocument doc = XDocument.Load(Server.MapPath("~/SecurePassword.xml"));
        foreach (XElement element in doc.XPathSelectElement("//Detail").Descendants())
        {
            value = element.Value;
        }

        if (rbllist.SelectedValue == "2")
        {
            DataTable dt = new DataTable();
            string txtbox = grntext.Text;
            objec.GRN = Convert.ToInt64(txtbox);
            if (securetext.Text.Trim() == value.ToString().Trim())
            {
                dt = objec.manualfailgrn();
                if (dt.Rows.Count > 0)
                {
                    divrpt.Visible = true;
                    rptmanualfail.Visible = true;
                    rptmanualfail.DataSource = dt;
                    rptmanualfail.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('No Record Found.')", true);
                    divrpt.Visible = false;
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Transaction password is not valid.!');", true);
            }
        }
        else if (rbllist.SelectedValue == "1")
        {
            divrpt.Visible = false;
        }
    }
    protected void rptmanualfail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        EgManualFailBL obj = new EgManualFailBL();
        e.Item.FindControl("btnpending").Visible = false;
        string[] grn = e.CommandArgument.ToString().Split(';');
        obj.GRN = Convert.ToInt64(grn[0]);
        obj.Amount = Convert.ToDouble(grn[1]);
        obj.UserId = Convert.ToInt32(Session["userid"]);
        int pending = obj.updatedatapending();
        if (pending == 1)
        {
            Label lblStatus = (Label)e.Item.FindControl("lblStatus");
            lblStatus.Text = "P";
        }
       
    }
    protected void rptmanualfail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptmanualfail.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblrecord = (Label)e.Item.FindControl("lblrecord");
                lblrecord.Visible = true;
            }           
        }
    }

   
}

