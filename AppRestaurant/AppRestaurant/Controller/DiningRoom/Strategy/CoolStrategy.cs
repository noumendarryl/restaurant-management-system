using AppRestaurant.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Controller.DiningRoom.Actors;
using AppRestaurant.Model.kitchen;

namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    class CoolStrategy : IOrderStrategy
    {
        Order IOrderStrategy.Order(CustomerController customer, MenuCard menuCard)
        {
            throw new NotImplementedException();
        }
    }
}
