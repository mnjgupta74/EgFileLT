<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="FrmCTDSoftCopy.aspx.cs" Inherits="WebPages_FrmCTDSoftCopy" Title="Egras.Raj.Nic.in" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="CTD Data SoftCopy"  class="pull-left">
                    <span _ngcontent-c6="" style="color: #FFF">CTD Data SoftCopy Generator</span></h2>
               <img src="../../Image/help1.png"  class="pull-right" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="left" Title="" />
            </div>
    <table width="100%" style="text-align: center" align="center" border="0" cellpadding="0"
        cellspacing="0">
        <%--<tr>
            <td align="center" colspan="4" style="height: 35px;">
                <b>CTD Data SoftCopy Generator</b>
            </td>
        </tr>--%>
        <tr>
            <td>
                From Date :
                <asp:TextBox ID="txtfromdate" MaxLength="10" TabIndex="0" runat="server" Style="width: 100px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtfromdate"
                    Format="dd/MM/yyyy" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtfromdate"
                    SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                To Date :
                <asp:TextBox ID="txttodate" MaxLength="10" TabIndex="1" runat="server" Style="width: 100px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txttodate"
                    Format="dd/MM/yyyy" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttodate"
                    SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td id="trmain" runat="server" visible="false">
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="400px">
                </asp:DropDownList>
            </td>
            <td align="center" colspan="2">
                <asp:Button ID="btnprint" runat="server" Text="Generate" OnClick="btnprint_Click"
                    Style="font-weight: 700" Width="150px" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height:40px;">
                <asp:Label ID="lblerr" runat="server" Text="" ForeColor="Red"></asp:Label></br>
            </td>
        </tr>
    </table>
</asp:Content>
