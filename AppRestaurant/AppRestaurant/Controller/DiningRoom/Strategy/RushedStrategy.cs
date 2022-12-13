using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Controller.DiningRoom.Actors;
using AppRestaurant.Controller.DiningRoom.Strategy;


namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    class RushedStrategy : IOrderStrategy
    {
        Order IOrderStrategy.Order(CustomerController customer, MenuCard menuCard)
        {
            throw new NotImplementedException();
        }
    }
}
