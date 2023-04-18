using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Controller.Pipes;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Model.Kitchen.DAO;
using AppRestaurant.Model.Kitchen.Ingredients;
using AppRestaurant.Model.Kitchen.Materials;
using AppRestaurant.View.Kitchen;

namespace AppRestaurant.Controller.Kitchen
{
    public class KitchenController : StreamString
    {
        public static KitchenModel Model { get; set; }
        public static KitchenView View { get; set; }
        public static Thread ChefThread { get; set; }
        public static Thread distantWaiterThread { get; set; }
        public static Thread deputyChefThread { get; set; }
        public static Thread KitchenClerkThread { get; set; }
        public static Thread diverThread { get; set; }

        public static Queue<Order> orderQueue { get; set; }
        private static Mutex orderQueueMut = new Mutex();
        private static ManualResetEvent orderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyOrderQueueMut = new Mutex();

        public static Queue<Order> pendingOrderQueue { get; set; }
        private static Mutex pendingOrderQueueMut = new Mutex();
        private static ManualResetEvent pendingOrderQueueMre = new ManualResetEvent(false);

        public static Queue<Order> doneOrderQueue { get; set; }
        private static Mutex doneOrderQueueMut = new Mutex();
        private static ManualResetEvent doneOrderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyDoneOrderQueueMut = new Mutex();

        public static Queue<Recipe> recipeQueue { get; set; }
        private static Mutex recipeQueueMut = new Mutex();
        private static ManualResetEvent recipeQueueMre = new ManualResetEvent(false);

        public static Queue<Ingredient> ingredientQueue { get; set; }
        public static Queue<RecipeStep> RecipeStepQueue { get; set; }

        public static Queue<Material> materialWashQueue { get; set; }
        private static Mutex materialWashQueueMut = new Mutex();
        private static ManualResetEvent materialWashQueueMre = new ManualResetEvent(false);
        private static Mutex notifyMaterialWashMut = new Mutex();

        public static string orderTitle;
        public static int nbOrders;
        public static int customerCount;
        public static int OrderCount;
        private static ClientThread kitchenSimul;
        private static List<string> values = new List<string>();

        public KitchenController(KitchenModel model, KitchenView view)
        {
            Model = model;
            View = view;
            kitchenSimul = new ClientThread();

            orderQueue = new Queue<Order>();
            pendingOrderQueue = new Queue<Order>();
            doneOrderQueue = new Queue<Order>();
            recipeQueue = new Queue<Recipe>();
            ingredientQueue = new Queue<Ingredient>();
            RecipeStepQueue = new Queue<RecipeStep>();
            materialWashQueue = new Queue<Material>();
        }

        public static void Start()
        {
            Model.AddObserver(View);

            for (int i = 1; i <= Model.chefs.Length; i++)
            {
                ChefThread = new Thread(new ThreadStart(ChefTask));
                ChefThread.Name = "Chef " + i;
                ChefThread.Start();
            }

            values = kitchenSimul.ReadFromClient();
            for (int i = 0; i < values.Count - 1; i++)
            {
                orderTitle = values[i].Split(',')[0].ToString();
                nbOrders += Convert.ToInt32(values[i].Split(',')[1]);
                customerCount = Convert.ToInt32(values[i].Split(',')[2]);
                OrderCount = Convert.ToInt32(values[i].Split(',')[3]);
                Console.WriteLine("Values split results : " + orderTitle + " " +nbOrders + " " + customerCount + " " + OrderCount);
            }

            distantWaiterThread = new Thread(new ThreadStart(DistantWaiterTask));
            distantWaiterThread.Name = "Waiter";
            distantWaiterThread.Start();

            for (int i = 1; i <= Model.deputyChefs.Length; i++)
            {
                deputyChefThread = new Thread(new ThreadStart(deputyChefTask));
                deputyChefThread.Name = "Deputy Chef " + i;
                deputyChefThread.Start();
            }

            for (int i = 1; i <= Model.kitchenClerks.Length; i++)
            {
                KitchenClerkThread = new Thread(new ThreadStart(KitchenClerkTask));
                KitchenClerkThread.Name = "Kitchen Clerk " + i;
                KitchenClerkThread.Start();
            }

            for (int i = 1; i <= Model.divers.Length; i++)
            {
                diverThread = new Thread(new ThreadStart(diverTask));
                diverThread.Name = "Diver " + i;
                diverThread.Start();
            }
        }

        [Obsolete]
        public static void Suspend()
        {
            for (int i = 1; i <= Model.chefs.Length; i++)
            {
                ChefThread.Suspend();
            }

            distantWaiterThread.Suspend();

            for (int i = 1; i <= Model.deputyChefs.Length; i++)
            {
                deputyChefThread.Suspend();
            }

            for (int i = 1; i <= Model.kitchenClerks.Length; i++)
            {
                KitchenClerkThread.Suspend();
            }

            for (int i = 1; i <= Model.divers.Length; i++)
            {
                diverThread.Suspend();
            }
        }

