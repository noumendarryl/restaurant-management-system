using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.DAO;

namespace AppRestaurant.Model.Kitchen.Ingredients
{
    public class Stock : Entity
    {
        public string stockTitle { get; set; }
        public string category { get; set; }
        public int quantity { get; set; }

        public Stock(string stockTitle, int quantity, string category)
        {
            this.stockTitle = stockTitle;
            this.quantity = quantity;
            this.category = category;
        }
    }
}
