<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgEditChallanDetails.aspx.cs" Inherits="WebPages_TO_EgEditChallanDetails"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function NumberOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) == false) {
                el.value = "";
            }
        }

        function DecimalNumber(el) {
            var ex = /^[0-9]+\.?[0-9]*$/;
            if (el.value != "") {
                if (ex.test(el.value) == false) {
                    alert('Incorrect Amount');
                    el.value = "";
                }
            }
        }

        function textCounter(field, field2, maxlimit) {
            var countfield = document.getElementById(field2);
            if (field.value.length > maxlimit) {
                field.value = field.value.substring(0, maxlimit);
                return false;
            } else {
                countfield.value = maxlimit - field.value.length;
            }
        }

       

    </script>

    <table id="tblheader" border="0" cellpadding="0" cellspacing="0" align="center" width="84%">
        <tr>
            <td style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Edit Challan Details" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset id="fieldamount" class="fldset" runat="server" align="center">
                    <legend style="color: #005CB8; font-size: small">Edit Challan Detail</legend>
                    <table id="Table1" border="1" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center">
                                GRN:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtGRN" runat="server" onChange="NumberOnly(this);" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Details" runat="server">
                            <td>
                                <table>
                                    <tr class="gridalternaterow">
                                        <td style="text-align: left; height: 24px;" colspan="1" class="tdstyle fcolor">
                                            Total Amount
                                        </td>
                                        <td style="text-align: left; height: 24px;" colspan="3">
                                            <asp:TextBox ID="txtTotalAmount" runat="server" Width="130px" onChange="DecimalNumber(this);"
                                                MaxLength="14"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td style="text-align: left" class="tdstyle fcolor">
                                            District
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlTreasury" runat="server" AutoPostBack="true" CssClass="borderRadius inputDesign"
                                                Width="186px" OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="--Select Location--"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Location"
                                                ControlToValidate="ddlTreasury" ValidationGroup="vldInsert" InitialValue="0"
                                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td style="text-align: left; vertical-align: top" class="tdstyle fcolor">
                                            Office Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td class="tdstyle" style="text-align: left;">
                                            <asp:DropDownList ID="ddlOfficeName" runat="server" AutoPostBack="true" CssClass="borderRadius inputDesign"
                                                OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" Width="186px">
                                                <asp:ListItem Value="0" Text="--Select Office--"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select Office"
                                                ControlToValidate="ddlOfficeName" ValidationGroup="vldInsert" InitialValue="0"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; vertical-align: top" class="tdstyle fcolor">
                                            &nbsp;Treasury
                                        </td>
                                        <td class="tdstyle" style="text-align: left;">
                                            <asp:DropDownListX ID="ddllocation" runat="server" CssClass="borderRadius inputDesign"
                                                Width="186px">
                                                <asp:ListItem Value="0" Text="--Select Treasury--"></asp:ListItem>
                                            </asp:DropDownListX>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Treasury"
                                                ControlToValidate="ddllocation" ValidationGroup="vldInsert" InitialValue="0"
                                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <%-- <tr class="gridalternaterow">
                                        <td class="style3 fcolor" align="center" colspan="4">
                                            Previous Period: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr class="gridalternaterow">
                                        <td class="style3 fcolor" style="width: 173px;" valign="top">
                                            Year(Period)
                                        </td>
                                        <td colspan="3" style="height: 10px; width: auto; text-align: left;">
                                            <asp:DropDownList ID="ddlYear" runat="server" Width="100px" Height="22px" CssClass="borderRadius ">
                                                <asp:ListItem Text="2009-10" Value="0910"></asp:ListItem>
                                                <asp:ListItem Text="2010-11" Value="1011"></asp:ListItem>
                                                <asp:ListItem Text="2011-12" Value="1112"></asp:ListItem>
                                                <asp:ListItem Text="2012-13" Value="1213"></asp:ListItem>
                                                <asp:ListItem Text="2013-14" Value="1314" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                ErrorMessage="Select Year!" ControlToValidate="ddlYear" ValidationGroup="vldInsert"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                            &nbsp;                                           
                                            <div id="divOneTime" runat="server" style="margin: -21px 0px 0px 270px;">
                                                <asp:TextBox ID="txtfromdate" runat="server" Width="100px" Height="16px" onkeypress="Javascript:return NumberOnly(event)"
                                                    onChange="javascript:return dateValidation()">
                                                </asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtfromdate">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                                    CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                                                </ajaxToolkit:MaskedEditExtender>                                                
                                                <asp:TextBox ID="txttodate" runat="server" Width="100px" Height="16px" onkeypress="Javascript:return NumberOnly(event)"
                                                    onChange="javascript:return dateValidation1()"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txttodate">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                                                    CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                                                </ajaxToolkit:MaskedEditExtender>
                                            </div>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td colspan="4" valign="middle" align="left">
                                            <asp:Label ID="lblinfo" runat="server" Text="Personal Detail :" Font-Bold="True"
                                                ForeColor="#D87E3D"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td style="margin-left: 10px; width: 124px;" class="tdstyle fcolor">
                                            Remiter's Name
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="180px" CssClass="borderRadius inputDesign"
                                                OnChange="CharCheck(this);"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ErrorMessage="Enter Full Name!"
                                                ControlToValidate="txtName" ValidationGroup="vldInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtName"
                                                CssClass="XMMessage" Text="Special character not allowed.!" ErrorMessage="Special character not allowed in Full Name.!"
                                                ValidationExpression="^[A-Za-z]([a-z]|[A-Z]|[0-9]|[.]|[-/ ]|[&])*$" Display="Dynamic"
                                                ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="tdstyle fcolor" style="width: 173px; text-align: left;">
                                            TIN/Lease No./Actt. No./Vehicle No.<br />
                                            /Tax-Id(If Any)
                                        </td>
                                        <td class="tdstyle" style="text-align: left">
                                            <asp:TextBox ID="txtTIN" runat="server" Width="180px" CssClass="borderRadius inputDesign"
                                                CausesValidation="true" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Your Tin Number !"
                                                ControlToValidate="txtTIN" ValidationGroup="vldnotInsert" Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:RegularExpressionValidator ID="rgvTinNo" runat="server" ControlToValidate="txtTIN"
                                                CssClass="XMMessage" Text="Special character not allowed.!" ErrorMessage="Special character not allowed in Tin No.!"
                                                ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                                Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr class="gridalternaterow">
                                        <td class="tdstyle fcolor" style="width: 124px">
                                            Address
                                        </td>
                                        <td class="tdstyle" style="text-align: left;">
                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="borderRadius inputDesign"
                                                OnkeyPress="javascript:Count(this,100);" Width="180px" MaxLength="100"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvaddress" runat="server" ErrorMessage="Enter Your Address !"
                                                ControlToValidate="txtaddress" ValidationGroup="vldInsert" Display="Dynamic"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtaddress"
                                                CssClass="XMMessage" ErrorMessage="Special character not allowed in Address.!"
                                                ValidationExpression="^([a-zA-Z0-9_.,:;*!#`$+\[(.*?)\]()'@%?={}&//\\ \s\-]*)$"
                                                Display="Dynamic" ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="tdstyle fcolor" style="width: 173px">
                                            Remarks(If Any)
                                        </td>
                                        <td class="tdstyle fcolor" style="text-align: left">
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="180px" OnkeyPress="javascript:textCounter(this,'counter',200);"
                                                CssClass="borderRadius inputDesign" MaxLength="100"></asp:TextBox>
                                            <br />
                                            Remaining Words:
                                            <input maxlength="3" size="3" id="counter" disabled="disabled">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRemark"
                                                CssClass="XMMessage" ErrorMessage="Special character not allowed in Remarks.!"
                                                ValidationExpression="^([a-zA-Z0-9_.,//\\\s\-]*)$" ValidationGroup="vldInsert"
                                                Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vldInsert"
                                    OnClick="btnSubmit_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </td>
                        </tr>
                        <tr id="rowLabel" runat="server" visible="false">
                            <td align="center" colspan="4" style="height: 16px">
                                <asp:Label ID="lblEmptyData" runat="server" Text="Record not found." ForeColor="green"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
