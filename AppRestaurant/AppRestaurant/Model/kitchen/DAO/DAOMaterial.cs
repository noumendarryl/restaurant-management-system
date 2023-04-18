using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOMaterial : DAOEntity<Material>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        private string materialName;
        private int quantity;

        public DAOMaterial(SqlConnection connection) : base(connection)
        {
            
        }

        /*
		 * Find a material according to its name
		 */
        override
        public Material find(string name)
        {
            command = new SqlCommand("SELECT * FROM dbo.Materiels WHERE nom = @nomMateriel", getConnection());
            command.Parameters.AddWithValue("@nomMateriel", name);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materialName = reader[1].ToString();
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
            return new Material(materialName, quantity);
        }

        /*
		 * Update the quantity of a given material
		 */
        override
        public void update(Material material)
        {
            try
            {
                command = new SqlCommand("UPDATE dbo.Materiels SET quantite = @quantite WHERE nom = @nomMateriel");
                command.Parameters.AddWithValue("@quantite", material.quantity);
                command.Parameters.AddWithValue("@nomMateriel", material.name);
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
        public List<Material> find(int id)
        {
            // Not Implemented
            return null;
        }

        override
        public void create(Material material)
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
