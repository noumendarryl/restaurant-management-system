using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.DiningRoom.Move;

namespace AppRestaurant.Model.DiningRoom.Actors
{
    public class Waiter : Position, IMove
    {

        public Waiter(int posX, int posY) : base(posX, posY){
        }
        public Waiter(Position position) : base(position.PosX, position.PosY){
        }
        public Waiter() : base(){
        }
        public void Move(int posX, int posY) { this.PosX = posX; this.PosY = posY; }

    }
}