        [Obsolete]
        public static void Resume()
        {
            for (int i = 1; i <= Model.chefs.Length; i++)
            {
                ChefThread.Resume();
            }

            distantWaiterThread.Resume();

            for (int i = 1; i <= Model.deputyChefs.Length; i++)
            {
                deputyChefThread.Resume();
            }

            for (int i = 1; i <= Model.kitchenClerks.Length; i++)
            {
                KitchenClerkThread.Resume();
            }

            for (int i = 1; i <= Model.divers.Length; i++)
            {
                diverThread.Resume();
            }
        }

        private static void ChefTask()
        {
            while (true)
            {
                Model.NotifyFreeEmployee("Chef");
                notifyOrderQueueMut.WaitOne();
                orderQueueMre.WaitOne();
                orderQueueMre.Reset();
                notifyOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Order received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Order received.");

                Console.WriteLine(Thread.CurrentThread.Name + " : Sending to a deputy chef");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Sending to a deputy chef");
                Model.NotifyBusyEmployee("Chef");

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int chefPosition = threadNumber - 1;

                // Getting current sprite
                Model.chefs[chefPosition].Sprite = Model.chefs[chefPosition].working;
                Model.NotifyWhenMoved(Model.chefs[chefPosition].PosX, Model.chefs[chefPosition].PosY, Model.chefs[chefPosition].PosX, Model.chefs[chefPosition].PosY);

                orderQueueMut.WaitOne();
                pendingOrderQueueMut.WaitOne();
                pendingOrderQueue.Enqueue(orderQueue.First<Order>());
                pendingOrderQueueMre.Set();
                pendingOrderQueueMut.ReleaseMutex();

                recipeQueueMut.WaitOne();
                recipeQueue.Enqueue(orderQueue.First<Order>().recipe);
                Thread.Sleep(Model.TIME_SLEEP);
                recipeQueueMre.Set();
                recipeQueueMut.ReleaseMutex();
                orderQueue.Dequeue();
                orderQueueMut.ReleaseMutex();

                // Changing sprite
                Model.chefs[chefPosition].Sprite = Model.chefs[chefPosition].working;
                Model.NotifyWhenMoved(Model.chefs[chefPosition].PosX, Model.chefs[chefPosition].PosY, Model.chefs[chefPosition].PosX, Model.chefs[chefPosition].PosY);
            }
        }

        private static void deputyChefTask()
        {
            while (true)
            {
                Model.NotifyFreeEmployee("Deputy Chef");
                notifyOrderQueueMut.WaitOne();
                recipeQueueMre.WaitOne();
                recipeQueueMre.Reset();

                notifyOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Recipe received.");
                Model.NotifyBusyEmployee("Deputy Chef");

                recipeQueueMut.WaitOne();
                Recipe recipe = recipeQueue.First<Recipe>();
                recipeQueue.Dequeue();
                recipeQueueMut.ReleaseMutex();

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int deputyChefPosition = threadNumber - 1;
                Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);

                //int pastX = Model.deputyChefs[deputyChefPosition].PosX;
                //int pastY = Model.deputyChefs[deputyChefPosition].PosY;
                //Model.deputyChefs[deputyChefPosition].moveRight();
                //Model.NotifyWhenMoved(pastX, pastY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);

                //Model.deputyChefs[deputyChefPosition].Sprite = Model.deputyChefs[deputyChefPosition].front;

                //Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);

                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    ingredient.quantity--;
                    //Ingredient ing = Model.daoIngredient.find(ingredient.name);
                    //Model.daoIngredient.update(ing);
                }

                foreach (RecipeStep task in recipe.recipeSteps)
                {
                    while (task.material.quantity == 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Model.NotifyEventLog(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Thread.Sleep(Model.TIME_SLEEP);
                    }
                    task.material.quantity--;
                    task.material.State = MaterialState.InUse;
                    Model.NotifyMaterialAvailaibility(task.material.name, task.material.State);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");

                    Thread.Sleep(task.duration * Model.TIME_SCALE);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Task '" + task.name + "' done.");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Task '" + task.name + "' done");
                    task.material.State = MaterialState.Dirty;

                    materialWashQueueMut.WaitOne();
                    if (task.material.State == MaterialState.Dirty && task.material.washable)
                    {
                        materialWashQueue.Enqueue(task.material);
                        materialWashQueueMre.Set();
                    }
                    else
                    {
                        task.material.quantity++;
                        task.material.State = MaterialState.Clean;
                    }
                    materialWashQueueMut.ReleaseMutex();
                }

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe '" + recipe.RecipeTitle + "' done.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Recipe '" + recipe.RecipeTitle + "' done.");

