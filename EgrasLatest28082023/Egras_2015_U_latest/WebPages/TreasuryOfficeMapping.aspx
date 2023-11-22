<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="TreasuryOfficeMapping.aspx.cs" Inherits="WebPages_TreasuryOfficeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/PageHeader.css" rel="stylesheet" />
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <style>
        input[type=text] {
            height: 40px !important;
            width: 200px;
        }

        .btn {
            height: 40px !important;
            width: 125px !important;
        }

        .mandatory {
            color: #ff0000;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div _ngcontent-c6="" class="tnHead minus2point5per">
                    <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
                        <span _ngcontent-c6="" style="color: #FFF">Treasury Office Mapping</span></h2>
                    <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Change Password" />
                </div>
                <div style="border: 1px solid green;padding-top: 15px;padding-bottom: 15px;">
                    <table width="90%">
                        <br />

                        <tr>
                            <td>
                                <asp:Label ID="lblOfficeID" AssociatedControlID="txtOfficeID" Text="Enter Office ID"
                                    runat="server" /><span class="mandatory">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOfficeID" MaxLength="10"  oncopy="return false" onpaste="return false" oncut="return false"  CssClass="form-control" runat="server" AutoComplete="Off" />
                            </td>
                            <td style="padding-top: 15px; padding-bottom: 15px;">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" />
                                &nbsp;
                            </td>
                        </tr>
                        <br />
                        <tr>
                            <td colspan="3">
                                <div class=" col-sm-12" style="padding-top: 15px;padding-bottom: 15px;">
                                    <asp:Label ID="lblloginInfo" runat="server" Style="text-align: center" Text="label1" Visible="false" ForeColor="Red"></asp:Label>
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false" />
                                    <asp:Repeater ID="rptStudentDetails" runat="server"
                                        OnItemDataBound="rptStudentDetails_ItemDataBound">
                                        <HeaderTemplate>
                                            <table border="1"  width="100%" >
                                                <tr style="background-color: #428bca; color: #FFF; height: 35px;" align="center">
                                                    <th style="text-align: center;">OfficeID</th>
                                                    <th style="text-align: center;">OfficeName</th>
                                                    <th style="text-align: center;">TreasuryCode</th>
                                                    <th style="text-align: center;">TreasuryName</th>
                                                    <th style="text-align: center;">Status</th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: white;" align="center" class="success">
                                                <td><%#Eval("OfficeID") %></td>
                                                <td><%#Eval("OfficeName") %></td>
                                                <td><%#Eval("TreasuryCode") %></td>
                                                <td><%#Eval("TreasuryName") %></td>
                                                <td id="tdID" runat="server"><%#Eval("Status") %></td>

                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </td>
                        </tr>
                    </table>

                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

