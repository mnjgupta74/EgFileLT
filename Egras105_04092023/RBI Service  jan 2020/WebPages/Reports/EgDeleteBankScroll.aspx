<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgDeleteBankScroll.aspx.cs" Inherits="WebPages_Reports_EgDeleteBankScroll"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../md5.js" type="text/javascript" language="javascript"></script>


    <script type="text/javascript">
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

            else
                return false;
        }
        function dateValidation() {
            var dtObj = document.getElementById("<%=txtDate.ClientID %>")

            var dtStr = dtObj.value
            var dtTemp = dtStr


            if (dtStr == '') {
                alert('Date cant be blank')
                dtObj.value = ""
                return false
            }
            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }

            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)

            if (dtTemp.indexOf('/') == -1) {
                alert('Invalid Date.\nPlease enter date in dd/mm/yyyy format.')
                dtObj.value = ""
                return false
            }

            var DayDt
            var MonDt
            var YearDt

            dtTemp = dtStr
            DayDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            dtTemp = dtTemp.substring(dtTemp.indexOf('/') + 1)
            MonDt = dtTemp.substring(0, dtTemp.indexOf('/'))
            YearDt = dtTemp.substring(dtTemp.indexOf('/') + 1)
            if (YearDt.length != 4) {
                alert('Invalid Date.Year should be in 4-digits.')
                dtObj.value = ""
                return false
            }


            if (isNaN(DayDt) || isNaN(MonDt) || isNaN(YearDt)) {
                alert("Invalid Date.\nPlease enter date in dd/mm/yyyy format.")
                dtObj.value = ""
                return false
            }

            var str2 = new Date();
            var s = str2.format("dd/MM/yyyy");
            var dt1 = parseInt(dtStr.substring(0, 2), 10);
            var mon1 = parseInt(dtStr.substring(3, 5), 10);
            var yr1 = parseInt(dtStr.substring(6, 10), 10);
            var dt2 = parseInt(s.substring(0, 2), 10);
            var mon2 = parseInt(s.substring(3, 5), 10);
            var yr2 = parseInt(s.substring(6, 10), 10);

            var date1 = (mon1 + '/' + dt1 + '/' + yr1);
            var date2 = (mon2 + '/' + dt2 + '/' + yr2);
            if (Date.parse(date2) < Date.parse(date1)) {
                alert("Entered Date cannot be greater than current date");
                dtObj.value = ""
                return false
            }
        }

        function Warning1() {

            var re = confirm("Are you sure want to delete this scroll ?");
            return re;
        }
        function isStrongPassword() {
            var newpassword = document.getElementById("<%=txtSecurePass.ClientID %>").value;
            document.getElementById("<%=txtSecurePass.ClientID %>").value = hex_md5(newpassword);
            return true;
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" />
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
            <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Datewise Bank-Scroll Report</legend>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 40px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Date:-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" MaxLength="10" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                                onChange="javascript:return dateValidation()"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDate"
                                Format="dd/MM/yyyy" TargetControlID="txtDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                         <td >
                            <asp:RadioButtonList runat="server" ID="Online_ManualRadioButton" Width="130px" RepeatDirection="Horizontal"  ForeColor="#336699">
                                <asp:ListItem Text="Online" Value="N" Selected="True" style="margin-right: 15px" />
                                <asp:ListItem Text="Manual" Value="M" />
                            </asp:RadioButtonList>
              
                         </td>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">
                                Select Bank:-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlbankname" runat="server" Width="220px" CssClass="borderRadius inputDesign">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="5">
                            <asp:Button ID="btndelete" runat="server" Text="Delete Scroll Data" Visible="false" /><%--OnClientClick="Javascript:return  Warning1()"--%>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btndelete"
                                PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="DivBackground">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px"
                                Style="display: none">
                                <div style="text-align: center; margin-top: 50px">
                                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px">
                                        Enter Your Transaction Password:-</span></b>&nbsp;
                                    <asp:TextBox ID="txtSecurePass" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btndelete_Click"
                                        OnClientClick="isStrongPassword();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="5">
                            <asp:GridView ID="grdScroll" runat="server" AutoGenerateColumns="False" Width="100%"
                                Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Record Found" EmptyDataRowStyle-Font-Bold="true"
                                EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                CellPadding="4" GridLines="None" OnPageIndexChanging="grdScroll_PageIndexChanging"
                                AllowPaging="True" PageSize="25" ForeColor="#333333">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CIN" HeaderText="CIN">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ref" HeaderText="Ref">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Bank Amount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
