using AppRestaurant.Model.Kitchen.DAO;
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

    public class Order : Entity
    {
        private OrderStatus orderStatus;
        private Dictionary<Recipe, int> plats = new Dictionary<Recipe, int>();
        public int tableNumber { get; set; }
        public int nbOrders { get; set; }
        public double price { get; set; }
        public Recipe recipe { get; set; }
        public OrderStatus State { get => orderStatus; set => orderStatus = value; }
        public Dictionary<Recipe, int> orderLine { get => plats; set => plats = value; }

        public Order()
        {
            plats = new Dictionary<Recipe, int>();
        }

        public Order(int tableNumber, int nbOrders, double price, Recipe recipe)
        {
            this.tableNumber = tableNumber;
            this.nbOrders = nbOrders;
            this.price = price;
            this.recipe = recipe;
        }
    }
}
