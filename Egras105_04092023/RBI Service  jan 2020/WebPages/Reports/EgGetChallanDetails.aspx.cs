using System;
using System.Data;
using System.Web.UI.WebControls;
using EgBL;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class WebPages_EgGetChallanDetails : System.Web.UI.Page
{
    EgChallanDetail objEgChallanDetail;
    EgEncryptDecrypt ObjEncryptDecrypt;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["UserID"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
            //for paging
            CurrentPage = 1;
            calendartodate.EndDate = DateTime.Now;
            calendarfromdate.EndDate = DateTime.Now;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //ViewState["GrnList"] = null;
            GetGrnList(1);
        }
        catch (Exception ex)
        {
            // Browserinfo objbrowseringo = new Browserinfo();
            string msg = ex.Message;
            EgErrorHandller obj = new EgErrorHandller();
            obj.InsertError(msg);
        }
    }

    private void parameters(int pageIndex)
    {
        objEgChallanDetail = new EgChallanDetail();
        objEgChallanDetail.Userid = Convert.ToInt32(Session["userid"].ToString());
        objEgChallanDetail.Usertype = Convert.ToInt32(Session["UserType"].ToString());
        objEgChallanDetail.pageIndex = pageIndex;
        if (txtGRN.Text == "")
        {
            objEgChallanDetail.GRN = 0;
        }
        else
        {
            objEgChallanDetail.GRN = Convert.ToInt64(txtGRN.Text);
        }

        if (txtfromdate.Text == "")
        {
            objEgChallanDetail.fromDate = Convert.ToDateTime("1900/01/01");

        }
        else
        {
            string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
            objEgChallanDetail.fromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
        }
        if (txttodate.Text == "")
        {
            objEgChallanDetail.Todate = Convert.ToDateTime("1900/01/01");
        }
        else
        {
            string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
            objEgChallanDetail.Todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
        }
    }



    protected void rptChallanFill_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkverify = (LinkButton)e.Item.FindControl("LinkVerify");
            DataRowView drv = e.Item.DataItem as DataRowView;
            string Ptype = string.Empty;
            string Pstatus = string.Empty;
            if (drv == null)
            {
                Label paytypes = (Label)e.Item.FindControl("lblPaymentType");
                Label Status = (Label)e.Item.FindControl("lblStatus");
                Ptype = paytypes.Text;
                Pstatus = Status.Text;
            }
            else
            {
                Ptype = drv.Row["Paymenttype"].ToString();
                Pstatus = drv.Row["Status"].ToString();
            }

            HiddenField hdnverify = (HiddenField)e.Item.FindControl("hdnverify");
            if (hdnverify.Value == "Y")
            {
                lnkverify.Visible = true;
            }
            else
            {
                lnkverify.Visible = false;
            }


            LinkButton lnkRepeat = (LinkButton)e.Item.FindControl("lnkrepeat");
            Label paytype = (Label)e.Item.FindControl("lblPaymentType");
            if (paytype.Text == "Online")
            {

                ImageButton imgviewbtn = (ImageButton)e.Item.FindControl("ImageViewbtn");
                imgviewbtn.Visible = true;
                ImageButton imgPrintbtn = (ImageButton)e.Item.FindControl("imgPrintbtn");
                imgPrintbtn.Visible = false;

            }
            else if (paytype.Text == "Manual")
            {

                ImageButton imgviewbtn = (ImageButton)e.Item.FindControl("ImageViewbtn");
                imgviewbtn.Visible = false;
                ImageButton imgPrintbtn = (ImageButton)e.Item.FindControl("imgPrintbtn");
                imgPrintbtn.Visible = true;
            }

            Label ChallanStatus = (Label)e.Item.FindControl("lblStatus");
            if (ChallanStatus.Text == "failure")
            {

                ImageButton imgviewbtn = (ImageButton)e.Item.FindControl("ImageViewbtn");
                imgviewbtn.Enabled = false;

            }
            else if (ChallanStatus.Text == "Pending")
            {

                ImageButton imgviewbtn = (ImageButton)e.Item.FindControl("ImageViewbtn");
                imgviewbtn.Enabled = false;

            }
            else if (ChallanStatus.Text == "Success")
            {

                ImageButton imgviewbtn = (ImageButton)e.Item.FindControl("ImageViewbtn");
                imgviewbtn.Enabled = true;
            }

            if (lnkverify.ToolTip.Substring(0, 7).Trim() == "0000000") /// added by sandeep to disable all operations for grn whose bank is closed
            {
                lnkverify.Visible = false;
            }
            if (Convert.ToInt32(Session["UserType"].ToString()) == 10)
            {
                HtmlTableCell tdLink = (HtmlTableCell)e.Item.FindControl("tdrepeat");
                tdLink.Visible = true;
            }
            else
            {
                HtmlTableCell tdLink = (HtmlTableCell)e.Item.FindControl("tdrepeat");
                tdLink.Visible = false;
            }
        }
        if (e.Item.ItemType == ListItemType.Header)
        {
            if (Convert.ToInt32(Session["UserType"].ToString()) == 10)
            {
                HtmlTableCell tdHeader = (HtmlTableCell)e.Item.FindControl("hdrRepeat");
                tdHeader.Visible = true;
            }
            else
            {
                HtmlTableCell tdHeader = (HtmlTableCell)e.Item.FindControl("hdrRepeat");
                tdHeader.Visible = false;
            }
        }
    }

    protected void rptChallanFill_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ObjEncryptDecrypt = new EgEncryptDecrypt();

        if (e.CommandName == "View")
        {
            Label Grn = (Label)e.Item.FindControl("LabelGRN");
            string grn = Grn.Text;
           // string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&UserId={1}&UserType={2}&Dept={3}", grn.ToString(), Session["UserId"].ToString(), Session["UserType"],"1"));
            strURLWithData = "../EgDefaceDetailNew.aspx?" + strURLWithData;
            string script = "window.open('" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
        }

        if (e.CommandName == "print")
        {
            Label Grn = (Label)e.Item.FindControl("LabelGRN");
            Session["GrnNumber"] = Grn.Text;
            string strURLWithData = "~/webpages/reports/EgManualChallan.aspx";
            Response.Redirect(strURLWithData);

        }
        if (e.CommandName.Equals("Repeat"))
        {
            string grn = Convert.ToString(e.CommandArgument);
            EgVisibility objVisible = new EgVisibility();

            bool eligible = objVisible.IsEligible(Convert.ToInt64(grn));
            if (eligible == true)
            {

                string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&Type={1}", grn.ToString(), "Repeat"));
                strURLWithData = "../EgEchallan.aspx?" + strURLWithData.ToString();
                Response.Redirect(strURLWithData);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MSG", "alert('This TRANSACTION would not process! ')", true);
                return;
            }
        }
        if (e.CommandName.Equals("Verify"))
        {
            VerifiedBankClass objverify = new VerifiedBankClass();
            LinkButton Imgverify = e.Item.FindControl("LinkVerify") as LinkButton;
            objverify.BSRCode = Imgverify.ToolTip.Substring(0, 7).Trim();
            objverify.PaymentMode = Imgverify.ToolTip.Substring(8, 1).Trim(); // Add By jp Gupta 27/4/2017 for Check Payment Mode
            LinkButton lnkrpt = e.Item.FindControl("lnkrepeat") as LinkButton;
            objverify.GRN = Convert.ToInt64(lnkrpt.CommandArgument);
            Label lblAmount = e.Item.FindControl("lblAmount") as Label;
            objverify.TotalAmount = Convert.ToDouble(lblAmount.Text.Trim());
            //objverify.flag = "R";
            string msg = objverify.Verify();
            parameters(1);
            //ViewState["GrnList"] = null;
            //BindRpt();
            GetGrnList(1);
            if (msg != null || msg != "")
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + msg + "');", true);
        }
    }


    //protected void txtSearch_TextChanged(object sender, EventArgs e)
    //{
    //    SearchText();
    //}

    //private void SearchText()
    //{

    //    parameters(1);
    //    int ItemPerPage = 14;
    //    if (CurrentPage == 1)
    //    {
    //        objEgChallanDetail.StartIdx = 1;
    //    }
    //    else
    //    {
    //        objEgChallanDetail.StartIdx = (((CurrentPage - 1) * ItemPerPage) + 1);
    //    }

    //    objEgChallanDetail.EndIdx = (((objEgChallanDetail.StartIdx) + ItemPerPage) - 1);
    //    DataTable dt1 = new DataTable();
    //    dt1 = objEgChallanDetail.FillSearchRecord();
    //    string searchstr = txtSearch.Text.ToUpper().ToString().Trim();

    //    var query = from p in dt1.AsEnumerable()
    //                where
    //                      p.Field<Int64>("GRN").ToString().Contains(searchstr) ||
    //                      p.Field<string>("RemitterName").ToUpper().Contains(searchstr) ||
    //                      (!string.IsNullOrEmpty(p.Field<string>("Bankdate")) && p.Field<string>("bankdate").Contains(searchstr)) ||
    //                      p.Field<string>("TotalAmount").ToUpper().Contains(searchstr) ||
    //                      p.Field<string>("Paymenttype").ToUpper().Contains(searchstr) ||
    //                      (!string.IsNullOrEmpty(p.Field<string>("Status")) && p.Field<string>("Status").Contains(searchstr)) ||
    //                      p.Field<Int64>("row").ToString().Contains(searchstr) ||
    //                      p.Field<string>("BankCode").ToString().Contains(searchstr) ||
    //                      p.Field<string>("VerifyStatus").ToString().Contains(searchstr)
    //                select new
    //                {
    //                    GRN = p.Field<Int64>("GRN"),
    //                    RemitterName = p.Field<string>("RemitterName"),
    //                    Bankdate = p.Field<string>("bankdate"),
    //                    BankName = p.Field<string>("BankName"),
    //                    TotalAmount = p.Field<string>("TotalAmount"),
    //                    Paymenttype = p.Field<string>("Paymenttype"),
    //                    Status = p.Field<string>("Status"),
    //                    row = p.Field<Int64>("row"),
    //                    VerifyStatus = p.Field<string>("VerifyStatus"),
    //                    BankCode = p.Field<string>("BankCode")
    //                };
    //    if (query.Count() > 0)
    //    {
    //        LabelMatch.Visible = false;
    //        rptChallanFill.DataSource = query;
    //        rptChallanFill.DataBind();
    //    }
    //    else
    //    {
    //        LabelMatch.Visible = true;
    //        LabelMatch.Text = "No Match Found";
    //        rptChallanFill.DataSource = null;
    //        rptChallanFill.DataBind();
    //    }
    //}


    #region Paging on Repeater
    public int CurrentPage   // maintain indexing with currentpage 
    {
        get
        {
            // look for current page in ViewState
            object o = this.ViewState["_CurrentPage"];
            if (o == null)
                return 0; // default page index of 0
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }


    #endregion
    private void GetGrnList(int pageIndex)
    {
        //DataTable dt = new DataTable();

        parameters(pageIndex);
        //if (ViewState["GrnList"] == null)
        //{
        if ((objEgChallanDetail.Todate - objEgChallanDetail.fromDate).TotalDays > 30)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 30 days');", true);
            return;
        }
        else
        {

            dt = new DataTable();
            dt = objEgChallanDetail.FillSearchRecord();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                //DataTable paggedtable = new DataTable();
                //paggedtable = dt.Clone();
                int rowSize = count >= 10 ? 10 : count;
                //int initial = (pageIndex - 1) * rowSize + 1;
                //int last = initial + rowSize - 1;

                //for (int i = 0; i < rowSize; i++)
                //{
                //    paggedtable.ImportRow(dt.Rows[i]);
                //}
                rptChallanFill.DataSource = dt;
                rptChallanFill.DataBind();
                //ViewState["GrnList"] = dt;
                trPage.Visible = true;
                rptChallanFill.Visible = true;
                //RowSearch.Visible = true;
                //LabelMatch.Visible = false;
                rowLabel.Visible = false;
                //txtSearch.Text = "";
                lblrecord.Visible = true;
                lblCurrentPage.Visible = true;
                lblrecord.Text = "Total Record :" + dt.Rows[0]["TotalCount"];
                lblCurrentPage.Text = "Page Number : " + pageIndex;
                PopulatePager(Convert.ToInt32(dt.Rows[0]["TotalCount"].ToString()), pageIndex);
            }
            else
            {
                rptChallanFill.Visible = false;
                //RowSearch.Visible = false;
                rowLabel.Visible = true;
                lblrecord.Visible = false;
                trPage.Visible = false;
                lblCurrentPage.Visible = false;
                lblEmptyData.Text = "No Record Found";
            }

        }
        //dt.Dispose();
        //}
        //else
        //{
        //    dt = (DataTable)ViewState["GrnList"];
        //    DataTable paggedtable = new DataTable();
        //    paggedtable = dt.Clone();
        //    int initial = (pageIndex - 1) * 10 + 1;
        //    int last = initial + 10 - 1;
        //    if (last > dt.Rows.Count)
        //    {
        //        last = dt.Rows.Count;
        //    }

        //    for (int i = initial - 1; i < last; i++)
        //    {
        //        paggedtable.ImportRow(dt.Rows[i]);
        //    }
        //    rptChallanFill.DataSource = paggedtable;
        //    rptChallanFill.DataBind();
        //    PopulatePager(dt.Rows.Count, pageIndex);
        //}
        //dt.Dispose();

    }
    private void PopulatePager(Int32 recordCount, int currentPage)
    {
        {
            //recordCount = 18;
            double totalPageCount = (double)((decimal)recordCount / 10);
            int pageCount = (int)Math.Ceiling(totalPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                if (currentPage > 1)
                {
                    pages.Add(new ListItem("<u>First</u>", "1", currentPage > 1));
                }

                if (currentPage != 1)
                {
                    pages.Add(new ListItem("<u>Previous</u>", (currentPage - 1).ToString()));
                }
                if (pageCount < 10)
                {
                    for (int i = 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else if (currentPage < 10)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                else if (currentPage > pageCount - 10)
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 1; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else
                {
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                    for (int i = currentPage - 2; i <= currentPage + 2; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("...", (currentPage).ToString(), false));
                }
                if (currentPage != pageCount)
                {
                    pages.Add(new ListItem("<u>Next</u>", (currentPage + 1).ToString()));
                    pages.Add(new ListItem("<u>Last</u>", pageCount.ToString(), currentPage < pageCount));

                }
            }

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }

        //List<ListItem> pages = new List<ListItem>();
        //int startIndex, endIndex;
        //int pagerSpan = 5;

        ////Calculate the Start and End Index of pages to be displayed.
        //double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(10));
        //int pageCount = (int)Math.Ceiling(dblPageCount);
        //startIndex = currentPage > 0 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        //endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        //if (currentPage > pagerSpan % 2)
        //{
        //    if (currentPage == 2)
        //    {
        //        endIndex = 5;
        //    }
        //    else
        //    {
        //        endIndex = currentPage + 2;
        //    }
        //}
        //else
        //{
        //    endIndex = (pagerSpan - currentPage) + 1;
        //}

        //if (endIndex - (pagerSpan - 1) > startIndex)
        //{
        //    startIndex = endIndex - (pagerSpan - 1);
        //}

        //if (endIndex > pageCount)
        //{
        //    endIndex = pageCount;
        //    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        //}

        ////Add the First Page Button.
        //if (currentPage > 1)
        //{
        //    pages.Add(new ListItem("First ", "1"));
        //}

        ////Add the Previous Button.
        //if (currentPage > 1)
        //{
        //    pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        //}

        //for (int i = startIndex; i <= endIndex; i++)
        //{
        //    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        //}

        ////Add the Next Button.
        //if (currentPage < pageCount)
        //{
        //    pages.Add(new ListItem(" >> ", (currentPage + 1).ToString()));
        //}

        ////Add the Last Button.
        //if (currentPage != pageCount)
        //{
        //    pages.Add(new ListItem(" Last", pageCount.ToString()));
        //}
        //rptPager.DataSource = pages;
        //rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetGrnList(pageIndex);
    }

}
