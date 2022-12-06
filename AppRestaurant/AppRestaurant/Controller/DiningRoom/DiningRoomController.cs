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
using AppRestaurant.Model.DiningRoom.Elements;
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

        private static List<Thread> orderThreads = new List<Thread>();

        public DiningRoomModel diningRoomModel;

        public DiningRoomController(DiningRoomModel diningRoomModel)
        {
            this.diningRoomModel = diningRoomModel;
            this.hotelMasterController = new HotelMasterController(diningRoomModel);
            this.lineChiefControllers = new List<LineChiefController>();
            this.roomClerkControllers = new List<RoomClerkController>();
            this.waiterControllers = new List<WaiterController>();

 
        }
        public void Run()
        {
            CustomersFactory factory = new CustomersFactory();

            factory.Subscribe(hotelMasterController);

            Thread homeClientThread = new Thread(() => installCustomers(factory, 3));
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
                customerQueueMtx.WaitOne();
                if (CustomerQueue.Count != 0)
                {
                    Customer clt = CustomerQueue.Dequeue();
                    clt.CustomerState = CustomerState.Ordering;
                    CustomerController customerController = new CustomerController(clt, new NormalStrategy());

                    orderThreads.Add(new Thread(() => customerOrder(customerController)));
                    orderThreads[orderThreads.Count - 1].Name = "Commande n_" + (orderThreads.Count - 1);
                    orderThreads[orderThreads.Count - 1].Start();
                }
                customerQueueMtx.ReleaseMutex();
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

        public void customerOrder(CustomerController customerController)
        {
            customerQueueMtx.WaitOne();
            Table table = FindCustomerTable(customerController.Customer);

            if(table != null)
            {
                OrderList.Enqueue(customerController.Order(table.MenuCard));
                customerController.Customer.CustomerState = CustomerState.Ordered;
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("========="+thread.Name + " prise========");
            }

            customerQueueMtx.ReleaseMutex();
        }

        private Table FindCustomerTable(Customer customer)
        {
            int nbSquare = this.diningRoomModel.Squares.Count;
            for (int i = 0; i < nbSquare; i++)
            {
                int nbLine = diningRoomModel.Squares[i].Lines.Count;
                for (int j = 0; j < nbLine; j++)
                {
                    int nbTable = diningRoomModel.Squares[i].Lines[j].Tables.Count;
                    for (int k = 0; k < nbTable; k++)
                    {
                        if (customer == diningRoomModel.Squares[i].Lines[j].Tables[k].Group )
                        {
                            return diningRoomModel.Squares[i].Lines[j].Tables[k];
                        }
                    }
                }
            }
            return null;
        }
    }
}
