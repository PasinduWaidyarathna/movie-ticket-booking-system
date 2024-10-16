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
    public partial class moviesManager : Form
    {
        public moviesManager()
        {
            InitializeComponent();
            dsMovies1.Clear();
            sqlDataAdapter1.Fill(dsMovies1);
        }

        public void disableAllButtons()
        {
            btnSearch.Enabled = false;
            btnAddNewMovie.Enabled = false;
            btnDelete.Enabled = false;
            btn_save.Enabled = false;
            btnUpdate.Enabled = false;

        }


        public void enableAllButtons()
        {
            btnSearch.Enabled = true;
            btnAddNewMovie.Enabled = true;
            btnDelete.Enabled = true;
            btn_save.Enabled = true;
            btnUpdate.Enabled = true;

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
                    sqlDataAdapter1.Update(dsMovies1);
                }
            }
            catch
            {
                MessageBox.Show("Please select the whole row instead of a cell");
            }
        }

        private void btnAddNewMovie_Click_1(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false;
            disableAllButtons();
            btn_save.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the search keyword from the search TextBox
            string searchValue = txtSearch.Text.Trim();

            // Use DataView to filter the dataset based on the title
            DataView dv = new DataView(dsMovies1.Tables[0]); // Assuming dsMovies1 is the DataSet containing your movie data

            // Apply the filter (you can modify this to match exactly or use a LIKE query)
            dv.RowFilter = string.Format("title LIKE '%{0}%'", searchValue);

            // Bind the filtered data to the DataGridView
            dataGridView1.DataSource = dv;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(dsMovies1);
            dataGridView1.ReadOnly = true;
            enableAllButtons();
            btn_save.Enabled = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
