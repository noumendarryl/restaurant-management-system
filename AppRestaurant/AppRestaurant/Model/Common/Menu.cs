using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.Common
{
    public class Menu
    {
        List<Recipe> entries;
        List<Recipe> dishes;
        List<Recipe> desserts;

        public Menu()
        {
            entries = new List<Recipe>();
            dishes = new List<Recipe>();
            desserts = new List<Recipe>();
        }

        public Menu(List<Recipe> entries, List<Recipe> dishes, List<Recipe> desserts)
        {
            this.entries = entries;
            this.dishes = dishes;
            this.desserts = desserts;
        }
        public List<Recipe> Entries { get => entries; set => entries = value; }
        public List<Recipe> Dishes { get => dishes; set => dishes = value; }
        public List<Recipe> Desserts { get => desserts; set => desserts = value; }
    }
}
