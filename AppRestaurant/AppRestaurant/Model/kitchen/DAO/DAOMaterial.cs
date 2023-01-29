using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOMaterial : DAOEntity<Material>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string materialName;
        private int quantity;

        /*
		 * Find a material according to its name
		 */
        public Material find(string name)
        {
            da.createSqlCommand("SELECT * FROM dbo.Materiels WHERE nom = @nomMateriel");
            da.getCmd().Parameters.AddWithValue("@nomMateriel", name);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materialName = reader[1].ToString();
                        quantity = (int)reader[2];
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Material(materialName, quantity);
        }

        /*
		 * Update the quantity of a given material
		 */
        public void update(Material material)
        {
            da.createSqlCommand("UPDATE dbo.Materiels SET quantite = @quantite WHERE nom = @nomMateriel");
            da.getCmd().Parameters.AddWithValue("@quantite", material.quantity);
            da.getCmd().Parameters.AddWithValue("@nomMateriel", material.name);
            da.executeNonQuery();
            da.close();
        }

        public List<Material> find(int id)
        {
            // Not Implemented
            return null;
        }

        public void create(Material material)
        {
            // Not implemented
        }

        public void delete(int id)
        {
            // Not implemented
        }
    }
}
