using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;

namespace AppRestaurant.Model.Kitchen.Materials
{
    public class KitchenMaterial : Material
    {
        public KitchenMaterial(string name, int quantity, Sprite sprite) : base(name, quantity)
        {
            setSprite(sprite);
        }
    }
}
