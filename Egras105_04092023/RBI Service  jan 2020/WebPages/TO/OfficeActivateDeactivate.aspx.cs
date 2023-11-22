using System;
using EgBL;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web;
using System.Web.UI.WebControls;

public partial class WebPages_To_OfficeActivateDeactivate : System.Web.UI.Page
{
    EgTreasuryMaster objTres;
    EgDepartmentMaster objdept;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
            objTres = new EgTreasuryMaster();
            objTres.FillTreasury(ddllocation);
            objdept = new EgDepartmentMaster();
            objdept.UserId = Convert.ToInt32(Session["UserId"]);
            objdept.PopulateDepartmentList(ddldepartment);
        }
    }


    //[WebMethod]
    //public static string getOfficeData(string DeptCode, string Location)
    //{
    //    OfficeActiveDeactiveBL objOfcAct = new OfficeActiveDeactiveBL();
    //    objOfcAct.DeptCode = Convert.ToInt32(DeptCode);
    //    objOfcAct.Tcode = Location;
    //    DataTable dt = new DataTable();
    //    dt = objOfcAct.FillOfficeList();
    //    JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
    //    {
    //        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
    //    };
    //    var a = JsonConvert.SerializeObject(dt, new JsonSerializerSettings() { DateFormatString = "dd-MM-yyyy hh:mm:ss" });
    //    return a;
    //}
    //[WebMethod]
    //public static string SubmitOfficeData(string Parameter)
    //{
    //    if (Convert.ToString(HttpContext.Current.Session["HandlerVal"]) != Parameter)
    //    {
    //        return "Invalid Request";
    //    }

    //    OfficeActiveDeactiveBL objOfcAct = new OfficeActiveDeactiveBL();
    //    objOfcAct.RemovedUserProfileList = Parameter.Split('^').GetValue(0).ToString();
    //    objOfcAct.SelectedUserProfileList = Parameter.Split('^').GetValue(1).ToString();
    //    objOfcAct.Tcode = Parameter.Split('^').GetValue(2).ToString();

    //    if (objOfcAct.RemovedUserProfileList == "" )
    //        objOfcAct.RemovedUserProfileList = "0";


    //    if (objOfcAct.SelectedUserProfileList == "")
    //        objOfcAct.SelectedUserProfileList = "0";

    //    objOfcAct.UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
    //    int res = objOfcAct.UpdateOfficeActiveDeactive();
    //    return JsonConvert.SerializeObject(res); 
    //}

    protected void rptDivActive_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        OfficeActiveDeactiveBL objOfcAct = new OfficeActiveDeactiveBL();
        objOfcAct.Tcode = ddllocation.SelectedValue;
        objOfcAct.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objOfcAct.officeId = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "Ac")
        {
            Label Label1 = (Label)e.Item.FindControl("Label1");
            if (Label1.Text == "A")
            {
                objOfcAct.DivisionActiveDeactive(e.CommandArgument.ToString(), "D");
                BindOffices();
            }
            else
            {
                objOfcAct.DivisionActiveDeactive(e.CommandArgument.ToString(), "A");
                BindOffices();
            }

        }
    }
    public void BindOffices()
    {
        OfficeActiveDeactiveBL objOfcAct = new OfficeActiveDeactiveBL();
        objOfcAct.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objOfcAct.Tcode = ddllocation.SelectedValue;
        objOfcAct.FillOfficeListRepeater(rptDivActive);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindOffices();
        btnActionSubmit();
    }

    
    public void btnActionSubmit()
    {
        ddllocation.Enabled = false;
        ddldepartment.Enabled = false;
        btnShow.Enabled = false;
        btnreset.Enabled = true;
    }
    public void btnActionReset()
    {
        ddllocation.Enabled = true;
        ddldepartment.Enabled = true;
        btnShow.Enabled = true;
        btnreset.Enabled = false;
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        btnActionReset();
        rptDivActive.DataSource = null;
        rptDivActive.DataBind();
    }
}