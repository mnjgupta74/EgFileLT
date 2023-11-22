using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EgBL;
using System.Web;

public partial class WebPages_EgEchallanForOffice : System.Web.UI.Page
{

    EgEncryptDecrypt ObjEncrytDecrypt;

    EgUserRegistrationBL objUserReg;
    DataTable schemaAmtTable = new DataTable();
    EgEMinusChallanBL objEChallan = new EgEMinusChallanBL();
    DataTable dt1 = new DataTable();
    Label lbl;
    TextBox tb;
    Label lbl2;

    string Val, Profilee, GRN, Type;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }

        //if (Request.QueryString.Count > 0)
        //{
        //    ddlYear.FinyearSelectType = 2;
        //    ddlYear.count = 9;
        //    string strReqq = Request.Url.ToString();
        //    strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
        //    ObjEncrytDecrypt = new EgEncryptDecrypt();
        //    List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
        //    if (strList == null || strList.Count < 0)
        //    {
        //        Response.Redirect("~\\logout.aspx");
        //    }
        //}


        if (Request.QueryString.Count > 0)
        {
            ddlYear.FinyearSelectType = 2;
            ddlYear.count = 9;
            string strReqq = Request.Url.ToString();
            strReqq = strReqq.Substring(strReqq.IndexOf('?') + 1);
            ObjEncrytDecrypt = new EgEncryptDecrypt();
            //EncryptDecryprBL ObjEncryptDecrypt = new EncryptDecryprBL();
            List<string> strList = ObjEncrytDecrypt.Decrypt(strReqq);
            if (strList != null)
            {
                if (strList.Count > 0)
                {
                    //if (strList[0].ToString() == "Office")
                    //{
                    //    Val = strList[1].ToString().Trim();
                    //    if (Session["mydatatable"] == null)
                    //        Response.Redirect("~\\Default.aspx");
                    //}
                    //else 
                    if (strList[0].ToString() == "Profile")
                    {
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
            objUserReg = new EgUserRegistrationBL();
            txttotalAmount.Enabled = false;
            txtamountwords.Enabled = false;

            objEChallan.FillTreasury(ddllocation);
            objEChallan.FillLocation(ddlTreasury);

            objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            //
            DataTable minusTable = objEChallan.GetOfficeForMinus();
            ddlOfficeName.Items.Insert(0, new ListItem(minusTable.Rows[0][1].ToString(), minusTable.Rows[0][0].ToString()));
            ddllocation.SelectedValue = minusTable.Rows[0][2].ToString();
            ddlTreasury.SelectedValue = minusTable.Rows[0][3].ToString();
            ddllocation.Enabled = false;
            ddlTreasury.Enabled = false;
            ddlOfficeName.Enabled = false;
            objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
            objEChallan.TreasuryCode = ddllocation.SelectedValue;
            objEChallan.GetOfficeWiseDDO(ddlDDO);

            objEChallan.FillBanks_Minus(ddlbankname);

            //Fill Treasury DropDown with Grouping  
            DropDownListX dp = new DropDownListX();
            DataTable treasuryData = new DataTable();
            treasuryData = dp.FillTreasury();

            for (int i = 1; i < 40; i++)
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



            //    if (Val != null)
            //    {

            //        dt1 = (DataTable)Session["mydatatable"];
            //        ViewState["GuestSchema"] = dt1;
            //        objEChallan.DeptCode = int.Parse(dt1.Rows[0][3].ToString());
            //        txtDept.Text = dt1.Rows[0][2].ToString();
            //        txtDept.Enabled = false;
            //        ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 4);
            //        //MajorHeadWiseCheck(dt1.Rows[0][1].ToString().Substring(0, 4));
            //        dt1 = (DataTable)ViewState["GuestSchema"];
            //        DynamicTable(dt1);
            //}
            //    else
            if (Profilee != null)
            {
                objEChallan.UserPro = Convert.ToInt32(Profilee);
                objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objEChallan.GetProfileListME(ddlProfile);
                ddlProfile.SelectedValue = Profilee + "- 1";
                ddlProfile.Enabled = false;

                dt1 = objEChallan.GetSchemaME();
                ViewState["profileSchema"] = dt1;
                txtDept.Text = dt1.Rows[0][6].ToString();
                txtDept.Enabled = false;
                ViewState["DeptCode"] = dt1.Rows[0][7].ToString();
                ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());

                objEChallan.DeptCode = int.Parse(dt1.Rows[0][7].ToString());
                //DynamicTable(dt1);
                MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), "Profile");

            }

            else
            {
                objEChallan.GRNNumber = Convert.ToInt64(GRN.ToString());
                dt1 = objEChallan.GetSchemaME();
                ViewState["RepeatSchema"] = dt1;
                txtDept.Enabled = false;
                txtDept.Text = dt1.Rows[0][6].ToString();
                ViewState["DeptCode"] = dt1.Rows[0][7].ToString();
                ViewState["majorHead"] = dt1.Rows[0][1].ToString().Substring(0, 13);
                Page.ClientScript.RegisterHiddenField("vCode", dt1.Rows[0][4].ToString());

                objEChallan.DeptCode = int.Parse(dt1.Rows[0][4].ToString());
                MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), "repeat");
                txtChequeDDNo.Text = string.Empty;

                //txtDept.Enabled = false;
                //dt1 = (DataTable)ViewState["RepeatSchema"];
                //MajorHeadWiseCheck(ViewState["majorHead"].ToString(), Convert.ToInt32(ViewState["DeptCode"]), "repeat");
                // DynamicTable(dt1);
            }

        }
        if (Profilee != null)
        {
            dt1 = (DataTable)ViewState["profileSchema"];
            DynamicTable(dt1);
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

    private void MajorHeadWiseCheck(string Majorhead, int deptCode, string status)
    {
        EgCheckChallanCondition objCheck = new EgCheckChallanCondition();
        objCheck.BudgetHead = Majorhead;
        objCheck.DeptCode = deptCode;
        int Result = 0;
        if (status == "repeat")
        {
            objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());
            Result = objEChallan.EChallanView();

            if (Result == 1)
            {
                //ddlTreasury.SelectedValue = objEChallan.DistrictName.ToString();
                //ddlProfile.SelectedValue = objEChallan.ProfileCode.ToString();
                //ddllocation.SelectedValue = objEChallan.TreasuryCode.ToString();
                //objEChallan.FillOfficeList(ddlOfficeName);
                //ddlOfficeName.SelectedValue = objEChallan.OfficeName.ToString();
                //ddllocation.SelectedValue = objEChallan.TreasuryCode.ToString();
                //ddlProfile.Enabled = false;
                ddlPeriod.SelectedValue = "1";
                txtPanNo.Text = objEChallan.PanNumber;
                string Ptype = objEChallan.TypeofPayment;
                //if (Ptype == "Manual")
                //{
                //    rblpaymenttype.SelectedValue = "3";
                //}
                //else
                //{
                // rblpaymenttype.SelectedValue = "4";
                // }

                objEChallan.OfficeName = Convert.ToInt32(ddlOfficeName.SelectedValue);
                objEChallan.TreasuryCode = ddllocation.SelectedValue;
                objEChallan.BudgetHead = dt1.Rows[0][1].ToString().Substring(0, 13);
                ////objEChallan.GetPdAccountList(ddlpdAccount);
                ////ddlpdAccount.SelectedValue = Convert.ToString(objEChallan.PDacc > 0 ? objEChallan.PDacc : objEChallan.DivCode);


                txtRemark.Text = objEChallan.Remark;
                //txtDeduct.Text = objEChallan.DeductCommission.ToString("0.00");
                txttotalAmount.Text = objEChallan.TotalAmount;
                txtChequeDDNo.Text = objEChallan.ChequeDDNo;
                txtamountwords.Text = objEChallan.AmountInWords;

                int Res = objEChallan.GetUserGrnDetail();
                if (Res == 1)
                {
                    txtName.Text = objEChallan.FirstName + " " + objEChallan.LastName;
                    txtaddress.Text = objEChallan.Address;
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
                txtPin.Text = objEChallan.PinCode;
                txtCity.Text = objEChallan.City.ToString().Trim();
            }
        }
    }
    public void DynamicTable(DataTable dt)
    {

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
            lbl.Text = dt.Rows[i][0].ToString() + " (" + budget + ")" + "/" + dt.Rows[i][2].ToString() + "/" + dt.Rows[i][3].ToString() + "/" + dt.Rows[i][4].ToString();
            // Add the control to the TableCell
            lbl.Width = 470;

            cell1.Controls.Add(lbl);

            tb.ID = "TextBox_" + dt.Rows[i][1].ToString();
            decimal moneytotal = Convert.ToDecimal(dt.Rows[i][5]);
            tb.Text = moneytotal.ToString("0.00"); //string.Format("{0:0.0}", moneytotal);
                                                   //tb.Text = "0.0";
            tb.MaxLength = 11;
            tb.Style.Add("text-align", "right");
            tb.Width = 180;


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
            //Add the TableRow to the Table
            tbl.Rows.Add(row);

        }
    }

    protected void btninsert_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (ddlbankname.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('select Bank!');", true);
            }
            else
            {
                submitclick();
            }
        }
    }
    protected void rblCashCheque_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtChequeDDNo.Text = string.Empty;
        spanCheque.Visible = rblCashCheque.SelectedValue == "2" ? true : false;
    }
    /// <summary>
    /// Insert User Challan Information and Schema  Amount Info  and Check Objects Amount not fill more than 9
    /// </summary>
    public void submitclick()
    {

        objEChallan.UserId = Convert.ToInt32(Session["UserId"].ToString());

        int rows = Convert.ToInt32(ViewState["rowcount"]);
        int count = 0;
        if (Profilee != null)
        {
            dt1 = (DataTable)ViewState["profileSchema"];
        }
        else
        {
            dt1 = (DataTable)ViewState["RepeatSchema"];
        }
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
            }
        }
        //selection of  DD or Cheque DD No or Cheque No  is Mandatory otherwise bydefault cash 29 July  2019
        if (rblCashCheque.SelectedValue.Trim() == "2" && txtChequeDDNo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Fill Cheque or DD No !!');", true);
            return;
        }
        if (count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Please Fill the Amount Detail')", true);
        }
        else if (count > 9)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('You can not fill more than 9 Schemas Amount !');", true);
        }

        else
        {
            int output = 0;
            InsertData();
            if (btninsert.Text == "Submit")
            {
                output = objEChallan.InsertChallan();
                Session["GrnNumber"] = Convert.ToString(output);
            }
            else
            {
                objEChallan.GRNNumber = Convert.ToInt32(Session["GrnNumber"]);
                // output = objEChallan.EgUpdateEChallan();

            }
            if (output != 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Saved Successfully')", true);
                string strURLWithData = ObjEncrytDecrypt.Encrypt(string.Format("GRN={0}", Session["GrnNumber"].ToString().Trim()));
                Response.Redirect("~/webpages/reports/EgManualChallan.aspx?" + strURLWithData.ToString());
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
    public void InsertData()
    {

        objEChallan.Profile = Convert.ToInt32(Profilee);
        objEChallan.TypeofPayment = "M";


        objEChallan.Identity = txtTIN.Text;

        if (Session["UserType"].ToString() == "4")
        {
            objEChallan.ObjectHead = dt1.Rows[0][4].ToString();
            objEChallan.VNC = dt1.Rows[0][3].ToString();
            objEChallan.PNP = dt1.Rows[0][2].ToString();
            objEChallan.ddo = Convert.ToInt32(ddlDDO.SelectedValue);
        }
        objEChallan.OfficeName = int.Parse(ddlOfficeName.SelectedValue);
        objEChallan.PanNumber = txtPanNo.Text;
        objEChallan.Location = ddllocation.SelectedValue;
        objEChallan.FullName = txtName.Text;
        objEChallan.ChallanYear = ddlYear.SelectedValue;
        objEChallan.Address = txtaddress.Text;
        objEChallan.City = txtCity.Text;
        objEChallan.PINCode = txtPin.Text;
        objEChallan.DeductCommission = 0;
        objEChallan.TotalAmount = txttotalAmount.Text;
        Session["TotalAmt"] = txttotalAmount.Text;
        objEChallan.ChequeDDNo = txtChequeDDNo.Text;
        objEChallan.BankName = ddlbankname.SelectedValue.Substring(0, 7);
        objEChallan.AmountInWords = txtamountwords.Text;
        objEChallan.Remark = txtRemark.Text;

        string Details = HiddenField1.Value; //SubmitExtraDetails();// HiddenField1.Value; //
        if (Details != "")
        {
            objEChallan.Details = Details;
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

        for (int j = 0; j < i; j++)
        {
            if (Convert.ToDouble(((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text) > 0 && ((TextBox)tbl.Rows[j].Cells[1].FindControl("TextBox_" + dt1.Rows[j][1].ToString())).Text != "")
            {

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

    //protected void ddlbankname_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (rblpaymenttype.SelectedValue == "3" && Convert.ToInt64(ddlbankname.SelectedValue) > 0)
    //    ddllocation.SelectedValue = ddlbankname.SelectedValue.Substring(7, 4);
    //    txtamountwords.Text = HiddenAmount.Value;
    //}

    //protected void ddlDDO_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtName.Text = ddlDDO.SelectedItem.Text.ToString().Split('-')[1];
    //}

}
