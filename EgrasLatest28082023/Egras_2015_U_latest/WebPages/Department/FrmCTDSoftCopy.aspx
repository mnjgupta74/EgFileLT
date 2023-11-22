<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="FrmCTDSoftCopy.aspx.cs" Inherits="WebPages_FrmCTDSoftCopy" Title="Egras.Rajasthan.gov.in" %>

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
    <table width="100%" style="text-align: center" align="center" border="1" cellpadding="0"
        cellspacing="0">
        <tr>
            <td  style="text-align: center; padding: 1%;">
                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">From Date :- </span></b>
                <asp:TextBox ID="txtfromdate" MaxLength="10"  Class="form-control pull-right" TabIndex="0" runat="server" Style="width: 50%" Height="35px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtfromdate"
                    Format="dd/MM/yyyy" TargetControlID="txtfromdate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtfromdate"
                    SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td  style="text-align: center; padding: 1%;">
                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">To Date :- </span></b>
                <asp:TextBox ID="txttodate"  Class="form-control pull-right" MaxLength="10" TabIndex="1" runat="server" Style="width: 50%" Height="35px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txttodate"
                    Format="dd/MM/yyyy" TargetControlID="txttodate">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttodate"
                    SetFocusOnError="true" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td id="trmain" runat="server" visible="false" style="    width: 30%;">
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="70%">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:Button ID="btnprint" runat="server" Text="Generate" OnClick="btnprint_Click"
                    Style="font-weight: 700;" Width="90%" Height="35px" class="btn btn-primary"/>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height:40px;">
                <asp:Label ID="lblerr" runat="server" Text="" ForeColor="Red"></asp:Label></br>
            </td>
        </tr>
    </table>
</asp:Content>
