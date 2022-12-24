using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.Items;

namespace AppRestaurant.Model.Kitchen.Materials
{
    public abstract class Material : MotionlessKitchenItem
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public bool washable { get; set; }

        public Material(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
            this.washable = true;
        }
    }
}
