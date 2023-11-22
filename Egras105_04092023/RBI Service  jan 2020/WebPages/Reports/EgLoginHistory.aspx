<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgLoginHistory.aspx.cs" Inherits="WebPages_EgLoginHistory" Title="Egras Login History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

   <script language="javascript" type="text/javascript">

    //Function to open NewWindow  
    function basicPopup() 
    {
        popupWindow = window.open("EgLoginInfo.aspx", 'popUpWindow', 'height=600,width=920,left=252,top=120,resizable=no,scrollbars=yes,toolbar=no,menubar=no,location=no,directories=no, status=No');
    }
    </script>

    <table id="tblheader" border="0" cellpadding="0" cellspacing="0" align="center" width="80%">
        <tr>
            <td style="text-align: center; height: 35px" valign="top">
                <asp:Label ID="Labelheader" runat="server" Text="Login History" Font-Bold="True"
                    Font-Size="Larger" ForeColor="#009900" Style="text-decoration: underline;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <b style="color: Green;">No of Active Users </b>:
                <asp:Label ID="LabelUser" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <br />
                <br />
                <fieldset id="fieldLoginDetail" runat="server" width="70%" >
                    <legend style="color: #005CB8; font-size: small">General User Login Details</legend>
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
                        <tr>
                            <td align="center">
                                <asp:DataList ID="DataListLoginDetails" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    OnItemDataBound="DataListLoginDetails_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="tableheader" width="920px" border="1" cellpadding="0" cellspacing="0">
                                            <tr style="background-color: #507CD1; color: #FFFFFF; font-weight: bold; font-size: small;">
                                                <td style="width: 150px;">
                                                    UserId
                                                </td>
                                                <td style="width:100px; text-align: center">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px;">
                                                    Status
                                                </td>
                                                <td style="width: 150px;">
                                                    UserId
                                                </td>
                                                <td style="width: 100px; text-align: center">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px;">
                                                    Status
                                                </td>
                                                <td style="width: 150px;">
                                                    UserId
                                                </td>
                                                <td style="width: 100px; text-align: center">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px; text-align: center">
                                                    Status
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="tablerecords" border="1" cellpadding="0" cellspacing="0" width="300px">
                                            <tr id="tRow" runat="server" style="height: auto; background-color: #EFF3FB; color: #003366;">
                                                <td style="width: 150px;">
                                                    <asp:Label ID="LabelUserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="LabelUserCount" runat="server" Text='<%#Eval("UserCount")%>'></asp:Label>
                                                </td>
                                                <td style="width: 40px;">
                                                    <asp:Image ID="Image1" Height="20" Width="20" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                
                            </td>
                        </tr>
                        <tr id="trdetails" runat="server">
                        <td align="right">
                        <asp:LinkButton  ID="LinkButton1" runat="server" ForeColor="Green" 
                        Font-Size="Small" OnClientClick="basicPopup();" >Click Here for Details!</asp:LinkButton>
                        </td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset id="Fieldset1" runat="server" width="70%">
                    <legend style="color: #005CB8; font-size: small">Officewise Login Details</legend>
                    <table id="Table2" border="0" cellpadding="0" cellspacing="0" align="center" width="60%">
                        <tr>
                            <td>
                                <%--Working DataList--%>
                                <asp:DataList ID="DataListOffice" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    OnItemDataBound="DataListOffice_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="tableheader" width="920px" border="1" cellpadding="0" cellspacing="0">
                                            <tr style="background-color: #507CD1; color: #FFFFFF; font-weight: bold; font-size: small;">
                                                <td style="width: 150px;">
                                                    OfficeName
                                                </td>
                                                <td id="Td1" style="width: 100px; text-align: center">
                                                    Office Id
                                                </td>
                                                <td runat="server" style="width: 30px; text-align: center" visible="false">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px;">
                                                    Status
                                                </td>
                                                <td style="width: 150px;">
                                                    OfficeName
                                                </td>
                                                <td id="Td2" style="width: 100px; text-align: center">
                                                    Office Id
                                                </td>
                                                <td style="width: 30px; text-align: center" visible="false" runat="server">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px;">
                                                    Status
                                                </td>
                                                <td style="width: 150px;">
                                                    OfficeName
                                                </td>
                                                <td id="Td3" style="width: 100px; text-align: center">
                                                    Office Id
                                                </td>
                                                <td style="width: 30px; text-align: center" visible="false" runat="server">
                                                    No. of Users
                                                </td>
                                                <td style="width: 30px; text-align: center">
                                                    Status
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table id="tablerecords1" border="1" cellpadding="0" cellspacing="0" width="300px">
                                            <tr id="tRow1" runat="server" style="height: auto; background-color: #EFF3FB; color: #003366;">
                                                <td style="width: 150px;">
                                                    <asp:Label ID="LabelUserName" runat="server" Text='<%#Eval("DeptNameEnglish")%>'></asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="LabelUserid" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                                </td>
                                                <td align="center" style="width: 40px;" visible="false">
                                                    <asp:Label ID="LabelUserCount" runat="server" Text='<%#Eval("UserCount")%>'></asp:Label>
                                                </td>
                                                <td align="center" style="width: 40px;">
                                                    <asp:Image ID="Image1" Height="20" Width="20" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    
  
</asp:Content>
