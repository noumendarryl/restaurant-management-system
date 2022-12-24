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
		 * @return the e
		 */
		 E find(string code);

		/*
		 * Update.
		 *
		 * @param code
		 *          the code
		 * @return the e
		 */
		void  update(int id, int quantity);
	}
}
