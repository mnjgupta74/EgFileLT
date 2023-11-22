using System;
using EgBL;
using System.IO;

public partial class WebPages_FrmCTDSoftCopy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserID"] == null) || Session["userid"].ToString() == "")
        {
            Response.Redirect("~\\LoginAgain.aspx");
        }

        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["UserId"]) == 52)
            {
                Departmentlist();
                ddlDepartment.SelectedValue = "18";
                ddlDepartment.Enabled = false;
                trmain.Visible = true;
            }
            else
                trmain.Visible = false;
        }
    }
    public void Departmentlist()
    {
        EgDeptAmountRptBL objEgDeptAmountRptBl = new EgDeptAmountRptBL();
        objEgDeptAmountRptBl.UserId = Convert.ToInt32(Session["UserId"]);
        objEgDeptAmountRptBl.PopulateDepartmentList(ddlDepartment);
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        EgCTD obj = new EgCTD();

        //Fetch Data on Giving Dates
        string[] fromdate = txtfromdate.Text.Trim().Replace("-", "/").Split('/');
        obj.FromDate = Convert.ToDateTime(fromdate[2].ToString() + "/" + fromdate[1].ToString() + "/" + fromdate[0].ToString());

        string[] todate = txttodate.Text.Trim().Replace("-", "/").Split('/');
        obj.Todate = Convert.ToDateTime(todate[2].ToString() + "/" + todate[1].ToString() + "/" + todate[0].ToString());

        TimeSpan datediff = obj.Todate - obj.FromDate;

        if (datediff.Days  > 90)
        {
            lblerr.Text = "FromDate and ToDate Not More Than 90 Days";
            return;
        }
        else if (datediff.Days < 0)
        {
            lblerr.Text = "FromDate should be less than ToDate";
            return;
        }
        else
        {
            lblerr.Text = "";
        }
        obj.Userid = Convert.ToInt32(Session["userid"].ToString());
        if (Convert.ToInt32(Session["UserId"]) == 52)
        {
            trmain.Visible = true;
            obj.DeptCode = Convert.ToInt32(ddlDepartment.SelectedValue);
        }
        else
        {
            trmain.Visible = false;
            obj.DeptCode = 0;
        }
        string Result = obj.GetCTDData();
        //////////////////////////////


        //Write Text File 
        string filePath = Server.MapPath("~/ManualChallan/") + "CTDSoftCopy.txt";
        StreamWriter w;
        w = File.CreateText(filePath);
        w.WriteLine(Result.Replace("^", "\r\n"));
        //w.WriteLine(Result.ToString());
        w.Flush();
        w.Close();

        System.IO.FileStream fs = null;
        fs = System.IO.File.Open(Server.MapPath("~/ManualChallan/" + "CTDSoftCopy.txt"), System.IO.FileMode.Open);
        byte[] btFile = new byte[fs.Length];
        fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
        fs.Close();
        Response.AddHeader("Content-disposition", "attachment; filename=" + "CTDSoftCopy.txt");
        Response.ContentType = "application/octet-stream";
        Response.BinaryWrite(btFile);
        Response.End();
        //////////////////////////////
    }
}