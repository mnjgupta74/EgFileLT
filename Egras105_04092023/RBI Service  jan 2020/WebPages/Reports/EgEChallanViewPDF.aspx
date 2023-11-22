<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgEChallanViewPDF.aspx.cs" Inherits="WebPages_EgEChallanViewPDF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <asp:Panel ID="pnlBackGround" runat="server" CssClass="popup-div-background">
                <div id="divFeedback" runat="server" class="popup-div-front">
                    <br />
                    <br />
                    <br />
                    <img src="../../App_Themes/images/progress.gif" />
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
            <div id="MainDiv" style="position: absolute; left: 50px; width: 1400px; height: 1300px;">
                <table width="90%" runat="server" id="tbl">
                    <tr>
                        <td>
                             <center>
                                <rsweb:ReportViewer ID="DefaceDetailSSRS" runat="server" Font-Names="Times New Roman"
                                    Font-Size="8pt" Height="700px" Width="100%" ShowToolBar="false" AsyncRendering="true">
                                </rsweb:ReportViewer>
                            </center>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
