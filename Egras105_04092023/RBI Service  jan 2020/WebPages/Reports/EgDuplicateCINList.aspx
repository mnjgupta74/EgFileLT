<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDuplicateCINList.aspx.cs" Inherits="WebPages_Reports_EgDuplicateCINList"
    Title="EgDuplicateCINList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/Control.js"></script>
    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>

    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Duplicate CIN List" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Duplicate CIN List</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Duplicate CIN List" />
    </div>
    <table style="width: 100%" align="center" id="MainTable" style="width: 100%" border="1">
        <tr>
            <td align="left">
                <b><span style="color: #336699">Bank:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddlbanks" runat="server" Width="50%" CssClass="form-control chzn-select">
                    </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlbanks" ErrorMessage="Select Bank !"
                    InitialValue="0" SetFocusOnError="True" ValidationGroup="de">*</asp:RequiredFieldValidator>
            </td>
            <td align="left">
                <b><span style="color: #336699">From Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtFromDate" runat="server" Width="120px" Height="100%" Style="display: initial !important" CssClass="form-control" 
                        onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                    Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtFromDate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
            </td>
            <td align="left">
                <b><span style="color: #336699">To Date:-</span></b>&nbsp;
                    <asp:TextBox ID="txtToDate" runat="server" Width="120px" Height="100%" Style="display: initial !important" CssClass="form-control" onkeypress="Javascript:return NumberOnly(event)"
                        onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                    Format="dd/MM/yyyy" TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                    CultureName="en-US" TargetControlID="txtToDate" AcceptNegative="None" runat="server">
                </ajaxToolkit:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
            </td>
            </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:RadioButtonList runat="server" ID="rdBtnList"  Style="width: 70% !important; display:contents !important" CssClass="form-control" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnList_SelectedIndexChanged">
                    <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right: 35px;" />
                    <asp:ListItem Text="Manual" Value="M" />
                </asp:RadioButtonList>
            </td>
            <td align="center">
                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-default" Height="100%" ValidationGroup="de" Text="Show" OnClick="btnShow_Click" />
            </td>
        </tr>
        <tr align="center" id="trrpt" runat="server" visible="false">
            <td colspan="5">
                <asp:Repeater ID="rptrDuplicateCINlst" runat="server">
                    <HeaderTemplate>
                        <table border="1" width="100%" cellpadding="0" cellspacing="0">
                            <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px">
                                <th>
                                    <b>S.No</b>
                                </th>
                                <th>
                                    <b>GRN</b>
                                </th>
                                <th>
                                    <b>CIN</b>
                                </th>
                                <th>
                                    <b>Amount</b>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem,"ROWNO")%>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem, "CIN")%>
                            </td>
                            <td align="right">
                                <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem,"ROWNO")%>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem,"GRN")%>
                            </td>
                            <td align="center">
                                <%# DataBinder.Eval(Container.DataItem, "CIN")%>
                            </td>
                            <td align="right">
                                <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.00}")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>

        <tr id="trmsg" runat="server" visible="false">
            <td align="center" colspan="5">
                <asp:Label ID="lblMessage" Visible="false" Text="No Record Found" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
            </td>
        </tr>
    </table>


</asp:Content>
