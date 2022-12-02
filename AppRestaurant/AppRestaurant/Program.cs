using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Model.DiningRoom;


namespace AppRestaurant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DiningRoomModel diningRoomModel = new DiningRoomModel();
            DiningRoomController diningRoomController = new DiningRoomController(diningRoomModel);
            diningRoomController.Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
