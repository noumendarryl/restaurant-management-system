using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Factory;
using AppRestaurant.Model.Common;
using AppRestaurant.Controller.DiningRoom.Actors;
using AppRestaurant.Controller.DiningRoom.Strategy;
using System.Threading;

namespace AppRestaurant.Controller.DiningRoom
{
    class DiningRoomController
    {
        private HotelMasterController hotelMasterController;

        private List<CustomerController> customerControllers;
        private List<LineChiefController> lineChiefControllers;
        private List<RoomClerkController> roomClerkControllers;
        private List<WaiterController> waiterControllers;

        static Queue<Customer> CustomerQueue = new Queue<Customer>();

        private MenuCard menuCard = new MenuCard(new Menu());

        private static ManualResetEvent customerQueueMre = new ManualResetEvent(false);

        private static Mutex customerQueueMtx = new Mutex();

        private static Queue<Order> OrderList = new Queue<Order>();

        public DiningRoomController(DiningRoomModel diningRoomModel)
        {
            this.hotelMasterController = new HotelMasterController(diningRoomModel);
            this.lineChiefControllers = new List<LineChiefController>();
            this.roomClerkControllers = new List<RoomClerkController>();
            this.waiterControllers = new List<WaiterController>();
        }
        public void Run()
        {
            CustomersFactory factory = new CustomersFactory();

            factory.Subscribe(hotelMasterController);

            //installCustomers(factory, 5);

            Thread homeClientThread = new Thread(() => installCustomers(factory, 5));
            homeClientThread.Name = "Home_customers";
            homeClientThread.Start();

            Thread takeOrderThread = new Thread(TakeOrder);
            takeOrderThread.Name = "Take_order";
            takeOrderThread.Start();

        }

        public void TakeOrder()
        {
            while (true)
            {
                //customerQueueMtx.WaitOne();
                customerQueueMre.WaitOne();
                if (CustomerQueue.Count != 0)
                {
                    Customer clt = CustomerQueue.Dequeue();
                    CustomerController customerController = new CustomerController(clt, new NormalStrategy());
                    OrderList.Enqueue(customerController.Order(menuCard));
                }
                customerQueueMre.Reset();
                //customerQueueMtx.ReleaseMutex();
            }
        }

        public void installCustomers(CustomersFactory factory,int nbCustomer)
        {
            for (int i = 0; i < nbCustomer; i++)
            {
                CustomerQueue.Enqueue(factory.CreateCustomers(4));
                customerQueueMre.Set();
            }
        }

    }
}
