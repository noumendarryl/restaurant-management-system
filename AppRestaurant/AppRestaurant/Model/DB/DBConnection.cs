using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRestaurant.Model.DB
{
	class DBConnection
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
			open();
		}

		/*
		 * Gets the single instance of DBConnection.
		 *
		 * @return single instance of DBConnection
		 */
		public static DBConnection getInstance()
		{
			if (INSTANCE == null)
			{
				INSTANCE = new DBConnection();
			}
			return INSTANCE;
		}

		public bool open()
		{
			try
			{
				conn = new SqlConnection("Data Source=(local);Initial Catalog=AppRestaurant;Integrated Security=true");
				if (conn.State == ConnectionState.Closed)
					conn.Open();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return true;
		}

		public void close()
		{
			conn.Close();
		}

		/*
		 * Gets the connection.
		 *
		 * @return the connection
		 */
		public SqlConnection getConnection()
		{
			return conn;
		}
	}
}
