<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="FrmCoveringLetter.aspx.cs" Inherits="WebPages_FrmCoveringLetter" Title="Covering Letter" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/FinYearDropdown.ascx" TagName="Finyear" TagPrefix="ucl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CallPrint(PrintArea) {
            var DivGrid = document.getElementById(PrintArea);
            var PrintWindow = window.open('CoverLetter.aspx', '', 'letf=0,top=0,width=770,height=700,toolbar=0,scrollbars=0,status=0');
            PrintWindow.document.write(DivGrid.innerHTML);
            PrintWindow.document.close();
            PrintWindow.focus();
            PrintWindow.print();
        }
   
    </script>

    <div id="Div1" runat="server">
        <table border="" align="center" width="60%">
            <tr>
                <td align="center" style="width: 663px; text-decoration: underline;">
                    <b>E-Treasury Account </b>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 663px">
                    <table align="center" style="width: 82%">
                        <tr>
                            <td style="text-align: left">
                                <b><span style="color: #336699">Select Month</span></b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstartmonth" runat="server" Width="150px">
                                    <asp:ListItem Value="0"> --Select--</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2"> February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October </asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Month"
                                    ControlToValidate="ddlstartmonth" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <b><span style="color: #336699">Select Year</span></b>
                            </td>
                            <td>
                           
                                 <asp:DropDownList ID="ddlyear" runat="server">                    
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                    ControlToValidate="ddlyear" ErrorMessage="Select Year">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b><span style="color: #336699">List Of Account</span></b>
                            </td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="rbllisttype" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Value="1">1st </asp:ListItem>
                                    <asp:ListItem Value="2">2nd </asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 32px; width: 663px;">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 663px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="Div2" visible="false" runat="server" style="border: thin solid #000000;
        width: 95%">
        <table align="center" style="width: 90%">
            <tr>
                <td align="right" style="font-size: smaller">
                    <b>ANNEXURE - C</b>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-size: large">
                    <b>Government of Rajasthan </b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b><span id="Treasuryname" style="font-size: large;" runat="server">E-Treasury Office,
                        Jaipur</span> </b>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 20px; font-size: larger;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Date :<b> <span id="datee" runat="server">date</span></b>
                </td>
            </tr>
            <tr>
                <td align="left">
                    No. : eGRAS/A.G./Account/<span id="Finyear" runat="server"></span>/
                    <asp:Image ID="image1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left " style="height: 20px">
                    <br />
                    <br />
                    The Principal Accountant General,
                </td>
            </tr>
            <br />
            <tr>
                <td align="left ">
                    (Accounts & Entitlement),<br />
                    Rajasthan, Jaipur.
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 32px;">
                    <b>Subject :- Submission of &nbsp;<span id="list2" runat="server"></span> list of Accounts
                        for the month of <span id="month" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left " style="width: 386px">
                    Sir/Madam,
                </td>
            </tr>
            <tr>
                <td align="left ">
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Kindly Find enclosed herewith the <b><span id="list3" runat="server">list </span>
                        </b> List of Accounts for the month of <b><span id="month1" runat="server"></span></b> </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List is being carried by 1___________ 2___________
                    </p>
                </td>
            </tr>
            <tr>
                <td align="left " style="height: 18px">
                    <b>Encl : </b>
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="list" runat="server">1. L.O.R.</span>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span1" runat="server">2. TY-33.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span3" runat="server">3. Closing Abstract</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span4" runat="server">4. Bank Statement-D.M.S.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span5" runat="server">5. L.O.P.</span><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="Span6" runat="server">6. TY-34</span><br />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 20px"><b>Kamal Preet Kaur</b>
                    <b><span id="ToName" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <b><span id="Treasury" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <br />
                    <br />
                    No. : eGRAS/A.G./Account/<span id="Finyear1" runat="server"></span>/
                    <asp:Image ID="Barcode" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 19px">
                    <asp:Label ID="Label1" runat="server" Text="Date : "></asp:Label>
                    <b><span id="datee1" runat="server"></span></b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <b>Copy forwarded for information and necessary action to :-</b>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 19px">
                    <br />
                    1. Additional Director, (Treasury & Budget ), D.T.A. , Rajsthan , Jaipur.
                </td>
            </tr>
            <tr>
                <td align="right">
                    <br /><b>Kamal Preet Kaur</b>
                    <b><span id="toname1" runat="server" ></span></b>
                    <br />
                    <b><span id="Treasury1" runat="server"></span></b>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div3" visible="false" runat="server" align="center">
        <input id="btnPrint" onclick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Div2');"
            type="button"  Height="33" Style="margin-right: 2%;margin-top:10px;" value="Print" />
         <asp:Button ID="btnSignPdf"  Style="margin-right: 2%;margin-top:10px;"  runat="server" Text="SignPDF" OnClick="btnSignPdf_Click" />
        <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" Text="Cancel" />
    </div>
     <rsweb:ReportViewer ID="rptCoverletter" runat="server" Width="100%" SizeToReportContent="true" Visible="false"
        AsyncRendering="false" ShowRefreshButton="false" ShowExportControls="false">
    </rsweb:ReportViewer>
</asp:Content>
