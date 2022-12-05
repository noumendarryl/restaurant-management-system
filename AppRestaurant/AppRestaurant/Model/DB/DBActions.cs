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
        private SqlDataReader reader;
        private int limitStock = 100;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
            }
            catch (Exception e)
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
            }
            catch (Exception e)
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
            da.createSqlCommand("SELECT * FROM dbo.Etapes WHERE ");
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return steps;
        }

        /*
	     * Update the stock according to deliveries.
	     */
        public void reStock(string nameIngredient)
        {
            values = new Dictionary<string, int>();

            da.createSqlCommand("SELECT * FROM dbo.Ingredients WHERE nom = @nom");
            da.getCmd().Parameters.AddWithValue("@nom", nameIngredient);

            reader = da.executeReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    values.Add(reader[1].ToString(), (int)reader[2]);
                }
                reader.Close();
            }

            foreach (var element in values)
            {
                da.createSqlCommand("UPDATE dbo.Ingredients SET quantity = @newquantity WHERE nom = @nom");
                da.getCmd().Parameters.AddWithValue("@nom", element.Key);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
