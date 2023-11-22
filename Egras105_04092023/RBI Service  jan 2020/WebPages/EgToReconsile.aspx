<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master"
    AutoEventWireup="true" CodeFile="EgToReconsile.aspx.cs" Inherits="BanksoftCopy" %>

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
                        <b>Reconsile</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Bank
                        <asp:DropDownList ID="ddlBank" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Date
                        <asp:TextBox ID="txtfromdate" runat="server" Width="100px" onkeypress="Javascript:return NumberOnly(event)"
                            onChange="javascript:return dateValidation()"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calendarfromdate" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="txtfromdate" TargetControlID="txtfromdate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditfromdate" Mask="99/99/9999" MaskType="Date"
                            CultureName="en-US" TargetControlID="txtfromdate" AcceptNegative="None" runat="server">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="rfcfromdate" runat="server" ControlToValidate="txtfromdate"
                            ErrorMessage="*" ValidationGroup="vldInsert"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnReconcile" runat="server" Text="Reconcile" OnClick="btnReconcile_Click" />
                    </td>
                </tr>
                   <tr>
                    <td align="center" >
                        <fieldset>
                            <legend style="color: Green;">Mismatched Records</legend>
                            <asp:GridView ID="grdVerify" runat="server" AutoGenerateColumns="False" Width="60%"
                                Font-Names="Verdana" Font-Size="10pt" EmptyDataText="No Recored Found" EmptyDataRowStyle-Font-Bold="true"
                                EmptyDataRowStyle-ForeColor="#336699" EmptyDataRowStyle-VerticalAlign="Middle"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="GRN" HeaderText="GRN">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <%-- <asp:BoundField DataField="Ref" HeaderText="Ref">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Cin" HeaderText="Cin" >
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>    --%>
                                    <%--<asp:BoundField DataField="Status" HeaderText="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>--%>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:n}">
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
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
