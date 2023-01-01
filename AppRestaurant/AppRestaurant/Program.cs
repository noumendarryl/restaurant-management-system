using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Controller.Kitchen;
using AppRestaurant.Controller.Pipes;
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
            // Dining Room Initialization
            DiningRoomModel diningRoomModel = new DiningRoomModel();
            DiningRoomController diningRoomController = new DiningRoomController(diningRoomModel);
            //ServerThread server = new ServerThread();
            //ClientThread client = new ClientThread();
            //server.WriteFromServer("SYNC;Hello from Dining room !;END");
            //client.ReadFromClient();
            //client.WriteFromClient("SYNC;Hello from Kitchen !;END");
            //server.ReadFromServer();

            // Kitchen Initialization
            KitchenModel kitchenModel = new KitchenModel();
            KitchenView kitchenView = new KitchenView(kitchenModel);
            KitchenController kitchenController = new KitchenController(kitchenModel, kitchenView);

            // Launching User Interfaces
            Application.Run(KitchenView.mainApp);
            Application.Run(KitchenView.setting);
            Application.Run(KitchenView.simulationForm);
        }
    }
}
