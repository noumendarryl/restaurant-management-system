using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOIngredient : DAOEntity<Ingredient>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string name;
        private int quantity;
        private List<Ingredient> ingredients;

        /*
		 * Find an ingredient according to its name
		 */
        public Ingredient find(string nameIngredient)
        {
            da.createSqlCommand("SELECT * FROM dbo.Ingredients WHERE nom = @nomIngredient");
            da.getCmd().Parameters.AddWithValue("@nomIngredient", nameIngredient);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[1].ToString();
                        quantity = (int)reader[2];
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Ingredient(name, quantity);
        }

        /*
		 * Get a list of all ingredients of a given recipe
		 */
        public List<Ingredient> find(int id)
        {
            ingredients = new List<Ingredient>();
            da.createSqlCommand("SELECT nom, quantite FROM dbo.utilise INNER JOIN dbo.Ingredients ON (dbo.utilise.id_ingredient = dbo.Ingredients.id_ingredient) WHERE id_recette = @id");
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
                        ingredients.Add(new Ingredient(name, quantity));
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ingredients;
        }

        /*
		 * Update the quantity of an ingredient
		 */
        public void update(Ingredient ingredient)
        {
           da.createSqlCommand("UPDATE dbo.Ingredients SET quantite = @quantite WHERE id = @id");
           da.getCmd().Parameters.AddWithValue("@quantite", ingredient.quantity);
           da.getCmd().Parameters.AddWithValue("@id", ingredient.id);
           da.executeNonQuery();
           da.close();
        }

        public void create(Ingredient ingredient)
        {
            // Not implemented
        }

        public void delete(int id)
        {
            // Not implemented
        }
    }
}
