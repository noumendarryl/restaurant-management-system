using AppRestaurant.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    class CoolStrategy : ICustomerStrategy
    {
        Order ICustomerStrategy.Order(MenuCard menuCard)
        {
            throw new NotImplementedException();
        }
    }
}
