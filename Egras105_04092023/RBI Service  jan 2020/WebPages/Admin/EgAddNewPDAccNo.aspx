<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage5.master" AutoEventWireup="true"
    CodeFile="EgAddNewPDAccNo.aspx.cs" Inherits="WebPages_Admin_EgAddNewPDAccNo"
    Title="AddNewPdAccount Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function NumberOnly(field) {
            var valid = "0123456789"
            var ok = "yes";
            var temp;
            for (var i = 0; i < field.value.length; i++) {
                temp = "" + field.value.substring(i, i + 1);
                if (valid.indexOf(temp) == "-1") ok = "no";
            }
            if (ok == "no") {
                alert("  Only Numeric Value Allowed.!");
                field.focus();
                field.select();
                field.value = "";
            }
        }
        //$(window).load(function (){
         
        //});
        $(document).ready(function () {
               $("#<%=lblPDDivision.ClientID %>").text("PD Account No:-");
            $("#<%=lblOfficeCode.ClientID %>").hide();
            $("#<%=txtOfficeCode.ClientID %>").hide();
            $("#<%=PDDivisionDetail.ClientID %>").hide();
            $("#<%=spanPD.ClientID %>").show();
            $("#<%=spanDiv.ClientID %>").hide();

        
        $("#<%=rblPdDiv.ClientID%>").change(function () {
            //var rbvalue = $("input[@name=<%=rblPdDiv.UniqueID%>]:radio:checked").val();
            var rbvalue = $("[id$='rblPdDiv']").find(":checked").val();
            if(rbvalue == "pd")
            {
                $("#<%=lblPDDivision.ClientID %>").text("PD Account No:-");
                $("#<%=lblOfficeCode.ClientID %>").hide();
                $("#<%=txtOfficeCode.ClientID %>").hide();
                $("#<%=spanPD.ClientID %>").show();
                $("#<%=spanDiv.ClientID %>").hide(); 
                //Clear Values
                $("#<%=lblPdDivText.ClientID %>").text("");
                $("#<%=lblPdDivNameText.ClientID %>").text("");
                $("#<%=lblMajorHeadOfficeCodeText.ClientID %>").text("");
                $("#<%=lblTreasuryCodeDetail.ClientID %>").text("");
                $("#<%=lblStatusText.ClientID %>").text("");
                $("#<%=txtPDDivision.ClientID %>").val("");
                $("#<%=txtTreasuryCode.ClientID %>").val("");
                $("#<%=txtOfficeCode.ClientID %>").val("");
                $("#<%=PDDivisionDetail.ClientID %>").hide();
                $("#<%=lblmsg.ClientID %>").hide();  
            }
            else
            {
                $("#<%=lblPDDivision.ClientID %>").text("Division Code:-");
                $("#<%=lblOfficeCode.ClientID %>").show();
                $("#<%=lblOfficeCode.ClientID %>").text("Office Code:-");
                $("#<%=txtOfficeCode.ClientID %>").show();
                $("#<%=spanPD.ClientID %>").hide();
                $("#<%=spanDiv.ClientID %>").show();
                //Clear Values
                $("#<%=lblPdDivText.ClientID %>").text("");
                $("#<%=lblPdDivNameText.ClientID %>").text("");
                $("#<%=lblMajorHeadOfficeCodeText.ClientID %>").text("");
                $("#<%=lblTreasuryCodeDetail.ClientID %>").text("");
                $("#<%=lblStatusText.ClientID %>").text("");
                $("#<%=txtPDDivision.ClientID %>").val("");
                $("#<%=txtTreasuryCode.ClientID %>").val("");
                $("#<%=txtOfficeCode.ClientID %>").val("");
                $("#<%=PDDivisionDetail.ClientID %>").hide();
                $("#<%=lblmsg.ClientID %>").hide();
            }
        });
        }); 
        
        //Call C# CodeBehind Method 
      function ShowCall() {
          
          //var rbvalue = $("input[@name=<%=rblPdDiv.UniqueID%>]:radio:checked").val();
          var rbvalue = $("[id$='rblPdDiv']").find(":checked").val();
    $.ajax({
      type: "POST",
      url: "EgAddnewPdaccno.aspx/Show",
      data: '{PDDiv:"'+$("#<%=txtPDDivision.ClientID %>").val()+'",TreasCode:"'+$("#<%=txtTreasuryCode.ClientID %>").val()+'",RBLValue:"'+rbvalue+'",OfficeCode:"'+$("#<%=txtOfficeCode.ClientID %>").val()+'"}',
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function(data){
      if (data.d[7] == "true") {
          if (rbvalue == "pd") {

              // FieldSet="PDDivision"
              $("#<%=lblPDDivision.ClientID %>").text("PD Account No:-");
              $("#<%=lblOfficeCode.ClientID %>").hide();
              $("#<%=txtOfficeCode.ClientID %>").hide();
              $("#<%=spanPD.ClientID %>").show();
              $("#<%=spanDiv.ClientID %>").hide();
              //PDDivisionDetail
              $("#<%=PDDivisionDetail.ClientID %>").show();
              $("#<%=lblPDDivDetailNo.ClientID %>").text("PD Account No:-");
              $("#<%=lblPDDivDetailName.ClientID %>").text("PD Account Name:-");
              $("#<%=lblMajorHeadOfficeCode.ClientID %>").text("MajorHead:-");
              $("#<%=lblStatus.ClientID %>").show();
              $("#<%=lblStatusText.ClientID %>").show();
              //PDDivisionDetail Actuall Data assignment
              $("#<%=lblPdDivText.ClientID %>").text(data.d[0]);
              $("#<%=lblPdDivNameText.ClientID %>").text(data.d[1]);
              $("#<%=lblMajorHeadOfficeCodeText.ClientID %>").text(data.d[2]);
              $("#<%=lblTreasuryCodeDetail.ClientID %>").text(data.d[3]);
              $("#<%=lblStatusText.ClientID %>").text(data.d[4]);
              if (data.d[5] == "false")//if Status = "D" Button disable
              {
                  $("#<%=btnSubmitUpdate.ClientID %>").hide();
              }
              $("#<%=btnSubmitUpdate.ClientID %>").attr('value', data.d[6]);
              if (data.d[6] == "false")// if no data then deatil div visibility hide
              {
                  $("#<%=btnSubmitUpdate.ClientID %>").hide();
                  $("#<%=lblmsg.ClientID %>").show();
                  $("#<%=lblmsg.ClientID %>").text("<B>PDAccNo is Not mapped In Treasury  </B>");
              }
              else {
                  $("#<%=lblmsg.ClientID %>").hide();
              }
          }
          else {
              // FieldSet="PDDivision"
              $("#<%=lblPDDivision.ClientID %>").text("Division Code:-");
              $("#<%=lblOfficeCode.ClientID %>").show();
              $("#<%=lblOfficeCode.ClientID %>").text("Office Code:-");
              $("#<%=txtOfficeCode.ClientID %>").show();
              $("#<%=spanPD.ClientID %>").hide();
              $("#<%=spanDiv.ClientID %>").show();
              //PDDivisionDetail
              $("#<%=PDDivisionDetail.ClientID %>").show();
              $("#<%=lblPDDivDetailNo.ClientID %>").text("Division Code:-");
              $("#<%=lblPDDivDetailName.ClientID %>").text("Division Name:-");
              $("#<%=lblMajorHeadOfficeCode.ClientID %>").text("Office Code:-");
              $("#<%=lblStatus.ClientID %>").hide();
              $("#<%=lblStatusText.ClientID %>").hide();
              //PDDivisionDetail Actuall Data assignment
              $("#<%=lblPdDivText.ClientID %>").text(data.d[0]);
              $("#<%=lblPdDivNameText.ClientID %>").text(data.d[1]);
              $("#<%=lblMajorHeadOfficeCodeText.ClientID %>").text(data.d[2]);
              $("#<%=lblTreasuryCodeDetail.ClientID %>").text(data.d[3]);
              $("#<%=btnSubmitUpdate.ClientID %>").attr('value', data.d[6]);
              if (data.d[6] == "false") {
                  $("#<%=btnSubmitUpdate.ClientID %>").hide();
                  $("#<%=lblmsg.ClientID %>").show();
                  $("#<%=lblmsg.ClientID %>").text("<B>PDAccNo is Not mapped In Treasury  </B>");
              }
              else {
                  $("#<%=lblmsg.ClientID %>").hide();
              }
          }
      }
      else {    
          alert("No Data Found");
      }
      },
      failure: onFailure 
        });
      }
      function onFailure()
      {
        alert("Error in Show");
      }
      
      
      function Update() {
      
          //var rbvalue = $("input[@name=<%=rblPdDiv.UniqueID%>]:radio:checked").val();
          var rbvalue = $("[id$='rblPdDiv']").find(":checked").val();
    $.ajax({
      type: "POST",
      url: "EgAddnewPdaccno.aspx/UpdateData",
      data: '{PDDivNo:"'+$("#<%=lblPdDivText.ClientID %>").text()+'",TreasCode:"'+$("#<%=lblTreasuryCodeDetail.ClientID %>").text()+'",RBLValue:"'+rbvalue+'",OfficeCode:"'+$("#<%=lblMajorHeadOfficeCodeText.ClientID %>").text()+'"}',
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success:function(data){
        alert(data.d);
         $("#<%=lblmsg.ClientID %>").show();
         $("#<%=lblmsg.ClientID %>").text(data.d);
      },
      failure:function(data){
        alert("Error in Update");
      }
      });
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
            <fieldset runat="server" id="Rbutton" style="width: 1000px; margin-left: 100px">
                <legend style="color: #336699; font-weight: bold">Add</legend>
                <div title="Add New" style="text-align: center;">
                    <asp:RadioButtonList runat="server" ID="rblPdDiv" RepeatDirection="Horizontal">
                        <asp:ListItem Text="PD Account" Value="pd" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Division Code" Value="div"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </fieldset>
            <br />
            <br />
            <div id="divPDDivisionMaster">
                <fieldset runat="server" id="PDDivision" style="width: 1000px; margin-left: 100px">
                    <span id="spanPD" runat="server"><legend style="color: #336699; font-weight: bold">PDAccount-Master</legend>
                    </span><span id="spanDiv" runat="server"><legend style="color: #336699; font-weight: bold">
                        DivisionCode-Master</legend></span>
                    <div style="width: 100%; height: 60px; margin-top: 15px" align="center">
                        <asp:Label runat="server" ID="lblPDDivision" Style="width: 250px; color: #336699;
                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                        <asp:TextBox runat="server" ID="txtPDDivision"></asp:TextBox>&nbsp <b><span style="width: 250px;
                            color: #336699; font-family: Arial CE; font-size: 13px;">Treasury Code:- </b>
                        <asp:TextBox runat="server" ID="txtTreasuryCode"></asp:TextBox>&nbsp
                        <asp:Label runat="server" ID="lblOfficeCode" Style="width: 250px; color: #336699;
                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                        <asp:TextBox runat="server" ID="txtOfficeCode"></asp:TextBox>&nbsp
                        <asp:Button runat="server" ID="btnShowDetail" Text="Show" OnClientClick="ShowCall(); return false;" />
                    </div>
                    <div>
                        <span style="width: 250px; color: Red; font-family: Arial CE; font-size: 13px; text-align: center;margin-left:350px;margin-right:80px">
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </span>
                    </div>
                    <div style="text-align: center; margin-left: 20%">
                        <fieldset runat="server" id="PDDivisionDetail" style="width: 550px;">
                            <legend style="color: #336699; font-weight: bold">Detail</legend>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPDDivDetailNo" runat="server" Style="width: 220px; color: #336699;
                                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblPdDivText" runat="server" Text="PDAccountNo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPDDivDetailName" runat="server" Style="width: 220px; color: #336699;
                                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblPdDivNameText" runat="server" Text="PDAccName"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <b><span style="width: 250px; color: #336699; font-family: Arial CE; font-size: 13px;">
                                            TreasuryCode :-</span></b>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblTreasuryCodeDetail" runat="server" Text="Treasury"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblMajorHeadOfficeCode" runat="server" Style="width: 220px; color: #336699;
                                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblMajorHeadOfficeCodeText" runat="server" Text="Major Head"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblStatus" runat="server" Text="Status:-" Style="width: 220px; color: #336699;
                                            font-family: Arial CE; font-size: 13px; font-weight: bold"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblStatusText" runat="server" Text="Status"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnSubmitUpdate" runat="server" Text="Submit" OnClientClick="Update(); return false;" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
