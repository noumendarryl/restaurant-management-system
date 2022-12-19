using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class KitchenMaterial : Material
    {
        public bool availability { get; set; }

        public KitchenMaterial(string name, int quantity, Sprite sprite) : base(name, quantity)
        {
            this.availability = true;
        }
    }
}
