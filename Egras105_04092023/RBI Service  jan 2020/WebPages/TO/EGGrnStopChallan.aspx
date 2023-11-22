<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EGGrnStopChallan.aspx.cs" Inherits="WebPages_EGGrnStopChallan" Title="Untitled Page" %>

<%@ Register Src="~/UserControls/Online_ManualRadioButton.ascx" TagPrefix="uc1" TagName="Online_ManualRadioButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../js/Control.js"></script>

    <script type="text/javascript">
        function NumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
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
                    <img src="../../App_Themes/Images/progress.gif" />
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
            <div style="text-align: center">
                <fieldset runat="server" id="fieldamount">
                    <legend style="color: #336699; font-weight: bold; font-size: 15px;">Stop OR Release GRN to Challan Generate</legend>
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="90%" align="center">
                        <tr style="height: 45px;">
                            <td colspan="3" align="center">
                                <asp:RadioButtonList runat="server" ID="rdl_Active_Deactive" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdl_Active_Deactive_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="StopToChallanGenerate" Value="S" Selected="True" style="margin-right: 15px; font-weight: bold; font-size: 15px;" />
                                    <asp:ListItem Text="ReleaseStoppedGRN" Value="R" style="font-weight: bold; font-size: 15px;" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="height: 45px" id="tr_GrnStopChallan" runat="server">
                            <td>
                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">GRN : </span></b>&nbsp;
                                <asp:TextBox ID="txtGrn" runat="server" MaxLength="20" Width="150px" Style="font-size: 20px; height: 25px;" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFromd" runat="server" ControlToValidate="txtGrn"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="padding-left: 17%; text-align: left;">
                                <uc1:Online_ManualRadioButton runat="server" ID="Online_ManualRadioButton" />
                                <asp:Button ID="btn_show" runat="server" Text="Show" OnClick="btn_show_Click" Style="float: right; width: 100px; height: 35px;" ValidationGroup="de" />
                            </td>
                        </tr>
                        <tr style="height: 45px" id="tr_GrnRelease" runat="server" visible="false">
                            <td>
                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">From Date : </span></b>&nbsp;
                                <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Style="font-size: 20px; height: 25px;" Width="120px" onkeypress="Javascript:return NumberOnly(event)"
                                    onChange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtFromDate"
                                    Format="dd/MM/yyyy" TargetControlID="txtfromdate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 15px;">To Date : </span></b>&nbsp;
                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Style="font-size: 20px; height: 25px;" Width="120px" onkeypress="Javascript:return NumberOnly(event)"
                                    onChange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                    Format="dd/MM/yyyy" TargetControlID="txttodate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="reqTod" runat="server" ControlToValidate="txtToDate"
                                    ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <b><span style="color: #336699; font-family: Arial CE; font-size: 15px;">Select Bank
                                    :</span></b>&nbsp;
                                <asp:DropDownList ID="ddlBank" runat="server" Width="200px" Style="float: right;" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcbank" runat="server" ControlToValidate="ddlBank"
                                    ErrorMessage="*" InitialValue="0" SetFocusOnError="True" ValidationGroup="de"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Show" OnClick="btn_show_Click" Style="float: right; width: 100px; height: 35px;" ValidationGroup="de" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3" style="height: 35px" align="center">
                                <asp:Label ID="lblamount" runat="server" Text="Label1" Visible="false" ForeColor="#336699"
                                    Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 26px" align="right">
                                <asp:Button ID="brnActiveDactive" runat="server" Text="Activate/DeActivate" Style="width: 100px; height: 35px; margin-bottom: 10px;" OnClick="brnActiveDactive_Click" Visible="false" />
                            </td>
                        </tr>
                        <tr align="center" id="tr_ShowRecord" runat="server" visible="false">
                            <td colspan="3" style="height: 35px">
                                <asp:GridView ID="grdGrnChallan" runat="server" DataKeyNames="GRN,Amount,BankCode" AutoGenerateColumns="False" AllowPaging="true"
                                    CellPadding="4" Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Record Found"
                                    EmptyDataRowStyle-Font-Bold="true" EditRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                                    EmptyDataRowStyle-VerticalAlign="Middle" Style="font-size: x-small" ShowFooter="True"
                                    ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="grdGrnChallan_RowDataBound" PageSize="20">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle ForeColor="#336699" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S No" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GRN" HeaderText="GRN">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BankName" ControlStyle-Font-Bold="true"
                                            HeaderText="Bank Name">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ControlStyle BackColor="AliceBlue" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BankChallanDate" ControlStyle-Font-Bold="true" DataFormatString = "{0:dd/MM/yyyy}" 
                                            HeaderText="Bank Date">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ControlStyle BackColor="AliceBlue" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Flag" ControlStyle-Font-Bold="true"
                                            HeaderText="Status">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ControlStyle BackColor="AliceBlue" Font-Bold="True" />
                                        </asp:BoundField>
                                        <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkbox" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" Text="Select ALL" OnCheckedChanged="chkAll_CheckedChanged"
                                                    AutoPostBack="True"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkbox" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
