using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOStock : DAOEntity<Stock>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string name;
        private string category;
        private int quantity;
        private List<Stock> stocks;

        /*
		 * Find a stock of ingredients according to its name
		 */
        public Stock find(string stockTitle)
        {
            da.createSqlCommand("SELECT * FROM dbo.Stock WHERE nom = @nomStock");
            da.getCmd().Parameters.AddWithValue("@nomStock", stockTitle);

            try
            {
                reader = da.executeReader();
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Stock(name, quantity, category);
        }

        /*
		 * Get a list of all ingredient stocks for a given recipe
		 */
        public List<Stock> find(int id)
        {
            stocks = new List<Stock>();
            da.createSqlCommand("SELECT nom, quantite, categorie FROM (dbo.Recette INNER JOIN dbo.utilise ON (dbo.Recette.id_recette = dbo.utilise.id_recette) INNER JOIN dbo.Stock ON ((dbo.utilise.id_ingredient = dbo.Stock.id_ingredient))) WHERE dbo.Recette.id_recette = @id");
            da.getCmd().Parameters.AddWithValue("@id_recette", id);

            try
            {
                reader = da.executeReader();
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return stocks;
        }

        /*
		 * Update the quantity of a stock
		 */
        public void update(Stock stock)
        {
            da.createSqlCommand("UPDATE dbo.Stock SET quantite = @quantite WHERE id = @id");
            da.getCmd().Parameters.AddWithValue("@quantite", stock.quantity);
            da.getCmd().Parameters.AddWithValue("@id", stock.id);
            da.executeNonQuery();
            da.close();
        }

        /*
		 * Create a new stock of ingredients
		 */
        public void create(Stock stock)
        {
            da.createSqlCommand("INSERT INTO dbo.Stock (nom, quantite, categorie) " +
                    "VALUES (@nom, @quantite, @categorie)");

            try
            {
                da.getCmd().Parameters.AddWithValue("@nom", stock.stockTitle);
                da.getCmd().Parameters.AddWithValue("@quantite", stock.quantity);
                da.getCmd().Parameters.AddWithValue("@categorie", stock.category);

                da.executeNonQuery();
                da.close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
		 * Delete a particular stock
		 */
        public void delete(int id)
        {
            da.createSqlCommand("DELETE FROM dbo.Stock WHERE id = @id");
            da.getCmd().Parameters.AddWithValue("@id", id);
            da.executeNonQuery();
            da.close();
        }
    }
}
