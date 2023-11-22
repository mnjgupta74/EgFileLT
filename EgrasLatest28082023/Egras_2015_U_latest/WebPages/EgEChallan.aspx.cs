using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EgBL;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Services;
using System.Net;
using Newtonsoft.Json;

public partial class WebPages_EgEChallan : System.Web.UI.Page
{

    EgEChallanBL objEChallan = new EgEChallanBL();
    EgGuestProfileBL objGuestProfile = new EgGuestProfileBL();
    //Megha
    EgEncryptDecrypt ObjEncrytDecrypt;
    EgUserRegistrationBL objUserReg = new EgUserRegistrationBL();

    Label lbl;
    TextBox tb;
    Label lbl2;
    DataTable schemaAmtTable = new DataTable();
    DataTable dt1 = new DataTable();
    string Val, Profilee, GRN, Type;
    int DeptCode, ServiceId,ProcUserId;
    string ActiveDeActiveBank;

    //bool multiplebudgetheadflag = false;                             
    // Multiple Head Process(Procurement Department)  There Challan would be  create with  service (specific  Service) with Multiple Heads 
    // On Date 21 April 2020 

    string[] BudgetHead;      // Check 8443001030000||108||109 In Procurement Challan
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
            {
                Response.Redirect("~\\LoginAgain.aspx");
            }
            //Megha
            if (Request.QueryString.Count > 0)
            {
                ddlYear.FinyearSelectType = 2;
                ddlYear.count = 15;
                string strReqq = Request.Url.ToString();
                strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
                ObjEncrytDecrypt = new EgEncryptDecrypt();
                //EncryptDecryprBL ObjEncryptDecrypt = new EncryptDecryprBL();
                List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
                if (strList != null)
                {
                    if (strList.Count > 0)
                    {
                        if (strList[0].ToString() == "Guest")
                        {
                            Val = strList[1].ToString().Trim();
                            if (Session["mydatatable"] == null)
                                Response.Redirect("~\\Default.aspx");
                        }
                        else if (strList[0].ToString() == "Profile")
                        {
                            if (strList[1].ToString().Contains("service"))
                            {
                                ServiceId = Convert.ToInt32(strList[1].Split('|').GetValue(1));
                                DeptCode = Convert.ToInt32(strList[1].Split('|').GetValue(2));
                                ProcUserId = Convert.ToInt32(strList[1].Split('|').GetValue(3));
                                Profilee = "1";
                            }
                            else
                                Profilee = strList[1].ToString();
                        }
                        else
                        {
                            GRN = strList[1].ToString();
                            Type = strList[3].ToString();
                        }
                    }
                    else
                    {
                        Response.Redirect("~\\Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~\\Default.aspx");
                }
            }
            if (!IsPostBack)   // fill DropDownList on PageLoad
            {

                //Page.SetFocus(ddlOfficeName);
                //objEChallan.GetChallanBanks(ddlbankname);
                FillBanks();
                //objEChallan.FillTreasury(ddllocation);
                objEChallan.FillLocation(ddlTreasury);
                //objUserReg.FillDistrictList(ddlcity);

                //Fill Treasury DropDown with Grouping

                DropDownListX dp = new DropDownListX();
                DataTable treasuryData = new DataTable();
                treasuryData = dp.FillTreasury();

                for (int i = 1; i < 56; i++)
                {
                    var rows = treasuryData.AsEnumerable().Where(t => t.Field<int>("TGroupCode") == i);

                    string group = rows.ElementAtOrDefault(0).Field<string>("TreasuryName"); // ElementAtOrDefault(0).treasuryName.ToString().Trim();
                    ddllocation.AddItemGroup(group.Trim());
                    foreach (var item in rows)
                    {
                        ListItem items = new ListItem(item.Field<string>("TreasuryName"), item.Field<string>("TreasuryCode"));
                        ddllocation.Items.Add(items);

                    }

                }

                objEChallan.UserPro = Convert.ToInt32(Profilee);
                objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objEChallan.GetProfileList(ddlProfile);
                ddlProfile.SelectedValue = Profilee;
                ddlProfile.Enabled = false;

                if (Val != null)
                {
                    dt1 = (DataTable)Session["mydatatable"];
                    ViewState["GuestSchema"] = dt1;
                    objEChallan.DeptCode = int.Parse(dt1.Rows[0][3].ToString());
                    txtDept.Text = dt1.Rows[0][2].ToString();
                    txtDept.Enabled = false;
                    ViewState["DeptCode"] = dt1.Rows[0][3].ToString();
                    if ((dt1.Rows[0][1].ToString().Substring(0, 13) == "7610002020300") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "0049048000702") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "8449001200400"))
                    {
                        ddllocation.Enabled = true;
                    }
                    ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                    Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][3].ToString());

                    MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), ddlZone, "Guest");

                }
                else if (Profilee != null)

                {
                    if (ServiceId > 0)
                    {
                        dt1 = objEChallan.GetServiceSchema(ServiceId, DeptCode);
                    }
                    else
                    {
                        dt1 = objEChallan.GetSchema();
                    }
                    ViewState["profileSchema"] = dt1;
                    txtDept.Text = dt1.Rows[0][3].ToString();
                    txtDept.Enabled = false;
                    ViewState["DeptCode"] = dt1.Rows[0][4].ToString();
                    if ((dt1.Rows[0][1].ToString().Substring(0, 13) == "7610002020300") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "0049048000702") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "8449001200400"))
                    {
                        ddllocation.Enabled = true;
                    }
                    ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                    Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());

                    objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
                    MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), ddlZone, "Profile");


                    //dt1 = objEChallan.GetSchema();
                    //ViewState["profileSchema"] = dt1;
                    //txtDept.Text = dt1.Rows[0][3].ToString();
                    //txtDept.Enabled = false;
                    //ViewState["DeptCode"] = dt1.Rows[0][4].ToString();
                    //ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                    //Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());

                    //objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
                    //MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), ddlZone, "Profile");

                }
                else
                {
                    if (GRN == null)
                    {
                        Response.Write("<Script>alert('Session Expired')</Script>");
                        Response.Redirect("~\\Default.aspx");
                    }

                    if (Type == "Update")
                    {
                        btninsert.Text = "Update";
                    }
                    else
                    {
                        btninsert.Text = "Submit";
                    }
                    objEChallan.GRNNumber = Convert.ToInt64(GRN.ToString());
                    dt1 = objEChallan.GetSchema();
                    hidGRN.Value = GRN;
                    ViewState["RepeatSchema"] = dt1;
                    txtDept.Enabled = false;
                    txtDept.Text = dt1.Rows[0][3].ToString();
                    ViewState["DeptCode"] = dt1.Rows[0][4].ToString();
                    if ((dt1.Rows[0][1].ToString().Substring(0, 13) == "7610002020300") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "0049048000702") || (dt1.Rows[0][1].ToString().Substring(0, 13) == "8449001200400"))
                    {
                        ddllocation.Enabled = true;
                    }
                    ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                    Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());

                    objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
                    MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), ddlZone, "repeat");
                    txtChequeDDNo.Text = string.Empty;

                }
                //if (ViewState["DeptCode"].ToString().Trim() != "86")
                //{
                //    txtDeduct.Style.Add("display", "none");
                //}
                //if (ViewState["DeptCode"].ToString() != "18")
                //{
                //    ListItem li = new ListItem();
                //    li.Value = "5";
                //    li.Text = "Payment gateway/Credit/Debit Card";
                //    rblpaymenttype.Items.Add(li);
                //}
            }
            if (Profilee != null)
            {
                dt1 = (DataTable)ViewState["profileSchema"];
                DynamicTable(dt1);
            }

            //for guest user
            else if (Val != null)
            {
                dt1 = (DataTable)ViewState["GuestSchema"];
                DynamicTable(dt1);
                HyperLink1.Visible = true;
            }
            else
            {
                objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());

                dt1 = (DataTable)ViewState["RepeatSchema"];

                DynamicTable(dt1);

            }
            txttotalAmount.Enabled = false;
            txtamountwords.Enabled = false;

            if (ddlPeriod.SelectedIndex != 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "onload", "DisplayDiv(" + ddlPeriod.SelectedIndex + ");", true);
            }
        }
        catch (Exception ex)
        {
            //Browserinfo objbrowseringo = new Browserinfo();
            //string msg = ex.Message + objbrowseringo.Browserinformaion();
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message + "btnRedirect" + Session["UserId"].ToString() +"-"+ Profilee);

        }

    }

    private void MajorHeadWiseCheck(string Majorhead, int deptCode, DropDownList ddlZone, string status)
    {
        EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
        objCheck.BudgetHead = Majorhead;
        objCheck.DeptCode = deptCode;

        if (objCheck.CheckCTDCase() == true)
        {
            Cache.Insert("CTD", objEChallan.GetCTDInformation(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
            DataTable CTDTable = HttpContext.Current.Cache["CTD"] as DataTable;
            var rows = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0"));
            DataTable dt = rows.Any() ? rows.CopyToDataTable() : CTDTable.Clone(); //CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0")).CopyToDataTable();
            ddlZone.DataSource = dt;
            ddlZone.DataTextField = "Zone_Circle_Ward";
            ddlZone.DataValueField = "Location_Code";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
            txtTIN.Text = "";
            trCTD.Visible = true;
            if (Session["UserType"].ToString().Trim() != "4".Trim())
                txtTIN.Attributes.Add("onChange", "javascript:CheckTIN();");

        }
        else if (objCheck.CheckHead0030() == true)
        {
            txtDeduct.Enabled = true;
        }
        //else if (objCheck.CheckHeadGreater8000() == true)
        //{
        //    divPD.Visible = true;
        //    ddlpdAccount.Items.Insert(0, "Select PD-Account");

        //}

        if (objCheck.CheckHead004000102() == true)
        {
            CTDMSG.Visible = true;
            CTDMSG.Text = "Warning: This Budget head is going out from Feb 1,2015. Hence select new budget head 0040-00-111 for all type of payments under VAT budget head.";
        }

        int Result = 0;
        if (status == "repeat")
        {
            objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            Result = objEChallan.EChallanView();

            if (Result == 1)
            {
                ddlTreasury.SelectedValue = objEChallan.DistrictName.ToString();
                ddlProfile.SelectedValue = objEChallan.ProfileCode.ToString();
                ddllocation.SelectedValue = objEChallan.TreasuryCode.ToString();
                objEChallan.FillOfficeList(ddlOfficeName);
                ddlOfficeName.SelectedValue = objEChallan.OfficeName.ToString();
                ddllocation.SelectedValue = objEChallan.TreasuryCode.ToString();
                ddlProfile.Enabled = false;
                ddlPeriod.SelectedValue = "1";
                txtPanNo.Text = objEChallan.PanNumber.Trim();
                string Ptype = objEChallan.TypeofPayment;
                if (Ptype == "Manual")
                {
                    rblpaymenttype.SelectedValue = "3";
                }
                else
                {
                    rblpaymenttype.SelectedValue = "4";
                }
                //if (objCheck.CheckHeadGreater8000() == true)
                //{
                objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
                objEChallan.TreasuryCode = ddllocation.SelectedValue;
                objEChallan.BudgetHead = dt1.Rows[0][1].ToString().Substring(0, 13);
                objEChallan.GetPdAccountList(ddlpdAccount);
                ddlpdAccount.SelectedValue = Convert.ToString(objEChallan.PDacc > 0 ? objEChallan.PDacc : objEChallan.DivCode);
                //}

                txtRemark.Text = objEChallan.Remark;
                txtDeduct.Text = objEChallan.DeductCommission.ToString("0.00");
                txttotalAmount.Text = objEChallan.TotalAmount;
                txtChequeDDNo.Text = objEChallan.ChequeDDNo;
                txtamountwords.Text = objEChallan.AmountInWords;

                int Res = objEChallan.GetUserGrnDetail();
                if (Res == 1)
                {
                    txtName.Text = objEChallan.FirstName + " " + objEChallan.LastName;
                    txtaddress.Text = objEChallan.Address;
                    txtMobileNo.Text = objEChallan.MobileNo;
                    txtPin.Text = objEChallan.PinCode;
                    txtCity.Text = objEChallan.City.ToString().Trim();
                }
            }
        }
        else if (status == "Profile")
        {
            Result = objEChallan.GetUserDetail();
            if (Result == 1 && Session["UserID"].ToString().Trim() != "73")
            {
                txtName.Text = objEChallan.FirstName + " " + objEChallan.LastName;
                txtaddress.Text = objEChallan.Address;
                txtMobileNo.Text = objEChallan.MobileNo;
                txtPin.Text = objEChallan.PinCode;
                txtCity.Text = objEChallan.City.ToString().Trim();
            }
        }
    }

    protected void ddlTreasury_SelectedIndexChanged(object sender, EventArgs e)
    {
        rblpaymenttype.SelectedValue = "4";

        try
        {
            if (Val != null)
            {
                dt1 = (DataTable)ViewState["GuestSchema"];
                objEChallan.DeptCode = int.Parse(dt1.Rows[0][3].ToString());

            }
            else if (Profilee != null)
            {
                dt1 = (DataTable)ViewState["profileSchema"];
                objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
            }
            else
            {
                objEChallan.GRNNumber = Convert.ToInt32(GRN);
                objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
                dt1 = objEChallan.GetSchema();
                objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
            }
            objEChallan.Tcode = ddlTreasury.SelectedValue;
            objEChallan.FillOfficeList(ddlOfficeName);
            divbank.Visible = false;
            FillBanks();
            //objEChallan.GetChallanBanks(ddlbankname);

        }
        catch (Exception ex)
        {
            //Browserinfo objbrowseringo = new Browserinfo();
            //string msg = ex.Message + objbrowseringo.Browserinformaion();
            Response.Redirect("EgErrorPage.aspx?error=" + ex.Message);
        }

    }

    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
        DataTable dt = objEChallan.FillOfficeWiseTreasury();
        if (dt.Rows.Count != 0)
        {
            ddllocation.SelectedValue = dt.Rows[0][1].ToString();
            //ddllocation.Enabled = false;
        }
        rblpaymenttype.SelectedValue = "4";
        FillBanks();
        //objEChallan.GetChallanBanks(ddlbankname);
        //if (Convert.ToInt32(ViewState["majorHead"].ToString().Substring(0, 4)) > 7999 && dt.Rows.Count != 0)
        if (dt.Rows.Count != 0)


        {
            divPD.Visible = true;

            objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
            objEChallan.TreasuryCode = dt.Rows[0][1].ToString();
            objEChallan.BudgetHead = dt1.Rows[0][1].ToString().Substring(0, 13);
            objEChallan.GetPdAccountList(ddlpdAccount);
        }

    }

    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        rblpaymenttype.SelectedValue = "4";
        FillBanks();
        //objEChallan.GetChallanBanks(ddlbankname);

        //if (Convert.ToInt32(ViewState["majorHead"].ToString().Substring(0, 4)) > 7999)
        //{
        divPD.Visible = true;

        objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
        objEChallan.TreasuryCode = ddllocation.SelectedValue;
        objEChallan.BudgetHead = dt1.Rows[0][1].ToString().Substring(0, 13);
        objEChallan.GetPdAccountList(ddlpdAccount);
        // }

    }

    public void DynamicTable(DataTable dt)
    {
        var HeadCount = (from r in dt.AsEnumerable()                  // Condition Close At 28 dec 2020
                         select r["BudgetHead"].ToString().Substring(0, 4)).Distinct().ToList();


        BudgetHead = (from r in dt.AsEnumerable()
                      select r["BudgetHead"].ToString().Substring(0, 13)).Distinct().ToArray();


        int rowCount = Convert.ToInt32(dt.Rows.Count);
        ViewState["rowcount"] = rowCount;

        pnlHead.Visible = true;

        pnlHead.Controls.Add(tbl);
        HtmlTableRow row;
        HtmlTableCell cell1;
        HtmlTableCell cell2;
        HtmlTableCell cell3;

        for (int i = 0; i < rowCount; i++)
        {

            row = new HtmlTableRow();
            cell1 = new HtmlTableCell();
            cell2 = new HtmlTableCell();
            cell3 = new HtmlTableCell();
            lbl = new Label();
            tb = new TextBox();
            lbl2 = new Label();

            // Set a unique ID for each Label added
            lbl.ID = "Label_" + i;
            string[] budgethead = dt.Rows[i][1].ToString().Split('-');
            string budget = budgethead[0].ToString().Substring(0, 4) + "-" + budgethead[0].ToString().Substring(4, 2) + "-" + budgethead[0].ToString().Substring(6, 3) + "-" + budgethead[0].ToString().Substring(9, 2) + "-" + budgethead[0].ToString().Substring(11, 2);
            lbl.Text = dt.Rows[i][0].ToString() + " (" + budget + ")";
            // Add the control to the TableCell
            lbl.Width = 470;

            cell1.Controls.Add(lbl);
            // Set a unique ID for each TextBox added
            if (dt.Columns.Count == 5)
            {
                tb.ID = "TextBox_" + dt.Rows[i][1].ToString();
                //   tb.ID = "TextBox_" + i;
                tb.Text = "0.00";
                tb.MaxLength = 11;
                tb.Style.Add("text-align", "right");
                tb.Width = 180;
            }
            else
            {
                tb.ID = "TextBox_" + dt.Rows[i][1].ToString();
                decimal moneytotal = Convert.ToDecimal(dt.Rows[i][2]);
                tb.Text = moneytotal.ToString("0.00"); //string.Format("{0:0.0}", moneytotal);
                tb.MaxLength = 11;
                tb.Style.Add("text-align", "right");
                tb.Width = 180;
            }
            // Add the control to the TableCell
            cell2.Controls.Add(tb);
            int b = i + 1;
            lbl2.ID = "lblSNo" + b;
            lbl2.Text = (i + 1).ToString() + ".";
            cell3.Controls.Add(lbl2);

            tb.Attributes.Add("onkeyup", "DecimalNumber(this);");
            tb.Attributes.Add("onPaste", "return false");
            tb.Attributes.Add("onBlur", "javascript:updateValue('" + tb.ID + "');");
            tb.Attributes.Add("onChange", "javascript:updateValue('" + tb.ID + "');");
            tb.Attributes.Add("onFocus", "javascript:ClearValue(this);");


            // Add the TableCell to the TableRow
            row.Cells.Add(cell3);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            if (HeadCount.Count > 1)
                tbl.Style.Add("background-color", "#c8d6d5");
            //Add the TableRow to the Table
            tbl.Rows.Add(row);

        }
        //ViewState["DynamicTable"] = true;
    }

    protected void btninsert_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
            objCheck.BudgetHead = ViewState["majorHead"].ToString();
            objCheck.payMode = rblpaymenttype.SelectedValue.ToString();
            objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
            objCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
            objCheck.pdVisible = ddlpdAccount.Visible;
            objCheck.treasuryCodeBank = ddlbankname.SelectedValue.ToString();
            objCheck.treasuryCodePd = ddlpdAccount.SelectedValue.ToString();
            objCheck.isPD = ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true;
            objCheck.proc_id = ProcUserId;
            objCheck.serviceBudgetHead = BudgetHead;

            if (ddlbankname.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('select Bank!');", true);
            }
            else if (objCheck.CheckSubmitCondition() == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + objCheck.msg + "');", true);
            }
            // Add Condition  For Identity Challan Id  Proc Tender Fee Or Non proc Tender Fee
            // Proc Tender Fee Allow Only E-Banking And   Non-Proc Tender Fee Allow Both Payment Mode 22 April 2020
            else if (ProcUserId==-1 && objCheck.payMode=="3")  
            {
              
                 ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('EProc Tender Fee Submit Only Through E-Banking');", true);
            }

            // else if (ddlpdAccount.SelectedValue != "0" && rblpaymenttype.SelectedValue == "4")
            // {
            //     ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('E-Banking is not allowed in PD Cases.');", true);
            // }
            // else if (rblpaymenttype.SelectedValue == "3" && ddlpdAccount.SelectedValue != "0" && ddlbankname.SelectedValue.Substring(7, 2).Trim() != ddlpdAccount.SelectedValue.Trim().Substring(ddlpdAccount.SelectedValue.Trim().Length - 4, 2))
            // {
            //     ddlbankname.SelectedValue = "0";
            //     ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Pd / Division does not match with selected treasury.!')", true);
            // }
            ////Add  8443 not allow for ebanking
            // else if ((ViewState["majorHead"].ToString().Substring(0, 4) == "8782" || ViewState["majorHead"].ToString().Substring(0, 4) == "8443") && rblpaymenttype.SelectedValue == "4")
            // {
            //     ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('E-Banking is not allowed in this Case. Please select 0215 for depositing amount.');", true);
            // }
            // else if (ddlpdAccount.Visible == true &&
            //     ddlpdAccount.SelectedValue == "0" && (
            //     ViewState["majorHead"].ToString().Substring(0, 4) == "8338" ||
            //     ViewState["majorHead"].ToString().Substring(0, 4) == "8342" ||
            //     ViewState["majorHead"].ToString().Substring(0, 4) == "8448" ||
            //     ViewState["majorHead"].ToString() == "8443001060000") &&
            //     (ViewState["majorHead"].ToString() != "8342001170100" &&
            //      ViewState["majorHead"].ToString() != "8342001170200" &&
            //      ViewState["majorHead"].ToString() != "8448001200705") // Change on 01/09/2014 as per request by user.
            //     )
            // {
            //     ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('please select Pd / Division Code.!');", true);
            // }
            // else if (
            //             ddlpdAccount.Visible == true &&
            //             ddlpdAccount.Items.Count > 1 &&
            //             ddlpdAccount.SelectedValue == "0" &&
            //             ViewState["majorHead"].ToString().Substring(0, 4) != "8782" &&
            //             ViewState["majorHead"].ToString().Substring(0, 4) != "8793"
            //         )
            // {
            //     ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select  Pd / Division Code.!');", true);
            // }
            else
            {

                submitclick();
            }
        }
    }
    /// <summary>
    /// Insert User Challan Information and Schema  Amount Info  and Check Objects Amount not fill more than 9
    /// </summary>
    public void submitclick()
    {

        objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());

        int rows = Convert.ToInt32(ViewState["rowcount"]);
        int count = 0;
        double SumTotalAmount = 0.0;
        if (Profilee != null)
        {
            dt1 = (DataTable)ViewState["profileSchema"];

        }
        else if (Val != null)
        {
            // DataTable dt = (DataTable)Cache["mydatatable"];
            dt1 = (DataTable)ViewState["GuestSchema"];
        }
        else
        {
            dt1 = (DataTable)ViewState["RepeatSchema"];

        }

        // Change Amount Condition  0 to 1 Rupees  Minimum in Every Head. 12 May 2020
        for (int j = 0; j < rows; j++)
        {
            if (Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text) > 0 && ((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text != "")
            {
                if ((Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text) - Math.Floor(Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text))) != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Amount should be only in Rupees not in Paise !! ');", true);
                    return;
                }
                count = count + 1;
                SumTotalAmount = SumTotalAmount + Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text);
            }
        }
        // added 30 july for set total sum of amount from dynamic budgetHead TextBox amount
        txttotalAmount.Text = Convert.ToString(SumTotalAmount - Convert.ToDouble(txtDeduct.Text));
        if (Convert.ToDouble(txtDeduct.Text) > 0 && (ViewState["DeptCode"].ToString().Trim() != "86" || dt1.Rows[0][1].ToString().Substring(0, 4) != "0030"))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Commission Amount not allowed!');", true);
            return;
        }
        if (Convert.ToDouble(txtDeduct.Text) > ((SumTotalAmount * 20.00) / 100.00))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Commission Amount not allowed more than 20% of Total Amount!');", true);
            return;
        }
        if (Convert.ToDouble(txtDeduct.Text) < 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Commission Amount Could not be in Minus!');", true);
            return;
        }
        if ((Convert.ToDouble(txttotalAmount.Text) - Math.Floor(Convert.ToDouble(txttotalAmount.Text))) != 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Amount should be only in Rupees not in Paise !! ');", true);
            return;
        }
        if (Convert.ToDouble(txttotalAmount.Text) < 1.00)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Total Amount Could not be Less than 1.00!');", true);
            return;
        }
        if (count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Fill the Amount Detail')", true);
        }

        //selection of  DD or Cheque DD No or Cheque No  is Mandatory otherwise bydefault cash 29 July  2019
        if (rblpaymenttype.SelectedValue.Trim() == "3" && rblCashCheque.SelectedValue.Trim() == "2" && txtChequeDDNo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Fill Cheque or DD No !!');", true);
            return;
        }
        else if (count > 9)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('You can not fill more than 9 Schemas Amount !');", true);
        }
        else if (count > 1 && Convert.ToDouble(txtDeduct.Text) > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Amount allowed in one BudgetHead/Purpose only!');", true);
        }
        else if (Convert.ToDouble(txttotalAmount.Text) >= 50000 && txtPanNo.Text.ToString().Trim() == string.Empty)
        {
            if (Session["UserType"].ToString() == "4")
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Message", "alert('TAN Number is Compulsory With Amount 50000 or Above!')", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Message", "alert('PAN Number is Compulsory With Amount 50000 or Above!')", true);
            txttotalAmount.Focus();
            return;
        }

        else
        {
            int output = 0;

            InsertData();
            if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Enter Vehicle No!')", true);
                txtTIN.Focus();
                return;
            }

            // Multiple Head Condition  21  April   2020
            //if (multiplebudgetheadflag)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Message", "alert('Amount Can Not Be Zero(0)   With Multiple Head Challan !!');", true);
            //    return;
            //}

            // for budgethead check 4 april 2015
            if (objEChallan.CheckBudgetHeadWithDept() == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Budget Head Not Map to Department');", true);
                return;
            }
            else
            {

                if (btninsert.Text == "Submit")
                {
                    output = objEChallan.InsertChallan();
                    Session["GrnNumber"] = Convert.ToString(output);
                }
                //else
                //{
                //    objEChallan.GRNNumber = Convert.ToInt32(Session["GrnNumber"]);
                //    //  output = objEChallan.EgUpdateEChallan();
                //}
            }
            if (output > 0)
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Saved Successfully')", true);
                //Session.Remove("mydatatable");  // added 30 july for kill Session 
                ObjEncrytDecrypt = new EgEncryptDecrypt();
                if (rblpaymenttype.SelectedValue == "3")
                {
                    // string strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("ManualBank={0}", ddlbankname.SelectedValue.ToString().Substring(7, 2)));
                    Response.Redirect("~/webpages/reports/EgManualChallan.aspx");
                    //if (ddlbankname.SelectedValue.ToString().Substring(7, 2) == "-1")// Added manual Scroll;
                    //{
                    //    //objEChallan.Location = ddllocation.SelectedValue;
                    //    // Response.Redirect("~/webpages/reports/EgEchallanViewRptAnywhere.aspx?" + strURLWithData.ToString());
                    //    Response.Redirect("~/webpages/reports/EgManualChallan.aspx?" + strURLWithData.ToString());


                    //}

                    //else
                    //{
                    //    Response.Redirect("~/webpages/reports/EgEchallanViewRptnew.aspx?" + strURLWithData.ToString());

                    //}
                }
                else
                {
                   
                    Response.Redirect("EgEChallanView.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Not Saved')", true);
            }
        }


    }


    /// <summary>
    /// Create Dynamic Table According to Profile Schema
    /// </summary>
    /// <param name="dt"></param>
    /// 

    public void InsertData()
    {

        objEChallan.Profile = Convert.ToInt32(ddlProfile.SelectedValue);
        if (rblpaymenttype.SelectedValue == "3")
        {
            objEChallan.TypeofPayment = "M";
        }
        else
        {
            objEChallan.TypeofPayment = "N";
        }
        EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
        objCheck.BudgetHead = ViewState["majorHead"].ToString();
        objCheck.DeptCode = Convert.ToInt32(ViewState["DeptCode"]);
        if (objCheck.CheckCTDCase() == true)
        {
            objEChallan.Zone = ddlZone.SelectedValue;
            objEChallan.Circle = ddlCircle.SelectedValue;
            objEChallan.Ward = ddlWard.SelectedValue;
            if (Session["UserID"].ToString() == "73" && txtTIN.Text == "")
            {
                objEChallan.Identity = "99999999999";
            }
            else
            {
                objEChallan.Identity = txtTIN.Text;
            }
        }
        else if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty)    // Add Condition For Vehicle No Compulsory  30/7/2018
        {
            return;
        }
        else
        {
            objEChallan.Identity = txtTIN.Text;
        }
        objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
        objEChallan.PanNumber = txtPanNo.Text;

        if (rblpaymenttype.SelectedValue == "3")
        {
            //{    Modify Code for Add sbi Any  with manual process 
            //    where beacuase in bank branch table we add treasury code -1 
            //    so there we find treasury with ddllocation drop down
            if (ddlbankname.SelectedValue.ToString().Substring(7, 2) == "-1")
            {
                objEChallan.Location = ddllocation.SelectedValue;

            }
            else
            {
                objEChallan.Location = ddlbankname.SelectedValue.ToString().Substring(7, 4);

            }
            objEChallan.BankName = ddlbankname.SelectedValue.ToString().Substring(0, 7);//ddllocation.SelectedValue;
        }
        else
        {
            objEChallan.Location = ddllocation.SelectedValue;
            objEChallan.BankName = ddlbankname.SelectedValue;
        }
        objEChallan.FullName = txtName.Text;
        objEChallan.ChallanYear = ddlYear.SelectedValue;
        objEChallan.Address = txtaddress.Text;
        objEChallan.City = txtCity.Text.Trim();
        objEChallan.MobileNo = txtMobileNo.Text;
        objEChallan.PINCode = txtPin.Text;
        objEChallan.DeductCommission = double.Parse(txtDeduct.Text);
        objEChallan.TotalAmount = txttotalAmount.Text;
        //Session["TotalAmt"] = txttotalAmount.Text;
        objEChallan.ChequeDDNo = txtChequeDDNo.Text;

        //objEChallan.BankBranch = txtbankbranch.Text;
        objEChallan.AmountInWords = txtamountwords.Text;
        objEChallan.Remark = txtRemark.Text;
        string Details = HiddenField1.Value; //SubmitExtraDetails();// HiddenField1.Value; //
        if (Details != "")
        {
            objEChallan.Details = Details;
        }
        objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
        objEChallan.Serviceid = ServiceId;    // 21 APril 2020
        objEChallan.ProcUserId = ProcUserId;

        if (objCheck.CheckHeadGreater8000WithPD() == true)
        {
            if (ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true)
                objEChallan.PDacc = Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4));
            else
                objEChallan.DivCode = Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4));
        }
        else if (ddlpdAccount.Items.Count > 1)
        {
            if (ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true)
                objEChallan.PDacc = ddlpdAccount.SelectedValue != "0" ? Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4)) : 0;
            else
                objEChallan.DivCode = ddlpdAccount.SelectedValue != "0" ? Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4)) : 0;
        }
        else
        {
            objEChallan.PDacc = 0;
        }
        // Set From date and Todate
        SetPeriod();
        // Set TVP for grn_schema_amount table
        objEChallan.dtSchema = AmountSave();

    }

    private void SetPeriod()
    {
        string Fdate = "";
        string Tdate = "";
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
            Fdate = Convert.ToString(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
            string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
            Tdate = Convert.ToString(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        }
        string year = ddlYear.SelectedValue.ToString().Substring(0, 2);
        string year1 = ddlYear.SelectedValue.ToString().Substring(ddlYear.SelectedValue.ToString().Length - 2);
        string FromChallanDate = ""; ;
        string ToChallanDate = "";
        if (ddlPeriod.SelectedValue == "3")
        {
            if (int.Parse(ddlMothly.SelectedValue.Substring(1, 2)) > 3)
            {
                FromChallanDate = "20" + year + ddlMothly.SelectedValue.Substring(0, 6);
                ToChallanDate = "20" + year + ddlMothly.SelectedValue.Substring(7, 6);
            }
            else
            {
                FromChallanDate = "20" + year1 + ddlMothly.SelectedValue.Substring(0, 6);
                ToChallanDate = "20" + year1 + ddlMothly.SelectedValue.Substring(7, 6);
            }
        }

        objEChallan.ChallanFromMonth = Convert.ToDateTime(ddlPeriod.SelectedValue == "1" ? "20" + year + "/04/01" :
                                        ddlPeriod.SelectedValue == "2" ? (ddlhalfyearly.SelectedValue == "1" ? "20" + year + "/04/01" : "20" + year + "/10/01") :
                                        ddlPeriod.SelectedValue == "3" ? FromChallanDate.ToString() :
                                        ddlPeriod.SelectedValue == "4" ?
                                         (ddlQUARTERLY.SelectedValue == "1" ? "20" + year + "/04/01" : ddlQUARTERLY.SelectedValue == "2" ? "20" + year + "/07/01" :
                                          ddlQUARTERLY.SelectedValue == "3" ? "20" + year + "/10/01" : ddlQUARTERLY.SelectedValue == "4" ? "20" + year1 + "/01/01" : null) :
                                           ddlPeriod.SelectedValue == "5" ? Fdate : null);
        objEChallan.ChallanToMonth = Convert.ToDateTime(ddlPeriod.SelectedValue == "1" ? "20" + year1 + "/03/31" :
                                           ddlPeriod.SelectedValue == "2" ? (ddlhalfyearly.SelectedValue == "1" ? "20" + year + "/09/30" : " 20" + year1 + "/03/31") :
                                           ddlPeriod.SelectedValue == "3" ? ToChallanDate.ToString() :
                                           ddlPeriod.SelectedValue == "4" ?
                                             (ddlQUARTERLY.SelectedValue == "1" ? " 20" + year + "/06/30" : ddlQUARTERLY.SelectedValue == "2" ? "20" + year + "/09/30" :
                                             ddlQUARTERLY.SelectedValue == "3" ? "20" + year + "/12/31" : ddlQUARTERLY.SelectedValue == "4" ? " 20" + year1 + "/03/31" : null) :
                                             ddlPeriod.SelectedValue == "5" ? Tdate : null);

    }

    public DataTable AmountSave()
    {
        CreateSchemaAmtTable();
        DataRow schemarow;
        int i = Convert.ToInt32(ViewState["rowcount"]);
        //var HeadCount = (from r in dt1.AsEnumerable()    // Condition Update 28/12/2020  Allow Zero in single Head
        //                 select r["BudgetHead"].ToString().Substring(0, 4)).Distinct().ToList();
        for (int j = 0; j < i; j++)
        {
            if (Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text) > 0 && ((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text != "")
            {
                // objEChallan = new EgEChallanBL();
                schemarow = schemaAmtTable.NewRow();
                string[] BudgetHeadName = dt1.Rows[j][1].ToString().Split('-');
                schemarow["BudgetHead"] = BudgetHeadName[0].ToString();

                if (Session["UserId"].ToString().Trim() == "73")
                {
                    if (int.Parse(BudgetHeadName[1].ToString()) > 100000)
                    {

                        schemarow["ScheCode"] = 0;
                    }
                    else
                    {

                        schemarow["ScheCode"] = int.Parse(BudgetHeadName[1].ToString());
                    }
                }
                else
                {

                    schemarow["ScheCode"] = int.Parse(BudgetHeadName[1].ToString());
                }

                schemarow["DeptCode"] = Convert.ToInt32(BudgetHeadName[2].ToString());
                schemarow["amount"] = Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text);//Convert.ToDouble("asdfasdf"); //
                schemarow["UserId"] = Convert.ToInt32(Session["UserId"].ToString());
                schemaAmtTable.Rows.Add(schemarow);
                schemaAmtTable.AcceptChanges();

            }
            else
            {
                //if (HeadCount.Count > 1)
                //    multiplebudgetheadflag = true;
            }
        }
        return schemaAmtTable;
    }

    private void CreateSchemaAmtTable()
    {
        schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
        schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));
    }

    protected void txttotalAmount_TextChanged(object sender, EventArgs e)
    {
        //  txtamountwords.Text = NumberToCurrencyText(Convert.ToDecimal(txttotalAmount.Text), System.MidpointRounding.ToEven);
    }

    public void SetFocus(Page sPage)
    {
        string[] sCtrl = null;
        string sCtrlId = null;
        Control sCtrlFound = default(Control);
        string sCtrlClientId = null;
        string sScript = null;

        sCtrl = sPage.Request.Form.GetValues("__EVENTTARGET");
        if ((sCtrl != null))
        {
            sCtrlId = sCtrl[0];
            sCtrlFound = sPage.FindControl(sCtrlId);
            if ((sCtrlFound != null))
            {
                sCtrlClientId = sCtrlFound.ClientID;
                sScript = "";
                sPage.ClientScript.RegisterStartupScript(typeof(string), "controlFocus", sScript);
            }
        }
    }
    protected void TextValidate(object source, ServerValidateEventArgs args)
    {
        int i = Convert.ToInt32(args.Value);
        if (Convert.ToInt32(args.Value) > 0)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }

    }
    protected void rblCashCheque_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtChequeDDNo.Text = string.Empty;
        spanCheque.Visible = rblCashCheque.SelectedValue == "2" ? true : false;
    }
    protected void rblpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtamountwords.Text = HiddenAmount.Value;

        if (ddllocation.SelectedValue == "0" && rblpaymenttype.SelectedValue == "3")
        {
            ddlbankname.Items.Clear();
            divbank.Visible = false;
            PayuDialog.Visible = false;
            FillBanks();
            //objEChallan.GetChallanBanks(ddlbankname);
            rblpaymenttype.SelectedValue = "4";
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Select Office Name First.!')", true);
        }
        else
        {
            if (rblpaymenttype.SelectedValue == "3")
            {
                EgCheckChallanCondition objManualAnuWhereBranchCheck = new EgCheckChallanCondition();
                objManualAnuWhereBranchCheck.pdAccountNo = ddlpdAccount.Items[0].ToString();// Passing value of Select Statement to Check Wether PD or Division Case
                objManualAnuWhereBranchCheck.treasuryCodeBank = ddllocation.SelectedValue;
                objManualAnuWhereBranchCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
                objEChallan.TreasuryCode = ddllocation.SelectedValue;
                if (objManualAnuWhereBranchCheck.CheckManualBanksForAnyWhereBranch())// ADDED by Sandeep for Manual Banks SBIAnyWhere
                    objEChallan.Type = -1;
                else
                    objEChallan.Type = 0;
                divbank.Visible = true;
                PayuDialog.Visible = false;
                ddlbankname.Items.Clear();
                //objEChallan.TreasuryCode = ddllocation.SelectedValue;
                objEChallan.FillBanks(ddlbankname);
                dialog.Visible = false;
                PayuDialog.Visible = false;
                ddlbankname.Enabled = true;
                objEChallan.TreasuryCode = ddllocation.SelectedValue;
            }
            else if (rblpaymenttype.SelectedValue == "4")
            {
                ddlbankname.Items.Clear();
                divbank.Visible = false;
                PayuDialog.Visible = false;
                FillBanks();
            }
            else if (rblpaymenttype.SelectedValue == "5")
            {
                ddlbankname.Items.Clear();
                divbank.Visible = false;
                ddlbankname.Enabled = true;
                objEChallan.GetChallanBanks_Payu(ddlbankname);
                //ddlbankname.DataValueField = ConfigurationManager.AppSettings["Epay"].ToString();
                //ddlbankname.DataValueField = ConfigurationManager.AppSettings["PAYU"].ToString();
                PayuDialog.Visible = true;
                dialog.Visible = true;
            }
            else if (rblpaymenttype.SelectedValue == "6")
            {
                ddlbankname.Items.Clear();
                divbank.Visible = false;
                dialog.Visible = false;
                PayuDialog.Visible = false;
                ddlbankname.Enabled = true;
                objEChallan.GetUpiBankList(ddlbankname);
                //divUPI.Visible = true;
                //FillBanks();
                //ddlbankname.Enabled = false;
            }
        }
    }

    protected void FillBanks()
    {
       
        EgEChallanBL objE = new EgEChallanBL();
        ddlbankname.Items.Clear();
        DataTable dt = objE.GetChallanBank();
        DataRow[] result = dt.Select("access = 'D'");
        foreach (DataRow row in result)
        {
            ActiveDeActiveBank = row[1].ToString();
        }
       var rows = dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() == "Y" || t.Field<string>("access").Trim() == "D");
        dt = rows.Any() ? rows.CopyToDataTable() : dt.Clone(); //dt.AsEnumerable().Where(t => t.Field<string>("access").Trim() == "Y").CopyToDataTable();
        

        ddlbankname.DataSource = dt;
        ddlbankname.DataTextField = "BankName";
        ddlbankname.DataValueField = "BSRCode";
        ddlbankname.DataBind();
        ddlbankname.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        ddlbankname.Enabled = true;
        dialog.Visible = false;
        
        //DisableInActiveSBIBanks(ActiveDeActiveBank);
        DisableInActiveBanks();   // call Method for to Check  Block BankService 29 may 2019
    }


    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue != "0")
        {
            DataTable CTDTable = HttpContext.Current.Cache["CTD"] as DataTable;
            var rows = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("1") && t.Field<string>("Parent_Zone") == ddlZone.SelectedValue);
            DataTable dt = rows.Any() ? rows.CopyToDataTable() : CTDTable.Clone();//CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("1") && t.Field<string>("Parent_Zone") == ddlZone.SelectedValue).CopyToDataTable();
            ddlCircle.DataSource = dt;
            ddlCircle.DataTextField = "Zone_Circle_Ward";
            ddlCircle.DataValueField = "Location_Code";
            ddlCircle.DataBind();
            ddlCircle.Items.Insert(0, new ListItem("--Select Circle--", "0"));
        }
    }
    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue != "0")
        {
            DataTable CTDTable = HttpContext.Current.Cache["CTD"] as DataTable;
            var rows = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("3") && t.Field<string>("Parent_Circle") == ddlCircle.SelectedValue);
            DataTable dt = rows.Any() ? rows.CopyToDataTable() : CTDTable.Clone();

            ddlWard.DataSource = dt;
            ddlWard.DataTextField = "Zone_Circle_Ward";
            ddlWard.DataValueField = "Location_Code";
            ddlWard.DataBind();
            ddlWard.Items.Insert(0, new ListItem("--Select Ward--", "0"));
        }
    }
    protected void txtTIN_TextChanged(object sender, EventArgs e)
    {
        Regex r = new Regex("^[a-zA-Z0-9\\//]*$");
        if (r.IsMatch(txtTIN.Text.Trim()) && (txtTIN.Text.Length == 10 || txtTIN.Text.Length == 11) && ViewState["DeptCode"].ToString().Trim() == "18")
        {
            ValidateTin();
        }
        else if (ViewState["DeptCode"].ToString().Trim() == "18")
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please enter valid Tin No!')", true);
            txtTIN.Text = "";
        }


        else if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Enter Vehicle No!')", true);
            return;
        }


    }

    protected void ValidateTin()
    {
        try
        {
            EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
            objCheck.BudgetHead = ViewState["majorHead"].ToString();
            objCheck.DeptCode = Convert.ToInt32(ViewState["DeptCode"]);
            if (objCheck.CheckCTDCase() == true)
            {

                CTDWebServhttps.IFMSValidationService objWeb = new CTDWebServhttps.IFMSValidationService();
                SbiEncryptionDecryption objEncry = new SbiEncryptionDecryption();
                SetPeriod();
                string FromDate = objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(1).ToString() + "/" + objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(0) + "/" + objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(2);
                string groupCode = Session["UserId"].ToString() == "73" ? dt1.Rows[0][4].ToString() : dt1.Rows[0][5].ToString();
                string EncData = "User=IFMSAdmin|Password=IFMSPassword|Tin=" + txtTIN.Text + "|GroupCode=" + groupCode + "|FromDate= " + FromDate;
                string cipherText = objEncry.Encrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"aes.key");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string returnData = objWeb.validateTIN(cipherText);
                if (returnData != "0")
                {

                    string plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"]+"aes.key");

                    List<string> lstPlainText = new List<string>();
                    string[] arrMsgs = plainText.Split('|');
                    string[] arrIndMsg;
                    for (int i = 0; i < arrMsgs.Length; i++)
                    {
                        arrIndMsg = arrMsgs[i].Split('=');
                        lstPlainText.Add(arrIndMsg[0]);
                        lstPlainText.Add(arrIndMsg[1]);
                    }
                    if (lstPlainText[1] != "0" && lstPlainText[15] == "true")
                    {


                        if (lstPlainText[13] != "true")
                        {
                            txtRemark.Text = "Rc Effective Date is Not Valid";
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Rc Effective Date is Not Valid')", true);
                        }

                        DataTable CTDTable = HttpContext.Current.Cache["CTD"] as DataTable;
                        var rowsZone = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[3]));
                        DataTable CTDZone = rowsZone.Any() ? rowsZone.CopyToDataTable() : CTDTable.Clone(); //CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[3])).CopyToDataTable();
                        var rowsCircle = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[5]));
                        DataTable CTDCircle = rowsCircle.Any() ? rowsCircle.CopyToDataTable() : CTDTable.Clone(); //CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[5])).CopyToDataTable();
                        var rowsWard = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[7]));
                        DataTable CTDWard = rowsWard.Any() ? rowsWard.CopyToDataTable() : CTDTable.Clone(); //CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[7])).CopyToDataTable();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Remitter Name is: '+ '" + lstPlainText[9].ToString() + "')", true);
                        txtName.Text = lstPlainText[9].ToString();
                        txtaddress.Text = lstPlainText[11].ToString();
                        txtName.Enabled = false;
                        txtaddress.Enabled = false;
                        ddlPeriod.Enabled = false;
                        txtTIN.Enabled = false;
                        //ddlcity.Enabled = false;
                        //SetCTDOfficeWiseTreasury(400);

                        trCTD.Visible = true;
                        ddlZone.Items.Clear();
                        ddlCircle.Items.Clear();
                        ddlWard.Items.Clear();
                        ddlZone.Items.Insert(0, new ListItem(CTDZone.Rows[0][0].ToString(), lstPlainText[3]));
                        ddlCircle.Items.Insert(0, new ListItem(CTDCircle.Rows[0][0].ToString(), lstPlainText[5]));
                        ddlWard.Items.Insert(0, new ListItem(CTDWard.Rows[0][0].ToString(), lstPlainText[7]));
                    }
                    else
                    {
                        //if (Session["UserID"].ToString().Trim() == "73")
                        //{
                        DataTable CTDTable = HttpContext.Current.Cache["CTD"] as DataTable;
                        var rows = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0"));
                        DataTable dt = rows.Any() ? rows.CopyToDataTable() : CTDTable.Clone(); //CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0")).CopyToDataTable();
                        ddlZone.DataSource = dt;
                        ddlZone.DataTextField = "Zone_Circle_Ward";
                        ddlZone.DataValueField = "Location_Code";
                        ddlZone.DataBind();
                        ddlZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
                        //}
                        //else
                        //{
                        //ddlZone.Items.Clear();
                        //}
                        txtTIN.Text = "";
                        ddlCircle.Items.Clear();
                        ddlWard.Items.Clear();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Invalid Tin No.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('We are unable to connect CTD Web Portal.')", true);
                    txtTIN.Text = "";
                    txtTIN.Focus();
                }
            }
            txtamountwords.Text = HiddenAmount.Value;
        }
        catch (Exception ex)
        {
            

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('We are unable to connect CTD Web Portal.')", true);
            txtTIN.Text = "";
            txtTIN.Focus();
            txtamountwords.Text = HiddenAmount.Value;
            return;
        }

    }
    [WebMethod]
    public static string EncryptData(string grn, string totamt)
    {
        EncryptDecryptionBL objenc = new EncryptDecryptionBL();
        var comm = grn + '-' + totamt;
        comm = objenc.Encrypt(comm);
        return comm;
    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["_ViewState"];
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        Session["_ViewState"] = viewState;
    }
    private void DisableInActiveBanks()
    {
        Dictionary<string, bool> items = null;
        try
        {
            using (System.IO.StreamReader r = new System.IO.StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/InActiveBanks.json")))
            {
                string JSON = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Dictionary<string, bool>>(JSON);
            }
            for (int i = 0; i < items.Count; i++)
            {
                try
                {      // ddlbankname.Items.FindByValue(items.ElementAt(i).Key).Attributes["disabled"] = "disabled";
                    ddlbankname.Items.FindByValue(items.ElementAt(i).Key).Attributes.Add("style", "color:orangered;background-color:wheat;");

                    ddlbankname.Items.FindByValue(items.ElementAt(i).Key).Attributes["title"] = ConfigurationManager.AppSettings["ServiceDown"].ToString();

                    //ddlbankname.Attributes.Add("onmouseover", _"this.title=this.options[this.selectedIndex].title");
                }
                catch (Exception ex)
                { }
            }
        }
        catch (System.IO.IOException es)
        {

        }


    }

    //private void DisableInActiveSBIBanks(string Bank)
    //{
    //    if (Bank != null)
    //    {
    //        ddlbankname.Items.FindByValue(Bank).Attributes["disabled"] = "disabled";
    //        ddlbankname.Items.FindByValue(Bank).Attributes["title"] = "SBI Bank Service will not be  Available 20:00 Hours To 00:00 Hours.";
    //    }
    //}
    
}
