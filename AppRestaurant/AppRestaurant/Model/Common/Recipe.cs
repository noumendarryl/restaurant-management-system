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
        public string RecipeTitle { get => recipeTitle; set => recipeTitle = value; }
        public int CookingTime { get => cookingTime; set => cookingTime = value; }
        public int RestingTime { get => restingTime; set => restingTime = value; }
        public int PeopleCount { get => peopleCount; set => peopleCount = value; }
        public double Price { get => price; set => price = value; }
        public RecipeType Type { get => type; set => type = value; }
        public List<Ingredient> ingredients { get; set; }
        public List<RecipeStep> recipeSteps { get; set; }

        RecipeType type;
        string recipeTitle;
        double price;
        int restingTime;
        int cookingTime;
        int peopleCount;

        public Recipe(RecipeType type, string recipeTitle, int peopleCount, int cookingTime)
        {
            this.type = type;
            this.recipeTitle = recipeTitle;
            this.peopleCount = peopleCount;
            this.cookingTime = cookingTime;
        }

        public Recipe(string recipeTitle, int cookingTime, int restingTime, int peopleCount, RecipeType type, double price, List<Ingredient> ingredients, List<RecipeStep> recipeSteps)
        {
            this.recipeTitle = recipeTitle;
            this.cookingTime = cookingTime;
            this.restingTime = restingTime;
            this.peopleCount = peopleCount;
            this.type = type;
            this.price = price;
            this.ingredients = ingredients;
            this.recipeSteps = recipeSteps;
        }
    }
}
