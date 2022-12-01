using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model;
using AppRestaurant.Model.kitchen;
using AppRestaurant.View;

namespace AppRestaurant.Controller
{
    public class kitchenController
    {
        private static int TIME_SCALE = 1000;

        public kitchenModel model { get; set; }
        public kitchenView view { get; set; }
        public Queue<Order> orderQueue { get; set; }
        private static Mutex orderQueueMut = new Mutex();
        private static ManualResetEvent orderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyOrderQueueMut = new Mutex();
        public Queue<Order> pendingOrderQueue { get; set; }
        private static Mutex pendingOrderQueueMut = new Mutex();
        private static ManualResetEvent pendingOrderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyPendingOrderQueueMut = new Mutex();

        public Queue<Order> doneOrderQueue { get; set; }
        private static Mutex doneOrderQueueMut = new Mutex();
        private static ManualResetEvent doneOrderQueueMre = new ManualResetEvent(false);
        private static Mutex notifyDoneOrderQueueMut = new Mutex();

        public Queue<Recipe> recipeQueue { get; set; }
        private static Mutex recipeQueueMut = new Mutex();
        private static ManualResetEvent recipeQueueMre = new ManualResetEvent(false);
        private static Mutex notifyRecipeQueueMut = new Mutex();

        public Queue<Ingredient> ingredientQueue { get; set; }
        private static Mutex ingredientQueueMut = new Mutex();
        private static ManualResetEvent ingredientQueueMre = new ManualResetEvent(false);
        private static Mutex notifyIngredientQueueMut = new Mutex();

        public Queue<RecipeStep> RecipeStepQueue { get; set; }
        private static Mutex RecipeStepQueueMut = new Mutex();
        private static ManualResetEvent RecipeStepQueueMre = new ManualResetEvent(false);
        private static Mutex notifyRecipeStepQueueMut = new Mutex();

        public Queue<kitchenMaterial> materialWashQueue { get; set; }
        private static Mutex materialWashQueueMut = new Mutex();
        private static ManualResetEvent materialWashQueueMre = new ManualResetEvent(false);
        private static Mutex notifyMaterialWashMut = new Mutex();

        public kitchenController(kitchenModel model, kitchenView view)
        {
            this.model = model;
            this.view = view;

            orderQueue = new Queue<Order>();
            pendingOrderQueue = new Queue<Order>();
            doneOrderQueue = new Queue<Order>();
            recipeQueue = new Queue<Recipe>();
            ingredientQueue = new Queue<Ingredient>();
            RecipeStepQueue = new Queue<RecipeStep>();
            materialWashQueue = new Queue<kitchenMaterial>();
        }

        public void Start()
        {
            model.AddObserver(view);

            for (int i = 1; i <= model.chefs.Length; i++)
            {
                Thread ChefThread = new Thread(new ThreadStart(ChefTask));
                ChefThread.Name = "Chef " + i;
                ChefThread.Start();
            }

            Thread distantWaiterThread = new Thread(new ThreadStart(DistantWaiterTask));
            distantWaiterThread.Name = "Waiter";
            distantWaiterThread.Start();

            for (int i = 1; i <= model.deputyChefs.Length; i++)
            {
                Thread deputyChefThread = new Thread(new ThreadStart(deputyChefTask));
                deputyChefThread.Name = "Deputy Chef " + i;
                deputyChefThread.Start();
            }

            for (int i = 1; i <= model.kitchenClerks.Length; i++)
            {
                Thread KitchenClerkThread = new Thread(new ThreadStart(KitchenClerkTask));
                KitchenClerkThread.Name = "Kitchen Clerk " + i;
                KitchenClerkThread.Start();
            }

            for (int i = 1; i <= model.divers.Length; i++)
            {
                Thread diverThread = new Thread(new ThreadStart(diverTask));
                diverThread.Name = "Diver " + i;
                diverThread.Start();
            }

            Application.Run(view.form);
        }

