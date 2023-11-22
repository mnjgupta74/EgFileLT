<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegrationErrorPage.aspx.cs" Inherits="IntegrationErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Egras.rajasthan.gov.in</title>
</head>
<body style="background-color:#f9f9f9">
    <form id="form1" runat="server">
        <div style="width:60%; margin-left:20%;">
            <img name="Grass" src="App_Themes/images/HeaderNewColor.jpg" alt="Grass" align="left" width="100%" <="" div="" style="
    margin-bottom: 5%;" /></div>
      <div style="width: 50%;height: 20%;margin-left: 24%; padding: 20px; background-color:#FFFFFF;clear: both; margin-top:5%;/*box-shadow: darkblue 0px 0px 3px 3px;*/">
          <div style="text-align:center; color:gray; font-size:30px; margin-bottom:30px;">Challan could not be processed due to irrevalent information received.</div>
          <div  style="text-align:center;  color:gray; padding:10px;font-size:19px;">Please contact to concern department.<br />
           <%--   <div style="color:blue; padding:7px;">e-to-rj@nic.in</div> <br />
              Phone no- 0141-5111007,5111010
              
          </div>--%>
          <%--<div style="color:blue; text-align:center; font-size:20px; padding-bottom:10px;text-decoration:underline;"><linkbutton id="lbkBacktoEgras" runat="server">Take me back to home</linkbutton></div>--%>
        </div>
          </div>
    <div style="text-align: center;margin-top: 30%;color: gray;">
        <asp:Label ID="lblError" runat="server" Text="Invalid Request Found.....!!"></asp:Label>
    </div>
    </form>
</body>
</html>
