using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen
{
    public class Kitchen
    {
        public KitchenMaterial[,] map { get; set; }

        public Kitchen()
        {
            map = new KitchenMaterial[18, 8];
        }
    }
}
