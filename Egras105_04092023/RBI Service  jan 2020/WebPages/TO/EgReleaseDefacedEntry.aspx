<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgReleaseDefacedEntry.aspx.cs" Inherits="WebPages_TO_EgReleaseDefacedEntry"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../md5.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" src="../../js/checkNumber.js"></script>
    <script language="javascript" type="text/javascript">


        function NumberOnly(ctrl) {
            var ch;

            if (window.event) {
                ch = ctrl.keyCode;
            }
            else if (ctrl.which) {
                ch = ctrl.which;
            }
            if ((ch >= 48 && ch <= 57))
                return true;

            else if (ch == 8)
                return true;

            else if (ch == 9)
                return true;
            else
                return false;
        }
        function isStrongPassword() {
            var newpassword = document.getElementById("<%=txtSecurePass.ClientID %>").value;
            document.getElementById("<%=txtSecurePass.ClientID %>").value = hex_md5(newpassword);
            return true;
        }
    </script>
    <style>
        #ctl00_ContentPlaceHolder1_rblReleaseServiceType_0 {
            margin-left: -190%;
        }

        #ctl00_ContentPlaceHolder1_rblReleaseServiceType_1 {
            margin-left: -2%;
        }

        #ctl00_ContentPlaceHolder1_txtgrn {
            margin-left: -10%;
        }

        #ctl00_ContentPlaceHolder1_btnSearch {
            margin-left: -2%;
        }
    </style>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../Image/waiting_process.gif" />
                    <br />
                    <br />
                    The request is being processed. Please wait...<br />
                    This window will disappear when the process is finished.
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenTextAmt" runat="server" />
            <asp:HiddenField ID="hdnRefrenceno" runat="server" Value="0" />
            <fieldset id="Fieldset1" runat="server" width="60%">
                <legend style="color: #005CB8; font-size: small">Release-Defaced-Challan</legend>
                <table id="Table1" border="1" cellpadding="1" cellspacing="1" align="center" style="width: 100%">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:RadioButtonList runat="server" ID="rblReleaseType" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblReleaseType_SelectedIndexChanged">
                                <asp:ListItem Text="Release Defaced Amount" Value="0" Selected="True" />
                                <asp:ListItem Text="Release Refunded Amount" Value="1" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:RadioButtonList runat="server" ID="rblReleaseServiceType" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblReleaseServiceType_SelectedIndexChanged">
                                <asp:ListItem Text="Manual" Value="0" Selected="True" />
                                <asp:ListItem Text="Online" Value="1" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" runat="server" id="tdManual">
                            <asp:Label ID="lblGrn" runat="server" Text="GRN" GroupName="Radio" onclick="Radio_Click()" />
                            <asp:TextBox ID="txtgrn" runat="server" Width="120" MaxLength="12" TabIndex="1" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                            &nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_onClick" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnSearch"
                                PopupControlID="PanelPassword" CancelControlID="btnPasswordCancel" BackgroundCssClass="DivBackground">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="PanelPassword" runat="server" BackColor="White" Height="150px" Width="400px"
                                Style="display: none">
                                <div style="text-align: center; margin-top: 50px">
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">Enter Your Transaction Password:-</span></b>&nbsp;
                            <asp:TextBox ID="txtSecurePass" runat="server" TextMode="Password" MaxLength="32"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnSearch_onClick"
                                        OnClientClick="isStrongPassword();" />
                                    <asp:Button ID="btnPasswordCancel" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            <asp:GridView ID="grdprofile" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdprofile_RowDataBound"
                                Font-Names="Verdana" Font-Size="10pt" DataKeyNames="GRN" EmptyDataText="No Record Found"
                                ShowFooter="true" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                                EmptyDataRowStyle-VerticalAlign="Middle" CellPadding="4" ForeColor="#333333"
                                GridLines="None" OnRowCommand="grdprofile_RowCommand" Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="middle" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="Remitter Name">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Defaced Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                   <%-- <asp:BoundField DataField="RefundAmount" HeaderText="Refund Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Release">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtn" ImageUrl="~/Image/searchicon.jpg" runat="server" Width="20"
                                                Height="25" OnClick="imgbtn_Click" ToolTip="Partily" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataTemplate>
                                    No Data Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />
                            <asp:GridView ID="grdOnline" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="grdprofile_RowDataBound"
                                Font-Names="Verdana" Font-Size="10pt" DataKeyNames="GRN" EmptyDataText="No Record Found"
                                ShowFooter="true" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                                EmptyDataRowStyle-VerticalAlign="Middle" CellPadding="4" ForeColor="#333333"
                                GridLines="None" OnRowCommand="grdprofile_RowCommand" Visible="false">
                                <Columns>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="middle" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="Remitter Name">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Defaced Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RefundAmount" HeaderText="Refund Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Release">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtn" ImageUrl="~/Image/searchicon.jpg" runat="server" Width="20"
                                                Height="25" OnClick="imgbtn_Click" ToolTip="Partily" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ReferenceNo" HeaderText="Refrence No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataTemplate>
                                    No Data Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="470px"
                Style="display: none">
                <table width="100%" style="border: Solid 3px #D55500; width: 100%; height: 100%"
                    cellpadding="0" cellspacing="0">
                    <tr style="background-color: #D55500">
                        <td colspan="2" style="height: 10%; color: White; font-weight: bold; font-size: larger"
                            align="center">Details
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Grn Number : &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblGRN_pop" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Total Amount: &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Defaced Amount : &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lbllastDeface" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Refunded Amount : &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblRefundedAmt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Amount Can Be Released : &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblRemaining" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 45%">Release Amount: &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" onChange="javascript:return DecimalNumber(this,ctl00_ContentPlaceHolder1_HiddenTextAmt);" />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAmount"
                                Type="double" ErrorMessage="Amount must be between 0 and 999999999" MaximumValue="999999999"
                                MinimumValue="0" ValidationGroup="a"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" ValidationGroup="a"
                                OnClientClick="javascript:CheckNull();" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
