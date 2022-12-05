using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Order : Entity
    {
        public int tableNumber { get; set; }
        public int nbOrders { get; set; }
        public double price { get; set; }
        public Recipe recipe { get; set; }
        public Order(int tablenumber, int nborders, double price, Recipe recipe)
        {
            this.tableNumber = tablenumber;
            this.nbOrders = nborders;
            this.price = price;
            this.recipe = recipe;
        }
    }
}
