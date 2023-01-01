using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.DAO;

namespace AppRestaurant.Model.Kitchen.Ingredients
{
    public class Ingredient : Entity
    {
       public string name { get; set; }
        public int quantity { get; set; }
        
        public Ingredient(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }
    }
}
