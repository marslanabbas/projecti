using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projecti
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["BookShopDB_Conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadCategories(); 
                BindBooksGrid();  
            }
        }

        
        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CategoryID, CategoryName FROM tblCategories ORDER BY CategoryName";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                   
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataTextField = "CategoryName"; 
                    ddlCategory.DataValueField = "CategoryID";  
                    ddlCategory.DataBind();

                    
                    ddlCategory.Items.Insert(0, new ListItem("Select Category ", "0"));
                }
                catch (Exception ex)
                {
                    ShowMessage("Error loading categories: " + ex.Message, "error");
                }
            }
        }

       
        private void BindBooksGrid()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    
                    string query = @"SELECT 
                                        b.BookID, b.Title, b.Author, b.Price, b.Stock, b.ImageURL, 
                                        c.CategoryName 
                                     FROM 
                                        tblBooks b
                                     LEFT JOIN 
                                        tblCategories c ON b.CategoryID = c.CategoryID
                                     ORDER BY 
                                        b.BookID DESC"; 

                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    gvBooks.DataSource = dt;
                    gvBooks.DataBind();
                }
                catch (Exception ex)
                {
                    ShowMessage("Error loading books: " + ex.Message, "error");
                }
            }
        }

        
        protected void btnSaveBook_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                ShowMessage("Book Title is required.", "error");
                return;
            }
            if (ddlCategory.SelectedValue == "0") 
            {
                ShowMessage("Please select a category.", "error");
                return;
            }
            if (!fuBookImage.HasFile)
            {
                ShowMessage("Please upload a cover image.", "error");
                return;
            }

            
            string imagePath = "";
            string fileName = "";
            try
            {
                fileName = Path.GetFileName(fuBookImage.FileName); 
               
                string serverPath = Server.MapPath("~/Uploads/") + fileName;
                fuBookImage.SaveAs(serverPath);

               
                imagePath = "~/Uploads/" + fileName;
            }
            catch (Exception ex)
            {
                ShowMessage("Error uploading image: " + ex.Message, "error");
                return; 
            }

            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO tblBooks 
                                     (Title, Author, Price, Stock, ImageURL, CategoryID) 
                                   VALUES 
                                     (@Title, @Author, @Price, @Stock, @ImageURL, @CategoryID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.AddWithValue("@ImageURL", imagePath); 
                        cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(ddlCategory.SelectedValue));

                        cmd.ExecuteNonQuery();

                        ShowMessage("Book saved successfully!", "success");

                        BindBooksGrid(); 
                        ClearForm(); 
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Error saving book: " + ex.Message, "error");
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

        
        private void ClearForm()
        {
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            ddlCategory.SelectedIndex = 0;
        }
    }
}