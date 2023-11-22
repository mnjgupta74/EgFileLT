<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgFAQUploadPDF.aspx.cs" Inherits="WebPages_TO_EgFAQUploadPDF" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset runat="server" id="lstrecord">
        <legend style="color: #336699; font-weight: bold">Upload-FAQ-File</legend>
        <center>
            <table style="width: 100%" align="center" id="MainTable">               
                <tr>
                    <td align="center">
                        <b><span style="width: 500px; color: #336699; font-family: Arial CE; font-size: 13px;">
                            &nbsp;Upload File :- </span></b>&nbsp;
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="430px" Height="25px" />
                        <asp:Button ID="btnUpload" runat="server" Text="Save" Width="100px" ValidationGroup="Vld"
                            OnClick="btnUpload_Click" />
                    </td>
                </tr>                
            </table>
        </center>
    </fieldset>
</asp:Content>
