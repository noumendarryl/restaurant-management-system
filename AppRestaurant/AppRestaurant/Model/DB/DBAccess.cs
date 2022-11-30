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
				this.dbconn = DBConnection.getInstance();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.Message);
			}
		}

		public void createSqlCommand (string query)
        {
			this.setCmd(new SqlCommand(query, this.dbconn.getConnection()));
		}

		public void executeNonQuery()
		{
			try
            {
				this.getCmd().ExecuteNonQuery();
				this.getCmd().Dispose();
			} catch (InvalidCastException err)
            {
				Console.WriteLine(err.Message);
            } catch (InvalidOperationException err)
			{
				Console.WriteLine(err.Message);
			} catch (SqlException err)
			{
				Console.WriteLine(err.Message);
			}
		}

		public SqlDataReader executeReader()
		{
			return this.getCmd().ExecuteReader();
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
			this.dbconn.close();
        }
	}
}
