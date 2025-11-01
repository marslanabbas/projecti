<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs" Inherits="projecti.ManageOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="ManageOrders.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Manage Customer Orders</h2>
    <p>Here you can view all customer orders and their status.</p>
    
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message-label" EnableViewState="False"></asp:Label>
    
    <asp:GridView ID="gvOrders" runat="server" 
        CssClass="grid-view"
        AutoGenerateColumns="False"
        DataKeyNames="OrderID" 
        
        OnRowEditing="gvOrders_RowEditing"
        OnRowCancelingEdit="gvOrders_RowCancelingEdit"
        OnRowUpdating="gvOrders_RowUpdating"
        OnRowDeleting="gvOrders_RowDeleting"
        OnRowDataBound="gvOrders_RowDataBound" 
        >
        
        <Columns>
            <asp:BoundField DataField="OrderID" HeaderText="Order ID" ReadOnly="True" />
            
            <asp:BoundField DataField="Username" HeaderText="Customer" ReadOnly="True" />
            
            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" 
                DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
            
            <asp:BoundField DataField="TotalAmount" HeaderText="Total (Rs.)" 
                DataFormatString="{0:N0}" ReadOnly="True" />

            
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem>Pending</asp:ListItem>
                        <asp:ListItem>Shipped</asp:ListItem>
                        <asp:ListItem>Delivered</asp:ListItem>
                        <asp:ListItem>Cancelled</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            
            <asp:CommandField 
                ShowEditButton="True" 
                ShowCancelButton="True" 
                ShowDeleteButton="True" 
                ButtonType="Link" 
                EditText="Edit" 
                DeleteText="Delete" 
                UpdateText="Update" />
        </Columns>

        <EmptyDataTemplate>
            No orders found.
        </EmptyDataTemplate>

    </asp:GridView>
</asp:Content>