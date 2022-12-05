using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.Common
{
    public enum RecipeType{
        Entry,
        Dish,
        Dessert
    }
    public class Recipe
    {
        RecipeType type;
        String recipeTitle;
        Double price;
        int restingTime;
        int cookingTime;
        int peopleCount;

        public Recipe(RecipeType type,String recipeTitle,int peopleCount, int cookingTime) {
            this.type = type;
            this.recipeTitle = recipeTitle;
            this.peopleCount = peopleCount;
            this.cookingTime = cookingTime;
        }
    }
}
