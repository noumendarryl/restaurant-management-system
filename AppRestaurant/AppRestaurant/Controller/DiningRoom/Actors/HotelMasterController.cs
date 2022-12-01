using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
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
        

        public bool CheckAvailableTables(Customer group)
        {
            return this.hotelMaster.RankChiefs.Exists(
                rankchief => rankchief.Squares[0].Lines[0].Tables.Exists(
                    table => (table.State == EquipmentState.Available)
                        && (table.NbPlaces >= group.Count)));
        }

        public RankChief FindRankChief(Customer group)
        {
            RankChief designatedRankchief = this.hotelMaster.RankChiefs.Find(
                rankchief => rankchief.Squares[0].Lines[0].Tables.Exists(
                    table => table.Group == group));
            return designatedRankchief;
        }

        public void CallRankChief(RankChief rankChief)
        {
            if (rankChief != null)
                rankChief.Move(hotelMaster.PosX - 10, hotelMaster.PosY);
        }

    }
}
