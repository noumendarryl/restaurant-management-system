using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Move;

namespace AppRestaurant.Model.DiningRoom.Elements
{
    public class Table : Equipment, IPosition
    {
        private int nbPlaces;
        private Customer group;
        private bool entree = false;
        private bool plate = false;
        private bool dessert = false;
        private int posX = 0;
        private int posY = 0;


        public Table(int nbPlaces)
        {
            this.nbPlaces = nbPlaces;
        }

        public Table(int nbPlaces, int posX, int posY)
        {
            this.nbPlaces = nbPlaces;
            this.posX = posX;
            this.posY = posY;
        }

        public int NbPlaces { get => nbPlaces; set => nbPlaces = value; }
        public Customer Group { get => group; set => group = value; }
        public bool Entree { get => entree; set => entree = value; }
        public bool Plate { get => plate; set => plate = value; }
        public bool Dessert { get => dessert; set => dessert = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
    }
}
