using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public delegate void ItemCommandHandler(string ItemName);

public partial class UserControls_CustomAlphabetRepeater : System.Web.UI.UserControl
{
    
    public event ItemCommandHandler ItemCommand;

    public bool Enabled
    {
        get
        {
            return repAlphabet.Visible;
        }
        set
        {
            repAlphabet.Visible = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreateAlphabetDataTable();
            LinkButton lnkbtn = (LinkButton)repAlphabet.Items[0].FindControl("lbtnSelectAlphabet");
            lnkbtn.CssClass = "LinkButtonActiveTab";
        }
    }

    
    protected void repAlphabet_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (ItemCommand != null)
        {
            for (int i = 0; i < repAlphabet.Items.Count; i++)
            {
                LinkButton lnkbtn = (LinkButton)repAlphabet.Items[i].FindControl("lbtnSelectAlphabet");
                lnkbtn.CssClass = "LinkButtonTab";
            }
            LinkButton lnk = (LinkButton)e.Item.FindControl("lbtnSelectAlphabet");
            lnk.CssClass = "LinkButtonActiveTab";
            ItemCommand(lnk.Text);
        }
    }

    private void CreateAlphabetDataTable()
    {
        DataTable dtHospice = new DataTable("alphabets");
        dtHospice.Columns.Add("Char");
        DataRow dr1 = dtHospice.NewRow();
        dr1[0] = "A";
        DataRow dr2 = dtHospice.NewRow();
        dtHospice.Rows.Add(dr1);
        dr2[0] = "B";
        dtHospice.Rows.Add(dr2);
        DataRow dr3 = dtHospice.NewRow();
        dr3[0] = "C";
        dtHospice.Rows.Add(dr3);
        DataRow dr4 = dtHospice.NewRow();
        dr4[0] = "D";
        dtHospice.Rows.Add(dr4);
        DataRow dr5 = dtHospice.NewRow();
        dr5[0] = "E";
        dtHospice.Rows.Add(dr5);
        DataRow dr6 = dtHospice.NewRow();
        dr6[0] = "F";
        dtHospice.Rows.Add(dr6);
        DataRow dr7 = dtHospice.NewRow();
        dr7[0] = "G";
        dtHospice.Rows.Add(dr7);
        DataRow dr8 = dtHospice.NewRow();
        dr8[0] = "H";
        dtHospice.Rows.Add(dr8);
        DataRow dr9 = dtHospice.NewRow();
        dr9[0] = "I";
        dtHospice.Rows.Add(dr9);
        DataRow dr10 = dtHospice.NewRow();
        dr10[0] = "J";
        dtHospice.Rows.Add(dr10);
        DataRow dr11 = dtHospice.NewRow();
        dr11[0] = "K";
        dtHospice.Rows.Add(dr11);
        DataRow dr12 = dtHospice.NewRow();
        dr12[0] = "L";
        dtHospice.Rows.Add(dr12);
        DataRow dr13 = dtHospice.NewRow();
        dr13[0] = "M";
        dtHospice.Rows.Add(dr13);
        DataRow dr14 = dtHospice.NewRow();
        dr14[0] = "N";
        dtHospice.Rows.Add(dr14);
        DataRow dr15 = dtHospice.NewRow();
        dr15[0] = "O";
        dtHospice.Rows.Add(dr15);
        DataRow dr16 = dtHospice.NewRow();
        dr16[0] = "P";
        dtHospice.Rows.Add(dr16);
        DataRow dr17 = dtHospice.NewRow();
        dr17[0] = "Q";
        dtHospice.Rows.Add(dr17);
        DataRow dr18 = dtHospice.NewRow();
        dr18[0] = "R";
        dtHospice.Rows.Add(dr18);
        DataRow dr19 = dtHospice.NewRow();
        dr19[0] = "S";
        dtHospice.Rows.Add(dr19);
        DataRow dr20 = dtHospice.NewRow();
        dr20[0] = "T";
        dtHospice.Rows.Add(dr20);
        DataRow dr21 = dtHospice.NewRow();
        dr21[0] = "U";
        dtHospice.Rows.Add(dr21);
        DataRow dr22 = dtHospice.NewRow();
        dr22[0] = "V";
        dtHospice.Rows.Add(dr22);
        DataRow dr23 = dtHospice.NewRow();
        dr23[0] = "W";
        dtHospice.Rows.Add(dr23);
        DataRow dr24 = dtHospice.NewRow();
        dr24[0] = "X";
        dtHospice.Rows.Add(dr24);
        DataRow dr25 = dtHospice.NewRow();
        dr25[0] = "Y";
        dtHospice.Rows.Add(dr25);
        DataRow dr26 = dtHospice.NewRow();
        dr26[0] = "Z";
        dtHospice.Rows.Add(dr26);
        DataRow dr27 = dtHospice.NewRow();
        dr27[0] = "Other";
        dtHospice.Rows.Add(dr27);
        repAlphabet.DataSource = dtHospice;
        repAlphabet.DataBind();
    }


}
