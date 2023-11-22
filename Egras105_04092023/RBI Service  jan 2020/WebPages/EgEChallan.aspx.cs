using EgBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_EgEChallan : System.Web.UI.Page
{
    EgEChallanBL objEChallan;//= new EgEChallanBL();
    EgEncryptDecrypt ObjEncrytDecrypt;
    // DataTable schemaAmtTable = new DataTable();
    DataTable dt1;//= new DataTable();
    string Profilee, GRN, Type;
    int DeptCode, ServiceId, ProcUserId;
    string ActiveDeActiveBank;
    //bool multiplebudgetheadflag = false;                             // Multiple Head Process(Procurement Department)  There Challan would be  create with  service (specific  Service) with Multiple Heads 
    // On Date 21 April 2020 
    string[] BudgetHead;      // Check 8443001030000||108||109 In Procurement Challan
    decimal total = 0;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        objEChallan = new EgEChallanBL();
        try
        {
            if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
            {
                Response.Redirect("~\\LoginAgain.aspx");
            }

            if (!IsPostBack)
            {
                //btnBasicInformation.Attributes.Add("data-toggle", "modal");
                //btnBasicInformation.Attributes.Add("data-target", "#myModal");


                dt1 = new DataTable();
                objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
                if (Request.QueryString.Count > 0)
                {
                    string strReqq = Request.Url.ToString();
                    strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
                    ObjEncrytDecrypt = new EgEncryptDecrypt();

                    List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);

                    if (strList != null)
                    {
                        if (strList.Count > 0)
                        {
                            if (strList[0].ToString() == "Profile")
                            {
                                objEChallan.UserPro = Convert.ToInt32(strList[1].ToString());
                                HdnProfile.Value = strList[1].ToString();
                            }
                            else if (strList[0].ToString() == "Service")
                            {
                                ServiceId = Convert.ToInt32(strList[1].Split('|').GetValue(1));
                                hdnServiceId.Value = ServiceId.ToString();
                                DeptCode = Convert.ToInt32(strList[1].Split('|').GetValue(2));
                                ProcUserId = Convert.ToInt32(strList[1].Split('|').GetValue(3));
                                hdnProcUserId.Value = ProcUserId.ToString();

                            }
                            else
                            {
                                Type = strList[3].ToString();
                                objEChallan.GRNNumber = Convert.ToInt64(strList[1].ToString());
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


                    txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    dt1 = (ServiceId > 0) ? objEChallan.GetServiceSchema(ServiceId, DeptCode) : objEChallan.GetSchema();

                    ViewState["Schema"] = dt1;
                    txtDept.InnerText = dt1.Rows[0][3].ToString();
                    spanbhdepartmentname.InnerText = dt1.Rows[0][3].ToString();
                    spanDepartment.InnerText = dt1.Rows[0][3].ToString();


                    //txtDept.Enabled = false;
                    ViewState["DeptCode"] = dt1.Rows[0][4].ToString();

                    ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                    //spancommission.Visible = ViewState["DeptCode"].ToString() == "86" ? true : false;
                    
                    //objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
                    Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());
                    objEChallan.FillLocation(ddldistrict);
                    //FillLocation();  // deptcode
                    DropDownListX dp = new DropDownListX();
                    DataTable treasuryData = new DataTable();
                    treasuryData = dp.FillTreasury();
                    for (int i = 1; i < 41; i++)
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
                    MajorHeadWiseCheck(dt1.Rows[0][1].ToString().Substring(0, 13).ToString(), Convert.ToInt32(dt1.Rows[0][4].ToString()), ddlZone, objEChallan.GRNNumber > 0 ? "repeat" : "profile");
                }
            }
        }
        catch (Exception ex)
        {
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(ex.Message + "btnRedirect" + Session["UserId"].ToString() + Profilee);
        }
    }

    #region Template Section

    #region Next Section
    protected void btnBasicInformation_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            budgethead1.Attributes.Add("class", "active");
            fldBasic.Visible = false;
            fldPersonal.Visible = false;
            fldBudget.Visible = true;
            fldBank.Visible = false;
            divpantan.InnerHtml = Session["UserType"].ToString() == "4" ? "TAN" : "Permanent Account Number(PAN)";
            divBHead.InnerHtml = CreateBudgetheadSection();
            //ScriptManager.RegisterStartupScript(this, GetType(), "addScript", "StampPopup();", true);

            if (Convert.ToInt32(ViewState["DeptCode"]) == 86)
            {
                DataTable dt = new DataTable();
                DataTable dtschema = new DataTable();
                dt = objEChallan.CheckStamp10PercentCaseWithBH();
                dtschema = (DataTable)ViewState["Schema"];
                try
                {
                    DataTable matched = (from table1 in dt.AsEnumerable()
                                         join table2 in dtschema.AsEnumerable()
                                         on table1.Field<String>("BudgetHead")
                                         equals table2.Field<String>("BudgetHead").Substring(0, 13)
                                         select table1).CopyToDataTable();

                    if (matched.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "addScript", "StampPopup();", true);
                    }
                }
                catch { }
            }
        }
    }
    protected void btnBudgetHead_Click(object sender, EventArgs e)
    {
        spanBudgetHeadMsg.Attributes.Add("style", "display:none");
        //spanBudgetHeadMsg.Visible = false;

        if (Page.IsValid)
        {

            //ViewState["BudgetHeadSchema"] = AmountSave();
            pdvisible.InnerText = divPD.Visible == true ? "true" : "false";
            #region Amount And BudgetHead Check Section

            objEChallan.UserType = Session["UserType"].ToString();
            objEChallan.dtSchema = AmountSave();
            ViewState["BudgetHeadSchema"] = objEChallan.dtSchema;

            objEChallan.DeductCommission = Convert.ToDouble(txtDeduct.Text);
            objEChallan.DeptCode = Convert.ToInt32(objEChallan.dtSchema.Rows[0][0].ToString());
            objEChallan.BudgetHead = objEChallan.dtSchema.Rows[0][4].ToString();
            //objEChallan.TotalAmount = (Convert.ToDouble(objEChallan.dtSchema.Compute("SUM(amount)", String.Empty)) - (objEChallan.DeductCommission)).ToString();
            double totalamount = Convert.ToDouble(objEChallan.dtSchema.Compute("SUM(amount)", String.Empty));
            double resPercent =  ((totalamount*20)/100);
            if (Convert.ToDouble(objEChallan.DeductCommission) > resPercent)
            {
                txttotalAmount.Text = objEChallan.dtSchema.Compute("SUM(amount)", String.Empty).ToString();
                txtDeduct.Text = "00";
            }
            else
            {
                txttotalAmount.Text = (Convert.ToDouble(objEChallan.dtSchema.Compute("SUM(amount)", String.Empty)) - (objEChallan.DeductCommission)).ToString();
            }
            objEChallan.TotalAmount = objEChallan.dtSchema.Compute("SUM(amount)", String.Empty).ToString();
            //  objEChallan.UserType = Session["UserType"].ToString();
            objEChallan.PanNumber = txtPanNo.Text;
            objEChallan.budgetheadcount = objEChallan.dtSchema.Rows.Count;

            if (objEChallan.CheckBudgetHeadLevelCondition())
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Message", "alert('" + objEChallan.msg + "')", true);
                //spanBudgetHeadMsg.InnerText = objEChallan.msg;
                //spanBudgetHeadMsg.Visible = true;
                assignAmountToBH();
                spanBudgetHeadMsg.InnerText = objEChallan.msg;
                spanBudgetHeadMsg.Attributes.Add("style", "display:block");
                //spanBudgetHeadMsg.Visible = true;
            }
            else
            {
                personal.Attributes.Add("class", "active");
                fldBasic.Visible = false;
                fldPersonal.Visible = true;
                fldBudget.Visible = false;
                fldBank.Visible = false;
            }
            #endregion
        }
    }
    protected void btnPersonal_Click(object sender, EventArgs e)
    {
        int output = 0;
        if (Page.IsValid)
        {

            if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty && hdnProcUserId.Value.ToString() != "-1")
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Enter Vehicle No!')", true);
                spanTin.Visible = ViewState["DeptCode"].ToString().Trim() == "104" ? true : false;
                spanTin.InnerHtml = ViewState["DeptCode"].ToString().Trim() == "104" ? "Please enter vehicle no !" : "";
                txtTIN.Focus();
                return;
            }
            else
            {
                //string strReqq = Request.Url.ToString();
                //strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
                //ObjEncrytDecrypt = new EgEncryptDecrypt();
                //List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
                //if (strList[0].ToString() == "Profile")
                //{
                //    objEChallan.Profile = Convert.ToInt32(strList[1].ToString());
                //}
                //else if (strList[0].ToString() == "Service")
                //{
                //    objEChallan.Serviceid = Convert.ToInt32(strList[1].Split('|').GetValue(1)); ;
                //    objEChallan.ProcUserId = Convert.ToInt32(strList[1].Split('|').GetValue(3));
                //    objEChallan.Profile = 1;
                //}
                //else
                //{
                //objEChallan.Profile = Convert.ToInt32(HdnProfile.Value);
                //}
                objEChallan.Serviceid = hdnServiceId.Value.Length > 0 ? Convert.ToInt32(hdnServiceId.Value) : 0;
                objEChallan.ProcUserId = hdnProcUserId.Value.Length > 0 ? Convert.ToInt32(hdnProcUserId.Value) : 0;
                //objEChallan.Profile = 1;
                objEChallan.Profile = (HdnProfile.Value.Length == 0 && hdnServiceId.Value.Length > 0) ? 1 : Convert.ToInt32(HdnProfile.Value);
                if (objEChallan.ProcUserId == -1 && rblpayMode.SelectedValue.ToString() == "M")
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('EProc Tender Fee Submit Only Through E-Banking');", true);
                    spanPersonalMsg.InnerText = "EProc Tender Fee Submit Only Through Net-Banking";
                    spanPersonalMsg.Attributes.Add("style", "display:block");
                    return;
                }
                EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
                objCheck.BudgetHead = ViewState["majorHead"].ToString();
                objCheck.DeptCode = Convert.ToInt32(ViewState["DeptCode"]);
                objCheck.payMode = rblpayMode.SelectedValue.ToString();
                objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
                objCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
                objCheck.pdVisible = Convert.ToBoolean(pdvisible.InnerText);
                //objCheck.treasuryCodeBank = ddlbankname.SelectedValue.ToString();
                //objCheck.treasuryCodePd = ddlpdAccount.SelectedValue.ToString();
                objCheck.isPD = ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true;
                objCheck.proc_id = objEChallan.ProcUserId;
                //BudgetHead = ViewState["BudgetHead"] as string[];
                objCheck.serviceBudgetHead = ViewState["BudgetHead"] as string[];
                if (objCheck.CheckSubmitCondition() == true)
                {
                    spanPersonalMsg.InnerText = objCheck.msg;
                    spanPersonalMsg.Attributes.Add("style", "display:block");
                    return;
                }
                else
                {
                    if (objCheck.CheckCTDCase() == true)
                    {
                        objEChallan.Zone = ddlZone.SelectedValue;
                        objEChallan.Circle = ddlCircle.SelectedValue;
                        objEChallan.Ward = ddlWard.SelectedValue;
                        objEChallan.Identity = txtTIN.Text;
                    }
                    if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty && hdnProcUserId.Value.ToString() != "-1")    // Add Condition For Vehicle No Compulsory  30/7/2018
                    {
                        spanTin.Visible = ViewState["DeptCode"].ToString().Trim() == "104" ? true : false;
                        spanTin.InnerHtml = ViewState["DeptCode"].ToString().Trim() == "104" ? "Please enter vehicle no !" : "";
                        txtTIN.Focus();
                        return;
                    }
                    else
                    {
                        objEChallan.Identity = txtTIN.Text;
                    }
                    objEChallan.TypeofPayment = rblpayMode.SelectedValue;
                    objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    objEChallan.dtSchema = (DataTable)ViewState["BudgetHeadSchema"];
                    objEChallan.DeductCommission = Convert.ToDouble(txtDeduct.Text);
                    objEChallan.DeptCode = Convert.ToInt32(objEChallan.dtSchema.Rows[0][0].ToString());
                    objEChallan.BudgetHead = objEChallan.dtSchema.Rows[0][4].ToString();
                    //objEChallan.TotalAmount = (Convert.ToDouble(objEChallan.dtSchema.Compute("SUM(amount)", String.Empty)) - (objEChallan.DeductCommission)).ToString();
                    objEChallan.TotalAmount = objEChallan.dtSchema.Compute("SUM(amount)", String.Empty).ToString();
                    objEChallan.PanNumber = txtPanNo.Text;
                    objEChallan.budgetheadcount = objEChallan.dtSchema.Rows.Count;
                    objEChallan.fldpersonal = true;
                    if (objEChallan.CheckBudgetHeadLevelCondition())
                    {
                        assignAmountToBH();
                        spanBudgetHeadMsg.InnerText = objEChallan.msg;
                        spanBudgetHeadMsg.Attributes.Add("style", "display:block");
                        return;
                    }
                    objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
                    objEChallan.PanNumber = txtPanNo.Text;
                    objEChallan.Location = ddllocation.SelectedValue;
                    objEChallan.FullName = txtName.Text;
                    objEChallan.Address = txtaddress.Text;
                    objEChallan.City = txtCity.Text.Trim();
                    objEChallan.MobileNo = txtMobileNo.Text;
                    objEChallan.PINCode = "000000"; //txtPin.Text;
                    objEChallan.DeductCommission = double.Parse(txtDeduct.Text);
                    objEChallan.TotalAmount = txttotalAmount.Text;
                    objEChallan.ChequeDDNo = txtChequeDDNo.Text;
                    objEChallan.Remark = txtRemark.Text;
                    string Details = HiddenField1.Value; //SubmitExtraDetails();// HiddenField1.Value; //
                    if (Details != "")
                    {
                        objEChallan.Details = Details;
                    }
                    objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
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
                    string Fdate = "";
                    string Tdate = "";
                    if (txtfromdate.Text != "" && txttodate.Text != "")
                    {
                        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
                        Fdate = Convert.ToString(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                        string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
                        Tdate = Convert.ToString(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
                    }
                    objEChallan.ChallanFromMonth = Convert.ToDateTime(Fdate.ToString());
                    objEChallan.ChallanToMonth = Convert.ToDateTime(Tdate.ToString());

                    // InsertData();
                    output = objEChallan.InsertChallan();
                    Session["GrnNumber"] = Convert.ToString(output);
                    Session["NetAmount"] = objEChallan.RetTotalAmount;
                    if (output <= 0)
                    {
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Not Saved')", true);
                        spanPersonalMsg.InnerText = "Record Not Saved";
                        spanPersonalMsg.Attributes.Add("style", "display:block");
                    }
                    if (Convert.ToInt64(Session["GrnNumber"]) > 0)
                    {
                        bank.Attributes.Add("class", "active");
                        fldBasic.Visible = false;
                        fldPersonal.Visible = false;
                        fldBudget.Visible = false;
                        fldBank.Visible = true;
                        txtChequeDDNo.Text = string.Empty;
                        divUPI.Visible = false;
                        divPopularBanks.Visible = true;
                        divPopularPG.Visible = false;


                        spanOffice.InnerText = ddlOfficeName.SelectedItem.ToString();
                        spanTreasury.InnerText = ddllocation.SelectedItem.ToString();
                        spanGRN.InnerText = Session["GrnNumber"].ToString();


                        if (rblpayMode.SelectedValue.Trim() == "M")
                        {
                            this.rblpaymenttype.Items[1].Attributes.Add("class", "");
                            this.rblpaymenttype.Items[2].Attributes.Add("class", "");
                            this.rblpaymenttype.Items[0].Attributes.Add("class", "");
                            this.rblpaymenttype.Items[3].Attributes.Add("class", "active");

                            this.rblpaymenttype.Items[1].Enabled = false;
                            this.rblpaymenttype.Items[2].Enabled = false;
                            this.rblpaymenttype.Items[0].Enabled = false;
                            this.rblpaymenttype.Items[3].Enabled = true;



                            dialog.Visible = false;
                            //PayuDialog.Visible = false;
                            ddlbankname.Enabled = true;
                            divManualBankSection.Visible = true;
                            txtChequeDDNo.Visible = true;
                            divWriteChequeNoLable.Visible = true;
                            divBank.Visible = true;
                            divPopularBanks.Visible = false;
                            divPopularPG.Visible = false;
                            rblpaymenttype.SelectedValue = "3";


                            EgCheckChallanCondition objManualAnuWhereBranchCheck = new EgCheckChallanCondition();
                            objManualAnuWhereBranchCheck.pdAccountNo = ddlpdAccount.Items[0].ToString();// Passing value of Select Statement to Check Wether PD or Division Case
                            objManualAnuWhereBranchCheck.treasuryCodeBank = ddllocation.SelectedValue;
                            objManualAnuWhereBranchCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
                            objEChallan.TreasuryCode = ddllocation.SelectedValue;
                            if (objManualAnuWhereBranchCheck.CheckManualBanksForAnyWhereBranch())// ADDED by Sandeep for Manual Banks SBIAnyWhere
                                objEChallan.Type = -1;
                            else
                                objEChallan.Type = 0;
                            ddlbankname.Items.Clear();
                            objEChallan.FillBanks(ddlbankname);
                            objEChallan.TreasuryCode = ddllocation.SelectedValue;

                        }
                        else
                        {
                            this.rblpaymenttype.Items[1].Attributes.Add("class", "");
                            this.rblpaymenttype.Items[2].Attributes.Add("class", "");
                            this.rblpaymenttype.Items[0].Attributes.Add("class", "active");
                            this.rblpaymenttype.Items[3].Attributes.Add("class", "");

                            this.rblpaymenttype.Items[1].Enabled = true;
                            this.rblpaymenttype.Items[2].Enabled = true;
                            this.rblpaymenttype.Items[0].Enabled = true;
                            this.rblpaymenttype.Items[3].Enabled = false;

                            rblpaymenttype.SelectedValue = "4";
                            dialog.Visible = false;
                            divManualBankSection.Visible = false;
                            txtChequeDDNo.Visible = false;
                            divWriteChequeNoLable.Visible = false;

                            FillBanks();
                        }

                        //this.rblpaymenttype.Items[2].Attributes.Add("class", "");
                        //this.rblpaymenttype.Items[0].Attributes.Add("class", "active");
                        //this.rblpaymenttype.Items[1].Attributes.Add("class", "");
                        //this.rblpaymenttype.Items[3].Attributes.Add("class", "");

                        //rblpaymenttype.SelectedValue = "4";
                        //dialog.Visible = false;
                        //divManualBankSection.Visible = false;
                        //txtChequeDDNo.Visible = false;
                        //divWriteChequeNoLable.Visible = false;
                        btnBank.Text = "PAY- ₹" + Convert.ToDouble(Session["NetAmount"]).ToString("#.##");
                        //FillBanks();
                    }
                }
            }
        }
    }
    //protected void btnReVerify_Click(object sender, EventArgs e)
    //{
    //    PayTmBL objpaytm = new PayTmBL();
    //    objpaytm.GRN = Session["GrnNumber"].ToString();
    //    var returndata = objpaytm.TransactionStatusAPI();
    //    if (returndata == "P")
    //    {
    //        btnReVerify.Visible = true;
    //        //btnBank.Enabled = false;
    //        spanpay.Visible = true;
    //        spanpay.InnerText = "Transaction Status Pending";
    //    }
    //    else if (returndata == "S")
    //    {
    //        btnReVerify.Visible = false;
    //        //btnBank.Enabled = false;
    //        spanpay.Visible = true;
    //        spanpay.InnerText = "Transaction Successfull";
    //    }
    //    else
    //    {
    //        btnReVerify.Visible = false;
    //        //btnBank.Enabled = true;
    //        spanpay.Visible = true;
    //        spanpay.InnerText = returndata;
    //    }
    //}
    protected void btnBank_Click(object sender, EventArgs e)
    {
        fldBasic.Visible = false;
        fldPersonal.Visible = false;
        fldBudget.Visible = false;
        divBankMsg.Visible = false;
        spanpay.Visible = false;
       // btnReVerify.Visible = false;
        divBankMessage.Visible = false;


        divBankMsg.InnerText = "";
        #region challanview

        if (Page.IsValid)
        {
            EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
            objCheck.BudgetHead = ViewState["majorHead"].ToString();
            //objCheck.payMode = rblpaymenttype.SelectedValue.ToString();
            if (rblpaymenttype.SelectedValue.ToString() == "4" || rblpaymenttype.SelectedValue.ToString() == "5" || rblpaymenttype.SelectedValue.ToString() == "6")
                objCheck.payMode = "N";
            else
                objCheck.payMode = "M";
            objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
            objCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
            objCheck.pdVisible = ddlpdAccount.Visible;
            objCheck.treasuryCodeBank = ddlbankname.SelectedValue.ToString();
            objCheck.treasuryCodePd = ddlpdAccount.SelectedValue.ToString();
            objCheck.isPD = ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true;
            objCheck.proc_id = hdnProcUserId.Value.Length > 0 ? Convert.ToInt32(hdnProcUserId.Value) : 0;
            BudgetHead = ViewState["BudgetHead"] as string[];
            objCheck.serviceBudgetHead = BudgetHead;

            if ((ddlbankname.SelectedValue == "0" && rblBank.SelectedValue == "" && rblpaymenttype.SelectedValue == "4") ||
                (ddlbankname.SelectedValue == "0" && rblpaymenttype.SelectedValue == "5" && rblPG.SelectedValue == "") ||
                (rblpaymenttype.SelectedValue == "3" && ddlbankname.SelectedValue == "0")
                )
            {
                divBankMessage.Visible = true;
                divBankMessage.InnerText = "Please Select Mode Of Payment !";
            }
            else if (rblpaymenttype.SelectedValue == "6" && (
                Convert.ToInt32(rblUpi.SelectedValue) <= 0 ||
                string.IsNullOrEmpty(txtUpi.Text) ||
                string.IsNullOrEmpty(drpUpiID.SelectedValue))
                )
            {
                divBankMsg.Visible = true;
                divBankMsg.InnerText = "Please Enter UPI ID or Select Payment Mode !";
            }
            else if (objCheck.CheckSubmitCondition() == true)
            {
                divBankMessage.InnerText = objCheck.msg;
                divBankMessage.Visible = true;
            }
            //selection of  DD or Cheque DD No or Cheque No  is Mandatory otherwise bydefault cash 29 July  2019
            else if (rblpaymenttype.SelectedValue.Trim() == "3" && rblCashCheque.SelectedValue.Trim() == "2" && txtChequeDDNo.Text.Trim() == "")
            {
                divBankMessage.InnerText = "Please Fill Cheque or DD No !!";
                divBankMessage.Visible = true;
            }
            else if (hdnProcUserId.Value == "-1" && rblpaymenttype.SelectedValue.ToString() == "3")
            {
                spanPersonalMsg.InnerText = "EProc Tender Fee Submit Only Through Net-Banking";
                spanPersonalMsg.Attributes.Add("style", "display:block");
                return;
            }
            else
            {
                objEChallan.GRNNumber = Convert.ToInt64(Session["GrnNumber"]);
                objEChallan.TypeofPayment = rblpaymenttype.SelectedValue == "3" ? "M" : "N";
                string bankname = string.Empty;
                string UpiId = string.Empty;
                if (rblpaymenttype.SelectedValue == "3")
                {
                    bankname = ddlbankname.SelectedValue.ToString().Substring(0, 7);
                }
                else if (rblpaymenttype.SelectedValue == "4")
                {
                    bankname = Convert.ToInt32(ddlbankname.SelectedValue) > 0 ? ddlbankname.SelectedValue.ToString() : rblBank.SelectedValue;
                }
                else if (rblpaymenttype.SelectedValue == "5")
                {
                    bankname = Convert.ToInt32(ddlbankname.SelectedValue) > 0 ? ddlbankname.SelectedValue.ToString() : rblPG.SelectedValue;
                }
                else if (rblpaymenttype.SelectedValue == "6")
                {
                    bankname = rblUpi.SelectedValue.Substring(0, 7);
                    UpiId = txtUpi.Text + drpUpiID.SelectedItem;
                    objEChallan.PayMode = "UPI";

                }
                objEChallan.BankName = bankname;
                objEChallan.ChequeDDNo = txtChequeDDNo.Text;
                objEChallan.TotalAmount = Session["NetAmount"].ToString();
                var output = objEChallan.InsertBankDetail();
                if (output > 0)
                {
                    ObjEncrytDecrypt = new EgEncryptDecrypt();
                    if (rblpaymenttype.SelectedValue == "3")
                    {
                        Response.Redirect("~/webpages/reports/EgManualChallan.aspx");

                    }
                    else if (rblpaymenttype.SelectedValue == "6")
                    {
                        if (objEChallan.BankName == "9930001")
                        {
                            //Response.Redirect("testpaytm.aspx", false);

                            //Context.ApplicationInstance.CompleteRequest();
                            PayTmBL objpaytm = new PayTmBL();
                            objpaytm.NetAmount = Convert.ToDouble(Session["NetAmount"]).ToString("#.##");
                            objpaytm.UserId = Session["UserId"].ToString();
                            objpaytm.BudgetHead = ViewState["BudgetHead"] as string[];
                            objpaytm.GRN = Session["GrnNumber"].ToString();
                            objpaytm.UpiId = UpiId;
                            var returndata = objpaytm.InitiateTransactionAPI();

                            if (returndata == "P")
                            {
                                //btnReVerify.Visible = true;
                                //btnBank.Enabled = false;
                                spanpay.Visible = true;
                                spanpay.InnerText = "Transaction Status Pending";
                            }
                            if (returndata == "S")
                            {
                                //btnReVerify.Visible = false;
                                //btnBank.Enabled = false;
                                spanpay.Visible = true;
                                spanpay.InnerText = "Transaction Successfull";
                                RemoteClass myremotepost = new RemoteClass();

                                Response.Redirect("ABC1.aspx");
                            }
                            else
                            {
                                //btnReVerify.Visible = false;
                                //btnBank.Enabled = true;
                                spanpay.Visible = true;
                                spanpay.InnerText = returndata;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Your GRN Is " + Session["GrnNumber"] + " ! we are redirecting you at other site for payment !'); window.location='EgEChallanView.aspx';", true);
                    }
                }
            }
            this.rblpaymenttype.Items[0].Attributes.Add("class", rblpaymenttype.SelectedValue == "4" ? "active" : "");
            this.rblpaymenttype.Items[1].Attributes.Add("class", rblpaymenttype.SelectedValue == "5" ? "active" : "");
            this.rblpaymenttype.Items[3].Attributes.Add("class", rblpaymenttype.SelectedValue == "3" ? "active" : "");
            this.rblpaymenttype.Items[2].Attributes.Add("class", rblpaymenttype.SelectedValue == "6" ? "active" : "");
        }
        #endregion
    }

    #endregion

    #region Previous Section
    protected void btnPrevBudgetHead_Click(object sender, EventArgs e)
    {
        budgethead1.Attributes.Add("class", "");
        fldBasic.Visible = true;
        fldPersonal.Visible = false;
        fldBudget.Visible = false;
        fldBank.Visible = false;
        txttotalAmount.Text = "00";
        txtDeduct.Text = "00";
        displyOff();
    }
    protected void btnPrevPersonal_Click(object sender, EventArgs e)
    {
        //setProgressBar(2, 5);
        personal.Attributes.Add("class", "");
        fldBasic.Visible = false;
        fldPersonal.Visible = false;
        fldBudget.Visible = true;
        fldBank.Visible = false;
        //txttotalAmount.Text = "00";
        //txtDeduct.Text = "00";
        displyOff();
        assignAmountToBH();
    }


    #endregion
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objEChallan.DeptCode = Convert.ToInt32(ViewState["DeptCode"].ToString());
            objEChallan.Tcode = ddldistrict.SelectedValue;
            objEChallan.FillOfficeList(ddlOfficeName);
            ddllocation.Items[0].Attributes.Add("style", "dislay:block");
            ddllocation.SelectedValue = "0";
            //ddldistrict.Focus();
        }
        catch (Exception ex)
        {
            Response.Redirect("EgErrorPage.aspx?error=" + ex.Message);
        }
    }
    protected void ddlOfficeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOfficeName.SelectedValue == "0")
        {
            ddllocation.Items[0].Attributes.Add("style", "dislay:block");
            ddllocation.SelectedValue = "0";
        }
        else
        {
            objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
            DataTable dt = objEChallan.FillOfficeWiseTreasury();
            if (dt.Rows.Count != 0)
            {
                ddllocation.SelectedValue = dt.Rows[0][1].ToString();
                //ddllocation.Items[0].Attributes.Add("style", "display:none");
            }
        }
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
            ddlWard.Items.Clear();
            ddlCircle.Items.Insert(0, new ListItem("--Select Circle--", "0"));
            ddlWard.Items.Insert(0, new ListItem("--Select Ward--", "0"));
        }
        else
        {
            ddlWard.Items.Clear();
            ddlCircle.Items.Clear();
            ddlCircle.Items.Insert(0, new ListItem("--Select Circle--", "0"));
            ddlWard.Items.Insert(0, new ListItem("--Select Ward--", "0"));
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
        else
        {
            ddlWard.Items.Clear();
            ddlWard.Items.Insert(0, new ListItem("--Select Ward--", "0"));
        }
        //assignAmountToBH();
    }

    protected void txtTIN_TextChanged(object sender, EventArgs e)
    {
        spanTin.Visible = false;

        //Regex ra = new Regex("^[a-zA-Z0-9\\//]*$");
        Regex r = new Regex("^[a-zA-Z0-9\\//]*$");
        Regex ra = new Regex("^[A-Za-z]{2}[0-9]{1,2}[a-zA-Z]{2}[0-9]{4}$");
        if (ViewState["DeptCode"].ToString().Trim() == "18" && !r.IsMatch(txtTIN.Text.Trim()))
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Enter Valid Tin No!')", true);
            spanTin.Visible = ViewState["DeptCode"].ToString().Trim() == "18" ? true : false;
            spanTin.InnerHtml = ViewState["DeptCode"].ToString().Trim() == "18" ? "Please Enter Valid Tin No!" : "";
            txtTIN.Text = "";
        }
        else if (r.IsMatch(txtTIN.Text.Trim()) && (txtTIN.Text.Length == 10 || txtTIN.Text.Length == 11) && ViewState["DeptCode"].ToString().Trim() == "18")
        {
            ValidateTin();
        }
        else if (ViewState["DeptCode"].ToString().Trim() == "104" && (!ra.IsMatch(txtTIN.Text.Trim()) || txtTIN.Text == string.Empty) && hdnProcUserId.Value.ToString() != "-1")
        {
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Enter Valid Vehicle No!')", true);
            spanTin.Visible = ViewState["DeptCode"].ToString().Trim() == "104" ? true : false;
            spanTin.InnerHtml = ViewState["DeptCode"].ToString().Trim() == "104" ? "Please Enter Valid Vehicle No (Ex:- RJ14KD8758)" : "";
            txtTIN.Text = string.Empty;
            txtTIN.Focus();
            return;
        }
        //txtaddress.Focus();
    }
    protected void rblUpi_SelectedIndexChanged(object sender, EventArgs e)
    {
        Dictionary<int, string> upiDataSource = new Dictionary<int, string>();
        this.rblpaymenttype.Items[0].Attributes.Add("class", "");
        this.rblpaymenttype.Items[1].Attributes.Add("class", "");
        this.rblpaymenttype.Items[2].Attributes.Add("class", "active");
        this.rblpaymenttype.Items[3].Attributes.Add("class", "");

        if (rblUpi.SelectedValue == "99300011")//paytm
        {
            upiDataSource.Add(0, "@paytm");

        }
        else if (rblUpi.SelectedValue == "99300012")
        {
            upiDataSource.Add(0, "@ybl");
            upiDataSource.Add(1, "@okhdfcbank");
            upiDataSource.Add(2, "@okicici");
            upiDataSource.Add(3, "@oksbi");
        }
        else if (rblUpi.SelectedValue == "99300013")
        {
            upiDataSource.Add(0, "@okaxis");
            upiDataSource.Add(1, "@ibl");
            upiDataSource.Add(2, "@axl");
        }
        else
        {
            upiDataSource.Add(0, "@paytm");
        }
        spanBankName.InnerHtml = rblUpi.SelectedItem.ToString();
        drpUpiID.DataTextField = "Value";
        drpUpiID.DataValueField = "Key";
        drpUpiID.DataSource = upiDataSource;
        drpUpiID.DataBind();
    }
    protected void rblpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        divUPI.Visible = false;
        divBankMsg.Visible = false;
        divBankMsg.InnerText = "";
        divBankMessage.Visible = false;
        divBankMessage.InnerText = "";
        ddlbankname.Visible = true;
        spanBankName.InnerHtml = "";
        divCheque.Visible = false;
        spanpay.Visible = false;


        if (rblpaymenttype.SelectedValue == "3")//Manual
        {
            this.rblpaymenttype.Items[1].Attributes.Add("class", "");
            this.rblpaymenttype.Items[2].Attributes.Add("class", "");
            this.rblpaymenttype.Items[0].Attributes.Add("class", "");
            this.rblpaymenttype.Items[3].Attributes.Add("class", "active");
            //this.rblpaymenttype.Items[4].Attributes.Add("class", "");

            dialog.Visible = false;
            //PayuDialog.Visible = false;
            ddlbankname.Enabled = true;
            divManualBankSection.Visible = true;
            txtChequeDDNo.Visible = true;
            divWriteChequeNoLable.Visible = true;
            divBank.Visible = true;
            divPopularBanks.Visible = false;
            divPopularPG.Visible = false;

            EgCheckChallanCondition objManualAnuWhereBranchCheck = new EgCheckChallanCondition();
            objManualAnuWhereBranchCheck.pdAccountNo = ddlpdAccount.Items[0].ToString();// Passing value of Select Statement to Check Wether PD or Division Case
            objManualAnuWhereBranchCheck.treasuryCodeBank = ddllocation.SelectedValue;
            objManualAnuWhereBranchCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
            objEChallan.TreasuryCode = ddllocation.SelectedValue;
            if (objManualAnuWhereBranchCheck.CheckManualBanksForAnyWhereBranch())// ADDED by Sandeep for Manual Banks SBIAnyWhere
                objEChallan.Type = -1;
            else
                objEChallan.Type = 0;
            ddlbankname.Items.Clear();
            objEChallan.FillBanks(ddlbankname);
            objEChallan.TreasuryCode = ddllocation.SelectedValue;

        }
        else if (rblpaymenttype.SelectedValue == "4")//Netbanking
        {
            this.rblpaymenttype.Items[2].Attributes.Add("class", "");
            this.rblpaymenttype.Items[0].Attributes.Add("class", "active");
            this.rblpaymenttype.Items[1].Attributes.Add("class", "");
            this.rblpaymenttype.Items[3].Attributes.Add("class", "");
            //this.rblpaymenttype.Items[4].Attributes.Add("class", "");

            dialog.Visible = false;
            //PayuDialog.Visible = false;
            divManualBankSection.Visible = false;
            txtChequeDDNo.Visible = false;
            divWriteChequeNoLable.Visible = false;
            divBank.Visible = true;
            divPopularBanks.Visible = true;
            divPopularPG.Visible = false;

            FillBanks();
        }
        else if (rblpaymenttype.SelectedValue == "5")//PG
        {
            this.rblpaymenttype.Items[0].Attributes.Add("class", "");
            this.rblpaymenttype.Items[1].Attributes.Add("class", "active");
            this.rblpaymenttype.Items[2].Attributes.Add("class", "");
            this.rblpaymenttype.Items[3].Attributes.Add("class", "");

            ddlbankname.Enabled = true;
            divManualBankSection.Visible = false;
            //PayuDialog.Visible = true;
            dialog.Visible = true;
            txtChequeDDNo.Visible = false;
            divWriteChequeNoLable.Visible = false;
            divBank.Visible = true;
            divPopularBanks.Visible = false;
            divPopularPG.Visible = true;
            ddlbankname.Visible = false;

            objEChallan.GetChallanBanks_Payu(ddlbankname);
        }
        else if (rblpaymenttype.SelectedValue == "6")//UPI
        {
            this.rblpaymenttype.Items[0].Attributes.Add("class", "");
            this.rblpaymenttype.Items[1].Attributes.Add("class", "");
            this.rblpaymenttype.Items[2].Attributes.Add("class", "active");
            this.rblpaymenttype.Items[3].Attributes.Add("class", "");

            ddlbankname.Enabled = false;
            dialog.Visible = false;
            divManualBankSection.Visible = false;
            txtChequeDDNo.Visible = false;
            divWriteChequeNoLable.Visible = false;
            divUPI.Visible = true;
            divBank.Visible = false;
            divPopularBanks.Visible = false;
            divPopularPG.Visible = false;
            spanBankName.InnerHtml = rblUpi.SelectedItem.ToString();

        }
        rblCashCheque.SelectedValue = "1";
    }
    protected void rblCashCheque_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.rblpaymenttype.Items[1].Attributes.Add("class", "");
        this.rblpaymenttype.Items[2].Attributes.Add("class", "");
        this.rblpaymenttype.Items[0].Attributes.Add("class", "");
        this.rblpaymenttype.Items[3].Attributes.Add("class", "active");

        txtChequeDDNo.Text = string.Empty;
        dialog.Visible = false;
        ddlbankname.Enabled = true;
        divManualBankSection.Visible = true;
        //txtChequeDDNo.Visible = true;
        divWriteChequeNoLable.Visible = true;
        divBankMessage.Visible = false;
        //txtChequeDDNo.Enabled = rblCashCheque.SelectedValue == "2" ? true : false;
        divCheque.Visible = rblCashCheque.SelectedValue == "2" ? true : false;
        spanBankName.InnerText = ddlbankname.SelectedValue == "0" ? "" : ddlbankname.SelectedItem.ToString();
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
            divZone.Visible = true;
            divWard.Visible = true;
            if (Session["UserType"].ToString().Trim() != "4".Trim())
                txtTIN.Attributes.Add("onChange", "javascript:CheckTIN();");
        }
        if (objCheck.CheckHead0030() == true)
        {
            txtDeduct.Enabled = true;
            divdeduct.Visible = true;
        }

        if (objCheck.CheckHead004000102() == true)
        {
            CTDMSG.Visible = true;
            CTDMSG.Text = "Warning: This Budget head is going out from Feb 1,2015. Hence select new budget head 0040-00-111 for all type of payments under VAT budget head.";
        }

        int Result = 0;
        if (status == "repeat")
        {
            objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            Result = objEChallan.RepeatChallanView();

            if (Result == 1)
            {
                ddldistrict.SelectedValue = objEChallan.DistrictName.ToString();
                HdnProfile.Value = objEChallan.ProfileCode.ToString();
                ddllocation.SelectedValue = objEChallan.TreasuryCode.ToString();
                objEChallan.Tcode = objEChallan.TreasuryCode.ToString();
                objEChallan.DeptCode = deptCode;
                objEChallan.FillOfficeList(ddlOfficeName);
                ddlOfficeName.SelectedValue = objEChallan.OfficeName.ToString();
                txtPanNo.Text = objEChallan.PanNumber.Trim();
                objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
                objEChallan.TreasuryCode = ddllocation.SelectedValue;
                objEChallan.BudgetHead = dt1.Rows[0][1].ToString().Substring(0, 13);
                objEChallan.GetPdAccountList(ddlpdAccount);
                ddlpdAccount.SelectedValue = Convert.ToString(objEChallan.PDacc > 0 ? objEChallan.PDacc : objEChallan.DivCode);
                txtRemark.Text = objEChallan.Remark;
                txtDeduct.Text = objEChallan.DeductCommission.ToString("00");

                int Res = objEChallan.GetUserGrnDetail();
                if (Res == 1)
                {
                    txtName.Text = objEChallan.FirstName + " " + objEChallan.LastName;
                    txtaddress.Text = objEChallan.Address;
                    txtMobileNo.Text = objEChallan.MobileNo;
                    txtCity.Text = objEChallan.City.ToString().Trim();
                }
            }
        }
        else if (status == "profile")
        {
            Result = objEChallan.GetUserDetail();
            if (Result == 1 && Session["UserID"].ToString().Trim() != "73")
            {
                txtName.Text = objEChallan.FirstName + " " + objEChallan.LastName;
                txtaddress.Text = objEChallan.Address;
                txtMobileNo.Text = objEChallan.MobileNo;
                txtCity.Text = objEChallan.City.ToString().Trim();
            }
        }
    }

    //private string CreateBudgetheadSection(DataTable dt)
    private string CreateBudgetheadSection()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Schema"];

        var HeadCount = (from r in dt.AsEnumerable()
                         select r["BudgetHead"].ToString().Substring(0, 4)).Distinct().ToList();

        BudgetHead = (from r in dt.AsEnumerable()
                      select r["BudgetHead"].ToString().Substring(0, 13)).Distinct().ToArray();
        ViewState["BudgetHead"] = BudgetHead;
        int rowCount = Convert.ToInt16(dt.Rows.Count);
        ViewState["rowcount"] = rowCount;


        objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
        objEChallan.TreasuryCode = ddllocation.SelectedValue;
        objEChallan.BudgetHead = dt.Rows[0][1].ToString().Substring(0, 13);
        objEChallan.GetPdAccountList(ddlpdAccount);
        divPD.Visible = objEChallan.PdOrDivFlag == 2 ? false : true;
        ddlpdAccount.Visible = objEChallan.PdOrDivFlag == 2 ? false : true;
        divPdacc.InnerText = objEChallan.PdOrDivTag;




        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rowCount; i++)
        {
            string[] bhead = dt.Rows[i][1].ToString().Split('-');
            string budget = bhead[0].ToString().Substring(0, 4) + "-" + bhead[0].ToString().Substring(4, 2) + "-" + bhead[0].ToString().Substring(6, 3) + "-" + bhead[0].ToString().Substring(9, 2) + "-" + bhead[0].ToString().Substring(11, 2);
            //string text = dt.Rows[i][0].ToString() + " (" + budget + ")";
            string amount = dt.Columns.Count == 5 ? "00" : Convert.ToDecimal(dt.Rows[i][2]).ToString("00");
            total = total + Convert.ToDecimal(amount);

            hdnDecuctAmount.Value = budget.Substring(0, 4).ToString() == "0030" ? "1" : "2"; //manage to display deduct amount section

            sb.Append("<div class=\"row\" style=\"margin: 0;font-size: 15px;font-weight: 600;color:#5b7fcf\">");
            sb.Append("<div class=\"col-12 col-sm-6 col-md-6 \"><span class=\"spanbh\">" + budget + "- </span><span class=\"spanpurpose\">" + dt.Rows[i][0].ToString() + "</span></div>");
            sb.Append("<div class=\"col-12 col-sm-5 col-md-5 \" style=\"text-align: right;\">");
            sb.Append("<input type = \"text\"id=\"TextBox_" + i.ToString() + "\" name=\"TextBox_" + i.ToString() + "\" MaxLength=\"11\"");
            sb.Append(" Style =\"text-align: right;height:30px;width:50%;font-size: 15px;font-weight: 600\" value = \"" + amount + "\" onfocus = \"javascript:ClearValue(this);\"");
            sb.Append(" onkeypress=\"return isNumber(event)\" onBlur =\"javascript:updateValue('TextBox_" + i.ToString() + "');\" onPaste=\"return false\" ");
            sb.Append(" onChange = \"javascript:return updateValue('TextBox_" + i.ToString() + "');\" />");
            sb.Append("</div>");
            sb.Append("</div>");
        }
        txttotalAmount.Text = (total - Convert.ToDecimal(txtDeduct.Text)).ToString("00");

        return sb.ToString();
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

        DisableInActiveSBIBanks(ActiveDeActiveBank);
        DisableInActiveBanks();   //call Method for to Check  Block BankService 29 may 2019
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
                ddlbankname.Items.FindByValue(items.ElementAt(i).Key).Attributes.Add("style", "color:orangered;background-color:wheat;");

                ddlbankname.Items.FindByValue(items.ElementAt(i).Key).Attributes["title"] = ConfigurationManager.AppSettings["ServiceDown"].ToString();
            }
        }
        catch (System.IO.IOException es)
        {

        }
    }

    private void DisableInActiveSBIBanks(string Bank)
    {
        if (Bank != null)
        {
            ddlbankname.Items.FindByValue(Bank).Attributes["disabled"] = "disabled";
            ddlbankname.Items.FindByValue(Bank).Attributes["title"] = "SBI Bank Service will not be  Available 20:00 Hours To 00:00 Hours.";
        }
    }

    #region Insertsection
    //public void submitclick()
    //{
    //    //spanPersonalMsg.Attributes.Add("style", "display:none");

    //    objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
    //    objEChallan.dtSchema = (DataTable)ViewState["BudgetHeadSchema"];
    //    objEChallan.DeductCommission = Convert.ToDouble(txtDeduct.Text);
    //    objEChallan.DeptCode = Convert.ToInt16(objEChallan.dtSchema.Rows[0][0].ToString());
    //    objEChallan.BudgetHead = objEChallan.dtSchema.Rows[0][4].ToString();
    //    //objEChallan.TotalAmount = (Convert.ToDouble(objEChallan.dtSchema.Compute("SUM(amount)", String.Empty)) - (objEChallan.DeductCommission)).ToString();
    //    objEChallan.TotalAmount = objEChallan.dtSchema.Compute("SUM(amount)", String.Empty).ToString();
    //    objEChallan.PanNumber = txtPanNo.Text;
    //    objEChallan.budgetheadcount = objEChallan.dtSchema.Rows.Count;
    //    objEChallan.fldpersonal = true;


    //    EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
    //    objCheck.BudgetHead = ViewState["majorHead"].ToString();
    //    objCheck.payMode = rblpayMode.SelectedValue.ToString();
    //    objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
    //    objCheck.pdAccountNoCount = ddlpdAccount.Items.Count;
    //    objCheck.pdVisible = Convert.ToBoolean(pdvisible.InnerText);
    //    objCheck.treasuryCodeBank = ddlbankname.SelectedValue.ToString();
    //    objCheck.treasuryCodePd = ddlpdAccount.SelectedValue.ToString();
    //    objCheck.isPD = ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true;
    //    objCheck.proc_id = ProcUserId;
    //    BudgetHead = ViewState["BudgetHead"] as string[];
    //    objCheck.serviceBudgetHead = BudgetHead;

    //    if (objEChallan.CheckBudgetHeadLevelCondition())
    //    {
    //        spanPersonalMsg.InnerText = objEChallan.msg;
    //        spanPersonalMsg.Attributes.Add("style", "display:block");
    //    }
    //    else if (objCheck.CheckSubmitCondition() == true)
    //    {
    //        spanPersonalMsg.InnerText = objCheck.msg;
    //        spanPersonalMsg.Attributes.Add("style", "display:block");
    //    }
    //    else
    //    {
    //        int output = 0;
    //        InsertData();
    //        output = objEChallan.InsertChallan();
    //        Session["GrnNumber"] = Convert.ToString(output);
    //        Session["NetAmount"] = objEChallan.RetTotalAmount;

    //        if (output <= 0)
    //        {
    //            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Not Saved')", true);
    //            spanPersonalMsg.InnerText = "Record Not Saved";
    //            spanPersonalMsg.Attributes.Add("style", "display:block");
    //        }
    //    }
    //}
    //public void InsertData()
    //{
    //    EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
    //    objCheck.BudgetHead = ViewState["majorHead"].ToString();
    //    objCheck.DeptCode = Convert.ToInt32(ViewState["DeptCode"]);
    //    if (objCheck.CheckCTDCase() == true)
    //    {
    //        objEChallan.Zone = ddlZone.SelectedValue;
    //        objEChallan.Circle = ddlCircle.SelectedValue;
    //        objEChallan.Ward = ddlWard.SelectedValue;
    //        if (Session["UserID"].ToString() == "73" && txtTIN.Text == "")
    //        {
    //            objEChallan.Identity = "99999999999";
    //        }
    //        else
    //        {
    //            objEChallan.Identity = txtTIN.Text;
    //        }
    //    }
    //    else if (ViewState["DeptCode"].ToString().Trim() == "104" && txtTIN.Text == string.Empty)    // Add Condition For Vehicle No Compulsory  30/7/2018
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        objEChallan.Identity = txtTIN.Text;
    //    }

    //    string strReqq = Request.Url.ToString();
    //    strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
    //    ObjEncrytDecrypt = new EgEncryptDecrypt();
    //    List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
    //    if (strList[0].ToString() == "Profile")
    //    {
    //        //Profilee = strList[1].ToString();
    //        //objEChallan.UserPro = Convert.ToInt32(strList[1].ToString());
    //        objEChallan.Profile = Convert.ToInt32(strList[1].ToString());
    //    }
    //    else if (strList[0].ToString() == "Service")
    //    {
    //        //ServiceId = Convert.ToInt32(strList[1].Split('|').GetValue(1));
    //        //DeptCode = Convert.ToInt32(strList[1].Split('|').GetValue(2));
    //        //ProcUserId = Convert.ToInt32(strList[1].Split('|').GetValue(3));
    //        //// Profilee = "1";
    //        //HdnProfile.Value = "1";
    //        objEChallan.Serviceid = Convert.ToInt32(strList[1].Split('|').GetValue(1)); ;
    //        objEChallan.ProcUserId = Convert.ToInt32(strList[1].Split('|').GetValue(3));
    //        objEChallan.Profile = 1;

    //    }

    //    else

    //    {
    //        objEChallan.Profile = Convert.ToInt32(HdnProfile.Value);


    //    }

    //    // objEChallan.Profile = Convert.ToInt32(HdnProfile.Value);
    //    objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
    //    objEChallan.PanNumber = txtPanNo.Text;
    //    objEChallan.Location = ddllocation.SelectedValue;
    //    objEChallan.FullName = txtName.Text;
    //    objEChallan.Address = txtaddress.Text;
    //    objEChallan.City = txtCity.Text.Trim();
    //    objEChallan.MobileNo = txtMobileNo.Text;
    //    objEChallan.PINCode = "000000"; //txtPin.Text;
    //    objEChallan.DeductCommission = double.Parse(txtDeduct.Text);
    //    objEChallan.TotalAmount = txttotalAmount.Text;
    //    objEChallan.ChequeDDNo = txtChequeDDNo.Text;

    //    objEChallan.Remark = txtRemark.Text;
    //    string Details = HiddenField1.Value; //SubmitExtraDetails();// HiddenField1.Value; //
    //    if (Details != "")
    //    {
    //        objEChallan.Details = Details;
    //    }
    //    objCheck.pdAccountNo = ddlpdAccount.SelectedValue.ToString();
    //    if (objCheck.CheckHeadGreater8000WithPD() == true)
    //    {
    //        if (ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true)
    //            objEChallan.PDacc = Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4));
    //        else
    //            objEChallan.DivCode = Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4));
    //    }
    //    else if (ddlpdAccount.Items.Count > 1)
    //    {
    //        if (ddlpdAccount.Items[0].ToString() == "--- Select Division Code ---" ? false : true)
    //            objEChallan.PDacc = ddlpdAccount.SelectedValue != "0" ? Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4)) : 0;
    //        else
    //            objEChallan.DivCode = ddlpdAccount.SelectedValue != "0" ? Convert.ToInt32(ddlpdAccount.SelectedValue.Remove(ddlpdAccount.SelectedValue.Length - 4)) : 0;
    //    }
    //    else
    //    {
    //        objEChallan.PDacc = 0;
    //    }

    //    SetPeriod();

    //}
    //private void SetPeriod()
    //{
    //    string Fdate = "";
    //    string Tdate = "";
    //    if (txtfromdate.Text != "" && txttodate.Text != "")
    //    {
    //        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
    //        Fdate = Convert.ToString(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
    //        string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
    //        Tdate = Convert.ToString(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
    //    }
    //    objEChallan.ChallanFromMonth = Convert.ToDateTime(Fdate.ToString());
    //    objEChallan.ChallanToMonth = Convert.ToDateTime(Tdate.ToString());
    //}
    public DataTable AmountSave()
    {
        DataTable schemaAmtTable = new DataTable();

        schemaAmtTable.Columns.Add(new DataColumn("DeptCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("ScheCode", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("amount", System.Type.GetType("System.Double")));
        schemaAmtTable.Columns.Add(new DataColumn("UserId", System.Type.GetType("System.Int32")));
        schemaAmtTable.Columns.Add(new DataColumn("BudgetHead", System.Type.GetType("System.String")));

        DataRow schemarow;

        dt1 = (DataTable)ViewState["Schema"];

        int i = Convert.ToInt16(dt1.Rows.Count);
        for (int j = 0; j < i; j++)
        {
            if (Convert.ToDouble(Request.Form["TextBox_" + j]) > 0 && (Request.Form["TextBox_" + j]) != "")
            {
                schemarow = schemaAmtTable.NewRow();
                string[] BudgetHeadName = dt1.Rows[j][1].ToString().Split('-');
                schemarow["BudgetHead"] = BudgetHeadName[0].ToString();
                schemarow["ScheCode"] = int.Parse(BudgetHeadName[1].ToString());
                schemarow["DeptCode"] = Convert.ToInt32(BudgetHeadName[2].ToString());
                schemarow["amount"] = Convert.ToDouble(Request.Form["TextBox_" + j]);//Convert.ToDouble("asdfasdf"); //
                schemarow["UserId"] = Convert.ToInt32(Session["UserId"].ToString());
                schemaAmtTable.Rows.Add(schemarow);
                schemaAmtTable.AcceptChanges();

            }
        }
        return schemaAmtTable;
    }

    #endregion

    #region ChallanView

    [WebMethod]
    public static string EncryptData(string grn, string totamt)
    {
        EncryptDecryptionBL objenc = new EncryptDecryptionBL();
        var comm = grn + '-' + totamt;
        comm = objenc.Encrypt(comm);
        return comm;
    }
    #endregion
    #endregion
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
                string Fdate = "";
                string Tdate = "";
                if (txtfromdate.Text != "" && txttodate.Text != "")
                {
                    string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
                    Fdate = Convert.ToString(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                    string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
                    Tdate = Convert.ToString(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
                }
                objEChallan.ChallanFromMonth = Convert.ToDateTime(Fdate.ToString());
                objEChallan.ChallanToMonth = Convert.ToDateTime(Tdate.ToString());

                string FromDate = objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(1).ToString() + "/" + objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(0) + "/" + objEChallan.ChallanFromMonth.ToShortDateString().Split('/').GetValue(2);
                //string groupCode = Session["UserId"].ToString() == "73" ? dt1.Rows[0][4].ToString() : dt1.Rows[0][5].ToString();
                string groupCode = ViewState["DeptCode"].ToString();
                string EncData = "User=IFMSAdmin|Password=IFMSPassword|Tin=" + txtTIN.Text + "|GroupCode=" + groupCode + "|FromDate= " + FromDate;
                string cipherText = objEncry.Encrypt(EncData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string returnData = objWeb.validateTIN(cipherText);
                if (returnData != "0")
                {

                    string plainText = objEncry.Decrypt(returnData, System.Web.Configuration.WebConfigurationManager.AppSettings["Securekey"] + "aes.key");

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
                        DataTable CTDZone = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[3])).CopyToDataTable();
                        DataTable CTDCircle = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[5])).CopyToDataTable();
                        DataTable CTDWard = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").Trim() == (lstPlainText[7])).CopyToDataTable();
                        //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Remitter Name is: '+ '" + lstPlainText[9].ToString() + "')", true);

                        spanPersonalMsg.Visible = true;
                        spanPersonalMsg.InnerText = "Remitter Name is: " + lstPlainText[9].ToString();

                        txtName.Text = lstPlainText[9].ToString();
                        txtaddress.Text = lstPlainText[11].ToString();
                        txtName.Enabled = false;
                        txtaddress.Enabled = false;
                        //ddlPeriod.Enabled = false;
                        txtTIN.Enabled = false;
                        //ddlcity.Enabled = false;
                        //SetCTDOfficeWiseTreasury(400);

                        //trCTD.Visible = true;
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
                        DataTable dt = CTDTable.AsEnumerable().Where(t => t.Field<string>("Location_Code").StartsWith("0")).CopyToDataTable();
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
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('Invalid Tin No.')", true);
                        spanPersonalMsg.Visible = true;
                        spanPersonalMsg.InnerText = "Invalid Tin No.";
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('We are unable to connect CTD Web Portal.')", true);

                    spanPersonalMsg.Visible = true;
                    spanPersonalMsg.InnerText = "We are unable to connect CTD Web Portal.";
                    txtTIN.Text = "";
                    txtTIN.Focus();
                }
            }
            //txtamountwords.Text = HiddenAmount.Value;
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('We are unable to connect CTD Web Portal.')", true);
            spanPersonalMsg.Visible = true;
            spanPersonalMsg.InnerText = "We are unable to connect CTD Web Portal.";
            txtTIN.Text = "";
            txtTIN.Focus();
            //txtamountwords.Text = HiddenAmount.Value;
            return;
        }

    }







    //protected void FillLocation()
    //{
    //    //objEChallan.FillLocation(ddldistrict);
    //    DropDownListX dp = new DropDownListX();
    //    DataTable treasuryData = new DataTable();
    //    treasuryData = dp.FillTreasury();

    //    for (int i = 1; i < 41; i++)
    //    {
    //        var rows = treasuryData.AsEnumerable().Where(t => t.Field<int>("TGroupCode") == i);

    //        string group = rows.ElementAtOrDefault(0).Field<string>("TreasuryName"); // ElementAtOrDefault(0).treasuryName.ToString().Trim();
    //        ddllocation.AddItemGroup(group.Trim());
    //        ddldistrict.AddItemGroup(group.Trim());
    //        foreach (var item in rows)
    //        {
    //            ListItem items = new ListItem(item.Field<string>("TreasuryName"), item.Field<string>("TreasuryCode"));
    //            ddllocation.Items.Add(items);
    //            ddldistrict.Items.Add(items);
    //        }

    //    }
    //}
    private void assignAmountToBH()
    {
        string amount;

        List<BudgetHead> listbh = new List<BudgetHead>();
        DataTable dtbh = new DataTable();
        DataTable dtsc = new DataTable();
        dtbh = (DataTable)ViewState["BudgetHeadSchema"];
        dtsc = (DataTable)ViewState["Schema"];

        int count = 0;
        for (int i = 0; i < Convert.ToInt16(ViewState["rowcount"]); i++)
        {
            bool exists = dtbh.Select().ToList().Exists(row => (row["BudgetHead"].ToString().ToUpper() + '-' + row["ScheCode"].ToString().ToUpper() + '-' + row["DeptCode"].ToString().ToUpper()) == dtsc.Rows[i]["BudgetHead"].ToString().ToUpper());
            string bh = dtsc.Rows[i]["BudgetHead"].ToString().Substring(0, 13);
            BudgetHead objBH = new BudgetHead();
            objBH.id = "TextBox_" + i.ToString();
            if (exists)
            {
                objBH.value = dtbh.Rows[count]["amount"].ToString();
                count++;
            }
            else
                objBH.value = "00";
            //objBH.value = exists == true ? matched.Rows[0]["amount"].ToString() : "0";// Request.Form["TextBox_" + i.ToString()];

            //for (int j = 0; j < dtbh.Rows.Count; j++)
            //{
            //    objBH.value = dtsc.Rows[i]["BudgetHead"].ToString() == dtbh.Rows[j]["BudgetHead"].ToString() + '-' + dtbh.Rows[j]["ScheCode"].ToString() + '-' + dtbh.Rows[j]["DeptCode"].ToString() ? dtbh.Rows[j]["amount"].ToString() : "0";
            //}
            listbh.Add(objBH);
        }
        amount = JsonConvert.SerializeObject(listbh);
        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "AssignBudgetheadwiseValue(" + amount + ");", true);
    }
    public void displyOff()
    {
        spanPersonalMsg.Attributes.Add("style", "display:none");
    }
}
public class BudgetHead
{
    public string id { get; set; }
    public string value { get; set; }
}

