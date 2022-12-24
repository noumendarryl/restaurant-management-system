using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;

namespace AppRestaurant.Model.DB
{
	public class DBAccess
	{
		private DBConnection dbconn;
		private SqlCommand cmd;

		public DBAccess()
		{
			try
			{
				dbconn = DBConnection.getInstance();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.Message);
			}
		}

		public void createSqlCommand(string query)
		{
			setCmd(new SqlCommand(query, dbconn.getConnection()));
		}

		public void executeNonQuery()
		{
			try
			{
				getCmd().ExecuteNonQuery();
				getCmd().Dispose();
			}
			catch (InvalidCastException err)
			{
				Console.WriteLine(err.Message);
			}
			catch (InvalidOperationException err)
			{
				Console.WriteLine(err.Message);
			}
			catch (SqlException err)
			{
				Console.WriteLine(err.Message);
			}
		}

		public SqlDataReader executeReader()
		{
			return getCmd().ExecuteReader();
		}

		public SqlCommand getCmd()
		{
			return cmd;
		}

		private void setCmd(SqlCommand value)
		{
			cmd = value;
		}

		public void close()
		{
			dbconn.close();
		}
	}
}
