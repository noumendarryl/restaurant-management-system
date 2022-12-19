using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.Common.Move;

namespace AppRestaurant.Model.DiningRoom.Actors
{
    public class LineChief
    {
        private bool available = true;

        public LineChief()
        {
            available = true;
        }

        public bool Available { get => available; set => available = value; }

    }
}
