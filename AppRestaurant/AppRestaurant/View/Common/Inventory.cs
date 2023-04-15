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
using AppRestaurant.Model.DB;

namespace AppRestaurant.View.Common
{
    public partial class Inventory : Form
    {
        public DBAccess da;

        public Inventory()
        {
            //da = new DBAccess();
            InitializeComponent();
        }

        private void siticoneTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter button
            if (e.KeyChar == (char)13)
            {
                iconButton4.PerformClick();
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

        private void siticoneTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter button
            if (e.KeyChar == (char)13)
            {
                iconButton9.PerformClick();
            }
        }

        private void siticoneTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter button
            if (e.KeyChar == (char)13)
            {
                iconButton6.PerformClick();
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Materiels"))
            {
                da.createSqlCommand("SELECT * FROM dbo.Materiels");
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
                using (DataTable dt = new DataTable("Materiels"))
                {
                    da.createSqlCommand("SELECT * FROM dbo.Reservations WHERE nom_Client=@nomClient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox2.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                        siticoneHtmlLabel1.Text = $"Total records : {dataGridView1.RowCount}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Ingredients"))
            {
                da.createSqlCommand("SELECT * FROM dbo.Ingredients");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Ingredients"))
                {
                    da.createSqlCommand("SELECT * FROM dbo.Reservations WHERE nom_Client=@nomClient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox1.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView2.DataSource = dt;
                        siticoneHtmlLabel2.Text = $"Total records : {dataGridView2.RowCount}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Commande"))
            {
                da.createSqlCommand("SELECT * FROM dbo.Commande");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView4.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Commande"))
                {
                    da.createSqlCommand("SELECT * FROM dbo.Reservations WHERE nom_Client=@nomClient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox4.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView4.DataSource = dt;
                        siticoneHtmlLabel4.Text = $"Total records : {dataGridView4.RowCount}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Stock"))
            {
                da.createSqlCommand("SELECT * FROM dbo.Stock");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView3.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Stock"))
                {
                    da.createSqlCommand("SELECT * FROM dbo.Reservations WHERE nom_Client=@nomClient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomClient", siticoneTextBox3.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView3.DataSource = dt;
                        siticoneHtmlLabel3.Text = $"Total records : {dataGridView3.RowCount}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
