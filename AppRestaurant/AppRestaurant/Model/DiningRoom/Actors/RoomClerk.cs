using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common.Move;
namespace AppRestaurant.Model.DiningRoom.Actors
{
    public class RoomClerk : Position, IMove
    {
        RoomClerk(int posX, int posY) : base(posX, posY) { }

        public RoomClerk() :base() { }

        public void Move(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }
    }
}
