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

namespace AppRestaurant.View.Common
{
    public partial class Booking : Form
    {
        private NewBooking newBooking;
        public DBAccess da;

        public Booking()
        {
            //da = new DBAccess();
            InitializeComponent();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            newBooking = new NewBooking();
            newBooking.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Reservations"))
            {
                da.createSqlCommand("SELECT * FROM dbo.Reservations");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Reservations"))
                {
                    da.createSqlCommand("SELECT * FROM dbo.Reservations WHERE nom_Client=@nomClient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox2.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        siticoneHtmlLabel1.Text = $"Total records : {dataGridView1.RowCount}";
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void siticoneTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter button
            if (e.KeyChar == (char)13) {
                iconButton2.PerformClick();
            } 
        }

        private void iconButton6_Click(object sender, EventArgs e)
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
                            
                            foreach(DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(column.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach(DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach(DataGridViewCell dCell in row.Cells)
                                {
                                    pTable.AddCell(dCell.Value.ToString());
                                }
                            }

                            using(FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
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

        private void iconButton3_Click(object sender, EventArgs e)
        {

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
