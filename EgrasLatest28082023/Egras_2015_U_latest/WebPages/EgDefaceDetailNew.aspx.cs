using System;
using System.Data;
using System.Web.UI;
using System.Text;
using EgBL;
using System.Collections.Generic;

public partial class WebPages_EgDefaceDetailNew : System.Web.UI.Page
{
    StringBuilder sb;
    EgDepartmentBL objDepartment;
    EgEncryptDecrypt ObjEncrytDecrypt;
    EgEChallanBL objEgEChallan;
    EgReleaseDefacedEntryBL objRelease;
    List<string> strList;
    int Result = 0;
    string GRN, UserId, Usertype;
    int challanNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
            Response.Redirect("~\\LoginAgain.aspx");
        }
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
                    if (strList[0].ToString() == "GRN")
                    {
                        GRN = strList[1].ToString();
                    }
                    else
                    {
                        challanNo = Convert.ToInt32(strList[1].ToString());
                    }
                    UserId = strList[3].ToString();
                    Usertype = strList[5].ToString();
                    if (strList[7].ToString() == "1")
                    {
                        ViewState["isPopUp"] = 1;
                        var menu = Page.Master.FindControl("vmenu1") as UserControl;
                        menu.Visible = false;
                        var lnk = Page.Master.FindControl("lnkLogout") as Control;
                        lnk.Visible = false;
                        UserControl uc = (UserControl)this.Page.Master.FindControl("hmenu1");
                        uc.Visible = false;
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
        if (!IsPostBack)
        {

            sb = new StringBuilder();
            objDepartment = new EgDepartmentBL();
            objEgEChallan = new EgEChallanBL();
            DataTable dt;
            DataTable dt1;
            objEgEChallan.GRNNumber = Convert.ToInt32(GRN);
            objEgEChallan.ChallanNo = challanNo;
            objEgEChallan.UserId = Convert.ToInt32(UserId);
            objEgEChallan.UserType = Usertype;
             
            //add on 18 may 2016
            objEgEChallan.FormId = "S";
             Result = objEgEChallan.EChallanView();

            //Departmentlbl.Text = objEgEChallan.Location;
            if (Result == 1)
            {

                //string GEN = "000000000" + Convert.ToString(objEgEChallan.GRNNumber);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                //Gennolbl.Text = GEN.Substring(GEN.Length - 10, 10);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                Gennolbl.Text = Convert.ToString(objEgEChallan.GRNNumber);
                formdatelbl.Text = objEgEChallan.ChallanDate.Substring(0, 10);

                //if (objEgEChallan.TypeofPayment == "M")
                //{
                //    TypeOfPaymentlbl.Text = "Manual";
                //}
                //else
                //{
                //    TypeOfPaymentlbl.Text = "Online";
                //}


                lblDept1.Text = objEgEChallan.Location;
                Tinlbl.Text = objEgEChallan.Identity;
                OfficeNamelbl.Text = objEgEChallan.Office;
                PanNolbl.Text = objEgEChallan.PanNumber;
                Locationlbl.Text = objEgEChallan.TreasuryName;
                fullnamelbl.Text = objEgEChallan.FullName;
                frommonthlbl.Text = Convert.ToString(objEgEChallan.ChallanFromMonth.ToString("dd/MM/yyyy"));
                tomonthlbl.Text = Convert.ToString(objEgEChallan.ChallanToMonth.ToString("dd/MM/yyyy"));
                TextBox1.Text = objEgEChallan.Address;
                Townlbl.Text = objEgEChallan.CityName.ToString();
                Pinlbl.Text = "(" + objEgEChallan.PINCode + ")";
                TextBox2.Text = objEgEChallan.Remark;            //":-Challan Working Good";
                deductcommisiionlbl.Text = objEgEChallan.DeductCommission.ToString("0.00");
                NetAmountlbl.Text = objEgEChallan.TotalAmount;
                objDepartment.UserId = objEgEChallan.UserId;
                objDepartment.Grn = Convert.ToInt32(Gennolbl.Text);
                // txtDeptRemarks.Text = objEgEChallan.DepartmentRemarks;
                //start DMFT
                if (objEgEChallan.PDacc > 0)
                {
                    trPdaccNo.Visible = true;
                    lblPdAccNo.Text = objEgEChallan.PDAccName.ToString();
                }
                //End DMFT 
                // 19 july 2019
                dt1 = objEgEChallan.fillChallan();
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                dt1.Dispose();
                dt = new DataTable();
                dt = objDepartment.FetDefaceDetails();

                Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "checkamounttotal()", true);

                // get RElease aMOUNT---
                //objRelease = new EgReleaseDefacedEntryBL();
                //objRelease.Grn= objDepartment.Grn;
                //double RelAmt = objRelease.GetReleaseAmount();

                if (dt.Rows.Count > 0)
                {
                    //////*************************Table Creation For Defaced Amount**************
                    sb.Append("<table width='400' id='module' border='1' cellpadding='1' cellspacing='0' style='background-color:transparent;border-color:green;color:#CA3B2B;'");
                    sb.Append("<tr><td colspan='4'align='center'  style='font-size: 12pt;padding-left:3px;'>Deface Detail(D*Deface,R*Refund)</td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                    sb.Append("S.No.");
                    sb.Append("&nbsp;</td>");
                    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                    sb.Append("Date");
                    sb.Append("&nbsp;</td>");
                    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                    sb.Append("Amount");
                    sb.Append("&nbsp;</td>");
                    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                    sb.Append("Status/BillNo/Date");
                    sb.Append("&nbsp;</td>");
                    sb.Append("<td style='font-size: 12pt;padding-left:3px;color:dark red;'>&nbsp;");
                    sb.Append("Action By");
                    sb.Append("&nbsp;</td>");


                    
                    sb.Append("</tr>");
                    int k;
                    if (dt.Rows.Count > 0)
                    {

                        for (k = 0; k <= dt.Rows.Count - 1; k++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style='font-size: 12pt;padding-left:3px;'>&nbsp;");
                            sb.Append(k + 1);
                            sb.Append("&nbsp;</td>");
                            sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");
                            sb.Append(dt.Rows[k]["TransDate"].ToString());
                            sb.Append("&nbsp;</td>");
                            sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");
                            sb.Append(dt.Rows[k]["Amount"].ToString());
                            sb.Append("&nbsp;</td>");

                            sb.Append("<td style='font-size:12pt;bold;padding-left:3px;'>&nbsp;");
                            sb.Append(dt.Rows[k]["deface"].ToString());

                            //if (dt.Rows[k]["deface"].ToString().Trim() == "R")
                            //    sb.Append("R/" + Convert.ToString(dt.Rows[k]["BillNo"]) + "/" + dt.Rows[k]["BillDate"].ToString());
                            //else if (dt.Rows[k]["deface"].ToString().Trim() == "T")
                            //    sb.Append("Released By E-TO");
                            //else
                            //    sb.Append("D");
                            sb.Append("&nbsp;</td>");

                            sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");
                            sb.Append(dt.Rows[k]["ActionBy"].ToString());
                            sb.Append("&nbsp;</td>");
                            sb.Append("</tr>");

                        }
                    }
                    //Commented by sandeep on 24/02/2016
                    //if (RelAmt != 0.0)
                    //{
                    //    sb.Append("<tr>");
                    //    sb.Append("<td style='font-size: 12pt;padding-left:3px;'>&nbsp;");
                    //    sb.Append(k + 1);
                    //    sb.Append("&nbsp;</td>");
                    //    sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");

                    //    sb.Append("&nbsp;</td>");
                    //    sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;'>&nbsp;");
                    //    sb.Append(RelAmt.ToString());
                    //    sb.Append("&nbsp;</td>");
                    //    sb.Append("<td style='font-size:12pt;bold;padding-left:3px;'>&nbsp;");
                    //    sb.Append("Released Amount");
                    //    sb.Append("&nbsp;</td>");
                    //    sb.Append("</tr>");
                    //}
                    sb.Append("</table>");
                    //////*************************Table Creation For Defaced Amount**************

                    literal1.Text = sb.ToString();
                    literal1.Visible = true;
                }


                // img.ImageUrl = ("../image/captcha.ashx?arg=" + grnid);
                //ChequeNolbl.Text = objEgEChallan.ChequeDDNo;
                nameofbanklbl.Text = objEgEChallan.BankName;

                // nameofbranchlbl.Text = objEgEChallan.BankBranch;
                if (objEgEChallan.TransDate != "")
                {
                    lbltransdate.Visible = true;
                    lbltransdate.Text = Convert.ToString(objEgEChallan.ChallanDate);

                }
                else
                {
                    lbltransdate.Visible = false;
                }
                if (objEgEChallan.Details != "")
                {
                    lnkExtraDetails.Visible = true;

                }

                if (objEgEChallan.ChallanNo != 0)
                {
                    lblChallan.Visible = true;
                    lblChallan.Text = "Challan No.   -  " + "<B>" + objEgEChallan.ChallanNo.ToString() + "<B/>";
                }
                Amountwordslbl.Text = objEgEChallan.AmountInWords;

                //objEgEChallan.fillChallan(GridView1);

                //Bitmap barcode = CreateBarCode(GEN.Substring(GEN.Length - 10, 10));// Commented By Sandeep on 02/03/2017 for removing Leading zeros
                //Bitmap barcode = CreateBarCode(Convert.ToString(objEgEChallan.GRNNumber));
                //barcode.Save(Server.MapPath("../Image/barcode.Gif"), ImageFormat.Gif);
                //Image2.ImageUrl = "~/Image/barcode.Gif";  //"barcode.aspx?path=" + objEgEChallan.Barcode;
                //barcode.Dispose();


                lblCIN.Text = Convert.ToString(objEgEChallan.CIN);
                lblRef.Text = Convert.ToString(objEgEChallan.Ref);
                if (lblCIN.Text == "0" && lblRef.Text == "0")
                {
                    lblCIN.Visible = false;
                    lblRef.Visible = false;
                }
                else
                {
                    lblCIN.Visible = true;
                    lblRef.Visible = true;
                }

            }
            else
            {
                ObjEncrytDecrypt = new EgEncryptDecrypt();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ScriptKey", "alert('Grn Does Not Belong To User.');window.location='Reports/EgGRNchallanView.aspx'; ", true);
                if (ViewState["isPopUp"] != null && ViewState["isPopUp"].ToString() == "1")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();", true);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect","alert('Record Not Found  OR  User Have Not Access Permission Particular GRN '); window.location='" 
                //    +Request.ApplicationPath + "/Webpages/Reports/EgGRNchallanView.aspx';", true);
            }
        }
      
    }
    //protected Bitmap CreateBarCode(string data)
    //{
    //    string Code = data;

    //    // Multiply the lenght of the code by 25 (just to have enough width)
    //    int w = Code.Length * 25;

    //    // Create a bitmap object of the width that we calculated and height of 30
    //    Bitmap oBitmap = new Bitmap(w, 30);
    //    Graphics oGraphics = Graphics.FromImage(oBitmap);

    //    PrivateFontCollection fnts = new PrivateFontCollection();
    //    fnts.AddFontFile(Server.MapPath("../WebPages/font/IDAutomationHC39M.ttf"));
    //    FontFamily fntfam = new FontFamily("IDAutomationHC39M", fnts);
    //    Font oFont = new Font(fntfam, 25);

    //    PointF oPoint = new PointF(2f, 2f);
    //    SolidBrush oBrushWrite = new SolidBrush(Color.Black);
    //    SolidBrush oBrush = new SolidBrush(Color.White);

    //    oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

    //    oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);
    //    oGraphics.Dispose();
    //    return oBitmap;
    //}



    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        // By Rachit Sharma on 20 Nov 2014
        // Status : DONE
        EgEncryptDecrypt ObjEncryptDecrypt = new EgEncryptDecrypt();
        string strURLWithData = "";
        strURLWithData = ObjEncryptDecrypt.Encrypt(string.Format("GRN={0}", GRN.ToString()));
        strURLWithData = "Reports/EgEchallanViewPDF.aspx?" + strURLWithData.ToString();
        Response.Redirect(strURLWithData);

    }
    protected override object LoadPageStateFromPersistenceMedium()
    {
        return Session["_ViewState"];
    }

    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        Session["_ViewState"] = viewState;
    }
}
