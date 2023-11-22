<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="UploadEgrasDocument.aspx.cs" Inherits="WebPages_Admin_UploadEgrasDocument" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
     <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />
     <style type="text/css">
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
     <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Challan Detail" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Upload PDF</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Upload PDF" />
    </div>
    <table width="100%" style="text-align: center" align="center" border="1">
        <tr>
            <td>
                <div class="row">
                    <div class="col-md-3" style="margin-top: 10px">
                        <b><span style="color: #336699">File Url </span></b><span class="mandatory"></span>:&nbsp;
                    </div>
                    <div class="col-md-9" style="width: 50%; display: inline-flex">
                       <asp:FileUpload ID="FilePDF" runat="server" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="regpdf" Text="Allow Pdf File Only" ErrorMessage="please upload only pdf files"
                            ControlToValidate="FilePDF" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" />
                    </div>
                </div>
            </td>
            <td>
                <td id="tdSubmit" runat="server" colspan="3" align="center">
                 <asp:Button ID="btnSubmit" runat="server" Style="margin-left: 20px; height: 30px;" Text="Submit" CssClass="btn btn-default" OnClick="btnSubmit_Click" />

                </td>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Visible="false" Style="margin-top: 10px">
        <asp:GridView runat="server" ID="grdPDFUpload" AutoGenerateColumns="False" Width="100%" RowStyle-CssClass="bottomBorder"
            BackColor="#5D7B9D" PageSize="18" BorderColor="#CCCCCC"
            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" ForeColor="#333333" GridLines="None" EmptyDataText="No Record Found"
            Visible="False" OnRowCommand="grdPDFUpload_RowCommand" DataKeyNames="PdfByte,FileName">
            <FooterStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                Font-Names="Times New Roman" Font-Size="15px" />
            <Columns>
                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1  %>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblID" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFName" Text='<%# Eval("FileName") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Upload date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUploadDate" Text='<%# Eval("UploadDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Status">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRow" runat="server"  CommandName="myCheckbox"  AutoPostBack="true" OnCheckedChanged="chkRow_CheckedChanged" CommandArgument='<%# Eval("Id") %>' Checked='<%#(Eval("Flag"))%>' />
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDownload" runat="server" Text="DownloadPDF" CommandArgument="<%# Container.DataItemIndex %>"
                            CommandName="Download" Font-Bold="true" Font-Size="Small"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div align="center">No records found.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>
</asp:Content>

