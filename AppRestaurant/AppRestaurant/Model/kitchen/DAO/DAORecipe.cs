using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAORecipe : DAOEntity<Recipe>
    {
        public DBAccess da;
        public DAOEntity<Entity> dao;
        private SqlDataReader reader;
        private int idRecipe;
        private string recipeTitle;
        private int cookingTime;
        private int restingTime;
        private int peopleCount;
        private RecipeType type;
        private double price;
        private List<Ingredient> ingredients;
        private List<RecipeStep> recipeSteps;

        /*
		 * Find a recipe according to its name
		 */
        public Recipe find(string name)
        {
            ingredients = new List<Ingredient>();
            recipeSteps = new List<RecipeStep>();
            da.createSqlCommand("SELECT * FROM dbo.Recette WHERE nom = @nom");
            da.getCmd().Parameters.AddWithValue("@nom", name);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       idRecipe = (int)reader[0];
                       recipeTitle = reader[1].ToString();
                       cookingTime = (int)reader[2];
                       restingTime = (int)reader[3];
                       peopleCount = (int)reader[4];
                       type = (RecipeType)reader[5];
                       price = (double)reader[6];
                       ingredients = ((DAOEntity<Ingredient>)dao).find(idRecipe);
                       recipeSteps = ((DAOEntity<RecipeStep>)dao).find(idRecipe);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Recipe(recipeTitle, cookingTime, restingTime, peopleCount, type, price, ingredients, recipeSteps);
        }

        public List<Recipe> find(int id)
        {
            // Not implemented
            return null;
        }

        public void update(Recipe recipe)
        {
            // Not implemented
        }

        public void create(Recipe recipe)
        {
            // Not implemented
        }

        public void delete(int id)
        {
            // Not implemented
        }
    }
}
