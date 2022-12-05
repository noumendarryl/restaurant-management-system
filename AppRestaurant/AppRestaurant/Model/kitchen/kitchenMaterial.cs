using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchenMaterial : Material
    {
        public bool availability { get; set; }

        public kitchenMaterial(string name, int quantity, Sprite sprite) : base(name, quantity)
        {
            this.availability = true;
        }
    }
}
