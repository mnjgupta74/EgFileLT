using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EgBL;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
public partial class WebPages_EgAddExtraDetail : System.Web.UI.Page
{
    EgEChallanBL objEChallan;
    TextBox tb;

    DataTable dt;
    /// <summary>
    /// On Page Load, read  dynamic table on add button and check QueryString count is less than 0 than logout this page and 
    /// if QueryString count is greaterthan 1 than show only details on grn in tabuler format else show buttons for create 
    /// new table and show previous details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] == null) || Session["UserId"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }
        if (Request.QueryString.Count > 0)
        {
            string id;
            EncryptDecryptionBL objenc = new EncryptDecryptionBL();
            var idnew = objenc.Decrypt(Request.QueryString[0].ToString());

            //for (int i = 0; i < id.Length; i++)
            //{
            //    c = inSb[i];
            //    c = (char)(c ^ 89); /// remember to use the same XORkey value you used in javascript
            //    outSb.Append(c);
            //}
            string[] idvalue = idnew.ToString().Split('-');
            id = idvalue[0].ToString();
            if (idvalue.Length == 2)
            {

                if (idvalue[1].ToString() == "View")
                {
                    divopenWindow1.Visible = false;
                    divopenWindow2.Visible = false;
                    divopenWindow3.Visible = false;
                    pnlpopup.Visible = false;
                    divShowExtra.Visible = true;
                    BindExtraDetails(Convert.ToInt32(id));

                }
                else
                {
                    divamount.Visible = true;
                    lbltotalAmount.Text =Convert.ToDecimal(idvalue[1]).ToString("00");
                }
            }
            objEChallan = new EgEChallanBL();
            if (id != "" && Request.QueryString.Count == 1)
            {
                objEChallan.GRNNumber = Convert.ToInt32(id);
                dt = new DataTable();
                dt = objEChallan.GetExtraDetails();
                if (dt.Rows.Count > 0)
                {
                    btnprevious.Visible = true;
                }
                else
                {
                    btnprevious.Visible = false;
                }
            }
            else
            {
                btnprevious.Visible = false;
            }
            //if (Request.QueryString.Count > 1)
            //{
            //    divopenWindow1.Visible = false;
            //    divopenWindow2.Visible = false;
            //    divopenWindow3.Visible = false;
            //    pnlpopup.Visible = false;
            //    divShowExtra.Visible = true;
            //    BindExtraDetails(Convert.ToInt32(id));
            //}
        }
        else
        {
            Response.Redirect("~\\logout.aspx");
        }
        if (txtrow.Text != "" && txtColumn.Text != "" && ViewState["temp"] != null)
        {
            CreateDetailDynamic(Convert.ToInt32(txtrow.Text), Convert.ToInt32(txtColumn.Text), null);
        }


    }
    /// <summary>
    /// Create new Dynamic Table and Check  txtrow text value is not greater than 50 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreateTable_Click(object sender, EventArgs e)
    {
        CreateDetailDynamic(Convert.ToInt32(txtrow.Text), Convert.ToInt32(txtColumn.Text), null);
        btnaddmore.Visible = true;
        divopenWindow3.Visible = true;
        //txttotalAmount.Text = "";
        DisableValue();
        btnreset.Visible = false;
        btnnew.Text = "Reset";
        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " updateValue('" + null + "')", true);
    }
    /// <summary>
    /// this function create new dynamic table 
    /// </summary>
    /// <param name="rows">textrow value</param>
    /// <param name="columns">textcolumn value</param>
    /// <param name="dt">pass dt if it has row else null </param>
    public void CreateDetailDynamic(int rows, int columns, DataTable dt)
    {
        int rowCount = Convert.ToInt32(rows);
        int ColumnCount = Convert.ToInt32(columns);
        pnlHead2.Visible = true;
        pnlHead2.Controls.Add(tblDynamic);
        tblDynamic.Rows.Clear();
        HtmlTableRow row;
        HtmlTableCell Cell;
        for (int i = 0; i < rowCount; i++)
        {
            row = new HtmlTableRow();
            for (int j = 0; j < ColumnCount; j++)
            {
                Cell = new HtmlTableCell();
                tb = new TextBox();
                tb.ID = "TextBox_" + Convert.ToString(i) + Convert.ToString(j);
                // tb.Text = "";


                tb.Style.Add("text-align", "Left");
                tb.Width = 150;

                RegularExpressionValidator validator = new RegularExpressionValidator();

                validator.ControlToValidate = tb.ID;


                if (j == columns - 1)
                {
                    decimal moneytotal = Convert.ToDecimal("0");
                    tb.Text = string.Format("{0:00}", moneytotal);
                    validator.ValidationExpression = "([0-9])*";
                    tb.ToolTip = "Only Numeric Value allow in this Column";
                    tb.Attributes.Add("onkeypress", "Javascript:return DecimalNumber(event);");
                    tb.Attributes.Add("onPaste", "return false");
                    tb.Attributes.Add("onFocus", "javascript:ClearValue(this);");
                    tb.Attributes.Add("onBlur", "javascript:updateValue('" + tb.ID + "');");
                    tb.Attributes.Add("onChange", "javascript:updateValue('" + tb.ID + "');");

                    tb.MaxLength = 12;

                }
                else
                {
                    tb.MaxLength = 20;
                    validator.ValidationExpression = "([a-z]|[A-Z]|[0-9]|[.]|[_()]|[ ]|[/])*";
                    tb.ToolTip = "Special Character are not Allow Except Dot ,(),Space ,UnderScore,Forward Slash.!";
                }
                validator.ErrorMessage = "*";
                validator.ValidationGroup = "a";
                // Rfv.ErrorMessage = "*";

                Cell.Controls.Add(validator);

                Cell.Height = "30px";
                Cell.Controls.Add(tb);
                row.Cells.Add(Cell);
                if (dt != null)
                {
                    string[] list = dt.Rows[0]["Details"].ToString().Split('^');
                    string[] ListValue = list[i].Split('#');
                    if (ListValue[j].ToString() != "0")
                    {
                        tb.Text = ListValue[j].ToString();
                    }
                }
            }
            tblDynamic.Rows.Add(row);
            ViewState["temp"] = "abc";
        }
    }
    /// <summary>
    /// get dynamic text box  value and pass value to Challan page 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnaddmore_Click(object sender, EventArgs e)
    {

        string Details = SubmitExtraDetails();
        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " callParentMethod('" + Details + "')", true);



    }
    /// <summary>
    /// This Function use for Get All Text  Box value  click on Add Button 
    /// </summary>
    /// <returns></returns>
    public string SubmitExtraDetails()
    {
        List<EgEChallanBL> listReocord = new List<EgEChallanBL>();
        string abc = "";
        if (txtrow.Text != "" && txtColumn.Text != "")
        {
            for (int i = 0; i < Convert.ToInt32(txtrow.Text); i++)
            {
                for (int j = 0; j < Convert.ToInt32(txtColumn.Text); j++)
                {

                    if (Convert.ToString(((TextBox)tblDynamic.Rows[i].Cells[j].FindControl("TextBox_" + Convert.ToString(i) + Convert.ToString(j))).Text) != "")
                    {
                        abc = abc + Convert.ToString(((TextBox)tblDynamic.Rows[i].Cells[j].FindControl("TextBox_" + Convert.ToString(i) + Convert.ToString(j))).Text) + "#";
                    }
                    else
                    {
                        abc = abc + "0" + "#";
                    }
                }

                abc = abc + "^";

            }
        }
        return abc;

    }
    /// <summary>
    /// This Function is use for Display Details on GRN Number.
    /// </summary>
    /// <param name="GRN">GRN number</param>
    public void BindExtraDetails(int GRN)
    {
        divShowExtra.Visible = true;
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        EgEChallanBL objEgEChallan = new EgEChallanBL();
        objEgEChallan.GRNNumber = Convert.ToInt32(GRN);
        dt = objEgEChallan.GetExtraDetails();
        if (dt.Rows.Count > 0)
        {
            divtblbankinfo.Visible = true;
            //string GEN = "000000000" + Convert.ToString(dt.Rows[0]["GRN"]);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
            //lblgrn.Text = "GRN :- " + GEN.Substring(GEN.Length - 10, 10);// Commented By Sandeep on 02/03/2017 for removing Leading zeros
            lblgrn.Text = "GRN :- " + Convert.ToString(dt.Rows[0]["GRN"]);
            //Bitmap barcode = CreateBarCode(GEN.Substring(GEN.Length - 10, 10));// Commented By Sandeep on 02/03/2017 for removing Leading zeros
            Bitmap barcode = CreateBarCode(Convert.ToString(dt.Rows[0]["GRN"]));
            barcode.Save(Server.MapPath("../Image/barcode.Gif"), ImageFormat.Gif);
            Image2.ImageUrl = "~/Image/barcode.Gif";
            barcode.Dispose();
            if (dt.Rows[0]["CIN"].ToString().Trim() != "" && dt.Rows[0]["Ref"].ToString().Trim() != "")
            {
                LabelCIN.Visible = true;
                LabelRef.Visible = true;
                LabelCIN.Text = "CIN :- " + dt.Rows[0]["CIN"].ToString();
                LabelRef.Text = "Reference :- " + dt.Rows[0]["REf"].ToString();
            }
            string[] list = dt.Rows[0]["Details"].ToString().Split('^');
            if (dt.Rows.Count > 0)
            {
                sb.Append("<table width='400' id='module' border='1' cellpadding='1' cellspacing='0' style='background-color:transparent;border-color:Black;color:Black;'");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        sb.Append("<tr>");
                        string[] ListValue2 = list[i].Split('#');
                        for (int j = 0; j < ListValue2.Length - 1; j++)
                        {
                            sb.Append("<td style='font-size: 12pt;bold;padding-left:3px;text-align:left'>&nbsp;");
                            if (ListValue2[j].ToString() != "0")
                            {
                                sb.Append(ListValue2[j].ToString());
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("&nbsp;</td>");
                        }
                        sb.Append("</tr>");


                    }
                }
                sb.Append("</table>");
                literal1.Text = sb.ToString();
                literal1.Visible = true;
                //ImgPrint2.Visible = true;

            }
        }

    }
    /// <summary>
    /// This function is use for show old previous details on GRN number and Bind Details With Dynamic Table
    /// </summary>
    /// <param name="GRN"> grn number</param>
    public void UpdateTable(int GRN)
    {
        DataTable dtExtra = new DataTable();
        EgEChallanBL objEgEChallan = new EgEChallanBL();
        objEgEChallan.GRNNumber = Convert.ToInt32(GRN);
        dtExtra = objEgEChallan.GetExtraDetails();
        if (dtExtra.Rows.Count > 0)
        {
            string[] list = dtExtra.Rows[0]["Details"].ToString().Split('^');
            string[] ListValue = list[0].Split('#');
            txtrow.Text = Convert.ToString(list.Length - 1);
            txtColumn.Text = Convert.ToString(ListValue.Length - 1);
            CreateDetailDynamic(Convert.ToInt32(list.Length - 1), Convert.ToInt32(ListValue.Length - 1), dtExtra);
        }
        btnaddmore.Visible = true;
        btnreset.Visible = true;
        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", " updateValue('" + null + "')", true);
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        ResetControl();
    }
    /// <summary>
    /// Reset row and Column Textbox and Dynamic Table 
    /// </summary>
    public void ResetControl()
    {
        tblDynamic.Rows.Clear();
        txtrow.Text = "";
        txtColumn.Text = "";
        btnaddmore.Visible = false;
    }
    /// <summary>
    /// This Button click  show Previous Details on GRN number and hide  row/column , text box for create new Dynamic Table 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnprevious_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            string id = Request.QueryString[0].ToString();
            EncryptDecryptionBL objenc = new EncryptDecryptionBL();
            var idnew = objenc.Decrypt(id);

            //for (int i = 0; i < id.Length; i++)
            //{
            //    c = inSb[i];
            //    c = (char)(c ^ 89); /// remember to use the same XORkey value you used in javascript
            //    outSb.Append(c);
            //}
            string[] idvalue = idnew.ToString().Split('-');
            id = idvalue[0].ToString();
            DisableValue();
            UpdateTable(Convert.ToInt32(id));
        }
    }
    /// <summary>
    /// This Function use for Disable control value when we Select Same As previous Value
    /// </summary>
    public void DisableValue()
    {
        divopenWindow2.Visible = true;
        txtrow.Enabled = false;
        txtColumn.Enabled = false;
        btnCreateTable.Enabled = false;
        btnreset.Enabled = false;
        divopenWindow3.Visible = true;
        divShowExtra.Visible = false;

        txttotalAmount.Text = "";
    }
    /// <summary>
    /// This Function use for Enable control value when create new Table
    /// </summary>
    public void EnableValue()
    {
        divopenWindow2.Visible = true;
        txtrow.Enabled = true;
        txtColumn.Enabled = true;
        btnCreateTable.Enabled = true;
        btnreset.Enabled = true;
        divopenWindow3.Visible = false;
        divShowExtra.Visible = false;

    }
    /// <summary>
    /// This Button Click Show row/column , text box for create new Dynamic Table 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnnew_Click(object sender, EventArgs e)
    {
        divopenWindow2.Visible = true;
        EnableValue();
        ResetControl();
    }
    private void Message(string str)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "PopupScript", "alert('" + str + "');", true);
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
        oGraphics.Dispose();
        return oBitmap;
    }


}
