using EgBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

using System.Web.Services;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Script.Services;

public partial class WebPages_Reports_EgSchemaAmountRpt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            //Page.SetFocus(ddldepartment);
            //Departmentlist();
        }
    }
    //public void Departmentlist()
    //{
    //    EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
    //    objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session["UserId"]);
    //    objEgDeptAmountRptBl.PopulateDepartmentList(ddldepartment);
    //}
    [WebMethod]
    public static string Departmentlist(string Session)
    {
        EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
        objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session.ToString());
        string result = objEgDeptAmountRptBl.BindDepartment();
        return result;
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
        TreasuryBudgetHeadRevenueBL objEgGetBudgetHead = new TreasuryBudgetHeadRevenueBL();
        //string ScheCode = "";
        //foreach (System.Web.UI.WebControls.ListItem item in ChkMajorHead.Items)
        //{
        //    if (item.Selected)
        //    {
        //        ScheCode += item.Value + "|";
        //    }
        //}
        objEgGetBudgetHead.DeptCode = DeptCode;
        objEgGetBudgetHead.MajorHead = MajorHead.ToString();
        DataTable dt = objEgGetBudgetHead.FillBudgetHeadwithMultiMajorHeads();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;
    }


    [WebMethod]
    public static string ShowData(ProfileData listdata)
    {
        string result = string.Empty;
        string str = "";
        //,ProfileData listdata,string FromDate, string ToDate
        TreasuryBudgetHeadRevenueBL objEgGetBudgetHead = new TreasuryBudgetHeadRevenueBL();
        ProfileData profileData = new ProfileData();
        profileData.Data = listdata.Data;
        
        foreach (var s in profileData.Data)
        {
            string[] SplitBudgethead = s.ToString().Split('|');
            str += SplitBudgethead[1].ToString() + "|";            
        }       
        profileData.DeptCode = listdata.DeptCode;
        profileData.FromDate = listdata.FromDate;
        profileData.ToDate = listdata.ToDate;
        //objEgGetBudgetHead.FromDate = Convert.ToDateTime(profileData.FromDate);
        //objEgGetBudgetHead.ToDate = Convert.ToDateTime(profileData.ToDate);
        string[] fromdate = profileData.FromDate.Split('/');
        objEgGetBudgetHead.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = profileData.ToDate.Split('/');
        objEgGetBudgetHead.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgGetBudgetHead.DeptCode = profileData.DeptCode;
        objEgGetBudgetHead.ScheCode = str.ToString();
        DataTable dt = objEgGetBudgetHead.FillTblSchemaAmount();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;
    }
    [WebMethod]
    public static string ShowDataTreasuryWise(ProfileData listdata)
    {
        string result = string.Empty;
        string str = "";
        //,ProfileData listdata,string FromDate, string ToDate
        TreasuryBudgetHeadRevenueBL objEgGetBudgetHead = new TreasuryBudgetHeadRevenueBL();
        ProfileData profileData = new ProfileData();
        profileData.Data = listdata.Data;

        foreach (var s in profileData.Data)
        {
            string[] SplitBudgethead = s.ToString().Split('|');
            str += SplitBudgethead[1].ToString() + "|";
        }
        profileData.DeptCode = listdata.DeptCode;
        profileData.FromDate = listdata.FromDate;
        profileData.ToDate = listdata.ToDate;
        //objEgGetBudgetHead.FromDate = Convert.ToDateTime(profileData.FromDate);
        //objEgGetBudgetHead.ToDate = Convert.ToDateTime(profileData.ToDate);
        string[] fromdate = profileData.FromDate.Split('/');
        objEgGetBudgetHead.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        string[] todate = profileData.ToDate.Split('/');
        objEgGetBudgetHead.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        objEgGetBudgetHead.DeptCode = profileData.DeptCode;
        objEgGetBudgetHead.ScheCode = str.ToString();
        DataTable dt = objEgGetBudgetHead.FillTblSchemaAmount_TreasuryWise();
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(dt);
        return JSONString;
    }

    public class ProfileData
    {
        public string[] Data { get; set; }
        public int DeptCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    //[WebMethod]

    //public static string SubmitFunction(int Deptcode , string listdata, string FromDate, string ToDate)
    //{
    //    TreasuryBudgetHeadRevenueBL objEgGetBudgetHead = new TreasuryBudgetHeadRevenueBL();
    //    string[] fromdate = FromDate.Split('/');
    //    objEgGetBudgetHead.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
    //    string[] todate = ToDate.Split('/');
    //    objEgGetBudgetHead.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
    //    objEgGetBudgetHead.DeptCode = Deptcode;
    //    objEgGetBudgetHead.ScheCode = listdata.ToString();
    //    DataTable dt = objEgGetBudgetHead.FillTblSchemaAmount();
    //    string JSONString = string.Empty;
    //    JSONString = JsonConvert.SerializeObject(dt);
    //    return JSONString;
    //}
}