        private void ChefTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for an order...");

                notifyOrderQueueMut.WaitOne();
                orderQueueMre.WaitOne();
                orderQueueMre.Reset();
                notifyOrderQueueMut.ReleaseMutex();
                Console.WriteLine(Thread.CurrentThread.Name + " : Order received.");
                Console.WriteLine(Thread.CurrentThread.Name + " : Sending to a deputy chef");
                
                // Changing sprite
                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int chefPosition = threadNumber - 1;
                model.chefs[chefPosition].setSprite(model.chefs[chefPosition].front);
                model.NotifyWhenMoved(model.chefs[chefPosition].posX, model.chefs[chefPosition].posY, model.chefs[chefPosition].posX, model.chefs[chefPosition].posY);

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
                model.chefs[chefPosition].setSprite(model.chefs[chefPosition].stop);
                model.NotifyWhenMoved(model.chefs[chefPosition].posX, model.chefs[chefPosition].posY, model.chefs[chefPosition].posX, model.chefs[chefPosition].posY);
            }
        }

        private void deputyChefTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for Recipe...");
                notifyOrderQueueMut.WaitOne();
                recipeQueueMre.WaitOne();
                recipeQueueMre.Reset();

                notifyOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe received.");

                recipeQueueMut.WaitOne();
                Recipe recipe = recipeQueue.First<Recipe>();
                recipeQueue.Dequeue();
                recipeQueueMut.ReleaseMutex();

                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int deputyChefPosition = threadNumber - 1;
                model.deputyChefs[deputyChefPosition].setSprite(model.chefs[deputyChefPosition].right);
                model.NotifyWhenMoved(model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY, model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY);

                for (int i = 0; i <= 4; i++)
                {
                    int pastX = model.deputyChefs[deputyChefPosition].posX;
                    int pastY = model.deputyChefs[deputyChefPosition].posY;
                    model.deputyChefs[deputyChefPosition].moveRight();
                    model.NotifyWhenMoved(model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY, model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY);
                }

                //if (deputyChefPosition % 2 == 0)
                //    model.deputyChefs[deputyChefPosition].currentSprite = model.deputyChefs[deputyChefPosition].GetSprite("moving-up");
                //else
                //    model.deputyChefs[deputyChefPosition].currentSprite = model.deputyChefs[deputyChefPosition].GetSprite("moving-down");

                model.NotifyWhenMoved(model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY, model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY);

                foreach (RecipeStep task in recipe.recipeSteps)
                {
                    while (task.material.quantity == 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Thread.Sleep(2000);
                    }

                    task.material.quantity--;
                    Console.WriteLine(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");
                    Thread.Sleep(task.duration * TIME_SCALE);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Task '" + task.name + "' done.");
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
                model.deputyChefs[deputyChefPosition].setSprite(model.chefs[deputyChefPosition].left);

                for (int i = 0; i <= 4; i++)
                {
                    int pastX = model.deputyChefs[deputyChefPosition].posX;
                    int pastY = model.deputyChefs[deputyChefPosition].posY;
                    model.deputyChefs[deputyChefPosition].moveLeft();
                    model.NotifyWhenMoved(model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY, model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY);
                }

                //if (deputyChefPosition % 2 == 0)
                //    model.deputyChefs[deputyChefPosition].currentSprite = model.deputyChefs[deputyChefPosition].GetSprite("moving-up");
                //else
                //    model.deputyChefs[deputyChefPosition].currentSprite = model.deputyChefs[deputyChefPosition].GetSprite("moving-down");

                model.NotifyWhenMoved(model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY, model.deputyChefs[deputyChefPosition].posX, model.deputyChefs[deputyChefPosition].posY);

                doneOrderQueueMut.WaitOne();
                pendingOrderQueueMut.WaitOne();
                doneOrderQueue.Enqueue(pendingOrderQueue.First<Order>());
                doneOrderQueueMre.Set();
                pendingOrderQueueMut.ReleaseMutex();
                doneOrderQueueMut.ReleaseMutex();
            }
        }

        private void KitchenClerkTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + ": Waiting for done orders...");

                notifyDoneOrderQueueMut.WaitOne();

                if (doneOrderQueueMre.WaitOne())
                {
                    doneOrderQueueMre.Reset();
                    notifyDoneOrderQueueMut.ReleaseMutex();

                    Console.WriteLine(Thread.CurrentThread.Name + " : Done order received");

                    int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                    int clerkPosition = threadNumber - 1;
                    model.kitchenClerks[clerkPosition].setSprite(model.kitchenClerks[clerkPosition].front);
                    model.NotifyWhenMoved(model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY, model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY);

                    for (int i = 0; i <= 4; i++)
                    {
                        int pastX = model.kitchenClerks[clerkPosition].posX;
                        int pastY = model.kitchenClerks[clerkPosition].posY;
                        model.kitchenClerks[clerkPosition].moveLeft();
                        model.NotifyWhenMoved(pastX, pastY, model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY);
                    }

                    doneOrderQueueMut.WaitOne();
                    Console.WriteLine(Thread.CurrentThread.Name + " : Moving'" + doneOrderQueue.First<Order>().recipe.name + "' to comptoir");
                    doneOrderQueue.Dequeue();
                    doneOrderQueueMut.ReleaseMutex();

                    model.kitchenClerks[clerkPosition].setSprite(model.kitchenClerks[clerkPosition].right);
                    model.NotifyWhenMoved(model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY, model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY);

                    for (int i = 0; i <= 4; i++)
                    {
                        int pastX = model.kitchenClerks[clerkPosition].posX;
                        int pastY = model.kitchenClerks[clerkPosition].posY;
                        model.kitchenClerks[clerkPosition].moveRight();
                        model.NotifyWhenMoved(pastX, pastY, model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY);
                    }

                    model.kitchenClerks[clerkPosition].setSprite(model.kitchenClerks[clerkPosition].back);
                    model.NotifyWhenMoved(model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY, model.kitchenClerks[clerkPosition].posX, model.kitchenClerks[clerkPosition].posY);
                }
            }
        }

        private void diverTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for a dirty material...");
                notifyMaterialWashMut.WaitOne();
                materialWashQueueMre.WaitOne();


                int threadNumber = int.Parse((Thread.CurrentThread.Name[Thread.CurrentThread.Name.Length - 1]) + "");
                int diverPosition = threadNumber - 1;
                model.divers[diverPosition].setSprite(model.divers[diverPosition].right);
                model.NotifyWhenMoved(model.divers[diverPosition].posX, model.divers[diverPosition].posY, model.divers[diverPosition].posX, model.divers[diverPosition].posY);

                materialWashQueueMut.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<kitchenMaterial>().name + "' received.");
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<kitchenMaterial>().name + "' ...");
                Thread.Sleep(2000);
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<kitchenMaterial>().name + "' done.");

                materialWashQueue.First<kitchenMaterial>().quantity++;

                materialWashQueue.Dequeue();

                model.divers[diverPosition].setSprite(model.divers[diverPosition].left);
                model.NotifyWhenMoved(model.divers[diverPosition].posX, model.divers[diverPosition].posY, model.divers[diverPosition].posX, model.divers[diverPosition].posX);

                materialWashQueueMut.ReleaseMutex();
                materialWashQueueMre.Reset();
                notifyMaterialWashMut.ReleaseMutex();

            }
        }

        private void DistantWaiterTask()
        {
            while (true)
            {
                Thread.Sleep(2000);
                orderQueueMut.WaitOne();
                orderQueue.Enqueue(new Order(1, 5, 80.30, model.recipes[0]));
                orderQueueMre.Set();
                orderQueueMut.ReleaseMutex();
            }
        }
    }
}
