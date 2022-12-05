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
        ICustomerStrategy customerStrategy;
        Customer customer;
        public CustomerController(Customer customer, ICustomerStrategy customerStrategy)
        {
            this.customer = customer;
            this.customerStrategy = customerStrategy;
        }
        public void changeOrderStrategy(ICustomerStrategy customerStrategy)
        {
            this.customerStrategy = customerStrategy;
        }

        public Order Order(MenuCard menuCard)
        {
            return this.customerStrategy.Order(menuCard);
        }

        public Customer Customer { get => customer; set => customer = value; }
    }
}
