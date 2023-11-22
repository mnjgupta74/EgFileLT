<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgPdAccForDMFT.aspx.cs" Inherits="WebPages_Admin_EgPdAccForDMFT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .PnlDesign {
            border: solid 1px #000000;
            height: 100px;
            width: 450px;
            overflow-y: scroll;
            background-color: aliceblue;
            font-size: 15px;
            font-family: Arial;
            overflow-x: hidden;
        }

        .txtbox {
            background: url("../../Image/dropdown.png");
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        function NumericOnly(el) {
            var ex = /^\s*\d+\s*$/;
            if (ex.test(el.value) === false) {
                alert('Enter Number Only');
                el.value = "";
            }
        }
    </script>
    <div id="divProfileOnOff" style="width: 1127px">
        <fieldset runat="server" id="Division" style="width: 1000px; margin-left: 100px">
            <span id="spanPD" runat="server">
                <legend style="color: #336699; font-weight: bold">PD Account Active/Deactive for DMFT</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; max-height: 350px; margin-top: 15px" align="center">
                <table style="width: 995px">
                    <tr>
                        <td>
                            <b><span style="color: #336699">Budget Head :-</span></b>&nbsp;
                            <asp:TextBox ID="txtMajorHead" MaxLength="4" runat="server" onChange="NumericOnly(this);"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="9999-99-999-99-99"
                                MaskType="None" CultureName="en-US" TargetControlID="txtMajorHead" AcceptNegative="None"
                                runat="server">
                            </ajaxToolkit:MaskedEditExtender>
                        </td>

                        <td>Treasury:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="drpTreasury" runat="server" Width="200px" AutoPostBack="true" class="chzn-select" OnSelectedIndexChanged="drpTreasury_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        
                    </tr>

                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblsel" runat="server" Visible="false">All PD Account:</asp:Label></b>
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblrem" runat="server" Visible="false">Active for DMFT:</asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="PanelSelected" runat="server" CssClass="PnlDesign" Visible="false">
                                <asp:CheckBoxList ID="chksel" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="chksel_SelectedIndexChanged"></asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td colspan="4">
                            <asp:Panel ID="PanelRemove" runat="server" CssClass="PnlDesign" Visible="false" Style="background-color: darkseagreen">
                                <asp:ListBox ID="lstRemove" runat="server" Style="width: 450px; background-color: darkseagreen; border: none; height: inherit" Visible="false"></asp:ListBox>
                            </asp:Panel>
                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" Width="100px" Visible="false" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
