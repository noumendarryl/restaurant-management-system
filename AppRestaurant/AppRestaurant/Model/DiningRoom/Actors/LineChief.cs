using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.DiningRoom.Move;

namespace AppRestaurant.Model.DiningRoom.Actors
{
    public class LineChief : Position, IMove
    {
        private Line line;
        private bool available = true;

        public LineChief()
        {
        }

        public LineChief(int posX, int posY) : base(posX, posY)
        {
        }

        public bool Available { get => available; set => available = value; }


        public void Move(int posX, int posY) { 
            this.PosX = posX;   this.PosY = posY; }
    }
}
