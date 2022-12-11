using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom.Actors;

namespace AppRestaurant.Model.DiningRoom.Factory
{
    public abstract class AbstractCustomersFactory
    {
        public abstract CustomerGroup CreateCustomers(int nbCustomers);
    }
}
