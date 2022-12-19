using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model.Common.Move;
using AppRestaurant.Model.kitchen;

namespace AppRestaurant.Model.DiningRoom.Actors
{

    public enum CustomerState
    {
        WaitTableAttribution,
        WaitLineChief,
        Installed,
        Ordering,
        TableDispose,
        Ordered,
        WaitEntree,
        WaitPlate,
        WaitDessert,
        WaitBill,
        Leave
    };

    public enum CustomerChoiceMenu
    {
        entry = 0,
        dish = 1,
        dessert = 2,
        entryAndDish = 3,
        entryAndDessert = 4,
        dishAndDessert = 5,
        all = 6
    };


    public class CustomerGroup : Position, IMove
    {
        private List<Order> orders;
        private CustomerState state;
        private CustomerChoiceMenu choiceMenu;
        private List<string> preferenceList;
        private int count = 0;
       
        public List<string> PreferenceList { get => preferenceList; set => preferenceList = value; }

        public CustomerChoiceMenu ChoiceMenu { get => choiceMenu; set => choiceMenu = value; }

        public List<Order> Orders { get => orders; set => orders = value; }

        public CustomerState CustomerState { get => state; set => state = value; }

        public int Count { get => count; set => count = value; }

        public CustomerGroup()
        {
            this.CustomerState = CustomerState.WaitTableAttribution;
            this.count = 1;
        }
        public CustomerGroup(int count) {
            this.count = count;
            this.CustomerState = CustomerState.WaitTableAttribution;
        }

        public void Move(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }
    }
}
