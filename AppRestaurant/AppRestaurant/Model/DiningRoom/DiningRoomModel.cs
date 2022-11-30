using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DiningRoom.Actors;
namespace AppRestaurant.Model.DiningRoom
{
    class DiningRoomModel
    {
        private HotelMaster hotelMaster;
        private static List<RoomClerk> roomclerks;

        static DiningRoomModel()
        {
            roomclerks.Add(new RoomClerk());
        }
        public DiningRoomModel()
        {
            hotelMaster = new HotelMaster();
        }

        public HotelMaster HotelMaster { get => hotelMaster; set => hotelMaster = value; }
        public static List<RoomClerk> RoomClerk { get => roomclerks; set => roomclerks = value; }

    }
}
