<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAgSoftCopy.aspx.cs" Inherits="WebPages_AG_EgAgSoftCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/Control.js"></script>

    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <div id="divChallanView">
        <div _ngcontent-c6="" class="tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="TY-33">
                <span _ngcontent-c6="" style="color: #FFF">AG Soft Copy Download</span></h2>
            <img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="AG Soft Copy Download" />
        </div>
        <table style="width: 100%;" border="1" align="center">
           
            <tr>
                <td style="text-align: left">Select One :
                                <asp:DropDownList ID="ddlType" runat="server" Width="200px">
                                    <asp:ListItem Value="Eg_SORHeader">SORHeader</asp:ListItem>
                                    <asp:ListItem Value="Eg_SORDetail">SORDetail</asp:ListItem>
                                    <asp:ListItem Value="Eg_lorHeader">LORHeader</asp:ListItem>
                                    <asp:ListItem Value="EG_LorDetail">LORDetail</asp:ListItem>
                                    <asp:ListItem Value="Eg_ChallanHeader">ChallanHeader</asp:ListItem>
                                    <asp:ListItem Value="Eg_Challandetail">Challan_Detail</asp:ListItem>
                                    <asp:ListItem Value="eg_sorheadernew">SOR Header New</asp:ListItem>
                                </asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lbltreasurycode" runat="server" Text="Treasury : "></asp:Label>
                    <asp:DropDownList ID="ddltreasury" runat="server" TabIndex="1" Width="200px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Treasury"
                        InitialValue="-1" ControlToValidate="ddltreasury" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Date From :
                                <asp:TextBox ID="txtDateFrom" runat="server" Width="100px"
                                    onpaste="return false" onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtDateTo)"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtDateFrom" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtDateFrom"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td style="text-align: left">Date To :
                                <asp:TextBox ID="txtDateTo" runat="server" Width="100px"
                                    onpaste="return false" onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtDateFrom)"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txtDateTo" TargetControlID="txtDateTo">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtDateTo" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txtDateTo"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Download"
                        Width="205px" ValidationGroup="a" />
                </td>
            </tr>
        </table>
</asp:Content>
