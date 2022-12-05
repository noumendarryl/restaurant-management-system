using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.DiningRoom.Actors;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.Common;

namespace AppRestaurant.Model.DiningRoom
{
    public class DiningRoomModel
    {
        private HotelMaster hotelMaster;
        private  List<RoomClerk> roomclerks;
        private  List<Waiter> waiters;
        private  List<Square> squares;
        private  List<LineChief> lineChiefs;
        private Menu menu; 
        private List<MenuCard> menuCards;


        public DiningRoomModel()
        {
            hotelMaster = new HotelMaster();
            
            squares = new List<Square>();

            roomclerks = new List<RoomClerk>();

            lineChiefs = new List<LineChief>();

            squares.Add(new Square());

            lineChiefs.Add(new LineChief());

            menuCards = new List<MenuCard>();

            for(int i = 0; i < 40; i++)
            {
                menuCards.Add(new MenuCard(menu));
            }

            squares[0].Lines.Add(new Line(4,6));
            //squares[0].Lines.Add(new Line());
            squares[0].Waiters.Add(new Waiter());

            squares[0].lineChiefs.Add(new LineChief());
            
            roomclerks.Add(new RoomClerk());
        }
        public DiningRoomModel(int nbSquares, int nbLines, int nbTablesPerLine, int nbSeatsPerTable, int nbLineChiefs, int nbWaiters, int nbRoomClerks, int nbMenuCard)
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


            menuCards = new List<MenuCard>();
            for (int i = 0; i < nbMenuCard; i++)
                menuCards.Add(new MenuCard(menu));
        }
        public HotelMaster HotelMaster { get => hotelMaster; set => hotelMaster = value; }
        public  List<RoomClerk> RoomClerks { get => roomclerks; set => roomclerks = value; }
        public  List<Waiter> Waiters { get => waiters; set => waiters = value; }
        public  List<Square> Squares { get => squares; set => squares = value; }
        public  List<LineChief> LineChiefs { get => lineChiefs; set => lineChiefs = value; }
        public  List<MenuCard> MenuCards { get => menuCards; set => menuCards = value; }
        public Menu Menu { get => menu; set => menu = value; }
    }
}
