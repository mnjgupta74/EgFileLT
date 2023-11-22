using EgBL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class WebPages_TO_PDActiveDeactive : System.Web.UI.Page
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
            ListItem li2 = new ListItem("Regular PD", "Y");
            //ListItem li3 = new ListItem("Only For Office", "N");
            li1.Attributes.Add("style", "color : #fedab9");
            li2.Attributes.Add("style", "color : #9acd32");
            //li3.Attributes.Add("style", "color : #fedab9");
            li2.Selected = true;
            
                li2.Attributes.Add("title", "Set PD for all users");
                li1.Attributes.Add("title", "Set PD for office only");
                rblType.Items.AddRange(new ListItem[] { li2, li1 });
           
            EgTreasuryMaster objTreasury = new EgTreasuryMaster();
            //objdept.UserId = Convert.ToInt32(Session["UserId"]);
            objTreasury.FillTreasury(ddlTreasury);
        }
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
    public static string getPdAccountList(string TreasuryCode, string BudgetHead)
    {
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.TreasuryCode = TreasuryCode;
        objHeadActiveDeactiveBL.BudgetHead = BudgetHead;
        return objHeadActiveDeactiveBL.GetPDList();
    }
    [WebMethod]
    //public static string UpdateHeadData(string BudgetHead, string type, int DeptCode)
    public static string UpdateHeadData(string Parameter)
    {        
        HeadActiveDeactiveBL objHeadActiveDeactiveBL = new HeadActiveDeactiveBL();
        objHeadActiveDeactiveBL.PDAccount = Parameter.Split('^').GetValue(0).ToString();
        objHeadActiveDeactiveBL.TreasuryCode = Parameter.Split('^').GetValue(2).ToString();
        objHeadActiveDeactiveBL.type = Parameter.Split('^').GetValue(1).ToString();
        objHeadActiveDeactiveBL.BudgetHead = Parameter.Split('^').GetValue(3).ToString();
        int returnVal = objHeadActiveDeactiveBL.UpdatePDStatus();
        if (returnVal == 1)
        {
            switch (Convert.ToInt32(HttpContext.Current.Session["userType"]))
            {
                case 5:
                    if (objHeadActiveDeactiveBL.type == "Y")
                        return "Selected PD are activated for all users";
                    else
                        return "Selected PD are activated for office only";
                case 2:
                    if (objHeadActiveDeactiveBL.type == "Y")
                        return "Selected PD are activated";
                    else
                        return "Selected PD are deactivated";
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