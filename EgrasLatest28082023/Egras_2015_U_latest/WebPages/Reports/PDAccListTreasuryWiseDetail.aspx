<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="PDAccListTreasuryWiseDetail.aspx.cs" Inherits="WebPages_Reports_PDAccListTreasuryWiseDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        $(document).ready(function () {
            document.getElementById("MasterTable").style.display = "none";
        });
    </script>
 <div style="text-align: center;">
    <h2> Treasury Wise Detail Report </h2>
    </div>
 <%--<asp:Button ID="show" OnClientClick="show result" Visible="true" />--%>
 <table style="width: 100%" align="center" style="text-align: center;" id="Ttable">
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
                            <b>BankName</b>
                        </th>
                        <th align="center">
                            <b>Payment Type</b>
                        </th>
                        <th align="center">
                            <b>Amount</b>
                        </th>
                        <th align="center">
                            <b>Date</b>
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
                    <td align="center">
                        <%#  Eval("BankName")%>
                    </td>
                     <td align="center">
                        <%#  Eval("PaymentType")%>
                    </td>
                     <td align="right">
                        <%#  Eval("TotalAmount", "{0:0.00}")%>
                    </td>
                     <td align="center">
                        <%#  Eval("BankChallanDate")%>
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
                    <td align="center">
                        <%#  Eval("BankName")%>
                    </td>
                     <td align="center">
                        <%#  Eval("PaymentType")%>
                    </td>
                     <td align="right">
                        <%#  Eval("TotalAmount", "{0:0.00}")%>
                    </td>
                     <td align="center">
                        <%#  Eval("BankChallanDate")%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
      
      </FooterTemplate>
    </asp:Repeater>
     </table>

</asp:Content>