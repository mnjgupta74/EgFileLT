<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgGRNBankStatusDetail.aspx.cs" Inherits="WebPages_Reports_EgGRNBankStatusDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style="margin-left: auto; margin-right: auto; text-align: center;">
    <h2> Day Wise GRN Bank Status Report </h2>
    </div>
 <asp:Button ID="show" OnClientClick="show result" Visible="true" />
 <table style="width: 100%" align="center" id="Ttable">
 <asp:Repeater ID="RepeaterPop" runat="server" Visible="false">
            <HeaderTemplate>
                <table border="1" width="85%" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #507CD1; color: White; font-weight: bold; height: 20px;">
                        <th align="center">
                            <b>S.No</b>
                        </th>
                        <th align="center">
                            <b>GRN</b>
                        </th>
                        <th align="center">
                            <b>Amount</b>
                        </th>

                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%# Container.ItemIndex+1 %>
                    </td>
                     <td align="center">
                         <%#  Eval("GRN")%> 
                    </td>
                    <td align="right">
                        <%#  Eval("Amount")%>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
               <tr>
                    <td align="center">
                        <%# Container.ItemIndex+1 %>
                    </td>
                     <td align="center">
                         <%#  Eval("GRN")%> 
                    </td>
                    <td align="right">
                        <%#  Eval("Amount")%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
      
      </FooterTemplate>
    </asp:Repeater>
     </table>

</asp:Content>

