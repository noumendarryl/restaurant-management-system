using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;

namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    class NormalStrategy : ICustomerStrategy
    {
        public Order Order(MenuCard menuCard)
        {
            return new Order();
        }
    }
}
