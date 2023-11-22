using EgBL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class EgBankOfficerDetails : System.Web.UI.Page
{
    EgLoginBL objLogin = new EgLoginBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EgEChallanBL objegechallan = new EgEChallanBL();
            objegechallan.GetBanks(DropDownList1);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try

        {
            if (HttpContext.Current.Session["captcha"].ToString().Trim() == inpHide.Text.Trim())
            {
                //  DataTable dt = HttpContext.Current.Cache["Nodal"] as DataTable; 
                objLogin.BsrCode = DropDownList1.SelectedValue.ToUpper();
                DataTable dt = objLogin.GetBankOfficerDetails();
                //var BankDetails = dt.AsEnumerable().Where(t => t.Field<string>("BsrCode") == DropDownList1.SelectedValue.ToUpper());
                //if (str.IndexOf(txtEmpName.Text, StringComparison.OrdinalIgnoreCase) >= 0)

                //if (BankDetails.Count() != 0)
                //{
                if (dt.Rows.Count > 0)
                {
                    //dt = BankDetails.CopyToDataTable();
                    DataList1.Visible = true;
                    lblMsg.Visible = false;
                    field1.Visible = true;
                    lbldeptName.Text = DropDownList1.SelectedItem.Text;
                    DataList1.DataSource = dt;
                    DataList1.DataBind();
                }
                else
                {
                    DataList1.Visible = false;
                    lblMsg.Visible = true;
                    field1.Visible = false;
                }
                ////}
                //else
                //{
                //    DataList1.Visible = false;
                //    lblMsg.Visible = true;
                //    field1.Visible = false;
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('Wrong Captcha Value !');", true);
            }
            inpHide.Text = string.Empty;
        }
        catch (Exception ex)
        {
            //HttpContext.Current.Session["captcha"] = inpHide.Text.Trim();

        }
      }

}
