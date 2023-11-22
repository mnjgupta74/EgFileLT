﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true" CodeFile="EgEChallanViewRptManualSuccess.aspx.cs" Inherits="WebPages_Reports_EgEChallanViewRptManualSuccess" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
        function openPopup() {
            var argObj = window;
            var id = '<%= Session["GrnNumber"] %>';

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebPages/EgEChallanView.aspx/EncryptData") %>',
                data: '{"id":"' + id + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    window.open("../EgAddExtraDetail.aspx?id=" + escape(msg.d), argObj, "dialogWidth:800px; dialogHeight:500px; dialogLeft:252px; dialogTop:120px; center:yes");
                }
            });
        }
        window.onload = function () { a(); }
        function a() {
            var found = false;
            var info = '';

            try {
                acrobat4 = new ActiveXObject('PDF.PdfCtrl.1');
                if (acrobat4) {
                    found = true;
                    info = 'v. 4.0';
                }
            }
            catch (e) {
                //???
            }

            if (!found) {
                try {
                    acrobat7 = new ActiveXObject('AcroPDF.PDF.1');
                    if (acrobat7) {
                        found = true;
                        info = 'v. 7+';
                    }
                }
                catch (e) {
                    //???
                }

                if (!found && navigator.plugins && navigator.plugins.length > 0) {
                    for (var i = 0; i < navigator.plugins.length; i++) {
                        if (navigator.plugins[i].name.indexOf('Adobe Acrobat') > -1) {
                            found = true;
                            info = navigator.plugins[i].description + ' (' + navigator.plugins[i].filename + ')';
                            break;
                        }
                    }
                }
            }



            if (!found) {

                document.getElementById('ctl00_ContentPlaceHolder1_tr').style.display = 'block';
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_tr').style.display = 'none';

                // document.getElementById('ctl00_ContentPlaceHolder1_tr').visible = 'false';
            }
            //        document.write("Acrobat Reader Installed : " + found);
            //        document.write("<br />");
            //        if (found) document.write("Info : " + info);
        }
    </script>

    <div id="MainDiv" style="position: absolute; left: 50px; width: 1400px; height: 1300px;">
        <table width="90%" runat="server" id="tbl" class="table">
            <tr id="tr" runat="server">
                <td>
                    Please install Acrobat Reader from this link : <a id="anchor" href="http://get.adobe.com/reader/">
                        <u>Click Here</u></a>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:Button ID="btnPDF" runat="server"  Text="Print"   Width="300px" onclick="btnPDF_Click" />                        
                        <asp:LinkButton ID="lnkExtraDetails" runat="server" Text="View Extra Details" Visible="false"
                            OnClientClick="openPopup();"></asp:LinkButton>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                   <center>
                        <rsweb:ReportViewer  ID="SSRSreport" runat="server" AsyncRendering="false" Width="70%" Height = "800PX">
                        </rsweb:ReportViewer>
                   </center>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

