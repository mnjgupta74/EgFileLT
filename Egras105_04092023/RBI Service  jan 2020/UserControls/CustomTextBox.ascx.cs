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

//public delegate void TextChangedHandler(string Text);
public partial class CustomTextBox : System.Web.UI.UserControl
{
    public event EventHandler TextChanged;

    #region Public Properties for Custom Textbox 

    ///<Summary>
    /// Set and get the text of TextBox
    /// </Summary>
    /// 

    public short _tabIndex;
    public short TabIndex
    {
        get
        {
            return _tabIndex;
        }
        set
        {
            _tabIndex = value;
            
        }
    }        
    public Unit Width
    {
        get
        {
            return txtCustom.Width;
        }
        set
        {
            txtCustom.Width = value;
        }
        
    }
    public bool AutoPostback
    {
        get
        {
            return txtCustom.AutoPostBack;
        }
        set
        {
            txtCustom.AutoPostBack= value;
        }

    }

    public bool CauseValidation
    {
         get
        {
            return txtCustom.CausesValidation;
        }
        set
        {
            txtCustom.Enabled = value;
        }

    }

    public bool Enabled
    {
        get
        {
            return txtCustom.Enabled;
        }
        set
        {
            txtCustom.Enabled = value;
            rngvldtxtCustom.Enabled = value;
            regvldtxtCustom.Enabled = value;
            reqvldtxtCustom.Enabled = value;
            cmpvldtxtCustom.Enabled = value;
        }
    }

    public string Text
    {
        get
        {
            return txtCustom.Text;
        }
        set
        {
            txtCustom.Text = value;
        }
    }

    public string CssClass
    {
        get
        {
            return txtCustom.CssClass;
        }
        set
        {
            txtCustom.CssClass = value;
        }
    }

    public TextBoxMode TextMode
    {
        get
        {
            return txtCustom.TextMode;
        }
        set
        {
            txtCustom.TextMode = value;
        }
    }

    public int MaxLength
    {
        get
        {
            return txtCustom.MaxLength;
        }
        set
        {
            txtCustom.MaxLength = value;
        }
    }

    public bool EnableFilter
    {
        get
        {
            return ftbrtxtCustom.Enabled;
        }
        set
        {
            ftbrtxtCustom.Enabled = value;
        }
    }

    public AjaxControlToolkit.FilterTypes FilterType
    {
        get
        {
            return ftbrtxtCustom.FilterType;
        }
        set
        {
            ftbrtxtCustom.FilterType = value;
        }
    }

    public string ValidChars
    {
        get
        {
            return ftbrtxtCustom.ValidChars;
        }
        set
        {
            ftbrtxtCustom.ValidChars = value;
        }
    }

    public bool ValidateWithRegularExpression
    {
        get
        {
            return regvldtxtCustom.Enabled;
        }
        set 
        {
            regvldtxtCustom.Enabled = value;
        }
    }

    public string RegularExpressionValidationErrorMessage
    {
        get
        {
            return regvldtxtCustom.ErrorMessage;
        }
        set
        {
            regvldtxtCustom.ErrorMessage = value;
        }
    }

    public bool IsRequired
    {
        get
        {
            return reqvldtxtCustom.Enabled;
        }
        set
        {
            reqvldtxtCustom.Enabled = value;
        }
    }

    public string RequiredValidationErrorMessage
    {
        get
        {
            return reqvldtxtCustom.ErrorMessage;
        }
        set
        {
            reqvldtxtCustom.ErrorMessage = value;
        }
    }

    public string RegularExpression
    {
        get
        {
            return regvldtxtCustom.ValidationExpression;
        }
        set
        {
            regvldtxtCustom.ValidationExpression = value;
        }
    }

    public bool RangeValidationRequired
    {
        get
        {
            return rngvldtxtCustom.Enabled;
        }
        set
        {
            rngvldtxtCustom.Enabled = value;
        }
    }

    public string RangeValidationErrorMessage
    {
        get
        {
            return rngvldtxtCustom.ErrorMessage;
        }
        set
        {
            rngvldtxtCustom.ErrorMessage = value;
        }
    }

    public ValidationDataType RangeValidationDataType
    {

        get
        {
            return rngvldtxtCustom.Type;
        }
        set
        {
            rngvldtxtCustom.Type = value;
        }
    }

    public string MinimumValue
    {
        get
        {
            return rngvldtxtCustom.MinimumValue;
        }
        set
        {
            rngvldtxtCustom.MinimumValue = value;
        }
    }

    public string MaximumValue
    {
        get
        {
            return rngvldtxtCustom.MaximumValue;
        }
        set
        {
            rngvldtxtCustom.MaximumValue = value;
        }
    }

    public bool CompareValidationRequired
    {
        get
        {
            return cmpvldtxtCustom.Enabled;
        }
        set
        {
            cmpvldtxtCustom.Enabled = value;
        }
    }

    public string CompareValidationErrorMessage
    {
        get
        {
            return cmpvldtxtCustom.ErrorMessage;
        }
        set
        {
            cmpvldtxtCustom.ErrorMessage = value;
        }
    }

    public ValidationDataType CompareValidationDataType
    {

        get
        {
            return cmpvldtxtCustom.Type;
        }
        set
        {
            cmpvldtxtCustom.Type = value;
        }
    }

    public string ValueToCompare
    {
        get
        {
            return cmpvldtxtCustom.ValueToCompare;
        }
        set
        {
            cmpvldtxtCustom.ValueToCompare = value;
        }
    }

    public ValidationCompareOperator CompareOperator
    {
        get
        {
            return cmpvldtxtCustom.Operator;
        }
        set
        {
            cmpvldtxtCustom.Operator=value;
        }
    }

    public string ControlToCompare
    {
        get
        {
            return cmpvldtxtCustom.ControlToCompare;
        }
        set
        {
            cmpvldtxtCustom.ControlToCompare = value;
        }
    }

    public string ValidationGroup
    {
        get
        {
            return rngvldtxtCustom.ValidationGroup;
        }
        set
        {
            rngvldtxtCustom.ValidationGroup = value;
            regvldtxtCustom.ValidationGroup = value;
            reqvldtxtCustom.ValidationGroup = value;
            cmpvldtxtCustom.ValidationGroup = value;
        }
    }

    public ValidatorDisplay DisplayMode
    {
        get
        {
            return rngvldtxtCustom.Display;
        }
        set
        {
            rngvldtxtCustom.Display = value;
            regvldtxtCustom.Display = value;
            reqvldtxtCustom.Display = value;
            cmpvldtxtCustom.Display = value;
        }
    }

  
#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       // txtCustom.Attributes.Add("OnChange", "setFieldsChanged();");
    }

    protected void txtCustom_TextChanged(object sender, EventArgs e)
    {
        if (TextChanged != null) TextChanged(this,EventArgs.Empty);
    }
}