                //for (int i = 0; i <= 4; i++)
                //{
                //    int oldX = Model.deputyChefs[deputyChefPosition].PosX;
                //    int oldY = Model.deputyChefs[deputyChefPosition].PosY;
                //    Model.deputyChefs[deputyChefPosition].moveLeft();
                //    Model.NotifyWhenMoved(oldX, oldY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);
                //}

                //Model.deputyChefs[deputyChefPosition].Sprite = Model.deputyChefs[deputyChefPosition].front;
                //Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);

                doneOrderQueueMut.WaitOne();
                pendingOrderQueueMut.WaitOne();
                doneOrderQueue.Enqueue(pendingOrderQueue.First<Order>());
                doneOrderQueueMre.Set();
                pendingOrderQueueMut.ReleaseMutex();
                doneOrderQueueMut.ReleaseMutex();
            }
        }

        private static void KitchenClerkTask()
        {
            while (true)
            {
                Model.NotifyFreeEmployee("KitchenClerk");
                notifyDoneOrderQueueMut.WaitOne();

                if (doneOrderQueueMre.WaitOne())
                {
                    doneOrderQueueMre.Reset();
                    notifyDoneOrderQueueMut.ReleaseMutex();

                    Console.WriteLine(Thread.CurrentThread.Name + " : Done order received");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Done order received");
                    Model.NotifyBusyEmployee("KitchenClerk");

                    int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                    int clerkPosition = threadNumber - 1;
                    //Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);

                    //for (int i = 0; i <= 4; i++)
                    //{
                    //    int pastX = Model.kitchenClerks[clerkPosition].PosX;
                    //    int pastY = Model.kitchenClerks[clerkPosition].PosY;
                    //    Model.kitchenClerks[clerkPosition].moveLeft();
                    //    Model.NotifyWhenMoved(pastX, pastY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);
                    //}

                    doneOrderQueueMut.WaitOne();
                    Console.WriteLine(Thread.CurrentThread.Name + " : Moving '" + doneOrderQueue.First<Order>().recipe.RecipeTitle + "' to comptoir");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Moving '" + doneOrderQueue.First<Order>().recipe.RecipeTitle + "' to comptoir");
                    doneOrderQueue.Dequeue();
                    doneOrderQueueMut.ReleaseMutex();

                    Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);

                    //for (int i = 0; i <= 4; i++)
                    //{
                    //    int pastX = Model.kitchenClerks[clerkPosition].PosX;
                    //    int pastY = Model.kitchenClerks[clerkPosition].PosY;
                    //    Model.kitchenClerks[clerkPosition].moveRight();
                    //    Model.NotifyWhenMoved(pastX, pastY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);
                    //}

                    //Model.kitchenClerks[clerkPosition].Sprite =  Model.kitchenClerks[clerkPosition].front;
                    //Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);
                }
            }
        }

        private static void diverTask()
        {
            while (true)
            {
                Model.NotifyFreeEmployee("Diver");
                notifyMaterialWashMut.WaitOne();
                materialWashQueueMre.WaitOne();

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int diverPosition = threadNumber - 1;
                Model.divers[diverPosition].Sprite = Model.divers[diverPosition].front;
                //Model.NotifyWhenMoved(Model.divers[diverPosition].PosX, Model.divers[diverPosition].PosY, Model.divers[diverPosition].PosX, Model.divers[diverPosition].PosY);

                materialWashQueueMut.WaitOne();
                Model.NotifyBusyEmployee("Diver");
                Console.WriteLine(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<Material>().name + "' received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<Material>().name + "' received.");

                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<Material>().name + "' ...");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<Material>().name + "' ...");

                Thread.Sleep(Model.TIME_SLEEP);
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<Material>().name + "' done.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<Material>().name + "' done.");

                materialWashQueue.First<Material>().quantity++;

                materialWashQueue.Dequeue();

                Model.divers[diverPosition].Sprite = Model.divers[diverPosition].front;
                //Model.NotifyWhenMoved(Model.divers[diverPosition].PosX, Model.divers[diverPosition].PosY, Model.divers[diverPosition].PosX, Model.divers[diverPosition].PosX);

                materialWashQueueMut.ReleaseMutex();
                materialWashQueueMre.Reset();
                notifyMaterialWashMut.ReleaseMutex();

            }
        }

        private static void DistantWaiterTask()
        {
            while (true)
            {
                Thread.Sleep(Model.TIME_SLEEP);
                orderQueueMut.WaitOne();
                for (int i = 0; i < Model.recipes.Count; i++)
                {
                    Order order = new Order(orderTitle, 1, nbOrders, nbOrders * Model.recipes[i].Price, Model.recipes[i]);
                    orderQueue.Enqueue(order);
                    Model.daoOrder.create(order);
                }
                orderQueueMre.Set();
                orderQueueMut.ReleaseMutex();
            }
        }
    }
}
