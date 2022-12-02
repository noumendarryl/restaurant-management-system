using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Factory;
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


        public DiningRoomController(DiningRoomModel diningRoomModel)
        {
            this.hotelMasterController = new HotelMasterController(diningRoomModel);
            this.lineChiefControllers = new List<LineChiefController>();
            this.roomClerkControllers = new List<RoomClerkController>();
            this.waiterControllers = new List<WaiterController>();
        }
        public void Run()
        {
            CustomersFactory factor = new CustomersFactory();

            factor.Subscribe(hotelMasterController);

            List<Customer> CustomerList = new List<Customer>();
            for (int i = 0; i < 5; i++)
            {
                CustomerList.Add(factor.CreateCustomers(4));
            }
        }
    }
}
