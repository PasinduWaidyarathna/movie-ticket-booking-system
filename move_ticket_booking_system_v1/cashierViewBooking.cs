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
    public partial class cashierViewBooking : Form
    {
        public cashierViewBooking()
        {
            InitializeComponent();
            dsViewBooking1.Clear();
            sqlDataAdapter1.Fill(dsViewBooking1);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search keyword from the search TextBox
            string searchValue = txtSearch.Text.Trim();

            // Use DataView to filter the dataset based on the title
            DataView dv = new DataView(dsViewBooking1.Tables[0]); // Assuming dsMovies1 is the DataSet containing your movie data

            // Apply the filter (you can modify this to match exactly or use a LIKE query)
            dv.RowFilter = string.Format("title LIKE '%{0}%'", searchValue);

            // Bind the filtered data to the DataGridView
            dataGridView1.DataSource = dv;
        }
    }
}
