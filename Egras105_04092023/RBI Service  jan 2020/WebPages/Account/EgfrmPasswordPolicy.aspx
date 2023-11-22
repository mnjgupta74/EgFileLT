<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage3.master" AutoEventWireup="true"
    CodeFile="EgfrmPasswordPolicy.aspx.cs" Inherits="WebPages_Account_EgfrmPasswordPolicy"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td align="center" style="background-color: #218EDB;">
                <b>Password Policies: </b>
            </td>
        </tr>
        <tr>
            <td>
                1. Password should contain atleast 6 characters.
            </td>
        </tr>
        <tr>
            <td>
                2. Password should contain atleast one numeric digit.
            </td>
        </tr>
        <tr>
            <td>
                3. Password should contain atleast one Capital Letter.
            </td>
        </tr>
        <tr>
            <td>
                4. Password should contain atleast one special character from !@#$*_-~=.
            </td>
        </tr>
    </table>
</asp:Content>
