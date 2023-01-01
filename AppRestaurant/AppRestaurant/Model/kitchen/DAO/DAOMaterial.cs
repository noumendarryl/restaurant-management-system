using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.DAO
{
    public class DAOMaterial : DAOEntity<KitchenMaterial>
    {
        public KitchenMaterial find(string code)
        {
            // Not Implemented
            return null;
        }

        public List<KitchenMaterial> find(int id)
        {
            // Not Implemented
            return null;
        }

        public void update(int id, int quantity)
        {
            // Not Implemented
        }
    }
}
