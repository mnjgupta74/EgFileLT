<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddContactUs.aspx.cs" Inherits="WebPages_TO_EgAddContactUs" Title="Egras.Raj.Nic.in" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset runat="server" id="lstrecord" style="width: 1000px;">
                <legend style="color: #336699; font-weight: bold">Contact Us Detail</legend>
                <table style="width: 900px" align="center" id="MainTable">
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Name :- </span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" AutoPostBack="false" ForeColor="#408080"
                                Width="49%" Height="20px" placeholder="Enter Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="txtFirstName" ValidationGroup="vldInsert" InitialValue="0"
                                ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Contact Number :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumber" runat="server" Width="49%" Height="20px" MaxLength="12"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="txtNumber" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumber"
                                Text="Enter Correct No." ErrorMessage="Enter Correct No." ValidationGroup="vldInsert"
                                ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                TargetControlID="txtNumber" WatermarkText="Number 1">
                            </ajaxToolKit:TextBoxWatermarkExtender>
                        </td>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Alternate Contact Number :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumber2" runat="server" Width="49%" Height="20px" MaxLength="11"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumber2"
                                Text="Enter Correct No." ErrorMessage="Enter Correct No." ValidationGroup="vldInsert"
                                ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                TargetControlID="txtNumber2" WatermarkText="Number 2">
                            </ajaxToolKit:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Alternate Contact Number :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumber2" runat="server" Width="150px" MaxLength="11"></asp:TextBox>
                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                TargetControlID="txtNumber2" WatermarkText="Number 2">
                            </ajaxToolKit:TextBoxWatermarkExtender>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                EmailID :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="49%" Height="20px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                ControlToValidate="txtEmail" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpForemail" runat="server" ControlToValidate="txtEmail"
                                Text="Enter Correct EmailId" ErrorMessage="Enter Correct EmailId" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="vldInsert" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Priority :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPriority" CssClass="selectpicker" runat="server"
                                 AppendDataBoundItems="true">
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                                <asp:ListItem Text="10" Value="10" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="ddlPriority" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <b><span style="width: 300px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                Prority :-</span></b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPriority" runat="server" Width="25%" Height="20px" AppendDataBoundItems="true">
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                                <asp:ListItem Text="10" Value="10" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="ddlPriority" ValidationGroup="vldInsert" ForeColor="Red" Style="text-align: center">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="LblActive" runat="server" Style="width: 300px; color: #336699; font-family: Arial CE;
                                    font-size: 13px;" Visible="false"> Active :</asp:Label>
                            </b>&nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkactive" runat="server" Visible="false" OnCheckedChanged="chkactive_CheckedChanged"
                                AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:Label ID="LblActdeactMsg" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 35px">
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vldInsert"
                                OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
                <table style="width: 900px" align="center">
                    <tr>
                        <td>
                            <asp:GridView ID="gridContactUs" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                EmptyDataText="No Record" ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="ContactId,IsDisabled"
                                OnRowDataBound="gridContactUs_RowDataBound">
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
                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact Number">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmailAddress" HeaderText="EmailID">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Priority" HeaderText="Priority">
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
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnGvDelete" runat="server" OnClick="btnGvDelete_Click" Text="Delete" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDisablePriority" runat="server"></asp:CheckBox>
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
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
