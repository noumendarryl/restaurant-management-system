using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Controller.DiningRoom.Strategy;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Actors;

namespace AppRestaurant.Controller.DiningRoom.Actors
{
    class CustomerController : ICustomer
    {
        IOrderStrategy customerStrategy;
        CustomerGroup customer;
        public CustomerController(CustomerGroup customer, IOrderStrategy customerStrategy)
        {
            this.customer = customer;
            this.customerStrategy = customerStrategy;
        }
        public void changeOrderStrategy(IOrderStrategy customerStrategy)
        {
            this.customerStrategy = customerStrategy;
        }

        public Order Order(MenuCard menuCard)
        {
            return this.customerStrategy.Order(this, menuCard);
        }

        public CustomerGroup Customer { get => customer; set => customer = value; }
    }
}
