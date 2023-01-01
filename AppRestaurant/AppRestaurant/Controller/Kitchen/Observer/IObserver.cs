using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Controller.Kitchen.Observer
{
    public interface IObserver
    {
        void UpdateWithMoves(int oldX, int oldY, int newX, int newY);
        void UpdateMaterial(string name, MaterialState state);
        void UpdateFreeMobilekitchenItem(string name);
        void UpdateBusyMobilekitchenItem(string name);
        void UpdateTaskEmployee(string name);
        void UpdateEventLog(string str);

    }
}
