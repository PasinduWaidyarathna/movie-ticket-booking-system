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
    public partial class NewBookingForm : Form
    {
        public static NewBookingForm instance;
        int available_seats = 0;
        float ticket_price = 0;
        float total_price = 0;
        string movie_id = "";
        string showtime_id = "";

        public NewBookingForm()
        {
            InitializeComponent();
            instance = this;
            disableTxtFieldsAndLoadData();
        }

        public void disableTxtFieldsAndLoadData()
        {
            textMovie.Enabled = false;
            textDate.Enabled = false;
            textPrice.Enabled = false;
            textTime.Enabled = false;
            textTotal.Enabled = false;

            textMovie.Text = cashierNewBooking.instance.movie;
            textDate.Text = cashierNewBooking.instance.date;
            textPrice.Text = cashierNewBooking.instance.ticket_price;
            textTime.Text = cashierNewBooking.instance.time;
            movie_id = cashierNewBooking.instance.movie_id1;
            showtime_id = cashierNewBooking.instance.showtime_id;
            //total_price = float.Parse(cashierNewBooking.instance.ticket_price);
            //available_seats = int.Parse(cashierNewBooking.instance.available_seats1);

        }

        private void SaveBookingToDatabase(string movieId, string showtimeId, int numOfSeats)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=movieDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            // Define the queries
            string updateSeatsQuery = "UPDATE showtimes SET available_seats = available_seats - @NumOfSeats WHERE movie_id = @MovieId AND showtime_id = @ShowtimeId";
            string insertBookingQuery = "INSERT INTO tickets (movie_id, showtime_id, purchase_date) VALUES (@MovieId, @ShowtimeId, @BookingDate)";
            string insertSalesQuery = "INSERT INTO sales (showtime_id, tickets_sold, total_income) VALUES (@ShowtimeId, @NumOfSeats, @TotalIncome)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // Update available seats
                    using (SqlCommand updateSeatsCommand = new SqlCommand(updateSeatsQuery, connection, transaction))
                    {
                        updateSeatsCommand.Parameters.AddWithValue("@MovieId", movieId);
                        updateSeatsCommand.Parameters.AddWithValue("@ShowtimeId", showtimeId);
                        updateSeatsCommand.Parameters.AddWithValue("@NumOfSeats", numOfSeats);
                        updateSeatsCommand.ExecuteNonQuery();
                    }

                    // Insert booking record
                    using (SqlCommand insertBookingCommand = new SqlCommand(insertBookingQuery, connection, transaction))
                    {
                        insertBookingCommand.Parameters.AddWithValue("@MovieId", movieId);
                        insertBookingCommand.Parameters.AddWithValue("@ShowtimeId", showtimeId);
                        insertBookingCommand.Parameters.AddWithValue("@BookingDate", DateTime.Today);
                        insertBookingCommand.ExecuteNonQuery();
                    }

                    // Insert sales record
                    using (SqlCommand insertSalesCommand = new SqlCommand(insertSalesQuery, connection, transaction))
                    {
                        insertSalesCommand.Parameters.AddWithValue("@ShowtimeId", showtimeId);
                        insertSalesCommand.Parameters.AddWithValue("@NumOfSeats", numOfSeats);
                        insertSalesCommand.Parameters.AddWithValue("@TotalIncome", ticket_price * numOfSeats); // Assuming ticket_price is a class-level variable

                        insertSalesCommand.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                    MessageBox.Show("Booking saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Roll back the transaction if an error occurs
                    if (transaction != null)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception rollbackEx)
                        {
                            MessageBox.Show("An error occurred while rolling back the transaction: " + rollbackEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("An error occurred while saving the booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void NewBookingForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numOfSeats;
            bool isValidNumber = int.TryParse(textNoTickets.Text, out numOfSeats);

            if (!isValidNumber)
            {
                MessageBox.Show("Please enter a valid number of tickets.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numOfSeats < available_seats)
            {
                total_price = ticket_price * numOfSeats;
                textTotal.Text = total_price.ToString();

                // update db 
                SaveBookingToDatabase(movie_id, showtime_id, numOfSeats);

                this.Close();
            }
            else
            {
                // Show error if requested seats exceed available seats
                MessageBox.Show("The number of tickets requested exceeds the available seats. Please enter a number less than or equal to the available seats.", "Seats Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddSeats_Click(object sender, EventArgs e)
        {
            int numOfSeats;
            bool isValidNumber = int.TryParse(textNoTickets.Text, out numOfSeats);

            if (!isValidNumber)
            {
                MessageBox.Show("Please enter a valid number of tickets.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (numOfSeats < available_seats)
            {
                total_price = ticket_price * numOfSeats;
                textTotal.Text = total_price.ToString();
            }
            else
            {
                // Show error if requested seats exceed available seats
                MessageBox.Show("The number of tickets requested exceeds the available seats. Please enter a number less than or equal to the available seats.", "Seats Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
