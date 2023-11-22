using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using AjaxControlToolkit;
using System.IO;

public partial class WebPages_EgDepartment : System.Web.UI.Page
{

    EgDepartmentBL objDepartment;
    EgDeptAmountRptBL objEgDeptAmountRptBL;
    EgEncryptDecrypt ObjEncryptDecrypt;




    protected void Page_Load(object sender, EventArgs e)
    {

        objEgDeptAmountRptBL = new EgDeptAmountRptBL();

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (!IsPostBack)
        {
            SetFocus(txtgrn);
            objEgDeptAmountRptBL.UserId = Convert.ToInt32(Session["UserId"].ToString());

        }
    }

    /// <summary>
    /// Bind Gridview with respective data
    /// </summary>
    public void Bind()
    {

        objDepartment = new EgDepartmentBL();
        GenralFunction BLobj = new GenralFunction();

        objDepartment.UserId = Convert.ToInt32(Session["UserId"].ToString());

        if (txtgrn.Text != "")
        {
            objDepartment.Grn = Convert.ToInt32(txtgrn.Text);
            if (Session["UserType"].ToString() == "4")
            {
                //  System.Data.SqlClient.SqlTransaction Trans = BLobj.Begintrans();
                string Deptexist = objDepartment.GetDeptExistance();
                //    BLobj.Endtrans();
                if (Convert.ToInt32(Deptexist.Substring(0)) > 0)
                {
                    DataTable dt = new DataTable();
                    ViewState["UserId"] = Convert.ToInt32(Deptexist.Substring(1, Convert.ToInt32(Deptexist.Length.ToString()) - 1));

                    dt = objDepartment.FillGrid();
                    if (dt.Rows.Count > 0)
                    {
                        grdprofile.DataSource = dt;
                        grdprofile.DataBind();

                    }
                    dt.Dispose();
                }
                else
                {
                    grdprofile.DataSource = null;
                    grdprofile.DataBind();
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('GRN not found.!')", true);
                }
            }
            if (Session["UserType"].ToString() == "3")
            {
                DataTable dt = new DataTable();
                dt = objDepartment.BindGrid();
                if (dt.Rows.Count > 0)
                {
                    grdprofile.DataSource = dt;
                    grdprofile.DataBind();

                }
                dt.Dispose();
            }
        }
        else
        {
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {

                string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
                objDepartment.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
                objDepartment.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
                if ((objDepartment.ToDate - objDepartment.FromDate).TotalDays > 30)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Date difference cannot be greater than 30 days');", true);
                    return;
                }
                if (Session["UserType"].ToString() == "4")
                {
                    DataTable dt = new DataTable();
                    dt = objDepartment.FillGrid();
                    grdprofile.DataSource = dt;
                    grdprofile.DataBind();
                }
                else if (Session["UserType"].ToString() == "3")
                {
                    DataTable dt = new DataTable();
                    dt = objDepartment.BindGrid();
                    if (dt.Rows.Count > 0)
                    {
                        grdprofile.DataSource = dt;
                        grdprofile.DataBind();
                    }
                    dt.Dispose();
                }
            }

        }
    }


    protected void imgbtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        objDepartment = new EgDepartmentBL();
        txtAmount.Text = "";
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        objDepartment.Grn = Convert.ToInt32(grdprofile.DataKeys[gvrow.RowIndex].Value);
        lblgrn.Text = objDepartment.Grn.ToString();
        //objDepartment.Grn = Convert.ToInt32(gvrow.Cells[0].Text);
        // Get Remaining amount
        objDepartment.GetPartialAmount();
        EgReleaseDefacedEntryBL objRelease = new EgReleaseDefacedEntryBL();
        objRelease.Grn = objDepartment.Grn;
        double RelAmt = objRelease.GetReleaseAmount();
        double amt = objDepartment.amount;
        if (amt == 0.0)
        {
            lbllastDeface.Text = "No Amount Deface";
        }
        else
        {
            lbllastDeface.Text = Convert.ToString(amt);
            lblrelAmount.Text = Convert.ToString(RelAmt);
        }

        if (RelAmt == 0.0)
            lblReleasedAmt.Text = "No Amount Released";
        else
            lblReleasedAmt.Text = Convert.ToString(RelAmt);

        objRelease.GetRefundAmount();//sandeep 11/01/2016
        double Refamt = objRelease.RefundAmount;
        if (Refamt == 0.0)
        {
            lblRefundedAmt.Text = "No Amount Refunded";
        }
        else
        {
            lblRefundedAmt.Text = Convert.ToString(Refamt);
        }
        lblTotal.Text = Convert.ToString(Convert.ToDouble(gvrow.Cells[3].Text));
        lblamt.Text = ((amt - RelAmt) + Refamt).ToString();
        //lblRemaining.Text = (Convert.ToDouble(gvrow.Cells[3].Text) - Convert.ToDouble(amt.ToString()) - Refamt).ToString();
        lblRemaining.Text = (Convert.ToDouble(gvrow.Cells[3].Text) - ((amt - RelAmt) + Refamt)).ToString();

        this.ModalPopupExtender1.Show();
        AjaxControlToolkit.Utility.SetFocusOnLoad(txtAmount);
    }

    /// <summary>
    /// used to deface the amount fully
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtn1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ImageButton btndetails = sender as ImageButton;
        GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
        objDepartment = new EgDepartmentBL();
        objDepartment.UserId = Convert.ToInt32(Session["UserId"].ToString());
        objDepartment.Grn = Convert.ToInt32(grdprofile.DataKeys[gvrow.RowIndex].Value);
        objDepartment.deface = "F";
        objDepartment.amount = Convert.ToDouble(gvrow.Cells[3].Text);
        int i = objDepartment.InsertFullDeface();
        if (i == 1)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('(Record Defaced Successfully.')", true);
            Bind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Not Defaced.')", true);
        }
    }

    /// <summary>
    /// used to deface the amount partially in popup window 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        LinkButton btndetails = sender as LinkButton;

        objDepartment = new EgDepartmentBL();
        if (txtAmount.Text.ToString() != "")
        {


            double remainamt = Math.Round(Convert.ToDouble(lblTotal.Text), 2) - Math.Round(Convert.ToDouble(lblamt.Text), 2);
            if (remainamt == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('You have Completly Defaced amount.')", true);
            }

            else if (Math.Round(Convert.ToDouble(txtAmount.Text), 2) > Math.Round(remainamt, 2))
            {

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Deface Amount can not be greater than Total Amount.')", true);
                txtAmount.Text = "";
            }

            else if (Math.Round(Convert.ToDouble(txtAmount.Text), 2) == 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Deface Amount can not be zero.')", true);
                txtAmount.Text = "";
            }

            else
            {
                // updated by CP for  Deface amount no change. dated : 30/03/2015

                double txtAmt = Math.Pow(Convert.ToDouble(txtAmount.Text), 1.1) + 19;

                if (Convert.ToDouble((HiddenTextAmt.Value)).ToString("0.00") != txtAmt.ToString("0.00"))
                    return;
                objDepartment.UserId = Convert.ToInt32(Session["UserId"].ToString());
                objDepartment.Grn = Convert.ToInt32(lblgrn.Text);
                objDepartment.deface = "P";
                objDepartment.amount = Convert.ToDouble(txtAmount.Text);


                int i = objDepartment.InsertPartialDeface();
                if (i == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Defaced Successfully.')", true);

                    Bind();


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Record Not Defaced.')", true);
                }

                txtAmount.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "MSG", "alert('Plz Fill Amount)", true);
        }
    }
    /// <summary>
    /// checks record is defaced fully or partially
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdprofile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objDepartment = new EgDepartmentBL();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnlnk = (LinkButton)e.Row.FindControl("lnkgrn");
            objDepartment.Grn = Convert.ToInt64(grdprofile.DataKeys[e.Row.RowIndex].Value);//Convert.ToInt32(btnlnk.Tex
            // check deface partially or fully 
            objDepartment.CheckDeface();
            if (objDepartment.deface == "P")
            {
                ImageButton ib1 = (ImageButton)e.Row.FindControl("imgbtn1");
                ib1.Enabled = false;
                ib1.ToolTip = "Can't Deface Amount";
            }
            //if (objDepartment.deface == "F")
            //{
            //    ImageButton ib = (ImageButton)e.Row.FindControl("imgbtn");
            //    ib.Enabled = false;
            //    ib.ImageUrl = "~/Image/delete.png";
            //    ib.Width = 16;
            //    ib.Height = 12;
            //    ib.ToolTip = "Can't Deface Amount";
            //    ImageButton ib1 = (ImageButton)e.Row.FindControl("imgbtn1");
            //    ib1.Enabled = false;
            //    ib1.ImageUrl = "~/Image/delete.png";
            //    ib1.Width = 16;
            //    ib1.Height = 12;
            //    ib1.ToolTip = "Can't Deface Amount";
            //}


            objDepartment.GetPartialAmount();
            EgReleaseDefacedEntryBL objRelease = new EgReleaseDefacedEntryBL();
            objRelease.Grn = objDepartment.Grn;
            double RelAmt = objRelease.GetReleaseAmount();
            objRelease.GetRefundAmount();
            double Refamt = objRelease.RefundAmount;
            if ((Convert.ToDouble(e.Row.Cells[3].Text) - Refamt - Convert.ToDouble(objDepartment.amount) + RelAmt) == 0 && (objDepartment.deface == "F"))
            {
                ImageButton ib = (ImageButton)e.Row.FindControl("imgbtn");
                ib.Enabled = false;
                ib.ImageUrl = "~/Image/delete.png";
                ib.Width = 16;
                ib.Height = 12;
                ib.ToolTip = "Can't Deface Amount";
                ImageButton ib1 = (ImageButton)e.Row.FindControl("imgbtn1");
                ib1.Enabled = false;
                ib1.ImageUrl = "~/Image/delete.png";
                ib1.Width = 16;
                ib1.Height = 12;
                ib1.ToolTip = "Can't Deface Amount";
            }
            // check Amount is not remaining
            if ((Convert.ToDouble(e.Row.Cells[3].Text) - Refamt - Convert.ToDouble(objDepartment.amount) + RelAmt) == 0)
            {
                ImageButton ib = (ImageButton)e.Row.FindControl("imgbtn");
                ib.Enabled = false;
                ib.ImageUrl = "~/Image/delete.png";
                ib.Width = 16;
                ib.Height = 12;
                ib.ToolTip = "Can't Deface Amount";
                ImageButton ib1 = (ImageButton)e.Row.FindControl("imgbtn1");
                ib1.Enabled = false;
                ib1.ImageUrl = "~/Image/delete.png";
                ib1.Width = 16;
                ib1.Height = 12;
                ib1.ToolTip = "Can't Deface Amount";
            }
        }

    }








    /// <summary>
    /// Add New Function show deface Amount Detail on Cross button 
    /// </summary>
    /// <param name="contextKey"></param>
    /// <returns> String Deface Amount Detail with defaceDate and Amount</returns>


    [System.Web.Services.WebMethodAttribute(),
    System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetDynamicContent(string contextKey)
    {
        EgDepartmentBL objDepartment = new EgDepartmentBL();
        DataTable dt = new DataTable();

        objDepartment.Grn = Convert.ToInt32(contextKey.ToString());

        dt = objDepartment.FillDefaceDetail();

        StringBuilder b = new StringBuilder();
        if (dt.Rows.Count > 0)
        {


            b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
            b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

            b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>");
            b.Append("<b>Deface Details</b>"); b.Append("</td></tr>");
            b.Append("<tr><td style='width:80px;'><b>TransactionDate</b></td>");
            b.Append("<td style='width:80px;'><b>Amount</b></td>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                b.Append("<tr>");
                b.Append("<td>" + dt.Rows[i]["TransDate"].ToString() + "</td>");
                b.Append("<td>" + dt.Rows[i]["Amount"].ToString() + "</td>");

                b.Append("</tr>");
            }
            b.Append("</table>");

            return b.ToString();
        }
        else
        {
            b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ");
            b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");

            b.Append("<tr><td  style='background-color:#336699; color:white;'>");
            b.Append("<b>Deface Details</b>"); b.Append("</td></tr>");
            b.Append("<tr align='center'><td style='width:80px;'><b>No Reocrd Found</b></td>");


            b.Append("</table>");

            return b.ToString();


        }
    }

    protected void grdprofile_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

            string behaviorID = "pce_" + e.Row.RowIndex;
            //pce.BehaviorID = behaviorID;

            //Image img = (Image)e.Row.FindControl("Imagesearch");

            string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
            string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

            //img.Attributes.Add("onmouseover", OnMouseOverScript);
            //img.Attributes.Add("onmouseout", OnMouseOutScript);
        }
    }

    /// <summary>
    /// Bind all records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Bind();
    }



    protected Bitmap CreateBarCode(string data)
    {
        string Code = data;

        // Multiply the lenght of the code by 25 (just to have enough width)
        int w = Code.Length * 25;

        // Create a bitmap object of the width that we calculated and height of 30
        Bitmap oBitmap = new Bitmap(w, 30);
        Graphics oGraphics = Graphics.FromImage(oBitmap);

        PrivateFontCollection fnts = new PrivateFontCollection();
        fnts.AddFontFile(Server.MapPath("../WebPages/font/IDAutomationHC39M.ttf"));
        FontFamily fntfam = new FontFamily("IDAutomationHC39M", fnts);
        Font oFont = new Font(fntfam, 25);

        PointF oPoint = new PointF(2f, 2f);
        SolidBrush oBrushWrite = new SolidBrush(Color.Black);
        SolidBrush oBrush = new SolidBrush(Color.White);

        oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

        oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);

        return oBitmap;
    }
    protected void grdprofile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ObjEncryptDecrypt = new EgEncryptDecrypt();
        if (e.CommandName == "grnbind")
        {
            LinkButton lb = (LinkButton)e.CommandSource;
            int grn = Convert.ToInt32(lb.Text);

            string strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}&userId={1}&usertype={2}&Dept={3}", lb.Text, Session["UserId"].ToString(), Session["UserType"].ToString(), "1"));
            //   lb.Attributes.Add("Onmouseover", "openPopUp('" + strURLWithData + "')");
            //strURLWithData = "EgDefaceDetailNew.aspx?" + strURLWithData;
            string script = "window.open('EgDefaceDetailNew.aspx?" + strURLWithData + "','window','Height=600px,width=1020px,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", script, true);
            //Response.Redirect(strURLWithData);
            //   popupWindow = window.open("EgDefaceDetailNew.aspx?" + str, 'popUpWindow', 'height=600,width=1020,left=152,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    }

    //protected override object LoadPageStateFromPersistenceMedium()
    //{
    //    return Session["_ViewState"];
    //}

    //protected override void SavePageStateToPersistenceMedium(object viewState)
    //{
    //    Session["_ViewState"] = viewState;
    //}


}