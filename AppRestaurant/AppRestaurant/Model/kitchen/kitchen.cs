using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchen
    {
        public int cookingFireNumber = 5;
        public int ovenNumber = 1;

        public kitchenMaterial[,] map { get; set; }

        public kitchen()
        {
            map = new kitchenMaterial[11, 10];

            for (int i = 0; i < cookingFireNumber; i++)
            {
                map[i, 0] = kitchenMaterialFactory.createCookingFire();
            }

            for (int i = 0; i < ovenNumber; i++)
            {
                map[i, 1] = kitchenMaterialFactory.createOven();
            }
        }
    }
}
