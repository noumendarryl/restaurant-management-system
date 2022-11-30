using AppRestaurant.Model.DiningRoom.Position;

namespace AppRestaurant.Model.DiningRoom.Position
{
    public class Position : IPosition
    {
        private int posX;
        private int posY;

        public Position()
        {
            posX = 0;
            posY = 0;
        }

        public Position(int posX, int posY)
        {
            this.posX = posX >= 0 ? posX : 0;
            this.posY = posY >= 0 ? posY : 0;

        }

        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                posX = value >= 0 ? value : 0;
            }
        }

        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                posY = value >= 0 ? value : 0;
            }
        }
    }
}
