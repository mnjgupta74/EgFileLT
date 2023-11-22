<%@ Page Title="Egras.Raj.Nic.in" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgAddGroupSubHeadHindiName.aspx.cs" Inherits="WebPages_Admin_EgAddGroupSubHeadHindiName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $('#<%= rblType.ClientID %>')
        $(document).ready(function() {
            $('#<%= rblType.ClientID %>').click(function() {
                var rblTypeOfBusiness = $('#<%=rblType.ClientID %> input:checked').val();
                ShowHide(rblTypeOfBusiness);
            });
        });
        function ShowHide(rblTypeOfBusiness) {

            if (rblTypeOfBusiness == 1) {
                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "none";

            }
            else {
                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "block";
            }
            document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            document.getElementById("<%= DivHeadlist.ClientID %>").style.display = "none";

        }

        function CheckMajorHeadlength() {
            var Mvalue = document.getElementById("<%= txtBudgetHead.ClientID %>").value
            if (Mvalue.length < 1) {
                alert('Please Enter BudgetHead/MajorHead.!');
                document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            }

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function() {
            $('#<%= rblType.ClientID %>').click(function() {
                var rblTypeOfBusiness = $('#<%=rblType.ClientID %> input:checked').val();
                ShowHide(rblTypeOfBusiness);
            });
        });
        function ShowHide(rblTypeOfBusiness) {

            if (rblTypeOfBusiness == 1) {

                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "none";

            }
            else {

                document.getElementById("<%= DivBudgetText.ClientID %>").style.display = "block";
            }
            document.getElementById("<%= txtBudgetHead.ClientID %>").value = "";
            document.getElementById("<%= DivHeadlist.ClientID %>").style.display = "none";

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
            <div style="text-align: center">
                <fieldset id="fieldamount" class="fieldset" runat="server" style="width: 900px; margin-left: 100px;">
                    <legend style="color: #005CB8; font-size: medium">Group-Sub Head Name</legend>
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                        <tr style="height: 50px;">
                            <td align="center">
                                <asp:RadioButtonList runat="server" ID="rblType" RepeatDirection="Horizontal" ForeColor="#336699"
                                    Font-Bold="true">
                                    <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="MajorHead/BudgetHead" Value="2" Selected="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <div id="DivBudgetText" style="display: none" runat="server">
                                    <asp:TextBox ID="txtBudgetHead" runat="server" MaxLength="13" onblur="javascript:CheckMajorHeadlength()"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                                        MaskType="None" CultureName="en-US" TargetControlID="txtBudgetHead" AcceptNegative="None"
                                        runat="server">
                                    </ajaxToolkit:MaskedEditExtender>
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 400px;" align="center">
                                <div id="DivHeadlist" style="margin-right: 10px; height: 380px; overflow: auto; display: block"
                                    runat="server">
                                    <asp:CheckBoxList ID="chkhead" runat="server" ForeColor="#336699" OnSelectedIndexChanged="chkhead_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="width: 500px; vertical-align: top;">
                                <table id="trheadDetails" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <b><span style="color: #336699;">BudgetHead:</span></b> &nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblhead" runat="server" Text="Head" ForeColor="#336699" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b><span style="color: #336699;">HeadName:</span></b> &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqFromd" runat="server" ControlToValidate="txtName"
                                                ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" ValidationGroup="de" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
