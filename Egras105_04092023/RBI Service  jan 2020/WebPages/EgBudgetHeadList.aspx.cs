using System;
using System.Data;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.Services;

public partial class WebPages_EgBudgetHeadList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
    }

    [WebMethod]
    public static string GetDepartment(string userid)
    {
        EgDepartmentMaster objDepartment = new EgDepartmentMaster();
        objDepartment.UserId = Convert.ToInt32(userid);
        string result = objDepartment.GetDepartmentByUserId();
        return result;
    }
    [WebMethod]
    public static string FillBudgetHeadList(string deptcode)
    {
        if (deptcode != "0")
        {
            EgDeptAmountRptBL objDeptAmount = new EgDeptAmountRptBL();
            objDeptAmount.DeptCode = Convert.ToInt32(deptcode);
            string result = objDeptAmount.FillSchemaMajorHeadWise_Dept();
            return result;
        }
        else
        {
            return "0";
        }
    }
    //public void FillBudgetHeadList()
    //{
    //    EgDeptAmountRptBL objEgDept = new EgDeptAmountRptBL();
    //    objEgDept.UserId = Convert.ToInt32(Session["UserId"]);
    //    if (Convert.ToInt32(Session["UserId"]) == 52)
    //    {
    //        trmain.Visible = true;
    //        objEgDept.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
    //    }
    //    else
    //    {
    //        trmain.Visible = false;
    //        objEgDept.DeptCode = 0;
    //    }
    //    DataTable dt = new DataTable();
    //    objEgDept.PopulateSchemaList(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        trlist.Visible = true;
    //        lblmsg.Visible = false;
    //        rptschema.DataSource = dt;
    //        rptschema.DataBind();
    //    }
    //    else
    //    {
    //        lblmsg.Visible = true;
    //        trlist.Visible = false;
    //        rptschema.DataSource = null;
    //        rptschema.DataBind();
    //    }
    //}

    //protected void rptschema_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DataRowView drv = (DataRowView)e.Item.DataItem;
    //        string majorHead = drv["BudgetHead"].ToString();

    //        DataTable dt = new DataTable();
    //        EgDeptAmountRptBL objEgDept = new EgDeptAmountRptBL();
    //        objEgDept.UserId = Convert.ToInt32(Session["UserId"]);
    //        objEgDept.majorHead = majorHead;
    //        if (Convert.ToInt32(Session["UserId"]) == 52)
    //        {
    //            trmain.Visible = true;
    //            objEgDept.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
    //        }
    //        else
    //        {
    //            trmain.Visible = false;
    //            objEgDept.DeptCode = 0;
    //        }
    //        objEgDept.FillSchemaMajorHeadWise_Dept(dt);

    //        Repeater innerRepeater = ((Repeater)e.Item.FindControl("rptInner"));
    //        innerRepeater.DataSource = dt; //Suppose dt is the data table to bind.
    //        innerRepeater.DataBind();

    //    }
    //}

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    FillBudgetHeadList();
    //}
}
