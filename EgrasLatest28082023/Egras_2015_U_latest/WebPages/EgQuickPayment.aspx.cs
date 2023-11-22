using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using EgBL;
using System.Web.Caching;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class WebPages_EgQuickPayment : System.Web.UI.Page
{
    EgQuickPaymentBL _objEgQuickPaymentBl;
    EgUserProfileBL _objEgUserProfileBl;
    DataTable _dtRecordsSearch;
    DataTable dtMajorHeads;
    static DataTable deptFill;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            tdTxt.Visible = false;                  // sets txtsearch as well as txtautocompletedepartments visibility to false
            btntxtSearch.Visible = false;           // hide search button on page load
            _objEgQuickPaymentBl = new EgQuickPaymentBL();
            deptFill = new DataTable();
            // fill datatable of departments for autocomplete
            deptFill = _objEgQuickPaymentBl.GetDeptList();
            if (Session["UserType"].ToString().Trim() == "9")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "GuestPopUp();", true);
            }
        }
    }

    // Button to search the Departments 
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //trselectedList.Visible = false;
        //lstselectedList.Items.Clear();
        try
        {
            _objEgQuickPaymentBl = new EgQuickPaymentBL();
            _dtRecordsSearch = new DataTable();
            _objEgQuickPaymentBl.type = rblSearchCriteria.SelectedValue;
            trPurposeList.Visible = false;
            //trPayment.Visible = false;

            // browser will choose default of Display Property

            DivAllPurpose.Style.Add("display", "");

            switch (rblSearchCriteria.SelectedValue)
            {
                case "P":
                    _objEgQuickPaymentBl.SearchText = txtSearch.Text.Trim();
                    break;
                case "B":
                    if ((txtSearch.Text.Length) < 4)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter atleast majorHead!');", true);
                    }
                    else
                        _objEgQuickPaymentBl.SearchText = txtSearch.Text.Trim();
                    break;
                case "D":
                    string getdept = txtAutoCompleteDepartments.Text.Trim();
                    _objEgQuickPaymentBl.SearchText = getdept.Contains('-') ? getdept.Split('-').GetValue(1).ToString() : getdept;
                    break;
            }

            // this is used to bind rblDepatments
            _dtRecordsSearch = _objEgQuickPaymentBl.GetSearchData();

            if (_dtRecordsSearch.Rows.Count > 0)
            {
                //  Clears all previous MajorHeads
                rblMajorhead.Items.Clear();
                
                
                trDept.Visible = true;
                rblDepartments.DataSource = _dtRecordsSearch;
                rblDepartments.DataTextField = "DeptNameEnglish";
                rblDepartments.DataValueField = "DeptCode";
                rblDepartments.DataBind();
            }
            else
            {

                trDept.Visible = false;
                //trPayment.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No Matches Found !');", true);

            }
        }
        catch (Exception ex)
        {

            trDept.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + ex.Message + "');", true);
        }
    }
      //----------Add data Table 12 May 2016

      public DataTable CreateDataSource()
         {
                DataTable dt = new DataTable();
                dt.Columns.Add("schemaname", typeof(string));
                dt.Columns.Add("schecode", typeof(string));
                 return dt;
         }

      public void AddRow(string schemaname, string schecode ,DataTable dt)
      {
          dt.Rows.Add(new object[] { schemaname, schecode });
          dt.AcceptChanges();
      }
    protected void rblDepartments_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMajorHeads();
        DataTable dt=CreateDataSource();
        foreach (ListItem li in lstPurposeHeads.Items)
        {
            if (li.Text.Substring(0, 17).Replace("-", "") == txtSearch.Text)
            {
                AddRow(li.Text,li.Value,dt);
            }
        }
        //if (dt.Rows.Count > 0)
        //{
        //    trselectedList.Style.Add("display", "");
        //    lstselectedList.Items.Clear();
        //    lstselectedList.DataSource = dt;
        //    lstselectedList.DataTextField = "schemaname";
        //    lstselectedList.DataValueField = "schecode";
        //    lstselectedList.DataBind();
        //    trselectedList.Visible = true;
        //}
        
    }
    // This method is used to fill majorheads based on departments,  detailed head base on majorhead and department

    private void FillMajorHeads()
    {
        dtMajorHeads = new DataTable();
        _objEgQuickPaymentBl = new EgQuickPaymentBL { DeptCode = Convert.ToInt32(rblDepartments.SelectedValue) };
        Cache.Insert("MajorHead", _objEgQuickPaymentBl.QuickPayDeptWiseMajorHeadList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);

        // fill datatable from which we bind rblMajorHeads

        DataTable HeadTable = HttpContext.Current.Cache["MajorHead"] as DataTable;

        // in case of BudgetHead

        switch (rblSearchCriteria.SelectedValue)
        {
            case "B":

                if (HeadTable != null)
                {
                    // Extracted value corresponding to the major head we entered  currently not in use

                    // var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("MajorHeadCode") == txtSearch.Text.ToString().Trim().Substring(0,4) && t.Field<string>("Count") == "0" && t.Field<string>("MajorHeadName").StartsWith(txtSearch.Text.ToString().Trim().Substring(0,4)));

                    // Extracted values where count = 0 for binding to rblMajorHeads but user cannot 
                    // Select it ( As told by Abhay sir in order to enable user to see what majorHeads
                    // are available in correspondance to the following department )

                    var param = HeadTable.AsEnumerable().Where(t => t.Field<string>("count") == "0");


                    dtMajorHeads = param.Any() ? param.CopyToDataTable() : HeadTable.Clone();
                }
                if (dtMajorHeads.Rows.Count > 0)
                {
                    // From dtMajorHeads Only that value will be selected which we entered in case of txtsearch

                    var query = from r in dtMajorHeads.AsEnumerable()
                                where r.Field<string>("MajorHeadCode") == txtSearch.Text.ToString().Trim().Substring(0, 4)
                                select r.Field<string>("MajorHeadCode");

                    if (query.Count() > 0)
                    {
                        foreach (string i in query)
                        {
                            string selectrblMajorHead = i;

                            rblMajorhead.Items.Clear();
                            rblMajorhead.DataSource = dtMajorHeads;
                            rblMajorhead.DataTextField = "MajorHeadName";
                            rblMajorhead.DataValueField = "MajorHeadCode";
                            rblMajorhead.DataBind();
                            // Selecting rblMajorHead value Automatically when user selects Department
                            rblMajorhead.SelectedValue = selectrblMajorHead;
                            rblMajorhead.Enabled = false;

                        }


                        // if rbl MajorHead value is equal to value in textsearch
                        // than we fill the dtAllpurpose which is used for chkPurpose

                        if (rblMajorhead.SelectedValue == txtSearch.Text.ToString().Trim().Substring(0, 4))
                        {
                            _objEgUserProfileBl = new EgUserProfileBL
                            {

                                majorheadcode = rblMajorhead.SelectedValue.Trim(),

                                DeptCode = Convert.ToInt32(rblDepartments.SelectedValue)

                            };

                            // fill the Budget-Head

                            DataTable dtAllPurpose = _objEgUserProfileBl.GetSchemaBudgetName();
                            if (dtAllPurpose.Rows.Count > 0)
                            {
                                var query1 = from dt in dtAllPurpose.AsEnumerable() where (dt.Field<string>("Schemaname").Replace("-", string.Empty)).Substring(0, txtSearch.Text.Length) == txtSearch.Text select dt;
                                DataTable dtBudgetPurposeTemp = new DataTable();
                                dtBudgetPurposeTemp.Columns.Add("schemaname");
                                dtBudgetPurposeTemp.Columns.Add("schecode");
                                foreach (DataRow dr in query1)
                                {
                                    dtBudgetPurposeTemp.Rows.Add(dr.ItemArray);
                                }
                                lstPurposeHeads.Items.Clear();
                                lstPurposeHeads.DataTextField = "schemaname";
                                lstPurposeHeads.DataValueField = "schecode";
                                lstPurposeHeads.DataSource = dtBudgetPurposeTemp;
                               
                                lstPurposeHeads.DataBind();
 
                                
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "HidePaymentSection();", true);      // hides trPurposelist, trPayment
                                //trPayment.Visible = true;
                                trPurposeList.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No BudgetHead found corresponding to MajorHead');", true);
                                trPurposeList.Visible = false;

                            }

                        }
                        else
                        {
                            CommonMessage();
                        }
                    }
                    else
                    {
                        CommonMessage();
                    }
                }

                else
                {
                    CommonMessage();
                }

                break;

            // In case of purpose

            case "P":

                if (HeadTable != null)
                {
                    var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("count") == "0");
                    dtMajorHeads = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
                }
                if (dtMajorHeads.Rows.Count > 0)
                {
                    rblMajorhead.DataSource = dtMajorHeads;
                    rblMajorhead.DataTextField = "MajorHeadName";
                    rblMajorhead.DataValueField = "MajorHeadCode";
                    rblMajorhead.DataBind();
                    rblMajorhead.Enabled = true;
                    trPurposeList.Visible = false;
                    //trPayment.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No BudgetHeads Found corresponding to the selected Department ');", true);
                    trDept.Visible = false;
                }

                break;

            // In Case  Department rbl is selected 

            case "D":

                if (HeadTable != null)
                {
                    var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("count") == "0");
                    dtMajorHeads = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
                }
                if (dtMajorHeads.Rows.Count > 0)
                {
                    rblMajorhead.Items.Clear();
                    rblMajorhead.DataSource = dtMajorHeads;
                    rblMajorhead.DataTextField = "MajorHeadName";
                    rblMajorhead.DataValueField = "MajorHeadCode";
                    rblMajorhead.DataBind();
                    rblMajorhead.Enabled = true;
                    trPurposeList.Visible = false;
                    //trPayment.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No MajorHeads Found corresponding to the selected Department ');", true);
                    rblMajorhead.Items.Clear();
                    trDept.Visible = true;
                    trPurposeList.Visible = false;
                    //trPayment.Visible = false;
                }
                break;

        }

    }
    private void FillPurposeCheckList()
    {
        trPurposeList.Style.Add("display", "");                                         // browser will choose default of Display Property
        //trPayment.Style.Add("display", "");                                             // browser will choose default of Display Property
        lstPurposeHeads.DataTextField = "schemaname";
        lstPurposeHeads.DataValueField = "schecode";
        lstPurposeHeads.DataBind();
    }

    private void CommonMessage()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No Majorhead and BudgetHead found corresponding to Department ');", true);
        rblMajorhead.Items.Clear();
        trPurposeList.Visible = false;
    }

    // payment Button 
    //protected void btnPayment_Click(object sender, EventArgs e)                        
    //{
    //    try
    //    {
    //        var SelectedPurpose = from i in lstPurposeHeads.Items.Cast<ListItem>()
    //                              where i.Selected == true
    //                              select i;

    //        int SelectedPurposeCount = SelectedPurpose.Count<ListItem>();

    //        if (SelectedPurposeCount > 0)
    //        {
    //            CreateSessionTable();

    //            foreach (var Item in SelectedPurpose)
    //            {
    //                DataRow dr = DtSession.NewRow();
    //                string[] schecodeArray = Item.Value.Split('-');
    //                string groupCode = schecodeArray[1].ToString().Trim();
    //                string schemaName = Item.Text.Substring(18).ToString().Trim();
    //                string budgetHead = Item.Text.ToString().Trim().Substring(0, 17).Replace("-", "");
    //                //  string ScheCode = BudgetHead + '-' + Item.Value;
    //                string scheCode = budgetHead + '-' + Item.Value.Split('-').GetValue(0) + '-' + rblDepartments.SelectedItem.Value;

    //                dr["Schemaname"] = schemaName;
    //                dr["Schecode"] = scheCode;
    //                dr["DeptName"] = rblDepartments.SelectedItem.Text.ToString().Trim();
    //                dr["DeptCode"] = rblDepartments.SelectedItem.Value;
    //                dr["GroupCode"] = groupCode;
    //                DtSession.Rows.Add(dr);
    //            }
    //            // session Table same as EgGuestProfile
    //            Session["mydatatable"] = DtSession;                               

    //            _objEncrypt = new EgEncryptDecrypt();
    //            string encryptedData = _objEncrypt.Encrypt(string.Format("Guest={0}", "guest"));
    //            string strUrlWithData = "EgEChallan.aspx?" + encryptedData.ToString();

    //            // false is used to not to stop the execution of the current thread.
    //            Response.Redirect(strUrlWithData, false);                          

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Select a Purpose !');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + ex.Message + "');", true);
    //    }
    //}

    //private void CreateSessionTable()
    //{
    //    DtSession = new DataTable();
    //    DtSession.Columns.Add("Schemaname", typeof(string));
    //    DtSession.Columns.Add("Schecode", typeof(string));
    //    DtSession.Columns.Add("DeptName", typeof(string));
    //    DtSession.Columns.Add("DeptCode", typeof(int));
    //    DtSession.Columns.Add("GroupCode", typeof(string));
    //}

    /*
        private void ShowChildSearchDiv()
        {
       
            DivAllPurpose.Style.Add("display", "none");
        }
    */

    /*
        private void ShowPurposeHeadDiv()
        {
            DivAllPurpose.Style.Add("display", "block");

        }
    */

    //private void HideAllPAgeContents()
    //{
    //    trDept.Visible = false;
    //    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "HidePaymentSection();", true);
    //}

    //  Method for txtAutocompleteDepartment AjaxAutoCompleteExtender Added By Rachit on 30 jan 14

    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod]
    public static List<string> GetDeptList(string prefixText, int count)
    {


        string text = prefixText.ToUpper();

        // getting all the departments from static datatable filled on pageload(DtSearch)

        var query = from t in deptFill.AsEnumerable()
                    orderby t.Field<Int32>("DeptCode"), t.Field<string>("DeptNameEnglish")
                    where (t.Field<string>("DeptNameEnglish").ToUpper().Contains(text))
                    select new
                    {
                        DeptNameEnglish = t.Field<string>("DeptNameEnglish"),
                    };

        List<string> lst = query.Take(10).Select(i => i.DeptNameEnglish.ToString().Trim()).ToList();

        if (lst.Count > 0)
            return lst.OrderBy(s => s).ToList();

        // if list count is 0 than add message to the list of No record found

        lst = new List<string> { "No record Found!" };
        return lst;

    }

    // The radiobutton for search criteria

    protected void rblSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

        switch (rblSearchCriteria.SelectedValue)
        {
            case "D":
                ClearAllDetails();
                txtSearch.Visible = false;
                txtAutoCompleteDepartments.Visible = true;
                RequiredFieldValidator1.Visible = false;
                RequiredFieldValidator5.Visible = true;
                break;



            case "P":
                ClearAllDetails();
                RequiredFieldValidator5.Visible = false;
                RequiredFieldValidator1.Visible = true;
                break;



            case "B":
                ClearAllDetails();
                RequiredFieldValidator5.Visible = false;
                RequiredFieldValidator1.Visible = true;
                break;

        }
    }
    void ClearAllDetails()               // This method clears all the div's , textboxes when we select a search Criteria (Rbl)
    {
        txtSearch.Visible = true;
        txtAutoCompleteDepartments.Visible = false;
        tdTxt.Visible = true;
        btntxtSearch.Visible = true;
        trDept.Visible = false;
        trPurposeList.Visible = false;
        //trPayment.Visible = false;
        txtSearch.Text = "";
        txtAutoCompleteDepartments.Text = "";
        //trselectedList.Visible = false;
    }

    protected void rblMajorhead_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstPurposeHeads.Items.Clear();

        _objEgUserProfileBl = new EgUserProfileBL
        {

            majorheadcode = rblMajorhead.SelectedValue.Trim(),

            DeptCode = Convert.ToInt32(rblDepartments.SelectedValue)

        };
        DataTable dtAllPurpose = _objEgUserProfileBl.GetSchemaBudgetName();          // fills chkBudgetHeads trPurposeList
        if (dtAllPurpose.Rows.Count > 0)
        {
            lstPurposeHeads.DataSource = dtAllPurpose;
            FillPurposeCheckList();

            DivAllPurpose.Style.Add("display", "");                                // browser will choose default of Display Property
            //trPayment.Visible = true;
            trPurposeList.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('No BudgetHead  Found');", true);
            //trPayment.Visible = false;
            trPurposeList.Visible = false;
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "HidePaymentSection();", true);
        }
    }
}
