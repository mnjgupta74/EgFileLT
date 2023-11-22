using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_FinancialYearDropDown : System.Web.UI.UserControl
{
    #region Public Properties for FinYearDropdown

    string finYear;
    int _FinYeartype, _Count;

    public bool Enabled
    {
        get
        {
            return ddlYear.Enabled;
        }
        set
        {
            ddlYear.Enabled = value;
        }
    }

    public bool Visible
    {
        get
        {
            return ddlYear.Visible;
        }
        set
        {
            ddlYear.Visible = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return ddlYear.SelectedValue;
        }
        set
        {
            ddlYear.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return ddlYear.SelectedIndex;
        }
        set
        {
            ddlYear.SelectedIndex = value;
        }
    }

    public int FinyearSelectType
    {
        get
        {
            return _FinYeartype;
        }
        set
        {
            _FinYeartype = value;
        }
    }

    public int count
    {
        get
        {
            return _Count;
        }
        set
        {
            _Count = value;
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FinYearDropdown();
            if (FinyearSelectType == 2)
            {
                ddlYear.SelectedValue = (DateTime.Now.Month > 3) ? ((DateTime.Now.Year)).ToString().Substring(2, 2) + ((DateTime.Now.Year + 1)).ToString().Substring(2, 2) :
                                ((DateTime.Now.Year - 1)).ToString().Substring(2, 2) + ((DateTime.Now.Year)).ToString().Substring(2, 2);
            }
        }
    }

    public void FinYearDropdown()
    {
        string ddlValue;
        string ddlText;
        if (FinyearSelectType != 0) 
        {
            FinyearSelectType = FinyearSelectType;
        }
        else 
        {
            FinyearSelectType = 0;
        }

        if (count != 0) 
        {
            count = count;
        }
        else 
        {
            count = 2;
        }
        for (int i = 0; i < count; i++)
        {
            if (FinyearSelectType == 0) 
            {
                ddlText = (DateTime.Now.Month > 3) ?
                           ((DateTime.Now.Year) - i).ToString() :
                           ((DateTime.Now.Year - 1) - i).ToString();
                ddlValue = ddlText;
            }
            else if (FinyearSelectType == 1)
            {
                ddlText = (DateTime.Now.Month > 3) ?
                            ((DateTime.Now.Year) - i).ToString() + ((DateTime.Now.Year + 1) - i).ToString().Substring(2, 2) :
                            ((DateTime.Now.Year - 1) - i).ToString() + ((DateTime.Now.Year) - i).ToString().Substring(2, 2);
                ddlValue = ddlText;
            }
            else if (FinyearSelectType == 2)
            {
                ddlText = ((DateTime.Now.Year) - i).ToString() + "-" + ((DateTime.Now.Year + 1) - i).ToString().Substring(2, 2);
                ddlValue = ddlText.Substring(2, 2) + ddlText.Substring(5, 2);
            }
            else
            {
                ddlText = (DateTime.Now.Month > 3) ?
                           (((DateTime.Now.Year) - i).ToString() + "-" + ((DateTime.Now.Year + 1) - i).ToString()) :
                           (((DateTime.Now.Year - 1) - i).ToString() + "-" + ((DateTime.Now.Year) - i).ToString());
                ddlValue = ddlText;
            }
            addListItem(ddlText, ddlValue);
        }
    }
    private void addListItem(string ddlText, string ddlValue) // Function to add ListItems to Dropdown
    {
        ListItem Item = new ListItem();
        Item.Text = ddlText.Trim();
        Item.Value = ddlValue.Trim();
        ddlYear.Items.Add(Item);
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}