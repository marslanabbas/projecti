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
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowMessage("Please fill all fields.", "error");
                return;
            }

            if (password != confirmPassword)
            {
                ShowMessage("Passwords do not match.", "error");
                return;
            }

            
            string connString = ConfigurationManager.ConnectionStrings["BookShopDB_Conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    
                    string checkQuery = "SELECT COUNT(*) FROM tblUsers WHERE Username = @Uname OR Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Uname", username);
                        checkCmd.Parameters.AddWithValue("@Email", email);

                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            ShowMessage("Username or Email already exists. Please try another.", "error");
                            return;
                        }
                    }

                   
                    string insertQuery = "INSERT INTO tblUsers (Username, Password, Email, Role) VALUES (@Uname, @Upass, @Email, 'Customer')";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Uname", username);
                        insertCmd.Parameters.AddWithValue("@Upass", password);
                        insertCmd.Parameters.AddWithValue("@Email", email);
                       

                        insertCmd.ExecuteNonQuery();

                        ShowMessage("Registration Successful! You can now login.", "success");

                        
                        txtUsername.Text = "";
                        txtEmail.Text = "";
                        txtPassword.Text = ""; 
                        txtConfirmPassword.Text = ""; 
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Database error: " + ex.Message, "error");
                }
            }
        }

        
        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            if (type == "success")
            {
                lblMessage.CssClass = "message-label success-message";
            }
            else
            {
                lblMessage.CssClass = "message-label error-message";
            }
        }
    }
}