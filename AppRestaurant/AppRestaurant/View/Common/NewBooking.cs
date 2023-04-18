using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using AppRestaurant.View.Kitchen;

namespace AppRestaurant.View.Common
{
    public partial class NewBooking : Form
    {
        public DBAccess da;
        private DataTable dt;
        private DataRow row;
        private string nameClient;
        private int nbPeople;
        private DateTime hour;

        public NewBooking()
        {
            da = new DBAccess();
            InitializeComponent();
        }

        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Minimize(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Save(object sender, EventArgs e)
        {
            nameClient = siticoneTextBox1.Text;
            nbPeople = (int) siticoneNumericUpDown1.Value;
            hour = siticoneDateTimePicker1.Value;

            dt = KitchenView.booking.dataGridView1.DataSource as DataTable;
            row = dt.NewRow();
            row[0] = nameClient; row[1] = nbPeople; row[2] = hour;

            da.createSqlCommand("INSERT INTO dbo.ReservationTable (nom_client, nb_personnes, horaire) " +
                    "VALUES (@nomClient, @nbPersonnes, @horaire)");

            try
            {
                da.getCmd().Parameters.AddWithValue("@nomClient", nameClient);
                da.getCmd().Parameters.AddWithValue("@nbPersonnes", nbPeople);
                da.getCmd().Parameters.AddWithValue("@horaire", hour);

                da.executeNonQuery();
                dt.Rows.Add(row);
                da.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Edit(object sender, EventArgs e)
        {
            nameClient = siticoneTextBox1.Text;
            nbPeople = (int)siticoneNumericUpDown1.Value;
            hour = siticoneDateTimePicker1.Value;

            int indexRow = KitchenView.booking.indexRow;
            DataGridViewRow newDataRow = KitchenView.booking.dataGridView1.Rows[indexRow];

            da.createSqlCommand("UPDATE dbo.ReservationTable SET nom_client = @nomClient, nb_personnes = @nbPersonnes, horaire = @horaire WHERE nom_client = @oldNomClient");

            try
            {
                da.getCmd().Parameters.AddWithValue("@nomClient", nameClient);
                da.getCmd().Parameters.AddWithValue("@nbPersonnes", nbPeople);
                da.getCmd().Parameters.AddWithValue("@horaire", hour);
                da.getCmd().Parameters.AddWithValue("@oldNomClient", KitchenView.booking.row.Cells[0].Value.ToString());

                da.executeNonQuery();
                newDataRow.Cells[0].Value = nameClient;
                newDataRow.Cells[1].Value = nbPeople;
                newDataRow.Cells[2].Value = hour;
                da.close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
