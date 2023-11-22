<%@ Page Title="Egras.Rajasthan.gov.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgBankScrollDetail.aspx.cs" Inherits="WebPages_Reports_EgBankScrollDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <legend style="color: #336699; font-weight: bold">Transaction-Detail</legend>
                <table style="width: 100%" align="center" id="MainTable">
                    <tr style="height: 40px">
                        <td align="center" colspan="2" style="width: 500px">
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Transaction
                                Date:-</span></b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtDate" runat="server" Width="80px" onkeypress="Javascript:return NumberOnly(event)"
                                CssClass="borderRadius inputDesign"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calDate" runat="server" PopupButtonID="txtDate"
                                Format="dd/MM/yyyy" TargetControlID="txtDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtDate"
                                SetFocusOnError="true" ErrorMessage="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2" align="left" style="width: 300px">
                            <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnshow_Click" />
                        </td>
                    </tr>
                    <tr id="trTrans" runat="server" visible="false" style="height: 40px">
                        <td colspan="4" align="center">
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Total Transaction:-</span></b>&nbsp;
                            <asp:Label ID="lblTrans" runat="server" Text="TotalTransaction" ForeColor="Green"
                                Font-Bold="true"></asp:Label>
                            <%-- </td>
                        <td colspan="2" align="left" style="width: 400px">--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><span style="color: #336699; font-family: Arial CE;
                                font-size: 13px;"> Total Amount:-</span></b>&nbsp;
                            <asp:Label ID="lblAmount" runat="server" Text="TotalAmount" ForeColor="Green" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr id="trNoRec" runat="server" visible="false" style="height: 40px">
                        <td colspan="4" align="center">
                            <asp:Label ID="Label1" runat="server" Text="No Record Found.!" ForeColor="#336699"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
