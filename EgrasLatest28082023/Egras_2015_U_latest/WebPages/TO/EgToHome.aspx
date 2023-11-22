<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgToHome.aspx.cs" Inherits="WebPages_TO_EgToHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-6" style="margin-top: 60px;border: 1px solid;padding-top: 10px;padding-bottom: 10px;height:150px">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblfname" runat="server" Text="Name:-" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFirstNameBound" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblsucc" runat="server" Text="Last Successful Login:-"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLastsuccess" runat="server" Text="Label" ForeColor="#003300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblfail" runat="server" Text="Last Failure Login:-"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbllastfail" runat="server" Text="Label" ForeColor="#CC3300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblpass" runat="server" Text="Last Change Password:-"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLastchange" runat="server" Text="Label" ForeColor="#000066"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>

