<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgGRNSearch.aspx.cs" Inherits="WebPages_Reports_EgGRNSearch" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../../CSS/EgrasCss.css" rel="stylesheet" type="text/css" />
    <link href="../../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="../../js/SweetAlert/sweetalert.min.js"></script>

    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/Control.js"></script>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <%--    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });
    </script>--%>
    <style type="text/css">
        .sweet-alert {
            left: 45%;
        }

        .chosen-container-single .chosen-single {
            width: 120%;
        }
    </style>
    <script type="text/javascript" language="javascript">

        var msg = "Dear Remitter, " + "\n \n" + "\
                   " + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + "Transaction Facility has been closed from Guest Login.  \
                     Please register yourself." + " \n\n" + "\
                   " + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + " Guest Login से Transaction सुविधा बंद कर दी गयी है ।";
        // msg
        var head = "";
        function GuestPopUp() {
            swal(head, msg);
        }

    </script>

    <center>
        <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="GRN Search Report" class="pull-left">
                <span _ngcontent-c6="" class="pull-right" style="color: #FFF">GRN Search Report</span></h2>
        </div>
        <table cellpadding="0" cellspacing="0" border="1" align="center" style="border-style: solid; width: 100%">
            <%--<tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblGrnSearch" runat="server" Text="GRN Search Report" ForeColor="#336699"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td style="width: 40%; padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;"><b>Departmentcode:</b></span></br>
                    <asp:DropDownList ID="ddldepartment" runat="server" Width="200 px" AutoPostBack="true" class="chzn-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="ddldepartment"
                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td style="padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;"><b>Treasurycode:</b></span></br>
                    <asp:DropDownList ID="ddltreasury" runat="server" Width="200 px" AutoPostBack="true" class="chzn-select"
                        OnSelectedIndexChanged="ddltreasury_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rftreasury" runat="server" ControlToValidate="ddltreasury"
                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" style="padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;"><b>Office Name:</b></span></br>
                    <asp:DropDownList ID="ddloffice" runat="server" Width="200 px" class="chzn-select">
                        <asp:ListItem Value="-1" Text="--Select Office Name--" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvoffice" runat="server" ControlToValidate="ddloffice"
                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        <b>GRN :</b></span></br>
                    <asp:TextBox ID="txtGRN" runat="server" MaxLength="12" Width="50%" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRN" ErrorMessage="*"
                        ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td style="padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        <b>Bank :</b></span></br>
                    <asp:DropDownList ID="ddlbankname" runat="server" Width="150 px" class="chzn-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlbankname"
                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td style="padding-bottom: 1%; padding-left: 1%;">
                    <span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        <b>Amount:</b></span></br>
                    <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Width="50%" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount"
                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" style="padding: 10px;">
                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="a" />
                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />

                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <center>
                        <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" Width="100%"
                            AutoGenerateColumns="false" EmptyDataText="NO Record Found !!" OnRowDataBound="GridView1_OnRowDataBound"
                            DataKeyNames="GRN">
                            <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#336699"></HeaderStyle>
                            <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField HeaderText="GRN" DataField="GRN" SortExpression="GRN" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField HeaderText="Registration Name" DataField="RegName" ItemStyle-HorizontalAlign="Left"
                                    SortExpression="RegName"></asp:BoundField>
                                <asp:BoundField HeaderText="Remitter Name" DataField="RemitterName" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField HeaderText="Amount" DataField="TotalAmount" SortExpression="TotalAmount"
                                    DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <center>
                                            <asp:ImageButton ID="ImageViewbtn" runat="server" ImageUrl="~/Image/view.png" CommandName="View"
                                                Width="60px" Height="20" ToolTip="View Details" Enabled="false" CommandArgument='<%#Eval("GRN")%>' /></center>
                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("Status") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </center>
                </td>
            </tr>
        </table>

    </center>
</asp:Content>
