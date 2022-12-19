using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common.Move;

namespace AppRestaurant.Model
{
    public class Position : IPosition
    {
        public int posX;
        public int posY;

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
