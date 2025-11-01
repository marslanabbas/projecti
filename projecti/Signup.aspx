<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="projecti.Signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link rel="stylesheet" href="Signup.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="signup-container">
            
            <h1 class="project-title">Online book shop</h1>
            
            <h2>Create Account</h2>
            
            <div class="form-group">
                <asp:Label ID="lblUsernameLabel" runat="server" Text="Username"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Choose a username"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lblEmailLabel" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Enter your email"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lblPasswordLabel" runat="server" Text="Password"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Create a password"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="lblConfirmPasswordLabel" runat="server" Text="Confirm Password"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm your password"></asp:TextBox>
            </div>

            <asp:Button ID="btnSignUp" runat="server" Text="SIGN UP" OnClick="btnSignUp_Click" CssClass="gradient-button" />

            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message-label"></asp:Label>

            <div class="login-link">
                Already have an account?<br />
                <a href="Login.aspx">LOGIN HERE</a>
            </div>

        </div>
    </form>
</body>
</html>
