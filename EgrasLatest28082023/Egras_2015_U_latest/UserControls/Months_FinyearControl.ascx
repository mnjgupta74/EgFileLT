<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Months_FinyearControl.ascx.cs" Inherits="UserControls_Months_FinyearControl" %>
<script src="../../JS/bootstrap.min.js"></script>
<link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
<%--<script src="../../JS/chosen.jquery.js"></script>--%>
<script>
    function YearChange() {
        var Currentdate = new Date().getFullYear();
        var ss = $('#<%=ddlYear.ClientID %> :selected').text();
        var sw = Currentdate.toString().substring(2, 2);
        if ($('#<%=ddlYear.ClientID %> :selected').text().substring(5, 2) == Currentdate.toString().substring(2, 2)) {
            $('select[id*="#<%=ddlYear.ClientID %>"] option').each(function (index, value) {

                if ($(this).text())
                    console.log("index is:" + index); // index
                console.log("value is" + $(this).val()); // dropdown option value
                console.log("dropdown text value is" + $(this).text()); //dropdown option text
            });
            $("#<%=ddlYear.ClientID %> option[value*='n/a']").prop('disabled', true);
        }

    }

</script>
<style>
</style>
<table runat="server" style="width: 94%; text-align: left" id="TABLE2"
    border="1" align="center">

    <tr>
        <td align="center">
            <b><span style="color: #336699">Finanicial Year:-</span></b>&nbsp;
            <asp:DropDownList ID="ddlYear" Width="120px" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                onchange="YearChange()" runat="server">
            </asp:DropDownList>
        </td>
        <td align="center">
            <b><span style="color: #336699">Month:-</span></b>&nbsp;
            <asp:DropDownList ID="ddlMonth" Width="40%" CssClass="form-control chzn-select" runat="server">
                <asp:ListItem Text="April" Value="4" />
                <asp:ListItem Text="May" Value="5" />
                <asp:ListItem Text="June" Value="6" />
                <asp:ListItem Text="July" Value="7" />
                <asp:ListItem Text="August" Value="8" />
                <asp:ListItem Text="September" Value="9" />
                <asp:ListItem Text="October" Value="10" />
                <asp:ListItem Text="November" Value="11" />
                <asp:ListItem Text="December" Value="12" />
                <asp:ListItem Text="January" Value="1" />
                <asp:ListItem Text="February" Value="2" />
                <asp:ListItem Text="March" Value="3" />
            </asp:DropDownList>
        </td>

    </tr>
</table>


