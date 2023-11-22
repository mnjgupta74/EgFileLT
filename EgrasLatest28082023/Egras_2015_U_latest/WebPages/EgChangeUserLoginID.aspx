<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="EgChangeUserLoginID.aspx.cs" Inherits="WebPages_EgChangeUserLoginID"
    Title="Untitled Page" %>
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" type="text/css">

    <style>
        .bg {
            background-color: #008fd3;
            padding: 20px;
            text-align: center;
            border-radius: 10px;
            font-size: 30px;
            color: white;
        }

        .container {
            text-align: center;
            margin-top: 90px;
        }

        .strike {
            margin-top: 40px;
        }

        .code {
            background-color: #e9f8ff;
            padding: 8px 15px 8px 15px;
            text-align: center;
            border-radius: 10px;
            font-size: 30px;
            color: #008fd3;
            font-weight: bold;
            box-shadow: 1px 2px 2px 0px #e4e4e4;
        }

        .btn {
            background-color: #20a0da;
            font-size: 20px;
            color: white !important;
            background-color: #20a0da !important;
            height: 5% !important;
           
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-sm-3">
                    <img class="strike" src="../Image/strike.png" />
                </div>

                <div class="col-sm-6">

                    <div class="bg">

                        <span>Your LoginID Has been Changed</span>
                    </div>
                </div>

                <div class="col-sm-3">

                    <img class="strike" src="../Image/strike.png" />
                </div>
            </div>


            <br />
            <br />

            <h1>Your New LoginID is:
            <span class="code" runat="server" id="spntxt"></span>


            </h1>

            <br />
            <br />


            <h1 style="font-size: 20px; text-decoration: underline;">Sign In With Your New Login ID </h1>

        </div>

        <br />
        <br />

        <div style="text-align: center;">
            <asp:Button ID="btnSubmit" runat="server" class="btn" Text="Continue" Style="font-size: 20px;" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>