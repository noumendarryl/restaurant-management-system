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
        
        public Customer GenerateClient()
        {
            Customer client;
            Random random = new Random();
            int randomNumber = random.Next(1, 100);
            switch (randomNumber)
            {
                case int rn when (rn <= 20):
                    client = new Customer();
                    //client = ClientFactoryC.Instance.CreateClient();
                    break;

                case int rn when (rn > 20 && rn < 80):
                    client = new Customer();
                    //client = ClientFactoryA.Instance.CreateClient();
                    break;

                case int rn when (rn >= 80 && rn <= 100):
                    client = new Customer();
                    //client = ClientFactoryB.Instance.CreateClient();
                    break;

                default:
                    client = new Customer();
                    //client = ClientFactoryA.Instance.CreateClient();
                    break;
            }
        
            return client;
        }

        public Customer CreateGroup(int clientNumber)
        {
            Customer group = new Customer(clientNumber);
 
            return group;
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
