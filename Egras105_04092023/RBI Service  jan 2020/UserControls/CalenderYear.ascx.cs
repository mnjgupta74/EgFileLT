using System;
using System.Web.UI.WebControls;

public partial class UserControls_CalenderYear : System.Web.UI.UserControl
{
    public bool Enabled
    {
        get
        {
            return ddlCalenderYear.Enabled;
        }
        set
        {
            ddlCalenderYear.Enabled = value;
        }
    }

    public bool Visible
    {
        get
        {
            return ddlCalenderYear.Visible;
        }
        set
        {
            ddlCalenderYear.Visible = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return ddlCalenderYear.SelectedValue;
        }
        set
        {
            ddlCalenderYear.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return ddlCalenderYear.SelectedIndex;
        }
        set
        {
            ddlCalenderYear.SelectedIndex = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void FinYearDropdown()
    {
        ddlCalenderYear.Items.Clear();

        string ddlValue;
        string ddlText;
        int count = DateTime.Now.Year;
        for (int i = count; i > 2011; i--)
        {
            
                ddlText = i.ToString();
                ddlValue = i.ToString();
                addListItem(ddlText, ddlValue);
        }
    }
    private void addListItem(string ddlText, string ddlValue) // Function to add ListItems to Dropdown
    {
        ListItem Item = new ListItem();
        Item.Text = ddlText.Trim();
        Item.Value = ddlValue.Trim();
        ddlCalenderYear.Items.Add(Item);
    }
}