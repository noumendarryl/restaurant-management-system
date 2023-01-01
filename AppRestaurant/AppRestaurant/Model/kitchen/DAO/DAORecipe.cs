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
        private List<Ingredient> ingredients;
        private List<RecipeStep> recipeSteps;

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

            return new Recipe(recipeTitle, cookingTime, restingTime, ingredients, recipeSteps);
        }

        public void update(int id, int quantity)
        {
            // Not implemented
        }

        public List<Recipe> find(int id)
        {
            // Not implemented
            return null;
        }
    }
}
