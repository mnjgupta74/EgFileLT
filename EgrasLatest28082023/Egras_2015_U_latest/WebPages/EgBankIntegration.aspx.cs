using System;
using System.Data;
using System.Web.UI;
using EgBL;


public partial class WebPages_EgBankIntegration : System.Web.UI.Page
{
    EgIntegrationChallanBL objEchallan;
    DataTable schemaAmtTable;
    public static UserControl Umenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        
        if (!IsPostBack)
        {
            if (Session["EgEchallanData"] != null)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["EgEchallanData"];
                if (dt.Rows[0]["pType"].ToString() == "N")
                {
                    EgCheckBudgetHead objEgFillBank = new EgCheckBudgetHead();
                    rblBanks.DataSource = objEgFillBank.FillBank1();
                    rblBanks.DataTextField = "BankName";
                    rblBanks.DataValueField = "BSRCode";
                    rblBanks.DataBind();
                    rblBanks.SelectedValue = "0006326";
                    Umenu = Page.Master.FindControl("vmenu1") as UserControl;
                    Umenu.Visible = false;
                }
                else if (dt.Rows[0]["pType"].ToString() == "M")
                {
                    EgCheckBudgetHead objEgFillBank = new EgCheckBudgetHead();
                    objEgFillBank.TreasuryCode = dt.Rows[0]["location"].ToString();

                    rblBanks.DataSource = objEgFillBank.FillManualBanks();
                    rblBanks.DataTextField = "BANKNAME";
                    rblBanks.DataValueField = "BSRCode";
                    rblBanks.SelectedIndex = 0;
                    rblBanks.DataBind();
                    Umenu = Page.Master.FindControl("vmenu1") as UserControl;
                    Umenu.Visible = false;
                }
            }
            else
            {
                //EgErrorHandller obj = new EgErrorHandller();
                //obj.InsertError("Session[EgEchallanData] is null|" + "PageLoad");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Session Expired. Try Again');", true);
                 Response.Redirect("~\\LoginAgain.aspx");
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["EgEchallanData"] != null)
        {
            objEchallan = new EgIntegrationChallanBL();
            DataTable dt = new DataTable();
            dt = (DataTable)Session["EgEchallanData"];
            objEchallan.TypeofPayment = dt.Rows[0]["pType"].ToString();
            // string Majorhead = dt.Rows[0]["schecode"].ToString().Split('-').GetValue(0).ToString().Substring(0, 4).Trim();
            objEchallan.OfficeName = Convert.ToInt32(dt.Rows[0]["officeCode"]);
            //objEChallan.PanNumber = dt.Rows[0]["PanNumber"].ToString();
            objEchallan.Location = dt.Rows[0]["location"].ToString();
            objEchallan.FullName = dt.Rows[0]["fullName"].ToString();
            objEchallan.ChallanYear = objEchallan.ChallanYear = dt.Rows[0]["ChallanYear"].ToString();
            objEchallan.Address = dt.Rows[0]["address"].ToString();
            //objEchallan.CityName = dt.Rows[0]["city"].ToString();
            objEchallan.City = dt.Rows[0]["city"].ToString(); //19;//
            objEchallan.PINCode = dt.Rows[0]["pincode"].ToString();
            objEchallan.DeductCommission = double.Parse(dt.Rows[0]["DeductCommission"].ToString());
            objEchallan.TotalAmount = dt.Rows[0]["TotalAmount"].ToString();
            objEchallan.BankName = rblBanks.SelectedValue;
            objEchallan.Mcode = Convert.ToInt32(dt.Rows[0]["merchantCode"]);
            objEchallan.AUIN = Convert.ToString(dt.Rows[0]["RefNo"]);
            objEchallan.Identity = dt.Rows[0]["identity"].ToString();
            objEchallan.Remark = dt.Rows[0]["remarks"].ToString();
            objEchallan.ChallanFromMonth = Convert.ToDateTime(dt.Rows[0]["fromDate"]);
            objEchallan.ChallanToMonth = Convert.ToDateTime(dt.Rows[0]["toDate"]);
            objEchallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objEchallan.filler = dt.Rows[0]["filler"].ToString();
            objEchallan.Zone = dt.Rows[0]["Zone"].ToString();
            objEchallan.Circle = dt.Rows[0]["Circle"].ToString();
            objEchallan.Ward = dt.Rows[0]["Ward"].ToString();
            objEchallan.PDacc = Convert.ToInt32(dt.Rows[0]["PdAcc"].ToString());


            if (objEchallan.filler != "A" && objEchallan.PDacc == 0)
            {

                objEchallan.DivCode = objEchallan.GetDivisionCode();
            }
            else
                objEchallan.filler = "0";


            if (Convert.ToBoolean(Session["IsDOITRedirect"]))//DOIT sandeep
                objEchallan.Profile = -1;

            int output = 0;
            objEchallan.dtSchema = AmountSave(dt);
            output = objEchallan.InsertChallan();
            if (output != 0)
            {
                Session["GrnNumber"] = Convert.ToString(output);
                Session["EgEchallanData"] = null;
                dt.Dispose();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Record Saved Successfully.');", true);
                EgEncryptDecrypt ObjEncrytDecrypt = new EgEncryptDecrypt();
                if (objEchallan.TypeofPayment == "N")
                    Response.Redirect("EgEChallanView.aspx");
                else if (objEchallan.TypeofPayment == "M")
                {
                    Response.Redirect("~/webpages/reports/EgManualChallan.aspx");
                    //if (rblBanks.SelectedValue.ToString().Substring(7, 2) == "-1")
                    //{
                    //    string strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("GRN={0}", Session["GrnNumber"].ToString().Trim()));
                    //    Response.Redirect("~/webpages/reports/EgEChallanViewRptAnyWhere.aspx?" + strURLWithData.ToString());
                    //}
                    //else
                    //{
                    //    string strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("GRN={0}", Session["GrnNumber"].ToString().Trim()));
                    //    Response.Redirect("~/webpages/reports/EgEchallanViewRptnew.aspx?" + strURLWithData.ToString());
                    //}

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Record Not Saved.');", true);
            }
        }
        else
        {
            //EgErrorHandller obj = new EgErrorHandller();
            //obj.InsertError("Session[EgEchallanData] is null|" + "ButtonClick");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Session Expired. Try Again');", true);
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }
    public DataTable AmountSave(DataTable dt)
    {

        CreateSchemaAmtTable();
        DataRow schemarow;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            schemarow = schemaAmtTable.NewRow();
            string[] BudgetHeadName = dt.Rows[i][0].ToString().Split('-');
            //schemarow["BudgetHead"] = BudgetHeadName[0].ToString();
            schemarow["BudgetHead"] = BudgetHeadName[0].ToString();
            if (Session["UserId"].ToString().Trim() == "73")
            {
                if (int.Parse(BudgetHeadName[1].ToString()) > 100000)
                {
                    schemarow["ScheCode"] = 0;
                }
                else
                {
                    schemarow["ScheCode"] = int.Parse(BudgetHeadName[1].ToString());
                }
            }
            else
            {

                schemarow["ScheCode"] = int.Parse(BudgetHeadName[1].ToString());
            }

            schemarow["DeptCode"] = Convert.ToInt32(BudgetHeadName[2].ToString());
            schemarow["amount"] = Convert.ToDouble(dt.Rows[i][1].ToString());
            schemarow["UserId"] = Convert.ToInt32(Session["UserId"].ToString());
            schemaAmtTable.Rows.Add(schemarow);
            schemaAmtTable.AcceptChanges();

        }
        return schemaAmtTable;
    }
    private void CreateSchemaAmtTable()
    {
        schemaAmtTable = new DataTable();
     
        schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
        schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));
    }
}

