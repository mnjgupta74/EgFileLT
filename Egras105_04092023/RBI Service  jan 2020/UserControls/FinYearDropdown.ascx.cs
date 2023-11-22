using System;
using System.Web.UI.WebControls;

public partial class UserControls_FinYearDropdown : System.Web.UI.UserControl
{
    #region Public Properties for FinYearDropdown

    int _FinYeartype, _Count;

    public bool Enabled
    {
        get
        {
            return ddlFinyear.Enabled;
        }
        set
        {
            ddlFinyear.Enabled = value;
        }
    }

    public bool Visible
    {
        get
        {
            return ddlFinyear.Enabled;
        }
        set
        {
            ddlFinyear.Enabled = value;
        }
    }

    public string Text
    {
        get
        {
            return ddlFinyear.Text;
        }
        set
        {
            ddlFinyear.Text = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return ddlFinyear.SelectedValue;
        }
        set
        {
            ddlFinyear.SelectedValue = value;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return ddlFinyear.SelectedIndex;
        }
        set
        {
            ddlFinyear.SelectedIndex = value;
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
                            ((DateTime.Now.Year) - i).ToString().Substring(2, 2) + ((DateTime.Now.Year + 1) - i).ToString().Substring(2, 2) :
                            ((DateTime.Now.Year - 1) - i).ToString().Substring(2, 2) + ((DateTime.Now.Year) - i).ToString().Substring(2, 2);
            }
            addListItem(ddlValue);
        }
    }


    private void addListItem(string ddlValue) // Function to add ListItems to Dropdown
    {
        ListItem Item = new ListItem();
        Item.Text = ddlValue.ToString();
        Item.Value = ddlValue.ToString();
        ddlFinyear.Items.Add(Item);
    }

}
