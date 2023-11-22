<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgBlockUnBlockOfficeForDiv.aspx.cs" Inherits="WebPages_Reports_EgBlockUnBlockOfficeForDiv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

    <fieldset runat="server" id="lstrecord" style="width: 1000px;">
        <legend style="color: #336699; font-weight: bold">UnBlock Office</legend>
        <table style="width: 101%" align="center" id="MainTable">
            <tr style="height: 45px">
                <td align="center">
                    <b><span style="color: #336699">Treasury Code:-</span></b>&nbsp;
                    <asp:DropDownList ID="ddlTreasury" runat="server" Width="120px">
                        <asp:ListItem Value="0" Text="--All Treasury--"></asp:ListItem>
                        <asp:ListItem Value="0100" Text="Ajmer"></asp:ListItem>
                        <asp:ListItem Value="0200" Text="ALWAR"></asp:ListItem>
                        <asp:ListItem Value="0300" Text="BANSWARA"></asp:ListItem>
                        <asp:ListItem Value="0400" Text="BARAN"></asp:ListItem>
                        <asp:ListItem Value="0500" Text="BARMER"></asp:ListItem>
                        <asp:ListItem Value="0600" Text="BEAWAR"></asp:ListItem>
                        <asp:ListItem Value="0700" Text="BHARATPUR"></asp:ListItem>
                        <asp:ListItem Value="0800" Text="BHILWARA"></asp:ListItem>
                        <asp:ListItem Value="0900" Text="BIKANER"></asp:ListItem>
                        <asp:ListItem Value="1000" Text="BUNDI"></asp:ListItem>
                        <asp:ListItem Value="1100" Text="CHITTORGARH"></asp:ListItem>
                        <asp:ListItem Value="1200" Text="CHURU"></asp:ListItem>
                        <asp:ListItem Value="1300" Text="DAUSA"></asp:ListItem>
                        <asp:ListItem Value="1400" Text="DHOLPUR"></asp:ListItem>
                        <asp:ListItem Value="1500" Text="DUNGARPUR"></asp:ListItem>
                        <asp:ListItem Value="1600" Text="GANGANAGAR"></asp:ListItem>
                        <asp:ListItem Value="1700" Text="HANUMANGARH"></asp:ListItem>
                        <asp:ListItem Value="1800" Text="JAIPUR (CITY)"></asp:ListItem>
                        <asp:ListItem Value="2000" Text="JAIPUR (RURAL)"></asp:ListItem>
                        <asp:ListItem Value="2100" Text="JAIPUR (SECTT.)"></asp:ListItem>
                        <asp:ListItem Value="2200" Text="JAISALMER"></asp:ListItem>
                        <asp:ListItem Value="2300" Text="JALORE"></asp:ListItem>
                        <asp:ListItem Value="2400" Text="JHALAWAR"></asp:ListItem>
                        <asp:ListItem Value="2500" Text="JHUNJHUNU"></asp:ListItem>
                        <asp:ListItem Value="2600" Text="JODHPUR (CITY)"></asp:ListItem>
                        <asp:ListItem Value="2700" Text="JODHPUR (RURAL)"></asp:ListItem>
                        <asp:ListItem Value="2800" Text="KAROLI"></asp:ListItem>
                        <asp:ListItem Value="2900" Text="KOTA"></asp:ListItem>
                        <asp:ListItem Value="3000" Text="NAGAUR"></asp:ListItem>
                        <asp:ListItem Value="3100" Text="PALI"></asp:ListItem>
                        <asp:ListItem Value="3200" Text="PRATAPGARH"></asp:ListItem>
                        <asp:ListItem Value="3300" Text="RAJSAMAND"></asp:ListItem>
                        <asp:ListItem Value="3400" Text="SAWAI MADHOPUR"></asp:ListItem>
                        <asp:ListItem Value="3500" Text="SIKAR"></asp:ListItem>
                        <asp:ListItem Value="3600" Text="SIROHI"></asp:ListItem>
                        <asp:ListItem Value="3700" Text="TONK"></asp:ListItem>
                        <asp:ListItem Value="3800" Text="UDAIPUR"></asp:ListItem>
                        <asp:ListItem Value="4100" Text="UDAIPUR RURAL"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="btnShow" runat="server" ValidationGroup="de" size="25" Text="Show" OnClick="btnShow_Click" Width="97px" /></td>
            </tr>
            <tr align="center" id="trrpt" runat="server">
                <td colspan="6">
                    <asp:Repeater ID="rptrUnblockOfficelist" OnItemCommand="rptrUnblockOfficelist_ItemCommand" OnItemDataBound="rptrUnblockOfficelist_ItemDataBound" runat="server">
                        <HeaderTemplate>
                            <table border="1" width="80%" cellpadding="0" cellspacing="0">
                                <tr style="background-color: #14c4ff; color: White; font-weight: bold; height: 20px">
                                    <th>
                                        <b>S.No</b>
                                    </th>
                                    <th>
                                        <b>UnBlock</b>
                                    </th>
                                    <th>
                                        <b>Office Name</b>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <asp:CheckBox Text="" runat="server" ID="chkBox" ToolTip ='<%# Eval("OfficeName") %>' />
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "OfficeName")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <asp:CheckBox Text="" runat="server" ID="chkBox" ToolTip ='<%# Eval("OfficeName") %>' />
                                </td>
                                <td align="center">
                                    <%# DataBinder.Eval(Container.DataItem, "OfficeName")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblDefaultMessage" runat="server" Text="Sorry, No Record Found." Visible="false">
                            </asp:Label>
                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Style="margin: 10px" Text="Update" />
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>

            </tr>
        </table>
    </fieldset>

</asp:Content>

