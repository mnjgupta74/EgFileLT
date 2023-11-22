<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgGrn.aspx.cs" Inherits="WebPages_charts_EgGrn" Title="Grn View" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <fieldset id="fldRev" runat="server" style="color: Green; border-top-left-radius: 0.5em 0.5em;
            height: 500px; border-top-right-radius: 0.5em 0.5em; border-bottom-left-radius: 0.5em 0.5em;
            border-bottom-right-radius: 0.5em 0.5em; z-index: 0.1px; box-shadow: 10px 10px 5px #888888;
            behavior: url(../PIE.htc); margin-top: -30px;">
            <legend style="color: Green;">
                <h4>
                    GRN View
                </h4>
            </legend>
            <center>
                <div style="margin-top: -20px;">
                    FromDate :<asp:TextBox ID="txtfromDate" runat="server"></asp:TextBox>ToDate :<asp:TextBox ID="txttoDate" runat="server"></asp:TextBox><asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" />
                </div>
            </center>
            <div style="float: left; width: 100px;" id="listbox" runat="server" visible="false">
                <asp:ListBox ID="lstGrn" Width="95px" Height="450px" Style="border: bold 1 green;"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstGrn_SelectedIndexChanged">
                </asp:ListBox>
            </div>
            <div style="float: left; width: 800px;" id="iframe" runat="server" visible="false">
                <asp:Label ID="lblgrn" runat="server" Width="1050px" Height="450px"></asp:Label>
            </div>
        </fieldset>
    </div>
    <ajaxToolkit:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="calTodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>


