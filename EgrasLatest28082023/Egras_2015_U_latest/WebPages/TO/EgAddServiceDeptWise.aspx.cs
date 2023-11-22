using EgBL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class WebPages_TO_EgAddServiceDeptWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
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
                EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
                objEgDeptServiceBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                DataTable ServiceTable = objEgDeptServiceBL.GetServiceNameDt();
                FillServiceList(ServiceTable);
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
    public void FillServiceList(DataTable ServiceTable)
    {
        ddlService.DataSource = ServiceTable;
        ddlService.DataTextField = "ServiceName";
        ddlService.DataValueField = "ServiceId";
        ddlService.DataBind();
        ddlService.Items.Insert(0, new ListItem("--- Select Service ---", "0"));
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
    public static string getServiceList(int DeptCode)
    {
        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.DeptCode = DeptCode;
        return objEgDeptServiceBL.GetServiceNameList1();
    }
    [WebMethod]
    public static string getServiceHeadsList(int DeptCode, int Service)
    {
        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.DeptCode = DeptCode;
        objEgDeptServiceBL.ServiceId = Service;
        return objEgDeptServiceBL.GetServiceHeadsList();
    }
    [WebMethod]
    public static string getBudgetHeadList(int DeptCode, string MajorHead)
    {
        string JSON1 = getServiceList(DeptCode);
        string JSONString = string.Empty;
        JSONString = getBudgetHeads(DeptCode, MajorHead);
        return JSONString + "|" + JSON1;
    }
    [WebMethod]
    public static string getBudgetHeads(int DeptCode, string MajorHead)
    {
        EgUserProfileBL objEgUserProfileBL = new EgUserProfileBL();
        objEgUserProfileBL.DeptCode = DeptCode;
        objEgUserProfileBL.majorheadcode = MajorHead;
        DataTable dt = objEgUserProfileBL.GetSchemaBudgetName();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;
    }
    [WebMethod]
    //public static string InsertServiceData(string BudgetHead, string ServiceName, int DeptCode)
    public static string InsertServiceData(string Parameter)
    {
        if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != Parameter)
        {
            return "Invalid Request";
        }
        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.BudgetHead = Parameter.Split('^').GetValue(0).ToString();
        objEgDeptServiceBL.DeptCode = Convert.ToInt32(Parameter.Split('^').GetValue(2));
        //objEgDeptServiceBL.ScheCode = ScheCode;
        objEgDeptServiceBL.ServiceName = Parameter.Split('^').GetValue(1).ToString();
        objEgDeptServiceBL.UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
        int returnVal = objEgDeptServiceBL.InsertDeptService();
        if (returnVal == 1)
        {
            return "Service Created Successfully";
        }
        else
        {
            return "Some error occured while processing your request";
        }
    }
    [WebMethod]
    //    public static string editServiceData(string BudgetHead, int ServiceId, int DeptCode)
    public static string editServiceData(string Parameter)
    {
        if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != Parameter)
        {
            return "Invalid Request";
        }
        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.BudgetHead = Parameter.Split('^').GetValue(0).ToString();
        objEgDeptServiceBL.DeptCode = Convert.ToInt32(Parameter.Split('^').GetValue(2));
        //objEgDeptServiceBL.ScheCode = ScheCode;
        objEgDeptServiceBL.ServiceId = Convert.ToInt32(Parameter.Split('^').GetValue(1));
        objEgDeptServiceBL.UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
        int returnVal = objEgDeptServiceBL.EditDeptService();
        if (returnVal == 1)
        {
            return "Service Updated Successfully";
        }
        else
        {
            return "Some error occured while processing your request";
        }
    }
    [WebMethod]
    //public static string ActiveDeactiveServiceData(int ServiceId, int DeptCode, bool isActiveFlag)
    public static string ActiveDeactiveServiceData(string Parameter)
    {
        if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != Parameter)
        {
            return "Invalid Request";
        }
        EgDeptServiceBL objEgDeptServiceBL = new EgDeptServiceBL();
        objEgDeptServiceBL.DeptCode = Convert.ToInt32(Parameter.Split('^').GetValue(1));
        objEgDeptServiceBL.ServiceId = Convert.ToInt32(Parameter.Split('^').GetValue(0));
        objEgDeptServiceBL.UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
        objEgDeptServiceBL.isActiveFlag = Convert.ToBoolean(Parameter.Split('^').GetValue(2));
        int returnVal = objEgDeptServiceBL.Active_DeactiveDeptService();
        if (returnVal == 1)
        {
            return objEgDeptServiceBL.isActiveFlag ? "Service Activated Successfully" : "Service Deactivated Successfully";
        }
        else
        {
            return "Some error occured while processing your request";
        }
    }
}