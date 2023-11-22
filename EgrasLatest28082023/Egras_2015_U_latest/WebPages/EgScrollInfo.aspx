<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgScrollInfo.aspx.cs" Inherits="WebPages_EgScrollInfo" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;

            if (evt.keyCode == 46) return (parts.length == 1);

            if (parts[0].length >= 14) return false;

            if (parts.length == 3 && parts[1].length >= 3) return false;
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
            <table style="width: 60%" align="center">
               <tr>
              <td colspan="4" style="text-align: center; height: 35px" valign="top" >
                  <asp:Label ID="Labelheader" runat="server" Text="Bank Scroll Data" Font-Bold="True" ForeColor="#009900"
                      style="text-decoration:underline; "></asp:Label>
                    
              </td>
            </tr>
               
                <tr>
                    <td>
                        <b>Date:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" Width="150px" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtDate"
                            Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtDate"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <b>Select Bank:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbankname" runat="server" Width="180px" CssClass="borderRadius inputDesign">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="ddlbankname"
                            SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de" InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    <br />
                    <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <fieldset>
                            <legend style="color: Green;">Mismatched Records</legend>
                            <asp:GridView ID="grdScroll" runat="server" AutoGenerateColumns="False" Width="60%"
                                Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Recored Found" EmptyDataRowStyle-Font-Bold="true"
                                EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
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
                            </asp:GridView>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
