<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageBooks.aspx.cs" Inherits="projecti.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ManageBooks</title>
    <link rel="stylesheet" href="ManageBooks.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Books</h2>
    <p>Here, you can add new books or view existing ones.</p>

    <div class="form-section">
        <h3>Add New Book</h3>
        
        <div class="form-group">
            <asp:Label ID="lblTitle" runat="server" Text="Book Title:"></asp:Label>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Label ID="lblAuthor" runat="server" Text="Author:"></asp:Label>
            <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" Width="350px">
                
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <asp:Label ID="lblPrice" runat="server" Text="Price (Rs.):"></asp:Label>
            <asp:TextBox ID="txtPrice" runat="server" TextMode="Number" step="10"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblStock" runat="server" Text="Stock (Qty):"></asp:Label>
            <asp:TextBox ID="txtStock" runat="server" TextMode="Number" step="1"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblImage" runat="server" Text="Cover Image:"></asp:Label>
            <asp:FileUpload ID="fuBookImage" runat="server" Width="350px" />
        </div>

        <div class="form-group">
            <asp:Button ID="btnSaveBook" runat="server" Text="Save Book" 
                OnClick="btnSaveBook_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message-label"></asp:Label>
        </div>
    </div>

    <h3>Existing Books</h3>
    
    <asp:GridView ID="gvBooks" runat="server" 
        CssClass="grid-view"
        AutoGenerateColumns="False"
        DataKeyNames="BookID"> 
        
        <Columns>
            <asp:BoundField DataField="BookID" HeaderText="ID" />
            
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="imgBook" runat="server" 
                        ImageUrl='<%# Eval("ImageURL") %>' 
                        CssClass="book-image-thumb" 
                        AlternateText='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" /> 
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:N0}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
        </Columns>
    </asp:GridView>
</asp:Content>
