using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAORecipe : DAOEntity<Recipe>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        public DAOEntity<Ingredient> daoIngredient;
        public DAOEntity<RecipeStep> daoRecipeStep;
        private int idRecipe;
        private string recipeTitle;
        private string type;
        private int cookingTime;
        private int restingTime;
        private int peopleCount;
        private float price;
        private List<Ingredient> ingredients;
        private List<RecipeStep> recipeSteps;

        public DAORecipe(SqlConnection connection) : base(connection)
        {
            daoIngredient = new DAOIngredient(connection);
            daoRecipeStep = new DAORecipeStep(connection);
        }

        /*
		 * Find a recipe according to its name
		 */
        override
        public Recipe find(string name)
        {
            ingredients = new List<Ingredient>();
            recipeSteps = new List<RecipeStep>();
            command  = new SqlCommand("SELECT * FROM dbo.Recette WHERE nom = @nomRecette", getConnection());
            command.Parameters.AddWithValue("@nomRecette", name);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       idRecipe = (int)reader[0];
                       recipeTitle = reader[1].ToString();
                       cookingTime = (int)reader[2];
                       restingTime = (int)reader[3];
                       peopleCount = (int)reader[4];
                        //type = reader[5].ToString();
                        price = (float)reader[6];
                       ingredients = daoIngredient.find(idRecipe);
                       recipeSteps = daoRecipeStep.find(idRecipe);
                    }
                    reader.Close();
                    Console.WriteLine(recipeTitle.ToString() + " " + cookingTime.ToString() + " " + restingTime.ToString() + " " + peopleCount.ToString() + " " + price.ToString());
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return new Recipe(recipeTitle, cookingTime, restingTime, peopleCount, price, ingredients, recipeSteps);
        }

        override
        public List<Recipe> find(int id)
        {
            // Not implemented
            return null;
        }

        override
        public void update(Recipe recipe)
        {
            // Not implemented
        }

        override
        public void create(Recipe recipe)
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
