using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AppRestaurant.Model.Kitchen.DAO
{
     public abstract class DAOEntity<E>
    {
		private SqlConnection connection;

		public DAOEntity(SqlConnection connection)
        {
			this.connection = connection;
        }

		protected SqlConnection getConnection()
        {
			return connection; 
        }
		
		public abstract E find(string code);
		
		public abstract List<E> find(int id);
		
		public abstract void create(E entity);

		public abstract void update(E entity);

		public abstract void delete(int id);

	}
}
