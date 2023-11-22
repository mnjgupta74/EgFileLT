using System;
using EgBL;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
public partial class WebPages_Admin_EgPdAccForDMFT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            EgTreasuryMaster objEgTreasuryMaster = new EgTreasuryMaster();
            objEgTreasuryMaster.FillTreasury(drpTreasury);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string data = string.Empty;
        if (lstRemove.Items.Count > 0)
        {
            foreach (ListItem item in lstRemove.Items)
                data = string.Concat(data, item.Value, ",");

            data = data.TrimEnd(',');
        }
        else
        {
            data = "0";
        }

        string data1 = string.Empty;
        foreach (ListItem li in chksel.Items)
        {
            if (li.Selected)
            {
                data1 = string.Concat(data1, li.Value, ",");
            }
        }
        data1 = data1.TrimEnd(',');
        EgPDAccountDmftBL objEgPDAccountDmftBL = new EgPDAccountDmftBL();
        objEgPDAccountDmftBL.RemovedPdAccList = data.ToString();
        objEgPDAccountDmftBL.SelectedPdAccList = data1.ToString();
        objEgPDAccountDmftBL.BudgetHead = txtMajorHead.Text;
        objEgPDAccountDmftBL.TresCode = drpTreasury.SelectedValue;
        objEgPDAccountDmftBL.UserId =Convert.ToInt32(Session["UserID"]);
        int res = objEgPDAccountDmftBL.UpdatePdAccountForDMFT();


        if (res == 1)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Updated Successfully')", true);
            txtMajorHead.Text = "";
            drpTreasury.SelectedValue = "0";
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
    protected void chksel_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstRemove.Items.Clear();
        foreach (ListItem li in chksel.Items)
        {
            if (!li.Selected)
            {
                lstRemove.DataTextField = "PdAccName";
                lstRemove.DataValueField = "PDAccNo";
                lstRemove.Items.Add(li);
            }
        }
        if (lstRemove.Items.Count > 0)
        {
            lstRemove.Visible = true;
            PanelRemove.Visible = true;
            lblrem.Visible = true;
        }
    }
    protected void drpTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMajorHead.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Please Fill MajorHead.')", true);
        }
        else if (Convert.ToInt32(drpTreasury.SelectedValue) > 0)
        {
            if (txtMajorHead.Text.Length < 13)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Enter Correct Budget Head.')", true);
            }
            else
            {
                lstRemove.Items.Clear();
                chksel.Items.Clear();
                EgPDAccountDmftBL objEgPDAccountDmftBL = new EgPDAccountDmftBL();
                objEgPDAccountDmftBL.TresCode = drpTreasury.SelectedValue;
                objEgPDAccountDmftBL.BudgetHead = txtMajorHead.Text;
                DataTable dt = objEgPDAccountDmftBL.GetPdAccountForDMFT();
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<DataRow> data1 = (from c in dt.AsEnumerable()
                                                  where c.Field<bool>("RunMode") == false
                                                  select
                                                  c);
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("PdAccName");
                    dt2.Columns.Add("PDAccNo");

                    btnSubmit.Visible = true;

                    if (data1.Count() > 0)
                    {
                        dt2 = data1.CopyToDataTable();
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        chksel.DataSource = dt;
                        chksel.DataTextField = "PdAccName";
                        chksel.DataValueField = "PDAccNo";
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
                                                  where c.Field<bool>("RunMode") == true
                                                  select
                                                  c);
                    if (data2.Count() > 0)
                    {
                        dt2 = data2.CopyToDataTable();
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        lstRemove.DataSource = dt2;
                        lstRemove.DataTextField = "PdAccName";
                        lstRemove.DataValueField = "PDAccNo";
                        lstRemove.DataBind();
                        lstRemove.Visible = true;
                        if (chksel.Items.Count == 0)
                        {
                            if (lstRemove.Items.Count > 0)
                            {
                                foreach (ListItem item in lstRemove.Items)
                                {
                                    chksel.DataTextField = "PdAccName";
                                    chksel.DataValueField = "PDAccNo";
                                    chksel.Items.Add(item);
                                }
                                chksel.Visible = true;
                                PanelSelected.Visible = true;
                                lblsel.Visible = true;
                            }
                        }
                        PanelRemove.Visible = true;

                        lblrem.Visible = true;
                        dt2.Clear();
                    }
                    foreach (ListItem item in lstRemove.Items)
                    {
                        foreach (ListItem li in chksel.Items)
                        {
                            if (li.Value == item.Value)
                            {
                                li.Selected = false;
                            }
                            //else
                            //{
                            //    li.Selected = true;
                            //}
                        }
                    }
                }


                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('No Record Found.')", true);
                }
            }
            }
        
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Select Treasury.')", true);
        }
    }
}