using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public interface IObserver
    {
        void UpdateWithMoves(int oldX, int oldY, int newX, int newY);
    }
}
