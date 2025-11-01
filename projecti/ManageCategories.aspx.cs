using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projecti
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        
        string connString = ConfigurationManager.ConnectionStrings["BookShopDB_Conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                
                BindGridView();
            }
        }

        
        protected void btnSaveCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            
            if (string.IsNullOrEmpty(categoryName))
            {
                ShowMessage("Category Name cannot be empty.", "error");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    
                    string query = "INSERT INTO tblCategories (CategoryName) VALUES (@CatName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CatName", categoryName);

                        cmd.ExecuteNonQuery(); 

                        ShowMessage("Category saved successfully!", "success");

                        txtCategoryName.Text = "";

                        BindGridView(); 
                    }
                }
                catch (Exception ex)
                {
                   
                    ShowMessage("Error saving category: " + ex.Message, "error");
                }
            }
        }

       
        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CategoryID, CategoryName FROM tblCategories ORDER BY CategoryName";

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); 

                        gvCategories.DataSource = dt; 
                        gvCategories.DataBind(); 
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error loading categories: " + ex.Message, "error");
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

        protected void gvCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            gvCategories.EditIndex = e.NewEditIndex;

            
            BindGridView();
        }

        
        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            
            gvCategories.EditIndex = -1;

           
            BindGridView();
        }

       
        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                
                int categoryID = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);

                
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM tblCategories WHERE CategoryID = @ID"; 
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", categoryID);
                        cmd.ExecuteNonQuery();
                    }
                }

                
                BindGridView();
                ShowMessage("Category deleted successfully.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error deleting category: " + ex.Message, "error");
            }
        }

        
        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                
                int categoryID = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);

               
                TextBox txtName = (TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtEditCategoryName");
                string newCategoryName = txtName.Text;

            
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "UPDATE tblCategories SET CategoryName = @Name WHERE CategoryID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", newCategoryName);
                        cmd.Parameters.AddWithValue("@ID", categoryID);
                        cmd.ExecuteNonQuery();
                    }
                }

                
                gvCategories.EditIndex = -1;
                BindGridView();
                ShowMessage("Category updated successfully.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating category: " + ex.Message, "error");
            }
        }
    }
}


    