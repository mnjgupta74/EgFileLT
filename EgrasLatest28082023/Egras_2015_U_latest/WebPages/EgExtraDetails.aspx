<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgExtraDetails.aspx.cs" Inherits="WebPages_EgExtraDetails" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ClickToPrint() {
            docPrint = window.open("", "mywindow", "location=1,status=1,scrollbars=1,width=1000,height=500");
            docPrint.document.open();
            docPrint.document.write('<html><head><title>ChallanPage</title>');
            docPrint.document.write('</head><body onLoad="self.print()"><left>');
            docPrint.document.write('</Center><br/><table width="1030px" height="50%" top="0"  border=0 font Size="8"><tr><td width="150%"><left><font face="Small Fonts">');
            docPrint.document.write(document.getElementById("divExtra").innerHTML);
            docPrint.document.write('</td></tr></table></left></font></body></html>');
            docPrint.document.close();

        }

    </script>

    <div>
        <table id="tblExtradetails" width="80%" align="center">
            <tr align="center">
                <td style="height: 16px; background-color: #B2D1F0" valign="top">
                    <div style="width: 100%; margin-left: 0px">
                        <asp:Label ID="lblExtra" runat="server" Text="Extras-Details" Font-Bold="True" ForeColor="#009900"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div align="center" id="divExtra">
                        <asp:Label ID="literal1" runat="server" Style="font-size: 11; border: 1px solid green;
                            color: #660000; background-color: Transparent" Visible="false"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 16px;" align="right">
                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Image/printer.jpg" runat="server"
                        OnClientClick="javascript:ClickToPrint();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
