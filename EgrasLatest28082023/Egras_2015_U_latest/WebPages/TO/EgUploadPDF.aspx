<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgUploadPDF.aspx.cs" Inherits="WebPages_TO_EgUploadPDF" Title="Egras.Rajasthan.gov.in" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset runat="server" id="lstrecord" style="width: 800px; margin-left: 100px;">
        <legend style="color: #336699; font-weight: bold">Upload-File</legend>
        <table style="width: 100%" align="center" id="MainTable">
            <tr>
                <td align="center">
                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        &nbsp; File Name&nbsp;&nbsp; :-</span></b>&nbsp;
                    <asp:TextBox ID="txtfilename" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtfilename" ValidationGroup="Vld"
                        runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                        &nbsp;Upload File :- </span></b>&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="256px" />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnUpload" runat="server" Text="Save" Width="117px" OnClick="btnUpload_Click"
                        ValidationGroup="Vld" />
                </td>
            </tr>
        </table>
        <div>
            <table width="100%" align="center">
                <tr id="trCircularList" runat="server">
                    <td style="width: 450px; vertical-align: top;">
                        <div id="DivCircularList" style="margin-right: 10px;">
                            <center>
                                <fieldset style="width: 80%;" id="FieldCircular" runat="server">
                                    <legend style="color: #336699;">Circular List</legend>
                                    <table border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt; font-family: Arial;
                                        width: 100%">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="RptCircular" runat="server" OnItemCommand="RptCircular_ItemCommand">
                                                    <HeaderTemplate>
                                                        <tr style="background-color: #507CD1; color: White; font-weight: bold; text-align: center;
                                                            height: 20px">
                                                            <td style="color: White; width: 60px;">
                                                                UploadID
                                                            </td>
                                                            <td style="color: White; text-align: left;">
                                                                PdfName
                                                            </td>
                                                            <td style="color: White; text-align: left;" colspan="3">
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #EFF3FB; height: 20px;">
                                                            <td align="center" style="font-size: 15;">
                                                                <asp:Label ID="lblPdfId" runat="server" Text='<%#Eval("PdfId") %>'></asp:Label>
                                                            </td>
                                                            <td style="font-size: 15; text-align: left;">
                                                                <asp:Label ID="lblpdfname" Text='<%#Eval("PdfName")%>' runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtpdfname" Text='<%#Eval("PdfName")%>' runat="server" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDelete" Text="delete" runat="server" CommandName="delete"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CommandName="Edit"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkCancel" Text="Cancel" runat="server" CommandName="Cancel"
                                                                    Visible="false"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style="background-color: #ffffff; height: 20px; font-family: Arial">
                                                            <td align="center" style="font-size: 15;">
                                                                <asp:Label ID="lblPdfId" runat="server" Text='<%#Eval("PdfId") %>'></asp:Label>
                                                            </td>
                                                            <td style="font-size: 15; text-align: left;">
                                                                <asp:Label ID="lblpdfname" Text='<%#Eval("PdfName")%>' runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtpdfname" Text='<%#Eval("PdfName")%>' runat="server" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkDelete" Text="delete" runat="server" CommandName="delete"></asp:LinkButton>&nbsp;
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CommandName="Edit"></asp:LinkButton>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkCancel" Text="Cancel" runat="server" CommandName="Cancel"
                                                                    Visible="false"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </center>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
