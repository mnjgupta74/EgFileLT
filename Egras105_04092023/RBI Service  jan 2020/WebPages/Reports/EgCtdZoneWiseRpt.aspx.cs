using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Data;
public partial class WebPages_Reports_EgCtdZoneWiseRpt : System.Web.UI.Page
{
    // Created by Rachit Sharma on 17 july 2014


    double TotalmoneySchema = 0;
    EgEChallanBL objEChallan;
    EgZoneCtdBL objEgZoneCtdBL;
    DataTable dtHead;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            // Filling Zone Dropdownlist on page load
            objEChallan = new EgEChallanBL();
            DataTable CTDTable = objEChallan.GetCTDInformation();
            DataTable dt = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0")).CopyToDataTable();
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Zone_Circle_Ward";
            ddlZone.DataValueField = "Location_Code";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("--Select All Zone--", "0"));


            divHeadAmountDetails.Visible = false;
            DivBudgetHead.Visible = false;
        }

    }

    protected void RptMajorHead_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {


            //double moneySchema = Convert.ToDouble(drv.Row["Amount"].ToString());
            string moneySchema1 = (e.Item.DataItem).ToString();
            double moneySchema = Convert.ToDouble(moneySchema1.Split(',')[1].Split('=')[1].Split('}')[0]);
            TotalmoneySchema = TotalmoneySchema + moneySchema;
        }
        if (TotalmoneySchema > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblSchemaTotal") as Label;
                labelAmount.Text = TotalmoneySchema.ToString("0.00");
            }
        }
    }

    protected void RptBudgetHead_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {


            //double moneySchema = Convert.ToDouble(drv.Row["Amount"].ToString());
            string moneySchema1 = (e.Item.DataItem).ToString();
            double moneySchema = Convert.ToDouble(moneySchema1.Split(',')[1].Split('=')[1].Split('}')[0]);
            TotalmoneySchema = TotalmoneySchema + moneySchema;
        }
        if (TotalmoneySchema > 0)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label labelAmount = e.Item.FindControl("lblBudgetHeadTotal") as Label;
                labelAmount.Text = TotalmoneySchema.ToString("0.00");
            }
        }

    }

    protected void btnshow_Click(object sender, EventArgs e)
    {

        dtHead = new DataTable();
        objEgZoneCtdBL = new EgZoneCtdBL();
        string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
        objEgZoneCtdBL.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
        objEgZoneCtdBL.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgZoneCtdBL.CtdMajorHead = ddlCTDMajorHead.SelectedValue;//== "0" ? "1" : ddlCTDMajorHead.SelectedValue;
        objEgZoneCtdBL.Zone = ddlZone.SelectedValue;
        // objEgZoneCtdBL.Circle = (ddlCircle.SelectedValue == "0" ? "0" : ddlCircle.SelectedValue);
        // objEgZoneCtdBL.Ward = (ddlWard.SelectedValue == "0" ? "0" : ddlWard.SelectedValue);

        dtHead = objEgZoneCtdBL.FillGrdCtdHeadWiseAmount();
        // Inserted data table in cache 
        // Now no need to again fill the datatable for binding to the repeater of budgethead
        Cache.Insert("dtBudgetHead", dtHead, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));

        // the getMajorHead variable shows the records by grouping them on the basis of majorhead and summing the amount 
        // we have in the last step of the Lambda expression use orderBy to order the fetched records
        // The grouping of records are done using Lanbda expression and not in the database itself

        var getMajorHead = from p in dtHead.AsEnumerable().Select(x => new
        {
            MajorHead = x.Field<string>("CtdHead").Substring(0, 4),
            Amount = x.Field<decimal>("CtdAmount")
        }).GroupBy(s => new { s.MajorHead }).Select(g => new
        {
            MajorHead = g.Key.MajorHead,
            Amount = g.Sum(x => x.Amount)
        }).OrderBy(s => s.MajorHead)
                           select p;

        // If the getMajorHead returns any value then only we bind it to the repeater
        if (getMajorHead.Count() > 0)
        {

            RptMajorHead.DataSource = getMajorHead;
            RptMajorHead.DataBind();
            divHeadAmountDetails.Visible = true;
            DivBudgetHead.Visible = false;
        }
        else // Else we show a error message to the user
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         "err_msg",
         "alert('Data not found!');",
         true);
            divHeadAmountDetails.Visible = false;
        }
    }
    // Created to null the repeater
    public void nullifyRepeater()
    {
        RptBudgetHead.DataSource = null;
        RptBudgetHead.DataBind();
        RptMajorHead.DataSource = null;
        RptMajorHead.DataBind();
    }

    // the dropdown of Ctd major heads
    protected void ddlCTDMajorHead_SelectIndexChanged(object sender, EventArgs e)
    {
        divHeadAmountDetails.Visible = false;
        DivBudgetHead.Visible = false;
        nullifyRepeater();
    }
    // dropdown of zone
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        divHeadAmountDetails.Visible = false;
        DivBudgetHead.Visible = false;
        nullifyRepeater();
    }

    // The main repeater which is used for displaying the amount corresponding to CTD heads and zones 
    protected void RptMajorHead_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        DataTable dt = (DataTable)Cache["dtBudgetHead"];  // The datatable contained in the cache

        // The variable BudgetHead is fetching the records for cached datatable all the Budgethead and amount 
        // Corresponding to them , on the click of the link button contained in main repeater (rptMajorHead)
        var BudgetHead = from p in dt.AsEnumerable().Select(x => new { MajorHead = x.Field<string>("CtdHead"), Amount = x.Field<decimal>("CtdAmount") }).Where(x => x.MajorHead.Substring(0, 4) == e.CommandArgument.ToString()) select p;
        if (BudgetHead.Count() > 0)
        {
            DivBudgetHead.Visible = true;
            RptBudgetHead.DataSource = BudgetHead;
            RptBudgetHead.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         "err_msg",
         "alert('Data not found!');",
         true);

        }

    }


}
