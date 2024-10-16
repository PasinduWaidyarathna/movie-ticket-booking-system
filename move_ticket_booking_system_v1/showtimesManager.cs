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
    public partial class showtimesManager : Form
    {
        public showtimesManager()
        {
            InitializeComponent();
            dsShowTime1.Clear();
            sqlDataAdapter1.Fill(dsShowTime1);
        }
        public void disableAllButtons()
        {
            btnSearch.Enabled = false;
            btnAddNewShowTime.Enabled = false;
            btnDelete.Enabled = false;
            btn_save.Enabled = false;
            btnUpdate.Enabled = false;

        }
        public void enableAllButtons()
        {
            btnSearch.Enabled = true;
            btnAddNewShowTime.Enabled = true;
            btnDelete.Enabled = true;
            btn_save.Enabled = true;
            btnUpdate.Enabled = true;

        }
        private void btnAddNewShowTime_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            disableAllButtons();
            btn_save.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            disableAllButtons();
            btn_save.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to delete this row? ", "Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    sqlDataAdapter1.Update(dsShowTime1);
                }
            }
            catch
            {
                MessageBox.Show("Please select the whole row instead of a cell");
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(dsShowTime1);
            dataGridView1.ReadOnly = true;
            enableAllButtons();
            btn_save.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search keyword from the search TextBox and parse it to an integer
            int searchValue;
            if (int.TryParse(txtSearch.Text.Trim(), out searchValue))
            {
                // Use DataView to filter the dataset based on the movie_id
                DataView dv = new DataView(dsShowTime1.Tables[0]); // Assuming dsShowTime1 is the DataSet containing your movie data

                // Apply the filter for an exact match on movie_id
                dv.RowFilter = string.Format("movie_id = {0}", searchValue);

                // Bind the filtered data to the DataGridView
                dataGridView1.DataSource = dv;
            }
            else
            {
                // Handle the case where the input is not a valid integer
                MessageBox.Show("Please enter a valid movie ID.");
            }
        }
    }
}
