<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EgUserWiseRevOrTrans.aspx.cs"
    Inherits="WebPages_Charts_EgUserWiseRevOrTrans" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EgUserWiseRevOrTrans</title>
    <link href="../../CSS/style.css" rel="stylesheet" type="text/css" />

    <script src="../../js/amcharts.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <fieldset id="fldRev" runat="server" style="color: Green; border-top-left-radius: 0.5em 0.5em;
            border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
            border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
            behavior: url(../PIE.htc);">
            <legend style="color: Green;">
                <h4>
                    User Wise Revenue And Transection Report</h4>
            </legend>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center" width="100%">
                        From Date :
                        <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqFromDate" runat="server" ControlToValidate="txtFromDate"
                            SetFocusOnError="true" Text="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        To Date :
                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqToDate" runat="server" ControlToValidate="txtToDate"
                            SetFocusOnError="true" Text="*" ValidationGroup="de"></asp:RequiredFieldValidator>
                        <%-- </td>
                <td>--%>&nbsp;
                        <%-- <asp:RadioButtonList ID="rdlRev" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow" Width="200px">
                        <asp:ListItem Text="Month Wise" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Day Wise" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
              
                    &nbsp;&nbsp;--%>
                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="de" OnClick="btnShow_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <center>
                            <asp:Label ID="lblhead" runat="server" Text="No Record Found !!" Visible="false"></asp:Label>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="fldColumn" runat="server" visible="false" style="color: Green; border-top-left-radius: 0.5em 0.5em;
                            border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
                            padding-top: 20px; border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
                            behavior: url(../PIE.htc);">
                            <legend style="color: Green;">Column Chart</legend>
                            <div id="divDERevCol" style="width: 100%; height: 300px; background-color: white;">
                                <asp:Literal ID="ltRevCol" runat="server"></asp:Literal>
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </fieldset>
        <ajaxToolkit:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy"
            TargetControlID="txtFromDate">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
        </ajaxToolkit:CalendarExtender>
    </div>
    </form>
</body>
</html>
