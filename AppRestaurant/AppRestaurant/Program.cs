using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.View;
using AppRestaurant.Controller;
using AppRestaurant.Model.kitchen;

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
            kitchenModel model = new kitchenModel();
            kitchenView view = new kitchenView(model);
            kitchenController controller = new kitchenController(model, view);

            Application.Run(kitchenView.mainApp);
            Application.Run(kitchenView.setting);
            Application.Run(kitchenView.simulationForm);
            //controller.Start();
        }
    }
}
