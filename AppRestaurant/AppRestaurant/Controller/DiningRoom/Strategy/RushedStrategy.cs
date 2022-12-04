using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;


namespace AppRestaurant.Controller.DiningRoom.Actors.Strategy
{
    class RushedStrategy : ICustomerStrategy
    {
        Order ICustomerStrategy.Order(MenuCard menuCard)
        {
            throw new NotImplementedException();
        }
    }
}
