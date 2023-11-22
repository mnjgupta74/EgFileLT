<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgSearchByIdentity.aspx.cs" Inherits="WebPages_Reports_EgSearchByIdentity" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../js/Control.js"></script>
    <link href="../../CSS/EgrasCss.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../../js/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/jquery-ui.js"></script>--%>
    <script type="text/javascript" src="../../JS/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <center>
        <style type="text/css">
            .btn-default {
                color: #f4f4f4;
                background-color: #428bca;
            }
        </style>
        <script type="text/css">
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
        </script>

        <div _ngcontent-c6="" class="tnHead minus2point5per">
            <h2 _ngcontent-c6="" title="Search Report" class="pull-left">
                <span _ngcontent-c6="" style="color: #FFF">Search Report By Identity</span></h2>
            <img src="../../Image/help1.png" class="pull-right" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="right" title="Search Report By Identity" />
        </div>

        <div class="modal fade bd-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <b>Identity may contain TIN No/Lease No/Actt. No/Vehicle No/Tax-Id.</b>
                </div>
            </div>
        </div>
        <table cellpadding="0" cellspacing="0" border="1" align="center" style="border-style: solid; width: 100%; padding: 1px;">
            <tr>
                <td align="left">
                    <b><span style="color: #336699">Department Code : </span></b>&nbsp;
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="40%" AutoPostBack="true" class="form-control chzn-select"
                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="req4" runat="server" ControlToValidate="ddlDepartment"
                        SetFocusOnError="true" ErrorMessage="*" ValidationGroup="a" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
                <td align="left" colspan="2">

                    <b><span style="width: 200px; color: #336699;">Enter Identity : </span></b>
                    <asp:TextBox ID="txtSearchByIdentity" runat="server" MaxLength="12" CssClass="form-control" Style="width: 150px; display: initial !important;" Height="100%"></asp:TextBox>
                    <a type="button" data-toggle="modal" data-target=".bd-example-modal-sm">
                        <img src="../../Image/help.png" height="20px" /></a>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchByIdentity" ErrorMessage="*"
                        ForeColor="Red" ValidationGroup="a"></asp:RequiredFieldValidator>

                </td>

            </tr>
            <tr>
                <td align="left">
                    <b><span style="color: #336699;">From Date : </span></b>&nbsp;
                        <asp:TextBox ID="txtfromdate" runat="server" TabIndex="1" CssClass="form-control" Style="display: initial !important;" Width="120px" Height="100%"
                            onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txttodate)" onpaste="return false" placeholder="From Date..."></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    <span style="color: #336699">
                        <b>To Date :</b></span>
                    <asp:TextBox ID="txttodate" runat="server" TabIndex="2" CssClass="form-control" Style="display: initial !important;" Width="120px" Height="100%"
                        onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtfromdate)" onpaste="return false" placeholder="To Date..."></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="txttodate" TargetControlID="txttodate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                        CultureName="en-US" TargetControlID="txttodate" AcceptNegative="None" runat="server">
                    </ajaxToolkit:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="rfcTodate" runat="server" ControlToValidate="txtfromdate"
                        ErrorMessage="*" ValidationGroup="a"></asp:RequiredFieldValidator>

                </td>
                <td align="center">
                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="a" CssClass="btn btn-default" Width="100px" Height="40px" />
                </td>
            </tr>
            <tr>
            </tr>
            <tr id="msgLable" runat="server" visible="false">
                <td colspan="3" align="center" style="width: 100%; height: 50px;">
                    <asp:Label ID="lblSearchByIdentity" runat="server" Text="Search Report By Identity" ForeColor="#336699"
                        Font-Bold="True" Style="font-size: large;"></asp:Label>
                </td>
            </tr>
            <tr id="rptShow" runat="server" visible="false">
                <td colspan="3" style="width: 100%">
                    <center>
                        <table width="100%">
                            <asp:Repeater ID="RptSearch" runat="server">
                                <HeaderTemplate>
                                    <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center; height: 30px;">

                                        <td style="color: White; text-align: center;">GRN
                                        </td>
                                        <td style="color: White; text-align: center;">Identity
                                        </td>
                                        <td style="color: White; text-align: center">Remitter Name
                                        </td>
                                        <td style="color: White; text-align: center;">Office
                                        </td>
                                        <td style="color: White; text-align: center;">DepositDate
                                        </td>
                                        <td style="color: White; text-align: center">Amount
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #EFF3FB; height: 25px;">

                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblGrn" ForeColor="#336699" runat="server" Text='<%# Eval("GRN") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: center;">
                                            <asp:Label ID="lblIdentity" ForeColor="#336699" runat="server" Text='<%# Eval("Identity") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblFullName" ForeColor="#336699" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblOffice" ForeColor="#336699" runat="server" Text='<%# Eval("office") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: center;">
                                            <asp:Label ID="lblDepositeDate" ForeColor="#336699" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("depositedate")) %>'></asp:Label>
                                        </td>
                                        <td align="center" style="font-size: 15; text-align: right;">
                                            <asp:Label ID="lblAmount" ForeColor="#336699" runat="server" Text='<%# string.Format("{0:0.00}", Eval("Amount"))%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #ffffff; height: 25px; font-family: Arial">

                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblGrn" ForeColor="#336699" runat="server" Text='<%# Eval("GRN") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: center;">
                                            <asp:Label ID="lblIdentity" ForeColor="#336699" runat="server" Text='<%# Eval("Identity") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblFullName" ForeColor="#336699" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: left;">
                                            <asp:Label ID="lblOffice" ForeColor="#336699" runat="server" Text='<%# Eval("office") %>'></asp:Label>
                                        </td>
                                        <td style="font-size: 15; text-align: center;">
                                            <asp:Label ID="lblDepositeDate" ForeColor="#336699" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("depositedate")) %>'></asp:Label>
                                        </td>
                                        <td align="center" style="font-size: 15; text-align: right;">
                                            <asp:Label ID="lblAmount" ForeColor="#336699" runat="server" Text='<%# string.Format("{0:0.00}", Eval("Amount"))%>'></asp:Label>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>

                            </asp:Repeater>
                        </table>
                    </center>
                </td>
            </tr>
        </table>
        </fieldset>
    </center>
</asp:Content>
