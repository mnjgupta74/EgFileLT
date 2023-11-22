<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgErrorInfo.aspx.cs" Inherits="WebPages_EgErrorInfo" Title="Error Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--Header table--%>
    <table id="tblheader" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
        <tr>
            <td style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Error Information" Font-Bold="True"
                    ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>
    </table>
    <fieldset id="fieldErrorInfo" runat="server">
        <legend style="color: #005CB8; font-size: small">DateWise Error Information</legend>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
            <%--Date/Search--%>
            <tr align="center">
                <td align="center" style="height: 30px; text-align: left;" valign="middle">
                    From Date :
                    <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFromd" runat="server" ErrorMessage="Required Field"
                        SetFocusOnError="true" ControlToValidate="txtFromDate" ValidationGroup="de"></asp:RequiredFieldValidator>
                    &nbsp; &nbsp; &nbsp;
                </td>
                <td align="center" style="height: 30px; text-align: center;" valign="middle">
                    To Date :
                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field"
                        SetFocusOnError="true" ControlToValidate="txtToDate" ValidationGroup="de"></asp:RequiredFieldValidator>
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rbltype" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Errors</asp:ListItem>
                        <asp:ListItem Value="2">BankService</asp:ListItem>
                        <asp:ListItem Value="3">BankResponse</asp:ListItem>
                        <asp:ListItem Value="4">EgrasService</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" ValidationGroup="de" TabIndex="3"
                        OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblEmptyData" runat="server" Text="" ForeColor="green" Visible="false"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table id="Table2" runat="server" border="1" cellpadding="0" cellspacing="0" align="center"
            width="60%">
            <%--  Repeater--%>
            <tr>
                <td>
                    <asp:TextBox ID="txtDecrypt" runat="server" TextMode="MultiLine" Width="800px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="rptErrorInfo" runat="server" OnItemCommand="rptErrorInfo_ItemCommand">
                        <HeaderTemplate>
                            <table style="background-color: #336699; font-size: 11pt; font-family: Arial; font-weight: normal;"
                                cellspacing="1" cellpadding="1" width="100%" align="center">
                                <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                    height: 20px">
                                    <td align="center" width="40%">
                                        ErrorName
                                    </td>
                                    <td align="center" width="40%">
                                        PageName
                                    </td>
                                    <td align="center" width="20%">
                                        Decrypt
                                    </td>
                                    <td align="center" width="20%">
                                        TransDate
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB; height: 20px; text-align: justify;">
                                <td width="40%">
                                   <asp:Label ID="lblError" runat="server" Text= '<%# Eval("ErrorName")%>' style="overflow:scroll;" width="800px"></asp:Label>
                                </td>
                                <td width="40%">
                                    <%#DataBinder.Eval(Container, "DataItem.PageName")%>
                                </td>
                                <td width="20%">
                                    <asp:LinkButton ID="lnkDecrypt" runat="server" CausesValidation="false" CommandName="Decrypt"
                                        CommandArgument='<%#Eval("PageName")%>' Text="Decrypt"></asp:LinkButton>
                                </td>
                                <td width="20%">
                                    <%#DataBinder.Eval(Container, "DataItem.TransDate")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
            TargetControlID="txtFromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>
    </fieldset>
</asp:Content>
