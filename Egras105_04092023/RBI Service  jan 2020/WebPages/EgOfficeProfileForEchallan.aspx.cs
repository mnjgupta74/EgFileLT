using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_EgOfficeProfileForEchallan : System.Web.UI.Page
{
    
    EgUserProfileBL objUserProfileBL;
    EgEMinusChallanBL objEMinusChallanBL; 
    DataTable dtValues;
    DataTable schemadt = new DataTable();
    EgEncryptDecrypt ObjEncryptDecrypt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)        
        {
            EgDeptAmountRptBL objDeptAmount = new EgDeptAmountRptBL();
            objEMinusChallanBL = new EgEMinusChallanBL();
            objDeptAmount.UserId = Convert.ToInt32(Session["UserId"].ToString());
            objDeptAmount.PopulateDepartmentList(ddldepartment);
            objEMinusChallanBL.GetObjectHeads(ddlObjectHead);
            ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
            btnMore.Enabled = false;
        }
    }

    /// <summary>
    /// creates DataTable
    /// </summary>
    /// <returns></returns>
    public DataTable CreateDataSource()
    {
        schemadt = new DataTable();
        schemadt.Columns.Add("Schemaname", typeof(string));
        schemadt.Columns.Add("Schececode", typeof(string));
        schemadt.Columns.Add("DeptName", typeof(string));
        schemadt.Columns.Add("DeptCode", typeof(int));
        schemadt.Columns.Add("GroupCode", typeof(string));
        schemadt.Columns.Add("ObjectHead", typeof(string));
        schemadt.Columns.Add("VNC", typeof(string));
        schemadt.Columns.Add("PNP", typeof(string));
        return schemadt;
    }

    /// <summary>
    /// adds row in DataTable
    /// </summary>
    /// <param name="schemaname"></param>
    /// <param name="Schececode"></param>
    /// <param name="dt"></param>
    public void AddRow(string schemaname, string Schececode, string groupCode, string ObjectHead, string VNC, string PNP, DataTable dt)
    {
        string[] word = ddldepartment.SelectedItem.Text.Split('-');
        dt.Rows.Add(new object[] { schemaname, Schececode, word[1].ToString(), word[0], groupCode, ObjectHead, VNC, PNP });
        dt.AcceptChanges();
    }

    /// <summary>
    /// bind all Schemas according to department
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objEMinusChallanBL = new EgEMinusChallanBL();

        //MajorHeadWebServ.WebService1 obj = new MajorHeadWebServ.WebService1();
        DataTable xml;//= obj.EgFillMajorHeadByDepartment(Convert.ToInt32(ddldepartment.SelectedValue));
        xml = objEMinusChallanBL.FillDeptwiseMajorHead(Convert.ToInt32(ddldepartment.SelectedValue),Convert.ToInt32(Session["UserID"]));
        ddlMajorHeadList.DataSource = xml;
        ddlMajorHeadList.DataTextField = "MajorHeadCodeName";
        ddlMajorHeadList.DataValueField = "MajorHeadCode";
        ddlMajorHeadList.DataBind();
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select Major Head--", "0"));
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
        objUserProfileBL = new EgUserProfileBL();
        objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objUserProfileBL.majorheadcode = Convert.ToString(ddlMajorHeadList.SelectedValue);
        FillBudgetHeads();
        if (Convert.ToInt32(ddlMajorHeadList.SelectedValue) > 7999)
        {
            ddlObjectHead.SelectedValue = "00";
            rblPlan.SelectedValue = "A";
            RblVoted.SelectedValue = "A";
        }
        else
        {
            ddlObjectHead.SelectedValue = "0";
            rblPlan.SelectedValue = "N";
            RblVoted.SelectedValue = "V";
        }
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
        dtValues=new DataTable();
        objEMinusChallanBL = new EgEMinusChallanBL();
        dtValues = CreateDataSource();

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
                AddRow(schename, schewithdept, schevalue[1], ddlObjectHead.SelectedValue, RblVoted.SelectedValue, rblPlan.SelectedValue, dtValues);
            }
            objEMinusChallanBL.BudgetHead = dtValues.Rows[0][1].ToString().Split('-')[0];
            objEMinusChallanBL.ObjectHead  = ddlObjectHead.SelectedValue;
            objEMinusChallanBL.VNC = RblVoted.SelectedValue;
            objEMinusChallanBL.PNP = rblPlan.SelectedValue;
            
            if (objEMinusChallanBL.CheckBudgetHead() == 1)
            {
                Session["mydatatable"] = schemadt;
                ObjEncryptDecrypt = new EgEncryptDecrypt();
                string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("Office={0}", "Office"));
                string strURL = "EgEChallanForOffice.aspx?" + strURLWithData;
                Response.Redirect(strURL);
            }
            else
            {
                DataTable dt = new DataTable();            
                DivCheckBudget.Visible = true;
                dt=objEMinusChallanBL.GetBudgetHeadPNPVNC();
                if (dt.Rows.Count > 0)
                {
                    grdCheckBudget.DataSource = dt;
                    grdCheckBudget.DataBind();
                    dt.Dispose();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Check Budget Head.');", true);
            }
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
            int majorHead = Convert.ToInt32(ddlMajorHeadList.SelectedValue);

            if (li.Selected == true)
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
                    if (lstselectedbudget.Items.Count > 0)
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('You can add only 1 BudgetHead/Purpose.!')", true);
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
        DivCheckBudget.Visible = false;
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
            objEMinusChallanBL = new EgEMinusChallanBL();
            objEMinusChallanBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
            DataTable HeadTable = objEMinusChallanBL.fillDeptWiseMajorHeadListForMinus();
            ddlMajorHeadList.DataSource = HeadTable;// HttpContext.Current.Cache["MajorHead"] as DataTable;
            ddlMajorHeadList.DataTextField = "MajorHeadName";
            ddlMajorHeadList.DataValueField = "MajorHeadCode";
            ddlMajorHeadList.DataBind();
            ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
            lstselectedbudget.Items.Clear();
            lstbudgethead.Items.Clear();
            ddlMajorHeadList.Enabled = true;
        }
    }
}
