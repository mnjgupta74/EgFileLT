using EgBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_TreasuryOfficeMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\logout.aspx");
        }
    }
    protected void rptStudentDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdID");
            DataRowView drv = e.Item.DataItem as DataRowView;
            string gen = Convert.ToString(drv["Status"]);

            if (gen == "Office Active")
            {
                td.Attributes.Add("style", "background-color:#1a8338;color:#FFFFFF;");
            }
            else
            {
                td.Attributes.Add("style", "background-color:#dd1a16;color:#FFFFFF;");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtOfficeID.Text == "" || Convert.ToInt32(txtOfficeID.Text) <= 0)
            {
                lblloginInfo.Visible = true;
                txtOfficeID.Text = "";
                lblloginInfo.Text = "Please Enter Valid OfficeID";
                rptStudentDetails.Visible = false;
            }
            else
            {
                EgGetTreasuryOfficeBL egBL = new EgGetTreasuryOfficeBL();
                DataTable dt = new DataTable();
                egBL.OfficeID = Convert.ToInt32(txtOfficeID.Text);
                dt = egBL.GetTreasuryOfficeMapList();
                if (dt.Rows.Count > 0)
                {
                    lblloginInfo.Visible = false;
                    rptStudentDetails.Visible = true;
                    rptStudentDetails.DataSource = dt;
                    rptStudentDetails.DataBind();
                }
                else
                {
                    lblloginInfo.Visible = true;
                    rptStudentDetails.Visible = false;
                    lblloginInfo.Text = "Record Not Found!";
                    rptStudentDetails.DataSource = null;
                    rptStudentDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblloginInfo.Visible = true;
            lblloginInfo.Text = "Invalid OfficeID";
            rptStudentDetails.Visible = false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Invalid OfficeID');", true);
        }
        //            PlaceHolder1.Visible = true;
        //            lblloginInfo.Visible = false;

        //            StringBuilder html = new StringBuilder();
        //            //Table start.
        //            html.Append("<table class='table' style='text - align:center' border = '1'>");

        //            //Building the Header row.
        //            html.Append("<tr >");
        //            foreach (DataColumn column in dt.Columns)
        //            {
        //                html.Append("<th>");
        //                html.Append(column.ColumnName);
        //                html.Append("</th>");
        //            }
        //            html.Append("</tr>");

        //            //Building the Data rows.
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                html.Append("<tr class='success'>");
        //                foreach (DataColumn column in dt.Columns)
        //                {
        //                    html.Append("<td>");
        //                    if (column.ColumnName == "Status" && row[column.ColumnName].ToString() == "Ofiice Active")
        //                    {
        //                        html.Append("<td style='background-color:green'>" + row[column.ColumnName].ToString() + "</td>");
        //                        //html.Append("< td style = 'background-color:White;font-size:12px;font-weight:bold;' >");
        //                        //string str = row[column.ColumnName].ToString();
        //                        //DateTime dateAndTime = DateTime.Parse(row[column.ColumnName].ToString());
        //                        //string cellValue = dateAndTime.ToShortDateString();
        //                        //html.Append(cellValue);
        //                    }
        //                    if (column.ColumnName == "Status" && row[column.ColumnName].ToString() == "Ofiice Deactive")
        //                    {
        //                        html.Append("<td style='background-color:red'>" + row[column.ColumnName].ToString() + "</td>");
        //                    }
        //                    //else
        //                    //{
        //                    //    html.Append(row[column.ColumnName]);
        //                    //    //html.Append("<td style='background-color:red'>" + row[column.ColumnName].ToString() + "</td>");
        //                    //}
        //                    html.Append(row[column.ColumnName]);
        //                    html.Append("</td>");
        //                }
        //                html.Append("</tr>");
        //            }

        //            //Table end.
        //            html.Append("</table>");
        //            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
        //        }
        //        else
        //        {
        //            PlaceHolder1.Visible = false;
        //            lblloginInfo.Visible = true;
        //            lblloginInfo.Text = "No data Found";
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    PlaceHolder1.Visible = false;
        //    lblloginInfo.Visible = true;
        //    EgErrorHandller obj = new EgErrorHandller();
        //    obj.InsertError(ex.Message);
        //    lblloginInfo.Text = "Please Enter Valid OfficeID";
        //}

    }
}