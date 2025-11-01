using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projecti
{
    public partial class Login : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
         
            string connString = ConfigurationManager.ConnectionStrings["BookShopDB_Conn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    
                    string query = "SELECT UserID, Username FROM tblUsers WHERE Username = @Uname AND Password = @Upass AND Role = 'Admin'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        cmd.Parameters.AddWithValue("@Uname", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Upass", txtPassword.Text.Trim());

                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                Session["AdminUsername"] = reader["Username"].ToString();
                                Session["AdminUserID"] = reader["UserID"].ToString();

                                Response.Redirect("AdminDashboard.aspx");
                            }
                            else
                            {
                                
                                lblError.Text = "Error: Invalid Username or Password.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = "Database connection error: " + ex.Message;
                }
            }
        } 
    }
    }

