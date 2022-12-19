using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class RecipeStep
    {
        public string name { get; set; }

        public int duration { get; set; }

        public KitchenMaterial material { get; set; }

        public RecipeStep(string name, int duration, KitchenMaterial material)
        {
            this.name = name;
            this.duration = duration;
            this.material = material;
        }
    }
}
