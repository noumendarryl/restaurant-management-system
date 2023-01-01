using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public interface DAOEntity<E>
    {
		/*
		 * Find.
		 *
		 * @param code
		 *          the code
		 * @return the E
		 */
		 E find(string code);

		/*
		 * Find.
		 *
		 * @param id
		 *          the id
		 * @return the E
		 */
		List<E> find(int id);

		/*
		 * Update.
		 *
		 * @param int
		 *          the int
		 *          
		 * @param quantity
		 *          the quantity
		 *          
		 * @return the E
		 */
		void update(int id, int quantity);
	}
}
