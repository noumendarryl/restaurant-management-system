using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Controller.DiningRoom.Actors;
using AppRestaurant.Model.Kitchen;

namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    interface IOrderStrategy
    {
        Order Order(CustomerController customer,MenuCard menuCard);
    }
}
