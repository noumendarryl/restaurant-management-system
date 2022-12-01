using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Ingredient : Entity
    {
        public string name { get; set; }

        public Ingredient(string name)
        {
            this.name = name;
        }
    }
}
