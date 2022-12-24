using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;
using AppRestaurant.Model.Kitchen;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOIngredient : DAOEntity<Ingredient>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string name;
        private int quantity;

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

        public void update(int id, int quantity)
        {
           da.createSqlCommand("UPDATE dbo.Ingredients SET quantity = @quantity WHERE id = @id");
           da.getCmd().Parameters.AddWithValue("@quantity", quantity);
           da.getCmd().Parameters.AddWithValue("@id", id);
           da.executeNonQuery();
           da.close();
        }
    }
}
