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
    public class DAOOrder : DAOEntity<Order>
    {
        public DBAccess da;
        private SqlCommand command;

        public DAOOrder(SqlConnection connection) : base(connection)
        {

        }

        /*
		 * Create an order 
		 */
        override
        public void create(Order order)
        {
            command = new SqlCommand("INSERT INTO dbo.Commande (titre, num_table, nb_commandes, prix) " +
                    "VALUES (@titreCommande, @numeroTable, @nbCommandes, @prix)", getConnection());

            try
            {
                command.Parameters.AddWithValue("@titreCommande", order.title);
                command.Parameters.AddWithValue("@numeroTable", order.tableNumber);
                command.Parameters.AddWithValue("@nbCommandes", order.nbOrders);
                command.Parameters.AddWithValue("@prix", order.price);

                command.ExecuteNonQuery();
                command.Dispose();
                getConnection().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        override
        public Order find(string code)
        {
            // Not implemented
            return null;
        }

        override
        public List<Order> find(int id)
        {
            // Not implemented
            return null;
        }

        override
        public void update(Order order)
        {
            // Not implemented
        }

        /*
		 * Delete a particular order
		 */
        override
        public void delete(int id)
        {
            try
            {
                command = new SqlCommand("DELETE FROM dbo.Commande WHERE id = @id", getConnection());
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
