<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgLoginInfo.aspx.cs" Inherits="WebPages_EgLoginInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EgLoginInfo</title>

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
        
         function ClearDate()
            {
             var fmdate = document.getElementById('txtFromdate')
             fmdate.value = "";
            }
            
            
            function ClearDate1()
            {
            var tdate = document.getElementById('txtTodate')
             tdate.value = "";
            }
            
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolKit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
    </ajaxToolKit:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../App_Themes/Images/progress.gif" />
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
            <table id="tblchallan" cellpadding="0" cellspacing="0" border="0" width="75%" align="center">
                <tr>
                    <td align="center" style="height: 35px;" colspan="3">
                        <asp:Label ID="LabelHeading" runat="server" Text="User Login Detail" ForeColor="Green"
                            Font-Size="Medium" Font-Underline="true"></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="height: 35px;">
                        From Date :- &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFromdate" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                           onchange="javascript:return dateValidation(this,ctl00_ContentPlaceHolder1_txtToDate)" onmousedown="ClearDate();"></asp:TextBox>
                        <ajaxToolKit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtFromdate">
                        </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtFromdate" AcceptNegative="None" runat="server">
                        </ajaxToolKit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtFromdate"
                            ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        To Date :- &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTodate" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                            onchange="javascript:return dateValidation1(this,ctl00_ContentPlaceHolder1_txtFromDate)" onmousedown="ClearDate1();"></asp:TextBox>
                        <ajaxToolKit:CalendarExtender ID="calendartodate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtTodate">
                        </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="MaskedEdittodate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtTodate" AcceptNegative="None" runat="server">
                        </ajaxToolKit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="rfctodate" runat="server" ControlToValidate="txtTodate"
                            ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnclick" Text="Click" runat="server" OnClick="btnclick_Click" ValidationGroup="vldInsert" />
                    </td>
                </tr>
            </table>
            <br />
            <table id="tablegrid" cellpadding="0" cellspacing="0" border="0" width="75%" align="center">
                <tr id="paging" runat="server">
                    <td colspan="3">
                        <center>
                            <asp:LinkButton ID="lnk_first" runat="server" Text="<< First " Visible="false" Font-Bold="true"
                                Font-Size="Small" OnClick="lnk_first_Click"></asp:LinkButton>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkpre" runat="server" Text="<< Previous " Visible="false" Font-Bold="true"
                                Font-Size="Small" OnClick="lnkpre_Click"></asp:LinkButton>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnknext" runat="server" Text=" Next >>" Font-Bold="true" Font-Size="Small"
                                Visible="false" OnClick="lnknext_Click"></asp:LinkButton>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lnk_last" runat="server" Text=" Last>>" Font-Bold="true" Font-Size="Small"
                                Visible="false" OnClick="lnk_last_Click"></asp:LinkButton>
                            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCurrentPage" runat="server" Text="" ForeColor="green" Width="95%"></asp:Label>
                        </center>
                    </td>
                </tr>
                <br />
                <tr>
                    <td align="center" colspan="3">
                        <fieldset runat="server" id="lstrecord" visible="false">
                            <legend style="color: Green;">User-Login Info</legend>
                            <table cellpadding="0" cellspacing="0" border="1" width="100%" align="center">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblrecord" runat="server" ForeColor="green" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdLogindetail" runat="server" AutoGenerateColumns="False" Width="100%"
                                            BorderColor="#507cd1" Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Record Found"
                                            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                            CellPadding="4" DataKeyNames="Flag" BackColor="White" BorderStyle="Double" BorderWidth="3px"
                                            GridLines="Horizontal" AllowPaging="true" PageSize="25" OnRowDataBound="grdLogindetail_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="LoginId" HeaderText="LoginId">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IpAddress" HeaderText="IpAddress">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LoginDate" HeaderText="LoginDate">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtn" runat="server" Width="16px" Height="16px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="40px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                            <PagerStyle BackColor="#507cd1" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507cd1" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
