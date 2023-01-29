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
    public class DAOOrder : DAOEntity<Order>
    {
        public DBAccess da;

        /*
		 * Create an order 
		 */
        public void create(Order order)
        {
            da.createSqlCommand("INSERT INTO dbo.Commande (num_table, nb_commandes, prix) " +
                    "VALUES (@numeroTable, @nbCommandes, @prix)");

            try
            {
                da.getCmd().Parameters.AddWithValue("@numeroTable", order.tableNumber);
                da.getCmd().Parameters.AddWithValue("@nbCommandes", order.nbOrders);
                da.getCmd().Parameters.AddWithValue("@prix", order.price);

                da.executeNonQuery();
                da.close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Order find(string code)
        {
            // Not implemented
            return null;
        }

        public List<Order> find(int id)
        {
            // Not implemented
            return null;
        }

        public void update(Order order)
        {
            // Not implemented
        }

        /*
		 * Delete a particular order
		 */
        public void delete(int id)
        {
            da.createSqlCommand("DELETE FROM dbo.Commande WHERE id = @id");
            da.getCmd().Parameters.AddWithValue("@id", id);
            da.executeNonQuery();
            da.close();
        }
    }
}
