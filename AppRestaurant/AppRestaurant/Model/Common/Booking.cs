using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.DAO; 

namespace AppRestaurant.Model.Common
{
    public class Booking : Entity
    {
        public string clientName { get; set; }
        public int nbPeople { get; set; }
        public DateTime hour { get; set; }

        public Booking(string clientName, int nbPeople, DateTime hour)
        {
            this.clientName = clientName;
            this.nbPeople = nbPeople;
            this.hour = hour;
        }
    }
}
