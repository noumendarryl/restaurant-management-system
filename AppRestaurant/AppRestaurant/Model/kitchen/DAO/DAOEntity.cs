using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public interface DAOEntity<E>
    {
		E find(string code);

		List<E> find(int id);

		void create(E entity);

		void update(E entity);

		void delete(int id);

	}
}
