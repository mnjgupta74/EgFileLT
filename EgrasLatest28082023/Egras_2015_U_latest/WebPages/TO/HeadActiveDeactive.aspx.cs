using EgBL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class WebPages_TO_HeadActiveDeactive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
            ListItem li1 = new ListItem("Only for Office", "O");
            ListItem li2 = new ListItem("Regular Challan", "Y");
            ListItem li3 = new ListItem("Head Closed", "N");
            li1.Attributes.Add("style", "color : #fedab9");
            li2.Attributes.Add("style", "color : #9acd32");
            li3.Attributes.Add("style", "color : #fedab9");
            li2.Selected = true;
            if (Convert.ToInt32(Session["userType"]) == 5)
            {
                li2.Attributes.Add("title", "Set Heads for all users");
                li1.Attributes.Add("title", "Set Heads for office only");
                rblType.Items.AddRange(new ListItem[] { li2, li1 });
            }
            else
            {
                li2.Attributes.Add("title", "Activate Heads");
                li3.Attributes.Add("title", "Deactivate Heads");
                rblType.Items.AddRange(new ListItem[] { li2, li3 });
            }
            EgDepartmentMaster objdept = new EgDepartmentMaster();
            objdept.UserId = Convert.ToInt32(Session["UserId"]);
            objdept.PopulateDepartmentList(ddldepartment);
            if (Session["UserType"].ToString().Trim() == "5")
            {
                ddldepartment.SelectedIndex = 1;
                ddldepartment.Enabled = false;
                EgUserProfileBL objEgUserProfileBL = new EgUserProfileBL();
                objEgUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                DataTable HeadTable = objEgUserProfileBL.fillDeptWiseMajorHeadList();
                var rows = HeadTable.AsEnumerable().Where(t => t.Field<string>("Count") == "0");
                DataTable dt = rows.Any() ? rows.CopyToDataTable() : HeadTable.Clone();
                FillMajorHeadList(dt);

                FillMajorHeadList(dt);
            }
        }
    }
    public void FillMajorHeadList(DataTable HeadTable)
    {
        ddlMajorHead.DataSource = HeadTable;
        ddlMajorHead.DataTextField = "MajorHeadName";
        ddlMajorHead.DataValueField = "MajorHeadCode";
        ddlMajorHead.DataBind();
        ddlMajorHead.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
    }
    [WebMethod]
    public static string getMajorHeadList(int DeptCode)
    {
        EgUserProfileBL objEgUserProfileBL = new EgUserProfileBL();
        objEgUserProfileBL.DeptCode = DeptCode;
        DataTable dt = objEgUserProfileBL.fillDeptWiseMajorHeadList();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;
    }
    [WebMethod]
    public static string getBudgetHeadList(int DeptCode, string MajorHead)
    {
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.DeptCode = DeptCode;
        objHeadActiveDeactiveBL.Majorhead = MajorHead;
        return objHeadActiveDeactiveBL.GetBudegtHeadsList();
    }
    [WebMethod]
    //public static string UpdateHeadData(string BudgetHead, string type, int DeptCode)
    public static string UpdateHeadData(string Parameter)
    {
        if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != Parameter)
        {
            return "Invalid Request";
        }
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.BudgetHead = Parameter.Split('^').GetValue(0).ToString();
        objHeadActiveDeactiveBL.DeptCode = Convert.ToInt32(Parameter.Split('^').GetValue(2));
        objHeadActiveDeactiveBL.type = Parameter.Split('^').GetValue(1).ToString();
        int returnVal = objHeadActiveDeactiveBL.UpdateHeadStatus();
        if (returnVal == 1)
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["userType"]))
            {
                case 5:
                    if (objHeadActiveDeactiveBL.type == "Y")
                        return "Selected heads are activated for all users";
                    else
                        return "Selected heads are activated for office only";
                case 2:
                    if (objHeadActiveDeactiveBL.type == "Y")
                        return "Selected heads are activated";
                    else
                        return "Selected heads are deactivated";
                default:
                        return "Data Updated Succesfully";
            }
            
        }
        else
        {
            return "Some error occured while processing your request";
        }
    }
}