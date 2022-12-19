using AppRestaurant.Model.kitchen;
using AppRestaurant.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AppRestaurant.Controller
{
    public class KitchenController : IObserver<Order>
    {
        public static KitchenModel Model { get; set; }
        public static KitchenView View { get; set; }
        public static Queue<Order> orderQueue { get; set; }
        private static Mutex orderQueueMut = new Mutex();
        private static ManualResetEvent orderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyOrderQueueMut = new Mutex();
        public static Queue<Order> pendingOrderQueue { get; set; }
        private static Mutex pendingOrderQueueMut = new Mutex();
        private static ManualResetEvent pendingOrderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyPendingOrderQueueMut = new Mutex();

        public static Queue<Order> doneOrderQueue { get; set; }
        private static Mutex doneOrderQueueMut = new Mutex();
        private static ManualResetEvent doneOrderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyDoneOrderQueueMut = new Mutex();

        public static Queue<Recipe> recipeQueue { get; set; }
        private static Mutex recipeQueueMut = new Mutex();
        private static ManualResetEvent recipeQueueMre = new ManualResetEvent(false);
        private static Mutex notifyRecipeQueueMut = new Mutex();

        public static Queue<Ingredient> ingredientQueue { get; set; }
        private static Mutex ingredientQueueMut = new Mutex();
        private static ManualResetEvent ingredientQueueMre = new ManualResetEvent(false);
        private static Mutex notifyIngredientQueueMut = new Mutex();

        public static Queue<RecipeStep> RecipeStepQueue { get; set; }
        private static Mutex RecipeStepQueueMut = new Mutex();
        private static ManualResetEvent RecipeStepQueueMre = new ManualResetEvent(false);
        private static Mutex notifyRecipeStepQueueMut = new Mutex();

        public static Queue<KitchenMaterial> materialWashQueue { get; set; }
        private static Mutex materialWashQueueMut = new Mutex();
        private static ManualResetEvent materialWashQueueMre = new ManualResetEvent(false);
        private static Mutex notifyMaterialWashMut = new Mutex();

        public static Thread ChefThread { get; set; }
        public static Thread distantWaiterThread { get; set; }
        public static Thread deputyChefThread { get; set; }
        public static Thread KitchenClerkThread { get; set; }
        public static Thread diverThread { get; set; }

        public KitchenController(KitchenModel model, KitchenView view)
        {
            Model = model;
            View = view;

            orderQueue = new Queue<Order>();
            pendingOrderQueue = new Queue<Order>();
            doneOrderQueue = new Queue<Order>();
            recipeQueue = new Queue<Recipe>();
            ingredientQueue = new Queue<Ingredient>();
            RecipeStepQueue = new Queue<RecipeStep>();
            materialWashQueue = new Queue<KitchenMaterial>();
        }

        public static void Start()
        {
            //Application.Run(kitchenView.mainApp);
            //Application.Run(kitchenView.setting);
            Model.AddObserver(View);

            for (int i = 1; i <= Model.chefs.Length; i++)
            {
                ChefThread = new Thread(new ThreadStart(ChefTask));
                ChefThread.Name = "Chef " + i;
                ChefThread.Start();
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
            //Application.Run(kitchenView.simulationForm);
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
                //Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for an order...");
                //Model.NotifyEventLog(Thread.CurrentThread.Name + " : Waiting for an order...");
                notifyOrderQueueMut.WaitOne();
                orderQueueMre.WaitOne();
                orderQueueMre.Reset();
                notifyOrderQueueMut.ReleaseMutex();
                Console.WriteLine(Thread.CurrentThread.Name + " : Order received.");
                Console.WriteLine(Thread.CurrentThread.Name + " : Sending to a deputy chef");

                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Order received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Sending to a deputy chef");
                Model.NotifyBusyEmployee("chef");

                // Changing sprite
                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int chefPosition = threadNumber - 1;

                int pastX = Model.deputyChefs[chefPosition].PosX;
                int pastY = Model.deputyChefs[chefPosition].PosY;
                Model.chefs[chefPosition].moveRight();
                Model.NotifyWhenMoved(pastX, pastY, Model.chefs[chefPosition].PosX, Model.chefs[chefPosition].PosY);

                orderQueueMut.WaitOne();
                pendingOrderQueueMut.WaitOne();
                pendingOrderQueue.Enqueue(orderQueue.First<Order>());
                pendingOrderQueueMre.Set();
                pendingOrderQueueMut.ReleaseMutex();

                recipeQueueMut.WaitOne();
                recipeQueue.Enqueue(orderQueue.First<Order>().recipe);
                Thread.Sleep(2000);
                recipeQueueMre.Set();
                recipeQueueMut.ReleaseMutex();
                orderQueue.Dequeue();
                orderQueueMut.ReleaseMutex();

                // Changing sprite
                //Model.chefs[chefPosition].getSprite();
                //Model.NotifyWhenMoved(Model.chefs[chefPosition].posX, Model.chefs[chefPosition].posY, Model.chefs[chefPosition].posX, Model.chefs[chefPosition].posY);
            }
        }

        private static void deputyChefTask()
        {
            while (true)
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for Recipe...");
                //Model.NotifyEventLog(Thread.CurrentThread.Name + " : Waiting for Recipe...");
                Model.NotifyFreeEmployee("deputy chef");
                notifyOrderQueueMut.WaitOne();
                recipeQueueMre.WaitOne();
                recipeQueueMre.Reset();

                notifyOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Recipe received.");
                Model.NotifyBusyEmployee("deputy chef");

                recipeQueueMut.WaitOne();
                Recipe recipe = recipeQueue.First<Recipe>();
                recipeQueue.Dequeue();
                recipeQueueMut.ReleaseMutex();

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int deputyChefPosition = threadNumber - 1;
                //Model.deputyChefs[deputyChefPosition].setSprite(Model.chefs[deputyChefPosition].right);
                //Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY, Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY);

                //for (int i = 0; i <= 4; i++)
                //{
                //    int pastX = Model.deputyChefs[deputyChefPosition].posX;
                //    int pastY = Model.deputyChefs[deputyChefPosition].posY;
                //    Model.deputyChefs[deputyChefPosition].moveRight();
                //    Model.NotifyWhenMoved(pastX, pastY, Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY);
                //}

                //if (deputyChefPosition % 2 == 0)
                //    Model.deputyChefs[deputyChefPosition].currentSprite = Model.deputyChefs[deputyChefPosition].GetSprite("moving-up");
                //else
                //    Model.deputyChefs[deputyChefPosition].currentSprite = Model.deputyChefs[deputyChefPosition].GetSprite("moving-down");

                //Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY, Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY);

                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    ingredient.quantity--;
                    //Ingredient ing = ((DAOEntity<Ingredient>)Model.dao).find(ingredient.name);

                    //((DAOEntity<Ingredient>)Model.dao).update(ing.id, ing.quantity);
                }

                foreach (RecipeStep task in recipe.recipeSteps)
                {
                    while (task.material.quantity == 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Model.NotifyEventLog(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Thread.Sleep(2000);
                    }
                    task.material.quantity--;
                    Model.NotifyMaterialAvailaibility(task.material.name);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");

                    Thread.Sleep(task.duration * Model.TIME_SCALE);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Task '" + task.name + "' done.");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");

                    materialWashQueueMut.WaitOne();
                    if (task.material.washable)
                    {
                        materialWashQueue.Enqueue(task.material);
                        materialWashQueueMre.Set();
                    }
                    else
                        task.material.quantity++;
                    materialWashQueueMut.ReleaseMutex();
                }

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe '" + recipe.name + "' done.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Recipe '" + recipe.name + "' done.");
                //Model.deputyChefs[deputyChefPosition].setSprite(Model.chefs[deputyChefPosition].left);

                //for (int i = 0; i <= 4; i++)
                //{
                //    int pastX = Model.deputyChefs[deputyChefPosition].posX;
                //    int pastY = Model.deputyChefs[deputyChefPosition].posY;
                //    Model.deputyChefs[deputyChefPosition].moveLeft();
                //    Model.NotifyWhenMoved(pastX, pastY, Model.deputyChefs[deputyChefPosition].posX, Model.deputyChefs[deputyChefPosition].posY);
                //}

                //if (deputyChefPosition % 2 == 0)
                //    Model.deputyChefs[deputyChefPosition].currentSprite = Model.deputyChefs[deputyChefPosition].GetSprite("moving-up");
                //else
                //    Model.deputyChefs[deputyChefPosition].currentSprite = Model.deputyChefs[deputyChefPosition].GetSprite("moving-down");

                Model.NotifyWhenMoved(Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY, Model.deputyChefs[deputyChefPosition].PosX, Model.deputyChefs[deputyChefPosition].PosY);

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
                //Console.WriteLine(Thread.CurrentThread.Name + ": Waiting for done orders...");
                //Model.NotifyEventLog(Thread.CurrentThread.Name + ": Waiting for done orders...");
                Model.NotifyFreeEmployee("kitchen clerk");
                notifyDoneOrderQueueMut.WaitOne();

                if (doneOrderQueueMre.WaitOne())
                {
                    doneOrderQueueMre.Reset();
                    notifyDoneOrderQueueMut.ReleaseMutex();

                    Console.WriteLine(Thread.CurrentThread.Name + " : Done order received");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Done order received");
                    Model.NotifyBusyEmployee("kitchen clerk");

                    int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                    int clerkPosition = threadNumber - 1;
                    Model.kitchenClerks[clerkPosition].setSprite(Model.kitchenClerks[clerkPosition].front);
                    //Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY, Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY);

                    //for (int i = 0; i <= 4; i++)
                    //{
                    //    int pastX = Model.kitchenClerks[clerkPosition].posX;
                    //    int pastY = Model.kitchenClerks[clerkPosition].posY;
                    //    Model.kitchenClerks[clerkPosition].moveLeft();
                    //    Model.NotifyWhenMoved(pastX, pastY, Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY);
                    //}

                    doneOrderQueueMut.WaitOne();
                    Console.WriteLine(Thread.CurrentThread.Name + " : Moving '" + doneOrderQueue.First<Order>().recipe.name + "' to comptoir");
                    Model.NotifyEventLog(Thread.CurrentThread.Name + " : Moving '" + doneOrderQueue.First<Order>().recipe.name + "' to comptoir");
                    doneOrderQueue.Dequeue();
                    doneOrderQueueMut.ReleaseMutex();

                    //Model.kitchenClerks[clerkPosition].setSprite(Model.kitchenClerks[clerkPosition].right);
                    //Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY, Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY);

                    //for (int i = 0; i <= 4; i++)
                    //{
                    //    int pastX = Model.kitchenClerks[clerkPosition].posX;
                    //    int pastY = Model.kitchenClerks[clerkPosition].posY;
                    //    Model.kitchenClerks[clerkPosition].moveRight();
                    //    Model.NotifyWhenMoved(pastX, pastY, Model.kitchenClerks[clerkPosition].posX, Model.kitchenClerks[clerkPosition].posY);
                    //}

                    //Model.kitchenClerks[clerkPosition].setSprite(Model.kitchenClerks[clerkPosition].back);
                    Model.NotifyWhenMoved(Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY, Model.kitchenClerks[clerkPosition].PosX, Model.kitchenClerks[clerkPosition].PosY);
                }
            }
        }

        private static void diverTask()
        {
            while (true)
            {
                //Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for a dirty material...");
                //Model.NotifyEventLog(Thread.CurrentThread.Name + " : Waiting for a dirty material...");
                Model.NotifyFreeEmployee("diver");
                notifyMaterialWashMut.WaitOne();
                materialWashQueueMre.WaitOne();

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int diverPosition = threadNumber - 1;
                //Model.divers[diverPosition].setSprite(Model.divers[diverPosition].right);
                //Model.NotifyWhenMoved(Model.divers[diverPosition].posX, Model.divers[diverPosition].posY, Model.divers[diverPosition].posX, Model.divers[diverPosition].posY);

                materialWashQueueMut.WaitOne();
                Model.NotifyBusyEmployee("diver");
                Console.WriteLine(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<KitchenMaterial>().name + "' received.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<KitchenMaterial>().name + "' received.");

                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<KitchenMaterial>().name + "' ...");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<KitchenMaterial>().name + "' ...");

                Thread.Sleep(2000);
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<KitchenMaterial>().name + "' done.");
                Model.NotifyEventLog(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<KitchenMaterial>().name + "' done.");

                materialWashQueue.First<KitchenMaterial>().quantity++;

                materialWashQueue.Dequeue();

                //Model.divers[diverPosition].setSprite(Model.divers[diverPosition].left);
                //Model.NotifyWhenMoved(Model.divers[diverPosition].posX, Model.divers[diverPosition].posY, Model.divers[diverPosition].posX, Model.divers[diverPosition].posX);

                materialWashQueueMut.ReleaseMutex();
                materialWashQueueMre.Reset();
                notifyMaterialWashMut.ReleaseMutex();

            }
        }

        private static void DistantWaiterTask()
        {
            while (true)
            {
                Thread.Sleep(2000);
                orderQueueMut.WaitOne();
                orderQueue.Enqueue(new Order(1, 5, 80.30, Model.recipes[0]));
                orderQueueMre.Set();
                orderQueueMut.ReleaseMutex();
            }
        }

        public void OnNext(Order value)
        {
           // throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
