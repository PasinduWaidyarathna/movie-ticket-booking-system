using System;
using System.Data;
using System.Windows.Forms;

namespace move_ticket_booking_system_v1
{
    public partial class cashierNewBooking : Form
    {
        public static cashierNewBooking instance;
        public string movieId = "";
        public string movie = "";
        public string date = "";
        public string time = "";
        public string ticket_price = "";
        public string available_seats1 = "";
        public string movie_id1 = "";
        public string showtime_id = "";


        public cashierNewBooking()
        {
            InitializeComponent();
            instance = this;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search keyword from the search TextBox
            string searchValue = txtSearch.Text.Trim();

            // Use DataView to filter the dataset based on the title
            DataView dv = new DataView(dsBooking1.Tables[0]); // Assuming dsBooking111 is the DataSet containing your booking data

            // Apply the filter (you can modify this to match exactly or use a LIKE query)
            dv.RowFilter = string.Format("title LIKE '%{0}%'", searchValue);

            // Bind the filtered data to the DataGridView
            dataGridView1.DataSource = dv;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Book")
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    movie = row.Cells["title"].Value.ToString();
                    date = row.Cells["show_date"].Value.ToString();
                    time = row.Cells["start_time"].Value.ToString();
                    ticket_price = row.Cells["ticketprice"].ToString();
                    available_seats1 = row.Cells["available_seats"].ToString();
                    showtime_id = row.Cells["show_time"].ToString();
                    movie_id1 = row.Cells["movie_id"].ToString();

                    NewBookingForm newBookingForm = new NewBookingForm();
                    newBookingForm.Show();

                    // Show a message box with the values
                    MessageBox.Show($"Button clicked!\nTitle: {movie}\n {ticket_price}");
                }
            }
        }
    }
}
