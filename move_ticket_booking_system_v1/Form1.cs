using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace move_ticket_booking_system_v1
{
    public partial class manager_dashboard : Form
    {
        public manager_dashboard()
        {
            InitializeComponent();

            loardform(new moviesManager());
            changeDashboardSelectedMenuItemColor(btnMovies);
        }

        public void loardform(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
            {
                this.mainPanel.Controls.RemoveAt(0);
            }

            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }

        private void changeDashboardSelectedMenuItemColor(Button selectedMenuItem)
        {
            // List of all menu buttons
            List<Button> menuButtons = new List<Button> { btnMovies, btnShowTimes, btnSales, btnBookings, btnUsers };

            // Iterate over each button to reset their styles
            foreach (Button button in menuButtons)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
            }

            // Change the color of the selected menu item
            selectedMenuItem.BackColor = ColorTranslator.FromHtml("#E80117");
            selectedMenuItem.ForeColor = Color.White;
        }

        private void btnBookings_Click_1(object sender, EventArgs e)
        {
            loardform(new bookingsManager());
            changeDashboardSelectedMenuItemColor(btnBookings);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnShowTimes_Click(object sender, EventArgs e)
        {
            loardform(new showtimesManager());
            changeDashboardSelectedMenuItemColor(btnShowTimes);
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            loardform(new moviesManager());
            changeDashboardSelectedMenuItemColor(btnMovies);
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            loardform(new salesManager());
            changeDashboardSelectedMenuItemColor(btnSales);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            loardform(new usersManager());
            changeDashboardSelectedMenuItemColor(btnUsers);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
