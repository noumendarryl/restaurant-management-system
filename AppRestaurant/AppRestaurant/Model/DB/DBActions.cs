using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.DB
{
    public class DBActions : IDBActions
    {
        private DBAccess da = new DBAccess();
        private Dictionary<string, int> values;
        private List<string> recipes;
        private List<string> steps;
        private List<string> materials;
        private List<string> ingredients;
        private SqlDataReader reader;
        private int limitStock = 100;
        private Boolean isOpened;

        public void getBooking()
        {
            da.createSqlCommand("SELECT * FROM dbo.ReservationTable");

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.Write("M/Mrs" + reader[1] + "booked");
                        Console.Write(" a table for " + reader[2] + " for ");
                        Console.WriteLine(reader[3]);
                    }
                    reader.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<string> getIngredients(string name)
        {
            ingredients = new List<string>();
            da.createSqlCommand("SELECT * FROM dbo.Ingredients WHERE nom = @nomIngredient");
            da.getCmd().Parameters.AddWithValue("@nomIngredient", name);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ingredients.Add(reader[1].ToString());
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

        public List<string> getMaterials(string type)
        {
            materials = new List<string>();
            da.createSqlCommand("SELECT * FROM dbo.Materials WHERE type = @typeMateriel");
            da.getCmd().Parameters.AddWithValue("@typeMateriel", type);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materials.Add(reader[3].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return materials;
        }

        public int getPrice(string nameRecipe)
        {
            int price = 0;

           da.createSqlCommand("SELECT Prix FROM dbo.Recette WHERE nom = @nomRecette");
           da.getCmd().Parameters.AddWithValue("@nomRecette", nameRecipe);

           try
           {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        price = (int)reader[0];
                    }
                    reader.Close();
                }
           } catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }

           return price;
        }

        /*
	     * Get a list of all the existing recipes.
	     */
        public List<string> getRecipes(string category)
        {
            recipes = new List<string>();
            da.createSqlCommand("SELECT * FROM dbo.Recette WHERE categorie = @categorie");
            da.getCmd().Parameters.AddWithValue("@categorie", category);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        recipes.Add(reader[1].ToString());
                    }
                    reader.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return recipes;
         }

        /*
	     * Get steps according to a specified recipe 
	     */
        public List<string> getSteps(string nameRecipe)
        {
            steps = new List<string>();
            da.createSqlCommand("");
            da.getCmd().Parameters.AddWithValue("@nomRecette", nameRecipe);
            reader = da.executeReader();

            try
            {
               if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        steps.Add(reader[3].ToString());
                    }
                    reader.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return steps;
        }

        /*
	     * Update the stock according to deliveries.
	     */
        public void reStock()
        {
            values = new Dictionary<string, int>();

            da.createSqlCommand("");
            da.getCmd().Parameters.AddWithValue("", limitStock);
            
            reader = da.executeReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    values.Add(reader[2].ToString(), (int)reader[3]);
                }
                reader.Close();
            }

            foreach (var element in values)
            {
                da.createSqlCommand("");
                da.getCmd().Parameters.AddWithValue("@nom",element.Key);
                da.getCmd().Parameters.AddWithValue("@newquantity", limitStock - element.Value);
                da.executeNonQuery();
            }

            da.close();
        }

        public void setBooking(string nameClient, int nbPeople, DateTime hour)
        {
            da.createSqlCommand("INSERT INTO dbo.ReservationTable (nom_client, nb_personnes, horaire) " +
                    "VALUES (@nomClient, @nbPersonnes, @horaire)");

            try
            {
                da.getCmd().Parameters.AddWithValue("@nomClient", nameClient);
                da.getCmd().Parameters.AddWithValue("@nbPersonnes", nbPeople);
                da.getCmd().Parameters.AddWithValue("@horaire", hour);

                da.executeNonQuery();
                da.close();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
	     * Update the stock according to client orders.
	     */
        public void updateStock(string nameRecipe, int nbOrders)
        {
            values = new Dictionary<string, int>();

            da.createSqlCommand("");
            da.getCmd().Parameters.AddWithValue("@nom", nameRecipe);
            da.getCmd().Parameters.AddWithValue("@nb_commandes", nbOrders);
            
            reader = da.executeReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //values.Add(reader[].ToString(), (int)reader[] * nbOrders);
                }
                reader.Close();
            }

            foreach (var item in values)
            {
                da.createSqlCommand("");
                da.getCmd().Parameters.AddWithValue("", item.Key);
                da.getCmd().Parameters.AddWithValue("", item.Value);
                da.executeNonQuery();
            }

            da.close();
        }
    }
}
