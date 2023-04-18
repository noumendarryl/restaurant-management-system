using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AppRestaurant.View.Common
{
    public partial class Inventory : Form
    {
        public DBAccess da;

        public Inventory()
        {
            da = new DBAccess();
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

        /*
		 * Load the materials' table
		 */
        public void loadMaterials()
        {
            using (DataTable dt = new DataTable("Materiels"))
            {
                da.createSqlCommand("SELECT nom, quantite, type FROM dbo.Materiels");
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
         *  Refresh the materials' table 
         */
        private void refreshMaterials(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Materiels"))
            {
                da.createSqlCommand("SELECT nom, quantite, type FROM dbo.Materiels");
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
         * Search for a specific material
         */
        private void searchMaterial(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Materiels"))
                {
                    da.createSqlCommand("SELECT nom, quantite, type FROM dbo.Materiels WHERE nom=@nomMateriel");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomMateriel", siticoneTextBox2.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Export the materials' table as a PDF document
         */
        private void exportMaterials(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Materials.pdf";
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
		 * Load the ingredients' table
		 */
        public void loadIngredients()
        {
            using (DataTable dt = new DataTable("Ingredients"))
            {
                da.createSqlCommand("SELECT nom, quantite FROM dbo.Ingredients");
                SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                adapter.Fill(dt);
                dataGridView2.DataSource = dt;
                if (dataGridView2.RowCount <= 10)
                {
                    siticoneHtmlLabel2.Text = $"Total records : 00{dataGridView2.RowCount - 1}";
                }
                else if (10 < dataGridView2.RowCount && dataGridView2.RowCount < 100)
                {
                    siticoneHtmlLabel2.Text = $"Total records : 0{dataGridView2.RowCount - 1}";
                }
                else
                {
                    siticoneHtmlLabel2.Text = $"Total records : {dataGridView2.RowCount - 1}";
                }
            }
        }

        /*
         * Refresh the ingredients' table
         */
        private void refreshIngredients(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Ingredients"))
            {
                da.createSqlCommand("SELECT nom, quantite FROM dbo.Ingredients");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;
                    if (dataGridView2.RowCount <= 10)
                    {
                        siticoneHtmlLabel2.Text = $"Total records : 00{dataGridView2.RowCount - 1}";
                    }
                    else if (10 < dataGridView2.RowCount && dataGridView2.RowCount < 100)
                    {
                        siticoneHtmlLabel2.Text = $"Total records : 0{dataGridView2.RowCount - 1}";
                    }
                    else
                    {
                        siticoneHtmlLabel2.Text = $"Total records : {dataGridView2.RowCount - 1}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * Search for a specific ingredient
         */
        private void searchIngredient(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Ingredients"))
                {
                    da.createSqlCommand("SELECT nom, quantite FROM dbo.Ingredients WHERE nom=@nomIngredient");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomIngredient", siticoneTextBox1.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView2.DataSource = dt;
                        if (dataGridView2.RowCount <= 10)
                        {
                            siticoneHtmlLabel2.Text = $"Total records :  00{dataGridView2.RowCount - 1}";
                        }
                        else if (10 < dataGridView2.RowCount && dataGridView2.RowCount < 100)
                        {
                            siticoneHtmlLabel2.Text = $"Total records : 0{dataGridView2.RowCount - 1}";
                        }
                        else
                        {
                            siticoneHtmlLabel2.Text = $"Total records : {dataGridView2.RowCount - 1}";
                        }
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

        /*
         * Export the ingredients' table as a PDF document
         */
        private void exportIngredients(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Ingredients.pdf";
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
                            PdfPTable pTable = new PdfPTable(dataGridView2.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(column.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow row in dataGridView2.Rows)
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
		 * Load the stock' table
		 */
        public void loadStock()
        {
            using (DataTable dt = new DataTable("Stock"))
            {
                da.createSqlCommand("SELECT nom, quantite, categorie FROM dbo.Stock");
                SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                adapter.Fill(dt);
                dataGridView3.DataSource = dt;
                if (dataGridView3.RowCount <= 10)
                {
                    siticoneHtmlLabel3.Text = $"Total records : 00{dataGridView3.RowCount - 1}";
                }
                else if (10 < dataGridView3.RowCount && dataGridView3.RowCount < 100)
                {
                    siticoneHtmlLabel3.Text = $"Total records : 0{dataGridView3.RowCount - 1}";
                }
                else
                {
                    siticoneHtmlLabel3.Text = $"Total records : {dataGridView3.RowCount - 1}";
                }
            }
        }

        /*
         * Refresh the stock's table
         */
        private void refreshStock(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Stock"))
            {
                da.createSqlCommand("SELECT nom, quantite, categorie FROM dbo.Stock");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView3.DataSource = dt;
                    if (dataGridView3.RowCount <= 10)
                    {
                        siticoneHtmlLabel3.Text = $"Total records : 00{dataGridView3.RowCount - 1}";
                    }
                    else if (10 < dataGridView3.RowCount && dataGridView3.RowCount < 100)
                    {
                        siticoneHtmlLabel3.Text = $"Total records : 0{dataGridView3.RowCount - 1}";
                    }
                    else
                    {
                        siticoneHtmlLabel3.Text = $"Total records : {dataGridView3.RowCount - 1}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * Search for a specific stock
         */
        private void searchStock(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Stock"))
                {
                    da.createSqlCommand("SELECT nom, quantite, categorie FROM dbo.Stock WHERE nom=@nomStock");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomStock", siticoneTextBox3.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView3.DataSource = dt;
                        if (dataGridView3.RowCount <= 10)
                        {
                            siticoneHtmlLabel3.Text = $"Total records : 00{dataGridView3.RowCount - 1}";
                        }
                        else if (10 < dataGridView3.RowCount && dataGridView3.RowCount < 100)
                        {
                            siticoneHtmlLabel3.Text = $"Total records : 0{dataGridView3.RowCount - 1}";
                        }
                        else
                        {
                            siticoneHtmlLabel3.Text = $"Total records : {dataGridView3.RowCount - 1}";
                        }
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

        /*
         * Export the stock's table as a PDF document
         */
        private void exportStock(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Stock.pdf";
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
                            PdfPTable pTable = new PdfPTable(dataGridView3.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView3.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(column.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow row in dataGridView3.Rows)
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
		 * Load the orders' table
		 */
        public void loadOrders()
        {
            using (DataTable dt = new DataTable("Commande"))
            {
                da.createSqlCommand("SELECT titre, num_table, nb_commandes, prix FROM dbo.Commande");
                SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                adapter.Fill(dt);
                dataGridView4.DataSource = dt;
                if (dataGridView4.RowCount <= 10)
                {
                    siticoneHtmlLabel4.Text = $"Total records : 00{dataGridView4.RowCount - 1}";
                }
                else if (10 < dataGridView4.RowCount && dataGridView4.RowCount < 100)
                {
                    siticoneHtmlLabel4.Text = $"Total records : 0{dataGridView4.RowCount - 1}";
                }
                else
                {
                    siticoneHtmlLabel4.Text = $"Total records : {dataGridView4.RowCount - 1}";
                }
            }
        }

        /*
         * Refresh the orders' table 
         */
        private void refreshOrders(object sender, EventArgs e)
        {
            using (DataTable dt = new DataTable("Commande"))
            {
                da.createSqlCommand("SELECT titre, num_table, nb_commandes, prix FROM dbo.Commande");
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                    adapter.Fill(dt);
                    dataGridView4.DataSource = dt;
                    if (dataGridView4.RowCount <= 10)
                    {
                        siticoneHtmlLabel4.Text = $"Total records : 00{dataGridView4.RowCount - 1}";
                    }
                    else if (10 < dataGridView4.RowCount && dataGridView4.RowCount < 100)
                    {
                        siticoneHtmlLabel4.Text = $"Total records : 0{dataGridView4.RowCount - 1}";
                    }
                    else
                    {
                        siticoneHtmlLabel4.Text = $"Total records : {dataGridView4.RowCount - 1}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * Search for a specific order
         */
        private void searchOrder(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = new DataTable("Commande"))
                {
                    da.createSqlCommand("SELECT titre, num_table, nb_commandes, prix FROM dbo.Commande WHERE titre=@nomCommande");
                    try
                    {
                        da.getCmd().Parameters.AddWithValue("@nomCommande", siticoneTextBox4.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(da.getCmd());
                        adapter.Fill(dt);
                        dataGridView4.DataSource = dt;
                        if (dataGridView4.RowCount <= 10)
                        {
                            siticoneHtmlLabel4.Text = $"Total records : 00{dataGridView4.RowCount - 1}";
                        }
                        else if (10 < dataGridView4.RowCount && dataGridView4.RowCount < 100)
                        {
                            siticoneHtmlLabel4.Text = $"Total records : 0{dataGridView4.RowCount - 1}";
                        }
                        else
                        {
                            siticoneHtmlLabel4.Text = $"Total records : {dataGridView4.RowCount - 1}";
                        }
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

        /*
         * Export the orders' table as a PDF document
         */
        private void exportOrders(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Orders.pdf";
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
                            PdfPTable pTable = new PdfPTable(dataGridView4.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView4.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(column.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow row in dataGridView4.Rows)
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
    }
}
