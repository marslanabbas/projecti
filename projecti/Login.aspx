<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="projecti.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="Login.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        
        <div class="login-container">
            <h1>Online Book Shop</h1>
            <h2>Login</h2>
            
            <div class="form-group">
                <asp:Label ID="lblUsernameLabel" runat="server" Text="Username"></asp:Label>
                <div class="input-with-icon">
                    <i class="fas fa-user icon"></i>
                    <asp:TextBox ID="txtUsername" runat="server" placeholder="Type your username"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblPasswordLabel" runat="server" Text="Password"></asp:Label>
                <div class="input-with-icon">
                    <i class="fas fa-lock icon"></i>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Type your password"></asp:TextBox>
                </div>
            </div>

            <div class="forgot-password">
                <a href="#">Forgot password?</a>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="LOGIN" OnClick="btnLogin_Click" CssClass="gradient-button" />

            <asp:Label ID="lblError" runat="server" Text="" CssClass="error-message"></asp:Label>
            <br />

            <div class="signup-link">
                Or Sign Up Using<br />
                <a href="Signup.aspx">SIGN UP</a>
            </div>

        </div>
    </form>
</body>
</html>
