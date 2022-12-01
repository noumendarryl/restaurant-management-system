using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Recipe
    {
        public string name { get; set; }
        public Ingredient[] ingredients { get; set; }
        public RecipeStep[] recipeSteps { get; set; }

        public Recipe(string name, Ingredient[] ingredients, RecipeStep[] recipesteps)
        {
            this.name = name;
            this.ingredients = ingredients;
            this.recipeSteps = recipesteps;
        }
    }
}
