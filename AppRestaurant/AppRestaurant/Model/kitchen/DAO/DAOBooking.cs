using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DB;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOBooking : DAOEntity<Booking>
    {
        public DBAccess da;
        private SqlDataReader reader;
        private SqlCommand command;
        private string clientName;
        private int nbPeople;
        private DateTime hour;

        public DAOBooking(SqlConnection connection) : base(connection)
        {
            
        }

        /*
		 * Create a booking 
		 */
        override
        public void create(Booking booking)
        {
            command = new SqlCommand("INSERT INTO dbo.ReservationTable (nom_client, nb_personnes, horaire) " +
                    "VALUES (@nomClient, @nbPersonnes, @horaire)", getConnection());

            try
            {
                command.Parameters.AddWithValue("@nomClient", booking.clientName);
                command.Parameters.AddWithValue("@nbPersonnes", booking.nbPeople);
                command.Parameters.AddWithValue("@horaire", booking.hour);

                command.ExecuteNonQuery();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
		 * Retrieve a booking according to the client name under which it was done
		 */
        override
        public Booking find(string clientName)
        {
            command = new SqlCommand("SELECT * FROM dbo.ReservationTable WHERE nom_client = @nomClient");
            command.Parameters.AddWithValue("@nomClient", clientName);

            try
            {
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        this.clientName = reader[1].ToString();
                        nbPeople = (int)reader[2];
                        hour = (DateTime)reader[3];
                    }
                    reader.Close();
                }
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new Booking(this.clientName, nbPeople, hour);
        }

        override
        public List<Booking> find(int id)
        {
            // Not implemented
            return null;
        }

        override
        public void update(Booking booking)
        {
            // Not implemented
        }

        /*
		 * Delete a particular booking
		 */
        override
        public void delete(int id)
        {
            try
            {
                command = new SqlCommand("DELETE FROM dbo.ReservationTable WHERE id = @id");
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
