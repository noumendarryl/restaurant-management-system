using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.DB
{
    public interface IDBActions
    {
        void getBooking();
        void setBooking(string nameClient, int nbPeople, DateTime hour);
        List<string> getRecipes(string category);
        List<string> getSteps(string nameRecipe);
        int getPrice(string nameRecipe);
        List<string> getMaterials(string name);
        List<string> getIngredients(string name);
        void updateStock(string nameRecipe, int nbOrders);
        void reStock();
    }
}