using System;
using System.IO;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Factory;
using AppRestaurant.Model.Common;
using AppRestaurant.Controller.DiningRoom.Actors;
using AppRestaurant.Controller.DiningRoom.Strategy;
using AppRestaurant.Controller.DiningRoom.Observer;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Controller.Kitchen;
using AppRestaurant.Controller.Pipes;

namespace AppRestaurant.Controller.DiningRoom
{
    class DiningRoomController : StreamString, IObservable<Order>
    {

        private static HotelMasterController hotelMasterController;
        public static DiningRoomModel DiningRoomModel;

        private static List<CustomerController> customerControllers;
        private static List<LineChiefController> lineChiefControllers;

        static string content;
        static int customerCount = 0;

        private static Queue<CustomerGroup> CustomerQueue = new Queue<CustomerGroup>();
        private static ManualResetEvent customerQueueMre = new ManualResetEvent(false);
        private static Mutex customerQueueMtx = new Mutex();

        private static Queue<Order> OrderList = new Queue<Order>();
        public static Queue<Order> OrderListing { get => OrderList; set => OrderList = value; }

        private static List<Thread> orderThreads = new List<Thread>();

        private static List<IObserver<Order>> observers;
        private static ServerThread diningRoomSimul;

        public DiningRoomController(DiningRoomModel diningRoomModel)
        {
            DiningRoomModel = diningRoomModel;
            diningRoomSimul = new ServerThread();
            hotelMasterController = new HotelMasterController(DiningRoomModel);
            lineChiefControllers = new List<LineChiefController>();
            observers = new List<IObserver<Order>>();
        }

        public static void Run()
        {
            CustomersFactory factory = new CustomersFactory();
            factory.Subscribe(hotelMasterController);

            installCustomers(factory, 3);

            Thread takeOrderThread = new Thread(() =>
            {
                while (true)
                {
                    customerQueueMtx.WaitOne();
                    if (CustomerQueue.Count != 0)
                    {
                        int id = customerCount++;
                        Console.WriteLine("=========Client n_" + id + " commande=======");

                        CustomerGroup clt = CustomerQueue.Dequeue();
                        clt.CustomerState = CustomerState.Ordering;
                        CustomerController customerController = new CustomerController(clt, new RandomOrderStrategy());

                        orderThreads.Add(new Thread(() => customerOrder(customerController)));
                        orderThreads[orderThreads.Count - 1].Name = "Commande n_" + (orderThreads.Count - 1) + " du client n_" + id;
                        orderThreads[orderThreads.Count - 1].Start();
                    }
                    customerQueueMtx.ReleaseMutex();
                }
            });
            takeOrderThread.Name = "Take_order";
            takeOrderThread.Start();

            Thread.Sleep(1000);
            Console.WriteLine("========== " + OrderListing.Count + " Commande(s) ont ete prise. ==========");

            foreach (Order comm in OrderListing)
            {
                foreach (KeyValuePair<Recipe, int> dic in comm.orderLine)
                {
                    Console.WriteLine("======= " + dic.Key.RecipeTitle + " : " + dic.Value + " =======");
                    content += dic.Key.RecipeTitle + ","+ dic.Value.ToString() + ";";
                }
            }
            diningRoomSimul.WriteFromServer(content);
        }

        public void TakeOrder()
        {
            while (true)
            {
                customerQueueMtx.WaitOne();
                if (CustomerQueue.Count != 0)
                {
                    int id = customerCount++;
                    Console.WriteLine("=========Client n_" + id + " commande=======");

                    CustomerGroup clt = CustomerQueue.Dequeue();
                    clt.CustomerState = CustomerState.Ordering;
                    CustomerController customerController = new CustomerController(clt, new RandomOrderStrategy());

                    orderThreads.Add(new Thread(() => customerOrder(customerController)));
                    orderThreads[orderThreads.Count - 1].Name = "Commande n_" + (orderThreads.Count - 1) + " du client n_" + id;
                    orderThreads[orderThreads.Count - 1].Start();
                }
                customerQueueMtx.ReleaseMutex();
            }
        }

        public static void installCustomers(CustomersFactory factory, int nbCustomer)
        {
            for (int i = 0; i < nbCustomer; i++)
            {
                CustomerQueue.Enqueue(factory.CreateCustomers(4));
                customerQueueMre.Set();
            }
        }

        public static void customerOrder(CustomerController customerController)
        {
            customerQueueMtx.WaitOne();
            int[] table = FindCustomerTable(customerController.Customer);

            if (table != null)
            {
                OrderList.Enqueue(customerController.Order(DiningRoomModel.Squares[table[0]].Lines[table[1]].Tables[table[2]].MenuCard));
                Order order = OrderList.Peek();
                foreach (IObserver<Order> observer in observers)
                {
                    observer.OnNext(order);
                }
                customerController.Customer.CustomerState = CustomerState.Ordered;
                Thread thread = Thread.CurrentThread;
                Console.WriteLine("=========" + thread.Name + " prise========");
            }

            customerQueueMtx.ReleaseMutex();
        }

        private static int[] FindCustomerTable(CustomerGroup customer)
        {
            int nbSquare = DiningRoomModel.Squares.Count;
            for (int i = 0; i < nbSquare; i++)
            {
                int nbLine = DiningRoomModel.Squares[i].Lines.Count;
                for (int j = 0; j < nbLine; j++)
                {
                    int nbTable = DiningRoomModel.Squares[i].Lines[j].Tables.Count;
                    for (int k = 0; k < nbTable; k++)
                    {
                        if (customer == DiningRoomModel.Squares[i].Lines[j].Tables[k].Group)
                        {
                            //return DiningRoomModel.Squares[i].Lines[j].Tables[k];
                            return new int[3] { i, j, k };
                        }
                    }
                }
            }
            return null;
        }

        public IDisposable Subscribe(IObserver<Order> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new DRUnsubscriber<Order>(observers, observer);
        }
    }
}
