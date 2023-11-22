using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_TO_EgEditChallanDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            Details.Visible = false;
            EgEChallanBL objEChallan = new EgEChallanBL();
            objEChallan.FillLocation(ddlTreasury);

            DropDownListX dp = new DropDownListX();
            DataTable treasuryData = new DataTable();
            treasuryData = dp.FillTreasury();

            for (int i = 1; i < 40; i++)
            {
                var rows = treasuryData.AsEnumerable().Where(t => t.Field<int>("TGroupCode") == i);

                string group = rows.ElementAtOrDefault(0).Field<string>("TreasuryName"); // ElementAtOrDefault(0).treasuryName.ToString().Trim();
                ddllocation.AddItemGroup(group.Trim());
                foreach (var item in rows)
                {
                    ListItem items = new ListItem(item.Field<string>("TreasuryName"), item.Field<string>("TreasuryCode"));
                    ddllocation.Items.Add(items);

                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text.Equals("Submit", StringComparison.InvariantCultureIgnoreCase))
        {
            EgEditChallanDetailsBL objEditChallan = new EgEditChallanDetailsBL();
            objEditChallan.GRN = Convert.ToInt64(txtGRN.Text);
            if (objEditChallan.GetGRNDetails() == 1)
            {
                if (objEditChallan.DeptCode.ToString() == "18")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Commerical Taxes Department.')", true);
                }
                else
                {
                    EgEChallanBL objEChallan = new EgEChallanBL();
                    ddlTreasury.SelectedValue = objEditChallan.District.ToString();
                    ddllocation.SelectedValue = objEditChallan.TreasuryCode.ToString();
                    objEChallan.Tcode = objEditChallan.District.ToString() + "00";
                    ViewState["DeptCode"] = objEditChallan.DeptCode.ToString();
                    objEChallan.DeptCode = Convert.ToInt32(objEditChallan.DeptCode.ToString());
                    txtTotalAmount.Text = objEditChallan.Amount.ToString();
                    txtName.Text = objEditChallan.FullName;
                    txtaddress.Text = objEditChallan.Address;
                    txtRemark.Text = objEditChallan.Remarks;
                    objEChallan.FillOfficeList(ddlOfficeName);
                    ddlOfficeName.SelectedValue = objEditChallan.Office.ToString();
                    Details.Visible = true;
                    txtGRN.Enabled = false;
                    txtTotalAmount.Enabled = false;
                    btnSubmit.Text = "Update";
                    txtfromdate.Text = objEditChallan.ChallanFromMonth.ToString("dd/MM/yyyy");
                    txttodate.Text = objEditChallan.ChallanToMonth.ToString("dd/MM/yyyy");
                    txtTIN.Text = objEditChallan.tin;
                    //int month = objEditChallan.month;
                    //switch (month)
                    //{
                    //    case 1:
                    //        ddlPeriod.SelectedValue = "3";                            
                    //        //lblPeriod.Text = "Monthly From   " + objEditChallan.ChallanFromMonth.ToShortDateString() + "   To   " + objEditChallan.ChallanToMonth.ToShortDateString();
                    //        break;
                    //    case 3:
                    //        ddlPeriod.SelectedValue = "4";
                    //        //lblPeriod.Text = "Quarterly From   " + objEditChallan.ChallanFromMonth.ToShortDateString() + "   To   " + objEditChallan.ChallanToMonth.ToShortDateString();
                    //        break;
                    //    case 6:
                    //        ddlPeriod.SelectedValue = "2";
                    //        //ddlhalfyearly.SelectedValue = 
                    //        //lblPeriod.Text = "Half Yearly From   " + objEditChallan.ChallanFromMonth.ToShortDateString() + "   To   " + objEditChallan.ChallanToMonth.ToShortDateString();
                    //        break;
                    //    case 12:
                    //        ddlPeriod.SelectedValue = "1";
                    //        //lblPeriod.Text = "Annual From   " + objEditChallan.ChallanFromMonth.ToShortDateString() + "   To   " + objEditChallan.ChallanToMonth.ToShortDateString();
                    //        break;
                    //    default:
                    //        ddlPeriod.SelectedValue = "5";
                    //        //lblPeriod.Text = "One Time From   " + objEditChallan.ChallanFromMonth.ToShortDateString() + "   To   " + objEditChallan.ChallanToMonth.ToShortDateString();
                    //        break;
                    //}

                }
            }
            else
            {
                lblEmptyData.Visible = true;
            }
        }
        else
        {
            EgEditChallanDetailsBL objEditChallan = new EgEditChallanDetailsBL();
            objEditChallan.GRN = Convert.ToInt32(txtGRN.Text);
            objEditChallan.TreasuryCode = ddllocation.SelectedValue;
            objEditChallan.Office = Convert.ToInt32(ddlOfficeName.SelectedValue);
            objEditChallan.FullName = txtName.Text;
            objEditChallan.Address = txtaddress.Text;
            objEditChallan.Remarks = txtRemark.Text;
            string Fdate = "";
            string Tdate = "";

            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
                Fdate = Convert.ToString(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
                Tdate = Convert.ToString(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
            }

            objEditChallan.ChallanFromMonth = Convert.ToDateTime( Fdate);
            objEditChallan.ChallanToMonth = Convert.ToDateTime( Tdate);
            objEditChallan.tin = txtTIN.Text;

            if (objEditChallan.EditChallanDetails() == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Record Updated Successfully')", true);
                Details.Visible = false;
                txtGRN.Enabled = true;
                txtGRN.Text = "";
                btnSubmit.Text = "Submit";
            }

        }
    }

    protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        EgEChallanBL objEChallan = new EgEChallanBL();
        objEChallan.DeptCode = int.Parse(ViewState["DeptCode"].ToString());
        objEChallan.Tcode = ddlTreasury.SelectedValue;
        objEChallan.FillOfficeList(ddlOfficeName);
    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        EgEChallanBL objEChallan = new EgEChallanBL();
        objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
        DataTable dt = objEChallan.FillOfficeWiseTreasury();
        if (dt.Rows.Count != 0)
        {
            ddllocation.SelectedValue = dt.Rows[0][1].ToString();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Details.Visible = false;
        btnSubmit.Text = "Submit";
        txtGRN.Enabled = true;
        txtGRN.Text = "";
    }   

}
