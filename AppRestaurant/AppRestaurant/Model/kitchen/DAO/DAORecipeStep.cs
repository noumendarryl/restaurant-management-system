using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAORecipeStep : DAOEntity<RecipeStep>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        private DAOEntity<Material> daoMaterial;
        private string name;
        private int duration;
        private KitchenMaterial material;
        private List<RecipeStep> recipeSteps;

        public DAORecipeStep(SqlConnection connection) : base(connection)
        {
            daoMaterial = new DAOMaterial(connection);
        }

        /*
		 * Get all the steps of a given recipe
		 */
        override
        public List<RecipeStep> find(int id)
        {
            recipeSteps = new List<RecipeStep>();
            command = new SqlCommand("SELECT intitule, duree, nom_materiel FROM dbo.Recette INNER JOIN dbo.Etapes ON (dbo.Recette.id_recette = dbo.Etapes.id_recette) WHERE dbo.Recette.id_recette = @id ORDER BY numero_etape ASC", getConnection());
            command.Parameters.AddWithValue("@id_recette", id);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[0].ToString();
                        duration = (int)reader[1];
                        material = (KitchenMaterial)daoMaterial.find(reader[2].ToString());
                        recipeSteps.Add(new RecipeStep(name, duration, material));
                    }
                    reader.Close();
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return recipeSteps;
        }

        override
        public RecipeStep find(string code)
        {
            // Not Implemented
            return null;
        }

        override
        public void update(RecipeStep recipeStep)
        {
            // Not implemented
        }

        override
        public void create(RecipeStep recipeStep)
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
