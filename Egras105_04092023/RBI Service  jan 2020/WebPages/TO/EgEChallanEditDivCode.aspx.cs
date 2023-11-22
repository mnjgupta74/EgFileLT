using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgBL;
using Microsoft.Reporting.WebForms;

public partial class WebPages_EgEChallanEditDivCode_temp : System.Web.UI.Page
{
    DataTable dt;
    EgEChallanEditDivCodeBL ObjEditDiv;
    EgEChallanBL objEgEChallan;
    DataTable DivList;
    DataTable dt1;
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\Default.aspx");
        }
        if (!IsPostBack)   // fill DropDownList on PageLoad
        {
            ObjEditDiv = new EgEChallanEditDivCodeBL();
            ObjEditDiv.UserID = Convert.ToInt32(Session["UserId"].ToString());
            ObjEditDiv.FillDivisionList(divcode);
            Session["DivList"] = ObjEditDiv.DivCodeList;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FunctionShowData();
    }

    public void FunctionShowData()
    {
        DataTable dt = new DataTable();

        if (Page.IsValid)
        {
            if (rbtnList.SelectedValue == "3")
            {
                EgEChallanEditDivCodeBL objEgEChallanEditDivCodeBL = new EgEChallanEditDivCodeBL();
                objEgEChallanEditDivCodeBL.GRN = Convert.ToInt64(txtGRNSearch.Text.Trim());
                objEgEChallanEditDivCodeBL.UserID = Convert.ToInt32(Session["UserId"].ToString());
                dt = objEgEChallanEditDivCodeBL.GetGRNChallanList();
                rptrManualSuccess.DataSource = dt;
                rptrManualSuccess.DataBind();
                dt.Dispose();
            }
            else
            {
                EgEChallanEditDivCodeBL objManaulSuccessCHallan = new EgEChallanEditDivCodeBL();
                if (txtmajorHead.Text != "")
                    objManaulSuccessCHallan.MajorHead = txtmajorHead.Text.Trim();
                else
                {
                    trrpt.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Enter MajorHead.')", true);
                    return;
                }
                string[] fromdate = txtFromDate.Text.Trim().Replace("-", "/").Split('/');
                objManaulSuccessCHallan.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());
                string[] todate = txtToDate.Text.Trim().Replace("-", "/").Split('/');
                objManaulSuccessCHallan.ToDate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());
                objManaulSuccessCHallan.officeCode = Convert.ToInt32(divcode.SelectedValue.Split('|').GetValue(0));
                //if (((objManaulSuccessCHallan.ToDate - objManaulSuccessCHallan.FromDate).TotalDays) > 15)
                //{
                //    trrpt.Visible = false;
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Date difference cannot be more than 15 days.')", true);
                //    return;
                //}
                objManaulSuccessCHallan.Type = Convert.ToInt16(rbtnList.SelectedValue);
                objManaulSuccessCHallan.UserID = Convert.ToInt32(Session["UserId"].ToString());
                if (((objManaulSuccessCHallan.ToDate - objManaulSuccessCHallan.FromDate).TotalDays) < 0)
                {
                    trrpt.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('From Date Must be less Then ToDate.')", true);
                    return;
                }
                else
                {
                    dt = objManaulSuccessCHallan.GetChallanList();
                    rptrManualSuccess.DataSource = dt;
                    rptrManualSuccess.DataBind();
                    dt.Dispose();

                }
            }
        }
    }

    protected void rptrManualSuccess_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            Label lblDivCode = (Label)e.Item.FindControl("lblDivCode");
            ViewState["DivCode"] = lblDivCode.Text;
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlDivCode");
            LinkButton lblCancel = (LinkButton)e.Item.FindControl("lnkCancel");
            lblDivCode.Visible = false;
            ddl.Visible = true;
            EgEChallanEditDivCodeBL objEgEChallanEditDivCodeBL = new EgEChallanEditDivCodeBL();
            if (lblEdit.Text == "Edit")
            {
                lblEdit.Text = "Update";
                lblCancel.Visible = true;
                ddl.Enabled = true;
                //objEgEChallanEditDivCodeBL.GRN = Convert.ToInt64(e.CommandArgument.ToString());
                ddl.DataTextField = "Divname";
                ddl.DataValueField = "SubDivisionofficecode";
                ddl.DataSource = Session["DivList"];
                ddl.DataBind();
                foreach (ListItem item in ddl.Items)
                {
                    if (item.Value.Split('|').GetValue(1).ToString() == lblDivCode.Text)
                    {
                        ddl.ClearSelection();
                        ddl.Items.FindByValue(item.Value).Selected = true;
                    }
                }
                //objEgEChallanEditDivCodeBL.FillDivisionListGRNWise(ddl);
            }
            else
            {
                objEgEChallanEditDivCodeBL.GRN = Convert.ToInt64(e.CommandArgument.ToString());
                objEgEChallanEditDivCodeBL.officeCode = Convert.ToInt32(ddl.SelectedValue.Split('|').GetValue(0));

                if (Convert.ToInt32(ViewState["DivCode"]) == 0)
                {
                    objEgEChallanEditDivCodeBL.DivCode = Convert.ToInt32(ddl.SelectedValue.Split('|').GetValue(1));
                    objEgEChallanEditDivCodeBL.UpdateDivCodeDetail();
                    FunctionShowData();
                }
                else
                {
                    if (Convert.ToInt32(ddl.SelectedValue.Split('|').GetValue(1)) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "clentscript", "alert('Division Code Could not change with Zero!!')", true);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('Division Code is not equal to Zero')", true);
                    }
                    else
                    {
                        objEgEChallanEditDivCodeBL.DivCode = Convert.ToInt32(ddl.SelectedValue.Split('|').GetValue(1));
                        objEgEChallanEditDivCodeBL.UpdateDivCodeDetail();
                        FunctionShowData();
                    }
                }



                
            }
        }
        else if (e.CommandName == "Cancel")
        {
            LinkButton lblEdit = (LinkButton)e.Item.FindControl("lblEdit");
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlDivCode");
            LinkButton lblCancel = (LinkButton)e.Item.FindControl("lnkCancel");
            Label lblDivCode = (Label)e.Item.FindControl("lblDivCode");
            Label lbl = (Label)e.Item.FindControl("lblDivCode");
            lblEdit.Text = "Edit";
            ddl.Items.Clear();
            //ddl.DataBind();
            ddl.Items.Add(lbl.Text);
            ddl.Enabled = false;
            lblDivCode.Visible = true;
            ddl.Visible = false;
            lblCancel.Visible = false;
        }
        else
        {

            objEgEChallan = new EgEChallanBL();
            dt1 = new DataTable();
            objEgEChallan.GRNNumber = Convert.ToInt32(e.CommandArgument.ToString());
            dt1 = objEgEChallan.EChallanViewSubRptPDF();

            string UName = System.Configuration.ConfigurationManager.AppSettings["UName"];
            string PWD = System.Configuration.ConfigurationManager.AppSettings["PWD"];
            string DOM = System.Configuration.ConfigurationManager.AppSettings["DOM"];
            string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"];
            string ReportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];
            string ReportName = "EgEchallanViewRptManualSuccess";

            SSRSreport.ShowCredentialPrompts = false;
            SSRSreport.ServerReport.ReportServerCredentials = new ReportCredentials(UName, PWD, DOM);
            SSRSreport.ProcessingMode = ProcessingMode.Remote;
            SSRSreport.ServerReport.ReportServerUrl = new System.Uri(ReportServerUrl);
            SSRSreport.ServerReport.ReportPath = ReportPath + ReportName;

            ReportParameter[] Param = new ReportParameter[3];
            Param[0] = new ReportParameter("GRN", objEgEChallan.GRNNumber.ToString());

            Param[1] = new ReportParameter("DeptName", dt1.Rows[0]["DeptNameEnglish"].ToString());
            Param[2] = new ReportParameter("MajorHead", dt1.Rows[0]["SchemaName"].ToString().Substring(0, 17));
            SSRSreport.ShowParameterPrompts = false;
            SSRSreport.ServerReport.SetParameters(Param);
            SSRSreport.ServerReport.Refresh();

            //Create as PDF
            byte[] returnValue = null;
            string format = "PDF";
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string extension = "pdf";
            string[] streams = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            returnValue = SSRSreport.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
            Response.Buffer = true;
            Response.Clear();

            Response.ContentType = mimeType;

            Response.AddHeader("content-disposition", "attachment; filename=EgEChallanViewManualSuccess.pdf");

            Response.BinaryWrite(returnValue);
            Response.Flush();
            Response.End();
        }
    }
    protected void rptrManualSuccess_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
         e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlDivCode");
            Label lbl = (Label)e.Item.FindControl("lblDivCode");
            ddl.Items.Add(lbl.Text);
        }
        if (rptrManualSuccess.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                trrpt.Visible = false;
                //Label lblDefaultMessage = (Label)e.Item.FindControl("lblDefaultMessage");
                lblDefaultMessage.Visible = true;
            }
        }
        else
        {
            trrpt.Visible = true;
            lblDefaultMessage.Visible = false;
        }
    }
    protected void btnPdfgenerate_Click(object sender, EventArgs e)
    {
        tr1.Visible = true;
        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ReportParameter[] param = new ReportParameter[6];
            string[] revdateFrom, revdateTo;
            revdateFrom = txtFromDate.Text.Trim().Split('/');
            param[0] = new ReportParameter("FromDate", (revdateFrom[2] + "/" + revdateFrom[1] + "/" + revdateFrom[0]));
            revdateTo = txtToDate.Text.Trim().Split('/');
            param[1] = new ReportParameter("ToDate", (revdateTo[2] + "/" + revdateTo[1] + "/" + revdateTo[0]));
            param[2] = new ReportParameter("MajorHead", (txtmajorHead.Text.Trim() == "" ? null : txtmajorHead.Text.Trim()));
            param[3] = new ReportParameter("Type", rbtnList.SelectedValue);
            param[4] = new ReportParameter("OfficeCode", divcode.SelectedValue.Split('|').GetValue(0).ToString());
            param[5] = new ReportParameter("UserId", Session["UserId"].ToString());
            SSRS objssrs = new SSRS();
            objssrs.LoadSSRS(rptManualSuccessDivisionWiserpt, "EgManualSuccessdivisionWiserpt", param);

        }
        //create PDF
        //if (Response.IsClientConnected) { Response.Flush(); }
        byte[] returnValue = null;
        string format = "PDF";
        string deviceinfo = "";
        string mimeType = "";
        string encoding = "";
        string extension = "pdf";
        string[] streams = null;
        Microsoft.Reporting.WebForms.Warning[] warnings = null;

        returnValue = rptManualSuccessDivisionWiserpt.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        Response.Buffer = true;
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=EgManualSuccessdivisionWiserpt.pdf");

        Response.BinaryWrite(returnValue);
        Response.Flush();
        Response.End();

    }
}