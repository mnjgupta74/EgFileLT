<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgCtdZoneWiseRpt.aspx.cs" Inherits="WebPages_Reports_EgCtdZoneWiseRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../js/Control.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function NumberOnly(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 3) return false;

            if (evt.keyCode == 46) return (parts.length == 1);

            if (parts[0].length >= 14) return false;

            if (parts.length == 3 && parts[1].length >= 3) return false;
        }   
    </script>

    <center>
        <fieldset runat="server" id="fieldamount" style="width: 90%;">
            <legend style="color: #336699; font-weight: bold">Zone Wise Report</legend>
            <div id="divCtdDataSelection" style="width: 100%; height: 400px;">
                <table style="margin-left: 1px">
                    <tr align="left">
                        <td>
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">From Date :
                            </span></b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                AutoCompleteType="None"  onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)" >></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" PopupButtonID="txtFromDate"
                                Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="ref1" runat="server" ControlToValidate="txtFromDate"
                                EnableClientScript="true" ErrorMessage="Please select date" SetFocusOnError="true"
                                Text="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">To Date : </span>
                            </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" onkeypress="Javascript:return NumberOnly(event)"
                                AutoCompleteType="None"   onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate"
                                Format="dd/MM/yyyy" TargetControlID="txtToDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                                EnableClientScript="true" ErrorMessage="Please select date" SetFocusOnError="true"
                                Text="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">MajorHead :
                            </span></b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCTDMajorHead" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCTDMajorHead_SelectIndexChanged">
                                <asp:ListItem Text="--Select All MajorHead--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="0028-आय तथा व्यय पर अन्य कर" Value="0028"></asp:ListItem>
                                <asp:ListItem Text="0040-बिक्री, व्यापार आदि पर कर" Value="0040"></asp:ListItem>
                                <asp:ListItem Text="0042-माल तथा यात्री कर" Value="0042"></asp:ListItem>
                                <asp:ListItem Text="0043-विद्युत कर तथा शुल्क" Value="0043"></asp:ListItem>
                                <asp:ListItem Text="0044-सेवा कर" Value="0044"></asp:ListItem>
                                <asp:ListItem Text="0045-वस्तुओं तथा सेवाओं पर अन्य कर तथा शुल्क" Value="0045"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b><span style="color: #336699; font-family: Arial CE; font-size: 13px;">Zone : </span>
                            </b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                                Height="24px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show" ValidationGroup="vldInsert" OnClick="btnshow_Click" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="vldInsert" OnClick="btnshow_Click" />
                        </td>
                    </tr>--%>
                </table>
                <table id="tblHeadDetails" align="left">
                    <tr>
                        <td style="vertical-align: top;">
                            <div id="divHeadAmountDetails" style="margin-top: 1px;" runat="server">
                                <fieldset style="width: 350px; margin-left: 50px;">
                                    <legend style="color: #336699;">Major Head Amount :</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                        width: 350px;">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="RptMajorHead" runat="server" OnItemCommand="RptMajorHead_ItemCommand"
                                                    OnItemDataBound="RptMajorHead_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                            height: 20px">
                                                            <td style="color: White;">
                                                                Sr.No
                                                            </td>
                                                            <td style="color: White; text-align: left">
                                                                MajorHead
                                                            </td>
                                                            <td style="color: White; text-align: right">
                                                                Amount&nbsp;
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <asp:LinkButton ID="lnkMajorHead" ForeColor="#336699" runat="server" Text='<%# Eval("MajorHead") %>'
                                                                    CommandArgument='<%# Eval("MajorHead") %>' CommandName="MheadCode"></asp:LinkButton>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <asp:LinkButton ID="lnkMajorHead" ForeColor="#336699" runat="server" Text='<%# Eval("MajorHead") %>'
                                                                    CommandArgument='<%# Eval("MajorHead") %>' CommandName="MheadCode"></asp:LinkButton>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <b><span style="color: #336699">Total Amount in (<img src="../../Image/rupees.jpg" />)
                                                                </span></b>
                                                            </td>
                                                            <td align="right" height="40px">
                                                                <asp:Label ID="lblSchemaTotal" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                        <td style="vertical-align: top;">
                            <div id="DivBudgetHead" runat="server" visible="true" style="margin-left: 10px; margin-top: 1px;
                                margin-right: 10px;">
                                <fieldset style="width: 350px;">
                                    <legend style="color: #336699;">BudgetHead Amount: </legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                        width: 350px">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="RptBudgetHead" runat="server" OnItemDataBound="RptBudgetHead_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                            height: 20px">
                                                            <td style="color: White; width: 10%;">
                                                                Sr.No
                                                            </td>
                                                            <td style="color: White; text-align: left">
                                                                BudgetHead
                                                            </td>
                                                            <td style="color: White; text-align: right">
                                                                Amount &nbsp;
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                                            <td align="center" style="font-size: 15; width: 10%;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <%# Eval("MajorHead")%>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                            <td align="center" style="font-size: 15;">
                                                                <%# Container.ItemIndex+1 %>
                                                            </td>
                                                            <td align="left" style="font-size: 15;">
                                                                <%# Eval("MajorHead")%>
                                                            </td>
                                                            <td align="center" style="font-size: 15; text-align: right;">
                                                                <%# string.Format("{0:0.00}", Eval("Amount"))%>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <b><span style="color: #336699">Total Amount in (<img src="../../Image/rupees.jpg" />):-</span></b>
                                                            </td>
                                                            <td align="right" height="40px">
                                                                <asp:Label ID="lblBudgetHeadTotal" runat="server" Font-Bold="true" ForeColor="#336699"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </center>
</asp:Content>
