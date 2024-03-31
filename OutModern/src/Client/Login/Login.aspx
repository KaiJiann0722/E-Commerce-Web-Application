﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OutModern.src.Client.Login.Login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>OutModern | Login</title>

    <!-- import font type css-->
    <link href="<%=Page.ResolveClientUrl("~/lib/fonts/ChakraPetch/ChakraPetch.css") %>" rel="stylesheet" />

    <!-- import fontawesome css-->
    <link href="<%= Page.ResolveClientUrl("~/lib/fontawesome/css/all.min.css") %>" rel="stylesheet" />

    <link href="css/Login.css" rel="stylesheet" />

    <!-- import tailwind js-->
    <script src="<%= Page.ResolveClientUrl("~/lib/tailwind/tailwind.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="column1"></div>
            <div class="column2">

                <div class="topRightBox">
                    <asp:HyperLink ID="hl_admin_login" runat="server" NavigateUrl="~/src/Client/Login/AdminLogin.aspx">
                    <span class="hl_admin_login_text"><u>Login as Admin</u></span>
                    </asp:HyperLink>
                </div>

                <div class="rightBox">
                    <span class="title">Log In</span>

                    <div class="loginBoxItem">
                        <asp:TextBox ID="txt_email" runat="server" class="loginTextBox" placeholder="Email" TextMode="Email"></asp:TextBox>
                    </div>

                    <div class="loginBoxItem">
                        <asp:TextBox ID="txt_password" runat="server" class="loginTextBox" TextMode="Password" placeholder="Password"></asp:TextBox>
                    </div>

                    <div class="loginBoxItem1">
                        <asp:CheckBox ID="chkbox_login" runat="server" Text="&nbsp;Keep me logged in" class="chkLogin" />
                    </div>

                    <div class="loginBoxButton">
                        <asp:Button ID="btn_login" runat="server" Text="Log In" CssClass="bg-black hover:bg-gray-700" Style="width: 100%; border-radius: 10px; padding: 10px; font-family: sans-serif; color: white; font-weight: bold; border: 1px solid #f5f5f5; margin-top: 10px; cursor: pointer;" OnClick="btn_login_Click" />
                    </div>

                    <div style="padding-top: 3px; padding-bottom: 30px;">
                        <asp:HyperLink ID="hl_forget_password" runat="server" class="hl_forget_password" NavigateUrl="~/src/Client/Login/ForgetPassword.aspx">Forget Password</asp:HyperLink>
                    </div>

                    <div style="padding: 10px; color: darkgrey;">
                        <hr />
                    </div>


                    <div style="padding-top: 10px; padding-bottom: 25px;" class="loginBoxBottom">
                        <span style="margin-top: 20px; padding-left: 85px; font-family: sans-serif; font-size: 15px; color: #b3b3b3;">New to Out Modern?</span>
                        <asp:HyperLink ID="hl_signup" runat="server" class="hl_signup" NavigateUrl="~/src/Client/Login/SignUp.aspx">Sign Up</asp:HyperLink>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>