<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage3.master" AutoEventWireup="true" CodeFile="EgDMSScrollPDF.aspx.cs" Inherits="WebPages_Admin_EgDMSScrollPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../js/bootstrap.min.css" rel="stylesheet" />
    <script src="../../js/bootstrap.min.js"></script>

     <%--<script src="../../js/jquery.min.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            if(<%= Session["UserID"] %> == "46")
            {     
                //$("#<%=ddlbank.ClientID %> option[value='1']").attr("disabled", "true");                
            }  
            else
            {
                $("#<%=ddlbank.ClientID %> option[value='1']").remove();     
            }
        });
    </script>
    <style>
        /*CSS FOR TOP HEADER STARTS*/

        .sectiontopheader[_ngcontent-c6] {
            border-top: 5px solid #337ab7;
            margin-bottom: 5px;
        }

        .tnHead, .sectiontopheader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            position: relative;
        }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6] {
                background: #337ab7;
            }

            .sectiontopheader h1, .tnHead h1 {
                padding: 8px 20px;
                position: relative;
                top: -5px;
                margin: 0;
                font-size: 18px;
            }

                .sectiontopheader h1:after, .tnHead h1:after {
                    position: absolute;
                    right: -34px;
                    top: 0;
                    content: '';
                    border-style: solid;
                    border-width: 34px 34px 0 0;
                }

            .sectiontopheader[_ngcontent-c6] h1[_ngcontent-c6]:after {
                border-color: #337ab7 transparent transparent;
            }
            
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
        /*#ctl00_ContentPlaceHolder1_grdDMSScroll td th
        {
            padding-top: 10px !important;
           
        }*/
        .bottomBorder td {
            border-color: whitesmoke;
            border-style: solid;
            border-bottom-width: 10px;
        }
    </style>

    <div _ngcontent-c6="" class="sectiontopheader tnHead minus2point5per">
        <h1 _ngcontent-c6="" title="DtaLetter">
            <span _ngcontent-c6="" style="color: #FFFFFF">DMS Scroll Uploads<br />
            DMS File FORMAT&nbsp; Start Four Character of IFSC Code _MMYYYY</span></h1>
        <%--<img src="../../Image/help1.png" style="height: 35px; width: 34px;" data-toggle="tooltip" data-placement="left" title="" />--%>
    </div>

    <div>
        <table align="center" style="width: 100%;" border="1" cellpadding="10" cellspacing="10">
            <tr>
                <td colspan="3" align="center">
                    <asp:RadioButtonList runat="server"
                        Style="display: contents !important" ID="rbtnList" Width="550px" CssClass="form-control" RepeatDirection="Horizontal"
                        AutoPostBack="true" OnSelectedIndexChanged="rbtnList_SelectedIndexChanged">
                        <asp:ListItem Text="Upload PDF " Value="1" Selected="True" style="margin-right: 35px" />
                        <asp:ListItem Text="PDF Download" Value="2" style="margin-right: 35px" />
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <b><span style="color: #336699">Month : </span></b>&nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server" TabIndex="2" Enabled="true" Width="120px" ToolTip="Select Month">
                </asp:DropDownList>
                </td>
                <td>
                    <b><span style="color: #336699">Year : </span></b>&nbsp;
                <asp:DropDownList ID="ddlYear" runat="server" TabIndex="2" Enabled="true" Width="120px" ToolTip="Select Financial Year">
                </asp:DropDownList>
                </td>

                <td>
                    <b><span style="color: #336699">Bank : </span></b>&nbsp;
                <asp:DropDownList ID="ddlbank" runat="server" TabIndex="2" Enabled="true" Width="150px" ToolTip="Select Month">
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td id="tdFileUpload" runat="server">
                    <div class="row">
                        <div class="col-md-3" style="margin-top: 10px">
                            <b><span style="color: #336699">File Url </span></b><span class="mandatory">*</span>:&nbsp;
                        </div>
                        <div class="col-md-9" style="width: 50%; display: inline-flex">
                            <asp:FileUpload runat="server" ID="fileDTA" CssClass="form-control" Width="250px" />
                            <asp:RegularExpressionValidator ID="regpdf" Text="Allow Pdf File Only" ErrorMessage="please upload only pdf files"
                                ControlToValidate="fileDTA" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" />
                        </div>
                    </div>


                </td>
                <td id="tdSubmit" runat="server" colspan="3" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Style="margin-left: 20px;height: 30px;" Text="Submit" class="btn btn-default" OnClick="btnSubmit_Click" />

                </td>
                <%--<td id="tdDownload" runat="server">
                    <asp:Button ID="btnDownload" runat="server" Text="Show" class="btn btn-primary" Visible="true" OnClick="btnDownload_Click" />
                </td>--%>
            </tr>
        </table>
      
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%" Visible="false" style="margin-top: 10px">
                        <asp:GridView runat="server" ID="grdDMSScroll" AutoGenerateColumns="False" Width="100%" RowStyle-CssClass="bottomBorder"
                            BackColor="#5D7B9D" PageSize="18" BorderColor="#CCCCCC"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="#333333" GridLines="None" EmptyDataText="No Record Found"
                            Visible="False" OnRowCommand="grdDMSScroll_RowCommand">
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
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"/>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblID" Text='<%# Eval("ID") %>'  Visible="false"></asp:Label>
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
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBankName" Text='<%# Eval("BankName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYear" Text='<%# Eval("FileYear") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblMonth" Text='<%# Eval("Filemonth") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13px"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBsrcode" Text='<%# Eval("Bsrcode") %>' Visible="false"></asp:Label>
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
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnDownload" runat="server" Text="DownloadPDF" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Download" Font-Bold="true" Font-Size="Small"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>

                </div>
           
   

</asp:Content>

