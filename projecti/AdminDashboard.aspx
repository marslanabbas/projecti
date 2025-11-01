<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="projecti.WebForm1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>AdminDashboard</title>
    <link rel="stylesheet" href="AdminDashboard.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h2>Welcome, Admin!</h2>
  <p>Here is the summary overview of your Online Book Shop.</p>
  <br /><hr /><br />

  <div class="dashboard-container">

      <div class="stat-box">
          <h3>Total Books</h3>
          <asp:Label ID="lblTotalBooks" runat="server" Text="0" CssClass="stat-number" />
      </div>

      <div class="stat-box">
          <h3>Total Customers</h3>
          <asp:Label ID="lblTotalCustomers" runat="server" Text="0" CssClass="stat-number" />
      </div>

      <div class="stat-box">
          <h3>Pending Orders</h3>
          <asp:Label ID="lblPendingOrders" runat="server" Text="0" CssClass="stat-number" />
      </div>
      
      <div class="stat-box">
          <h3>Total Categories</h3>
          <asp:Label ID="lblTotalCategories" runat="server" Text="0" CssClass="stat-number" />
      </div>

  </div>
  

</asp:Content>



