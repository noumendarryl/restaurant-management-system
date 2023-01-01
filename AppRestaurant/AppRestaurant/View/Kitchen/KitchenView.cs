using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen;
using System.Windows.Forms;
using AppRestaurant.Controller.Kitchen.Observer;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.View.Kitchen
{
    public class KitchenView : IObserver
    {
        public static int FRAME_SIZE = 32;

        public static AppRestaurant mainApp { get; set; }
        public static Simulation simulationForm { get; set; }
        public static Setting setting { get; set; }

        public KitchenView(KitchenModel model)
        {
            mainApp = new AppRestaurant(model);
            simulationForm = new Simulation(model);
            setting = new Setting(simulationForm);
        }

        void IObserver.UpdateWithMoves(int oldX, int oldY, int newX, int newY)
        {
            simulationForm.panel5.Invoke((MethodInvoker)delegate
            {
                simulationForm.panel5.Invalidate(new Rectangle(oldX * FRAME_SIZE, oldY * FRAME_SIZE, FRAME_SIZE, FRAME_SIZE));
                simulationForm.panel5.Invalidate(new Rectangle(newX * FRAME_SIZE, newY * FRAME_SIZE, FRAME_SIZE, FRAME_SIZE));
                simulationForm.panel5.Update();
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
            simulationForm.Invoke((MethodInvoker)delegate
            {
                simulationForm.richTextBox.AppendText(str + "\n");
            });
        }
    }
}
