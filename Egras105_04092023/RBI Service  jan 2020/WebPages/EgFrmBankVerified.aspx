<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgFrmBankVerified.aspx.cs" Inherits="WebPages_EgFrmBankVerified" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../js/SweetAlert/sweetalert.css" rel="stylesheet" /> 
    <script src="../js/SweetAlert/sweetalert.min.js"></script>
    <style>
        .sweet-alert p {
            text-align:center;
        }
    </style>
    <script>
        $('#ctl00_ContentPlaceHolder1_txtGRN').change(function () {
            //var a = $("[id*=rdblReset] input:checked").val();
            var flag = false;
            if (isNaN($('#ctl00_ContentPlaceHolder1_txtGRN').val())) {
                flag = true;
            }
            var len = $('#ctl00_ContentPlaceHolder1_txtGRN').val().length;
            if (len != 10 && !flag) {
                alert("Enter Correct Mobile Number !!");
                $('#txt_Login').val("");
                $('#txt_Login').focus();
            }
        });
        $('#<%=Online_ManualRadioButton.ClientID %>').change(function () {
            $('#<%=txtGRN.ClientID %>').val('');
        });
        function NumberOnly(evt) {
                if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
                var parts = evt.srcElement.value.split('.');
                if (parts.length > 3) return false;
                if (evt.keyCode == 46) return (parts.length == 1);
                if (parts[0].length >= 14) return false;
                if (parts.length == 3 && parts[1].length >= 3) return false;
        }
        function myAlert(heading, mycontent) {
            swal(heading, mycontent);
        }
    </script>
    <script src="../js/Control.js" language="javascript" type="text/javascript"></script>
    <table style="width: 100%" align="center" id="MainTable">
        <tr>
            <td  align="center">
                 <asp:RadioButtonList runat="server" ID="Online_ManualRadioButton" Width="150px" RepeatDirection="Horizontal" ForeColor="#336699"  >
                    <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right:15px"/>
                    <asp:ListItem Text="Manual" Value="M"/>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <b><span style="color: #336699;font-size: 20px; text-align:center">GRN</span></b>&nbsp;
            </td>
        </tr>
        <tr style="height: 45px">
             <td align="center" colspan="1" style="font-size: larger;">
                <asp:TextBox ID="txtGRN" runat="server" Width="280px" Height="30px" AutoComplete="Off" Style="font-size: xx-large  ! important;" MaxLength="30" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGRN"
                    SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnVerify" runat="server" Text="Verify" ValidationGroup="de" Height="30px" Width="120px" OnClick="btnVerify_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="de" Height="30px" Width="120px" OnClick="btnReset_Click" />
            </td>
            <td align="center">
                
            </td>
        </tr>
        <tr runat="server" id="trStatus" visible="false">
            <td colspan="3">
                <center>
                    <br /><br /><br /><br /><br /><br />
                    <asp:Label Text="" ID="lblStatus" Style="font-size:xx-large; color:blue" runat="server" />
                </center>
            </td>
        </tr>
    </table>
</asp:Content>

