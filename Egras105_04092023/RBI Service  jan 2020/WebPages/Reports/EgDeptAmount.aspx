<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgDeptAmount.aspx.cs" Inherits="WebPages_Reports_EgDeptAmount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

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
    </script>
       <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
     <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">--%>
    <script src="../../js/bootstrap.min.js"></script>
    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div style="text-align: center">
        <%-- <table id="Tableheader" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="6" style="text-align: center; height: 25px" valign="top">
                    <asp:Label ID="Labelheader" runat="server" Text="Department wise Report" Font-Bold="True" ForeColor="#009900"
                        Style="text-decoration: underline;"></asp:Label>
                </td>
            </tr>
        </table>--%>
                    <div _ngcontent-c6="" class="tnHead minus2point5per">
                <h2 _ngcontent-c6="" title="TY-33">
                    <span _ngcontent-c6="" style="color: #FFF">Department-BudgetHeads Report </span></h2>
                <img src="../../Image/help1.png" style="height: 35px;width: 34px;" data-toggle="tooltip" data-toggle="tooltip" data-placement="left" Title="" />
            </div>
        <%--<fieldset runat="server" id="fieldamount" style="width: 950px; margin-left: 100px">
            <legend style="color: #336699; font-weight: bold">Department-BudgetHeads Report</legend>--%>
            <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
              <%--  <tr>
                    <td align="center" colspan="6" bgcolor="#66CCFF">
                        <asp:RadioButtonList ID="rblTransactionType" runat="server" RepeatDirection="Horizontal"
                            ForeColor="#006600" AutoPostBack="true" OnSelectedIndexChanged="rblTransactionType_SelectedIndexChanged"
                            Height="19px" Width="788px" Font-Bold="True">
                          <asp:ListItem Value="1" Text="Payment Initiative & Bank Response Received Date" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Bank Response Date"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
                <tr>
                    <td style="heigth: 45px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            From Date : </span></b>&nbsp;
                    </td>
                    <td style="height: 45px">
                        <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                             onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqFromd" runat="server" ControlToValidate="txtFromDate"
                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td style="heigth: 45px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            To Date : </span></b>&nbsp;
                    </td>
                    <td style="heigth: 45px">
                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                            onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqTod" runat="server" ControlToValidate="txtToDate"
                            ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <td style="heigth: 45px">
                        <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            Department Name : </span></b>&nbsp;
                    </td>
                    <td style="heigth: 45px">
                        &nbsp;&nbsp;<asp:DropDownList ID="ddldepartment" runat="server" Width="270px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldepartment"
                            InitialValue="0" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                    </td>
                    <%--  <td>
                    
                </td>--%>
                </tr>
                <tr>
                    <td colspan="6"  align="right">
                        <asp:Button ID="btn_show"   Width="120px"  runat="server" Text="Show" OnClick="btn_show_Click" ValidationGroup="de" />
                    </td>
                </tr>
                <%--  <tr align="center">
                    <td colspan="6">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#336699"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Verdana" Font-Size="10pt"
                            OnRowDataBound="GridView1_RowDataBound" ShowFooter="true" FooterStyle-BackColor="#336699"
                            Width="80%" EmptyDataText="No Record Found" EditRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                            EmptyDataRowStyle-VerticalAlign="Middle">
                            <Columns>
                                <asp:TemplateField HeaderText="S No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Budget Head" DataField="BudgetHead">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Purpose" DataField="Purpose">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="BankDate" DataField="BankDate">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataFormatString="{0:n}" HeaderText="Amount" DataField="Amount">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#336699" ForeColor="White" />
                        </asp:GridView>
                        <asp:Panel ID="Panel1" runat="server">
                        </asp:Panel>
                    </td>
                </tr>--%>
            </table>
            <table width="100%">
                <tr style="display: block;" id="trRevenue">
                    <td style="width: 500px; vertical-align: top;">
                        <div id="RptDiv" runat="server" visible="false" style="margin-right: 10px; height: 380px;
                            overflow: scroll">
                            <fieldset style="width: 330px;">
                                <legend style="color: #336699;">BudgetHead/Purpose Report</legend>
                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                    width: 400px; margin-left: 10px; padding-bottom: 10px; padding-top: 10px;">
                                    <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand" OnItemDataBound="rpt_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                height: 20px">
                                                <td style="color: White;">
                                                    Sr.No
                                                </td>
                                                <td style="color: White;">
                                                    Budget Head/Purpose
                                                </td>
                                                <td style="color: White; text-align: right">
                                                    Total Amount
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: #EFF3FB; height: 20px;">
                                                <td align="center" style="font-size: 15;">
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <td style="font-size: 15;">
                                                    <asp:LinkButton ID="lnkBudgethead" ForeColor="#336699" runat="server" Text='<%# Eval("BudgetHead") %>'
                                                        CommandArgument='<%# Eval("ScheCode") %>' CommandName="Show"></asp:LinkButton>
                                                </td>
                                                <td align="center" style="font-size: 15; text-align: right;">
                                                    <%# string.Format("{0:0.00}", Eval("TotalAmt"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                <td align="center" style="font-size: 15;">
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <td style="font-size: 15;">
                                                    <asp:LinkButton ID="lnkBudgethead" ForeColor="#336699" runat="server" Text='<%# Eval("BudgetHead") %>'
                                                        CommandArgument='<%# Eval("ScheCode") %>' CommandName="Show"></asp:LinkButton>
                                                </td>
                                                <td align="center" style="font-size: 15; text-align: right;">
                                                    <%# string.Format("{0:0.00}",Eval("TotalAmt")) %>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <b><span style="width: 400px; color: #336699">Total Head Amount:-</span></b>
                                                </td>
                                                <td align="right" height="40px">
                                                    <asp:Label ID="lblamt" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                    <td style="vertical-align: top; width: 400px">
                        <div id="rptSchemaDiv" runat="server" visible="false" style="margin-right: 10px;
                            height: 380px; overflow: scroll">
                            <fieldset style="width: 400px;">
                                <legend style="color: #336699;">DateWise :
                                    <asp:Label ID="lblBudgethead" runat="server" Visible="false" ForeColor="Green" Font-Underline="true"></asp:Label>
                                </legend>
                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                    width: 380px; margin-left: 10px; padding-bottom: 10px; padding-top: 10px;">
                                    <asp:Repeater ID="rptschema" runat="server" OnItemDataBound="rptschema_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                height: 20px">
                                                <td style="color: White; width: 10px">
                                                    Sr.No
                                                </td>
                                                <%-- <td style="color: White; text-align: left">
                                                    Purpose/BudgetHead
                                                </td>--%>
                                                <td style="color: White; text-align: center">
                                                    Date
                                                </td>
                                                <td style="color: White; text-align: right">
                                                    Total Amount
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: #EFF3FB; height: 20px;">
                                                <td align="center" style="font-size: 15; width: 10px">
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <%-- <td align="left" style="font-size: 15;">
                                                    <%# Eval("ScheCode") %>
                                                </td>--%>
                                                <td align="center" style="font-size: 15;">
                                                    <%# Eval("BankDate") %>
                                                </td>
                                                <td align="center" style="font-size: 15; text-align: right;">
                                                    <%# string.Format("{0:0.00}",Eval("TotAmt"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                <td align="center" style="font-size: 15; width: 10px">
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <%-- <td align="left" style="font-size: 15;">
                                                    <%# Eval("ScheCode") %>
                                                </td>--%>
                                                <td align="center" style="font-size: 15;">
                                                    <%# Eval("BankDate") %>
                                                </td>
                                                <td align="center" style="font-size: 15; text-align: right;">
                                                    <%# string.Format("{0:0.00}", Eval("TotAmt"))%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <tr>
                                                <td align="left" colspan="2">
                                                    <b><span style="width: 300px; color: #336699">Total Schema Amount:-</span></b>
                                                </td>
                                                <td align="right" height="40px">
                                                    <asp:Label ID="lblSchemaTotal" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                </td>
                                            </tr>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        <%--</fieldset>--%>
    </div>
    <ajaxToolKit:CalendarExtender ID="calFromd" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
    </ajaxToolKit:CalendarExtender>
    <ajaxToolKit:CalendarExtender ID="calToD" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
    </ajaxToolKit:CalendarExtender>
</asp:Content>
