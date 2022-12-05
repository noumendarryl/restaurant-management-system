using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchen
    {
        public kitchenMaterial[,] map { get; set; }

        public kitchen()
        {
            map = new kitchenMaterial[18, 8];
        }
    }
}
