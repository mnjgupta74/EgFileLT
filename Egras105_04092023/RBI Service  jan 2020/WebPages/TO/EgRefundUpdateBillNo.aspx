<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgRefundUpdateBillNo.aspx.cs" Inherits="WebPages_EgRefundUpdateBillNo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        th {
            margin: 0 0 0 0;
            background-color: #F7F6F3;
        }
    </style>

    <script type="text/javascript">
        function PwdEnable() {
            var GRN = $('#ctl00_ContentPlaceHolder1_txtgrn').val();
            if (GRN == null || GRN == "" || GRN.length > 12) {
                alert("Please Enter correct GRN");
            }
            else {
                var GRN = document.getElementById("#ctl00_ContentPlaceHolder1_txtgrn")
                var GRNAtt = GRN.getAttribute("disabled");
                if (GRNAtt != "disabled") {
                    $('#PanelTableShow').show();
                }
            }
        }
    </script>

    <%--==================CSS-JQUERY LOADER==================--%>
    <style type="text/css">
        #cover-spin {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            display: none;
        }

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        #cover-spin::after {
            content: '';
            display: block;
            position: absolute;
            left: 48%;
            top: 40%;
            width: 40px;
            height: 40px;
            border-style: solid;
            border-color: black;
            border-top-color: transparent;
            border-width: 4px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
    </style>

    <div id="cover-spin"></div>
    <script type="text/javascript">
        function NumberOnly(evt) {
            //if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            //var parts = evt.srcElement.value.split('.');
            //if (parts.length > 3) return false;

            //if (evt.keyCode == 46) return (parts.length == 1);

            //if (parts[0].length >= 14) return false;

            //if (parts.length == 3 && parts[1].length >= 3) return false;
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day after today!");
            }

        }
        $(document).ready(function () {
            $('[name*="ctl00$ContentPlaceHolder1$btnSearch"]').click(function () {
                if ($(<%=txtgrn.ClientID%>).val() != "") {
                    $('#cover-spin').show(0)

                }
            });
        });
    </script>

    <%--==============END CSS - JQUERY LOADER============--%>


    <fieldset runat="server" id="lstrecord" style="width: 1000px;">
        <asp:HiddenField ID="hdnRnd" runat="server" />
        <legend style="color: #336699; height: 100%; font-weight: bold">Edit Refund Amount Bill Number</legend>
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12" style="text-align: center; padding-bottom: 40px">
                    GRN : &nbsp;
                                <asp:TextBox ID="txtgrn" runat="server" Height="30px" type="number" MaxLength="12" onkeypress="Javascript:return NumberOnly(event)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" CommandName="search" runat="server" Text="Submit" OnClientClick="PwdEnable(); return false;"
                        OnClick="btnSearch_Click"  Height="30px" width= "10%" style="font-size: 15px;" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" CommandName="search" runat="server" Text="Reset" style="font-size: 15px;"  Height="30px" width="10%"
                        OnClick="btnReset_Click" />

                    <br />
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblRemitterName" Font-Bold="true" Visible="false" runat="server" Text=""></asp:Label>
            </div>
    </fieldset>
    <fieldset runat="server" id="PanelTableShow" style="width: 1000px;" visible="false">
        <div class="row" id="PanelTable" runat="server" float="center" align="center">
            <%--style="text-align: center; box-sizing:border-box; object-fit:cover; height:100%; width:500px; padding:10px; border: 2px solid black">--%>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-12">

                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <table cellspacing="0" rules="all" border="1" width="100%">
                                    <tr>
                                        <%--<th scope="col" style="width: 120px;padding: 10px 10px 10px 10px;">Remitter Name
                                    </th>--%>
                                        <th scope="col" style="width: 120px; padding: 10px 10px 10px 10px;">BillNo
                                        </th>
                                        <th scope="col" style="width: 120px; padding: 10px 10px 10px 10px;">Date
                                        </th>
                                        <th scope="col" style="width: 120px; padding: 10px 10px 10px 10px;">Total Amount 
                                        </th>
                                        <th scope="col" style="width: 120px; padding: 10px 10px 10px 10px;">Deface Amount 
                                        </th>
                                        <th scope="col" style="width: 120px; padding: 10px 10px 10px 10px;">Actions
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <%-- <td>
                                    <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>' Visible="false" />
                                    <asp:Label ID="lblName" runat="server"  Text='<%# Eval("RemitterName") %>' />

                                </td>--%>
                                    <td>
                                        <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>' Visible="false" />
                                        <asp:Label ID="lblBillNo" Style="text-align: center; font-weight: bold; margin-left: 51px" runat="server" Text='<%# Eval("BillNo") %>' />
                                        <asp:TextBox ID="txtBillNo" runat="server" Width="120" Style="margin-left: 51px; font-weight: bold" Text='<%# Eval("BillNo") %>'
                                            Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Style="margin-left: 20px; font-weight: bold" Text='<%# Eval("Billdate", "{0:d}") %>' />
                                        <asp:TextBox ID="txtDate" runat="server" Style="margin-left: 20px; font-weight: bold" Width="120" Text='<%# Eval("Billdate", "{0:d}") %>'
                                            Visible="false" />
                                        <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtDate"
                                            Format="dd/MM/yyyy" TargetControlID="txtDate" OnClientDateSelectionChanged="checkDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                                            CultureName="en-US" TargetControlID="txtDate" AcceptNegative="None" runat="server">
                                        </ajaxToolkit:MaskedEditExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotalAmount" runat="server" Style="margin-left: 20px;" Text='<%# Eval("Amount") %>' />

                                    </td>
                                    <td>
                                        <asp:Label ID="lblDefaceAmount" runat="server" Style="margin-left: 20px;" Text='<%# Eval("DefaceAmount") %>' />
                                        <asp:TextBox ID="txtDefaceAmount" runat="server" Style="margin-left: 20px; font-weight: bold" Width="120" Text='<%# Eval("DefaceAmount") %>'
                                            Visible="false" />
                                    </td>
                                    <td style="width: 180px;">
                                        <asp:Button ID="lnkEdit" Style="margin-left: 76px;" runat="server" Text="Edit"
                                            CommandName="deliver" CssClass="btn btn-primary" OnClick="OnEdit" />
                                        <asp:Button ID="lnkUpdate" Visible="false" Style="margin-left: 30px;" runat="server" Text="Update"
                                            CommandName="deliver" CssClass="btn btn-primary" OnClick="OnUpdate" />
                                        <asp:Button ID="lnkCancel" Visible="false" runat="server" Text="Cancel"
                                            CommandName="deliver" CssClass="btn btn-primary" OnClick="OnCancel" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
