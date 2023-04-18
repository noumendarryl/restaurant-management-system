using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOStock : DAOEntity<Stock>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        private string name;
        private string category;
        private int quantity;
        private List<Stock> stocks;

        public DAOStock(SqlConnection connection) : base(connection)
        {
            
        }

        /*
		 * Find a stock of ingredients according to its name
		 */
        override
        public Stock find(string stockTitle)
        {
            command = new SqlCommand("SELECT * FROM dbo.Stock WHERE nom = @nomStock", getConnection());
            command.Parameters.AddWithValue("@nomStock", stockTitle);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[1].ToString();
                        quantity = (int)reader[2];
                        category = reader[3].ToString();
                    }
                    reader.Close();
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new Stock(name, quantity, category);
        }

        /*
		 * Get a list of all ingredient stocks for a given recipe
		 */
        override
        public List<Stock> find(int id)
        {
            stocks = new List<Stock>();
            command = new SqlCommand("SELECT nom, quantite, categorie FROM (dbo.Recette INNER JOIN dbo.utilise ON (dbo.Recette.id_recette = dbo.utilise.id_recette) INNER JOIN dbo.Stock ON ((dbo.utilise.id_ingredient = dbo.Stock.id_ingredient))) WHERE dbo.Recette.id_recette = @id", getConnection());
            command.Parameters.AddWithValue("@id_recette", id);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[1].ToString();
                        quantity = (int)reader[2];
                        category = reader[3].ToString();
                        stocks.Add(new Stock(name, quantity, category));
                    }
                    reader.Close();
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return stocks;
        }

        /*
		 * Update the quantity of a stock
		 */
        override
        public void update(Stock stock)
        {
            try
            {
                command = new SqlCommand("UPDATE dbo.Stock SET quantite = @quantite WHERE id = @id", getConnection());
                command.Parameters.AddWithValue("@quantite", stock.quantity);
                command.Parameters.AddWithValue("@id", stock.id);
                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
		 * Create a new stock of ingredients
		 *
		 */
        override
        public void create(Stock stock)
        {
            command = new SqlCommand("INSERT INTO dbo.Stock (nom, quantite, categorie) " +
                    "VALUES (@nom, @quantite, @categorie)", getConnection());

            try
            {
                command.Parameters.AddWithValue("@nom", stock.stockTitle);
                command.Parameters.AddWithValue("@quantite", stock.quantity);
                command.Parameters.AddWithValue("@categorie", stock.category);

                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
		 * Delete a particular stock
		 */
        override
        public void delete(int id)
        {
            try
            {
                command = new SqlCommand("DELETE FROM dbo.Stock WHERE id = @id", getConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
