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
        public Material[,] map { get; set; }

        public Kitchen()
        {
            map = new Material[18, 8];
        }
    }
}
