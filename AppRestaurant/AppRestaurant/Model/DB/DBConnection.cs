using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.DB
{
	public class DBConnection
	{
		/* The instance. */
		private static DBConnection INSTANCE = null;

		/* The connection. */
		private SqlConnection conn;

		/*
		 * Instantiates a new DB connection.
		 */
		public DBConnection()
		{
			this.open();
		}

		/*
		 * Gets the single instance of DBConnection.
		 *
		 * @return single instance of DBConnection
		 */
		public static DBConnection getInstance()
		{
			if (DBConnection.INSTANCE == null)
			{
				DBConnection.INSTANCE = new DBConnection();
			}
			return DBConnection.INSTANCE;
		}

		public Boolean open()
		{
			try
			{
				this.conn = new SqlConnection("Data Source=(local);Initial Catalog=AppRestaurant;Integrated Security=true");
				this.conn.Open();
			}
			catch (SqlException e)
			{
				Console.WriteLine(e.Message);
			}
			return true;
		}

		public void close()
		{
			this.conn.Close();
		}

		/*
		 * Gets the connection.
		 *
		 * @return the connection
		 */
		public SqlConnection getConnection()
		{
			return this.conn;
		}
	}
}
