using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EgBL;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Linq;

public partial class WebPages_EgGuestProfile : System.Web.UI.Page
{
    EgGuestProfileBL objEgGuestProfile = new EgGuestProfileBL();
    EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
    DataTable dtValues = new DataTable();
    DataTable schemadt = new DataTable();
    EgEncryptDecrypt ObjEncryptDecrypt;
    static DataTable DtSearch = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            EgUserProfileBL objEgUserProfileBL = new EgUserProfileBL();
            objEgUserProfileBL.PopulateDepartmentList(ddlDeptPopup);
            objEgGuestProfile.PopulateDepartmentList(ddldepartment);
            ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
            btnMore.Enabled = false;
            DtSearch = objUserProfileBL.GetDeptList();
        }
    }

    /// <summary>
    /// creates DataTable
    /// </summary>
    /// <returns></returns>
    public DataTable CreateDataSource()
    {
        schemadt.Columns.Add("Schemaname", typeof(string));
        schemadt.Columns.Add("Schececode", typeof(string));
        schemadt.Columns.Add("DeptName", typeof(string));
        schemadt.Columns.Add("DeptCode", typeof(int));
        schemadt.Columns.Add("GroupCode", typeof(string));
        return schemadt;
    }

    /// <summary>
    /// adds row in DataTable
    /// </summary>
    /// <param name="schemaname"></param>
    /// <param name="Schececode"></param>
    /// <param name="dt"></param>
    public void AddRow(string schemaname, string Schececode, string groupCode, DataTable dt)
    {
        string[] word = ddldepartment.SelectedItem.Text.Split('-');
        dt.Rows.Add(new object[] { schemaname, Schececode, word[1].ToString(), word[0], groupCode });
        dt.AcceptChanges();
    }

    /// <summary>
    /// bind all Schemas according to department
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objUserProfileBL = new EgUserProfileBL();
        objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        if (objUserProfileBL.DeptCode == 101)
        {
            string message = BuildMessage();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "myAlert('ध्यान दें:-','" + message + "');", true);

        }  
        //Cache.Insert("MajorHead", objUserProfileBL.fillDeptWiseMajorHeadList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
        DataTable HeadTable = objUserProfileBL.fillDeptWiseMajorHeadList(); // HttpContext.Current.Cache["MajorHead"] as DataTable;
        var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("Count") == "0");
        DataTable dt = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
        FillMajorHeadList(dt);
        txtDepartment.Text = ddldepartment.SelectedItem.Text;
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
    protected void ddlMajorHeadList_SelectedIndexChanged(object sender, EventArgs e)
    {
        objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objUserProfileBL.majorheadcode = Convert.ToString(ddlMajorHeadList.SelectedValue);
        objUserProfileBL.UserType = Convert.ToInt32(Session["UserType"]);
        FillBudgetHeads();
    }
    protected void FillBudgetHeads()
    {
        DataTable dt = objUserProfileBL.GetSchemaBudgetName();
        if (dt.Rows.Count > 0)
        {
            BudgetSchema.Visible = true;
            lstbudgethead.Visible = true;
            lstbudgethead.DataSource = dt;

            lstbudgethead.DataTextField = "SchemaName";
            lstbudgethead.DataValueField = "ScheCode";
            lstbudgethead.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('There is no purpose in this BudgetHead.')", true);
            lstbudgethead.Visible = false;
        }
    }
    public void FillMajorHeadList(DataTable HeadTable)
    {
        ddlMajorHeadList.DataSource = HeadTable;
        ddlMajorHeadList.DataTextField = "MajorHeadName";
        ddlMajorHeadList.DataValueField = "MajorHeadCode";
        ddlMajorHeadList.DataBind();
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));

        //objUserProfileBL.DeptCode = Convert.ToInt16(ddldepartment.SelectedValue);
        //objUserProfileBL.fillDeptWiseMajorHeadList(ddlMajorHeadList);
    }

    /// <summary>
    /// used to redirct the page to Echallan with values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        txtDepartment.Text = "Search Department By Name Or Code";
        dtValues = CreateDataSource();
        txtDepartment.Text = "Search Department By Name Or Code";
        if (lstselectedbudget.Items.Count > 0)
        {
            foreach (ListItem li in lstselectedbudget.Items)
            {
                string schename = li.Text.Substring(18);

                string[] schecode = li.Text.Substring(0, 17).Split('-');
                string[] schevalue = li.Value.Split('-');
                if (Convert.ToInt32(schevalue[0].ToString()) > 10000)
                {
                    schevalue[0] = "0";
                }

                string schewithdept = ConvertStringArrayToString(schecode) + '-' + schevalue[0].ToString() + '-' + schevalue[2].ToString();
                AddRow(schename, schewithdept, schevalue[1], dtValues );
            }

            Session["mydatatable"] = schemadt;
            //Response.Redirect("~/webpages/reports/EgEchallanViewRptnew.aspx?" + strURLWithData.ToString());
            ObjEncryptDecrypt = new EgEncryptDecrypt();
            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Guest={0}", "guest"));
            string strURL = "EgEChallan.aspx?" + strURLWithData;
            Response.Redirect(strURL);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please add schemas.');", true);
        }
    }

    /// <summary>
    /// used to convert array values into string
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    static string ConvertStringArrayToString(string[] array)
    {
        StringBuilder builder = new StringBuilder();
        foreach (string value in array)
        {
            builder.Append(value);

        }
        return builder.ToString();
    }

    /// <summary>
    /// used to move List time to another List Box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Add only single BudgetHead in case of MajorHead 8000 and above.')", true);
                }
                else
                {
                    addlabel.Visible = false;
                    lstselectedbudget.Visible = true;
                    int y = checkSchemaExist(li.Value);
                    if (y == 1)
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored Already Exist ')", true);
                    else if (y == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('This BudgetHead/Purpose not allowed ')", true);
                    }
                    else
                    {
                        if (lstselectedbudget.Items.Count > 8)
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('You can add only 9 BudgetHead/Purpose.!')", true);
                        else
                        {
                            lstselectedbudget.Items.Add(new ListItem(li.Text, li.Value + "-" + ddldepartment.SelectedValue));
                            ddldepartment.Enabled = false;
                            ddlMajorHeadList.Enabled = false;
                        }

                    }
                }
            }
        }
    }


    /// <summary>
    /// used to remove the List itme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        }
    }

    /// <summary>
    /// check duplicacy of Schema
    /// </summary>
    /// <param name="Schema"></param>
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
    /// Reset all control's value
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnreset_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        ddldepartment.SelectedValue = "0";
        lstselectedbudget.Items.Clear();
        lstbudgethead.Items.Clear();
        ddldepartment.Enabled = true;
        BudgetSchema.Visible = false;
        ddlMajorHeadList.Enabled = true;
        ddlMajorHeadList.Items.Clear();
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
        btnMore.Enabled = false;
        txtDepartment.Text = "Search Department By Name Or Code";
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

        //var qu = DtSearch.AsEnumerable().Where(s => s.Field<string>("DeptNameEnglish").ToUpper() == Text).Select(w => w.Field<string>("DeptNameEnglish"));

        var query = from t in DtSearch.AsEnumerable()
                    orderby t.Field<string>("DeptNameEnglish")// getting all the value from static datatable filled on pageload(DtSearch)
                    where t.Field<string>("DeptNameEnglish").ToUpper().Contains(Text)
                    select new
                   {
                       DeptNameEnglish = t.Field<string>("DeptNameEnglish"),
                   };
        foreach (var i in query.Take(10))                                           //taking top 5 matching records from the LINQ variable
        {
            LST.Add(i.DeptNameEnglish.ToString().Trim());

        };

        if (LST.Count > 0)
            return LST.OrderBy(s => s).ToList();

        else
            LST = new List<string> { "No record Found!" };                      // if list count is 0 than add message to the list of no record found
        return LST;

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
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Department does not exists.')", true);
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