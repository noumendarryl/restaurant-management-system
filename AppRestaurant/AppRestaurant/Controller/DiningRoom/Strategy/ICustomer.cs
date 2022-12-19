using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model.kitchen;

namespace AppRestaurant.Controller.DiningRoom.Strategy
{
    interface ICustomer
    {
        Order Order(MenuCard menuCard);

        void changeOrderStrategy(IOrderStrategy customerStrategy);
    }
}
