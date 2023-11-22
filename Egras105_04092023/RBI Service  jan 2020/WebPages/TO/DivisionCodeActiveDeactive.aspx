<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="DivisionCodeActiveDeactive.aspx.cs" Inherits="WebPages_TO_DivisionCodeActiveDeactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script src="../../js/moment.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />

    <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
        .btn-default.disabled, .btn-default[disabled], fieldset[disabled] .btn-default, .btn-default.disabled:hover, .btn-default[disabled]:hover, fieldset[disabled] .btn-default:hover, .btn-default.disabled:focus, .btn-default[disabled]:focus, fieldset[disabled] .btn-default:focus, .btn-default.disabled:active, .btn-default[disabled]:active, fieldset[disabled] .btn-default:active, .btn-default.disabled.active, .btn-default[disabled].active, fieldset[disabled] .btn-default.active {
                background-color: #abaaaa;
                border-color: #ccc;
            }
    </style>

    <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="BudgetHeadDetail">
            <span _ngcontent-c6="" style="color: #FFF">Division Code Active/Deactive</span></h2>
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td align="left" width="50%">
                <b><span style="color: #336699">Treasury Name </span></b>&nbsp;
                <asp:DropDownList ID="ddlTreasury" runat="server" Width="50%" AutoPostBack="true" 
                    class="chzn-select" OnSelectedIndexChanged="ddlTreasury_SelectedIndexChanged">
                         
                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Department"
                                ControlToValidate="ddlTreasury" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
            </td>
            <td align="left">
                <b><span style="color: #336699">Office Name </span></b>&nbsp;
                <asp:DropDownList ID="ddlOffice" runat="server" Width="50%" AutoPostBack="false" class="chzn-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Office"
                                ControlToValidate="ddlOffice" ValidationGroup="de" InitialValue="0" ForeColor="Red"
                                Style="text-align: center">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnShow" runat="server" Text="Show" Height="32px" OnClick="btnShow_Click"
                                            TabIndex="20" ValidationGroup="de" class="btn btn-default"/>
                <asp:Button ID="btnreset" runat="server" Text="Reset" Height="32px" OnClick="btnreset_Click"
                                            TabIndex="20" ValidationGroup="de" class="btn btn-default"/>
            </td>
        </tr>
    </table>
    <table runat="server" style="width: 100%;" id="TABLE2" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td align="center">
                        <asp:Repeater ID="rptDivActive" runat="server" OnItemCommand="rptDivActive_ItemCommand">
                            <HeaderTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #5F8DC9;">
                                        <td align="left"  height="25px">
                                            <b>Division Name</b>
                                        </td>
                                        <td align="left"  height="25px">
                                            <b>Action</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblEmpty" Style="text-align: center" runat="server" Text="No Record Found"
                                     Font-Bold="true" ForeColor="Teal" 
                                    Visible='<%#bool.Parse((rptDivActive.Items.Count==0).ToString())%>'></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" width="122px" height="25px">
                                            <%-- <asp:HyperLink ID="hp1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Profile") %>'>
                                            </asp:HyperLink>--%>
                                            <%# DataBinder.Eval(Container.DataItem,"DivisionName")%>
                                        </td>                                        
                                        <td align="center" width="100px" height="25px">
                                            <asp:LinkButton ID="lnkDetail" runat="server" CommandName="Ac" 
                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DivCode") %>'> 
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Flag")%>' Visible="false" 
                                                Display="Dynamic" ></asp:Label>
                                                 <%# Eval("Flag").ToString() == "A" ? "<img src='../../Image/active.png' title='Make Hide' border='0' style='width: 8%'/>" : "<img src='../../Image/deactive.png' title='Make Show' border='0' style='width: 8%'/>"%>                                           
                                            
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="background-color: #E1E9F2;">
                                        <td align="left" width="122px" height="25px">
                                            <%-- <asp:HyperLink ID="hp1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Profile") %>'>
                                            </asp:HyperLink>--%>
                                            <%# DataBinder.Eval(Container.DataItem,"DivisionName")%>
                                        </td>
                                        <td align="center" width="100px" height="25px">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Flag")%>' Visible="false" 
                                                Display="Dynamic" ></asp:Label>
                                            <asp:LinkButton ID="lnkDetail" runat="server" CommandName="Ac" 
                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DivCode") %>'>
                                        <%# Eval("Flag").ToString() == "A" ? "<img src='../../Image/active.png' title='Make Hide' border='0' style='width: 8%'/>" : "<img src='../../Image/deactive.png' title='Make Show' border='0' style='width: 8%'/>"%>
                                                 <%--Text='<%# Eval("Flag").ToString() == "Y" ? "Active" : "De Activate"  %>'--%>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
</asp:Content>

