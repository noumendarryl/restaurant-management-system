using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Siticone.Desktop.UI.WinForms;

namespace AppRestaurant.View.Common
{
    public partial class Booking : Form
    {
        private NewBooking newBooking;
        public DBAccess da;
        public int indexRow;
        public DataGridViewRow row;

        public Booking()
        {
            da = new DBAccess();
            InitializeComponent();
        }

        /*
		 * Load the bookings' table
		 */
        public void loadBookings()
        {
            using (DataTable dt = new DataTable("ReservationTable"))
            {
                da.createSqlCommand("SELECT nom_client, nb_personnes, horaire FROM dbo.ReservationTable");
                SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.RowCount <= 10)
                {
                    siticoneHtmlLabel1.Text = $"Total records : 00{dataGridView1.RowCount - 1}";
                }
                else if (10 < dataGridView1.RowCount && dataGridView1.RowCount < 100)
                {
                    siticoneHtmlLabel1.Text = $"Total records : 0{dataGridView1.RowCount - 1}";
                }
                else
                {
                    siticoneHtmlLabel1.Text = $"Total records : {dataGridView1.RowCount - 1}";
                }
            }
        }

        /*
		 * Refresh the bookings' table
		 */
        private void refreshBooking(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("ReservationTable"))
            {
                da.createSqlCommand("SELECT nom_client, nb_personnes, horaire FROM dbo.ReservationTable");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    if (dataGridView1.RowCount <= 10)
                    {
                        siticoneHtmlLabel1.Text = $"Total records : 00{dataGridView1.RowCount - 1}";
                    }
                    else if (10 < dataGridView1.RowCount && dataGridView1.RowCount < 100)
                    {
                        siticoneHtmlLabel1.Text = $"Total records : 0{dataGridView1.RowCount - 1}";
                    }
                    else
                    {
                        siticoneHtmlLabel1.Text = $"Total records : {dataGridView1.RowCount - 1}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
		 * Search for a specific booking
		 */
        private void searchBooking(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("ReservationTable"))
            {
                da.createSqlCommand("SELECT nom_client, nb_personnes, horaire FROM dbo.ReservationTable WHERE nom_Client=@nomClient");
                try
                {
                    da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox2.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    if (dataGridView1.RowCount <= 10)
                    {
                        siticoneHtmlLabel1.Text = $"Total records : 00{dataGridView1.RowCount - 1}";
                    }
                    else if (10 < dataGridView1.RowCount && dataGridView1.RowCount < 100)
                    {
                        siticoneHtmlLabel1.Text = $"Total records : 0{dataGridView1.RowCount - 1}";
                    }
                    else
                    {
                        siticoneHtmlLabel1.Text = $"Total records : {dataGridView1.RowCount - 1}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void siticoneTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter button
            if (e.KeyChar == (char)13)
            {
                iconButton2.PerformClick();
            }
        }

        /*
		 * Export the bookings' table as a PDF document
		 */
        private void exportBooking(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Bookings.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Unable to write data in disk" + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(column.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell dCell in row.Cells)
                                {
                                    pTable.AddCell(dCell.Value.ToString());
                                }
                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Data Export Successfully !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data : " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No record Found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       /*
		 * Add a booking
		 */
        private void addBooking(object sender, EventArgs e)
        {
            newBooking = new NewBooking();
            newBooking.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            row = dataGridView1.Rows[indexRow];

            newBooking.siticoneTextBox1.Text = row.Cells[0].Value.ToString();
            newBooking.siticoneNumericUpDown1.Value = (int)row.Cells[1].Value;
            newBooking.siticoneDateTimePicker1.Value = (DateTime)row.Cells[2].Value;
        }

        /*
		 * Edit a booking
		 */
        private void editBooking(object sender, EventArgs e)
        {
            newBooking = new NewBooking();
            newBooking.siticoneButton2.Visible = false;
            newBooking.siticoneButton3.Visible = true;
            newBooking.Show();
        }

        /*
		 * Delete a booking
		 */
        private void deleteBooking(object sender, EventArgs e)
        {

        }

    }
}
