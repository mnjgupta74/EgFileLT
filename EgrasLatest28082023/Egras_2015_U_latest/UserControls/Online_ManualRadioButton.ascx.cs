using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_Online_ManualRadioButton : System.Web.UI.UserControl
{
    public string SelectedValue { get { return rdBtnList.SelectedValue; } }
    public event EventHandler rdBtnList_onSelectedIndexChange;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void rdBtnList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rdBtnList_onSelectedIndexChange != null)
            this.rdBtnList_onSelectedIndexChange(this, e);       
    }
}