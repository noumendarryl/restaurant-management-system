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
		private SqlConnection con;

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
				con = new SqlConnection("Data Source=51.159.177.162,9097;Initial Catalog=Tastyl;Persist Security Info=True;User ID=superadmin;Password=Admin123");
				if (con.State == ConnectionState.Closed)
					con.Open();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return true;
		}

		public void close()
		{
			con.Close();
		}

		/*
		 * Gets the connection.
		 *
		 * @return the connection
		 */
		public SqlConnection getConnection()
		{
			return con;
		}
	}
}
