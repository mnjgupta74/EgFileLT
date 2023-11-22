<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgMasterSchema.aspx.cs" Inherits="WebPages_MasterPages_EgMasterSchema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function Success() {

            alert('Record Updated Successfully.');
            // your page name

        }
        function Success1() {

            alert('Record Not Updated. ');
            // your page name

        }
        function save() {

            alert('Record Save Successfully.');
            // your page name

        }
        function save1() {

            alert('Record Not Saved. ');
            // your page name

        }

        function ChangeCase(elem) {
            elem.value = elem.value.toUpperCase();
        }
    </script>

    <%--  <script type="text/javascript">
        $(document).ready(function() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "EgMasterSchema.aspx/BindDatatoDropdown",
                data: "{}",
                dataType: "json",
                success: function(data) {
                    $.each(data.d, function(key, value) {
                    $("#ddlbudgethead").append($("<option></option>").val(value.ScheCode).html(value.BudgetHead));
                    });
                },
                error: function(result) {
                    alert("Error");
                }
            });
        });
</script>--%>
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
            <table runat="server" style="width: 70%;" id="TABLE1" cellpadding="0" border="1"
                cellspacing="0" align="center">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblSchema" runat="server" Text="BUDGETHEAD-PURPOSE" ForeColor="Green"
                            Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 354px">
                        <asp:Label ID="lbldepartment" runat="server" Text=" Department List:-"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddldepartment" class="chzn-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"
                            Width="80%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfcdepartmentlist" runat="server" ControlToValidate="ddldepartment"
                            ErrorMessage="*" InitialValue="0" ValidationGroup="vldinsert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 354px">
                        <asp:Label ID="lblBudgetHead" runat="server" Text="Budget Head List :-"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlbudgethead" class="chzn-select" runat="server" OnSelectedIndexChanged="ddlbudgethead_SelectedIndexChanged"
                            AutoPostBack="True" Width="80%">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfcbudgethead" runat="server" ControlToValidate="ddlbudgethead"
                            ErrorMessage="*" InitialValue="0" ValidationGroup="vldinsert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 354px; height: 26px;">
                        <asp:Label ID="lblschemaname" runat="server" Text="Purpose Name:-"></asp:Label>
                    </td>
                    <td style="height: 26px">
                        <asp:TextBox ID="txtschemaname" runat="server" MaxLength="300" 
                            onChange="ChangeCase(this);" Font-Names="Arial Unicode MS"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfcschemaname" runat="server" ControlToValidate="txtschemaname"
                            ErrorMessage="*" ValidationGroup="vldinsert"></asp:RequiredFieldValidator>
                       <%-- <asp:RegularExpressionValidator ID="rgv" ResourceName="rgv" ValidationGroup="vldinsert"
                            runat="server" ControlToValidate="txtschemaname" ErrorMessage="Special character not allowed."
                            ValidationExpression="([a-z]|[A-Z]|[0-9]|[.]|[-/ ])*" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" ValidationGroup="vldinsert" />
                        <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdbudgetschema" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="ScheCode,DeptName">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="DeptName" HeaderText="DepartmentName" Visible="false" />--%>
                                <asp:BoundField DataField="BudgetHead" HeaderText="BudgetHead">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SchemaName" HeaderText="Purpose">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ScheCode" HeaderText="ScheCode" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGvEdit" runat="server" OnClick="btnGvEdit_Click" Text="Edit" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
