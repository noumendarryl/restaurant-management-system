using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom;
using AppRestaurant.Model.DiningRoom.Move;
using AppRestaurant.Model.DiningRoom.Elements;
using AppRestaurant.Model.DiningRoom.Actors;

namespace AppRestaurant.Controller.DiningRoom.Actors
{
    class HotelMasterController
    {
        private HotelMaster hotelMaster;


        public HotelMasterController(HotelMaster hotelMaster)
        {
            this.hotelMaster = hotelMaster;
        }
        

        public Table CheckAvailableTables(Customer group, DiningRoomModel diningRoomModel)
        {
            int nbSquare = diningRoomModel.Squares.Count;
            for (int i = 0; i < nbSquare; i++)
            {
                int nbLine = diningRoomModel.Squares[i].Lines.Count;
                for (int j = 0; j < nbLine; j++)
                {
                    int nbTable = diningRoomModel.Squares[i].Lines[j].Tables.Count;
                    for (int k = 0; k < nbLine; k++)
                    {
                        if (group.Count < diningRoomModel.Squares[i].Lines[j].Tables[k].NbPlaces && diningRoomModel.Squares[i].Lines[j].Tables[k].State == EquipmentState.Available)
                        {
                            diningRoomModel.Squares[i].Lines[j].Tables[k].Group = group;
                            return diningRoomModel.Squares[i].Lines[j].Tables[k];
                        }
                    }
                }
            }
            return null; 
        }

        public LineChief FindLineChief(Customer group, DiningRoomModel diningRoomModel)
        {
            foreach(LineChief lineChief in diningRoomModel.LineChiefs)
            {
                if (lineChief.Available)
                    return lineChief;
            }
            return null;
        }
        // pan defVal posX = 10, posY
        public void CallRankChief(LineChief lineChief, Position adjust)
        {
            if (lineChief != null)
                //  lineChief.Move(hotelMaster.PosX - 10, hotelMaster.PosY);
                lineChief.Move(hotelMaster.PosX - adjust.PosX, hotelMaster.PosY - adjust.PosY);
        }

    }
}
