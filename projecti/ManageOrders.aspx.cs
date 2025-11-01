using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projecti
{
    public partial class ManageOrders : System.Web.UI.Page
    {
       
        string connString = ConfigurationManager.ConnectionStrings["BookShopDB_Conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView(); 
            }
        }

        
        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    
                    string query = @"SELECT 
                                        O.OrderID, 
                                        U.Username, 
                                        O.OrderDate, 
                                        O.TotalAmount, 
                                        O.Status 
                                     FROM 
                                        tblOrders O
                                     JOIN 
                                        tblUsers U ON O.UserID = U.UserID
                                     ORDER BY 
                                        O.OrderDate DESC";

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvOrders.DataSource = dt;
                        gvOrders.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error loading orders: " + ex.Message, "error");
                }
            }
        }

        
        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrders.EditIndex = e.NewEditIndex;
            BindGridView(); 
        }

        
        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrders.EditIndex = -1; 
            BindGridView();
        }

       
        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int orderID = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

                DropDownList ddlStatus = (DropDownList)gvOrders.Rows[e.RowIndex].FindControl("ddlStatus");
                string newStatus = ddlStatus.SelectedValue;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE tblOrders SET Status = @Status WHERE OrderID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@ID", orderID);
                        cmd.ExecuteNonQuery();
                    }
                }

                gvOrders.EditIndex = -1; 
                BindGridView();
                ShowMessage("Order status updated successfully.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating status: " + ex.Message, "error");
            }
        }

       
        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int orderID = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);

                
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE tblOrders SET Status = 'Cancelled' WHERE OrderID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", orderID);
                        cmd.ExecuteNonQuery();
                    }
                }

                BindGridView();
                ShowMessage("Order has been cancelled.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error cancelling order: " + ex.Message, "error");
            }
        }

        
        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow && gvOrders.EditIndex == e.Row.RowIndex)
            {
                try
                {
                    
                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

                    
                    string currentStatus = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                    
                    if (ddlStatus.Items.FindByValue(currentStatus) != null)
                    {
                        ddlStatus.SelectedValue = currentStatus;
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error binding status: " + ex.Message, "error");
                }
            }
        }

        
        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            if (type == "success")
            {
                lblMessage.CssClass = "message-label success";
            }
            else
            {
                lblMessage.CssClass = "message-label error";
            }
        }
    }
}