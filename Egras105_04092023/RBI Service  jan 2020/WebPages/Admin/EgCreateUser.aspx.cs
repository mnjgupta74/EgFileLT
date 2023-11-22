using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;

public partial class WebPages_Admin_EgCreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\Default.aspx");
        }

        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            EgEChallanBL objEgEChallanBL = new EgEChallanBL();
            if (Session["UserType"].ToString() == "5")
            {
                EgDeptAmountRptBL objEgDeptAmountRptBL = new EgDeptAmountRptBL();
                objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objEgDeptAmountRptBL.PopulateDepartmentList(ddldepartment);
                objEgEChallanBL.FillTreasury(ddlTreasury);
                UserType.Visible = false;
                trTIN.Visible = false; // Add  23 july
            }
            else if (Session["UserType"].ToString() == "4")
            {
                DeptRow.Visible = false;
                UserType.Visible = false;
                tblOffice.Visible = false;
                trTIN.Visible = false;
            }
            else if (Session["UserType"].ToString() == "3")
            {
                tblSubTreasury.Visible = true;
                EgUserRegistrationBL objSubTreasuryFill = new EgUserRegistrationBL();
                objSubTreasuryFill.TreasuryCode = Session["UserName"].ToString().Substring(1);
                objSubTreasuryFill.FillSubTreasuryList(ddlSubTreasury);
                DeptRow.Visible = false;
                UserType.Visible = false;
                tblOffice.Visible = false;
                trTIN.Visible = false;
            }
            else
            {
                tblOffice.Visible = false;
                DeptRow.Visible = false;
                trTIN.Visible = true;
            }
            EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
            //objUserReg.FillDistrictList(ddlcity);
            objEgEChallanBL.GetBanks(ddlbank);
            BindRepeater();
            objUserReg.QuestionId = RandomNumber(1, 20);
            string[] QuestionAndAnswer = objUserReg.GetQuestionAndAnswer().Split('_');
            lblpass.Text = QuestionAndAnswer[0].ToString();
            ViewState["Answer"] = QuestionAndAnswer[1].ToString();
            objUserReg.FillStateList(ddlstate);
        }
        

    }
    public void BindRepeater()
    {
        DataTable dt = new DataTable();
        EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
        dt= objUserReg.FillDepartmentGrid();
        gdRows.DataSource = dt;
        gdRows.DataBind();
        dt.Dispose();

    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (txtlogin.Text != "")
            {
                if (ViewState["Answer"].ToString() == txtpass.Text)
                {
                    if (txtpassward.Text == txtrepassward.Text)
                    {
                        //if (txtfirst.Text != "" && txtlast.Text != "")
                        //{
                            if (rbltype.SelectedValue == "" && Session["UserType"].ToString() == "5")
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Select your user type in radio button list.!')", true);
                            }
                            else
                            {
                                EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
                                objUserReg.Type = 1;
                                objUserReg.LoginID = txtlogin.Text;
                                objUserReg.OfficeId = Convert.ToInt32(ddlOfficeName.SelectedValue.ToString().Trim());
                                 int user = objUserReg.CheckExistingOfficeLogin();
                           

                                if (user != 0 && Session["UserType"].ToString() == "3")
                                {
                                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('User Already Created.!')", true);
                                }
                                else if (user != 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('User Already Created.!')", true);
                                }
                                else
                                {
                                    objUserReg.FirstName = txtfirst.Text;
                                    objUserReg.LastName = txtlast.Text;

                                    string[] fromdate = txtBirthDate.Text.Trim().Replace("-", "/").Split('/');
                                    objUserReg.DOB = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

                                    objUserReg.Address = txtaddress.Text;
                                    objUserReg.City = txtCity.Text.Trim();
                                    objUserReg.State = int.Parse(ddlstate.SelectedValue);
                                    objUserReg.Country = int.Parse(ddlcountry.SelectedValue);
                                    objUserReg.Identity = txtTIN.Text;
                                    objUserReg.Password = txtpassward.Text;
                                    objUserReg.VerificationCode = null;
                                    objUserReg.AttemptNumber = Convert.ToString(0);
                                   
                                    if (Session["UserType"].ToString() == "5" && rbltype.SelectedValue == "4")
                                    {
                                        objUserReg.UserType = Convert.ToString(rbltype.SelectedValue);
                                        objUserReg.Dept = ddldepartment.SelectedValue + ",";
                                        objUserReg.Identity = ddlOfficeName.SelectedValue.ToString().Trim();//txtlogin.Text.Trim(); // Add
                                }
                                    else if (Session["UserType"].ToString() == "4")
                                    {
                                        objUserReg.UserId = Convert.ToInt32(Session["UserId"].ToString());
                                        //DataTable dt = objUserReg.FillDepartmentGridUserWise();
                                        //objUserReg.Dept = dt.Rows[0]["DeptCode"].ToString() + ",";
                                        objUserReg.Dept = objUserReg.FillDepartmentGridUserWise() + ",";
                                        objUserReg.UserType = "4";
                                        objUserReg.Identity = objUserReg.GetOfficeID().Trim(); //Add
                                    }
                                    else if (Session["UserType"].ToString() == "3")
                                    {
                                        objUserReg.UserType = "3";
                                        if (ddlSubTreasury.SelectedValue.Trim() == "0")
                                        {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Select SubTreasury.')", true);
                                        return;
                                        }
                                        objUserReg.Identity = ddlSubTreasury.SelectedValue.Trim();
                                    }
                                    else
                                    {
                                        if (rbltype.SelectedValue == "5")
                                        {
                                            objUserReg.UserType = Convert.ToString(rbltype.SelectedValue);
                                            objUserReg.UserId = Convert.ToInt32(Session["UserId"].ToString());
                                            //DataTable dt = objUserReg.FillDepartmentGridUserWise();
                                           // objUserReg.Dept = dt.Rows[0]["DeptCode"].ToString() + ",";
                                            objUserReg.Dept = objUserReg.FillDepartmentGridUserWise() + ",";
                                        }
                                        else
                                        {
                                            string dept = "";
                                            objUserReg.UserType = Convert.ToString(ddlusertype.SelectedValue);
                                            foreach (GridViewRow gvr in gdRows.Rows)
                                            {
                                                if (((CheckBox)gvr.FindControl("chkdept")).Checked == true)
                                                {
                                                    dept = dept + gdRows.DataKeys[gvr.RowIndex].Values[0].ToString() + ",";
                                                }
                                            }
                                            objUserReg.Dept = dept;
                                        }
                                    }
                                    if (txtmobile.Text.CompareTo("") == 0)
                                    {
                                        objUserReg.MobilePhone = "";
                                    }
                                    else
                                    {
                                        objUserReg.MobilePhone = txtmobile.Text.Trim();
                                    }
                                    objUserReg.PinCode = txtpincode.Text;
                                    objUserReg.Email = txtEmailId.Text;
                                    int S = 0;
                                    if (objUserReg.UserType.Trim() != "0")
                                    {
                                        S = objUserReg.InsertUserData();
                                    }
                                    if (S == 1)
                                    {
                                        int Uid = objUserReg.GetUserId();
                                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "JavaScript:Success();", true);
                                        clearControl();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Data not inserted.')", true);
                                    }
                                } 
                            }
                        //}
                        
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('FirstName And LastName is Not Valid')", true);
                        //}
                    }
                    
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Please Give your Answer')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('password and confirm password not match.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Login Id can not be blank.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "PopupScript", "alert('Entry is not valid.')", true);
        }
    }
    public void ChechExistingLogin(object sender, EventArgs e)
    {
        
        if (txtlogin.Text != "")
        {
            EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();
            objUserReg.LoginID = txtlogin.Text;
            objUserReg.OfficeId = Convert.ToInt32(ddlOfficeName.SelectedValue.ToString().Trim());
            int user = objUserReg.CheckExistingOfficeLogin(); 

            if (user != 0)
            {
                Image1.Visible = true;
                Image1.ImageUrl = "~/Image/delete.png";
            }
            else
            {
                Image1.Visible = true;
                Image1.ImageUrl = "~/Image/success.png";
            }
        }
        else
        {
            lblMsg.Text = "Enter Login Id";
        }
    }
    public void clearControl()
    {
        txtlogin.Text = "";
        txtfirst.Text = "";
        txtlast.Text = "";
        ddlusertype.SelectedIndex = 0;
        txtTIN.Text = "";

        txtaddress.Text = "";
        txtCity.Text = "";
        ddlstate.SelectedValue = "0";
        ddlcountry.SelectedValue = "0";
        txtBirthDate.Text = "";

        txtpincode.Text = "";
        txtEmailId.Text = "";
        txtmobile.Text = "";
        lblMsg.Text = "";
        Image1.Visible = false;
        ddldepartment.SelectedValue = "0";
        ddlOfficeName.SelectedValue = "0";
        ddlTreasury.SelectedValue = "0";
        ddlSubTreasury.SelectedValue = "0";
    }

    protected void rbltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString() == "5" && rbltype.SelectedValue == "5")
        {
            txtlogin.Text = "";
            txtlogin.Enabled = true;
            lblNoRecords.Visible = false;
            tblOffice.Visible = false;
            trTIN.Visible = true;
        }
        else
        {
            tblOffice.Visible = true;
            trTIN.Visible = false;
        }
        ResetValue();
    }
    public void ResetValue()
    {
        Image1.Visible = false;
        txtlogin.Text = "";
        ddlOfficeName.SelectedValue = "0";

    }

    protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        EgEChallanBL objEgEChallanBL = new EgEChallanBL();
        objEgEChallanBL.DeptCode = Convert.ToInt32(ddldepartment.SelectedValue);
        objEgEChallanBL.Tcode = ddlTreasury.SelectedValue;
        objEgEChallanBL.FillOfficeList(ddlOfficeName);
        txtlogin.Text = "";
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtlogin.Text = "Off"+ddlOfficeName.SelectedValue.ToString().Trim();

        txtlogin.Enabled = false;
    }
    protected void ddlSubTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtlogin.Text = "STO" + ddlSubTreasury.SelectedValue.ToString().Trim();
        txtlogin.Enabled = false;
    }


    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblNoRecords.Visible = false;
        var id = ddlusertype.SelectedValue;
        if (ddlusertype.SelectedValue == "5")
        {
            
                BindRepeater();
            if (gdRows.Rows.Count < 1)
            {
                lblNoRecords.Visible = true;
            }
            else {
                lblNoRecords.Visible = false;
            }
            
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "DisplayDiv(" + id + ");", true);
    }
}