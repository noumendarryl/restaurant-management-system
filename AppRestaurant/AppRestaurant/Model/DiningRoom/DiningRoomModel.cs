using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen;

namespace AppRestaurant.Model.DiningRoom
{
    public class DiningRoomModel
    {
        private HotelMaster hotelMaster;
        private  List<RoomClerk> roomclerks;
        private  List<Waiter> waiters;
        private  List<Square> squares;
        private  List<LineChief> lineChiefs;
        private MenuCard menuCard;
        private Queue<MenuCard> menuCards;


        Dictionary<string, List<Recipe>> menu;

        public DiningRoomModel()
        {
            menu = new Dictionary<string, List<Recipe>>();

            menu.Add("Entry",new List<Recipe>() { new Recipe(RecipeType.Entry, "Feuillete de crabe",4, 1000), new Recipe(RecipeType.Entry, "Oeufs cocotte",4,200) });
            menu.Add("Dish", new List<Recipe>() { new Recipe(RecipeType.Dish, "Bouillinade anguilles ou poissons",4,345), new Recipe(RecipeType.Dish, "Boles de picoulats", 25, 560) });
            menu.Add("Dessert", new List<Recipe>() { new Recipe(RecipeType.Dessert, "Tiramisu", 4, 1200), new Recipe(RecipeType.Dessert,"Creme brule", 3, 450) });

            menuCard = new MenuCard();

            menuCard.Menu = menu;

            hotelMaster = new HotelMaster();
            
            squares = new List<Square>();

            roomclerks = new List<RoomClerk>();

            lineChiefs = new List<LineChief>();

            squares.Add(new Square());

            lineChiefs.Add(new LineChief());

            menuCards = new Queue<MenuCard>();

            for(int i = 0; i < 40; i++)
            {
                MenuCard mCard = new MenuCard();
                mCard.Menu = menu;
                menuCards.Enqueue(mCard);
            }

            squares[0].Lines.Add(new Line(4,6));
            //squares[0].Lines.Add(new Line());
            squares[0].Waiters.Add(new Waiter());

            squares[0].lineChiefs.Add(new LineChief());
            
            roomclerks.Add(new RoomClerk());
        }
        public DiningRoomModel(int nbSquares, int nbLines, int nbTablesPerLine, int nbSeatsPerTable, int nbLineChiefs, int nbWaiters, int nbRoomClerks, int nbMenuCard, Dictionary<string, List<Recipe>> menu)
        {
            hotelMaster = new HotelMaster();

            squares = new List<Square>();

            roomclerks = new List<RoomClerk>();

            lineChiefs = new List<LineChief>();

            for (int i=0; i < nbSquares; i++)
                squares.Add(new Square());

            for (int i = 0; i < nbSquares; i++)
                for(int j=0; j < nbLines; j++)
                    squares[i].Lines.Add(new Line(nbTablesPerLine, nbSeatsPerTable));

            //squares[0].Lines.Add(new Line());
            for (int i = 0; i < nbSquares; i++)
                for(int j=0; j < nbLineChiefs; j++)
                    squares[i].lineChiefs.Add(new LineChief());

            for (int i = 0; i < nbWaiters; i++)
                squares[i].Waiters.Add(new Waiter());

            for (int i = 0; i < nbRoomClerks; i++)
                roomclerks.Add(new RoomClerk());


            menuCards = new Queue<MenuCard>();
            for (int i = 0; i < nbMenuCard; i++)
            {
                MenuCard menuCard = new MenuCard();
                menuCard.Menu = menu;
                menuCards.Enqueue(menuCard);
            }
        }
        public HotelMaster HotelMaster { get => hotelMaster; set => hotelMaster = value; }
        public  List<RoomClerk> RoomClerks { get => roomclerks; set => roomclerks = value; }
        public  List<Waiter> Waiters { get => waiters; set => waiters = value; }
        public  List<Square> Squares { get => squares; set => squares = value; }
        public  List<LineChief> LineChiefs { get => lineChiefs; set => lineChiefs = value; }

        public Queue<MenuCard> MenuCards { get => menuCards; set => menuCards = value; }
        public MenuCard MenuCard { get => menuCard; set => menuCard = value; }
        public Dictionary<string, List<Recipe>> Menu { get => menu; set => menu = value; }
    }
}
