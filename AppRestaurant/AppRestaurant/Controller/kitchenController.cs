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
                ChefThread.Name = "Chef (" + i + ")";
                ChefThread.Start();
            }

            Thread distantWaiterThread = new Thread(new ThreadStart(DistantWaiterTask));
            distantWaiterThread.Name = "Waiter";
            distantWaiterThread.Start();

            for (int i = 1; i <= model.deputyChefs.Length; i++)
            {
                Thread deputyChefThread = new Thread(new ThreadStart(deputyChefTask));
                deputyChefThread.Name = "Deputy Chef (" + i + ")";
                deputyChefThread.Start();
            }

            for (int i = 1; i <= model.kitchenClerks.Length; i++)
            {
                Thread KitchenClerkThread = new Thread(new ThreadStart(KitchenClerkTask));
                KitchenClerkThread.Name = "Kitchen Clerk (" + i + ")";
                KitchenClerkThread.Start();
            }

            for (int i = 1; i <= model.divers.Length; i++)
            {
                Thread diverThread = new Thread(new ThreadStart(diverTask));
                diverThread.Name = "Diver (" + i  + ")";
                diverThread.Start();
            }

            Application.Run(view.form);
        }

        private void ChefTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for an order... ");

                orderQueueMre.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " : Order received. ");
                Console.WriteLine(Thread.CurrentThread.Name + " : Sending to a deputy chef ");

                orderQueueMut.WaitOne();
                pendingOrderQueueMut.WaitOne();
                pendingOrderQueue.Enqueue(orderQueue.First<Order>());
                pendingOrderQueueMre.Set();
                pendingOrderQueueMut.ReleaseMutex();

                recipeQueueMut.WaitOne();
                recipeQueue.Enqueue(orderQueue.First<Order>().recipe);
                recipeQueueMre.Set();
                recipeQueueMut.ReleaseMutex();
                orderQueue.Dequeue();

                orderQueueMut.ReleaseMutex();

                orderQueueMre.Reset();
            }
        }

        private void deputyChefTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for Recipe... ");
                notifyOrderQueueMut.WaitOne();
                recipeQueueMre.WaitOne();
                recipeQueueMre.Reset();
                notifyOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe received. ");

                recipeQueueMut.WaitOne();
                Recipe recipe = recipeQueue.First<Recipe>();
                recipeQueue.Dequeue();
                recipeQueueMut.ReleaseMutex();


                foreach (RecipeStep task in recipe.recipeSteps)
                {
                    while (task.material.quantity == 0)
                    {
                        Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for " + task.material.name);
                        Thread.Sleep(2000);
                    }
                    --task.material.quantity;
                    Console.WriteLine(Thread.CurrentThread.Name + " : Doing task '" + task.name + "' ...");
                    Thread.Sleep(task.duration * TIME_SCALE);
                    Console.WriteLine(Thread.CurrentThread.Name + " : Task '" + task.name + "' done.");

                    materialWashQueueMut.WaitOne();
                    materialWashQueue.Enqueue(task.material);
                    materialWashQueueMre.Set();
                    materialWashQueueMut.ReleaseMutex();
                }

                Console.WriteLine(Thread.CurrentThread.Name + " : Recipe '" + recipe.name + "' done.");


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
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for done orders...");

                notifyDoneOrderQueueMut.WaitOne();
                doneOrderQueueMre.WaitOne();
                doneOrderQueueMre.Reset();
                notifyDoneOrderQueueMut.ReleaseMutex();

                Console.WriteLine(Thread.CurrentThread.Name + " : Done order received");

                doneOrderQueueMut.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " : Moving'" + doneOrderQueue.First<Order>().recipe.name + "' to comptoir");
                doneOrderQueue.Dequeue();
                doneOrderQueueMut.ReleaseMutex();
            }
        }

        private void diverTask()
        {
            while (true)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " : Waiting for material to wash...");
                notifyMaterialWashMut.WaitOne();
                materialWashQueueMre.WaitOne();
                materialWashQueueMut.WaitOne();
                Console.WriteLine(Thread.CurrentThread.Name + " : Material '" + materialWashQueue.First<kitchenMaterial>().name + "' received.");
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<kitchenMaterial>().name + "' ...");
                Thread.Sleep(2000);
                Console.WriteLine(Thread.CurrentThread.Name + " : Washing Material '" + materialWashQueue.First<kitchenMaterial>().name + "' done.");

                kitchenMaterial material = materialWashQueue.First<kitchenMaterial>();
                material.quantity++;

                materialWashQueue.Dequeue();

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
