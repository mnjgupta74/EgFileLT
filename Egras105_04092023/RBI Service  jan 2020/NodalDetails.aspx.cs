using EgBL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
public partial class NodalDetails : System.Web.UI.Page
{
    EgLoginBL objLogin = new EgLoginBL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            objUserProfileBL.PopulateDepartmentList(DropDownList1);
            // Get Department List 
            //if (HttpContext.Current.Cache["DepartmentList"] as DataTable == null)
            //{
            //    Cache.Insert("DepartmentList", objLogin.PopulateDepartmentList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
            //}
            //else
            //    DropDownList1.DataSource = HttpContext.Current.Cache["DepartmentList"];
            //DropDownList1.DataTextField = "deptnameEnglish";
            //DropDownList1.DataValueField = "deptcode";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Session["captcha"].ToString().Trim() == inpHide.Text.Trim())
            {
                //  DataTable dt = HttpContext.Current.Cache["Nodal"] as DataTable; 
                DataTable dt = new DataTable();
                dt=objLogin.GetNodalOfficerDetails();
                var NodalDetails = dt.AsEnumerable().Where(t => t.Field<Int32>("DeptCode") == Convert.ToInt32(DropDownList1.SelectedValue.ToUpper()));
                //if (str.IndexOf(txtEmpName.Text, StringComparison.OrdinalIgnoreCase) >= 0)

                if (NodalDetails.Count() != 0)
                {
                    if (NodalDetails.First() != null)
                    {
                        dt = NodalDetails.CopyToDataTable();
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
                }
                else
                {
                    DataList1.Visible = false;
                    lblMsg.Visible = true;
                    field1.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('Wrong Captcha Value !');", true);
            }
              inpHide.Text = string.Empty;
        }
         catch(Exception ex)
        {
           // HttpContext.Current.Session["captcha"] = inpHide.Text.Trim();
            // Do something with e, please.
        }

    }

    
}
