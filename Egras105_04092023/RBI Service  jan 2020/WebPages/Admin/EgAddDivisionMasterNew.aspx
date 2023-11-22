<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgAddDivisionMasterNew.aspx.cs" Inherits="WebPages_Admin_EgAddDivisionMasterNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .ddlchklst {
            width: 170px;
            border: solid 1px silver;
        }

            .ddlchklst ul {
                margin: 0;
                padding: 0;
                border-top: solid 1px silver;
            }

            .ddlchklst li {
                list-style: none;
            }
    </style>
    <style type="text/css">
        .PnlDesign {
            border: solid 1px #000000;
            height: 100px;
            width: 450px;
            overflow-y: scroll;
            background-color: aliceblue;
            font-size: 15px;
            font-family: Arial;
        }

        .txtbox {
            background: url("../../Image/dropdown.png");
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
        }
        #ctl00_ContentPlaceHolder1_btnAdd {
        min-width:200px;
        }
    </style>

    <script type="text/javascript">
        $('input.chksel').click(function () {
            //$('#chksel').on('click', function () {
            
            if ($(this).is(':checked')) {
                // handle checkbox check
                alert($(this).val());
            } else {
                // checkbox is unchecked
                alert('unchecked')
            }
        });
        $('#chkRemove').on('click', function () {
            if ($(this).is(':checked')) {
                // handle checkbox check
                alert($(this).val());
            } else {
                // checkbox is unchecked
                alert('unchecked')
            }
        });
    </script>
    <div id="divPDDivisionMaster" style="width: 1127px">
        <fieldset runat="server" id="Division" style="width: 1000px; margin-left: 100px">
            <span id="spanPD" runat="server"><legend style="color: #336699; font-weight: bold">Add New Division-Master</legend>
            </span><span id="spanDiv" runat="server"></span>
            <div style="width: 100%; max-height: 350px; margin-top: 15px" align="center">
                <table style="width: 995px">
                    <tr>
                        <td colspan="2"  style="text-align:-webkit-center">
                            <asp:RadioButtonList ID="rbldivtype" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbldivtype_SelectedIndexChanged">
                                <asp:ListItem Text="Division" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="SubDivision" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    <tr>
                        <td>Treasury:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="drpTreasury" runat="server" Width="200px"></asp:DropDownList>
                        </td>
                        <td>Department:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:DropDownList ID="drpDepartment" runat="server" Width="322px" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trdrpdiv" runat="server" >
                        <td>Division Code:-&nbsp;
                            <asp:DropDownList ID="drpdivision" runat="server" Width="200px" OnSelectedIndexChanged="drpdivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trNewOffice" runat="server">
                        
                        <td colspan="2" style="text-align:right;"><asp:Button ID="btnAdd" runat="server" Text="Add New Office if Not Present" OnClick="btnAdd_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right" id="tdnewoffice" runat="server">Office Id:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddOffice" runat="server" placeholder="Enter your Office Id"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblofc" runat="server" Visible="false">Select Items to Add Division/SubDivision:</asp:Label></b>
                        </td>
                        <td>
                            <b>
                            <asp:Label ID="lblsel" runat="server" Visible="false">Selected Divisions:</asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="PanelSelected" runat="server" CssClass="PnlDesign" Visible="false">
                                <asp:CheckBoxList ID="chksel" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="chksel_SelectedIndexChanged"></asp:CheckBoxList>
                            </asp:Panel>
                        </td>
                        <td colspan="4">
                            <div id="PnlDesign">
                           <asp:ListBox ID="lstSelected" runat="server" Style="width: 450px; height:100px; background-color: antiquewhite" Visible="false"></asp:ListBox>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" Width="200px" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>