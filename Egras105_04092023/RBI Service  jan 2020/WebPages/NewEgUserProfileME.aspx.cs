using EgBL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_NewEgUserProfileME : System.Web.UI.Page
{
    EgUserProfileBL objUserProfileBL;
    EgEMinusChallanBL objEMinusChallanBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\logout.aspx");
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
            BindUserProfile();
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objEMinusChallanBL = new EgEMinusChallanBL();

        //MajorHeadWebServ.WebService1 obj = new MajorHeadWebServ.WebService1();
        DataTable dt;//= obj.EgFillMajorHeadByDepartment(Convert.ToInt32(ddldepartment.SelectedValue));
        dt = objEMinusChallanBL.FillDeptwiseMajorHead(Convert.ToInt32(ddldepartment.SelectedValue), Convert.ToInt32(Session["UserID"]));
        ddlMajorHeadList.DataSource = dt;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lstselectedbudget.Items.Count > 0)
        {
            EgUserProfileBL_ME objme = new EgUserProfileBL_ME();
            objme.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string[] SplitBudgethead = lstselectedbudget.Items[0].Text.Split('-');
            objme.BudgetHead = SplitBudgethead[0].ToString() + SplitBudgethead[1].ToString() + SplitBudgethead[2].ToString() + SplitBudgethead[3].ToString() + SplitBudgethead[4].ToString();
            string[] Value = lstselectedbudget.Items[0].Value.Split('-');
            //if (Convert.ToInt32(Value[0]) > 100000)
            //{
            //    objme.ScheCode = 0;
            //}
            //else
            //{
            //objme.ScheCode = Convert.ToInt32(Value[0].ToString());
            //}
            objme.ScheCode = 0;
            objme.DeptCode = Convert.ToInt32(Value[2].ToString());
            objme.ProfileName = txtProfileName.Text;
            objme.ObjectHead = ddlObjectHead.SelectedValue;
            objme.PlanNonPlan = rblPlan.SelectedValue;
            objme.VotedCharged = RblVoted.SelectedValue;
            //To Check Conditions For budgethead
            objEMinusChallanBL = new EgEMinusChallanBL();
            objEMinusChallanBL.BudgetHead = SplitBudgethead[0].ToString() + SplitBudgethead[1].ToString() + SplitBudgethead[2].ToString() + SplitBudgethead[3].ToString() + SplitBudgethead[4].ToString();
            objEMinusChallanBL.ObjectHead = ddlObjectHead.SelectedValue;
            objEMinusChallanBL.VNC = RblVoted.SelectedValue;
            objEMinusChallanBL.PNP = rblPlan.SelectedValue;

            if (objEMinusChallanBL.CheckBudgetHead() == 1)
            {
                if (btnSubmit.Text == "Submit")
                {
                    //EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
                    GenralFunction gf = new GenralFunction();
                   // System.Data.SqlClient.SqlTransaction Trans = gf.Begintrans();
                    int Maxpro = objme.GetMaxUserProME();
                    objme.UserPro = Convert.ToInt32(Maxpro + 1);
                  //  gf.Endtrans();
                    int res = objme.InsertUserProfileForME();
                    if (res == 1)
                    {
                        BindValue();
                        //string msg = objUserProfileBL.RedirectToEChallan();
                        //if (msg != string.Empty)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('" + msg + "')", true);
                        //}
                        //else
                        //{
                        //    string url = objUserProfileBL.UrlWithData;
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Save Successfully');window.location ='" + url + "';", true);
                        //}
                    }
                    else
                    {
                        DivCheckBudget.Visible = false;
                        grdCheckBudget.DataSource = null;
                        grdCheckBudget.DataBind();
                        grdCheckBudget.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored not Saved.')", true);
                    }
                }
                else
                {
                    objme.UserPro = Convert.ToInt32(ViewState["UserPro"].ToString());
                    objme.deleteUserProfileME();
                    int res = objme.InsertUserProfileForME();
                    if (res == 1)
                    {
                        BindValue();
                        btnSubmit.Text = "Submit";
                        DivCheckBudget.Visible = false;
                        grdCheckBudget.DataSource = null;
                        grdCheckBudget.DataBind();
                        grdCheckBudget.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored Update Successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Recored not Updated.')", true);
                    }
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DivCheckBudget.Visible = true;
                dt= objEMinusChallanBL.GetBudgetHeadPNPVNC();

                grdCheckBudget.DataSource = dt;
                grdCheckBudget.DataBind();

                dt.Dispose();
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Check Budget Head.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please add schemas.');", true);
        }
    }

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
    public void BindUserProfile()
    {
        EgUserProfileBL_ME objEgUserProfileBL_ME = new EgUserProfileBL_ME();
        objEgUserProfileBL_ME.UserId = Convert.ToInt32(Session["userId"].ToString());
        objEgUserProfileBL_ME.FillUserProfileRptME(rptuserprofile);
    }
    public void BindValue()
    {
        BindUserProfile();
        //listBox2Values.Value = "";
        lstselectedbudget.Items.Clear();
        lstbudgethead.Items.Clear();
        BudgetSchema.Visible = false;
        txtProfileName.Text = "";
        ddldepartment.Enabled = true;
        ddldepartment.SelectedValue = "0";
        ddlMajorHeadList.Items.Clear();
        ddlMajorHeadList.Enabled = true;
        ddlMajorHeadList.Items.Insert(0, new ListItem("--Select MajorHead--", "0"));
        //txtDepartment.Enabled = true;
    }

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

    protected void rptuserprofile_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { '/', '-' });
            txtProfileName.Text = commandArgsAccept[1].ToString();
            EgUserProfileBL_ME objEgUserProfileBL_ME = new EgUserProfileBL_ME();
            objEgUserProfileBL_ME.UserPro = Convert.ToInt32(commandArgsAccept[0].ToString());//it gives first ID
            ViewState["UserPro"] = objEgUserProfileBL_ME.UserPro;
            //objUserProfileBL.DeptCode = Convert.ToInt32(commandArgsAccept[1].ToString());//it gives second ID
            objEgUserProfileBL_ME.ProfileName = commandArgsAccept[1].ToString();//it gives second ID
            objEgUserProfileBL_ME.UserId = Convert.ToInt32(Session["userId"].ToString());
            DataTable dt = objEgUserProfileBL_ME.GetDeptSchemaNewME();

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
                objEgUserProfileBL_ME.DeptCode = int.Parse(dt.Rows[0][3].ToString());
                objEgUserProfileBL_ME.MajorHeadCode = dt.Rows[0][0].ToString().Substring(0, 4);
                DataTable dtMajorHead = objEgUserProfileBL_ME.fillDeptWiseMajorHeadListME();
                ddlMajorHeadList.DataSource = dtMajorHead;
                ddlMajorHeadList.DataTextField = "MajorHeadName";
                ddlMajorHeadList.DataValueField = "MajorHeadCode";
                ddlMajorHeadList.DataBind();
                if (ddlMajorHeadList.Items.FindByValue(objEgUserProfileBL_ME.MajorHeadCode.Trim()) != null && ddldepartment.Items.FindByValue(objEgUserProfileBL_ME.DeptCode.ToString().Trim()) != null)
                {
                    BudgetSchema.Visible = true;
                    lstselectedbudget.Visible = true;
                    btnSubmit.Text = "Update";
                    ddldepartment.SelectedValue = dt.Rows[0][3].ToString();
                    ddldepartment.Enabled = false;

                    ddlMajorHeadList.SelectedValue = dt.Rows[0][0].ToString().Substring(0, 4);
                    ddlMajorHeadList.Enabled = false;
                    btnMore.Enabled = false;
                    Header.Visible = true;
                    objUserProfileBL = new EgUserProfileBL();
                    objUserProfileBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
                    objUserProfileBL.majorheadcode = Convert.ToString(ddlMajorHeadList.SelectedValue);
                    FillBudgetHeads();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Profile is Not Valid.!')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Profile is Not Valid.!')", true);
            }
        }
        if (e.CommandName == "Ac")
        {
            EgUserProfileBL_ME objEgUserProfileBL_ME = new EgUserProfileBL_ME();
            Label Label1 = (Label)e.Item.FindControl("Label1");
            if (Label1.Text == "Y")
            {
                objEgUserProfileBL_ME.ProfileActiveDeactive_ME(e.CommandArgument.ToString(), "N");
                BindUserProfile();
            }
            else
            {
                objEgUserProfileBL_ME.ProfileActiveDeactive_ME(e.CommandArgument.ToString(), "Y");
                BindUserProfile();
            }

        }
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

}