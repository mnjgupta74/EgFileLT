using EgBL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Text;
using System.Web.UI.WebControls;

public partial class GetGRNStatus : System.Web.UI.Page
{
    EgLoginBL objLogin;//= new EgLoginBL();

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        inpHide.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnLogin').click();return false;}} else {return true}; ");
        txtGRN.Focus();
        if (!Page.IsPostBack)
        {

            objLogin = new EgLoginBL();
            Random randomclass = new Random();
            Session.Add("Rnd1", randomclass.Next().ToString());
            btnLogin.Attributes.Add("onclick", "javascript:return clickme(" + Session["Rnd1"] + ");");
            // Cache.Insert("Nodal", objLogin.GetNodalOfficerDetails(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);   //  Set Nodal Office Data
            // Cache.Insert("DepartmentList", objLogin.PopulateDepartmentList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration); // Get Department List 
        }
    }
    #endregion


    #region Common Function

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    SetFocus(txtUserName);
    //}
    /// <summary>
    /// check user authentication
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {


        if (Page.IsValid)
        {
            string imageValue = String.Empty;
            if (txtGRN.Text.Contains(" "))
            {
                lblloginInfo.Visible = true;
                inpHide.Text = "";
                txtGRN.Text = "";
                lblloginInfo.Text = "Invalid GRN";
                return;
            }
            string ip = HttpContext.Current.Request.ServerVariables[ConfigurationManager.AppSettings["IPAddressHTTP"]].ToString(); //Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string imageValue = inpHide.Value;
            imageValue = inpHide.Text.Trim();

            if (HttpContext.Current.Session["captcha"] == null || HttpContext.Current.Session["captcha"].ToString() == "")
            {
                Response.Redirect("~\\GetGRNStatus.aspx", true);
            }
            try
            {

                if (HttpContext.Current.Session["captcha"].ToString().ToLower().Trim() != "" && imageValue.Trim() != "" && imageValue.Trim() != "0")
                {
                    if (HttpContext.Current.Session["captcha"].ToString().Trim() == imageValue.ToString().Trim())
                    {
                        //EgGrnGetStatus eg = new EgGrnGetStatus();
                        EgGrnGetStatusBL eg = new EgGrnGetStatusBL();
                        string GRN = txtGRN.Text;
                        if (txtGRN.Text != "")
                        {
                            DataTable dt = new DataTable();
                            eg.GRN = Convert.ToInt32(txtGRN.Text);
                            dt = eg.GetGRNStatus();
                            if (dt.Rows.Count > 0)
                            {
                                PlaceHolder1.Visible = true;
                                lblloginInfo.Visible = false;

                                StringBuilder html = new StringBuilder();

                                //Table start.
                                html.Append("<table class='table' style='text - align:center' border = '1'>");

                                //Building the Header row.
                                html.Append("<tr >");
                                foreach (DataColumn column in dt.Columns)
                                {
                                    html.Append("<th>");
                                    html.Append(column.ColumnName);
                                    html.Append("</th>");
                                }
                                html.Append("</tr>");

                                //Building the Data rows.
                                foreach (DataRow row in dt.Rows)
                                {
                                    html.Append("<tr class='success'>");
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        html.Append("<td>");
                                        if(column.ColumnName == "ChallanDate" && row[column.ColumnName].ToString() != "")
                                        {
                                            string str = row[column.ColumnName].ToString();
                                            DateTime dateAndTime = DateTime.Parse(row[column.ColumnName].ToString());
                                            string cellValue = dateAndTime.ToShortDateString();
                                            html.Append(cellValue);
                                        }
                                        //if (column.ColumnName == "Amount" && row[column.ColumnName].ToString() != "")
                                        //{
                                        //    string str = Convert.ToDouble(row[column.ColumnName]).ToString();
                                        //    html.Append(str);
                                        //}
                                        else
                                        {
                                            html.Append(row[column.ColumnName]);
                                        }
                                        
                                        html.Append("</td>");
                                    }
                                    html.Append("</tr>");
                                }

                                //Table end.
                                html.Append("</table>");
                                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                            }
                            else
                            {
                                PlaceHolder1.Visible = false;
                                lblloginInfo.Visible = true;
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('Please Enter Valid GRN');", true);
                                lblloginInfo.Text = "No data Found";
                            }

                            //string GetStatus = eg.GetGrnStatus(GRN);
                            //string[] arr = GetStatus.Split('|');

                            //if (arr[2] == "S")
                            //    GetStatus = "Success" + " and GRN : " + arr[0] + " " + " Amount : " + arr[1] + " Date : "+ arr[3];
                            //else if (arr[2] == "P")
                            //    GetStatus = "Pending" + " and GRN : " + arr[0] + " " + " Amount : " + arr[1];
                            //else if (arr[2] == "F")
                            //    GetStatus = "Fail" + " and GRN : " + arr[0] + " " + " Amount : " + arr[1];
                            //lblloginInfo.Visible = true;
                            ////ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('GRN Status is ''" + GetStatus + "');", true);
                            //lblloginInfo.Text = "GRN Status is " + GetStatus;

                        }
                        else
                        {
                            PlaceHolder1.Visible = false;
                            lblloginInfo.Visible = true;
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('Please Enter Valid GRN');", true);
                            lblloginInfo.Text = "Please Enter Valid GRN";
                        }
                    }
                    else
                    {
                        PlaceHolder1.Visible = false;
                        lblloginInfo.Visible = true;
                        inpHide.Text = "";
                        txtGRN.Text = "";
                        lblloginInfo.Text = "Incorrect Captcha Code !!";
                    }
                }
            }
            catch (Exception ex)
            {
                PlaceHolder1.Visible = false;
                lblloginInfo.Visible = true;
                //Browserinfo objbrowseringo = new Browserinfo();
                //string msg = ex.Message + objbrowseringo.Browserinformaion();
                EgErrorHandller obj = new EgErrorHandller();
                obj.InsertError(ex.Message + "  2ndCatch");
                lblloginInfo.Text = "Please Enter Valid GRN";
            }
        }
    }
    #endregion
}