using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOBooking : DAOEntity<Booking>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private string clientName;
        private int nbPeople;
        private DateTime hour;

        /*
		 * Create a booking 
		 */
        public void create(Booking booking)
        {
            da.createSqlCommand("INSERT INTO dbo.ReservationTable (nom_client, nb_personnes, horaire) " +
                    "VALUES (@nomClient, @nbPersonnes, @horaire)");

            try
            {
                da.getCmd().Parameters.AddWithValue("@nomClient", booking.clientName);
                da.getCmd().Parameters.AddWithValue("@nbPersonnes", booking.nbPeople);
                da.getCmd().Parameters.AddWithValue("@horaire", booking.hour);

                da.executeNonQuery();
                da.close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
		 * Retrieve a booking according to the client name under which it was done
		 */
        public Booking find(string clientName)
        {
            da.createSqlCommand("SELECT * FROM dbo.ReservationTable WHERE nom_client = @nomClient");
            da.getCmd().Parameters.AddWithValue("@nomClient", clientName);

            try
            {
                reader = da.executeReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.clientName = reader[1].ToString();
                        nbPeople =  (int)reader[2];
                        hour  = (DateTime)reader[3];
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Booking(this.clientName, nbPeople, hour);
        }

        public List<Booking> find(int id)
        {
            // Not implemented
            return null;
        }

        public void update(Booking booking)
        {
            // Not implemented
        }

        /*
		 * Delete a particular booking
		 */
        public void delete(int id)
        {
            da.createSqlCommand("DELETE FROM dbo.ReservationTable WHERE id = @id");
            da.getCmd().Parameters.AddWithValue("@id", id);
            da.executeNonQuery();
            da.close();
        }
    }
}
