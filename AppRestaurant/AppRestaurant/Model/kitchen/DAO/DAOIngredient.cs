using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOIngredient : DAOEntity<Ingredient>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        private string name;
        private int quantity;
        private List<Ingredient> ingredients;

        public DAOIngredient(SqlConnection connection) : base(connection)
        {
            
        }

        /*
		 * Find an ingredient according to its name
		 */
        override
        public Ingredient find(string nameIngredient)
        {
            command = new SqlCommand("SELECT * FROM dbo.Ingredients WHERE nom = @nomIngredient");
            command.Parameters.AddWithValue("@nomIngredient", nameIngredient);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[1].ToString();
                        quantity = (int)reader[2];
                    }
                    reader.Close();
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new Ingredient(name, quantity);
        }

        /*
		 * Get a list of all ingredients of a given recipe
		 */
        override
        public List<Ingredient> find(int id)
        {
            ingredients = new List<Ingredient>();
            command = new SqlCommand("SELECT nom, quantite FROM dbo.utilise INNER JOIN dbo.Ingredients ON (dbo.utilise.id_ingredient = dbo.Ingredients.id_ingredient) WHERE id_recette = @id");
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
                        ingredients.Add(new Ingredient(name, quantity));
                    }
                    reader.Close();
                    for(int i = 0; i < ingredients.Count; i++)
                    {
                        Console.WriteLine(ingredients[i]);
                    }
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ingredients;
        }

        /*
		 * Update the quantity of an ingredient
		 */
        override
        public void update(Ingredient ingredient)
        {
            try
            {
                command = new SqlCommand("UPDATE dbo.Ingredients SET quantite = @quantite WHERE id = @id");
                command.Parameters.AddWithValue("@quantite", ingredient.quantity);
                command.Parameters.AddWithValue("@id", ingredient.id);
                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        override
        public void create(Ingredient ingredient)
        {
            // Not implemented
        }

        override
        public void delete(int id)
        {
            // Not implemented
        }
    }
}
