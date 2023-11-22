<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgDefaceReleaseRequest.aspx.cs" Inherits="WebPages_Department_EgDefaceReleaseRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../js/SweetAlert/sweetalert.css" rel="stylesheet" />
    <script src="../../js/SweetAlert/sweetalert.min.js"></script>
    <script src="../../js/bootstrap.min.js"></script>
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../../CSS/PageHeader.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript">
        function validate(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("Invalid entry! Only numbers are accepted!");
                field.value = "";
                field.focus();
                field.select();
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function myAlert(heading, mycontent) {
            swal(heading, mycontent);
        }
        //function myAlert(heading, mycontent) {
        //    //swal(heading, mycontent);
        //    swal({
        //        title: heading,
        //        text: mycontent,
        //        button: "Close",
        //    });
        //}
    </script>

    <style>
        .btn-default {
            color: #f4f4f4;
            background-color: #428bca;
        }
    </style>
    <div _ngcontent-c6="" class="tnHead minus2point5per">
        <h2 _ngcontent-c6="" title="Release-Defaced-Challan" class="pull-left">
            <span _ngcontent-c6="" style="color: #FFF">Defaced-Release-Request</span></h2>
        <img src="../../Image/help1.png" class="pull-right" style="height: 44px; width: 34px;" title="Defaced-Release-Request" />
    </div>
    <table style="width: 100%; text-align: left"
        border="1" align="center">
        <tr>
            <td align="center" width="25%">
                <span style="width: 300px; color: #336699;  font-size: 13px;"><b>GRN:</b></span>
                <asp:TextBox ID="txtGRN" CssClass="form-control" runat="server" Style="font-size: Small; height: 30px; width: 150px; display: inline-flex;"
                    OnChange="validate(this);" MaxLength="12" ValidationGroup="vldGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfcGRN" ValidationGroup="vldGroup" runat="server" ErrorMessage="*" Display="Dynamic" ControlToValidate="txtGRN"></asp:RequiredFieldValidator>
            </td>
            <td align="center" width="25%">
                <span style="width: 300px; color: #336699;  font-size: 13px;"><b>Amount:</b></span>
                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" Style="font-size: Small; height: 30px; width: 150px; display: inline-flex;" 
                    TextMode="number" pattern="[0-9]*"
                    ValidationGroup="vldGroup" MaxLength="12"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfcAmount" ValidationGroup="vldGroup" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
            </td>
            <td align="center" width="50%">
                <span style="width: 300px; color: #336699;  font-size: 13px;"><b>Upload Sanction PDF</b></span>
                <asp:FileUpload runat="server" ID="fileDTA" CssClass="form-control" Style="width: 50%; display: inline-flex" ValidationGroup="vldGroup" />
                <asp:RegularExpressionValidator ID="regpdf" Text="Only PDF File Allowed" Message="please Select PDF"
                    ControlToValidate="fileDTA" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" ValidationGroup="vldGroup" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnView" runat="server" Style="margin-left: 20px; height: 35px;" Text="View" class="btn btn-default" OnClick="btnView_Click" />
            </td>
            <td align="center" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="vldGroup" Style="margin-left: 20px; height: 35px;" Text="Submit" class="btn btn-default" OnClick="btnSubmit_Click" />
            </td>

        </tr>
    </table>

    <table width="100%" style="margin-top: 20px">
        <tr id="trRefNO" runat="server" visible="false">
            <td align="center">
                <asp:Label ID="lblRefNO" runat="server" Text="" Font-Bold="true"></asp:Label></td>
        </tr>
        <tr runat="server" id="trgrddefacerelease" visible="false">
            <td>
                <asp:GridView ID="grddefacerelease" runat="server" AutoGenerateColumns="False" Width="100%"
                    Font-Names="Verdana" Font-Size="10pt" DataKeyNames="GRN" EmptyDataText="No Record Found"
                    ShowFooter="true" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="#336699"
                    EmptyDataRowStyle-VerticalAlign="Middle" CellPadding="4" ForeColor="#333333" OnRowCommand="grddefacerelease_RowCommand"
                    GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo">
                            <ItemTemplate>
                                <asp:Label ID="SNO" runat="server" Text='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%# Container.DataItemIndex + 1 %>'></asp:Label>

                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GRN">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkgrn" CommandName="grnbind" Text='<%#Eval("GRN")%>' CommandArgument='<%#Eval("GRN")%>'
                                    runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="FA Action" DataField="FaAction">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="FA Actiont Date" DataField="FaActionDate">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="HOD Action" DataField="HodAction">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="HOD Actiont Date" DataField="HodActionDate">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>



                        <asp:TemplateField HeaderText="Reference No">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePannelpdf" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReferenceNo" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:LinkButton ID="btnReferenceNo" runat="server" CommandName="ReferenceNo" Text='<%#Eval("ReferenceNo")%>' CommandArgument='<%#Eval("GRN")%>' CausesValidation="false" />

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="#507CD1" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate>
                        No Data Found
                    </EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

