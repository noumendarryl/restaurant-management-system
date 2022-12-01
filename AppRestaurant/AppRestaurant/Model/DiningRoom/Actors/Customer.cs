using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;

namespace AppRestaurant.Model.DiningRoom.Actors
{

    public enum CustomerState
    {
        WaitTableAttribution,
        WaitRankChief,
        Ordering,
        TableDispose,
        Ordered,
        WaitEntree,
        WaitPlate,
        WaitDessert,
        WaitBill,
        Dead
    };

    public class Customer
    {
        private List<Order> orders;
        private CustomerState state;
        private int count;
        public List<Order> Orders { get => orders; set => orders = value; }

        public CustomerState CustomerState { get => state; set => state = value; }

        public int Count { get => count; set => count = value; }

        public Customer() : base()
        {
            this.CustomerState = CustomerState.WaitTableAttribution;
            this.count = 1;
        }
        public Customer(int count) {
            this.count = count;
            this.CustomerState = CustomerState.WaitTableAttribution;
        }

    }
}
