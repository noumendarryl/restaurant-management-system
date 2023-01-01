using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAORecipeStep : DAOEntity<RecipeStep>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string name;
        private int duration;
        private KitchenMaterial material;
        private List<RecipeStep> recipeSteps;

        public RecipeStep find(string code)
        {
            // Not Implemented
            return null;
        }

        /*
		 * Get all the steps of a given recipe
		 */
        public List<RecipeStep> find(int id)
        {
            recipeSteps = new List<RecipeStep>();
            da.createSqlCommand("SELECT nom, duree, materiel FROM dbo.Recette INNER JOIN dbo.Etapes ON (dbo.Recette.id_recette = dbo.Etapes.id_recette) WHERE dbo.Recette.id_recette = @id ORDER BY numero_etape ASC");
            da.getCmd().Parameters.AddWithValue("@id_recette", id);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[1].ToString();
                        duration = (int)reader[2];
                        material = (KitchenMaterial)reader[3];
                        recipeSteps.Add(new RecipeStep(name, duration, material));
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return recipeSteps;
        }

        public void update(int id, int quantity)
        {
            // Not implemented
        }
    }
}
