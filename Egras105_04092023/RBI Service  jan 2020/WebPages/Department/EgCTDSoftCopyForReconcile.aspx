<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgCTDSoftCopyForReconcile.aspx.cs" Inherits="WebPages_Department_EgCTDSoftCopyForReconcile"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $('#<%= rblSearchType.ClientID %>')
        $(document).ready(function() {
            $('#<%= rblSearchType.ClientID %>').click(function() {
                var rblTypeOfBusiness = $('#<%=rblSearchType.ClientID %> input:checked').val();
                ShowHide(rblTypeOfBusiness);
            });
        });
        function ShowHide(rblTypeOfBusiness) {       
            if (rblTypeOfBusiness == "M") {
                document.getElementById("<%= tdSlab.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<%= tdSlab.ClientID %>").style.display = "block";
            }           
        }
    </script>
        <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>

    <div>
        <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="TY-33">
                    <span _ngcontent-c6="" style="color: #FFF">CTD Slab Wise Soft Copy</span></h2>
               <img src="../../Image/help1.png" style="height: 35px;width: 34px;" data-toggle="tooltip" data-placement="left" Title="CTD Slab Wise Soft Copy" />
            </div>
        <table class="style1" align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
            <%--<tr>
                <td colspan="2" style="font-weight: 700; text-align: center; height: 49px">
                    <center>
                        <b>CTD Slab Wise Soft Copy</b></center>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2" align="center">
                    <asp:RadioButtonList ID="rblSearchType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="E" Selected="True">Online</asp:ListItem>
                        <asp:ListItem Value="M">Manual</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 400px" align="center">
                    <b>Date</b> :<asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txtDate"
                        ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 400px" align="center" runat="server" id="tdSlab">
                    <b>Slab:</b> :
                    <asp:DropDownList ID="ddlSlab" runat="server" CssClass="borderRadius inputDesign"
                        Width="120px">
                        <asp:ListItem Value="-1" Text="--Select Slab--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="0"></asp:ListItem>
                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                        ErrorMessage="Select Slab!" InitialValue="-1" ControlToValidate="ddlSlab" ValidationGroup="vldInsert"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 168px" colspan="2" align="center">
                    &nbsp;
                    <asp:Button ID="Click" runat="server" Text="Submit" ValidationGroup="vldInsert" OnClick="Click_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
