using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.DAO;
using AppRestaurant.Model.Kitchen.Ingredients;

namespace AppRestaurant.Model.Common
{
    public enum RecipeType
    {
        Entry,
        Dish,
        Dessert
    }

    public class Recipe : Entity
    {
        public String RecipeTitle { get => recipeTitle; set => recipeTitle = value; }
        public int CookingTime { get => cookingTime; set => cookingTime = value; }
        public int RestingTime { get => restingTime; set => restingTime = value; }
        public List<Ingredient> ingredients { get; set; }
        public List<RecipeStep> recipeSteps { get; set; }

        RecipeType type;
        String recipeTitle;
        Double price;
        int restingTime;
        int cookingTime;
        int peopleCount;

        public Recipe(RecipeType type, String recipeTitle, int peopleCount, int cookingTime)
        {
            this.type = type;
            this.recipeTitle = recipeTitle;
            this.peopleCount = peopleCount;
            this.cookingTime = cookingTime;
        }

        public Recipe(string recipeTitle, int cookingTime, int restingTime, List<Ingredient> ingredients, List<RecipeStep> recipeSteps)
        {
            this.recipeTitle = recipeTitle;
            this.cookingTime = cookingTime;
            this.restingTime = restingTime;
            this.ingredients = ingredients;
            this.recipeSteps = recipeSteps;
        }
    }
}
