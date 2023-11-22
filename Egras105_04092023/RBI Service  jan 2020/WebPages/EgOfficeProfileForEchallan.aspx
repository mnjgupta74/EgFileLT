<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgOfficeProfileForEchallan.aspx.cs" Inherits="WebPages_EgOfficeProfileForEchallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../App_Themes/Images/progress.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table runat="server" style="width: 90%;" id="TABLE1" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblSchema" runat="server" Text="Office Schema" ForeColor="Green" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="40%">
                        Department:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddldepartment" runat="server" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"
                            Width="50%" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="ddldepartment"
                            ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td width="50%">
                        MajorHead: &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlMajorHeadList" runat="server" OnSelectedIndexChanged="ddlMajorHeadList_SelectedIndexChanged"
                            AutoPostBack="true" Width="50%">
                        </asp:DropDownList>
                        <asp:Button ID="btnMore" runat="server" Text="More Heads..." OnClick="btnMore_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMajorHeadList"
                            InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                    </td>
                    <%--<td width="10%">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" Text="Back"></asp:HyperLink>
                    </td>--%>
                </tr>
            </table>
            <div id="BudgetSchema" runat="server" style="margin-left: 51px; width: 100%;" visible="false">
                <table id="budgetlist" border="1" cellpadding="0" cellspacing="1" style="width: 90%;">
                    <tr>
                        <td width="46%">
                            <asp:ListBox ID="lstbudgethead" runat="server" Width="100%" Height="97px"></asp:ListBox>
                        </td>
                        <td width="8%" style="text-align: center;">
                            <asp:Button ID="btnnext" runat="server" Text="&gt;&gt;" OnClick="btnnext_Click" />
                            &nbsp;&nbsp;
                            <br />
                            <br />
                            <asp:Button ID="btnprev" runat="server" OnClick="btnprev_Click" Text="&lt;&lt;" />
                            &nbsp;
                        </td>
                        <td width="46%">
                            <asp:Label ID="addlabel" runat="server" Text="Add Your Budget Head/Purpose" ForeColor="#339933"></asp:Label>
                            <asp:ListBox ID="lstselectedbudget" runat="server" Width="100%" Height="96px" Style="margin-right: 0px">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="text-align: left;" colspan="3">
                            <b>Object Head:</b>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="ddlObjectHead" runat="server" Width="30%">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvObjectHead" runat="server" ControlToValidate="ddlObjectHead"
                                ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" style="text-align: left;">
                            <b>Plan/Non Plan:</b>
                            <asp:RadioButtonList ID="rblPlan" runat="server" Font-Bold="False" RepeatDirection="Horizontal"
                                ValidationGroup="vldInsert">
                                <asp:ListItem Selected="True" Text="SF" Value="P"></asp:ListItem>
                                <asp:ListItem  Enabled="false" Text="Non Plan" Value="N"></asp:ListItem>
                                <asp:ListItem Text="CA" Value="C"></asp:ListItem>
                                <asp:ListItem Text="N.A." Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfcPNP" runat="server" ControlToValidate="rblPlan"
                                Display="None" ErrorMessage="Select any one" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style3" style="text-align: left;" colspan="2">
                            <b>Voted/Charged:</b>
                            <asp:RadioButtonList ID="RblVoted" runat="server" Font-Bold="False" RepeatDirection="Horizontal"
                                ValidationGroup="vldInsert">
                                <asp:ListItem Selected="True" Text="Voted" Value="V"></asp:ListItem>
                                <asp:ListItem Text="Charged" Value="C"></asp:ListItem>
                                <asp:ListItem Text="N.A." Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfcVoted" runat="server" ControlToValidate="RblVoted"
                                Display="None" ErrorMessage="Selection Required" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="a" />
                            <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                </table>
            </div>           
            <div id="DivCheckBudget" runat="server" style="margin-left: 170px; margin-bottom:200px; width: 100%;" visible="false">
                <table id="Table2" border="1" cellpadding="0" cellspacing="1" style="width: 60%;">
                    <tr>
                        <td align="center">
                            <b> Budget Head List: </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdCheckBudget" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                Width="100%" ShowFooter="True" FooterStyle-BackColor="#336699" EmptyDataRowStyle-ForeColor="#336699"
                                EmptyDataRowStyle-VerticalAlign="Middle">
                                <Columns>
                                    <asp:BoundField DataField="BudgetHead" HeaderText="BudgetHead">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Objectheadcode" HeaderText="ObjectHead">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BFCType" HeaderText="SF/Non Plan">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="headtype" HeaderText="Voted/Charged">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#336699" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
