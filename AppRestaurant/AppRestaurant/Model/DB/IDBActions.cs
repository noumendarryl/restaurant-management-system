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
        int getPrice(string nameRecipe);
        void reStock(string nameIngredient);
    }
}