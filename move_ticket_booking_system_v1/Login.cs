using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace move_ticket_booking_system_v1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Retrieve the email and password entered by the user
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Connection string - replace with your actual database connection string
            //string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=movieDB;Integrated Security=True;Trust Server Certificate=True";
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=movieDB;Integrated Security=True";

            // SQL query to check if the user exists with the provided email and password
            string query = "SELECT role FROM Users WHERE email = @Email AND password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        // Adding parameters to the SQL query
                        adapter.SelectCommand.Parameters.AddWithValue("@Email", email);
                        adapter.SelectCommand.Parameters.AddWithValue("@Password", password);

                        // Filling the result into a DataTable
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            // User exists, check the role
                            string role = dt.Rows[0]["role"].ToString();

                            if (role == "admin")
                            {
                                // Open the admin dashboard
                                manager_dashboard managerDashboard = new manager_dashboard();
                                managerDashboard.Show();
                                this.Hide();
                            }
                            else if (role == "cashier")
                            {
                                // Open the cashier dashboard
                                cashierDashboard cashierView = new cashierDashboard();
                                cashierView.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Unrecognized role. Please contact support.");
                            }
                        }
                        else
                        {
                            // Invalid credentials
                            MessageBox.Show("Invalid email or password. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
