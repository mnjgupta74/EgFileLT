using EgBL;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class NodalDetails : System.Web.UI.Page
{
    EgLoginBL objLogin = new EgLoginBL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            inpHide.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnsubmit').click();return false;}} else {return true}; ");
            EgUserProfileBL objUserProfileBL = new EgUserProfileBL();
            objUserProfileBL.PopulateDepartmentList(DropDownList1);
            // Get Department List 
            //if (HttpContext.Current.Cache["DepartmentList"] as DataTable == null)
            //{
            //    Cache.Insert("DepartmentList", objLogin.PopulateDepartmentList(), null, DateTime.Now.AddHours(8), Cache.NoSlidingExpiration);
            //}
            //else
            //    DropDownList1.DataSource = HttpContext.Current.Cache["DepartmentList"];
            //DropDownList1.DataTextField = "deptnameEnglish";
            //DropDownList1.DataValueField = "deptcode";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string imageValue = String.Empty;
            imageValue = inpHide.Text.Trim();
            string groupList = (string)(Session["captcha"]);
            if (HttpContext.Current.Session["captcha"].ToString().ToLower().Trim() != "" && imageValue.Trim() != "" && imageValue.Trim() != "0")
            {
                if (HttpContext.Current.Session["captcha"].ToString().Trim() == imageValue.ToString().Trim())
                {
                    //  DataTable dt = HttpContext.Current.Cache["Nodal"] as DataTable; 
                    DataTable dt = new DataTable();
                    dt = objLogin.GetNodalOfficerDetails();
                    var NodalDetails = dt.AsEnumerable().Where(t => t.Field<Int32>("DeptCode") == Convert.ToInt32(DropDownList1.SelectedValue.ToUpper()));
                    //if (str.IndexOf(txtEmpName.Text, StringComparison.OrdinalIgnoreCase) >= 0)

                    if (NodalDetails.Count() != 0)
                    {
                        if (NodalDetails.First() != null)
                        {
                            dt = NodalDetails.CopyToDataTable();
                            DataList1.Visible = true;
                            lblMsg.Visible = false;
                            field1.Visible = true;
                            lbldeptName.Text = DropDownList1.SelectedItem.Text;
                            DataList1.DataSource = dt;
                            DataList1.DataBind();
                        }
                        else
                        {
                            DataList1.Visible = false;
                            lblMsg.Visible = true;
                            field1.Visible = false;
                        }
                    }
                    else
                    {
                        DataList1.Visible = false;
                        lblMsg.Visible = true;
                        field1.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('Wrong Captcha Value !');", true);
                }
                inpHide.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            // HttpContext.Current.Session["captcha"] = inpHide.Text.Trim();
            // Do something with e, please.
        }
    }

    //public void captchagenerate()
    //{
    //    Bitmap objBitmap = new Bitmap(130, 80);
    //    Graphics objGraphics = Graphics.FromImage(objBitmap);
    //    objGraphics.Clear(Color.White);
    //    Random objRandom = new Random();
    //    objGraphics.DrawLine(Pens.Black, objRandom.Next(0, 50), objRandom.Next(10, 30), objRandom.Next(0, 200), objRandom.Next(0, 50));
    //    objGraphics.DrawRectangle(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(0, 20), objRandom.Next(50, 80), objRandom.Next(0, 20));
    //    objGraphics.DrawLine(Pens.Blue, objRandom.Next(0, 20), objRandom.Next(10, 50), objRandom.Next(100, 200), objRandom.Next(0, 80));
    //    Brush objBrush =
    //        default(Brush);
    //    //create background style  
    //    HatchStyle[] aHatchStyles = new HatchStyle[]
    //    {
    //            HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal, HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
    //                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross, HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
    //                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid, HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal
    //    };
    //    //create rectangular area  
    //    RectangleF oRectangleF = new RectangleF(0, 0, 300, 300);
    //    objBrush = new HatchBrush(aHatchStyles[objRandom.Next(aHatchStyles.Length - 3)], Color.FromArgb((objRandom.Next(100, 255)), (objRandom.Next(100, 255)), (objRandom.Next(100, 255))), Color.White);
    //    objGraphics.FillRectangle(objBrush, oRectangleF);
    //    //Generate the image for captcha  
    //    string captchaText = string.Format("{0:X}", objRandom.Next(1000000, 9999999));
    //    //add the captcha value in session  
    //    Session["CaptchaVerify"] = captchaText.ToLower();
    //    Font objFont = new Font("Courier New", 15, FontStyle.Bold);
    //    //Draw the image for captcha  
    //    objGraphics.DrawString(captchaText, objFont, Brushes.Black, 20, 20);
    //    objBitmap.Save(Response.OutputStream, ImageFormat.Gif);
    //}
}
