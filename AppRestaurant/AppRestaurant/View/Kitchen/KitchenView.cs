using System;
using System.Threading;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Controller.Kitchen.Observer;
using AppRestaurant.Model.Kitchen.Materials;
using AppRestaurant.View.Common;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace AppRestaurant.View.Kitchen
{
    public class KitchenView : IObserver
    {
        public static int FRAME_SIZE = 32;

        public static MainApp app { get; set; }
        public static Setting setting { get; set; }
        public static Simulation simulation { get; set; }
        public static Monitoring monitoring { get; set; }
        public static Inventory inventory { get; set; }
        public static Booking booking { get; set; }
        public StreamWriter writer { get; set; }
        public string applicationPath { get; set; }
        public string logFilePath { get; set; }
        public DateTime dateTime { get; set; }

        public KitchenView(KitchenModel model)
        {
            simulation = new Simulation(model);
            monitoring = new Monitoring();
            inventory = new Inventory();
            booking = new Booking();
            setting = new Setting(simulation, monitoring);
            app = new MainApp(simulation, setting, monitoring, inventory, booking);
        }

        void IObserver.UpdateWithMoves(int oldX, int oldY, int newX, int newY)
        {
            simulation.panel4.Invoke((MethodInvoker)delegate
            {
                simulation.panel4.Invalidate(new Rectangle(oldX * FRAME_SIZE, oldY * FRAME_SIZE, FRAME_SIZE, FRAME_SIZE));
                simulation.panel4.Invalidate(new Rectangle(newX * FRAME_SIZE, newY * FRAME_SIZE, FRAME_SIZE, FRAME_SIZE));
                simulation.panel4.Update();
            });

            Thread.Sleep(40);
        }

        
        void IObserver.UpdateMaterial(string name, MaterialState state)
        {
            throw new NotImplementedException();
        }

        void IObserver.UpdateFreeMobilekitchenItem(string name)
        {
            switch (name)
            {
                case "Chef":
                    break;
                case "Deputy Chef":
                    break;
                case "KitchenClerk":
                    break;
                case "Diver":
                    break;
            }
        }

        void IObserver.UpdateBusyMobilekitchenItem(string name)
        {
            switch (name)
            {
                case "Chef":
                    break;
                case "Deputy Chef":
                    break;
                case "KitchenClerk":
                    break;
                case "Diver":
                    break;
            }
        }

        void IObserver.UpdateTaskEmployee(string name)
        {
            throw new NotImplementedException();
        }

        void IObserver.UpdateEventLog(string str)
        {
            simulation.Invoke((MethodInvoker)delegate
            {
                simulation.siticoneTextBox1.AppendText(str + "\n");
                saveLogs(str);
            });
        }

        private void saveLogs(string log)
        {
            // The directory that your program is installed in
            applicationPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            logFilePath = Path.Combine(applicationPath, "SimulationLogs.txt");
            writer = new StreamWriter(logFilePath, true);
            dateTime = DateTime.Now;

            writer.WriteLine(dateTime + "    " + log.ToString());

            writer.Close();
        }
    }
}
