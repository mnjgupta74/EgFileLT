<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="BankMonthlyReport.aspx.cs" Inherits="WebPages_BankMonthlyReport" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function checkFileExtension(elem) {
            var filePath = elem.value;

            if (filePath.indexOf('.') == -1)
                return false;

            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'jpg';
            validExtensions[1] = 'jpeg';
            validExtensions[2] = 'bmp';
            validExtensions[3] = 'png';
            validExtensions[4] = 'gif';
            validExtensions[5] = 'tif';
            validExtensions[6] = 'tiff';
            validExtensions[7] = 'doc';
            validExtensions[8] = 'xls';
            validExtensions[9] = 'pdf';
            validExtensions[10] = 'xml';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
            return false;

        }
       
    </script>

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
            <table style="width: 100%; text-align: center;">
                <tr>
                    <td>
                        <b>Day wise Monthly Statement</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Select DMS Xml File For Upload :
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload and Save" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div runat="server" id="FsetMismatch" visible="false">
                            <asp:Label ID="lblTotalRecord" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Label ID="lblsum" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
