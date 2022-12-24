using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Controller.Kitchen;
using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.View.Kitchen;

namespace AppRestaurant
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            KitchenModel model = new KitchenModel();
            KitchenView view = new KitchenView(model);
            KitchenController controller = new KitchenController(model, view);

            Application.Run(KitchenView.mainApp);
            Application.Run(KitchenView.setting);
            Application.Run(KitchenView.simulationForm);
            //controller.Start();
        }
    }
}
