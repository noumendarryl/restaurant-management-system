using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DB;
using System.Windows.Forms;

namespace AppRestaurant.Model.DB
{
	public class DBAccess
	{
		private DBConnection dbcon;
		private SqlCommand cmd;

		public DBAccess()
		{
			try
			{
				dbcon = DBConnection.getInstance();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void createSqlCommand(string query)
		{
			setCmd(new SqlCommand(query, dbcon.getConnection()));
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
				MessageBox.Show(err.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (InvalidOperationException err)
			{
				MessageBox.Show(err.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (SqlException err)
			{
				MessageBox.Show(err.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			dbcon.close();
		}
	}
}
