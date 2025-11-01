<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCategories.aspx.cs" Inherits="projecti.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="ManageCategories.css" />
<title>ManageCateories</title>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Manage Categories</h2>
    <p>Here you can add new categories or view existing ones.</p>

    <div class="form-section">
        <h3>Add New Category</h3>
        
        <div class="form-group">
            <asp:Label ID="lblCategoryName" runat="server" Text="Category Name:"></asp:Label>
            <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Button ID="btnSaveCategory" runat="server" Text="Save Category" 
                OnClick="btnSaveCategory_Click" />
        </div>

        <div class="form-group">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message-label"></asp:Label>
        </div>
    </div>

    <h3>Existing Categories</h3>

    
   <asp:GridView ID="gvCategories" runat="server" 
    CssClass="grid-view"
    AutoGenerateColumns="False" 
    
    DataKeyNames="CategoryID" 
    
    OnRowEditing="gvCategories_RowEditing"
    OnRowDeleting="gvCategories_RowDeleting"
    OnRowUpdating="gvCategories_RowUpdating"
    OnRowCancelingEdit="gvCategories_RowCancelingEdit">
    
    <Columns>
        <asp:BoundField DataField="CategoryID" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Category Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEditCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        
        <asp:CommandField 
            ShowEditButton="True" 
            ShowDeleteButton="True" 
            ButtonType="Link"
            EditText="Edit"
            DeleteText="Delete"
            UpdateText="Update"
            CancelText="Cancel" />
    </Columns>
</asp:GridView>
     </asp:Content>