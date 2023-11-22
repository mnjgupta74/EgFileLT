<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgTotalTransaction.aspx.cs" Inherits="WebPages_Reports_EgTotalTransaction"
    Title="EgTotalTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../CSS/styleDataList.css" rel="Stylesheet" type="text/css" />
    
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

    <fieldset runat="server" id="fieldamount" style="width: 950px; margin-left: 100px;">
        <legend style="color: #336699; font-weight: bold;">Total Transaction</legend>
        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="font-weight: bold; font-size: 13px; font-family: Arial CE; color: #336699;"
                    align="center">
                    From Date :
                </td>
                <td style="height: 45px; ">
                    <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="100px" Height="25px" onkeypress="Javascript:return NumberOnly(event)"
                       onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFromd" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                    <ajaxToolKit:CalendarExtender ID="calFromd" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                    </ajaxToolKit:CalendarExtender>
                </td>
                <td style="font-weight: bold; font-size: 13px; font-family: Arial CE; color: #336699;">
                    To Date :
                </td>
                <td style="height: 45px;">
                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="100px" Height="25px" onkeypress="Javascript:return NumberOnly(event)"
                         onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqTod" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" SetFocusOnError="true" ValidationGroup="de"></asp:RequiredFieldValidator>
                    <ajaxToolKit:CalendarExtender ID="calToD" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolKit:CalendarExtender>
                </td>
                <td style="height: 45px;">
                    <asp:Button ID="btn_show" runat="server" Text="Show" Width="100px"  Height="33px" CssClass="btn btn-default" OnClick="btn_show_Click" ValidationGroup="de" />
                </td>
            </tr>
            <%--  <tr>
                <td colspan="5">
                    <div>
                        <asp:DataList ID="DataListBankTransaction" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="2px" CellPadding="14" CellSpacing="9" Font-Names="Verdana"
                            Font-Size="Small" GridLines="None" RepeatColumns="3" RepeatDirection="Horizontal"
                            Width="100%">
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Large" ForeColor="White"
                                HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Bank Transaction Detail</HeaderTemplate>
                            <ItemStyle BackColor="#EFF3FB" ForeColor="Black" BorderWidth="1px" BorderColor="#507CD1"
                                BorderStyle="Outset" />
                            <ItemTemplate>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblCName" runat="server" ForeColor="#336699" Font-Bold="true" Text='<%# Eval("BankName") %>'
                                    Font-Underline="true"></asp:Label>
                                <br />
                                <br />
                                <b>Online-Challan:</b>
                                <asp:Label ID="lblOnline" runat="server" Text='<%# Eval("Online") %>'></asp:Label>
                                <br />
                                <br />
                                <b>Online Total Amount:</b>
                                <asp:Label ID="lblAmount" runat="server" Text='  <%# string.Format("{0:0.00}", Eval("Amount"))%>'></asp:Label>
                                <br />
                                <br />
                                <b>Manual-Challan:</b>
                                <asp:Label ID="lblManual" runat="server" Text='<%# Eval("Manual") %>'></asp:Label>
                                <br />
                                <br />
                                <b>Manual-Total Amount:</b>
                                <asp:Label ID="LabelMAmount" runat="server" Text=' <%# string.Format("{0:0.00}", Eval("MAmount"))%>'></asp:Label>
                                <br />
                                <br />
                            </ItemTemplate>
                            <AlternatingItemStyle BackColor="#ffffff" ForeColor="Black" BorderWidth="1px" BorderColor="#507CD1" />
                            <AlternatingItemTemplate>
                                <itemtemplate>
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Label ID="lblCName" runat="server" ForeColor="#336699" Font-Bold="true" Text='<%# Eval("BankName") %>' Font-Underline="true"></asp:Label>
                                <br />
                                <br />
                                <b>Online-Challan:</b>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Online") %>'></asp:Label>
                                <br />
                                <br />
                                <b>Online Total Amount:</b>
                                <asp:Label ID="lblCity" runat="server" Text='  <%# string.Format("{0:0.00}", Eval("Amount"))%>'></asp:Label>
                                <br />
                                <br />
                                <b>Manual-Challan:</b>
                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Manual") %>'></asp:Label>
                                <br />
                                <br />
                                <b>Manual-Total Amount:</b>
                                <asp:Label ID="Label1" runat="server" Text=' <%# string.Format("{0:0.00}", Eval("MAmount"))%>'></asp:Label>
                                <br />
                                <br />
                            </itemtemplate>
                            </AlternatingItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>--%>
            <tr style="height: 45px">
                <td colspan="5">
                    <div>
                        <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small"
                            RepeatColumns="3" RepeatDirection="Horizontal" Width="950px">
                            <ItemStyle BackColor="#EFF3FB" ForeColor="Black" BorderWidth="0px" BorderColor="#507CD1"
                                BorderStyle="Outset" />
                            <ItemTemplate>
                                <div id="DataListstyle">
                                    <ul id="plans">
                                        <li class="plan">
                                            <ul class="planContainer">
                                                <li class="title">
                                                    <h2>
                                                        <asp:Label Width="80%" ID="lblCName" runat="server" Text='<%# Eval("BankName") %>'
                                                            Style="font-size: medium" Font-Bold="true" Font-Underline="true"></asp:Label></h2>
                                                </li>
                                                <li>
                                                    <ul class="options">
                                                        <li><span style="color: Gray;"><b>Online Challan:</b>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Online") %>' ForeColor="Gray"
                                                                Font-Bold="true"></asp:Label></span></li>
                                                        <li><span style="color: Gray;"><b>Online Amount:</b>
                                                            <asp:Label ID="Label3" runat="server" Text='  <%# string.Format("{0:0.00}", Eval("Amount"))%>'
                                                                ForeColor="Gray" Font-Bold="true"></asp:Label></span></li>
                                                        <li><span><b>Manual Challan:</b>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Manual") %>' Font-Bold="true"></asp:Label></span></li>
                                                        <li><span><b>Manual Amount:</b>
                                                            <asp:Label ID="Label1" runat="server" Text=' <%# string.Format("{0:0.00}", Eval("MAmount"))%>'
                                                                Font-Bold="true"></asp:Label></span></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
