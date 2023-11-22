using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Caching;
using System.Linq;
using EgBL;
using System.Text;


public partial class WebPages_NewEgUserProfile : System.Web.UI.Page
{
    EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
    GenralFunction gf;
    static DataTable DtSearch = new DataTable();
    EgHomeBL objEgHomeBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            
            Response.Redirect("~\\logout.aspx");
        }
        if (!IsPostBack)
        {
            Page.SetFocus(ddldepartment);
            objUserProfileBL.PopulateDepartmentList(ddldepartment);
            //BindDepartment();
            BindUserProfile();
            ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
            btnMore.Enabled = false;
            DtSearch = objUserProfileBL.GetDeptList();
        }

    }
    /// <summary>
    /// Bind Department DropDown
    /// </summary>
    //public void BindDepartment()
    //{
    //    //if (HttpContext.Current.Cache["DepartmentList"] == null)
    //    //{
    //        objUserProfileBL.PopulateDepartmentList(ddldepartment);
    //    //}
    //    //else
    //    //{
    //    //    ddldepartment.DataSource = HttpContext.Current.Cache["DepartmentList"];
    //    //    ddldepartment.DataTextField = "deptnameEnglish";
    //    //    ddldepartment.DataValueField = "deptcode";
    //    //    ddldepartment.DataBind();
    //    //    ddldepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
    //    //}

    //}
    /// <summary>
    /// Show User Created Profile List 
    /// </summary>
    public void BindUserProfile()
    {

        objUserProfileBL.UserId = Convert.ToInt32(Session["userId"].ToString());
        objUserProfileBL.FillUserProfileRpt(rptuserprofile);
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        if (objUserProfileBL.DeptCode == 101)
        {
            string message = BuildMessage();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "myAlert('ध्यान दें:-','" + message + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "myAlert('ध्यान दें:-','" + message + "');", true);


        }

        //Cache.Insert("MajorHead", objUserProfileBL.fillDeptWiseMajorHeadList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
        DataTable HeadTable = objUserProfileBL.fillDeptWiseMajorHeadList(); // HttpContext.Current.Cache["MajorHead"] as DataTable;
        var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("Count") == "0");
        DataTable dt = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
        FillMajorHeadList(dt);
        lstbudgethead.Visible = false;
        txtDepartment.Text = ddldepartment.SelectedItem.Text;                  // txtdepartment selected text is ddlDepartment value when user selects dropdownlist
        if (ddldepartment.SelectedValue != "0")
        {
            btnMore.Enabled = true;
            BudgetSchema.Visible = true;
        }
        else
        {
            btnMore.Enabled = false;
            BudgetSchema.Visible = false;
        }
    }
    public void FillMajorHeadList(DataTable HeadTable)
    {
        tblSearch.Visible = false;
        ddlMajorHeadList.DataSource = HeadTable;
        ddlMajorHeadList.DataTextField = "MajorHeadName";
        ddlMajorHeadList.DataValueField = "MajorHeadCode";
        ddlMajorHeadList.DataBind();
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
    }
    protected void ddlMajorHeadList_SelectedIndexChanged(object sender, EventArgs e)
    {

        objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objUserProfileBL.majorheadcode = Convert.ToString(ddlMajorHeadList.SelectedValue);
        objUserProfileBL.UserType = Convert.ToInt32(Session["UserType"]);
        FillBudgetHeads();
    }
    /// <summary>
    /// Show DepartmentWise BudgetHead List in Listbox
    /// </summary>
    protected void FillBudgetHeads()
    {

        DataTable dt = objUserProfileBL.GetSchemaBudgetName();
        if (dt.Rows.Count > 0)
        {
            BudgetSchema.Visible = true;
            lstbudgethead.Visible = true;
            lstbudgethead.DataSource = dt;
            tblSearch.Visible = true;
            lstbudgethead.DataTextField = "SchemaName";
            lstbudgethead.DataValueField = "ScheCode";
            lstbudgethead.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('There is no schema in this BudgetHead.')", true);
            lstbudgethead.Visible = false;
            tblSearch.Visible = false;
        }
    }
    /// <summary>
    ///  Check Dublicate Schema in selected budgethead schema list
    /// </summary>
    /// <param name="Schema"></param>
    /// <param name="rid"></param>
    /// <returns></returns>
    private int checkSchemaExist(string Schema)  // to verify the ddo in gridview for duplicaci  
    {
        string[] spiltCode = Schema.ToString().Split('-');
        int x = 0;
        if (lstselectedbudget.Items.Count < 1)
        {
            ViewState["GroupCode"] = Convert.ToInt32(spiltCode[1]);
        }
        else if (lstselectedbudget.Items.Count > 0 && Convert.ToInt32(ViewState["GroupCode"]) == Convert.ToInt32(spiltCode[1]))
        {
            foreach (ListItem li in lstselectedbudget.Items)
            {
                string Schemaname = li.Value;
                if (Schemaname.Equals(Schema + "-" + ddldepartment.SelectedValue))
                {
                    x = 1;
                    break;
                }
            }
        }
        else
        {
            x = 2;
        }

        return x;
    }
    /// <summary>
    /// Insert and Update Profile 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        

        if (Page.IsValid)
        {
            BindInsertAndUpdateData();
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Entry is Not Valid.!')", true);
        }
        txtDepartment.Text = "Search Department By Name Or Code";
    }
    /// <summary>
    /// Function For Insert and Updating data
    /// </summary>
    public void BindInsertAndUpdateData()
    {
        List<EgUserProfileBL> listReocord = new List<EgUserProfileBL>();

        foreach (ListItem li in lstselectedbudget.Items)
        {
            objUserProfileBL = new EgUserProfileBL();
            objUserProfileBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string[] SplitBudgethead = li.Text.Split('-');
            objUserProfileBL.BudgetHead = SplitBudgethead[0].ToString() + SplitBudgethead[1].ToString() + SplitBudgethead[2].ToString() + SplitBudgethead[3].ToString() + SplitBudgethead[4].ToString();
            string[] Value = li.Value.Split('-');
            if (Convert.ToInt32(Value[0]) > 100000)
            {

                objUserProfileBL.ScheCode = 0;
            }
            else
            {
                objUserProfileBL.ScheCode = Convert.ToInt32(Value[0].ToString());
            }
            objUserProfileBL.DeptCode = Convert.ToInt32(Value[2].ToString());
            objUserProfileBL.ProfileName = txtProfileName.Text;

            if (btnSubmit.Text == "Submit")
            {
                gf = new GenralFunction();
              //  System.Data.SqlClient.SqlTransaction Trans = gf.Begintrans();
                int Maxpro = objUserProfileBL.GetMaxUserPro();
                objUserProfileBL.UserPro = Convert.ToInt32(Maxpro + 1);
               // gf.Endtrans();   // close Transaction  add line 14/1/2016
            }
            else
            {
                objUserProfileBL.UserPro = Convert.ToInt32(ViewState["UserPro"].ToString());
            }
            listReocord.Add(objUserProfileBL);
        }
        if (listReocord.Count > 0)
        {
            if (btnSubmit.Text == "Submit")
            {
                int i = objUserProfileBL.InsertUserProfile(listReocord);
                if (i == 1)
                {
                    BindValue();
                    //string msg = objUserProfileBL.RedirectToEChallan();
                    var ObjEncryptDecrypt = new EgEncryptDecrypt();
                    string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Profile={0}", objUserProfileBL.UserPro));
                    string UrlWithData = "EgEChallan.aspx?" + strURLWithData;
                    //string url = objUserProfileBL.UrlWithData;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save Successfully');window.location ='" + UrlWithData + "';", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Record not Saved.')", true);
                }
            }
            else
            {
                objUserProfileBL.deleteUserProfile(); // Delete Entry from table
                int i = objUserProfileBL.InsertUserProfile(listReocord);
                if (i == 1)
                {
                    BindValue();
                    btnSubmit.Text = "Submit";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Record Update Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Record not Updated.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please fill the records');", true);
        }

    }
    public void BindValue()
    {
        BindUserProfile();
        listBox2Values.Value = "";
        lstselectedbudget.Items.Clear();
        lstbudgethead.Items.Clear();
        BudgetSchema.Visible = false;
        txtProfileName.Text = "";
        ddldepartment.Enabled = true;
        ddldepartment.SelectedValue = "0";
        //ddlMajorHeadList.SelectedValue = "0";
        ddlMajorHeadList.Items.Clear();
        ddlMajorHeadList.Enabled = true;
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
        txtDepartment.Enabled = true;
    }

    protected void rptuserprofile_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.DataItem != null)
            {
                string pro = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Profile"));
            }
        }
    }
    /// <summary>
    /// Updaet Schema Profile
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptuserprofile_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {

            string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { '/', '-' });
            txtProfileName.Text = commandArgsAccept[1].ToString();
            objUserProfileBL.UserPro = Convert.ToInt32(commandArgsAccept[0].ToString());//it gives first ID
            ViewState["UserPro"] = objUserProfileBL.UserPro;
            //objUserProfileBL.DeptCode = Convert.ToInt32(commandArgsAccept[1].ToString());//it gives second ID
            objUserProfileBL.ProfileName = commandArgsAccept[1].ToString();//it gives second ID
            objUserProfileBL.UserId = Convert.ToInt32(Session["userId"].ToString());
            DataTable dt = objUserProfileBL.GetDeptSchemaNew();

            ViewState["Schema"] = dt;
            if (dt.Rows.Count > 0)
            {
                ViewState["GroupCode"] = dt.Rows[0][2].ToString().Split('-').GetValue(1);
                lstbudgethead.Items.Clear();
                lstselectedbudget.Items.Clear();
                //ddldepartment.SelectedValue = Convert.ToString(objUserProfileBL.DeptCode);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string listtext;
                    string listvalue;
                    if (dt.Rows[i][1].ToString() == "")
                    {
                        listtext = dt.Rows[i][0].ToString().Substring(0, 4) + '-' + dt.Rows[i][0].ToString().Substring(4, 2) + '-' + dt.Rows[i][0].ToString().Substring(6, 3) + '-' + dt.Rows[i][0].ToString().Substring(9, 2) + '-' + dt.Rows[i][0].ToString().Substring(11, 2) + "-" + dt.Rows[i][1].ToString();
                    }
                    else
                    {
                        listtext = dt.Rows[i][0].ToString().Substring(0, 4) + '-' + dt.Rows[i][0].ToString().Substring(4, 2) + '-' + dt.Rows[i][0].ToString().Substring(6, 3) + '-' + dt.Rows[i][0].ToString().Substring(9, 2) + '-' + dt.Rows[i][0].ToString().Substring(11, 2) + "-" + dt.Rows[i][1].ToString();
                        //  listtext =    dt.Rows[i][0].ToString() + "-" + dt.Rows[i][1].ToString();
                    }
                    if (dt.Rows[i][2].ToString() == "0")
                    {
                        listvalue = "-" + dt.Rows[i][3].ToString();
                    }
                    else
                    {
                        listvalue = dt.Rows[i][2].ToString() + "-" + dt.Rows[i][3].ToString();
                    }
                    lstselectedbudget.Items.Add(new ListItem(listtext, listvalue));
                }
                objUserProfileBL.DeptCode = int.Parse(dt.Rows[0][3].ToString());
                objUserProfileBL.majorheadcode = dt.Rows[0][0].ToString().Substring(0, 4);
                DataTable dtMajorHead = objUserProfileBL.fillDeptWiseMajorHeadList();
                ddlMajorHeadList.DataSource = dtMajorHead;
                ddlMajorHeadList.DataTextField = "MajorHeadName";
                ddlMajorHeadList.DataValueField = "MajorHeadCode";
                ddlMajorHeadList.DataBind();
                if (ddlMajorHeadList.Items.FindByValue(objUserProfileBL.majorheadcode.Trim()) != null && ddldepartment.Items.FindByValue(objUserProfileBL.DeptCode.ToString().Trim()) !=null)
                {
                    BudgetSchema.Visible = true;
                    lstselectedbudget.Visible = true;
                    btnSubmit.Text = "Update";
                    ddldepartment.SelectedValue = dt.Rows[0][3].ToString();
                    txtDepartment.Enabled = false;
                    ddldepartment.Enabled = false;

                    ddlMajorHeadList.SelectedValue = dt.Rows[0][0].ToString().Substring(0, 4);
                    txtDepartment.Text = ddldepartment.SelectedItem.Text;
                    txtDepartment.Enabled = false;
                    ddlMajorHeadList.Enabled = false;
                    btnMore.Enabled = false;
                    Header.Visible = true;
                    FillBudgetHeads();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Profile is Not Valid.!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Profile is Not Valid.!')", true);
            }
        }
        //if (e.CommandName == "Ac")
        //{
        //    LinkButton lnkDetail = (LinkButton)e.Item.FindControl("lnkDetail");
        //    if (lnkDetail.Text == "Active")
        //    {
        //        objUserProfileBL.ProfileActiveDeactive(e.CommandArgument.ToString(),"N");
        //        BindUserProfile();
        //        lnkDetail.Text = "InActive";
        //        // Do Something  
        //    }
        //    else
        //    {
        //        objUserProfileBL.ProfileActiveDeactive(e.CommandArgument.ToString(),"Y");
        //        BindUserProfile();
        //        lnkDetail.Text = "Active";
        //        // Do Something  
        //    }
        //}
        if (e.CommandName == "Ac")
        {
            Label Label1 = (Label)e.Item.FindControl("Label1");
            if (Label1.Text == "Y")
            {
                objUserProfileBL.ProfileActiveDeactive(e.CommandArgument.ToString(), "N");
                BindUserProfile();
            }
            else
            {
                objUserProfileBL.ProfileActiveDeactive(e.CommandArgument.ToString(), "Y");
                BindUserProfile();
            }
            
        }

    }
    /// <summary>
    /// Reset All control 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreset_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        ddldepartment.SelectedValue = "0";
        //ddlbudgethead.SelectedValue = "0";
        lstselectedbudget.Items.Clear();
        lstbudgethead.Items.Clear();
        ddldepartment.Enabled = true;
        txtProfileName.Text = "";
        BudgetSchema.Visible = false;
        ddlMajorHeadList.Enabled = true;
        btnMore.Enabled = true;
        ddlMajorHeadList.Items.Clear();
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
        btnMore.Enabled = false;
        txtDepartment.Text = "Search Department By Name Or Code";
        txtDepartment.Enabled = true;
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in lstbudgethead.Items)
        {
            //int majorHead = Convert.ToInt32(ddlMajorHeadList.SelectedValue);
            int majorHead = Convert.ToInt32(li.Text.Substring(0, 4));

            if (li.Selected == true)
            {
                if (majorHead >= 7999  && lstselectedbudget.Items.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Add only single BudgetHead in case of MajorHead 8000 and above.')", true);
                }
                else
                {
                    addlabel.Visible = false;
                    lstselectedbudget.Visible = true;
                    int y = checkSchemaExist(li.Value);
                    if (y == 1)
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Recored Already Exist ')", true);
                    else if (y == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('This BudgetHead/Purpose not allowed ')", true);
                    }
                    else
                    {
                        if (lstselectedbudget.Items.Count > 8)
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('You can add only 9 BudgetHead/Purpose.!')", true);
                        else
                        {
                            lstselectedbudget.Items.Add(new ListItem(li.Text, li.Value + "-" + ddldepartment.SelectedValue));
                            ddldepartment.Enabled = false;
                            ddlMajorHeadList.Enabled = false;
                            txtDepartment.Enabled = false;
                        }

                    }
                }
            }
        }
    }

    protected void btnprev_Click(object sender, EventArgs e)
    {
        addlabel.Visible = false;
        lstselectedbudget.Visible = true;
        lstselectedbudget.Items.Remove(lstselectedbudget.SelectedItem);
        if (lstselectedbudget.Items.Count < 1)
        {
            addlabel.Visible = true;
            lstselectedbudget.Visible = false;
            ddldepartment.Enabled = true;
            ddlMajorHeadList.Enabled = true;
            txtDepartment.Enabled = true;
        }
    }

    /// <summary>
    /// displaying more MajorHeads
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMore_Click(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedValue != "0")
        {
            objUserProfileBL = new EgUserProfileBL();
            objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
            DataTable HeadTable = objUserProfileBL.fillDeptWiseMajorHeadList();
            ddlMajorHeadList.DataSource = HeadTable; // HttpContext.Current.Cache["MajorHead"] as DataTable;
            ddlMajorHeadList.DataTextField = "MajorHeadName";
            ddlMajorHeadList.DataValueField = "MajorHeadCode";
            ddlMajorHeadList.DataBind();
            ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
            lstselectedbudget.Items.Clear();
            lstbudgethead.Items.Clear();
            ddlMajorHeadList.Enabled = true;
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCountries(string prefixText, int count)                //  Method for txtDepartment AjaxAutoCompleteExtender By Rachit on 20 Nov 13
    {


        string Text = prefixText.ToUpper().ToString();


        List<string> LST = new List<string>();


        var query = from t in DtSearch.AsEnumerable()
                    orderby t.Field<string>("DeptNameEnglish"), t.Field<Int32>("DeptCode")// getting all the value from static datatable filled on pageload(DtSearch)
                    where (t.Field<string>("DeptNameEnglish").ToUpper().Contains(Text))
                    select new
                   {
                       DeptNameEnglish = t.Field<string>("DeptNameEnglish"),
                   };
        foreach (var i in query.Take(10))



        //taking top 5 matching records from the LINQ variable
        {
            LST.Add(i.DeptNameEnglish.ToString().Trim());

        };

        if (LST.Count > 0)
            return LST.OrderBy(s => s).ToList();

        else
            LST = new List<string> { "No record Found!" };                      // if list count is 0 than add message to the list of no record found
        return LST;
        //  }
    }

  protected void txtDepartment_TextChanged(object sender, EventArgs e)
    {
        int value;
        if (txtDepartment.Text == "No record Found!")                                  // if no record found and user tries to select the "No Record Found!" 
        {
            txtDepartment.Text = "Please type correct department!";
            ddldepartment.SelectedValue = "0";
        }
        else
        {
            var getdeptcode = txtDepartment.Text;                                                 //getting value in variable getdeptcode of txtDepartments
            if ((getdeptcode.IndexOf('-') > 0) || (int.TryParse(getdeptcode ,out value)))
            {
                Random r = new Random();
                int Getcode = Convert.ToInt32(int.TryParse(getdeptcode.Split('-')[0],out value) ? getdeptcode.Split('-')[0] : getdeptcode = r.Next().ToString() );
                var query = from i in ddldepartment.Items.Cast<ListItem>()
                            where ((ListItem)i).Text.Contains(Getcode.ToString())
                            select i;

                if (query.Count() > 0)
                {
                    ddldepartment.SelectedValue = Getcode.ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PopupScript", "alert('Department does not exists.')", true);
                    return;
                }
                objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                Cache.Insert("MajorHead", objUserProfileBL.fillDeptWiseMajorHeadList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
                DataTable HeadTable = HttpContext.Current.Cache["MajorHead"] as DataTable;
                var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("Count") == "0");
                DataTable dt = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
                FillMajorHeadList(dt);
                lstbudgethead.Visible = false;
                //set ddlDepartment as disabled
                // ""  txtDepartment
                if (ddldepartment.SelectedValue != "0")
                {
                    btnMore.Enabled = true;
                    BudgetSchema.Visible = true;
                }
                else
                {
                    btnMore.Enabled = false;
                    BudgetSchema.Visible = false;
                }
            }
        }


    }

    // Add new massage format for 101 department
    public string BuildMessage()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("राज्य सरकार के निर्देशानुसार राज्य बीमा एवं सामान्य प्रावधायी निधि योजना से संबंधित जमाओं हेतु प्रक्रिया में परिवर्तन किया गया है | यदि आप निम्न में से किसी एक प्रकार की जमा करवाना चाहते है तो ध्यान दें- ");
        builder.Append("  ").Append("\\r\\n");
        builder.Append("\\r\\n");
        builder.Append("1.	राज्य बीमा प्रीमियम कटौती ").Append("\\n");
        builder.Append("2.	राज्य बीमा ऐरियर प्रीमियम कटौती  ").Append("\\n");
        builder.Append("3.	राज्य बीमा ऋण का पुनर्भुगतान ").Append("\\r\\n");
        builder.Append("4.	राज्य बीमा ऋण पर अदा कीये जाने वाले ब्याज का पुनर्भुगतान ").Append("\\r\\n");
        builder.Append("5.	सामान्य प्रावधायी निधि की कटौती  ").Append("\\r\\n");
        builder.Append("6.	सामान्य प्रावधायी निधि की एरियर कटौती ").Append("\\r\\n");
        builder.Append("7.	सामान्य प्रावधायी निधि के अस्थायी आहरण का पुनर्भुगतान ").Append("\\r\\n");
        builder.Append("  ").Append("\\r\\n");
        builder.Append("उक्त समस्त जमाओं हेतु अब जमाकर्ता को ई-ग्रास के माद्यम से राशि जमा करवाने से पूर्व राज्य बीमा एवं प्रावधायी निधि विभाग के ऑनलाईन पोर्टल www.sipfportal.rajasthan.gov.in पर यथोचित रूप से लॉगिन कर आवश्यक विवरण अनिवार्य रूप से दर्ज करना होगा | वर्तमान में यह सुविधा आहरण एवं वितरण अधिकारी के स्तर  पर उपलब्ध है | वे विभाग जो राजस्थान सरकार के कोष से प्रत्यक्ष रूप से जुड़े हुये नहीं है तथा जिनकी सेल्फ ट्रेजरी है अथवा कैश मद से जुड़े हैं उन्हें उक्त प्रक्रियाओं हेतु आवश्यक रूप से वांछित विवरण दर्ज करना होगा |");

        builder.Append("  ").Append("\\r\\n");
        builder.Append("\\r\\n");
        builder.Append("दर्ज किये गये विवरण के अनुरूप ही चयन किये गये ऑनलाईन/ऑफलाईन प्रकार के अनुसार ही ई-ग्रास पर भुगतान का लिंक उपलब्ध होगा | राज्य बीमा एवं प्रावधायी निधि विभाग के पोर्टल पर भुगतान के लिंक द्वारा ई-ग्रास पर पूर्व की भांति भुगतान प्रक्रिया संपादित होगी ");
        builder.Append("  ").Append("\\r\\n");
        builder.Append("\\r\\n");

        builder.Append("स्पष्ट है कि ई-ग्रास पर उक्त प्रक्रियाओं हेतु बिना विभाग के पोर्टल पर विवरण दर्ज किये सीधे संबंधित राशि जमा नहीं की जा सकेगी | ");

        builder.Append("  ").Append("\\r\\n");
        builder.Append("जमाकर्ताओं से आग्रह है कि किसी प्रकार की कठिनाई उत्पन्न होने पर विभागीय हेल्पडेस्क एवं टोलफ्री नं -18001806268 पर संपर्क करें | उक्त प्रक्रिया की जानकारी विभागीय वेबसाईट www.sipf.rajasthan.gov.in के DDO’s Corner पर देखी जा सकती है |     ").Append("\\r\\n");
        // Get a reference to the StringBuilder's buffer content.
        return builder.ToString();

    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["_ViewState"];
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        Session["_ViewState"] = viewState;
    }
}