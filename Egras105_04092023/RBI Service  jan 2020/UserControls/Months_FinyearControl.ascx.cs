using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Months_FinyearControl : System.Web.UI.UserControl
{
    #region Public Properties for FinYearDropdown

    string finYear;
    DateTime currentdate;
    int _FinYeartype, _Count;

    public bool Enabled
    {
        get
        {
            return ddlMonth.Enabled;
        }
        set
        {
            ddlMonth.Enabled = value;
        }
    }

    public bool Visible
    {
        get
        {
            return ddlMonth.Enabled;
        }
        set
        {
            ddlMonth.Enabled = value;
        }
    }

    public string Text
    {
        get
        {
            return ddlMonth.Text;
        }
        set
        {
            ddlMonth.Text = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return ddlMonth.SelectedValue;
        }
        set
        {
            ddlMonth.SelectedValue = value;
        }
    }

    public string SelectedDate
    {
        get
        {
            return ddlMonth.SelectedValue + "/01/" + (Convert.ToInt32(ddlMonth.SelectedValue) >= 4 ? ddlYear.SelectedItem.ToString().Substring(0, 4) : (ddlYear.SelectedItem.ToString().Substring(0, 2) + ddlYear.SelectedItem.ToString().Substring(5, 2)));
        }
    }

    public int SelectedIndex
    {
        get
        {
            return ddlMonth.SelectedIndex;
        }
        set
        {
            ddlMonth.SelectedIndex = value;
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
            FinyearSelectType = 3;
            count = 4;
            currentdate = DateTime.Now;
            finYear = (currentdate.Month >= 4 ? currentdate.Year + 1 : currentdate.Year).ToString();

            FinYearDropdown();
        }
    }
    public void FinYearDropdown() // for Single value without Argument
    {
        string ddlValue;

        // type=0 --> 2014(Finyear) , type=1 --> 1415(TokenFinYear)

        if (FinyearSelectType != 0) // with argument value
        {
            FinyearSelectType = FinyearSelectType;
        }
        else // By Dafault Value
        {
            FinyearSelectType = 0;
        }

        if (count != 0) // with argument value
        {
            count = count;
        }
        else // By Dafault Value
        {
            count = 2;
        }

        for (int i = 0; i < count; i++)
        {
            if (FinyearSelectType == 0) // for Finyear
            {
                ddlValue = (DateTime.Now.Month > 3) ?
                           ((DateTime.Now.Year) - i).ToString() :
                           ((DateTime.Now.Year - 1) - i).ToString();
            }
            else // For TokenFinyear
            {
                ddlValue = (DateTime.Now.Month > 3) ?
                            ((DateTime.Now.Year) - i).ToString() +"-"+ ((DateTime.Now.Year + 1) - i).ToString().Substring(2, 2) :
                            ((DateTime.Now.Year - 1) - i).ToString() + "-" + ((DateTime.Now.Year) - i).ToString().Substring(2, 2);
            }
            addListItem(ddlValue);
        }
    }


    private void addListItem(string ddlValue) // Function to add ListItems to Dropdown
    {
        ListItem Item = new ListItem();
        Item.Text = ddlValue.ToString();
        Item.Value = ddlValue.ToString();
        ddlYear.Items.Add(Item);
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedItem.ToString() == finYear)
        {
            foreach ( ListItem item in ddlMonth.Items )
            {
                if (Convert.ToInt32(item.Value) > DateTime.Now.Month)
                {
                    item.Attributes.Add( "disabled", "disabled" );
                }
            }
        }
    }
}