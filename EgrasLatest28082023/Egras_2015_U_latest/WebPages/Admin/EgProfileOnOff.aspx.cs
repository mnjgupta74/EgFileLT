using System;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

public partial class WebPages_Admin_EgProfileOnOff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rbltype.SelectedValue == "1")
        {
            string data = string.Empty;
            foreach (ListItem item in lstRemove.Items)
                data = string.Concat(data, item.Value, ",");

            data = data.TrimEnd(',');
            string data1 = string.Empty;
            foreach (ListItem li in chksel.Items)
            {
                if (li.Selected)
                {
                    data1 = string.Concat(data1, li.Value, ",");
                }
            }
            data1 = data1.TrimEnd(',');
            EgProfileBL objEgProfileBl = new EgProfileBL();
            objEgProfileBl.RemovedUserProfileList = data.ToString();
            objEgProfileBl.SelectedUserProfileList = data1.ToString();
            objEgProfileBl.LoginId = txtLoginId.Text;
            int res = objEgProfileBl.UpdateProfileList();


            if (res == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Updated Successfully')", true);
                txtLoginId.Text = "";
                lstRemove.Items.Clear();
                chksel.Items.Clear();
                PanelRemove.Visible = false;
                PanelSelected.Visible = false;
                lblrem.Visible = false;
                lblsel.Visible = false;
                btnSubmit.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Could Not be Updated')", true);
            }
        }
        else
        {
            EgProfileBL objEgProfileBL = new EgProfileBL();
            objEgProfileBL.LoginId = txtLoginId.Text;
            objEgProfileBL.LoggedinUserId = Convert.ToInt32(Session["UserId"]);
            objEgProfileBL.IsActive = chkuserAct.Checked;
            int res = objEgProfileBL.UpdateUserActiveDeactiveStatus();
            if (res == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Updated Successfully')", true);
                btnSubmit.Visible = false;
                txtLoginId.Text = "";
                chkuserAct1.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Could Not be Updated')", true);
            }
        }
    }
    protected void chksel_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstRemove.Items.Clear();
        foreach (ListItem li in chksel.Items)
        {
            if (!li.Selected)
            {

                lstRemove.DataTextField = "UserProfile";
                lstRemove.DataValueField = "UserPro";
                lstRemove.Items.Add(li);

            }
            //else
            //if (li.Selected)
            //{
            //    lstRemove.Items.Remove(li.Text);
            //}
        }
        if (lstRemove.Items.Count > 0)
        {
            lstRemove.Visible = true;
            PanelRemove.Visible = true;
            lblrem.Visible = true;
        }
    }

    protected void txtLoginId_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtLoginId.Text))
        {
            if (rbltype.SelectedValue == "1")
            {
                EgProfileBL objEgProfileBL = new EgProfileBL();
                DataTable dt = new DataTable();
                lstRemove.Items.Clear();
                chksel.Items.Clear();
                trprofilelabel.Visible = true;
                trprofilepanel.Visible = true;
                objEgProfileBL.LoginId = txtLoginId.Text;

                dt = objEgProfileBL.GetProfileList();
                IEnumerable<DataRow> data1 = (from c in dt.AsEnumerable()
                                              where c.Field<string>("flag") == "Y"
                                              select
                                              c);
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("UserProfile");
                dt2.Columns.Add("UserPro");
                if (data1.Count() > 0)
                {
                    dt2 = data1.CopyToDataTable();
                }
                if (dt2.Rows.Count > 0)
                {
                    chksel.DataSource = dt;
                    chksel.DataTextField = "UserProfile";
                    chksel.DataValueField = "UserPro";
                    chksel.DataBind();
                    (from i in chksel.Items.Cast<ListItem>() select i).ToList().ForEach(i => i.Selected = true);
                    //for (int i = 0; i < dt2.Rows.Count; i++)
                    //{
                    //    chksel.Items[i].Selected = true;
                    //}
                    chksel.Visible = true;
                    PanelSelected.Visible = true;
                    lblsel.Visible = true;
                    //lblofc.Visible = true;
                    dt2.Clear();
                }
                IEnumerable<DataRow> data2 = (from c in dt.AsEnumerable()
                                              where c.Field<string>("flag") == "N"
                                              select
                                              c);
                if (data2.Count() > 0)
                {
                    dt2 = data2.CopyToDataTable();
                }
                if (dt2.Rows.Count > 0)
                {
                    lstRemove.DataSource = dt2;
                    lstRemove.DataTextField = "UserProfile";
                    lstRemove.DataValueField = "UserPro";
                    lstRemove.DataBind();
                    lstRemove.Visible = true;
                    if (chksel.Items.Count == 0)
                    {
                        if (lstRemove.Items.Count > 0)
                        {

                            foreach (ListItem item in lstRemove.Items)
                            {
                                chksel.DataTextField = "UserProfile";
                                chksel.DataValueField = "UserPro";
                                chksel.Items.Add(item);
                            }
                            chksel.Visible = true;
                            PanelSelected.Visible = true;
                            lblsel.Visible = true;
                        }

                    }

                    foreach (ListItem item in lstRemove.Items)
                    {

                        foreach (ListItem li in chksel.Items)
                        {
                            if (li.Text == item.Text)
                            {
                                li.Selected = false;

                            }
                        }
                    }


                    //for (int i = 0; i < dt2.Rows.Count; i++)
                    //    {

                    //        chksel.Items[i].Selected = false;
                    //    }
                    PanelRemove.Visible = true;
                    lblrem.Visible = true;
                    btnSubmit.Visible = true;
                    dt2.Clear();
                }
            }
            else
            {
                chkuserAct1.Visible = true;
                btnSubmit.Visible = true;
                EgProfileBL objEgProfileBL = new EgProfileBL();
                objEgProfileBL.LoginId = txtLoginId.Text;
                DataTable dt = objEgProfileBL.GetUserActiveDeactiveStatus();
                var res = (from c in dt.AsEnumerable()
                           select
                           c.Field<bool>("IsActive")).First();
                if (res)
                {
                    chkuserAct.Checked = true;
                }
                else
                {
                    chkuserAct.Checked = false;
                }

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Enter Login Id.')", true);
            chkuserAct1.Visible = false;
            btnSubmit.Visible = false;
            trprofilelabel.Visible = false;
            trprofilepanel.Visible = false;
        }

    }

    protected void rbltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbltype.SelectedValue == "2")
        {
            trprofilepanel.Visible = false;
            trprofilelabel.Visible = false;
            btnSubmit.Visible = false;
            txtLoginId.Text = "";
        }
        else
        {
            chkuserAct1.Visible = false;
            btnSubmit.Visible = false;
            txtLoginId.Text = "";
        }
    }
}