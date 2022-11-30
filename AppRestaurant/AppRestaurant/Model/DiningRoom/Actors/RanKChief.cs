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
    public class RankChief : Position, IMove
    {
        private List<Square> squares;
        private bool available = true;

        public RankChief()
        {
            this.squares = new List<Square>();
            this.squares.Add(new Square());

        }

        public RankChief(int posX, int posY) : base(posX, posY)
        {
            this.squares = new List<Square>();
            this.squares.Add(new Square());
        }


        public List<Square> Squares { get => squares; set => squares = value; }
        public bool Available { get => available; set => available = value; }


        void IMove.Move(int posX, int posY) { 
            this.PosX = posX;   this.PosY = posY; }
    }
}
