<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="BankSoftCopy.aspx.cs" Inherits="WebPages_BankSoftCopyUpload" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />

    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                $(document).ready(function () {
                    EnableDisableUploadButton();
                });
                function progressbar() {
                    $('.ajaxloader').css('display', 'block');
                }
                function EnableDisableUploadButton() {
                    var FileUpload1 = document.getElementById('<%= FileUpload1.ClientID %>');
                    var btnUpload = document.getElementById('<%= btnUpload.ClientID %>');
                    if (FileUpload1.value.length > 0) {
                        $(btnUpload).removeAttr("disabled");
                        try {
                            return checkFileExtension(FileUpload1);
                        }
                        catch (err) {
                            return false;
                        }
                    }
                    else {
                        $(btnUpload).attr("disabled", "disabled");
                    }

                    return false;
                }
            <%--    function CheckFileExist() {
                    
                    var URL = 'http://localhost:9671/Server%20-%20Server/BankUploadInfo/'
                    var fileName = $('#<%=FileUpload1.ClientID%>').val();
                    
                    $.ajax({
                        type: "POST",
                        url: "BanksoftCopy.aspx/CheckFileExist",
                        data: "{'filename':'"+ fileName +"'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d)
                                Confirm();
                            else
                                __doPostBack('<%= btnUpload.UniqueID %>', '');
                        },
                        failure: function (response) {
                            alert("Fail");
                        },
                        error: function (response) {
                            alert("Error");
                        }
                    });
                    
                }--%>
                <%--function Confirm() {
                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("File Already Exist. Do You Want To Replace It?")) {
                        __doPostBack('<%= btnUpload.UniqueID %>', '');
                    } else {
                        return false;
                    }
                }--%>
                
            </script>
            <style>
        .ajaxloader {
            position: fixed;
            width: 100%;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background-color: rgba(255,255,255,0.7);
            z-index: 9999;
            /*display: none;*/
        }

        @-webkit-keyframes spin {
            from {
                -webkit-transform: rotate(0deg);
            }

            to {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }

        .ajaxloader::after {
            content: '';
            /*display: block;*/
            position: absolute;
            left: 48%;
            top: 40%;
            width: 70px;
            height: 70px;
            border-style: solid;
            border-color: #1b2a47;
            border-top-color: lightcyan;
            border-width: 10px;
            border-radius: 50%;
            -webkit-animation: spin .8s linear infinite;
            animation: spin .8s linear infinite;
        }
                .btn-default {
                    color: #f4f4f4;
                    background-color: #428bca;
                }
    </style>
            <div id="dialog" style="display: none">
                * please Change your password
                <br>
                    <br></br>
                    * now it is mandatory to change password after 45 days </br>
            </div>
            <div id="ajaxloader" class="ajaxloader" runat="server">
               </div>
            <table style="width: 100%; text-align: center;">
                <tr align="left" style="margin-bottom:10px">
                    <td>
                        <asp:Label ID="lblBankUpload" runat="server" Text="Bank-Soft Copy Upload" Font-Bold="True"
                            ForeColor="#009900"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td align="center">
                        <table width="100%" border="1">
                             <tr align="center" style="margin-bottom:10px">
                                <td colspan="4">
                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="BankXML" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="DMSPDF" style="margin-left: 20px" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                            </tr>
                            <tr id="trXML" runat="server">
                                <td align="center" style="width: 200px">
                                    <b>Select Xml File For Upload :</b>
                                </td>
                                <td align="center">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </td>
                                <td id="trSave" runat="server" visible="true" style="width: 100px">
                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload Scroll" OnClientClick="return progressbar();"
                                         CausesValidation="true"/>
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                      <%--  <input type="hidden" id="hdnFileName" runat="server" />--%>
                         <asp:Label ID="lblFileName" runat="server" Visible="false" style="color:#009900; font-weight:600;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="display: none;">
                        <div style="border: dashed 3px green; width: 600px;">
                            <table width="100%" border="0">
                                <tr>
                                    <td align="center" width="50%">
                                        <b style="color: White; size: 14pt;">Verify Status :</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="50%" valign="top">
                                        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <fieldset id="FsetMismatch" runat="server" visible="false">
                            <legend style="color: Green;">Mismatched Records</legend>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblTotalRecord" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="lblsum" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdVerify" runat="server" AutoGenerateColumns="False" Width="100%"
                                            Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Mismatched Records Found"
                                            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle" OnRowDataBound="grdVerify_RowDataBound"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataRowStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:BoundField DataField="GRN" HeaderText="GRN">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TreasuryAmount" HeaderText="Treasury Amount" DataFormatString="{0:n}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BankAmount" HeaderText="Bank Amount" DataFormatString="{0:n}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Treasury Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTreasuryDate" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBankDate" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="TreasuryDate" HeaderText="Treasury Date">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BankDate" HeaderText="Bank Date">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="Status" HeaderText="Treasury Status">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnProcessToNext" Visible="false" Style="width: 200px; background-color: Green; color: White; height: 35px; font-weight: bold; font-size: 14px"
                            runat="server"
                            Text="Submit to Process" OnClick="btnProcessToNext_Click" />
                    </td>
                </tr>
            </table>
             <div id="divDMSPDF" runat="server" visible="false">
                <iframe id="iFrameDMSPDF" width="100%" height="500px" name="ContainerIframe" runat="server"></iframe>
            </div>
    
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnVerify" />--%>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
