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
		 * Update stock quantity
		 */
        public void update(int id, int quantity)
        {
            da.createSqlCommand("UPDATE dbo.Stock SET quantity = @newquantity WHERE id = @id");
            da.getCmd().Parameters.AddWithValue("@newquantity", quantity);
            da.getCmd().Parameters.AddWithValue("@id", id);
            da.executeNonQuery();
            da.close();
        }
    }
}
