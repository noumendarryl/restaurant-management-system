using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Controller.DiningRoom.Actors;

namespace AppRestaurant.Controller.DiningRoom
{
    class DiningRoomController
    {
        private HotelMasterController hotelMasterController;

        private List<CustomerController> customerControllers;
        private List<LineChiefController> lineChiefControllers;
        private List<RoomClerkController> roomClerkControllers;
        private List<WaiterController> waiterControllers;


        DiningRoomController(DiningRoomModel diningRoomModel)
        {
            this.hotelMasterController = new HotelMasterController(diningRoomModel.HotelMaster);
            this.lineChiefControllers = new List<LineChiefController>(lineChiefControllers);
            this.roomClerkControllers = new List<RoomClerkController>(roomClerkControllers);
            this.waiterControllers = new List<WaiterController>(waiterControllers);
        }
    }
}
