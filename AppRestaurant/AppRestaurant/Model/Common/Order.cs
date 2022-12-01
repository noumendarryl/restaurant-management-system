using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.Common
{
    public enum OrderStatus
    {
        inCooking,
        updated,
        registred,
        delivered,
        ready,
    }
    public class Order
    {
        private OrderStatus orderStatus;
        private Dictionary<Recipe,int> plats;

        public OrderStatus State { get => orderStatus; set => orderStatus = value; }
        public Dictionary<Recipe, int> orderLine { get => plats; set => plats = value; }


    }
}